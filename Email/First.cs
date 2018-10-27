using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Email
{
    public partial class First : Form
    {
        public First()
        {
            InitializeComponent();
        }
        public void createxml()
        {
            try
            {

                XmlTextWriter xtw = new XmlTextWriter(Application.CommonAppDataPath + "/emailsetup.xml", Encoding.UTF8);
                // XmlTextWriter xtw = new XmlTextWriter(@"../../Resources/emailsetup.xml", Encoding.UTF8);
                xtw.Formatting = Formatting.Indented;
                xtw.WriteStartElement("Emailsetup");

                xtw.WriteStartElement("Account");

                xtw.WriteStartElement("Email");        //<email> 
                xtw.WriteString(textBox_email.Text);
                xtw.WriteEndElement();               ///</Email>

                xtw.WriteStartElement("Password");  ////<pass>
                xtw.WriteString(textBox_pass.Text);
                xtw.WriteEndElement();              //</pass>

                xtw.WriteStartElement("Hint");  ////<hint>
                xtw.WriteString(textBox_hint.Text);
                xtw.WriteEndElement();              //</hint>

                xtw.WriteEndElement();             //</account>

                xtw.WriteEndElement();            /////</emailsetup>
                xtw.Close();
            }
            catch {
                MessageBox.Show("Error", "Some Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            try
            {

                doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
                XmlNode account = doc.CreateElement("Account");

                bool f=true;
                foreach (XmlNode node in doc.SelectNodes("Emailsetup/Account"))
                {
                    if (node.SelectSingleNode("Email").InnerText == textBox_email.Text)
                    {
                        f = false;
                        break; ;
                    }
                }

                if (f)
                {
                    XmlNode email = doc.CreateElement("Email");
                    email.InnerText = textBox_email.Text;
                    account.AppendChild(email);

                    XmlNode pass = doc.CreateElement("Password");
                    pass.InnerText = textBox_pass.Text;
                    account.AppendChild(pass);

                    XmlNode hint = doc.CreateElement("Hint");
                    hint.InnerText = textBox_hint.Text;
                    account.AppendChild(hint);

                    doc.DocumentElement.AppendChild(account);
                  
                    doc.Save(Application.CommonAppDataPath + "/emailsetup.xml");
                    MessageBox.Show("Account added Successfully");
                }
                else
                {
                    MessageBox.Show("Error", "Email already registered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                createxml();
                doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
                MessageBox.Show("Account added Successfully");
                // doc.Load(@"../../Resources/emailsetup.xml");
            }
            
           
            
            this.Close();
        }

        private void First_FormClosing(object sender, FormClosingEventArgs e)
        {
        
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
