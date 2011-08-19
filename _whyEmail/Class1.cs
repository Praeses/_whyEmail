using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.IO;
using RazorEngine;

namespace _whyEmail
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine( "OMG" );
            Console.ReadLine();

            StuffUNeed stuff = new StuffUNeed();
            stuff.To = "zac.kleinpeter@praeses.com";
            stuff.From = "_why@theluckystiff.ruby";
            stuff.Subject = "hi";

            var w00t = new OMG<StuffUNeed>(stuff,"C:\\tmp\\users.txt", stuff);
            Console.WriteLine( w00t.Send() );
            Console.ReadLine();
        }
    }

    public class OMG<T>
    {
        private MailMessage _mailMsg;
        public string FilePath { get; set; }
        public T JsonStuff { get; set; }

        public OMG( StuffUNeed foo, string FILE_PATH, T JSONSTUFF )
        {
            FilePath = FILE_PATH;
            JsonStuff = JSONSTUFF;
            // To
            _mailMsg = new MailMessage();
            _mailMsg.To.Add( foo.To );

            // From
            MailAddress mailAddress = new MailAddress( foo.From );
            _mailMsg.From = mailAddress;

            // Subject and Body
            _mailMsg.Subject = foo.Subject;
        }

        public bool Send()
        {
            _mailMsg.Body = CreateBody();

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient( "mail.praeses.com", Convert.ToInt32( 25 ) );
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential( "mwoods79", "fourrandomwords" );
            smtpClient.Credentials = credentials;
  
            //smtpClient.Send( _mailMsg );
            smtpClient.SendAsync( _mailMsg, null );
            return true;
        }

        public string CreateBody()
        {
            string body = "";
            TextReader reader = new StreamReader( FilePath );
            body = Razor.Parse( reader.ReadToEnd(), JsonStuff );
            return body;
        }
    }

    public struct StuffUNeed
    {
      public string To { get; set; }
      public string From { get; set; }
      public string CC { get; set; }
      public string Subject { get; set; }
    }
}
