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
    public partial class Session_List : Form
    {
        public Session_List(string patid, string patnam, string medname, Boolean end, string begdat, string enddat, string initialamount)
        {

            InitializeComponent();
            Patient_ID_SesLabel.Text = patid;
            Patient_Name_SesLabel.Text = patnam;
            Sess_Med_Name.Text = medname;
            situation = end;
            begin = begdat;
            endo = enddat;
            firstamount = initialamount;
            fill();
        }
        public static SqlConnection sescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        Boolean situation;
        string nope, begin, endo, actualend, actualbegin, firstamount;
        DateTime beg, ed;
   
        public void fill()
        {
            sescon.Open();
            string sef=  "SELECT Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone_Consistency, Attention_Span, Score, Used_Amount FROM Session_Table WHERE Medication = '"+Sess_Med_Name.Text+"'"+"AND ID = "+Patient_ID_SesLabel.Text+"AND Date>= '"+actualbegin+"' ORDER BY Date";
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
                sef = "SELECT Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone_Consistency, Attention_Span, Score, Used_Amount FROM Session_Table WHERE Medication='" + Sess_Med_Name.Text + "'" + "AND ID=" + Patient_ID_SesLabel.Text + "AND Date BETWEEN '" + actualbegin + "' AND '" + actualend + "' ORDER BY Date";
            }
            
            
            SqlCommand sescomm = new SqlCommand(sef, sescon);
            
            DataSet des = new DataSet();
            SqlDataAdapter seadp = new SqlDataAdapter(sef, sescon);
            seadp.Fill(des,"Patient_Sessions");
            Patient_Session_Grid.DataSource = des.Tables[0];
            sescon.Close();
            if (situation == true)
            {
                Add_New_Session_Button.Enabled = false;
            }
            Patient_Session_Grid.Columns[0].Width = 65;
            Patient_Session_Grid.Columns[3].Width = 60;
            Patient_Session_Grid.Columns[4].Width = 60;
            Patient_Session_Grid.Columns[5].Width = 60;
            Patient_Session_Grid.Columns[6].Width = 60;
            Patient_Session_Grid.Columns[7].Width = 60;
            Patient_Session_Grid.Columns[8].Width = 60;
            Patient_Session_Grid.Columns[9].Width = 60;
            Patient_Session_Grid.Columns[3].HeaderText = "Sleep Quality";
            Patient_Session_Grid.Columns[5].HeaderText = "Eye Contact";
            Patient_Session_Grid.Columns[6].HeaderText = "Voice Tone";
            Patient_Session_Grid.Columns[7].HeaderText = "Attention Span";
            Patient_Session_Grid.Columns[9].HeaderText = "Used Amount (mg)";

        }

        private void Session_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            Patient_Med_History jo = new Patient_Med_History(Patient_ID_SesLabel.Text, Patient_Name_SesLabel.Text);
            this.Hide();
            jo.Show();
        }

        private void Add_New_Session_Button_Click(object sender, EventArgs e)
        {
            DateTime jok = DateTime.Now;
            if ((beg - jok).TotalDays <= -30)
            {
                
            }
            this.Hide();
            Add_Session_Form sesa = new Add_Session_Form(Patient_ID_SesLabel.Text, Patient_Name_SesLabel.Text, Sess_Med_Name.Text, situation, nope, begin, endo, firstamount);
            sesa.Show();
        }
        string datecarry;
        object dat;
        int selectint;
        private void Patient_Session_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectint = e.RowIndex;
            DataGridViewRow row = new DataGridViewRow();
            row = Patient_Session_Grid.Rows[selectint];
            if (e.ColumnIndex == 0)
            {
                if (!(row.Cells[2].Value == DBNull.Value || row.Cells[2].Value == null || String.IsNullOrWhiteSpace(row.Cells[2].Value.ToString())))
                {
                    dat = row.Cells[2].Value;
                    datecarry = Convert.ToString(dat);
                    View_Session__Notes vio = new View_Session__Notes(Patient_ID_SesLabel.Text, Sess_Med_Name.Text, datecarry);
                    vio.Show();
                }
            }
        }

        private void View_Graph_Button_Click(object sender, EventArgs e)
        {
            Patient_Att_Graph uo = new Patient_Att_Graph(Patient_ID_SesLabel.Text, Sess_Med_Name.Text, begin, endo);
            this.Hide();
            uo.Show();
        }
    }
}
