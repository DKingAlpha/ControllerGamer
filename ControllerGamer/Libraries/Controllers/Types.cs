using SharpDX.DirectInput;
using System;

namespace ControllerGamer.Libraries.Controllers
{
    public class StickStatus
    {
        public int X = 32767, Y = 32767, Z = 32767, RX = 32767, RY = 32767, RZ = 32767;

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
            return String.Format("Button ID:{0} {1} \r\n", ID, Pressed ? "Pressed" : "Released");
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
        public int ID = -1;
        public readonly bool Pressed, Released;

        public override string ToString()
        {
            return String.Format("Button ID:{0} {1} \r\n", ID, Pressed ? "Pressed" : "Released");
        }

        public DKeyboardEventArgs(KeyboardUpdate state) : base(state)
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
    public class DMouseEventArgs : ControllerEventArgs
    {
        public int ID = -1;
        public readonly bool Pressed, Released;

        public override string ToString()
        {
            return String.Format("Button ID:{0} {1} \r\n", ID, Pressed ? "Pressed" : "Released");
        }

        public DMouseEventArgs(MouseUpdate state) : base(state)
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

    public class ControllerEventArgs : EventArgs
    {
        public JoystickUpdate RawState { get; }
        public ControllerEventArgs(JoystickUpdate state) : base()
        {
            RawState = state;
        }
        public ControllerEventArgs(KeyboardUpdate state) : base()
        {
           // RawState = state;
        }
        public ControllerEventArgs(MouseUpdate state) : base()
        {
            // RawState = state;
        }

        public override string ToString()
        {
            return RawState.ToString();
        }
    }
}
