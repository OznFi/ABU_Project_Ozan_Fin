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
    public partial class Add_Double_Session : Form
    {
        public Add_Double_Session(string patsesid, string patsesnam, string medses,string medses1, string medses2, string bego, string firstoldamount, string secondoldamount, Boolean fillervalue, string fillerendat)
        {
            InitializeComponent();
            SesAdd_Patient_ID.Text = patsesid;
            SesAdd_Patient_Name.Text = patsesnam;
            SessAdd_Medication.Text = medses;
            fillertempvalue = fillervalue;
            b = bego;
            fillerendtemp = fillerendat;
            firstmedname = medses1;
            secondmedname = medses2;
            firstnumber = Convert.ToDouble(firstoldamount);
            secondnumber = Convert.ToDouble(secondoldamount);
            fillall();
        }
        SqlConnection adsescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string ok = DateTime.Now.ToString();
        Boolean fillertempvalue;
        DateTime picko;
        DateTime actd;
        string firstmedname, secondmedname, fillerendtemp;
        string yearstr, firstamount, secondamount;
        string actualdate, b;
        int sleep;
        int eyecont;
        int attent;
        double firstnumber = 0;
        double secondnumber = 0;
        int Endexists;
        int appet;
        int voice;
        int scor;
        int id;
        int filler;

        private void fillall()
        {

            First_Med_ChangeLab.Text += " ";
            First_Med_ChangeLab.Text += firstmedname;
            Second_Med_ChangeLab.Text += " ";
            Second_Med_ChangeLab.Text += secondmedname;
            Appetite_Combo.Items.Add("01 ");
            Appetite_Combo.Items.Add("02 ");
            Appetite_Combo.Items.Add("03 ");
            Appetite_Combo.Items.Add("04 ");
            Appetite_Combo.Items.Add("05");
            Appetite_Combo.Items.Add("06 ");
            Appetite_Combo.Items.Add("07 ");
            Appetite_Combo.Items.Add("08 ");
            Appetite_Combo.Items.Add("09 ");
            Appetite_Combo.Items.Add("10 ");
            Appetite_Combo.Items.Add("11 ");
            Appetite_Combo.Items.Add("12");
            Appetite_Combo.Items.Add("13");
            Appetite_Combo.Items.Add("14");
            Appetite_Combo.Items.Add("15");
            Appetite_Combo.Items.Add("16");
            Appetite_Combo.Items.Add("17 ");
            Appetite_Combo.Items.Add("18");
            Appetite_Combo.Items.Add("19");
            Appetite_Combo.Items.Add("20");


            Sleep_Combo.Items.Add("01 ");
            Sleep_Combo.Items.Add("02 ");
            Sleep_Combo.Items.Add("03 ");
            Sleep_Combo.Items.Add("04 ");
            Sleep_Combo.Items.Add("05 ");
            Sleep_Combo.Items.Add("06 ");
            Sleep_Combo.Items.Add("07 ");
            Sleep_Combo.Items.Add("08 ");
            Sleep_Combo.Items.Add("09 ");
            Sleep_Combo.Items.Add("10 ");
            Sleep_Combo.Items.Add("11 ");
            Sleep_Combo.Items.Add("12 ");
            Sleep_Combo.Items.Add("13 ");
            Sleep_Combo.Items.Add("14 ");
            Sleep_Combo.Items.Add("15 ");
            Sleep_Combo.Items.Add("16 ");
            Sleep_Combo.Items.Add("17 ");
            Sleep_Combo.Items.Add("18 ");
            Sleep_Combo.Items.Add("19 ");
            Sleep_Combo.Items.Add("20 ");

            Eye_Contact_Combo.Items.Add("01");
            Eye_Contact_Combo.Items.Add("02");
            Eye_Contact_Combo.Items.Add("03");
            Eye_Contact_Combo.Items.Add("04");
            Eye_Contact_Combo.Items.Add("05");
            Eye_Contact_Combo.Items.Add("06");

            Eye_Contact_Combo.Items.Add("07");
            Eye_Contact_Combo.Items.Add("08");
            Eye_Contact_Combo.Items.Add("09");
            Eye_Contact_Combo.Items.Add("10");
            Eye_Contact_Combo.Items.Add("11");
            Eye_Contact_Combo.Items.Add("12");
            Eye_Contact_Combo.Items.Add("13");
            Eye_Contact_Combo.Items.Add("14");
            Eye_Contact_Combo.Items.Add("15");
            Eye_Contact_Combo.Items.Add("16");
            Eye_Contact_Combo.Items.Add("17");
            Eye_Contact_Combo.Items.Add("18");
            Eye_Contact_Combo.Items.Add("19");
            Eye_Contact_Combo.Items.Add("20");

            Voice_Tone_Combo.Items.Add("01 ");
            Voice_Tone_Combo.Items.Add("02 ");
            Voice_Tone_Combo.Items.Add("03 ");
            Voice_Tone_Combo.Items.Add("04 ");
            Voice_Tone_Combo.Items.Add("05 ");
            Voice_Tone_Combo.Items.Add("06 ");
            Voice_Tone_Combo.Items.Add("07 ");
            Voice_Tone_Combo.Items.Add("08 ");
            Voice_Tone_Combo.Items.Add("09 ");
            Voice_Tone_Combo.Items.Add("10 ");
            Voice_Tone_Combo.Items.Add("11 ");
            Voice_Tone_Combo.Items.Add("12 ");
            Voice_Tone_Combo.Items.Add("13 ");
            Voice_Tone_Combo.Items.Add("14 ");
            Voice_Tone_Combo.Items.Add("15 ");
            Voice_Tone_Combo.Items.Add("16 ");
            Voice_Tone_Combo.Items.Add("17 ");
            Voice_Tone_Combo.Items.Add("18 ");
            Voice_Tone_Combo.Items.Add("19 ");
            Voice_Tone_Combo.Items.Add("20 ");


            Attention_Combo.Items.Add("01 ");
            Attention_Combo.Items.Add("02 ");
            Attention_Combo.Items.Add("03 ");
            Attention_Combo.Items.Add("04 ");
            Attention_Combo.Items.Add("05 ");
            Attention_Combo.Items.Add("06 ");
            Attention_Combo.Items.Add("07 ");
            Attention_Combo.Items.Add("08 ");
            Attention_Combo.Items.Add("09 ");
            Attention_Combo.Items.Add("10 ");
            Attention_Combo.Items.Add("11 ");
            Attention_Combo.Items.Add("12");
            Attention_Combo.Items.Add("13");
            Attention_Combo.Items.Add("14 ");
            Attention_Combo.Items.Add("15 ");
            Attention_Combo.Items.Add("16 ");
            Attention_Combo.Items.Add("17 ");
            Attention_Combo.Items.Add("18 ");
            Attention_Combo.Items.Add("19 ");
            Attention_Combo.Items.Add("20 ");
        }
        Boolean checkstart;
        SqlConnection adsescon3 = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public void sesnum()
        {
            MessageBox.Show(SesAdd_Patient_ID.Text);
            string checkzero = "SELECT COUNT (*) FROM Double_Session_Table WHERE Full_Combination='" + SessAdd_Medication.Text + "' AND ID=" + SesAdd_Patient_ID.Text + " AND End_Status IS NULL";
            adsescon3.Open();
            SqlCommand checko = new SqlCommand(checkzero, adsescon3);
            Endexists = (int)checko.ExecuteScalar();
            if (Endexists > 0)
            {
                checkstart = !true;
            }
            else
            {
                checkstart = true;
            }
            adsescon3.Close();
        }
        private void TodayCheck_CheckedChanged(object sender, EventArgs e)
        {
            Session_Day.Enabled = !(TodayCheck.CheckState == CheckState.Checked);
            Session_Month.Enabled = !(TodayCheck.CheckState == CheckState.Checked);
            Specific_Date_Check.Enabled = !(TodayCheck.CheckState == CheckState.Checked);
        }

        private void Specific_Date_Check_CheckStateChanged(object sender, EventArgs e)
        {
            TodayCheck.Enabled = !(Specific_Date_Check.CheckState == CheckState.Checked);
        }


        private void Register_Session_Button_Click(object sender, EventArgs e)
        {
            sesnum();
            picko = Convert.ToDateTime(ok.ToString());
            yearstr = picko.Year.ToString();
            if (Specific_Date_Check.Checked == true)
            {
                actualdate = yearstr + "-" + Session_Month.Text + "-" + Session_Day.Text;
                actd = Convert.ToDateTime(actualdate);
            }
            if (TodayCheck.Checked == true)
            {
                actd = DateTime.Now;
            }
            sleep = Convert.ToInt32(Sleep_Combo.Text.Substring(0, 2));
            appet = Convert.ToInt32(Appetite_Combo.Text.Substring(0, 2));
            scor = Convert.ToInt32(Score_Box.Text);
            eyecont = Convert.ToInt32(Eye_Contact_Combo.Text.Substring(0, 2));
            voice = Convert.ToInt32(Voice_Tone_Combo.Text.Substring(0, 2));
            attent = Convert.ToInt32(Attention_Combo.Text.Substring(0, 2));
            id = Convert.ToInt32(SesAdd_Patient_ID.Text);

            // START LOOKING FROM HERE
            //firstbox
            if (!(string.IsNullOrWhiteSpace(Change_First_Amount_Box.Text)))
            {
                if (int.TryParse(Change_First_Amount_Box.Text, out filler) == false)
                {
                    MessageBox.Show("Enter a proper amount");
                }
                else
                {
                    firstamount = Change_First_Amount_Box.Text;
                    firstnumber = Convert.ToDouble(firstamount);
                    //if (Endexists < 0)
                    //{
                        string update = "UPDATE Double_Medication_Historty SET First_Used_Amount=" + firstnumber + " WHERE (ID=@ID AND End_Date IS NULL)";
                        adsescon.Open();

                        SqlCommand adeseso = new SqlCommand(update, adsescon);
                        adeseso.Parameters.AddWithValue("ID", id);
                        

                        adeseso.ExecuteNonQuery();

                        adsescon.Close();

                        

                    //}
                    //else
                    //{


                    //}
                }
            }
            if (!(string.IsNullOrWhiteSpace(Change_Second_Amount_Box.Text)))
            {
                if (int.TryParse(Change_Second_Amount_Box.Text, out filler) == false)
                {
                    MessageBox.Show("Enter a proper amount");
                }
                else
                {
                    secondamount = Change_Second_Amount_Box.Text;
                    secondnumber = Convert.ToDouble(secondamount);
                    string update = "UPDATE Double_Medication_Historty SET Second_Used_Amount=" + secondnumber + " WHERE (ID=@ID AND End_Date IS NULL)";
                    adsescon.Open();

                    SqlCommand adeseso = new SqlCommand(update, adsescon);
                    adeseso.Parameters.AddWithValue("ID", id);


                    adeseso.ExecuteNonQuery();

                    adsescon.Close();
                }
            }
            string adse = "INSERT INTO Double_Session_Table (First_Medication, Second_Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone, Attention_Span, ID, Score, Notes, Session_Number, First_Used_Amount, Full_Combination, Second_Used_Amount ) ";
            adse += "VALUES (@First_Medication, @Second_Medication, @Date, @Quality_of_Sleep, @Appetite, @Eye_Contact, @Voice_Tone, @Attention_Span, @ID, @Score, @Notes, @Session_Number, @First_Used_Amount, @Full_Combination, @Second_Used_Amount )";
            adsescon.Open();
            SqlCommand adsescomm = new SqlCommand(adse, adsescon);
            adsescomm.Parameters.AddWithValue("First_Medication", firstmedname);
            adsescomm.Parameters.AddWithValue("Second_Medication", secondmedname);
            adsescomm.Parameters.AddWithValue("Date", actd);
            //adsescomm.Parameters.AddWithValue("First_Medication", firstmedname);
            adsescomm.Parameters.AddWithValue("Quality_of_Sleep", sleep);
            adsescomm.Parameters.AddWithValue("Appetite", appet);
            adsescomm.Parameters.AddWithValue("Eye_Contact", eyecont);
            adsescomm.Parameters.AddWithValue("Voice_Tone", voice);
            adsescomm.Parameters.AddWithValue("Attention_Span", attent);
            adsescomm.Parameters.AddWithValue("ID", id);
            adsescomm.Parameters.AddWithValue("Score", scor);
            adsescomm.Parameters.AddWithValue("Notes", Testr.Text);
            if (Endexists < 0)
            {
                adsescomm.Parameters.AddWithValue("Session_Number", 0);
            }
            else
            {
                adsescomm.Parameters.AddWithValue("Session_Number", Endexists + 1);
            }
            adsescomm.Parameters.AddWithValue("First_Used_Amount", firstnumber);
            adsescomm.Parameters.AddWithValue("Full_Combination", SessAdd_Medication.Text);
            adsescomm.Parameters.AddWithValue("Second_Used_Amount", secondnumber);
            adsescomm.ExecuteNonQuery();
            adsescon.Close();
            MessageBox.Show("Session Added");
            Double_Session_List jog = new Double_Session_List(SesAdd_Patient_ID.Text, SesAdd_Patient_Name.Text, SessAdd_Medication.Text, fillertempvalue, b, fillerendtemp, firstnumber.ToString(), secondnumber.ToString(), firstmedname, secondmedname);
            this.Hide();
            jog.Show();
            
        }
    }
}
