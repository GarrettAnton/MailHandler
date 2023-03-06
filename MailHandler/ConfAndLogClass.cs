using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace MailHandler
{
    class Config
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
    }


    abstract class FileHandler
    {
        protected string Path;
        public  FileHandler ()
        {
        }

        public FileHandler(string path)
        {
            Path = path;
        }

        public abstract void Executor();      
    }

    class ConfigHandler : FileHandler
    {
        private Config config;
        public ConfigHandler ()
        {
            this.Path = Directory.GetCurrentDirectory() + "\\" +"config.json";
        }
        public override void Executor()
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                string json =  sr.ReadToEnd();
                config = JsonSerializer.Deserialize<Config>(json) ?? throw new Exception("Can not read config");
            }
        }
        public Config GetConfig ()
        {
            return config;
        }
    }
    class LoggerHandler : FileHandler
    {
        private string logMessage;
        public string LogMessage
        {
            set
            {
                logMessage = value;
            }
        }
        public LoggerHandler()
        {
            this.Path = Directory.GetCurrentDirectory() + "\\" + DateTime.Today.ToString().Substring(0, 10) + ".log";
        }
        public override void Executor()
        {
            if (!File.Exists(Path))
            {
                using (FileStream fs = new FileStream(Path, FileMode.Create))
                {
                    
                }
            }
            using (StreamWriter sw = new StreamWriter(Path,true))
            {
                sw.WriteLineAsync(DateTime.Now + " _____ " + logMessage);
            }
        }
    }
}
