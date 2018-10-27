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
    public partial class AccountSwitch : Form
    {
        Form1 f;
        public AccountSwitch(Form1 home)
        {
            InitializeComponent();
            f = home;
            label2.Text = "logged in as :"+home.label_signin.Text;
           
        }

        private void AccountSwitch_Load(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
                Dictionary<String,String> comboSource = new Dictionary<String,String>();

                comboSource.Add("Select", "Select");
                  
                foreach (XmlNode node in doc.SelectNodes("Emailsetup/Account"))
                {
                    comboSource.Add(node.SelectSingleNode("Email").InnerText, node.SelectSingleNode("Password").InnerText);
                  
                }
                comboBox1.DataSource = new BindingSource(comboSource, null);
                comboBox1.DisplayMember = "Key";
                comboBox1.ValueMember = "Value";
            }
            catch {
                MessageBox.Show("Error", "Some Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (((KeyValuePair<String, String>)comboBox1.SelectedItem).Key != "Select")
            {
                f.label_signin.Text = ((KeyValuePair<String, String>)comboBox1.SelectedItem).Key;
                f.label_pass.Text = ((KeyValuePair<String, String>)comboBox1.SelectedItem).Value;
                this.Close();
            }
        }
    }
}
