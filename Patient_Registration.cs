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
    public partial class Patient_Registration : Form
    {
        public Patient_Registration(string usid)
        {
            InitializeComponent();
            User_ID_Register.Text = usid;
        }
        List<int> b = new List<int>();
        public static SqlConnection regcon = new SqlConnection ("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string fullname;
        public static SqlConnection regcon2 = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        int ag;
        int id;
        int jo;
        long phonnum;
        string datd;
        DateTime da;
        private void Register_Patient_Button_Click(object sender, EventArgs e)
        {
            if (PatientReg_Name_Text != null && PatientReg_Surname_Text != null && PatientReg_Age_Text!=null)
            {
                
                fullname = PatientReg_Name_Text.Text+ " "+PatientReg_Surname_Text.Text;
                ag = Convert.ToInt16(PatientReg_Age_Text.Text);
                phonnum = Convert.ToInt64(PatientReg_Phone_Text.Text);
                const string command = "INSERT INTO Patient_List(Name, Age, Occupation, Phone_Number, ID, Responsible_User_ID)";
                string q = command;
                q += "VALUES(@Name, @Age, @Occupation, @Phone_Number, @ID, @Responsible_User_ID) ";
                string ho = "INSERT INTO Patient_Medication_Historty(ID, Name, Medication, Starting_Date, Amount_Used)";
                ho += "VALUES (@ID, @Name, @Medication, @Starting_Date, @Amount_Used)";
                regcon.Open();
                regcon2.Open();
                //datd = Year_Reg_Box.Text + "-" + Month_Reg_Box.Text + "-" + Day_Reg_Box.Text;
                //int amoun = Convert.ToInt32(Used_AmountBox.Text);
                da = Convert.ToDateTime(datd);
                jo = Convert.ToInt32(User_ID_Register.Text);
                SqlCommand comm = new SqlCommand(q, regcon);
                
                comm.Parameters.AddWithValue("Name", fullname);
                comm.Parameters.AddWithValue("Age", ag);
                comm.Parameters.AddWithValue("Occupation", PatientReg_Occupation_Text.Text);
                comm.Parameters.AddWithValue("Phone_Number", phonnum);
                dupe();
                comm.Parameters.AddWithValue("ID", d);
                comm.Parameters.AddWithValue("Responsible_User_ID", jo);
                
                comm.ExecuteNonQuery();
                
                MessageBox.Show("Patient Has Been Registered");
                PatientReg_Name_Text.Text = "";
                PatientReg_Occupation_Text.Text = "";
                PatientReg_Phone_Text.Text = "";
                PatientReg_Age_Text.Text = "";
                PatientReg_Surname_Text.Text = "";
            }
            else
            {
                MessageBox.Show("Please Fill the Areas Properly");
            }
        }

        private void Patient_Registration_FormClosing(object sender, FormClosingEventArgs e)
        {
            Choice cg = new Choice(User_ID_Register.Text);
            cg.Show();
        }
        int a;
        int d;
        int c = 0;
        public static SqlConnection dupcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public void dupe()
        {
            dupcon.Open();
            string ko = "SELECT COUNT(*) FROM Patient_List WHERE(ID = @ID)";
            SqlCommand ju = new SqlCommand(ko, dupcon);
            Random r = new Random();
            a = r.Next(10000, 99999);
            d = a;
            ju.Parameters.AddWithValue("@ID", a);
            int pass = (int)ju.ExecuteScalar();
            if (pass > 0)
            {
                b.Insert(c, d);
            }
            if (b.Contains(a))
                dupe();
            else
            {

                b.Insert(c, d);
                c++;
            }
        }
    }
}
