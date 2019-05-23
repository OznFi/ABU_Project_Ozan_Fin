using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using Microsoft.VisualBasic;

namespace Licence_Project
{
    public partial class Choice : Form
    {
        public Choice(string chid)
        {
            InitializeComponent();
            Choice_ID_Label.Text = chid;
        }

        private void Patient_Register_Choice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Patient_Registration rg = new Patient_Registration(Choice_ID_Label.Text);
            rg.Show();
        }

        private void View_Patient_Button_Click(object sender, EventArgs e)
        {
            this.Close();
            Patients py = new Patients(Choice_ID_Label.Text);
            py.Show();
        }

        private void Clashing_Med_Choice_Click(object sender, EventArgs e)
        {
            this.Hide();
            Conflicting_Medicines meco = new Conflicting_Medicines(Choice_ID_Label.Text);
            meco.Show();
        }
        
        
        private void View_Statistics_Button_Click(object sender, EventArgs e)
        {
            Linear_Regression ko = new Linear_Regression();
            this.Hide();
            ko.Show();
        }
    }
}
