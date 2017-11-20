using System;
using System.IO;
using System.Text;
using System.Threading;
using ControllerGamer.Libraries;
using ControllerGamer.Libraries.Windows;
using ControllerGamer.Libraries.SimInput;
using ControllerGamer.Libraries.Controllers;

using EventType = SharpDX.DirectInput.JoystickOffset;

namespace GameProfile
{
    public class ControllerCallbacks
    {
        private int ls_x, ls_y;
        private int rs_x, rs_y;
        private bool Shifting = false;
        
        // Constructor
        public ControllerCallbacks()
        {
            //Logger.Log("Running Profile for League of Legends...");
            new Thread(Shift).Start();
            
        }
        
        public void OnStickEventReceived(StickEventArgs stick_event)
        {
            //Logger.Log(stick_event);
            ls_x = stick_event.StickStatus.X;
            ls_y = stick_event.StickStatus.Y;
            
            rs_x = stick_event.StickStatus.RX;
            rs_y = stick_event.StickStatus.RY;
            
            if(Math.Abs(ls_x-32767)<5000 && Math.Abs(ls_y-32767)<5000)
            {
                Keyboard.KeyPress(VK.VK_S);
                //Mouse.Move(1920/2,1080/2);
            }
            else
            {
                double px = (ls_x / 65535.0) * DeviceUtils.ScreenSizeX * 0.5 * (DeviceUtils.ScreenSizeY/DeviceUtils.ScreenSizeX) + (0.5*DeviceUtils.ScreenSizeX - 0.25 * DeviceUtils.ScreenSizeY);
                double py = (ls_y / 65535.0) * DeviceUtils.ScreenSizeY * 0.5 + 0.25* DeviceUtils.ScreenSizeY ;
                Mouse.RightClick((int)px,(int)py);
            }
            
            if(Math.Abs(rs_x-32767)>500 || Math.Abs(rs_y-32767)>500)
            {
                Shifting = true;
            }
            else
            {
                Shifting = false;
            }
        
        }
        
        public void OnDPadEventReceived(DPadEventArgs dpad)
        {
            //Logger.Log(dpad);
            if(dpad.UP)Keyboard.KeyPress(VK.VK_1);
            if(dpad.DOWN)Keyboard.KeyPress(VK.VK_2);
            if(dpad.LEFT)Keyboard.KeyPress(VK.VK_3);
            if(dpad.RIGHT)Keyboard.KeyPress(VK.VK_4);
        }
        
        public void OnButtonEventReceived(ButtonEventArgs button)
        {
            if(button.Released)return;
            // Pressed => KeyPress[KeyDown,KeyUp]
            //Logger.Log(button);
            switch(button.ID)
            {
                case 0: // A
                    Keyboard.KeyPress(VK.VK_E);
                    break;
                case 1: // B
                    Keyboard.KeyPress(VK.VK_R);
                    break;
                case 2: // X
                    Keyboard.KeyPress(VK.VK_Q);
                    break;
                case 3: // Y
                    Keyboard.KeyPress(VK.VK_W);
                    break;
                case 4: // LT
                    Keyboard.KeyPress(VK.VK_D);
                    break;
                case 5: // RT
                    Keyboard.KeyPress(VK.VK_F);
                    break;
                case 6: // SELECT/BACK
                    break;
                case 7: // START
                    break;
                case 8: // Left Stick Button
                    Keyboard.KeyPress(VK.CTRL);
                    break;
                case 9: // Right Stick Button
                    Keyboard.KeyPress(VK.VK_5);
                    break;
            }
            
        }
        
        private void Shift()
        {
            while(true)
            {
                if(Shifting) Mouse.Shift((int)((rs_x-32767)*30/65536),(int)((rs_y-32767)*30/65536));
                Thread.Sleep(15);
            }
        }
        
        public void OnEventReceived(ControllerEventArgs gamepad_event)
		{
            // Show Remaining Unwrapped Input
            Logger.Log(gamepad_event.RawState);
        }
    }
}
