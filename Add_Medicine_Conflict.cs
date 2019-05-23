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
    public partial class Add_Medicine_Conflict : Form
    {
        public Add_Medicine_Conflict(string useless)
        {
            InitializeComponent();
            Useless_Lab.Text = useless;
        }
        string constr;
        string fullconf;
        string fullconf2;
        string constr2;
        new SqlConnection confcon= new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        private void Register_New_Conflict_Click(object sender, EventArgs e)
        {
            confcon.Open();
            fullconf = First_MedBox.Text + "-" + Conf_MedBox.Text;
            fullconf2 = Conf_MedBox.Text + "-" + First_MedBox.Text;
            constr = "INSERT INTO Medicine_Conflict (First_Medicine, Second_Medicine, The_Conflict)";
            constr+= "VALUES (@First_Medicine, @Second_Medicine, @The_Conflict)";
            constr2 = "INSERT INTO Medicine_Conflict (The_Conflict)";
            constr2 += "VALUES (@The_Conflict)";
            SqlCommand adconfm2 = new SqlCommand(constr2, confcon);
            SqlCommand adconfcom = new SqlCommand(constr, confcon);
            adconfm2.Parameters.AddWithValue("@The_Conflict", fullconf2);
            adconfcom.Parameters.AddWithValue("@First_Medicine", First_MedBox.Text);
            adconfcom.Parameters.AddWithValue("@Second_Medicine", Conf_MedBox.Text);
            adconfcom.Parameters.AddWithValue("@The_Conflict", fullconf);
            adconfcom.ExecuteNonQuery();
            adconfm2.ExecuteNonQuery();
            MessageBox.Show("Medication conflict has been registered");
            First_MedBox.Text = "";
            Conf_MedBox.Text = "";
            confcon.Close();
        }

        private void Add_Medicine_Conflict_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Conflicting_Medicines hof = new Conflicting_Medicines(Useless_Lab.Text);
            hof.Show();
        }
    }
}
