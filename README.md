# Controller Gamer
Aiming at providing a platform where players dynamically mapping any controller to any games!

`Notice` This project is still in early development, only working as a prototype. Lots of functions descripted below are to be implemented in the future.

## Concept:
By using techniques including `Runtime Compiling` , this project makes it possible and easy for "advanced user" to "program" in a mapping profile.

Which means, providen with a template profile in a form of C# 'script', "advanced users" are able to implement their mapping logic in the methods, and share them in a workshop(which is still under development). The other users will be able to download the profile, check the script if they like, and load them just as a script.

To simplify the work on the userside, we have provided interfaces as 'Mouse', 'Keyboard','DeviceUtils' with varieties of methods, ensuring the final mapping works in most games.

## Working progress
By now, joysticks have been supported with no big surprise. The next milestone is to support MIDI controller.

To be more specificall. Launchpad is no more limited to tetris now. We are to make it possible for any PC games.

Launchpad & LoL, imagine that.

## Credits
`SharpDX` for an easy access to DirectX
`.NetFramwork` for features of Reflection and Runtime Compiling in a more 'native' way.

## Example of Profile

## config.ini
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

namespace GameProfile
{
    public class ControllerCallbacks
    {
        private int ls_x, ls_y;
        private int rs_x, rs_y;
        public ControllerCallbacks()
        {
            
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
            
        
        }
        
        public void OnDPadEventReceived(DPadEventArgs dpad)
        {
            //Logger.Log(dpad);
            if(dpad.UP)Keyboard.KeyPress(VK.VK_1);
            ...
        }
        
        public void OnButtonEventReceived(ButtonEventArgs button)
        {
            if(button.Released)return;
            // Pressed => KeyPress[KeyDown,KeyUp]
            //Logger.Log(button);
            switch(button.ID)
            {
                case 0: // Button A
                    Keyboard.KeyPress(VK.VK_E);
                    break;
                ...
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