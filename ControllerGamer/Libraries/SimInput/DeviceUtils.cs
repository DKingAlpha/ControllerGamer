using ControllerGamer.Libraries.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControllerGamer.Libraries.SimInput
{
    public static class DeviceUtils
    {
        public static bool IsKeyDown(VK keyCode)
        {
            Int16 result = WinAPI.GetKeyState((UInt16)keyCode);
            return (result < 0);
        }

        public static bool IsKeyUp(VK keyCode)
        {
            return !IsKeyDown(keyCode);
        }

        public static bool IsHardwareKeyDown(VK keyCode)
        {
            var result = WinAPI.GetAsyncKeyState((UInt16)keyCode);
            return (result < 0);
        }

        public static bool IsHardwareKeyUp(VK keyCode)
        {
            return !IsHardwareKeyDown(keyCode);
        }


        public static bool IsTogglingKeyInEffect(VK keyCode)
        {
            Int16 result = WinAPI.GetKeyState((UInt16)keyCode);
            return (result & 0x01) == 0x01;
        }

        public static Position MousePosition
        {
            get
            {
                Position pos;
                pos.X = Control.MousePosition.X;
                pos.Y = Control.MousePosition.Y;
                return pos;
            }
            set
            {
                Mouse.Move(value.X, value.Y);

            }
        }

        public static int ScreenSizeX { get { return Screen.PrimaryScreen.Bounds.Width; } set { } }
        public static int ScreenSizeY { get { return Screen.PrimaryScreen.Bounds.Height; } set { } }

        

        public static int MousePositionX => Control.MousePosition.X;

        public static int MousePositionY => Control.MousePosition.Y;
        
    }
}
