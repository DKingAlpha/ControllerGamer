using System;
using System.Runtime.InteropServices;

namespace ControllerGamer.Libraries.Windows
{

    internal static class WinAPI
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int16 GetAsyncKeyState(UInt16 virtualKeyCode);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern Int16 GetKeyState(UInt16 virtualKeyCode);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 SendInput(UInt32 numberOfInputs, INPUT[] inputs, Int32 sizeOfInputStructure);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        public static extern UInt32 MapVirtualKey(UInt16 uCode, UInt32 uMapType);
        

        public static UInt16 GetScanCode(VK uCode)
        {
            return (UInt16)MapVirtualKey((UInt16)uCode, (UInt32)MapVirtualKeyMapTypes.MAP_VK_TO_VSC);
        }


        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        public extern static int FormatMessage(int flag, ref IntPtr source, int msgid, int langid, ref string buf, int size, ref IntPtr args);
        public static string GetSysErrMsg(int errCode)
        {
            IntPtr tempptr = IntPtr.Zero;
            string msg = null;
            FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);
            return msg;
        }
    }
}
