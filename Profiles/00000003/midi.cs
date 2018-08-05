using System;
using System.IO;
using System.Text;
using System.Threading;
using ControllerGamer.Libraries;
using ControllerGamer.Libraries.Windows;
using ControllerGamer.Libraries.SimInput;
using ControllerGamer.Libraries.Controllers;
using Sanford.Multimedia.Midi;

using Key = SharpDX.DirectInput.Key;
using EventType = SharpDX.DirectInput.JoystickOffset;

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
            Logger.Log("Profile Running.");
            
            // Load Launchpad (Helper class) from MIDIout#2
            if(MIDIs.Get(2)!=null) lpd = new Launchpad(MIDIs.Get(2));   // lame but the only pain

        }
        
        public void OnMidiEventReceived(MidiEventArgs e)                 // receiving all types of midi msgs
        {
            Logger.Log(e);
            lpd.Midi.BasePath = (string)e.AdditionalData;                // working profile path, to find midi file
            if (e.RawState is ChannelMessageEventArgs)
            {
                if(e.Pressed)
                {
                    //lpd.Midi.PlayMidiFile(@"midires\gameover.mid");
                    
                    lpd.SetColumnColor(e.Note%10, e.Velocity);         // param channel can be omitted if 0
                    lpd.SetRowColor(e.Note/10, e.Velocity);
                    // lpd.SetColor(e.Note + 11, e.Velocity);
                    // lpd.SetColor(e.Note + 9, e.Velocity);                      // default channel 0
                    // lpd.SetColor(e.Note - 11, e.Velocity);
                    // lpd.SetColor(e.Note - 9, e.Velocity);
                }else
                {
                    // lpd.UnsetColor(e.Note+11);
                    // lpd.UnsetColor(e.Note+9);
                    // lpd.UnsetColor(e.Note-11);
                    // lpd.UnsetColor(e.Note-9);
                    lpd.UnsetColumnColor(e.Note%10);
                    lpd.UnsetRowColor(e.Note/10);
                }
            }
        }
        

        public void OnEventReceived(ControllerEventArgs e)
		{
            // Show unhandled events
            Logger.Log(e);
        }
    }
    
    
}
