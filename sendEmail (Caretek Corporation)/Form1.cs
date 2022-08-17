using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sendEmail__Caretek_Corporation_
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
            The system will generate a new file using Memory Stream with name format "MMDDYYYY_HHMMSS.txt". For example the file name will be "01232022_153524.txt". The file should be created in memory only using Memory Stream and should not be saved on hard drive. 
            Then System will put some random text in that file in the following way 
            Name: [Some Random Text of length between 3- 20] space [Some Random Text of length between 3- 20].   
            Age: Some Random number between 20 and 100. 
             For example 
            Name:   Rene Davis 
            Age: 54. 

            Then send it through Gmail with following subject 

            PatientReport_ + file name 

            For example subject will be “PatientReport_01232022_153524.txt" 
            */

            //made by Muhammad Naeem Tariq

            //creating memory stream
            string fileName = DateTime.Now.ToString("MMddyyyy_HHmmss") + ".txt";
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.StreamWriter writer = new System.IO.StreamWriter(ms);
            string fileContent = "Name: " + RandomString(3, 20) + " " + RandomString(3, 20) + "\n" + "Age: " + RandomNumber(20, 100);
            writer.Write(fileContent);
            writer.Flush();
            ms.Seek(0, System.IO.SeekOrigin.Begin);


            //sending email
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Plain);
            System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
            attach.ContentDisposition.FileName = fileName;
            attach.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Attachment;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new System.Net.Mail.MailAddress("gointoall@gmail.com");
            msg.To.Add(new System.Net.Mail.MailAddress("gointoall@gmail.com"));
            msg.Subject = "PatientReport_" + fileName;
            msg.Body = "";
            msg.Attachments.Add(attach);
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("gointoall@gmail.com", "pass");
            smtp.EnableSsl = true;
            smtp.Send(msg);

            MessageBox.Show("Email has been sent");
            this.Close();

        }
        Random random = new Random();
        public string RandomString(int length, int max)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, RandomNumber(length, max))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}

