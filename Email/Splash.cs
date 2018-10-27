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
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i < 100)
                i++;
            else
            {
                timer1.Enabled = false;

                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(Application.CommonAppDataPath + "/emailsetup.xml");
                    new Login().Show();
                    this.Hide();
                }
                catch
                {
                    new First().Show();
                    this.Hide();
                }

               
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
           
            
        }
    }
}
