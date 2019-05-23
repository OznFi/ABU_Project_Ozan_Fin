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
    public partial class Patients : Form
    {
        public Patients(string wt)
        {
            InitializeComponent();
            fill();
            PatientIDListLabel.Text = wt;
        }
        //List<int> b = new List<int>();
        public static SqlConnection patcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public void fill()
        {
            patcon.Open();
            string f = "SELECT Name, Age, Occupation, Phone_Number, ID, Responsible_User_ID FROM Patient_List";
            SqlCommand filcom = new SqlCommand(f, patcon);
            DataSet dt = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter("SELECT Name, Age, Occupation, Phone_Number, ID FROM Patient_List", patcon);
            adp.Fill(dt, "Patients");
            Patients_ID_View.DataSource = dt.Tables[0];
            Patients_ID_View.Columns[5].HeaderText = "Phone Number";
            Patients_ID_View.Columns[0].Width = 76;
            Patients_ID_View.Columns[1].Width = 76;
            //Patients_ID_View.Columns[0].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9pt);
            patcon.Close();
        }

        private void Patients_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Choice gk = new Choice(PatientIDListLabel.Text);
            gk.Show();
        }
        int selectedrownindex;
        object g;
        object g2;
        string pasd;
        string pasd2;
        private void Patients_ID_View_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            selectedrownindex = e.RowIndex;
            DataGridViewRow row = new DataGridViewRow();
            row = Patients_ID_View.Rows[selectedrownindex];
            if (e.ColumnIndex == 0)
            {
                if (!(row.Cells[2].Value == DBNull.Value || row.Cells[2].Value == null || String.IsNullOrWhiteSpace(row.Cells[2].Value.ToString())))
                {
                    g = row.Cells[6].Value;
                    g2 = row.Cells[2].Value;
                    pasd = Convert.ToString(g);
                    pasd2 = Convert.ToString(g2);
                    this.Hide();
                    Patient_Med_History mef = new Patient_Med_History(pasd, pasd2);
                    mef.Show();
                }
                else
                {
                   
                }
            }
            if (e.ColumnIndex == 1)
            {
                if (!(row.Cells[2].Value == DBNull.Value || row.Cells[2].Value == null || String.IsNullOrWhiteSpace(row.Cells[2].Value.ToString())))
                {
                    g = row.Cells[6].Value;
                    g2 = row.Cells[2].Value;
                    pasd = Convert.ToString(g);
                    pasd2 = Convert.ToString(g2);
                    Patient_Double_Med_History jok = new Patient_Double_Med_History(pasd, pasd2);
                    this.Hide();
                    jok.Show();
                }
            }
        }
        //int a;
        //int d;
        //int c = 0;
        //public static SqlConnection dupcon = new SqlConnection("Data Source=OZAN-BILGISAYAR\\SQLEXPRESS;Initial Catalog=Alternative Medicine;Integrated Security=True");
        //public void dupe()
        //{
        //  dupcon.Open();
        //string ko = "SELECT COUNT(*) FROM Patient_List WHERE(ID = @ID)";
        //SqlCommand ju = new SqlCommand(ko, dupcon);
        //Random r = new Random();
        //a = r.Next(10000, 99999);
        //d = a;
        //ju.Parameters.AddWithValue("@ID", a);
        //int pass = (int)ju.ExecuteScalar();
        //if (pass > 0)
        //{
        //  b.Insert(c, d);
        //}
        //if (b.Contains(a))
        //  dupe();
        //else
        //{

        //  b.Insert(c, d);
        //c++;
        //}
        //}        

    }
}
