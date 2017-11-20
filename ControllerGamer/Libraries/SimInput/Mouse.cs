using ControllerGamer.Libraries.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControllerGamer.Libraries.SimInput
{
    public static class Mouse
    {
        private static Int32 Coord_amp_x(int px)
        {
            return px * 65536 / DeviceUtils.ScreenSizeX;
        }
        private static Int32 Coord_amp_y(int py)
        {
            return py * 65536 / DeviceUtils.ScreenSizeY;
        }

        public static void Move(int pixel_x, int pixel_y)
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = Coord_amp_x(pixel_x);
            input[0].Data.Mouse.Y = Coord_amp_y(pixel_y);
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.Move | (UInt32)MouseFlag.Absolute;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));
        }
        public static void Move(Position pos)
        {
            Move(pos.X, pos.Y);
        }

        public static void Shift(int pixel_x, int pixel_y) // offset to the current position
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = pixel_x;
            input[0].Data.Mouse.Y = pixel_y;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.Move;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }

        public static void Shift(Position pos)
        {
            Shift(pos.X, pos.Y);
        }


        public static void LeftDown()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.LeftDown;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void LeftUp()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.LeftUp;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void LeftClick()
        {
            LeftDown();
            LeftUp();
        }
        public static void LeftClick(int pixel_x,int pixel_y)
        {
            Move(pixel_x, pixel_y);
            LeftDown();
            LeftUp();
        }

        public static void LeftDoubleClick()
        {
            LeftClick();
            LeftClick();
        }

        public static void LeftDoubleClick(int pixel_x, int pixel_y)
        {
            Move(pixel_x, pixel_y);
            LeftClick();
            LeftClick();
        }


        public static void RightDown()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.RightDown;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void RightUp()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.RightUp;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void RightClick()
        {
            RightDown();
            RightUp();
        }

        public static void RightClick(int pixel_x, int pixel_y)
        {
            Move(pixel_x, pixel_y);
            RightDown();
            RightUp();
        }

        public static void RightDoubleClick()
        {
            RightClick();
            RightClick();
        }
        public static void RightDoubleClick(int pixel_x, int pixel_y)
        {
            Move(pixel_x, pixel_y);
            RightClick();
            RightClick();
        }

        public static void MiddleDown()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.MiddleDown;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void MiddleUp()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.MiddleUp;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 0;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void MiddleClick()
        {
            MiddleDown();
            MiddleUp();
        }
        public static void MiddleClick(int pixel_x, int pixel_y)
        {
            Move(pixel_x, pixel_y);
            MiddleDown();
            MiddleUp();
        }

        public static void MiddleDoubleClick()
        {
            MiddleClick();
            MiddleClick();
        }
        public static void MiddleDoubleClick(int pixel_x, int pixel_y)
        {
            Move(pixel_x, pixel_y);
            MiddleClick();
            MiddleClick();
        }



        public static void X1Down()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.XDown;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 1;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void X1Up()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.XUp;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 1;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void X1Click()
        {
            X1Down();
            X1Up();
        }
        public static void X1DoubleClick()
        {
            X1Click();
            X1Click();
        }

        public static void X2Down()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.XDown;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 2;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void X2Up()
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.XUp;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = 2;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }
        public static void X2Click()
        {
            X2Down();
            X2Up();
        }
        public static void X2DoubleClick()
        {
            X2Click();
            X2Click();
        }
        public static void VerticalScroll(int neg_or_pos_amount)
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.VerticalWheel;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = neg_or_pos_amount;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }

        public static void HorizontalScroll(int neg_or_pos_amount)
        {
            INPUT[] input = new INPUT[1];
            input[0].Type = (UInt32)InputType.Mouse;
            input[0].Data.Mouse.X = 0;
            input[0].Data.Mouse.Y = 0;
            input[0].Data.Mouse.Flags = (UInt32)MouseFlag.HorizontalWheel;
            input[0].Data.Mouse.Time = 0;
            input[0].Data.Mouse.MouseData = neg_or_pos_amount;
            input[0].Data.Mouse.ExtraInfo = UIntPtr.Zero;
            WinAPI.SendInput(1, input, 28);
        }

    }
}
