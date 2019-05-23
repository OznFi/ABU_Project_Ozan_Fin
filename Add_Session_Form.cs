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
    public partial class Add_Session_Form : Form
    {
        public Add_Session_Form(string patsesid, string patsesnam, string medses, Boolean forma, string fulltext, string bego, string endo, string inamount)
        {
            InitializeComponent();
            SesAdd_Patient_ID.Text = patsesid;
            SesAdd_Patient_Name.Text = patsesnam;
            SessAdd_Medication.Text = medses;
            alltext = fulltext;
            fillall();
            idga = forma;
            filllist();
            b = bego;
            d = endo;
            amount = inamount;
        }
        string alltext, b, d;
        Boolean idga;
        SqlConnection adsescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string actualdate;
        DateTime actd;
        int sleep;
        int eyecont;
        int attent;
        int appet;
        int voice;
        int id;
        bool checkstart;
        int pick2;
        string yearstr;
        string ok = DateTime.Now.ToString();
        DateTime picko;
        private void fillall()
        {
            Appetite_Combo.Items.Add("01 Barely Eating");
            Appetite_Combo.Items.Add("02 ");
            Appetite_Combo.Items.Add("03 ");
            Appetite_Combo.Items.Add("04 ");
            Appetite_Combo.Items.Add("05 Missing Meals");
            Appetite_Combo.Items.Add("06 ");
            Appetite_Combo.Items.Add("07 ");
            Appetite_Combo.Items.Add("08 ");
            Appetite_Combo.Items.Add("09 ");
            Appetite_Combo.Items.Add("10 Decent ");
            Appetite_Combo.Items.Add("11 ");
            Appetite_Combo.Items.Add("12");
            Appetite_Combo.Items.Add("13");
            Appetite_Combo.Items.Add("14");
            Appetite_Combo.Items.Add("15 Full Meals");
            Appetite_Combo.Items.Add("16");
            Appetite_Combo.Items.Add("17 ");
            Appetite_Combo.Items.Add("18");
            Appetite_Combo.Items.Add("19");
            Appetite_Combo.Items.Add("20 Excessive Eating");
            

            Sleep_Combo.Items.Add("01 Barely Sleeping");
            Sleep_Combo.Items.Add("02 ");
            Sleep_Combo.Items.Add("03 ");
            Sleep_Combo.Items.Add("04 ");
            Sleep_Combo.Items.Add("05 Distrupted ");
            Sleep_Combo.Items.Add("06 ");
            Sleep_Combo.Items.Add("07 ");
            Sleep_Combo.Items.Add("08 ");
            Sleep_Combo.Items.Add("09 ");
            Sleep_Combo.Items.Add("10 Undistrupted");
            Sleep_Combo.Items.Add("11 ");
            Sleep_Combo.Items.Add("12 ");
            Sleep_Combo.Items.Add("13 ");
            Sleep_Combo.Items.Add("14 ");
            Sleep_Combo.Items.Add("15 Lengthy");
            Sleep_Combo.Items.Add("16 ");
            Sleep_Combo.Items.Add("17 ");
            Sleep_Combo.Items.Add("18 ");
            Sleep_Combo.Items.Add("19 ");
            Sleep_Combo.Items.Add("20 More than 10 hours");

            Eye_Contact_Combo.Items.Add("01 None");
            Eye_Contact_Combo.Items.Add("02");
            Eye_Contact_Combo.Items.Add("03");
            Eye_Contact_Combo.Items.Add("04");
            Eye_Contact_Combo.Items.Add("05 Brief Moments");
            Eye_Contact_Combo.Items.Add("06");
            
            Eye_Contact_Combo.Items.Add("07");
            Eye_Contact_Combo.Items.Add("08");
            Eye_Contact_Combo.Items.Add("09");
            Eye_Contact_Combo.Items.Add("10 Decent");
            Eye_Contact_Combo.Items.Add("11");
            Eye_Contact_Combo.Items.Add("12");
            Eye_Contact_Combo.Items.Add("13");
            Eye_Contact_Combo.Items.Add("14");
            Eye_Contact_Combo.Items.Add("15 Frequent");
            Eye_Contact_Combo.Items.Add("16");
            Eye_Contact_Combo.Items.Add("17");
            Eye_Contact_Combo.Items.Add("18");
            Eye_Contact_Combo.Items.Add("19");
            Eye_Contact_Combo.Items.Add("20 Constant");

            Voice_Tone_Combo.Items.Add("01 Barely any talk");
            Voice_Tone_Combo.Items.Add("02 ");
            Voice_Tone_Combo.Items.Add("03 ");
            Voice_Tone_Combo.Items.Add("04 ");
            Voice_Tone_Combo.Items.Add("05 Inconsistent");
            Voice_Tone_Combo.Items.Add("06 ");
            Voice_Tone_Combo.Items.Add("07 ");
            Voice_Tone_Combo.Items.Add("08 ");
            Voice_Tone_Combo.Items.Add("09 ");
            Voice_Tone_Combo.Items.Add("10 Decent");
            Voice_Tone_Combo.Items.Add("11 ");
            Voice_Tone_Combo.Items.Add("12 ");
            Voice_Tone_Combo.Items.Add("13 ");
            Voice_Tone_Combo.Items.Add("14 ");
            Voice_Tone_Combo.Items.Add("15 Proper ");
            Voice_Tone_Combo.Items.Add("16 ");
            Voice_Tone_Combo.Items.Add("17 ");
            Voice_Tone_Combo.Items.Add("18 ");
            Voice_Tone_Combo.Items.Add("19 ");
            Voice_Tone_Combo.Items.Add("20 Great");


            Attention_Combo.Items.Add("01 None");
            Attention_Combo.Items.Add("02 ");
            Attention_Combo.Items.Add("03 ");
            Attention_Combo.Items.Add("04 ");
            Attention_Combo.Items.Add("05 Easily Distracted");
            Attention_Combo.Items.Add("06 ");
            Attention_Combo.Items.Add("07 ");
            Attention_Combo.Items.Add("08 ");
            Attention_Combo.Items.Add("09 ");
            Attention_Combo.Items.Add("10 Decent");
            Attention_Combo.Items.Add("11 ");
            Attention_Combo.Items.Add("12");
            Attention_Combo.Items.Add("13");
            Attention_Combo.Items.Add("14 ");
            Attention_Combo.Items.Add("15 Focused");
            Attention_Combo.Items.Add("16 ");
            Attention_Combo.Items.Add("17 ");
            Attention_Combo.Items.Add("18 ");
            Attention_Combo.Items.Add("19 ");
            Attention_Combo.Items.Add("20 Completely Focused");

            Testr.Text = alltext;
        }
        int sesnumber2;
        int Endexists;
        public List<int> chrs;
        int scor;
        int filler, id2;
        double numberamount = 0;
        string amount;
        public object nums;

        private void Register_Session_Button_Click(object sender, EventArgs e)
        {
            sesnum();
            if (int.TryParse(Score_Box.Text, out filler) == false)
            {
                MessageBox.Show("Please enter a valid number");
            }
            else
            {
                scor = Convert.ToInt32(Score_Box.Text);
                if (scor < 0 || scor > 100)
                {
                    MessageBox.Show("Please enter a number between 0 and 100");
                }
                else
                {
                    
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
                        
                        eyecont = Convert.ToInt32(Eye_Contact_Combo.Text.Substring(0, 2));
                        voice = Convert.ToInt32(Voice_Tone_Combo.Text.Substring(0, 2));
                        attent = Convert.ToInt32(Attention_Combo.Text.Substring(0, 2));
                        id = Convert.ToInt32(SesAdd_Patient_ID.Text);
                    if (!(string.IsNullOrWhiteSpace(Change_Amount_Box.Text)))
                    {
                        if (int.TryParse(Change_Amount_Box.Text, out filler) == false)
                        {
                            MessageBox.Show("Enter a proper amount");
                        }
                        else
                        {
                            amount = Change_Amount_Box.Text;
                            numberamount = Convert.ToDouble(amount);
                            if (Endexists < 0)
                            {
                                string update = "UPDATE Patient_Medication_Historty SET Amount_Used=" + numberamount + " WHERE (ID=@ID AND Medication=@Medication AND Starting_Date=@Starting_Date AND End_Date IS NULL)";
                                adsescon.Open();
                                string adse = "INSERT INTO Session_Table (Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone_Consistency, Attention_Span, ID, Score, Notes, Session_Number ) ";
                                adse += "VALUES (@Medication, @Date, @Quality_of_Sleep, @Appetite, @Eye_Contact, @Voice_Tone_Consistency, @Attention_Span, @ID, @Score, @Notes, @Session_Number)";
                                SqlCommand adsescomm = new SqlCommand(adse, adsescon);
                                SqlCommand adeseso = new SqlCommand(update, adsescon);
                                adeseso.Parameters.AddWithValue("ID", id);
                                adeseso.Parameters.AddWithValue("Medication", SessAdd_Medication.Text);
                                adeseso.Parameters.AddWithValue("Starting_Date", Convert.ToDateTime(b));
                                adsescomm.Parameters.AddWithValue("Medication", SessAdd_Medication.Text);
                                adsescomm.Parameters.AddWithValue("Date", actd);
                                adsescomm.Parameters.AddWithValue("Quality_of_Sleep", sleep);
                                adsescomm.Parameters.AddWithValue("Appetite", appet);
                                adsescomm.Parameters.AddWithValue("Eye_Contact", eyecont);
                                adsescomm.Parameters.AddWithValue("Voice_Tone_Consistency", voice);
                                adsescomm.Parameters.AddWithValue("Attention_Span", attent);
                                adsescomm.Parameters.AddWithValue("ID", id);
                                adsescomm.Parameters.AddWithValue("Score", scor);
                                adsescomm.Parameters.AddWithValue("Notes", Testr.Text);
                                adsescomm.Parameters.AddWithValue("Session_Number", 0);
                                adsescomm.Parameters.AddWithValue("Used_Amount", numberamount);
                                adeseso.ExecuteNonQuery();
                                adsescomm.ExecuteNonQuery();
                                adsescon.Close();

                                MessageBox.Show("Session Added");
                                this.Hide();
                                Session_List hj = new Session_List(SesAdd_Patient_ID.Text, SesAdd_Patient_Name.Text, SessAdd_Medication.Text, idga, b, d, amount);
                                hj.Show();
                            }
                            else
                            {

                                int sesnumber;
                                adsescon3.Open();
                                string getsesnum = "SELECT *  FROM Session_Table WHERE (Medication='" + SessAdd_Medication.Text + "' AND ID=" + SesAdd_Patient_ID.Text + " AND End_Status IS NULL)";
                                string update = "UPDATE Patient_Medication_Historty SET Amount_Used=" + numberamount + " WHERE (ID=@ID AND Medication=@Medication AND Starting_Date=@Starting_Date AND End_Date IS NULL)";
                                sesnumber = Convert.ToInt32(nums);
                                string kek = Convert.ToString(sesnumber);
                                adsescon.Open();
                                string adse = "INSERT INTO Session_Table (Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone_Consistency, Attention_Span, ID, Score, Notes, Session_Number, Used_Amount ) ";
                                adse += "VALUES (@Medication, @Date, @Quality_of_Sleep, @Appetite, @Eye_Contact, @Voice_Tone_Consistency, @Attention_Span, @ID, @Score, @Notes, @Session_Number, @Used_Amount)";
                                SqlCommand adsescomm = new SqlCommand(adse, adsescon);
                                adsescomm.Parameters.AddWithValue("Medication", SessAdd_Medication.Text);
                                SqlCommand adeseso = new SqlCommand(update, adsescon);
                                adeseso.Parameters.AddWithValue("ID", id);
                                adeseso.Parameters.AddWithValue("Medication", SessAdd_Medication.Text);
                                adeseso.Parameters.AddWithValue("Starting_Date", Convert.ToDateTime(b));
                                adsescomm.Parameters.AddWithValue("Date", actd);
                                adsescomm.Parameters.AddWithValue("Quality_of_Sleep", sleep);
                                adsescomm.Parameters.AddWithValue("Appetite", appet);
                                adsescomm.Parameters.AddWithValue("Eye_Contact", eyecont);
                                adsescomm.Parameters.AddWithValue("Voice_Tone_Consistency", voice);
                                adsescomm.Parameters.AddWithValue("Attention_Span", attent);
                                adsescomm.Parameters.AddWithValue("ID", id);
                                adsescomm.Parameters.AddWithValue("Score", scor);
                                adsescomm.Parameters.AddWithValue("Notes", Testr.Text);
                                adsescomm.Parameters.AddWithValue("Session_Number", Endexists + 1);
                                adsescomm.Parameters.AddWithValue("Used_Amount", numberamount);
                                adeseso.ExecuteNonQuery();
                                adsescomm.ExecuteNonQuery();
                                adsescon.Close();
                                adsescon3.Close();
                                this.Hide();
                                Session_List hj = new Session_List(SesAdd_Patient_ID.Text, SesAdd_Patient_Name.Text, SessAdd_Medication.Text, idga, b, d, amount);
                                hj.Show();
                            }

                        }
                    }
                    else
                    {
                        numberamount = Convert.ToDouble(amount);
                        if (Endexists < 0)
                        {
                            adsescon.Open();
                            string adse = "INSERT INTO Session_Table (Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone_Consistency, Attention_Span, ID, Score, Notes, Session_Number ) ";
                            adse += "VALUES (@Medication, @Date, @Quality_of_Sleep, @Appetite, @Eye_Contact, @Voice_Tone_Consistency, @Attention_Span, @ID, @Score, @Notes, @Session_Number)";
                            SqlCommand adsescomm = new SqlCommand(adse, adsescon);
                            adsescomm.Parameters.AddWithValue("Medication", SessAdd_Medication.Text);
                            adsescomm.Parameters.AddWithValue("Date", actd);
                            adsescomm.Parameters.AddWithValue("Quality_of_Sleep", sleep);
                            adsescomm.Parameters.AddWithValue("Appetite", appet);
                            adsescomm.Parameters.AddWithValue("Eye_Contact", eyecont);
                            adsescomm.Parameters.AddWithValue("Voice_Tone_Consistency", voice);
                            adsescomm.Parameters.AddWithValue("Attention_Span", attent);
                            adsescomm.Parameters.AddWithValue("ID", id);
                            adsescomm.Parameters.AddWithValue("Score", scor);
                            adsescomm.Parameters.AddWithValue("Notes", Testr.Text);
                            adsescomm.Parameters.AddWithValue("Session_Number", 0);
                            adsescomm.Parameters.AddWithValue("Used_Amount", numberamount);
                            adsescomm.ExecuteNonQuery();
                            adsescon.Close();

                            MessageBox.Show("Session Added");
                            this.Hide();
                            Session_List hj = new Session_List(SesAdd_Patient_ID.Text, SesAdd_Patient_Name.Text, SessAdd_Medication.Text, idga, b, d, amount);
                            hj.Show();
                        }
                        else
                        {

                            int sesnumber;
                            adsescon3.Open();
                            string getsesnum = "SELECT *  FROM Session_Table WHERE (Medication='" + SessAdd_Medication.Text + "' AND ID=" + SesAdd_Patient_ID.Text + " AND End_Status IS NULL)";
                            
                            sesnumber = Convert.ToInt32(nums);
                            string kek = Convert.ToString(sesnumber);
                            adsescon.Open();
                            string adse = "INSERT INTO Session_Table (Medication, Date, Quality_of_Sleep, Appetite, Eye_Contact, Voice_Tone_Consistency, Attention_Span, ID, Score, Notes, Session_Number, Used_Amount ) ";
                            adse += "VALUES (@Medication, @Date, @Quality_of_Sleep, @Appetite, @Eye_Contact, @Voice_Tone_Consistency, @Attention_Span, @ID, @Score, @Notes, @Session_Number, @Used_Amount)";
                            SqlCommand adsescomm = new SqlCommand(adse, adsescon);
                            adsescomm.Parameters.AddWithValue("Medication", SessAdd_Medication.Text);
                            adsescomm.Parameters.AddWithValue("Date", actd);
                            adsescomm.Parameters.AddWithValue("Quality_of_Sleep", sleep);
                            adsescomm.Parameters.AddWithValue("Appetite", appet);
                            adsescomm.Parameters.AddWithValue("Eye_Contact", eyecont);
                            adsescomm.Parameters.AddWithValue("Voice_Tone_Consistency", voice);
                            adsescomm.Parameters.AddWithValue("Attention_Span", attent);
                            adsescomm.Parameters.AddWithValue("ID", id);
                            adsescomm.Parameters.AddWithValue("Score", scor);
                            adsescomm.Parameters.AddWithValue("Notes", Testr.Text);
                            adsescomm.Parameters.AddWithValue("Session_Number", Endexists + 1);
                            adsescomm.Parameters.AddWithValue("Used_Amount", numberamount);
                            adsescomm.ExecuteNonQuery();
                            adsescon.Close();
                            adsescon3.Close();
                            this.Hide();
                            Session_List hj = new Session_List(SesAdd_Patient_ID.Text, SesAdd_Patient_Name.Text, SessAdd_Medication.Text, idga, b, d, amount);
                            hj.Show();
                        }
                    }
                }
            }
        }
        string[] medlist;
        private void TodayCheck_CheckedChanged(object sender, EventArgs e)
        {
            Session_Day.Enabled = !(TodayCheck.CheckState == CheckState.Checked);
            Session_Month.Enabled= !(TodayCheck.CheckState == CheckState.Checked); 
            Specific_Date_Check.Enabled= !(TodayCheck.CheckState == CheckState.Checked);
        }

        private void Specific_Date_Check_CheckStateChanged(object sender, EventArgs e)
        {
            TodayCheck.Enabled = !(Specific_Date_Check.CheckState == CheckState.Checked);
        }

        private void Add_Notes_Click(object sender, EventArgs e)
        {
            Add_Session_Notes notr = new Add_Session_Notes(SesAdd_Patient_ID.Text, SesAdd_Patient_Name.Text, SessAdd_Medication.Text, idga, b, d);
            notr.Show();
        }
        SqlConnection adsescon2 = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public void filllist()
        {
            string reds = "SELECT * FROM Patient_Medication_Historty WHERE ID=@ID";
            id2 = Convert.ToInt32(SesAdd_Patient_ID.Text);
            adsescon2.Open();
            SqlCommand redcom = new SqlCommand(reds, adsescon2);
            redcom.Parameters.AddWithValue("@ID", id2);
            SqlDataReader medread = redcom.ExecuteReader();
            int i = 0;
            var golist = new List<string>();
            while (medread.Read())
            {
                var check = medread["End_Date"];
                
                if (check == DBNull.Value)
                {
                    golist.Add(medread.GetString(2));
                }
            }
            medlist = golist.ToArray();
        }
       
        SqlConnection adsescon3 = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public void sesnum()
        {
            MessageBox.Show(SesAdd_Patient_ID.Text);
            string checkzero = "SELECT COUNT (*) FROM Session_Table WHERE Medication='"+SessAdd_Medication.Text+"' AND ID="+SesAdd_Patient_ID.Text+" AND End_Status IS NULL";
            adsescon3.Open();
            SqlCommand checko = new SqlCommand(checkzero, adsescon3);
            Endexists = (int)checko.ExecuteScalar();
            if(Endexists> 0)
            {
                checkstart = !true;
            }
            else
            {
                checkstart = true;
            }
            adsescon3.Close();
        }
    }
}
