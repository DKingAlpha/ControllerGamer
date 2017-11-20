using ControllerGamer.Libraries.ProfileLoader;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ControllerGamer.Libraries.Controllers
{
    internal class Controller : Joystick
    {
        private Thread thread = null;
        private bool Running = false;



        private StickStatus LastStickStatus = new StickStatus();


        public delegate void ControllerEventHandler(ControllerEventArgs e);
        public event ControllerEventHandler EventReceived;

        public Controller(Guid guid) : base(new DirectInput(),guid)
        {
            Properties.BufferSize = 1024;
        }
        
        private void _run()
        {
            List<int> helper_sticks = new List<int>() { 0, 4, 8, 12, 16, 20 };
            List<int> helper_pov = new List<int>() { 32,36,40,44 };

            while (Running == true)
            {
                Poll();
                JoystickUpdate[] datas = GetBufferedData();
                foreach (JoystickUpdate state in datas)
                {
                    if (EventReceived != null)
                    {
                        ControllerEventArgs cea = null;

                        if (helper_sticks.Contains(state.RawOffset)) cea = new StickEventArgs(state, LastStickStatus);
                        if (helper_pov.Contains(state.RawOffset)) cea = new DPadEventArgs(state);
                        if (state.RawOffset >=48 && state.RawOffset <=175) cea = new ButtonEventArgs(state);

                        if (cea==null) cea = new ControllerEventArgs(state);

                        EventReceived(cea);
                    }
                }
                Thread.Sleep(10);
            }
        }


        public bool Start()
        {
            if (thread == null)
            {
                Acquire();
                // restart. previous thread will automatically exit.
                Running = true;
                // start a new thread
                thread = new Thread(_run);
                thread.Start();
                return true;
            }
            return false ;
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
            if(profile.Compile())
                EventReceived += profile.OnEventReceived;
        }
        public void UnMapToProfile(Profile profile)
        {
            if (profile.IsCompiled)
                EventReceived -= profile.OnEventReceived;
        }
    }
}
