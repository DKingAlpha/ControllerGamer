# Controller Gamer

Aiming at providing a platform where players dynamically mapping any controller to any games!
We provide an all-in-one interface, and you need to provide the idea.

Update: Pre-Release version is avaliable. [Take a look](https://github.com/DKingCN/ControllerGamer/releases)

News: Update will be postponed till enough attention to this project is received.

Known Bugs: Serious memory leakage. Inconsistent interface parameters.

## Concept:
By using techniques including `Runtime Compiling` and `Reflection`, this project makes it possible and easy for "advanced user" to "program" in a mapping profile.

Which means, providen with a template profile in a form of C# 'script', "advanced users" are able to implement their COMPLEX mapping logic in the methods, and share them in a workshop(which is still under development). The other users will be able to download the profile, check the script if they want, and load them just as a script.

To simplify the work on the userside, we have provided interfaces as 'Mouse', 'Keyboard', 'MIDI', 'Screen' with varieties of methods, ensuring the final mapping works in most games.

Besides, sending simple text command to the specific port will trigger a keyboard/mouse action, just like in command line. Program with that!

![Protograph](https://github.com/DKingCN/ControllerGamer/raw/master/protograph.png)
![GUI](https://github.com/DKingCN/ControllerGamer/raw/master/gui.png)


## What can I do with this?
* DIY a HANDY/COMPLEX mapping profile for your favorite input device and favorite game, and program some lights effect back if it's midi device!
* Tell the tool what to do with your Keyboard/Mouse from somewhere else, let's say, an App.

Cool stuffs you can do with this project (Example):
* Get your Left Stick REALLY working in League of Legends. I mean, simply mapping it to arrow keys or mouse shift (like what other tools do) is really stupid.
* Get your MIDI rod/knob working in FlightSimulatorX.
* Get your Launchpad Pro working like a real backpack or inventory. Or just fully control a game from that device.
* Control your game from an Android/iOS App.
* Program a game on MIDI device itself, with patience.
* Make your Launchpad a Windows App Launcher. ( note => WinKey+R, sleep(1) ,apppath)
* Macro.  (Dont do it in online games, I mean, really, dont.)

## Supported Interfaces.
* HID(Joysticks/Keyboard/Mouse/MIDI) => Keyboard/Mouse/MIDI
* Screen color to MIDIOUT.
* Auxiliary Launchpad Helper class.
* Port Listner which handles external instructions.

Steam Workshop are to be implemented in the future:



## Credits
`SharpDX` for an easy access to DirectX

`.NetFramwork` for features of Reflection and Runtime Compiling in a more 'native' way.

## Example of Text Instructions
These commands are all legal and working.

```
Mouse.LeftClick();
Mouse.LeftClick()
mouse.leftclick()
mouse.leftclick(200,300)
mouse.leftclick

mouse leftclick
mouse leftclick 200, 300
mouse leftclick 200  300
keyboard keypress  vk_a  vk_Q  VK_A  vk_w
keyboard keycombination  lctrl vk_6 
```
* Commands are Divided by \\r\\n ,or \\n(not recommended)
* Case-Insensitive
* Space/Comma/Semicolon-Insensitive
* Multispace-Insensitive


## Example of Profile

### config.ini
```
ProfileName=League of Legend
IconFileName=league.png
TargetProcess=League of Legends.exe
CSharpSourceFileName=game_mapping.cs
ControllerName=Controller (XBOX 360 For Windows) && MIDIIN2 (Launchpad Pro)
Description=Controller Profile For League of Legends.
```

### icon file
images named in config.ini


### C# 'Script'
A full example to help you understand how it works.

```
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
        
        // helps to shift mouse pointer
        private void Shift()
        {
            while(true)
            {
                if(Shifting) Mouse.Shift((int)((rs_x-32767)*30/65536),(int)((rs_y-32767)*30/65536));
                Thread.Sleep(15);
            }
        }
                
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
        {
            Logger.Log("Profile Running");
            
            Keyboard.FullScanCodeMode = true;   // virtual key codes might not works to some game settings, so scan mode instead.

            // MIDIs.ListAll();         // helps to find out which device you want.
            lpd = new Launchpad(MIDIs.Get("MIDIOUT2"));

            Thread th = new Thread(SyncScreen);
            th.IsBackground = true;
            th.Start();
                        
            Thread th1 = new Thread(Shift);
            th1.IsBackground = true;
            th1.Start();
        }

        // Constructor
        public ControllerCallbacks()
        {
            Logger.Log("Profile Running");
            
            Keyboard.FullScanCodeMode = true;   // virtual key codes might not works to some game settings, so scan mode instead.

            if(MIDIs.Get(2)!=null) lpd = new Launchpad(MIDIs.Get(2));  // lame but the only pain
            
            Thread th = new Thread(Shift);
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
                }
            }
            
        }
        
        public void OnMouseEventReceived(DMouseEventArgs e)
        {
            Logger.Log(e);
        }
        
        public void OnStickEventReceived(StickEventArgs stick_event)
        {
            //Logger.Log(stick_event);
            ls_x = stick_event.StickStatus.X;
            ls_y = stick_event.StickStatus.Y;
            
            rs_x = stick_event.StickStatus.RX;
            rs_y = stick_event.StickStatus.RY;
            
            // Left Stick
            if(Math.Abs(ls_x-32767)<5000 && Math.Abs(ls_y-32767)<5000)
            {
                Keyboard.KeyPress(VK.VK_S);
                //Mouse.Move(1920/2,1080/2);
            }
            else
            {
                double px = (ls_x / 65535.0) * Screen.X * 0.5 * (Screen.Y/Screen.X) + (0.5*Screen.X - 0.25 * Screen.Y);
                double py = (ls_y / 65535.0) * Screen.Y * 0.5 + 0.25* Screen.Y ;
                Mouse.RightClick((int)px,(int)py);
            }
            
            // Right Stick
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

        
        public void OnEventReceived(ControllerEventArgs e)
		{
            // Show unhandled events
            Logger.Log(e);
        }
    }
    
    
}

```
