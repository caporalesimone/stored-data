using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace StoredData
{
    public class StoredData
    {
        public enum StoreType
        {
            Global,
            Server,
            Character
        }

        [System.Serializable()]
        private class Server
        {
            public Dictionary<string, object> ServerGlobal { get; set; }
            public Dictionary<string, Character> Characters { get; set; }
            public Server()
            {
                ServerGlobal = new Dictionary<string, object>();
                Characters = new Dictionary<string, Character>();
            }
        }

        //[System.Serializable()]
        public class Character
        {
            public Dictionary<string, object> Settings { get; set; }

            public Character()
            {
                Settings = new Dictionary<string, object>();
            }

            public void AddSetting(string key, object value)
            {
                Settings[key] = value;
            }
        }

        private class JSONStorage
        {
            public Dictionary<string, object> Global { get; set; }
            public Dictionary<string, Server> Servers { get; set; }
            public JSONStorage()
            {
                Global = new Dictionary<string, object>();
                Servers = new Dictionary<string, Server>();
            }
        }

        private readonly FileInfo _fileName;    // path of the JSON file
        private readonly string _serverName;    // Name of the server
        private readonly string _characterName; // Name of the account

        public StoredData(string storedDataFolder, string serverName, string characterName)
        {
            _fileName = new FileInfo(Path.Combine(storedDataFolder, "StoredData.json"));
            _serverName = serverName;
            _characterName = characterName;

            if (storedDataFolder == "") storedDataFolder = ".";

            if (!Directory.Exists(storedDataFolder))
            {
                Directory.CreateDirectory(storedDataFolder);
            }

            if (!File.Exists(_fileName.FullName))
            {
                File.Create(_fileName.FullName).Close();
            }

            string jsonText = File.ReadAllText(_fileName.FullName);

            bool save = false;
            JSONStorage jsonObj = JsonConvert.DeserializeObject<JSONStorage>(jsonText);

            if (jsonObj == null)
            {
                jsonObj = new JSONStorage();
                save = true;
            }

            if (!jsonObj.Servers.ContainsKey(_serverName))
            {
                jsonObj.Servers[_serverName] = new Server();
                save = true;
            }

            if (!jsonObj.Servers[_serverName].Characters.ContainsKey(_characterName))
            {
                jsonObj.Servers[_serverName].Characters[_characterName] = new Character();
                save = true;
            }

            if (save)
            {
                jsonText = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(_fileName.FullName, jsonText);
            }
        }

        public void StoreData(object data, string keyName, StoreType storage)
        {
            string jsonText = File.ReadAllText(_fileName.FullName);
            JSONStorage jsonObj = JsonConvert.DeserializeObject<JSONStorage>(jsonText) ?? throw new Exception("Error reading JSON file");

            switch (storage)
            {
                case StoreType.Global:
                    jsonObj.Global[keyName] = data;
                    break;
                case StoreType.Server:
                    jsonObj.Servers[_serverName].ServerGlobal[keyName] = data;
                    break;
                case StoreType.Character:
                default:
                    var character = jsonObj.Servers[_serverName].Characters[_characterName];
                    character.AddSetting(keyName, data);
                    break;
            }

            jsonText = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(_fileName.FullName, jsonText);
        }
    }
}
