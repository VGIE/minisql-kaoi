using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbManager.Security
{
    public class Manager
    {
        public List<Profile> Profiles { get; private set; } = new List<Profile>();

        private string m_username;
        public Manager(string username)
        {
            m_username = username;
        }

        public bool IsUserAdmin()
        {
            //TODO DEADLINE 5: Return true if the user logged-in (m_username) is the admin, false otherwise
            Profile profile = ProfileByUser(m_username);
            if(profile != null && profile.Name == Profile.AdminProfileName)
            {
                return true;
            }

            return false;
        }

        public bool IsPasswordCorrect(string username, string password)
        {
            //TODO DEADLINE 5: Return true if the user's password is correct. The given password should be encrypted before comparing with the saved one
            User user = UserByName(username);
            if (user == null)
                return false;
            password = Encryption.Encrypt(password);
            if (user.EncryptedPassword == password)
                return true;
            return false; 
        }

        public void GrantPrivilege(string profileName, string table, Privilege privilege)
        {
            //TODO DEADLINE 5: Add this privilege on this table to the profile with this name
            //If the profile or the table don't exist, do nothing

            if(!IsUserAdmin())
            {
                return;
            }

            if (string.IsNullOrEmpty(profileName) || string.IsNullOrEmpty(table) || privilege == null)
            {
                return;
            }

            Profile profile = ProfileByName(profileName);

            if (profile != null && table != null)
            {
                profile.GrantPrivilege(table, privilege);
            }
            
        }

        public void RevokePrivilege(string profileName, string table, Privilege privilege)
        {
            //TODO DEADLINE 5: Remove this privilege on this table to the profile with this name
            //If the profile or the table don't exist, do nothing
            if(!IsUserAdmin())
            {
                return;
            }

            if (string.IsNullOrEmpty(profileName) || string.IsNullOrEmpty(table))
            {
                return;
            }

            if(IsUserAdmin() && IsGrantedPrivilege(profileName, table, privilege))
            {
                Profile profile = ProfileByName(profileName);
                if((profile != null) || (table != null))
                {
                    profile.RevokePrivilege(table, privilege);
                }
                
            }
        }

        public bool IsGrantedPrivilege(string username, string table, Privilege privilege)
        {
            //TODO DEADLINE 5: Return true if the username has this privilege on this table. False otherwise (also in case of error)
            
            if(username == null || table == null || privilege == null)
                return false;
            

            Profile profile = ProfileByUser(username);

            if(profile == null)
                return false;
            
            if(profile.Name == Profile.AdminProfileName)
                return true;

            return profile.IsGrantedPrivilege(table, privilege);
            
        }

        public void AddProfile(Profile profile)
        {
            //TODO DEADLINE 5: Add this profile
            if(IsUserAdmin())
            {
                if(profile != null)
                {
                    Profiles.Add(profile);
                }
            }
        }

        public User UserByName(string username)
        {
            //TODO DEADLINE 5: Return the user by name. If it doesn't exist, return null
            foreach(Profile f in Profiles)
            {
                foreach(User u in f.Users)
                {
                    if(u.Username == username)
                    {
                        return u;
                    }
                }
            }
            return null;
            
        }

        public Profile ProfileByName(string profileName)
        {
            //TODO DEADLINE 5: Return the profile by name. If it doesn't exist, return null
            foreach(Profile p in Profiles)
            {
                if(p.Name == profileName)
                {
                    return p;
                }
            }
            return null;
            
        }

        public Profile ProfileByUser(string username)
        {
            foreach (Profile profile in Profiles)
            {
                foreach (User user in profile.Users)
                {
                    if (user.Username.Equals(username))
                    {
                        return profile;
                    }
                }
            }
            return null;
        }

        public bool RemoveProfile(string profileName)
        {
            //TODO DEADLINE 5: Remove this profile Ainhoa
            if(!IsUserAdmin())
            { 
                return false; 
            }
            var prof = ProfileByName(profileName);
            if (prof != null)
            {
                return Profiles.Remove(prof);
            }

            return false;
        }

        public static Manager Load(string databaseName, string username)
        {
            //TODO DEADLINE 5: Load all the profiles and users saved for this database. The Manager instance should be created with the given username
            
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), databaseName);
                string filePath = Path.Combine(path, "manager.txt");

                Manager manager = new Manager(username);

                if(!File.Exists(filePath))
                    return manager;
                
                
                using (TextReader reader = File.OpenText(filePath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if(string.IsNullOrWhiteSpace(line))
                            continue;
                        
                        Profile profile = new Profile();
                        profile.Name = line;

                        while ((line = reader.ReadLine()) != null && line != "Users:")
                        {
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            string[] parts = line.Split('=');

                            if (parts.Length == 2)
                            {
                                string table = parts[0].Trim();
                                string[] privileges = parts[1].Split('|');

                                foreach (string privilege in privileges)
                                {
                                    if (Enum.TryParse(privilege.Trim(), out Privilege parsedPrivilege))
                                    {
                                        profile.GrantPrivilege(table, parsedPrivilege);
                                    }
                                }
                            }
                        }

                        while ((line = reader.ReadLine()) != null && line != "-------")
                        {
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            string[] parts = line.Split(':');

                            if (parts.Length == 2)
                            {
                                User user = new User
                                {
                                    Username = parts[0],
                                    EncryptedPassword = parts[1]
                                };

                                profile.Users.Add(user);
                            }
                        }   
                        manager.Profiles.Add(profile);                      
                    }
                }           
            
                return manager;
            
            }
            catch
            {
                return null;
            }
    }

        public void Save(string databaseName)
        {
            //TODO DEADLINE 5: Save all the profiles and users/passwords created for this database.
            try
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), databaseName);
                Directory.CreateDirectory(folder);

                string securityFile = Path.Combine(folder, "manager.txt");

                using (TextWriter tw = File.CreateText(securityFile))
                {
                    foreach (Profile p in Profiles)
                    {
                        tw.WriteLine(p.Name);

                        foreach (string table in p.PrivilegesOn.Keys)
                        {
                            string privLine = table + "=";
                            bool first = true;
                            foreach (Privilege priv in p.PrivilegesOn[table])
                            {
                                if (!first)
                                    privLine += "|";
                                privLine += priv.ToString();
                                first = false;
                            }
                            tw.WriteLine(privLine);
                        }

                        tw.WriteLine("Users:");

                        foreach (User u in p.Users)
                        {
                            tw.WriteLine(u.Username + ":" + u.EncryptedPassword);
                        }

                        tw.WriteLine("-------");
                    }
                }
            }
            catch
            {
                return;
            }
        }
    }
}
