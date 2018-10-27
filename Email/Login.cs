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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            First f = new First();
            f.ShowDialog();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string hint = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
            int flag = 0;
            foreach (XmlNode node in doc.SelectNodes("Emailsetup/Account"))
            {
                if (node.SelectSingleNode("Email").InnerText==textBox_email.Text )
                {
                    if (node.SelectSingleNode("Password").InnerText == textBox_pass.Text)
                    {
                        flag = 1;
                    }
                    else
                        hint = node.SelectSingleNode("Hint").InnerText;
                }
                
            }

            if (flag == 1)
            {
                Form1 f = new Form1();
                f.label_signin.ResetText();
                f.label_signin.Text = textBox_email.Text;
                f.label_pass.Text = textBox_pass.Text;
                f.label_pass.Hide();
                f.Show();
                this.Hide();
            }
            else
            {
                label_hint.Text = "Hint: " + hint;
                MessageBox.Show("Invalid Login","Error");

            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really want to Exit ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.ExitThread();
            }
        }

    }
}
