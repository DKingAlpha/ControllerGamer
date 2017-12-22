using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ControllerGamer.Libraries.ProfileLoader;
using Sanford.Multimedia;
using Sanford.Multimedia.Midi;

//https://www.codeproject.com/Articles/6228/C-MIDI-Toolkit
//https://github.com/tebjan/Sanford.Multimedia.Midi/blob/master/Demo/MidiWatcher/Form1.cs
//https://github.com/tebjan/Sanford.Multimedia.Midi/blob/master/Demo/SequencerDemo/Form1.cs

namespace ControllerGamer.Libraries.Controllers
{
    internal class MIDI : Controller
    {
        private int DeviceID = -1;
        private InputDevice midiinput = null;
        private event ControllerEventHandler EventReceived;
        private bool Running = false;
        private string Name = null;

        private void inDevice_Error(object sender, ErrorEventArgs e)
        {
            Logger.Log(e.Error.Message);
        }

        public void Dispose()
        {
            if(midiinput!=null) midiinput.Dispose();
        }

        private void HandlerAnyMessageReceived(object sender, EventArgs e)
        {
            EventReceived(new MidiEventArgs((InputDevice)sender,e,Name));
        }

        public MIDI(int id)
        {
            DeviceID = id;
        }


        public string GetDetail()
        {
            MidiInCaps midiincap = InputDevice.GetDeviceCapabilities(DeviceID);

            string res = "";
            res = res + "ControllerName: " + midiincap.name + "\r\n";
            res = res + "DriverVersion: " + midiincap.driverVersion.ToString() + "\r\n";
            res = res + "ManufacturerID: " + midiincap.mid.ToString() + "\r\n";
            res = res + "ProductID: " + midiincap.pid.ToString() + "\r\n";
            res = res + "SupportID: " + midiincap.support.ToString() + "\r\n";

            return res;
        }

        public string GetProductName()
        {
            return InputDevice.GetDeviceCapabilities(DeviceID).name;
        }

        public bool Start()
        {
            if (!Running)
            {
                try
                {
                    Name = InputDevice.GetDeviceCapabilities(DeviceID).name;

                    midiinput = new InputDevice(DeviceID);
                    midiinput.Error += inDevice_Error;
                    midiinput.ChannelMessageReceived += HandlerAnyMessageReceived;
                    midiinput.SysCommonMessageReceived += HandlerAnyMessageReceived;
                    midiinput.SysExMessageReceived += HandlerAnyMessageReceived;
                    midiinput.SysRealtimeMessageReceived += HandlerAnyMessageReceived;
                    
                    midiinput.StartRecording();
                    Running = true;
                    return true;
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    Logger.Log(e.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Stop()
        {
            if (Running)
            {
                try
                {
                    midiinput.StopRecording();

                    midiinput.Error -= inDevice_Error;
                    midiinput.ChannelMessageReceived -= HandlerAnyMessageReceived;
                    midiinput.SysCommonMessageReceived -= HandlerAnyMessageReceived;
                    midiinput.SysExMessageReceived -= HandlerAnyMessageReceived;
                    midiinput.SysRealtimeMessageReceived -= HandlerAnyMessageReceived;

                    midiinput.Close();
                    Running = false;
                    return true;
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    Logger.Log(e.StackTrace);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void MapToProfile(Profile profile)
        {
            if (!profile.IsRunning)
                profile.Start();
            if (profile.IsRunning)
                EventReceived += profile.OnEventReceived;
        }

        public void UnMapToProfile(Profile profile)
        {
            if (!profile.IsRunning)
                profile.Start();
            if (profile.IsRunning)
                EventReceived -= profile.OnEventReceived;
        }

    }

}
