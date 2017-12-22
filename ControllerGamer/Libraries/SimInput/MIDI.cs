using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControllerGamer.Libraries.SimInput
{
    public static class MIDIs
    {
        private static List<MIDI> LoadDevices = new List<MIDI>();

        public static MIDI Get(int id)
        {
            foreach (var Device in LoadDevices)
            {
                if (Device.DeviceID == id) return Device;
            }
            if (id < OutputDevice.DeviceCount)
            {
                MIDI newdevice = new MIDI(id);
                LoadDevices.Add(newdevice);
                return newdevice;
            }
            else
                return null;
        }

        public static MIDI Get(string productname)
        {
            foreach (var Device in LoadDevices)
            {
                if (Device.GetProductName().StartsWith(productname)) return Device;
            }
            for (int i = 0; i < OutputDevice.DeviceCount; i++)
            {
                MidiOutCaps midioutcap = OutputDevice.GetDeviceCapabilities(i);
                if (midioutcap.name.StartsWith(productname))
                {
                    MIDI newdevice = new MIDI(i);
                    LoadDevices.Add(newdevice);
                    return newdevice;
                }
            }
            return null;
        }

        public static string ListAll()
        {
            string res = "MidiOut Devices:\r\n\r\n";

            for (int i = 0; i < OutputDevice.DeviceCount; i++)
            {
                MidiOutCaps midioutcap = OutputDevice.GetDeviceCapabilities(i);

                res = res + "ControllerName: " + midioutcap.name + "\r\n";
                res = res + "DriverVersion: " + midioutcap.driverVersion.ToString() + "\r\n";
                res = res + "ManufacturerID: " + midioutcap.mid.ToString() + "\r\n";
                res = res + "ProductID: " + midioutcap.pid.ToString() + "\r\n";
                res = res + "SupportID: " + midioutcap.support.ToString() + "\r\n";

                res = res + "\r\n";
            }
            return res;
        }

    }

    public class MIDI
    {
        public OutputDevice Device = null;
        public int DeviceID = -1;
        public Sequencer seqr = new Sequencer();
        public string BasePath = null;

        public MIDI(int deviceid)
        {
            DeviceID = deviceid;
            Start();
        }
        public string GetDetail()
        {
            MidiOutCaps midioutcap = OutputDevice.GetDeviceCapabilities(DeviceID);

            string res = "";
            res = res + "ControllerName: " + midioutcap.name + "\r\n";
            res = res + "DriverVersion: " + midioutcap.driverVersion.ToString() + "\r\n";
            res = res + "ManufacturerID: " + midioutcap.mid.ToString() + "\r\n";
            res = res + "ProductID: " + midioutcap.pid.ToString() + "\r\n";
            res = res + "SupportID: " + midioutcap.support.ToString() + "\r\n";

            return res;
        }

        public string GetProductName()
        {
            return OutputDevice.GetDeviceCapabilities(DeviceID).name;
        }

        public void Start()
        {
            if (DeviceID != -1)
            {
                Device = new OutputDevice(DeviceID);
                Device.RunningStatusEnabled = true;

                seqr.Sequence = new Sequence();
                seqr.ChannelMessagePlayed += OnChannelMessagePlayed;
                seqr.SysExMessagePlayed += OnSysExMessagePlayed;
            }
        }
        

        public void Send(params byte[] bytes)
        {
            if (bytes.Length <= 4)
            {
                int msg = 0;
                for(int i=0;i<bytes.Length;i++)
                {
                    msg += bytes[i] << (i * 8);
                }
                Device.SendShort(msg);
            }
            else
            {
                Device.Send(new SysExMessage(bytes));
            }
        }

        public void Stop()
        {
            if (DeviceID != -1)
            {
                seqr.Sequence.Clear();
                seqr.Sequence.Dispose();
                seqr.ChannelMessagePlayed -= OnChannelMessagePlayed;
                seqr.SysExMessagePlayed -= OnSysExMessagePlayed;
                Device.Close();
                Device.Dispose();
                Device = null;
                DeviceID = -1;
            }
        }



        public void PlayMidiFile(string filename)
        {
            seqr.Sequence.Clear();
            seqr.Sequence.Load(BasePath + @"\" + filename);
            seqr.Start();
        }

        public void OnSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
            if (DeviceID != -1)
            {
                Device.Send(e.Message);
            }
        }

        public void OnChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            if (DeviceID != -1)
            {
                Device.Send(e.Message);
            }
        }
    }


}
