using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Utility
{
    /// <summary>
    /// 游戏配置文件
    /// </summary>
    public class GameConfig : Singleton<GameConfig>
    {
        public GameConfigData Data;

        private string path;

        public GameConfig()
        {
            Data = new GameConfigData();
            path = Path.Combine(Application.persistentDataPath, "config.txt");
            Load();
        }
        
        public void Load()
        {
            try
            {
                if(!File.Exists(path)) Save();
                StreamReader reader = new StreamReader(path);
                Data = JsonUtility.FromJson<GameConfigData>(reader.ReadToEnd());
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void Save()
        {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(stream);
            string json = JsonUtility.ToJson(Data);
            writer.Write(json);
            writer.Close();
        }



    }
}