using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Hosting;

namespace WcfService.domain
{
    public class Properties
    {
        private Dictionary<String, String> list;
        private FileInfo configFile;
        private static DirectoryInfo configFolder;

        public Properties(String file)
        {
            configFile = new FileInfo(Path.Combine(HostingEnvironment.MapPath("~/config/"), file + ".ini"));
            Console.Error.WriteLine(configFile.FullName);
            // Sets the configFolder field if needed
            if (configFolder == null)
                configFolder = configFile.Directory;
            // Creates the configFolder if needed
            if (!configFolder.Exists)
                configFolder.Create();
            
            reload(file);
        }

        public String get(String field, String defValue)
        {
            return (get(field) == null) ? (defValue) : (get(field));
        }

        public String get(String field)
        {
            return (list.ContainsKey(field)) ? (list[field]) : (null);
        }

        public void set(String field, Object value)
        {
            if (!list.ContainsKey(field))
                list.Add(field, value.ToString());
            else
                list[field] = value.ToString();
        }

        public void Save()
        {
            Save(this.configFile);
        }

        public void Save(String filename)
        {
            Save(new FileInfo(configFolder.FullName + filename));
        }

        public void Save(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
                fileInfo.Create();

            StreamWriter file = new StreamWriter(fileInfo.FullName);

            foreach (String prop in list.Keys.ToArray())
            {
                if (!String.IsNullOrWhiteSpace(list[prop]))
                    file.WriteLine(prop + "=" + list[prop]);
            }

            file.Close();
        }

        public void reload()
        {
            reload(this.configFile);
        }

        public void reload(String filename)
        {
            reload(new FileInfo(configFolder.FullName + filename));
        }

        public void reload(FileInfo fileInfo)
        {
            list = new Dictionary<String, String>();

            if (fileInfo.Exists)
                loadFromFile(fileInfo);
            else
                fileInfo.Create();
        }

        private void loadFromFile(FileInfo fileInfo)
        {
            foreach (String line in File.ReadAllLines(fileInfo.FullName))
            {
                if ((!String.IsNullOrEmpty(line)) &&
                    (!line.StartsWith(";")) &&
                    (!line.StartsWith("#")) &&
                    (!line.StartsWith("'")) &&
                    (line.Contains('=')))
                {
                    int index = line.IndexOf('=');
                    String key = line.Substring(0, index).Trim();
                    String value = line.Substring(index + 1).Trim();

                    if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                        (value.StartsWith("'") && value.EndsWith("'")))
                    {
                        value = value.Substring(1, value.Length - 2);
                    }

                    try
                    {
                        //ignore dublicates
                        list.Add(key, value);
                    }
                    catch { }
                }
            }
        }


    }
}
