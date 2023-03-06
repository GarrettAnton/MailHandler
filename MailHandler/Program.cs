using System;
using XmlCommandLibrary;

namespace MailHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerHandler lg = new LoggerHandler();
            try
            {
                lg.LogMessage = "-----START-----";
                lg.Executor();
                lg.LogMessage = "Try to open config";
                lg.Executor();
                ConfigHandler cf = new ConfigHandler();
                cf.Executor();
                Config config = cf.GetConfig();

                lg.LogMessage = "Try to set email to config";
                lg.Executor();
                POPGetMail getMail = new POPGetMail(config.Host, config.Username, config.Password);
                getMail.Set();
                getMail.Connect();

                lg.LogMessage = "Try to read CommandFile";
                lg.Executor();
                XmlReadWrite<XmlCommandFile> readWrite = new XmlReadWrite<XmlCommandFile>(config.Path);
                XmlCommandFile xmlCommandFile = readWrite.Read();

                lg.LogMessage = "Try to read email and add commands to xml instance";
                lg.Executor();
                POPGetCommand getCommand = new POPGetCommand(getMail);
                xmlCommandFile.listWithCommands.AddRange(getCommand.GetCommands(config.Title));

                lg.LogMessage = "Try write CommandFile";
                lg.Executor();
                readWrite.Write(xmlCommandFile);
                getMail.Disconnect();
            }
            catch (Exception e)
            {
                lg.LogMessage = e.Message;
                lg.Executor();
            }
            finally
            {
                lg.LogMessage = "-----FINISH-----";
                lg.Executor();
            }
            //Console.ReadLine();
        }
    }
}
