using Sanford.Multimedia.Midi;
using SharpDX.DirectInput;
using System;

namespace ControllerGamer.Libraries.Controllers
{
    public class StickStatus
    {
        public int X = -1, Y = -1, Z = -1, RX = -1, RY = -1, RZ = -1;

        public override string ToString()
        {
            return String.Format("X:{0} Y:{1} Z:{2} RX:{3} RY:{4} RZ:{5}", X,Y,Z,RX,RY,RZ);
        }

    }

    public class StickEventArgs : ControllerEventArgs
    {
        public StickStatus StickStatus = null;

        public int X = 0, Y = 0, Z = 0, RX = 0, RY = 0, RZ = 0;


        public override string ToString()
        {
            return StickStatus.ToString();
        }
        
        public StickEventArgs(JoystickUpdate state, StickStatus last_stickstatus) : base(state)
        {
            StickStatus = last_stickstatus;
            switch (state.Offset)
            {
                case JoystickOffset.X:
                    StickStatus.X = state.Value;
                    break;
                case JoystickOffset.Y:
                    StickStatus.Y = state.Value;
                    break;
                case JoystickOffset.Z:
                    StickStatus.Z = state.Value;
                    break;
                case JoystickOffset.RotationX:
                    StickStatus.RX = state.Value;
                    break;
                case JoystickOffset.RotationY:
                    StickStatus.RY = state.Value;
                    break;
                case JoystickOffset.RotationZ:
                    StickStatus.RZ= state.Value;
                    break;
            }
        }
    }
    public class DPadEventArgs : ControllerEventArgs
    {

        public bool UP = false, DOWN = false, RIGHT = false, LEFT = false, Released = false;

        public override string ToString()
        {
            string output = "DPad ";
            if (UP) output += "UP ";
            if (DOWN) output += "DOWN ";
            if (RIGHT) output += "RIGHT ";
            if (LEFT) output += "LEFT ";
            if (Released) output += "Released ";
            return output;
        }


        public DPadEventArgs(JoystickUpdate state) : base(state)
        {
            if (state.Value == -1)
            {
                Released = true;
                return;
            }
            switch (state.Value / 4500)
            {
                case 0:
                    UP = true;
                    break;
                case 1:
                    UP = true;
                    RIGHT = true;
                    break;
                case 2:
                    RIGHT = true;
                    break;
                case 3:
                    RIGHT = true;
                    DOWN = true;
                    break;
                case 4:
                    DOWN = true;
                    break;
                case 5:
                    DOWN = true;
                    LEFT = true;
                    break;
                case 6:
                    LEFT = true;
                    break;
                case 7:
                    LEFT = true;
                    UP = true;
                    break;
            }
        }
    }
    public class ButtonEventArgs : ControllerEventArgs
    {
        public int ID = -1;
        public readonly bool Pressed, Released;

        public override string ToString()
        {
            return String.Format("Button ID:{0} {1}", ID, Pressed ? "Pressed" : "Released");
        }

        public ButtonEventArgs(JoystickUpdate state) : base(state)
        {
            ID = state.RawOffset - 48;
            if (state.Value == 0)
            {
                Released = true;
                Pressed = false;
            }
            else
            {
                Pressed = true;
                Released = false;
            }
        }
    }

    public class DKeyboardEventArgs : ControllerEventArgs
    {
        public Key Key;
        public readonly bool Pressed, Released;

        public override string ToString()
        {
            return String.Format("Key:{0} {1}", Key.ToString(), Pressed ? "Pressed" : "Released");
        }

        public DKeyboardEventArgs(KeyboardUpdate state) : base(state)
        {
            Key = state.Key;
            Pressed = state.IsPressed;
            Released = state.IsReleased;
        }
    }
    public class DMouseEventArgs : ControllerEventArgs
    {
        public MouseOffset Offset;
        public readonly int Value;
        public readonly bool Pressed, Released;
        public readonly int X, Y;

        public override string ToString()
        {
            return String.Format("Offset:{0} {1} Position: {2} {3}", Offset, Pressed ? "Pressed" : "Released",X ,Y);
        }

        public DMouseEventArgs(MouseUpdate state) : base(state)
        {
            Offset = state.Offset;
            Value = state.Value;

            X = System.Windows.Forms.Control.MousePosition.X;
            Y = System.Windows.Forms.Control.MousePosition.Y;

            if (state.Value == 0)
            {
                Released = true;
                Pressed = false;
            }
            else
            {
                Pressed = true;
                Released = false;
            }
        }
        
    }
    public class MidiEventArgs : ControllerEventArgs
    {
        public readonly bool Pressed, Released;
        public readonly InputDevice Device;
        public readonly int Note = -1;
        public readonly int Channel = -1;
        public readonly int Velocity = -1;
        public readonly int DeviceID = -1;
        public MidiEventArgs(InputDevice sender, EventArgs e) : base(e)
        {
            Device = sender;
            if (e is ChannelMessageEventArgs msg)
            {
                Channel = msg.Message.MidiChannel;
                Note = msg.Message.Data1;
                Velocity = msg.Message.Data2;
                DeviceID = Device.DeviceID;
                if (msg.Message.Data2 > 0)
                {
                    Pressed = true;
                    Released = false;
                }
                else
                {
                    Pressed = true;
                    Released = false;
                }
            }
        }

        public override string ToString()
        {
            return Note != -1 ? 
                String.Format("Note:{0} {1}   Velocity:{2}", Note.ToString(), Pressed ? "Pressed" : "Released", ((ChannelMessageEventArgs)RawState).Message.Data2) 
                :
                GetDetail();
        }

        public string GetDetail()
        {
            string res = "";
            if (RawState is ChannelMessageEventArgs)
            {
                ChannelMessageEventArgs msg = (ChannelMessageEventArgs)RawState;
                res += "MessageType: " + msg.Message.MessageType.ToString() + "\t";
                res += "Command: " + msg.Message.Command.ToString() + "\t";
                res += "Channel: " + msg.Message.MidiChannel.ToString() + "\t";
                res += "Note: " + msg.Message.Data1.ToString() + "\t";
                res += "Velocity: " + msg.Message.Data2.ToString();
            }
            if (RawState is SysExMessageEventArgs)
            {
                SysExMessageEventArgs msg = (SysExMessageEventArgs)RawState;
                res += "MessageType: " + msg.Message.MessageType.ToString() + "\t";
                res += "SysExType: " + msg.Message.SysExType.ToString() + "\t";
                res += "RawBytes(Hex): ";
                foreach (byte b in msg.Message)
                    res += string.Format("{0:X2} ", b);
            }

            if (RawState is SysCommonMessageEventArgs)
            {
                SysCommonMessageEventArgs msg = (SysCommonMessageEventArgs)RawState;
                res += "MessageType: " + msg.Message.MessageType.ToString() + "\t";
                res += "SysCommonType: " + msg.Message.SysCommonType.ToString() + "\t";
                res += msg.Message.Data1.ToString() + msg.Message.Data2.ToString();
                res += msg.Message.Data2.ToString();
            }

            if (RawState is SysRealtimeMessageEventArgs)
            {
                SysRealtimeMessageEventArgs msg = (SysRealtimeMessageEventArgs)RawState;
                res += msg.Message.MessageType.ToString() + "\t";
                res += "SysRealtimeType: " + msg.Message.SysRealtimeType.ToString();
            }

            return res;
        }
    }


    public class ControllerEventArgs : EventArgs
    {
        public object RawState { get; }
        public object AdditionalData = null;
        
        public ControllerEventArgs(JoystickUpdate state) : base()
        {
            RawState = state;
        }
        public ControllerEventArgs(KeyboardUpdate state) : base()
        {
            RawState = state;
        }
        public ControllerEventArgs(MouseUpdate state) : base()
        {
             RawState = state;
        }

        public ControllerEventArgs(EventArgs state) : base()
        {
            RawState = state;
        }

        public override string ToString()
        {
            return RawState.ToString();
        }
    }
}
