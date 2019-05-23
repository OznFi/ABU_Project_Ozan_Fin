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
    public partial class Conflicting_Medicines : Form
    {
        public Conflicting_Medicines(string forid)
        {
            InitializeComponent();
            Carry_ID_Lab.Text = forid;
            conflictfill();
        }
        SqlConnection displayconf = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog = Alternative Medicine;Integrated Security = True");
        private void Add_Conflict_Button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Add_Medicine_Conflict gof = new Add_Medicine_Conflict(Carry_ID_Lab.Text);
            gof.Show();
        }
        private void conflictfill()
        {
            displayconf.Open();
            string confcom = "SELECT The_Conflict FROM Medicine_Conflict ORDER BY The_Conflict";
            SqlCommand confillcom = new SqlCommand(confcom, displayconf);
            DataSet confset = new DataSet();
            SqlDataAdapter confadp = new SqlDataAdapter(confcom, displayconf);
            confadp.Fill(confset, "Goconf");
            Conflicting_Meds_Grid.DataSource = confset.Tables[0];
            displayconf.Close();
            Conflicting_Meds_Grid.Columns[0].Width = 200;
        }

        private void Conflicting_Medicines_FormClosing(object sender, FormClosingEventArgs e)
        {
            Choice ko = new Choice(Carry_ID_Lab.Text);
            this.Hide();
            ko.Show();
        }
    }
}
