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
    public partial class Patient_Double_Med_History : Form
    {
        public Patient_Double_Med_History(string id, string name)
        {
            InitializeComponent();
            Patient_ID_Lab.Text = id;
            Patient_Name_Lab.Text = name;
            medfill();
        }

        public static SqlConnection medcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection Endcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");

        int ho;
        int selectid;
        object go;
        object go2;
        string meme;//id
        string meme2;//name
        string meme3;//medication name
        string begindate, enddate;
        object gu;
        int getid;
        object goget, go3, go4, go5;
        string gogetprox;
        int selectint;
        string ok = DateTime.Now.ToString();
        object naem;
        DateTime picko, substitute;
        string conv;
        public void medfill()
        {
            medcon.Open();
            ho = Convert.ToInt32(Patient_ID_Lab.Text);
            string fe = "SELECT ID, Name, First_Medication, Second_Medication, Starting_Date, End_Date, First_Used_Amount, Second_Used_Amount FROM Double_Medication_Historty WHERE ID= " + ho + " ORDER BY Starting_Date";
            SqlCommand medcom = new SqlCommand(fe, medcon);
            DataSet dte = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(fe, medcon);
            adp.Fill(dte, "Patients_Med");
            Med_History_Grid.DataSource = dte.Tables[0];
            medcon.Close();
            //End_Medication_But.Enabled = false;
            Med_History_Grid.Columns[0].Width = 75;
            Med_History_Grid.Columns[8].Width = 75;
            Med_History_Grid.Columns[7].Width = 75;
            Med_History_Grid.Columns[1].Width = 75;
            Med_History_Grid.Columns[3].HeaderText = "First Medication";
            Med_History_Grid.Columns[4].HeaderText = "Second Medication";
            Med_History_Grid.Columns[5].HeaderText = "Starting Date";
            Med_History_Grid.Columns[6].HeaderText = "End Date";
            Med_History_Grid.Columns[7].HeaderText = "First Medication Amount (mg)";
            Med_History_Grid.Columns[8].HeaderText = "Second Medication Amount (mg)";

        }

        private void Add_New_MedCombo_Click(object sender, EventArgs e)
        {
            string checksing = "SELECT COUNT (*) FROM Patient_Medication_Historty WHERE ID=" + Patient_ID_Lab.Text + " AND End_Date IS NULL";
            medcon.Open();
            SqlCommand checksingle = new SqlCommand(checksing, medcon);
            int singlecount = (int)checksingle.ExecuteScalar();
            medcon.Close();
            if (singlecount > 0)
            {
                MessageBox.Show("The patient is using a solo medication");
            }
            else
            {
                if (Med_History_Grid.Rows.Count == 1 && Med_History_Grid.Rows[0].Cells[3].Value == null)
                {
                    Add_Double_Medication joh = new Add_Double_Medication(Patient_ID_Lab.Text, Patient_Name_Lab.Text);
                    this.Hide();
                    joh.Show();
                }
                else
                {
                    int g = 0;
                    for (int o = 0; o < Med_History_Grid.Rows.Count - 1; o++)
                    {
                        if (Med_History_Grid.Rows[o].Cells[6].Value == null || Med_History_Grid.Rows[o].Cells[6].Value == DBNull.Value || String.IsNullOrWhiteSpace(Med_History_Grid.Rows[o].Cells[6].Value.ToString()))
                        {
                            g++;
                        }
                    }
                    if (g >= 1)
                    {
                        MessageBox.Show("Patients can't have more than a single active medication ");
                    }
                    else
                    {
                        DataGridViewRow row2 = new DataGridViewRow();
                        row2 = Med_History_Grid.Rows[1];
                        naem = row2.Cells[2];
                        conv = Convert.ToString(naem);
                        Add_Double_Medication joh = new Add_Double_Medication(Patient_ID_Lab.Text, Patient_Name_Lab.Text);
                        this.Hide();
                        joh.Show();
                    }
                }
            }
        }
        string endstr, endstat;
        public void endactivemed()
        {
            for (int o = 0; o < Med_History_Grid.Rows.Count - 1; o++)
            {
                if(Med_History_Grid.Rows[o].Cells[6].Value == null || Med_History_Grid.Rows[o].Cells[6].Value == DBNull.Value || String.IsNullOrWhiteSpace(Med_History_Grid.Rows[o].Cells[6].Value.ToString()))
                {
                    DateTime oko = DateTime.Now;
                    object kpo = Med_History_Grid.Rows[o].Cells[5].Value;
                    DateTime starting = Convert.ToDateTime(kpo);
                    if ((oko - starting).TotalDays >= 30)
                    {
                        //FILLED WITH THE CODE THAT IS SIMILAR TO ITS SINGULAR MEDICINE VARIATION, WHICH ENDS THE MED IF THE DURATION OF THE ACTIVE MEDICINE WAS LONGER THAN 30 DAYS
                        //THIS PLACE IS LEFT INTENTIONALLY BLANK AS ITS ORIGINAL USE SEVERELY LIMITS THE DEMO
                    }
                }
            }
        }
        private void Patient_Double_Med_History_FormClosing(object sender, FormClosingEventArgs e)
        {
            Patients jok = new Patients(Patient_ID_Lab.Text);
            this.Hide();
            jok.Show();
        }

        string prox;
        Boolean ended;
        string final = DateTime.Now.ToString("yyyy/MM/dd");

        private void Med_History_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            selectint = e.RowIndex;
            DataGridViewRow row = new DataGridViewRow();
            row = Med_History_Grid.Rows[selectint];
            if (!(row.Cells[2].Value == DBNull.Value || row.Cells[2].Value == null || String.IsNullOrWhiteSpace(row.Cells[2].Value.ToString())))
            {
                End_Medication_But.Enabled = true;
                if (e.ColumnIndex == 0)
                {


                    go = row.Cells[1].Value;
                    getid = Convert.ToInt32(go);
                    goget = row.Cells[3].Value;
                    gogetprox = Convert.ToString(goget);
                    Boolean ended2;
                    if (row.Cells[6].Value == null || row.Cells[6].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()))
                    {
                        ended2 = false;

                        //enddate = DateTime.Now.ToString();
                        enddate = "";
                    }
                    else
                    {
                        ended2 = true;
                        go4 = row.Cells[6].Value;//end date
                        enddate = Convert.ToString(go4);

                    }
                    object go6 = row.Cells[4].Value;
                    ended = ended2;
                    go = row.Cells[3].Value;
                    go2 = row.Cells[8].Value;
                    gu = row.Cells[7].Value;
                    go3 = row.Cells[5].Value;//begin date
                    go5 = row.Cells[6].Value;
                    begindate = Convert.ToString(go3);
                    string kek = go5.ToString();//enddate
                    string med2 = go6.ToString();//medname 2
                    meme3 = Convert.ToString(gu);//first amount
                    meme = Convert.ToString(go);//medname 1
                    meme2 = Convert.ToString(go2);//secondamount
                    Double_Session_List joto = new Double_Session_List(Patient_ID_Lab.Text, Patient_Name_Lab.Text, meme + "-" + med2, ended2, begindate, kek, meme3, meme2, meme, med2);
                    this.Hide();
                    joto.Show();



                }



            }
        }

        DateTime reals;
        private void End_Medication_But_Click(object sender, EventArgs e)
        {
            for (int o = 0; o < Med_History_Grid.Rows.Count - 1; o++)
            {
                if (Med_History_Grid.Rows[o].Cells[6].Value == null || Med_History_Grid.Rows[o].Cells[6].Value == DBNull.Value || String.IsNullOrWhiteSpace(Med_History_Grid.Rows[o].Cells[6].Value.ToString()))
                {
                    picko = Convert.ToDateTime(ok.ToString());
                    prox = picko.Year.ToString() + "-" + picko.Month.ToString() + "-" + picko.Day.ToString();
                    reals = Convert.ToDateTime(prox);
                    object go6 = Med_History_Grid.Rows[o].Cells[4].Value;
                    object go7 = Med_History_Grid.Rows[o].Cells[9].Value;
                    go = Med_History_Grid.Rows[o].Cells[3].Value;
                    go2 = Med_History_Grid.Rows[o].Cells[8].Value;
                    gu = Med_History_Grid.Rows[o].Cells[7].Value;
                    go3 = Med_History_Grid.Rows[o].Cells[5].Value;//begin date
                    go5 = Med_History_Grid.Rows[o].Cells[6].Value;
                    begindate = Convert.ToString(go3);
                    string kek = go5.ToString();//enddate
                    string med2 = go6.ToString();//medname 2
                    string gogetproxe = go7.ToString();
                    meme3 = Convert.ToString(gu);//first amount
                    meme = Convert.ToString(go);//medname 1
                    meme2 = Convert.ToString(go2);//secondamount
                    DialogResult sure = MessageBox.Show("Are you sure you want to end the medication?", "End Medication", MessageBoxButtons.YesNo);
                    if (sure == DialogResult.Yes)
                    {
                        Endcon.Open();
                        endstat = "UPDATE Double_Session_Table SET End_Status=1 WHERE ID=@ID AND Full_Combination=@Full_Combination AND Date<'" + prox + "'";
                        endstr = "UPDATE Double_Medication_Historty SET End_Date='" + prox + "'" + "WHERE (ID=@ID AND Full_Combination=@Full_Combination AND End_Date IS NULL)";
                        MessageBox.Show(getid.ToString());
                        SqlCommand endcomm = new SqlCommand(endstr, Endcon);
                        SqlCommand endsta = new SqlCommand(endstat, Endcon);
                        endcomm.Parameters.AddWithValue("@ID", Patient_ID_Lab.Text);
                        endcomm.Parameters.AddWithValue("@Full_Combination", gogetproxe);
                        endsta.Parameters.AddWithValue("@ID", Patient_ID_Lab.Text);
                        endsta.Parameters.AddWithValue("@Full_Combination", gogetproxe);
                        endsta.ExecuteNonQuery();
                        endcomm.ExecuteNonQuery();
                        medfill();
                        Endcon.Close();

                    }
                }
                
            }
        }
    }
}
