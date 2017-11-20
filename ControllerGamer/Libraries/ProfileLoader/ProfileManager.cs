using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerGamer.Libraries.ProfileLoader
{
    /// <summary>
    /// This is actually a runtime compiler ,
    /// which compiles profiles(*.cs) in profile/ , with some reflection work,
    /// So a user can create any logic as complex as he wants to react to controller events.
    /// 
    /// (Yes, basical programming techique is needed.)
    /// 
    /// Profile has to implement callbacks which will be called later.
    /// </summary>
    internal static class ProfileManager
    {

        private static List<Profile> profiles = new List<Profile>();
        public static List<Profile> LoadProfiles()
        {
            profiles.Clear();
            if (Directory.Exists(@"Profiles"))
            {
                string[] profile_directories = Directory.GetDirectories(@"Profiles");
                foreach (string profile_path in profile_directories)
                {
                    Profile profile = new Profile(profile_path);
                    if (profile.IsValid) profiles.Add(profile);
                }
            }
            else
            {
                Directory.CreateDirectory("Profiles");
            }
            return profiles;
        }

        public static Profile GetProfile(int index)
        {
            return profiles[index];
        }
        public static Profile GetProfile(string profile_id)
        {
            foreach (var prof in profiles)
                if (prof.Config.ProfileID == profile_id)
                    return prof;
            return null;
        }

        public static List<string> ListProfile()
        {
            List<string> profiles_id = new List<string>();
            foreach (Profile dic in profiles)
            {
                profiles_id.Add(dic.Config.ProfileID.Remove(0,9));
            }

            return profiles_id;
        }

        private static bool CreateConfig()
        {
            return false;
        }

        public static bool DeleteProfile(string profile_id)
        {
            if (Directory.Exists(@"Profiles\" + profile_id))
            {
                Directory.Delete(@"Profiles\" + profile_id, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SaveProfile(bool isCreatingNew)
        {
            string profile_id;
            // pop up a window for more detail
            if (isCreatingNew)
            {
                Random rd = new Random();
                profile_id = rd.Next(10000000, 100000000).ToString();
                while (Directory.Exists(@"Profiles\" + profile_id))
                {
                    profile_id = rd.Next(10000000, 100000000).ToString();
                }
                Directory.CreateDirectory(@"Profiles\" + profile_id);
                File.Copy(CreateProfileDetail.IconPath, @"Profiles\" + profile_id + @"\" + CreateProfileDetail.Config.IconFileName, true);
            }
            else
            {
                profile_id = CreateProfileDetail.SelectedProfile.Config.ProfileID;
            }


            //CreateConfig
            FileStream inifile = new FileStream(@"Profiles\" + profile_id + @"\config.ini", FileMode.Create);
            inifile.Close();
            INI config = new INI(@"Profiles\" + profile_id + @"\config.ini");

            config.SetValue("ProfileName", CreateProfileDetail.Config.ProfileName);
            config.SetValue("IconFileName", CreateProfileDetail.Config.IconFileName);
            config.SetValue("TargetProcess", CreateProfileDetail.Config.TargetProcess);
            config.SetValue("CSharpSourceFileName", CreateProfileDetail.Config.CSharpSourceFileName);
            config.SetValue("ControllerName", CreateProfileDetail.Config.ControllerName);
            config.SetValue("Description", CreateProfileDetail.Config.Description.Replace("\r\n", @"\r\n").Replace("\n", @"\n"));

            config.Save();

            // dump template .cs
            FileStream cs = new FileStream(@"Profiles\" + profile_id + @"\" + CreateProfileDetail.Config.CSharpSourceFileName, FileMode.Create);
            StreamWriter csw = new StreamWriter(cs);
            csw.Write(CreateProfileDetail.Config.CSharpSourceContent);
            csw.Close();

    }

        //public static bool SaveAsNewProfile()
        //{

        //}
        



        public static bool ChangeProfileID(string old_id, string new_id)
        {
            if (Directory.Exists(@"Profiles") && (!Directory.Exists(@"Profiles" + new_id)))
            {
                Directory.Move(@"Profiles\" + old_id, @"Profiles" + new_id);
                return true;
            }
            else
                return false;
        }
    }
}
