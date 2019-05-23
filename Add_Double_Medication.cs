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
    public partial class Add_Double_Medication : Form
    {
        public Add_Double_Medication(string id, string name)
        {
            InitializeComponent();
            Patient_Id_Label.Text = id;
            Patient_Name_Lab.Text = name;
        }
        public static SqlConnection excon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string ok = DateTime.Now.ToString();
        DateTime picko;
        string yearstr, first, second;
        string actualdate ;
        double altjo1 = 0;
        double altjo2 = 0;
        DateTime actd;
        int actid;

        private void checkcombo(string medone, string medtwo)
        {
            string combo1 = medone + "-" + medtwo;
            string combo2 = medtwo + "-" + medone;
            string checko = "SELECT COUNT (*) FROM Double_Medication_Historty WHERE Full_Combination IN('"+combo1+"', '"+combo2+"')";
            excon.Open();
            SqlCommand checkcomb = new SqlCommand(checko, excon);
            int comexist = (int)checkcomb.ExecuteScalar();
            
            if (comexist > 0)
            {
                string checkon = "SELECT COUNT(*) FROM Double_Medication_Historty WHERE First_Medication='" + medtwo + "' AND Full_Combination IN('" + combo1 + "', '" + combo2 + "')";
                SqlCommand checkone = new SqlCommand(checkon, excon);
                int comone = (int)checkone.ExecuteScalar();
                if (comone > 0)
                {
                    first = Second_Reg_MedBox.Text;
                    second = First_Reg_MedBox.Text;
                    altjo1 = Convert.ToDouble(Second_RegAmount_Box.Text);
                    altjo2 = Convert.ToDouble(First_RegAmount_Box.Text);
                }
                else
                {
                    first = First_Reg_MedBox.Text;
                    second = Second_Reg_MedBox.Text;
                    altjo1 = Convert.ToDouble(First_RegAmount_Box.Text);
                    altjo2 = Convert.ToDouble(Second_RegAmount_Box.Text);
                }
            }
            else
            {
                first = First_Reg_MedBox.Text;
                second = Second_Reg_MedBox.Text;
                altjo1 = Convert.ToDouble(First_RegAmount_Box.Text);
                altjo2 = Convert.ToDouble(Second_RegAmount_Box.Text);
            }
            excon.Close();
        }

        private void Register_DoubleMedBut_Click(object sender, EventArgs e)
        {
            checkcombo(First_Reg_MedBox.Text, Second_Reg_MedBox.Text);
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
            string combo = first + "-" + second;
            string combo2 = second + "-" + first;
            //actid = Convert.ToInt32(First_Reg_MedBox.Text);
            string checkconfstr = "SELECT COUNT (*) FROM Medicine_Conflict WHERE The_Conflict IN('" + combo + "', '" + combo2 + "')";
            SqlCommand checkconf = new SqlCommand(checkconfstr, excon);
            int conflict0 = (int)checkconf.ExecuteScalar();
            excon.Close();
            if (conflict0 > 0)
            {
                DialogResult res = MessageBox.Show("This combination is also in the medication conflict list. Do you want to add to patient's medications? ", "Exit", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    excon.Open();
                    double jo = altjo1;
                    double jo2 = altjo2;
                    string ho2 = "INSERT INTO Double_Medication_Historty(ID, Name, First_Medication, Second_Medication, Starting_Date, End_Date, First_Used_Amount, Second_Used_Amount, Full_Combination)";
                    ho2 += "VALUES (@ID, @Name, @First_Medication, @Second_Medication, @Starting_Date, @End_Date, @First_Used_Amount, @Second_Used_Amount, @Full_Combination)";
                    SqlCommand excom = new SqlCommand(ho2, excon);

                    excom.Parameters.AddWithValue("ID", Patient_Id_Label.Text);
                    excom.Parameters.AddWithValue("Name", Patient_Name_Lab.Text);
                    excom.Parameters.AddWithValue("First_Medication", first);
                    excom.Parameters.AddWithValue("Second_Medication", second);
                    excom.Parameters.AddWithValue("Starting_Date", actd);
                    excom.Parameters.AddWithValue("End_Date", DBNull.Value);
                    excom.Parameters.AddWithValue("First_Used_Amount", jo);
                    excom.Parameters.AddWithValue("Second_Used_Amount", jo2);
                    excom.Parameters.AddWithValue("Full_Combination", first + "-" + second);
                    excom.ExecuteNonQuery();
                    MessageBox.Show("Medication Registered");
                    excon.Close();
                }
            }
            else
            {
                excon.Open();
                double jo = altjo1;
                double jo2 = altjo2;
                string ho2 = "INSERT INTO Double_Medication_Historty(ID, Name, First_Medication, Second_Medication, Starting_Date, End_Date, First_Used_Amount, Second_Used_Amount, Full_Combination)";
                ho2 += "VALUES (@ID, @Name, @First_Medication, @Second_Medication, @Starting_Date, @End_Date, @First_Used_Amount, @Second_Used_Amount, @Full_Combination)";
                SqlCommand excom = new SqlCommand(ho2, excon);

                excom.Parameters.AddWithValue("ID", Patient_Id_Label.Text);
                excom.Parameters.AddWithValue("Name", Patient_Name_Lab.Text);
                excom.Parameters.AddWithValue("First_Medication", first);
                excom.Parameters.AddWithValue("Second_Medication", second);
                excom.Parameters.AddWithValue("Starting_Date", actd);
                excom.Parameters.AddWithValue("End_Date", DBNull.Value);
                excom.Parameters.AddWithValue("First_Used_Amount", jo);
                excom.Parameters.AddWithValue("Second_Used_Amount", jo2);
                excom.Parameters.AddWithValue("Full_Combination", first + "-" + second);
                excom.ExecuteNonQuery();
                MessageBox.Show("Medication Registered");
                excon.Close();
            }
        }
    }
}
