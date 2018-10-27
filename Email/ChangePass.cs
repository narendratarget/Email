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
    public partial class ChangePass : Form
    {
        String email;
        public ChangePass(String email)
        {
            InitializeComponent();
            this.email = email;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            int flag = 0;
            doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
            foreach (XmlNode node in doc.SelectNodes("Emailsetup/Account"))
            {
                if (node.SelectSingleNode("Email").InnerText == textBox_email.Text && node.SelectSingleNode("Password").InnerText == textBox_old_pass.Text)
                {
                    if (textBox_new_pass.Text == textBox_retype_pass.Text)
                    {
                        if (textBox_email.Enabled == true)
                        {
                            node.SelectSingleNode("Email").InnerText = textBox_email.Text;
                        }

                        node.SelectSingleNode("Password").InnerText = textBox_new_pass.Text;
                        flag = 1;
                    }
                    else
                    {
                        textBox_retype_pass.Text = "";
                        textBox_new_pass.Text = "";
                        MessageBox.Show("Password not matched");

                    }
                }
                else
                    MessageBox.Show("Old Password not matched");
            }
            
            if (flag == 1)
            {
                doc.Save(Application.CommonAppDataPath + "/emailsetup.xml");
                MessageBox.Show("Changes are Saved Successfully");
                this.Close();
            }
        }

        private void ChangePass_Load(object sender, EventArgs e)
        {
            textBox_email.Text = email;
        }
    }
}
