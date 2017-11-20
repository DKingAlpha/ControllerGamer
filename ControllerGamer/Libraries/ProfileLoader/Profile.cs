using ControllerGamer.Libraries.Controllers;
using ControllerGamer.Libraries.SimInput;
using ControllerGamer.Libraries.Windows;
using SharpDX.DirectInput;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ControllerGamer.Libraries.ProfileLoader
{

    internal class ProfileConfig
    {
        public string ProfileID;
        public string ProfileName;
        public string IconFileName;
        public string CSharpSourceFileName;
        public string TargetProcess;
        public string ControllerName;
        public string Description;
        public string CSharpSourceContent;
    }

    internal class Profile
    {
        public ProfileConfig Config = new ProfileConfig();
        public bool IsValid = false;
        public bool IsCompiled = false;
        private object ProfileProgram;
        private Type MethodCaller;
        private bool IsPrivate = true;
        

        public Profile(string Profile_path)
        {
            try
            {
                Config.ProfileID = Profile_path.Remove(0,9);
                string[] files = Directory.GetFiles(Profile_path);
                if (files.Contains(Profile_path + @"\config.ini"))
                {
                    INI config = new INI(Profile_path + @"\config.ini");
                    Config.ProfileName = config.GetValue("ProfileName");
                    Config.IconFileName = config.GetValue("IconFileName");
                    Config.CSharpSourceFileName = config.GetValue("CSharpSourceFileName");
                    Config.TargetProcess = config.GetValue("TargetProcess");
                    Config.ControllerName = config.GetValue("ControllerName");
                    Config.Description = config.GetValue("Description").Replace(@"\r\n","\n").Replace(@"\n", "\n");

                    if (files.Contains(Profile_path + @"\" + Config.CSharpSourceFileName))
                    {
                        Config.CSharpSourceContent = File.ReadAllText(Profile_path + @"\" + Config.CSharpSourceFileName);
                        IsValid = true;
                    }
                }
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }

        public Profile(ProfileConfig _Config)
        {
            Config = _Config;
            IsValid = true;
        }

        public bool Compile()
        {
            if (IsValid)
            {
                try
                {
                    ProfileProgram = CompileFromSource();
                    if (ProfileProgram != null)
                    {
                        MethodCaller = ProfileProgram.GetType();
                        if (MethodCaller != null)
                        {
                            IsCompiled = true;
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    Logger.Log(e.StackTrace);
                    return false;
                }
            }
            else
                return false;
        }

        private object CompileFromSource()
        {
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
            CompilerParameters objCompilerParameters = new CompilerParameters();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                string location = assembly.Location;
                if (!String.IsNullOrEmpty(location))
                {
                    objCompilerParameters.ReferencedAssemblies.Add(location);
                }
            }
            objCompilerParameters.GenerateExecutable = false;
            objCompilerParameters.GenerateInMemory = true;
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, Config.CSharpSourceContent);
            if (cr.Errors.HasErrors)
            {
                Logger.Log("Compiling error:");
                foreach (CompilerError err in cr.Errors)
                {
                    //Logger.Log( ": Line " + err.Line + ":" + err.ErrorText);
                    Logger.Log(err.ToString().Replace(err.FileName, Config.CSharpSourceFileName));
                }
                return null;
            }
            else
            {
                Assembly objAssembly = cr.CompiledAssembly;
                return objAssembly.CreateInstance("GameProfile.ControllerCallbacks");
            }
        }

        [STAThread]
        public void OnEventReceived(ControllerEventArgs controllerEventArgs)
        {
            object[] args = { controllerEventArgs };
            // default
            string CallbackName = "OnEventReceived";

            if (controllerEventArgs is StickEventArgs) CallbackName = "OnStickEventReceived";
            if (controllerEventArgs is DPadEventArgs) CallbackName = "OnDPadEventReceived";
            if (controllerEventArgs is ButtonEventArgs) CallbackName = "OnButtonEventReceived";

            MethodCaller.GetMethod(CallbackName).Invoke(obj: ProfileProgram, parameters: args);
        }
        
    }
}
