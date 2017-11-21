using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControllerGamer.Libraries.Controllers
{
    internal static class Controllers
    {
        private static List<Controller> controllers = new List<Controller>();


        private static void Logcon(Controller con)
        {
            Logger.Log("Found Controller " + controllers.IndexOf(con) + ":");
            Logger.Log(con.GetDetail()+"\r\n");
        }

        public static int LoadControllers()
        {
            
            DirectInput dinput = new SharpDX.DirectInput.DirectInput();

            // Keyboard
            Controller kb = new Keyboard();
            controllers.Add(kb);
            Logcon(kb);

            // Mouse
            Controller mouse = new Mouse();
            controllers.Add(mouse);
            Logcon(mouse);

            // xbox360-like joystick
            foreach (DeviceInstance dinstance in dinput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly))
            {
                if (dinstance.InstanceGuid != Guid.Empty)
                {
                    Controller con = new Joystick(dinstance.InstanceGuid);
                    controllers.Add(con);
                    Logcon(con);
                }

            }
            // ps3-like joystick
            foreach (DeviceInstance dinstance in dinput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AttachedOnly))
            {
                if (dinstance.InstanceGuid != Guid.Empty)
                {
                    Controller con = new Joystick(dinstance.InstanceGuid);
                    controllers.Add(con);
                    Logcon(con);
                }

            }
            // Not implemented below
            foreach (DeviceInstance dinstance in dinput.GetDevices(DeviceType.Driving, DeviceEnumerationFlags.AttachedOnly))
            {
                if (dinstance.InstanceGuid != Guid.Empty)
                {
                    Controller con = new Joystick(dinstance.InstanceGuid);
                    controllers.Add(con);
                    Logcon(con);
                }

            }
            foreach (DeviceInstance dinstance in dinput.GetDevices(DeviceType.FirstPerson, DeviceEnumerationFlags.AttachedOnly))
            {
                if (dinstance.InstanceGuid != Guid.Empty)
                {
                    Controller con = new Joystick(dinstance.InstanceGuid);
                    controllers.Add(con);
                    Logcon(con);
                }

            }
            foreach (DeviceInstance dinstance in dinput.GetDevices(DeviceType.Flight, DeviceEnumerationFlags.AttachedOnly))
            {
                if (dinstance.InstanceGuid != Guid.Empty)
                {
                    Controller con = new Joystick(dinstance.InstanceGuid);
                    controllers.Add(con);
                    Logcon(con);
                }

            }

            return controllers.Count;
        }

        public static Controller GetController(int index)
        {
            if (controllers.Count > 0)
                return controllers[index];
            else
                return null;
        }

        public static int Count {
            get
            {
                return controllers.Count;
            }
        }

        /// <summary>
        /// Will only return the first matched controller.
        /// </summary>
        /// <param name="controller_name"></param>
        /// <returns></returns>
        public static Controller GetController(string controller_name)
        {
            if (controller_name.Length > 0)
            {
                foreach (var con in controllers)
                    if (con.GetProductName().ToLower().Contains(controller_name.ToLower()))
                        return con;
            }
            return null;
        }

        public static void StopControllers()
        {
            foreach (var con in controllers)
            {
                con.Stop();
            }
        }

        public static void Clear()
        {
            foreach (var con in controllers)
            {
                con.Stop();
                con.Dispose();
            }
            controllers.Clear();
        }
    }
}
