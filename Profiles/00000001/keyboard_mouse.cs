using System;
using System.IO;
using System.Text;
using System.Threading;
using ControllerGamer.Libraries;
using ControllerGamer.Libraries.Windows;
using ControllerGamer.Libraries.SimInput;
using ControllerGamer.Libraries.Controllers;

using Key = SharpDX.DirectInput.Key;

namespace GameProfile
{
    public class ControllerCallbacks
    {
        private int ls_x, ls_y;
        private int rs_x, rs_y;
        private bool Shifting = false;
        private Launchpad lpd = null;
        
        // Constructor
        public ControllerCallbacks()
        {
            Logger.Log("Running Profile for League of Legends...");
            
            Keyboard.FullScanCodeMode = true;   // virtual key codes might not works to some game settings, so scan mode instead.

        }
        
        public void OnKeyboardEventReceived(DKeyboardEventArgs e)
        {
            Logger.Log(e);
            if(e.Released)
            {
                switch(e.Key)
                {
                    case Key.A:                             // Type: Key
                        Keyboard.KeyPress(VK.VK_K);         // Type: VK
                                         // Alert: if VK_A here, it will trigger a closed loop
                                         // Reason: KeyPress 'A' generates a Release Event at last, which triggers KeyDown 'A' again.
                                         // This can not be distinguish by DeviceUtils.IsHardwareKeyDown.
                                         // Solution: add if statement before KeyPress, if there might be a collision
                        break;
                    case Key.Space:
                        double px = 0.5 * DeviceUtils.ScreenSizeX;
                        double py = 0.5 * DeviceUtils.ScreenSizeY;
                        Mouse.RightClick((int)px,(int)py);
                }
            }
            
        }
        
        public void OnMouseEventReceived(DMouseEventArgs e)
        {
            Logger.Log(e);
        }
        
        
        public void OnEventReceived(ControllerEventArgs e)
		{
            // Show pther unhandled events
            Logger.Log(e);
        }
    }
    
    
}
