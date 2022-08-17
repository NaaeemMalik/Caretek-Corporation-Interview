using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

using MailKit.Net.Pop3;
using MailKit;
using MimeKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System.IO;

namespace getEmail__Caretek_Corporation_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
             * check email where subject starts with PatientReport_
             Name: [Some Random Text of length between 3- 20] space [Some Random Text of length between 3- 20].   
            Age: Some Random number between 20 and 100. 
            file name: MMDDYYYY_HHMMSS.txt
             For example 
            Name:   Rene Davis 
            Age: 54. 
            */
//get new mails using MailKit
            var mailRepository = new MailRepository("imap.gmail.com", 993, true, "gointoall@gmail.com", "pass");
           mailRepository.GetAllMails();
            //exit
            this.Close();

        }

      
    }
public class MailRepository : Form
{
    private readonly string mailServer, login, password;
    private readonly int port;
    private readonly bool ssl;

    public MailRepository(string mailServer, int port, bool ssl, string login, string password)
    {
        this.mailServer = mailServer;
        this.port = port;
        this.ssl = ssl;
        this.login = login;
        this.password = password;
    }



    public void GetAllMails()
    {

        using (var client = new ImapClient())
        {
            client.Connect(mailServer, port, ssl);

            // Note: since we don't have an OAuth2 token, disable
            // the XOAUTH2 authentication mechanism.
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            client.Authenticate(login, password);

            // The Inbox folder is always available on all IMAP servers...
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);
            var results = inbox.Search(SearchOptions.All, SearchQuery.DeliveredAfter(DateTime.Now.AddMinutes(-10) ));
            foreach (var uniqueId in results.UniqueIds)
            {
                var msg = inbox.GetMessage(uniqueId);
                    if (msg.Subject.IndexOf("PatientReport_")==0 && msg.Subject.EndsWith(".txt") && msg.Subject.Length==33)
                    {
                        var at= msg.Attachments.FirstOrDefault();
                        if (at is MimePart part && part.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            using (var stream = new MemoryStream())
                            {
                                part.Content.DecodeTo(stream);
                                stream.Position = 0;
                                using (var reader = new StreamReader(stream))
                                {
                                    //first line is Name
                                    var name = reader.ReadLine();
                                    //second line is Age
                                    var age = reader.ReadLine();
                                    //remove Name: from name
                                    name = name.Substring(6);
                                    //remove Age: from age
                                    age = age.Substring(4);
                                    MessageBox.Show( name + "\n" + age, msg.Subject );
                                }
                            }
                        }

                    }
                }

                client.Disconnect(true);
        }

    }
}

}