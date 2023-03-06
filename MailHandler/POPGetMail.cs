using System;
using System.Collections.Generic;
using System.Text;
using Spire.Email.Pop3;
using Spire.Email;
using System.Linq;
using XmlCommandLibrary;

namespace MailHandler
{
    class POPGetMail
    {
        private string Host;
        private string Username;
        private string Password;
        private Pop3Client Pop3;



        public POPGetMail(string host, string username, string password)
        {
            Host = host;
            Username = username;
            Password = password;
            Pop3 = new Pop3Client();
        }

        public Pop3Client POP3
        {
            get
            {
                return Pop3;
            }
        }

        public void Set()
        {
            Pop3.Host = Host;
            Pop3.Username = Username;
            Pop3.Password = Password;
            Pop3.Port = 995;
            Pop3.EnableSsl = true;
            //Pop3.Timeout = 3000000;
        }
        public void Connect()
        {
            Pop3.Connect();              
        }
        public void Disconnect()
        {
            Pop3.Disconnect();
        }

    }

    class POPGetCommand
    {
        private POPGetMail PopGetMail;
        private List<string> CommandsList;
        public POPGetCommand(POPGetMail popGetMail)
        {
            PopGetMail = popGetMail;
        }

        public List<string > GetCommands(string title)
        {
            CommandsList = new List<string>();
            int count = PopGetMail.POP3.GetMessageCount();
            for (int i = 1; i <= count; i++)
            {
                MailMessage mm = PopGetMail.POP3.GetMessage(i);
                if (mm.Subject == title)
                {
                    CommandsList.Add((mm.BodyText).Replace("\r\n", ""));
                }
                PopGetMail.POP3.DeleteMessage(i);               
            }            
            return CommandsList;
        }
    }

}
