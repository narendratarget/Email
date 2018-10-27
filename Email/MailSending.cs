using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Email
{
    public partial class MailSending : Form
    {
        Form1 f;
        public MailSending(Form1 f)
        {
            InitializeComponent();
            this.f = f;
            label2.Text = "To:" +f.varto + "\nCc:" + f.varcc + "\nBcc:" + f.varbcc+"\nAttachment:"+f.att;
        }
        
        static bool mailSent = false;
        public void SendMail()
        {
           
            //Builed The MSG
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            foreach (String to in f.textBox_to.Text.Split(';'))
            {
                msg.To.Add(to);
                f.varto++;
            }
            foreach (String cc in f.textBox_cc.Text.Split(';'))
            {
                if (cc.Length > 0)
                {
                    msg.CC.Add(cc); f.varcc++;
                }
            }
            foreach (String bcc in f.textBox_bcc.Text.Split(';'))
            {
                if (bcc.Length > 0)
                {
                    msg.Bcc.Add(bcc);
                    f.varbcc++;
                }
            }
            label2.Text = "To:" + f.varto + "\nCc:" + f.varcc + "\nBcc:" + f.varbcc + "\nAttachment:" + f.att;
            msg.From = new MailAddress(f.label_signin.Text, "This msg Sent Using Narendra's App( " + f.textBox_subject.Text + " )", System.Text.Encoding.UTF8);

            msg.Subject = f.textBox_subject.Text;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = f.rich_body.Text;
            //  Attachment data = new Attachment(textBox1.Text);
            //    msg.Attachments.Add(data);
            foreach (Label t in f.panel2.Controls)
            {
                Attachment data = new Attachment(t.Text);
                msg.Attachments.Add(data);
                f.att++;
            }

            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;

            //Add the Creddentials
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(f.label_signin.Text, f.label_pass.Text);
            client.Port = 587;//or use 587            
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.SendCompleted += new SendCompletedEventHandler
                (client_SendCompleted);
            object userState = msg;
            try
            {
                //you can also call client.Send(msg)
                client.SendAsync(msg, userState);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show(ex.Message, "Send Mail Error");
            }
        }



        void client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage mail = (MailMessage)e.UserState;
            string subject = mail.Subject;

            if (e.Cancelled)
            {
                string cancelled = string.Format("[{0}] Send canceled.", subject);
                MessageBox.Show(cancelled);
            }
            if (e.Error != null)
            {
                string error = String.Format("[{0}] {1}", subject, e.Error.ToString());
                MessageBox.Show(error);
            }
            else
            {
                label1.Text = "Sent Successfully...";
                pictureBox1.Hide();
                label1.Location = new Point(linkLabel1.Location.X - 250, label1.Location.Y);
                label2.Location = new Point(linkLabel1.Location.X - 250, label2.Location.Y);
               /// MessageBox.Show("Mail sent successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            mailSent = true;
        }

        private void MailSending_Load(object sender, EventArgs e)
        {
           SendMail();
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
