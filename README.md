# Controller Gamer
Aiming at providing a platform where players dynamically mapping any controller to any games!

`Notice` This project is still in early development, only working as a prototype. Lots of functions descripted below are to be implemented in the future. Complete documents will be released once the project is done.

## Concept:
By using techniques including `Runtime Compiling` and `Reflection`, this project makes it possible and easy for "advanced user" to "program" in a mapping profile.

Which means, providen with a template profile in a form of C# 'script', "advanced users" are able to implement their COMPLEX mapping logic in the methods, and share them in a workshop(which is still under development). The other users will be able to download the profile, check the script if they want, and load them just as a script.

To simplify the work on the userside, we have provided interfaces as 'Mouse', 'Keyboard', 'MIDI', 'DeviceUtils' with varieties of methods, ensuring the final mapping works in most games.

Besides, sending simple text command to the specific port will trigger a keyboard/mouse action, just like in command line. Program with that!

![Protograph](https://github.com/DKingCN/ControllerGamer/raw/master/protograph.png)

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
* Macro.  (Dont do it in online games, I really mean it.)

## Supported Interfaces.
* HID(Joysticks/Keyboard/Mouse/MIDI) => Keyboard/Mouse/MIDI
* Port Listner which handles external instructions.

## Working progress

These features have been implemented:
* Joysticks => K/M(Keyboard/Mouse)
* K/M => K/M  (Risk of endless loop has NOT been tested)
* Text Command(port) => K/M   (See Chapter #Port Listener )

The following features are to be implemented soon.
* MIDI controller IN
* MIDI controller OUT (light feedback, note)

These features are to be implemented in a lower priority:
* Screen utils(stream?, color?, areacolor?)
* Steam Workshop


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
ControllerName=Controller (XBOX 360 For Windows)
Description=Controller Profile For League of Legends.
```

### icon file
images named in config.ini


### C# 'Script'
A peek of how it works.

This script defines how we simulate a 'joystick' for LoL,

Moving the left stick is defined to move the hero BY clicking right mouse button in a circle area. (Which shows the distinguishing feature of this project.)


```
using ...

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

```
