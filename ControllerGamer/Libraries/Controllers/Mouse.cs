﻿using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControllerGamer.Libraries.ProfileLoader;
using System.Threading;

namespace ControllerGamer.Libraries.Controllers
{
    class Mouse : SharpDX.DirectInput.Mouse, Controller
    {
        private Thread thread = null;
        private bool Running = false;

        event ControllerEventHandler EventReceived;


        public Mouse() : base(new DirectInput())
        {
            Properties.BufferSize = 2048;
        }

        public string GetDetail()
        {
            string res = "";
            res = res + "\tControllerName\t" + Information.ProductName;
            res = res + "\tType\t\t" + Information.Type;
            res = res + "\tProductGuid\t" + Information.ProductGuid;
            res = res + "\tPovCount\t" + Capabilities.PovCount;
            res = res + "\tAxeCount\t" + Capabilities.AxeCount;
            res = res + "\tButtonCount\t" + Capabilities.ButtonCount;
            res = res + "\tFlags\t\t" + Capabilities.Flags;

            return res;
        }

        public string GetProductName()
        {
            return Information.ProductName;
        }
        private void _run()
        {
            while (Running == true)
            {
                Poll();
                var datas = GetBufferedData();
                foreach (var state in datas)
                {
                    EventReceived?.Invoke(new DMouseEventArgs(state));
                }
                Thread.Sleep(10);
            }
        }

        public bool Start()
        {
            if (thread == null)
            {
                Acquire();
                Running = true;
                thread = new Thread(_run);
                thread.IsBackground = true;
                thread.Start();
                return true;
            }
            return false;
        }

        public bool Stop()
        {
            if (thread != null)
            {
                Running = false;
                Unacquire();
                return true;
            }
            else
                return false;
        }
        public void MapToProfile(Profile profile)
        {
            if (profile.Compile())
                EventReceived += profile.OnEventReceived;
        }
        public void UnMapToProfile(Profile profile)
        {
            if (profile.IsCompiled)
                EventReceived -= profile.OnEventReceived;
        }
    }
}
