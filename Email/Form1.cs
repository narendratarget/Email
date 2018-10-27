using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Email
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int varto = 0, varcc = 0, varbcc = 0, att = 0;
        private void button_send_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                MailSending m = new MailSending(this);
                m.ShowDialog();
                this.Enabled = true;
                Form1 f = new Form1();
                f.label_signin.ResetText();
                f.label_signin.Text = this.label_signin.Text;
                f.label_pass.Text = this.label_pass.Text;
                f.label_pass.Hide();
                f.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Check you Email Addresses .\n \nEmail Not Sent due to some technical reason\n\nplease Contact Narendra \n\nat +919627950018 or\n\nmail us on narendratarget@gmail.com", "Error ...");
                this.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] str = openFileDialog1.FileNames;
                int ly = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    Label t = new Label();
                    t.Name = "lbl_" + i;
                    t.Text = str[i];
                    t.Width = 250;
                    t.Font=new Font("Arial", 8);

                    t.Location = new Point(10,ly + t.Height+2);
                  
                    panel2.Controls.Add(t);
                    ly = ly + t.Height;
                }
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            foreach (TextBox t in panel2.Controls)
            {
                MessageBox.Show("id=" + t.Name + "\nText=" + t.Text);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePass ch = new ChangePass(label_signin.Text);
            ch.textBox_email.Text = label_signin.Text;
            ch.ShowDialog();
            
        }

        private void label_signin_Click(object sender, EventArgs e)
        {
            AccountSwitch a = new AccountSwitch(this);
            a.ShowDialog();
        }

        private void lbl_logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Accounts a = new Accounts();
            a.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Help h = new Help();
            h.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
