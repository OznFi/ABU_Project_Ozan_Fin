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
    public partial class Double_Session_List : Form
    {
        public Double_Session_List(string id, string name, string medname, Boolean end, string begdat, string enddat, string firstamount, string secondamount, string med1, string med2 )
        {
            InitializeComponent();
            Patient_ID_Lab.Text = id;
            Patient_Name_Lab.Text = name;
            Med_Name_Lab.Text = medname;
            situation = end;
            begin = begdat;
            endo = enddat;
            firstamount1 = firstamount;
            secondamount1 = secondamount;
            mede1 = med1;
            mede2 = med2;
            fill();
        }
        public static SqlConnection sescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        Boolean situation;
        int selectint;
        string datecarry, mede1, mede2;
        object dat;
        string nope, begin, endo, actualend, actualbegin, firstamount1, secondamount1;

        private void Double_Session_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            Patient_Double_Med_History jo = new Patient_Double_Med_History(Patient_ID_Lab.Text, Patient_Name_Lab.Text);
            this.Hide();
            jo.Show();
        }

        private void View_Grap_But_Click(object sender, EventArgs e)
        {
            Double_Att_Graph joh = new Double_Att_Graph(Patient_ID_Lab.Text, Med_Name_Lab.Text, begin, endo, Patient_Name_Lab.Text, situation, firstamount1, secondamount1, mede1, mede2);
            this.Hide();
            joh.Show();
        }

        private void Add_New_Session_But_Click(object sender, EventArgs e)
        {
            Add_Double_Session joh = new Add_Double_Session(Patient_ID_Lab.Text, Patient_Name_Lab.Text, Med_Name_Lab.Text, mede1, mede2, begin, firstamount1, secondamount1, situation, endo);
            this.Hide();
            joh.Show();
        }

        private void Patient_Session_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectint = e.RowIndex;
            DataGridViewRow row = new DataGridViewRow();
            row = Patient_Session_Grid.Rows[selectint];
            if (e.ColumnIndex == 0)
            {
                if (!(row.Cells[2].Value == DBNull.Value || row.Cells[2].Value == null || String.IsNullOrWhiteSpace(row.Cells[2].Value.ToString())))
                {
                    dat = row.Cells[3].Value;
                    datecarry = Convert.ToString(dat);
                    View_Double_Session_Notes jog = new View_Double_Session_Notes(Patient_ID_Lab.Text, Med_Name_Lab.Text, datecarry);
                    jog.Show();
                    //View_Session__Notes vio = new View_Session__Notes(Patient_ID_SesLabel.Text, Sess_Med_Name.Text, datecarry);
                    //vio.Show();
                }
            }
        }

        DateTime beg, ed;
        public void fill()
        {
            sescon.Open();
            string sef = "SELECT First_Medication, Second_Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone, Attention_Span, Score, First_Used_Amount, Second_Used_Amount FROM Double_Session_Table WHERE Full_Combination = '" + Med_Name_Lab.Text + "'" + "AND ID = " + Patient_ID_Lab.Text + "AND Date>= '" + actualbegin + "' ORDER BY Date";
            beg = Convert.ToDateTime(begin);
            actualbegin = beg.Year.ToString() + "-" + beg.Month.ToString() + "-" + beg.Day.ToString();
            if (endo == "")
            {

            }
            else
            {
                ed = Convert.ToDateTime(endo);
                actualend = ed.Year.ToString() + "-" + ed.Month.ToString() + "-" + ed.Day.ToString();
                actualbegin = beg.Year.ToString() + "-" + beg.Month.ToString() + "-" + beg.Day.ToString();
                sef = "SELECT First_Medication, Second_Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone, Attention_Span, Score, First_Used_Amount, Second_Used_Amount FROM Double_Session_Table WHERE Full_Combination='" + Med_Name_Lab.Text + "'" + "AND ID=" + Patient_ID_Lab.Text + "AND Date BETWEEN '" + actualbegin + "' AND '" + actualend + "' ORDER BY Date";
            }


            SqlCommand sescomm = new SqlCommand(sef, sescon);

            DataSet des = new DataSet();
            SqlDataAdapter seadp = new SqlDataAdapter(sef, sescon);
            seadp.Fill(des, "Patient_Sessions");
            Patient_Session_Grid.DataSource = des.Tables[0];
            sescon.Close();
            if (situation == true)
            {
                Add_New_Session_But.Enabled = false;
            }
            Patient_Session_Grid.Columns[0].Width = 65;
            Patient_Session_Grid.Columns[4].Width = 60;
            Patient_Session_Grid.Columns[5].Width = 60;
            Patient_Session_Grid.Columns[6].Width = 60;
            Patient_Session_Grid.Columns[7].Width = 60;
            Patient_Session_Grid.Columns[8].Width = 60;
            Patient_Session_Grid.Columns[9].Width = 60;
            Patient_Session_Grid.Columns[10].Width = 60;
            Patient_Session_Grid.Columns[11].Width = 60;
            Patient_Session_Grid.Columns[1].HeaderText = "First Medicine";
            Patient_Session_Grid.Columns[2].HeaderText = "Second Medicine";
            Patient_Session_Grid.Columns[4].HeaderText = "Sleep Quality";
            Patient_Session_Grid.Columns[6].HeaderText = "Eye Contact";
            Patient_Session_Grid.Columns[7].HeaderText = "Voice Tone";
            Patient_Session_Grid.Columns[8].HeaderText = "Attention Span";
            Patient_Session_Grid.Columns[10].HeaderText = "First Medicine Amount (mg)";
            Patient_Session_Grid.Columns[11].HeaderText = "Second Medicine Amount (mg)";

        }


    }
}
