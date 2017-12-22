using ControllerGamer.Libraries.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ControllerGamer.Libraries.SimInput
{
    public static class Keyboard
    {
        /// <summary>
        /// Switch to use scan codes all the time without using any virtual key codes.
        /// Change this if a game cannot receive simulated inputs.
        /// </summary>
        public static bool FullScanCodeMode = true;


        /// <summary>
        /// http://www.computer-engineering.org/ps2keyboard/scancodes2.html
        /// </summary>
        private static VK[] extendkeys =  {
            VK.LWIN, VK.RWIN, VK.RCONTROL, VK.RALT, VK.APPS,
            VK.INSERT, VK.DELETE, VK.HOME, VK.END, VK.PAGEUP, VK.PAGEDOWN,
            VK.RIGHT, VK.UP, VK.LEFT, VK.DOWN,
            VK.SCROLL, VK.PAUSE, VK.PRINTSCREEN, VK.DIVIDE
        };
        private static bool IsExtendedKey(VK virtualKeyCode)
        {

            if (extendkeys.Contains(virtualKeyCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private static void InputDataFactory(ref INPUT input, VK virtualKeyCode,bool Up=false)
        {
            input.Type = (UInt32)InputType.Keyboard;
            input.Data.Keyboard.Time = 0;
            if (FullScanCodeMode)
            {
                input.Data.Keyboard.Scan = WinAPI.GetScanCode(virtualKeyCode);
                input.Data.Keyboard.KeyCode = 0;
                input.Data.Keyboard.Time = 0;
                input.Data.Keyboard.ExtraInfo = UIntPtr.Zero;
                input.Data.Keyboard.Flags = (UInt32)KeyboardFlag.ScanCode | (Up ? (UInt32)KeyboardFlag.KeyUp : 0) | (IsExtendedKey(virtualKeyCode) ? (UInt32)KeyboardFlag.ExtendedKey:0);
            }
            else
            {
                input.Data.Keyboard.KeyCode = (UInt16)virtualKeyCode;
                input.Data.Keyboard.Scan = 0;
                input.Data.Keyboard.Time = 0;
                input.Data.Keyboard.ExtraInfo = UIntPtr.Zero;
                input.Data.Keyboard.Flags = (Up ? (UInt32)KeyboardFlag.KeyUp : 0) | (IsExtendedKey(virtualKeyCode) ? (UInt32)KeyboardFlag.ExtendedKey : 0);
            }
        }


        public static bool IsKeyDown(VK keyCode)
        {
            var result = WinAPI.GetAsyncKeyState((UInt16)keyCode);
            return (result < 0);
        }

        public static bool IsKeyUp(VK keyCode)
        {
            return !IsKeyDown(keyCode);
        }


        public static void KeyDown(VK virtualKeyCode)
        {
            INPUT[] input = new INPUT[1];
            InputDataFactory(ref input[0],virtualKeyCode);
            WinAPI.SendInput(1, input,  Marshal.SizeOf(typeof (INPUT)));
        }

        public static void KeyDown(params VK[] virtualKeyCodes)
        {
            INPUT[] input = new INPUT[virtualKeyCodes.Length];
            for (int i=0;i<virtualKeyCodes.Length;i++)
            {
                InputDataFactory(ref input[0], virtualKeyCodes[i]);
            }
            WinAPI.SendInput((UInt32)input.Length, input,  Marshal.SizeOf(typeof (INPUT)));
        }


        public static void KeyUp(VK virtualKeyCode)
        {
            INPUT[] input = new INPUT[1];
            InputDataFactory(ref input[0], virtualKeyCode, Up: true);
            WinAPI.SendInput(1, input,  Marshal.SizeOf(typeof (INPUT)));
        }
        public static void KeyUp(params VK[] virtualKeyCodes)
        {
            INPUT[] input = new INPUT[virtualKeyCodes.Length];
            for (int i = 0; i < virtualKeyCodes.Length; i++)
            {
                InputDataFactory(ref input[i], virtualKeyCodes[i], Up: true);
            }
            WinAPI.SendInput((UInt32)input.Length, input,  Marshal.SizeOf(typeof (INPUT)));
        }

        /// <summary>
        /// Sequentially press keys one by one
        /// </summary>
        /// <param name="virtualKeyCode"></param>
        public static void KeyPress(VK virtualKeyCode)
        {
            INPUT[] input = new INPUT[2];
            InputDataFactory(ref input[0], virtualKeyCode);
            WinAPI.SendInput(1, input,  Marshal.SizeOf(typeof (INPUT)));
            InputDataFactory(ref input[1], virtualKeyCode, Up: true);
            WinAPI.SendInput(1, new INPUT[]{ input[1]},  Marshal.SizeOf(typeof (INPUT)));
        }
        public static void KeyPress(params VK[] virtualKeyCodes)
        {
            INPUT[] input = new INPUT[2*virtualKeyCodes.Length];
            for (int i = 0; i < virtualKeyCodes.Length; i++)
            {
                InputDataFactory(ref input[2*i], virtualKeyCodes[i]);
                InputDataFactory(ref input[2*i+1], virtualKeyCodes[i], Up: true);
            }
            WinAPI.SendInput((UInt32)input.Length, input,  Marshal.SizeOf(typeof (INPUT)));
        }


        /// <summary>
        /// Press down keys one by one , then release them in the reverse order.
        /// For example, ALT+TAB requires ALT held first and TAB later. KeyPress doesnot work in this situation but KeyCombination does.
        /// </summary>
        /// <param name="virtualKeyCodes"></param>
        public static void KeyCombination(params VK[] virtualKeyCodes)
        {
            INPUT[] input = new INPUT[2 * virtualKeyCodes.Length];
            for (int i = 0; i < virtualKeyCodes.Length; i++)
            {
                InputDataFactory(ref input[i], virtualKeyCodes[i]);
            }
            for (int i = virtualKeyCodes.Length; i < 2 * virtualKeyCodes.Length ; i++)
            {
                InputDataFactory(ref input[i], virtualKeyCodes[2 * virtualKeyCodes.Length - 1 - i], Up: true);
            }
            WinAPI.SendInput((UInt32)input.Length, input,  Marshal.SizeOf(typeof (INPUT)));

        }


        public static void Text(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            INPUT[] input = new INPUT[2*bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                input[2 * i].Type = (UInt32)InputType.Keyboard;
                input[2 * i].Data.Keyboard.Scan = bytes[i];
                input[2 * i].Data.Keyboard.KeyCode = 0;
                input[2 * i].Data.Keyboard.Time = 0;
                input[2 * i].Data.Keyboard.ExtraInfo = UIntPtr.Zero;
                input[2 * i].Data.Keyboard.Flags = (UInt32)KeyboardFlag.Unicode;

                input[2 * i + 1].Type = (UInt32)InputType.Keyboard;
                input[2 * i + 1].Data.Keyboard.Scan = bytes[i];
                input[2 * i + 1].Data.Keyboard.KeyCode = 0;
                input[2 * i + 1].Data.Keyboard.Time = 0;
                input[2 * i + 1].Data.Keyboard.ExtraInfo = UIntPtr.Zero;
                input[2 * i + 1].Data.Keyboard.Flags = (UInt32)KeyboardFlag.Unicode | (UInt32)KeyboardFlag.KeyUp;


                if ((bytes[i] & 0xFF00) == 0xE000)
                {
                    input[2 * i].Data.Keyboard.Flags |= (UInt32)KeyboardFlag.ExtendedKey;
                    input[2 * i + 1].Data.Keyboard.Flags |= (UInt32)KeyboardFlag.ExtendedKey;
                }

            }

            WinAPI.SendInput((UInt32)input.Length, input, Marshal.SizeOf(typeof(INPUT)));

        }
    }
}
