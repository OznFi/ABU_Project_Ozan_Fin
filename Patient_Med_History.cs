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
    public partial class Patient_Med_History : Form
    {
        public Patient_Med_History(string us, string nam)
        {
            InitializeComponent();
            PatName.Text = us;
            Patient_Name_Lab.Text = nam;
            medfill();
            fillthelist();

        }
        int selectid;
        object naem;
        object go;
        object go2;
        int selectint;
        string conv;
        object goget, go3, go4, go5;
        string gogetprox;
        string meme;//id
        string meme2;//name
        string meme3;//medication name
        string begindate, enddate;
        object gu;
        int getid;
        string ok = DateTime.Now.ToString();
        DateTime picko, substitute;
        public static SqlConnection medcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection Endcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            End_Medication_Button.Enabled = true;
            selectint = e.RowIndex;
            DataGridViewRow row = new DataGridViewRow();
            row = Med_History_Grid.Rows[selectint];
            if (e.ColumnIndex == 0)
            {

                Boolean ended2;
                if (row.Cells[5].Value == null || row.Cells[5].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[5].Value.ToString()))
                {
                    ended2 = false;

                    //enddate = DateTime.Now.ToString();
                    enddate = "";
                }
                else
                {
                    ended2 = true;
                    go4 = row.Cells[5].Value;//end date
                    enddate = Convert.ToString(go4);

                }
                if (row.Cells[1].Value == DBNull.Value || row.Cells[1].Value == null || String.IsNullOrWhiteSpace(row.Cells[1].Value.ToString()))
                {
                    MessageBox.Show("HERE");
                }
                else
                {


                    ended = ended2;
                    go = row.Cells[1].Value;
                    go2 = row.Cells[2].Value;
                    gu = row.Cells[3].Value;
                    go3 = row.Cells[4].Value;//begin date
                    go5 = row.Cells[6].Value;

                    begindate = Convert.ToString(go3);
                    string kek = go5.ToString();
                    meme3 = Convert.ToString(gu);
                    meme = Convert.ToString(go);
                    meme2 = Convert.ToString(go2);
                    this.Hide();
                    Session_List l = new Session_List(PatName.Text, Patient_Name_Lab.Text, meme3, ended, begindate, enddate, kek);
                    l.Show();




                    go = row.Cells[1].Value;
                    getid = Convert.ToInt32(go);
                    goget = row.Cells[3].Value;
                    gogetprox = Convert.ToString(goget);
                }
            }
        }
        int numofrow;
        Boolean ended;
        int ho;
        public void medfill()
        {
            medcon.Open();
            ho = Convert.ToInt32(PatName.Text);
            string fe = "SELECT ID, Name, Medication, Starting_Date, End_Date, Amount_Used FROM Patient_Medication_Historty WHERE ID= "+ho+" ORDER BY Starting_Date";
            SqlCommand medcom = new SqlCommand(fe, medcon);   
            DataSet dte = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter(fe, medcon);
            adp.Fill(dte, "Patients_Med");           
            Med_History_Grid.DataSource = dte.Tables[0];
            medcon.Close();
            //End_Medication_Button.Enabled = false;
            Med_History_Grid.Columns[0].Width = 75;
            Med_History_Grid.Columns[4].HeaderText = "Starting Date";
            Med_History_Grid.Columns[5].HeaderText = "End Date";
            Med_History_Grid.Columns[6].HeaderText = "Used Amount (mg)";
        }
       
        int i = 0;
        public void fillthelist()
        {
            //THIS PLACE IS LEFT MOSTLY AS COMMENTS AS ITS ORIGINAL USE SEVERELY LIMITS THE DEMO
            // THIS FUNCTIONALITY IS FOR ENDING MEDICINE THAT HAS SURPASSED A MONTH OF ACTIVE USE IT IS NOT  
            string[] clear0 =new string[Med_History_Grid.Rows.Count] ;
            for (int o=0; o<Med_History_Grid.Rows.Count-1; o++)
            {
                
                if (Med_History_Grid.Rows[o].Cells[5].Value ==null || Med_History_Grid.Rows[o].Cells[5].Value == DBNull.Value|| String.IsNullOrWhiteSpace(Med_History_Grid.Rows[o].Cells[5].Value.ToString()))
                {
                    object kpo = Med_History_Grid.Rows[o].Cells[4].Value;
                    DateTime starting = Convert.ToDateTime(kpo);
                    DateTime oko = DateTime.Now;
                    DateTime thirty = starting.AddDays(30);
                    if ((oko- starting).TotalDays>=30)
                    {

                       // go = Med_History_Grid.Rows[o].Cells[1].Value;
                        //go2 = Med_History_Grid.Rows[o].Cells[2].Value;
                        //gu = Med_History_Grid.Rows[o].Cells[3].Value;
                        //go3 = Med_History_Grid.Rows[o].Cells[4].Value;//begin date
                        //go5 = Med_History_Grid.Rows[o].Cells[6].Value;
                        //go = Med_History_Grid.Rows[o].Cells[1].Value;
                        //getid = Convert.ToInt32(go);
                        //goget = Med_History_Grid.Rows[o].Cells[3].Value;
                        //gogetprox = Convert.ToString(goget);
                        ////clear0[o] = Med_History_Grid.Rows[o].Cells[3].Value.ToString();
                        //Endcon.Open();
                        //endstat = "UPDATE Session_Table SET End_Status=1 WHERE ID=@ID AND Medication=@Medication AND Date<'" + thirty + "'";
                        //endstr = "UPDATE Patient_Medication_Historty SET End_Date='" + thirty + "'" + "WHERE (ID=@ID AND Medication=@Medication)";
                        //MessageBox.Show(getid.ToString());
                        //SqlCommand endcomm = new SqlCommand(endstr, Endcon);
                        //SqlCommand endsta = new SqlCommand(endstat, Endcon);
                        //endcomm.Parameters.AddWithValue("@ID", getid);
                        //endcomm.Parameters.AddWithValue("@Medication", gogetprox);
                        //endsta.Parameters.AddWithValue("@ID", getid);
                        //endsta.Parameters.AddWithValue("@Medication", gogetprox);
                        //endsta.ExecuteNonQuery();
                        //endcomm.ExecuteNonQuery();

                    }
                    medfill();
                    Endcon.Close();
                }
            }
            clear = clear0;
        }
        private void Patient_Med_History_Load(object sender, EventArgs e)
        {

        }
        public List<string> sendlist=new List<string>();
        string[] clear;
        private void Add_New_Medication_Click(object sender, EventArgs e)
        {
            string checkdoubmed= "SELECT COUNT (*) FROM Double_Medication_Historty WHERE ID='" + PatName.Text + "' AND End_Date IS NULL";
            medcon.Open();
            SqlCommand checkdoub = new SqlCommand(checkdoubmed, medcon);
            
            int doubcheck = (int)checkdoub.ExecuteScalar();
            medcon.Close();
            if (doubcheck > 0)
            {
                MessageBox.Show("Patient is using a double medication");
            }
            else
            {
                if (Med_History_Grid.Rows.Count == 1 && Med_History_Grid.Rows[0].Cells[3].Value == null)
                {

                    Extra_Medication joh = new Extra_Medication(Patient_Name_Lab.Text, PatName.Text, clear);
                    this.Hide();
                    joh.Show();
                }
                else
                {
                    int g = 0;
                    for (int o = 0; o < Med_History_Grid.Rows.Count - 1; o++)
                    {
                        if (Med_History_Grid.Rows[o].Cells[5].Value == null || Med_History_Grid.Rows[o].Cells[5].Value == DBNull.Value || String.IsNullOrWhiteSpace(Med_History_Grid.Rows[o].Cells[5].Value.ToString()))
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
                        this.Hide();
                        Extra_Medication te = new Extra_Medication(Patient_Name_Lab.Text, PatName.Text, clear);
                        te.Show();
                    }
                }
            }
        }

        private void Patient_Med_History_FormClosing(object sender, FormClosingEventArgs e)
        {
            Patients jok = new Patients(PatName.Text);
            jok.Show();
            this.Hide();
            
        }

        string endstr, endstat;
        string prox;
        string final = DateTime.Now.ToString("yyyy/MM/dd");
        DateTime reals;
        private void End_Medication_Button_Click(object sender, EventArgs e)
        {
            for (int o = 0; o < Med_History_Grid.Rows.Count - 1; o++)
            {
                if (Med_History_Grid.Rows[o].Cells[5].Value == null || Med_History_Grid.Rows[o].Cells[5].Value == DBNull.Value || String.IsNullOrWhiteSpace(Med_History_Grid.Rows[o].Cells[5].Value.ToString()))
                {
                    picko = Convert.ToDateTime(ok.ToString());
                    prox = picko.Year.ToString() + "-" + picko.Month.ToString() + "-" + picko.Day.ToString();
                    reals = Convert.ToDateTime(prox);
                    go = Med_History_Grid.Rows[o].Cells[1].Value;
                    go2 = Med_History_Grid.Rows[o].Cells[2].Value;
                    gu = Med_History_Grid.Rows[o].Cells[3].Value;
                    go3 = Med_History_Grid.Rows[o].Cells[4].Value;//begin date
                    go5 = Med_History_Grid.Rows[o].Cells[6].Value;
                    
                    getid = Convert.ToInt32(go);
                    goget = Med_History_Grid.Rows[o].Cells[3].Value;
                    gogetprox = Convert.ToString(goget);
                    DialogResult sure = MessageBox.Show("Are you sure you want to end the medication?", "End Medication", MessageBoxButtons.YesNo);
                    if (sure == DialogResult.Yes)
                    {
                        Endcon.Open();
                        endstat = "UPDATE Session_Table SET End_Status=1 WHERE ID=@ID AND Medication=@Medication AND Date<'" + prox + "'";
                        endstr = "UPDATE Patient_Medication_Historty SET End_Date='" + prox + "'" + "WHERE (ID=@ID AND Medication=@Medication AND End_Date IS NULL)";
                        MessageBox.Show(getid.ToString());
                        SqlCommand endcomm = new SqlCommand(endstr, Endcon);
                        SqlCommand endsta = new SqlCommand(endstat, Endcon);
                        endcomm.Parameters.AddWithValue("@ID", getid);
                        endcomm.Parameters.AddWithValue("@Medication", gogetprox);
                        endsta.Parameters.AddWithValue("@ID", getid);
                        endsta.Parameters.AddWithValue("@Medication", gogetprox);
                        endsta.ExecuteNonQuery();
                        endcomm.ExecuteNonQuery();

                    }
                    medfill();
                    Endcon.Close();
                }
            }
        }
    }
}
