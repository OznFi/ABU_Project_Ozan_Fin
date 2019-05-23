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
    public partial class Extra_Medication : Form
    {
        public Extra_Medication(string nam, string id, string[] listo)
        {
            InitializeComponent();
            Ex_Med_Name.Text = nam;
            Ex_Med_ID.Text = id;
            clear2 = listo;
            
        }
        string exdat;
        string[] clear2;
        public List<string> lists=new List<string>();
        DateTime ne;
        int actid;
        DateTime? end = null;
        string yearstr;
        string actualdate;
        DateTime actd;
        string ok = DateTime.Now.ToString();
        DateTime picko;
        public static SqlConnection excon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection exconf = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        private void Extra_Med_Register_Button_Click(object sender, EventArgs e)
        {
            if (clear2.Contains(Extra_Med_Box.Text))
            {
                MessageBox.Show("This medication is already being used by the patient");
            }
            else
            {
                if (checkconf()==true)
                {
                    MessageBox.Show("The suggested medication is in conflict with another medication, check the medication conflict table");
                }
                if (checkconf() == false)
                {
                    picko = Convert.ToDateTime(ok.ToString());
                    yearstr = picko.Year.ToString();
                    if (Specific_MedCheck.Checked == true)
                    {
                        actualdate = yearstr + "-" + Extra_Med_Month.Text + "-" + Extra_Med_Day.Text;
                        actd = Convert.ToDateTime(actualdate);
                    }
                    if (Today_MedCheck.Checked == true)
                    {
                        actd = DateTime.Now;
                    }
                    excon.Open();
                    actid = Convert.ToInt32(Ex_Med_ID.Text);
                    double jo = Convert.ToDouble(New_MedAmount.Text);
                    string ho2 = "INSERT INTO Patient_Medication_Historty(ID, Name, Medication, Starting_Date, End_Date, Amount_Used)";
                    ho2 += "VALUES (@ID, @Name, @Medication, @Starting_Date, @End_Date, @Amount_Used)";
                    SqlCommand excom = new SqlCommand(ho2, excon);
                    //exdat = Extra_Med_Year.Text + "-" + Extra_Med_Month.Text + "-" + Extra_Med_Day.Text;
                    
                    excom.Parameters.AddWithValue("Name", Ex_Med_Name.Text);
                    excom.Parameters.AddWithValue("ID", actid);
                    excom.Parameters.AddWithValue("Medication", Extra_Med_Box.Text);
                    excom.Parameters.AddWithValue("Starting_Date", actd);
                    excom.Parameters.AddWithValue("End_Date", DBNull.Value);
                    excom.Parameters.AddWithValue("Amount_Used", jo);
                    excom.ExecuteNonQuery();
                    MessageBox.Show("Medication Registered");
                    this.Hide();
                    Patient_Med_History gobak = new Patient_Med_History(Ex_Med_ID.Text, Ex_Med_Name.Text);
                    gobak.Show();
                    excon.Close();
                }
            }
        }
        string meda;
        string modea;
        private Boolean checkconf()
        {
            exconf.Open();
            for(int i=0; i < clear2.Length; i++)
            {
                meda = Extra_Med_Box.Text + "-" + clear2[i];
                modea = clear2[i] + "-" + Extra_Med_Box.Text;
                string copf = "SELECT COUNT(*) FROM Medicine_Conflict WHERE(The_Conflict= @The_Conflict)";
                SqlCommand jun = new SqlCommand(copf, exconf);
                jun.Parameters.AddWithValue("@The_Conflict",meda);
                int confexist = (int)jun.ExecuteScalar();
                if (confexist >0)
                {
                    exconf.Close();
                    return true;
                }
                SqlCommand cope = new SqlCommand(copf, exconf);
                cope.Parameters.AddWithValue("@The_Conflict", modea);
                int conexist2 = (int)cope.ExecuteScalar();
                if (conexist2 > 0)
                {
                    exconf.Close();
                    return true;
                }
            }
            exconf.Close();
            return false;
            
        }

        private void Today_MedCheck_CheckedChanged(object sender, EventArgs e)
        {
            if(Extra_Med_Month.Enabled)
            {
                Extra_Med_Month.Enabled= !(Today_MedCheck.CheckState == CheckState.Checked);
                Extra_Med_Day.Enabled=!(Today_MedCheck.CheckState == CheckState.Checked);
            }
            Specific_MedCheck.Enabled = !(Today_MedCheck.CheckState == CheckState.Checked);
        }

        private void Specific_MedCheck_CheckedChanged(object sender, EventArgs e)
        {
            Today_MedCheck.Enabled = !(Specific_MedCheck.CheckState == CheckState.Checked);
            Extra_Med_Month.Enabled = true;
            Extra_Med_Day.Enabled = true;
        }

        private void View_AVMeds_But_Click(object sender, EventArgs e)
        {
            Short_Single_Medicine_List op = new Short_Single_Medicine_List();
            op.Show();
        }
    }
}
