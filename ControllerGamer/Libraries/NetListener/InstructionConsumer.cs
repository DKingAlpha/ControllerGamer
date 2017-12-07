using ControllerGamer.Libraries.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ControllerGamer.Libraries.NetListener
{
    internal static class InstructionConsumer
    {
        private static Queue<string> InstructionsProvider = null;

        private static Dictionary<string,Type> Devices = new Dictionary<string, Type>();

        public static void Start(Queue<string> instructions)
        {
            InstructionsProvider = instructions;
            Thread th = new Thread(new ThreadStart(() => {
                while (InstructionsProvider != null)
                {
                    while(InstructionsProvider.Count > 0)
                    {
                        Parse(InstructionsProvider.Dequeue());
                    }
                    Thread.Sleep(50);
                }
            }));
            th.IsBackground = true;
            th.Start();
        }

        public static void Stop()
        {
            InstructionsProvider = null;
        }


        private static Type GetDevice(string _target)
        {
            string target = _target.ToLower();
            if (!Devices.ContainsKey(target))
            {
                foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (t.IsClass && t.Namespace == "ControllerGamer.Libraries.SimInput" && t.Name.ToLower() == target)
                    {
                        Devices.Add(target, t);
                        break;
                    }
                }
            }

            if (Devices.ContainsKey(target))
                return Devices[target];
            else
                return null;
        }

        private static VK GetVK(string vkname)
        {
            foreach (var fi in typeof(VK).GetFields())
                if (fi.Name.ToLower() == vkname.ToLower())
                    return (VK)fi.GetRawConstantValue();
            return VK.NONE;
        }

        private static void Parse(string ins)
        {
            try
            {
                string trimed = ins.Trim(new char[] { ' ',';'});
                string[] splited1 = trimed.Split(new char[] { '.', ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                string target = splited1[0];
                string[] splited2 = splited1[1].Split(new char[] { '(', ' ', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string command = splited2[0];
                object[] args = null;


                MethodInfo method = null;

                // find method , parse args
                if (target.ToLower() == "mouse")
                {
                    args = new object[splited2.Length - 1];

                    foreach (var m in GetDevice(target).GetMethods())
                    {
                        if (m.Name.ToLower() == command.ToLower())
                        {
                            if (m.GetParameters().Length == args.Length)
                            {
                                method = m;
                                break;
                            }
                        }
                    }

                    for (int i = 1; i < splited2.Length; i++)
                    {
                        args[i - 1] = int.Parse(splited2[i]);
                    }
                }

                if (target.ToLower() == "keyboard")
                {
                    args = new object[1];

                    foreach (var m in GetDevice(target).GetMethods())
                    {
                        if (m.Name.ToLower() == command.ToLower())
                        {

                            if (command.ToLower() == "text")
                            {
                                // text string
                                method = m;
                                args[0] = splited1[1].Trim(new char[] { ' ', ';', '"', '(', ')' });
                                break;
                            }
                            else
                            {   // VK[]
                                if (splited2.Length - 1 > 1)
                                {
                                    if (m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType.IsArray)
                                    {
                                        method = m;
                                        // arg
                                        VK[] argarray = new VK[splited2.Length - 1];
                                        args[0] = argarray;
                                        for (int idx = 0; idx < splited2.Length - 1; idx++) argarray[idx] = GetVK(splited2[idx + 1]);
                                        break;
                                    }
                                }
                                else
                                {
                                    // VK
                                    if (m.GetParameters().Length == 1)
                                    {
                                        method = m;
                                        GetVK(splited2[1]);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                // finally, invoke
                if(method!=null) method.Invoke(null, args);

            }
            catch (Exception e)
            {
                Logger.Log(e);
                Logger.Log(e.StackTrace);
            }
        }

    }
}
