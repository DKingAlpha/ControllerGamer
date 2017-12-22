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
        Launchpad lpd = null;
        int last_x=0, last_y=0;
        
        // Constructor
        public ControllerCallbacks()
        {
            // out
            // nanoKontrol: 1
            // Launchpad: 3
            lpd = new Launchpad(MIDIs.Get(3));
        }

        public void OnMidiEventReceived(MidiEventArgs e)
        {
            if(e.DeviceID == 0)     // nanoKontrol
            {
                lpd.UnsetColumnColor(e.Note - 35);
                lpd.SetColor( (int)(e.Velocity/12.7)*10 + (e.Note-35), (int)(e.Velocity/12.7)*10);
            }
            if(e.DeviceID == 2)     // Launchpad
            {
                if(e.Pressed)
                {
                    lpd.SetColor(e.Note,e.Velocity);
                }
                else
                {
                    lpd.UnsetColor(e.Note);
                }
            }
        }
        
        public void OnMouseEventReceived(DMouseEventArgs e)
        {
            int x = (int)(10*e.X/1920.0);
            int y = 9 - (int)(9 * e.Y/(1080.0*2));
            if(!(x==last_x && y==last_y))
            {
                //Logger.Log(String.Format("Changing at {0} {1}",x,y));
                lpd.UnsetColor(last_x+last_y*10);
                lpd.UnsetRowColor(last_y);
                lpd.UnsetColumnColor(last_x);
                last_x=x;
                last_y=y;
                int color = last_x+last_y*10;
                lpd.SetColor(last_x+last_y*10,color);
                lpd.SetRowColor(last_y,color);
                lpd.SetColumnColor(last_x,color);
            }
        }
        
        public void OnEventReceived(ControllerEventArgs gamepad_event)
        {
            // Show Remaining Unwrapped Input
            Logger.Log(gamepad_event.RawState);
        }
    }
}
