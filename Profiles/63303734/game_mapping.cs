using System;
using System.IO;
using System.Text;
using System.Drawing;
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
        private Launchpad lpd = null;
        private int last_x=0, last_y=0;
        
        private bool sync = true;
        
        private void SyncScreen()
        {
            while(true)
            {
                if(sync)
                {
                    RefreshLaunchpad();
                }
                Thread.Sleep(5000);
            }
        }
        
        public void Stop()
        {
               sync = false;
        }

        private void RefreshLaunchpad()
        {
            Bitmap bmp = Screen.ZoomScreen(0,0,1920,1080,8,8);
            for(int j=1;j<=8;j++)
            {
                for(int i=1;i<=8;i++)
                {
                    Color clr = bmp.GetPixel(i-1, 8-j);
                    lpd.SetColor(i+j*10,clr.R/4, clr.G/4, clr.B/4);
                }
            }
            bmp.Dispose();
        }
        
        // Constructor
        public ControllerCallbacks()
        {
            MIDIs.ListAll();
            lpd = new Launchpad(MIDIs.Get("MIDIOUT2"));

            Thread th = new Thread(SyncScreen);
            th.IsBackground = true;
            th.Start();
        }

        public void OnMidiEventReceived(MidiEventArgs e)
        {
            if(e.Name.StartsWith("nanoKontrol"))     // nanoKontrol
            {
                lpd.UnsetColumnColor(e.Note - 35);
                lpd.SetColor( (int)(e.Velocity/12.7)*10 + (e.Note-35), (int)(e.Velocity/12.7)*10);
            }
            if(e.Name.StartsWith("MIDIIN"))     // Launchpad
            {
                int x = e.Note%10, y = e.Note/10;
                if(e.Pressed)
                {
                    UpdateMouseOnLaunchpad(x,y);
                    lpd.SetColor(x+y*10,45);
                    Mouse.Move(240*x,135*(9-y));
                }
                else
                {
                    lpd.SetColor(x+y*10,35);
                }
            }
        }
        
        private void UpdateMouseOnLaunchpad(int x, int y)
        {
            if(!(x==last_x && y==last_y))
            {
                lpd.UnsetColor(last_x+last_y*10);
                last_x=x;
                last_y=y;
                lpd.SetColor(last_x+last_y*10,35);
            }
        }
        
        
        public void OnEventReceived(ControllerEventArgs gamepad_event)
        {
            // Show Remaining Unwrapped Input
            Logger.Log(gamepad_event.RawState);
        }
    }
}
