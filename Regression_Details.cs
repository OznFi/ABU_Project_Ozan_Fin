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
using System.Windows.Forms.DataVisualization.Charting;
using MathNet;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Licence_Project
{
    public partial class Regression_Details : Form
    {
        public Regression_Details(string medname)
        {
            InitializeComponent();
            medicine = medname;
            makeprep();
            makethegrid();
            getparameters();
            getavrg();
            
        }

        public static SqlConnection regrescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection mother = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public List<double> SessionNumber = new List<double>();
        public List<double> UsedAmount = new List<double>();
        public List<double> Attribute = new List<double>();
        public List<double> AllUsedAmounts = new List<double>();
        public List<string> Medications = new List<string>();
        public List<double> PatientIDs = new List<double>();
        public List<double> StartNumber = new List<double>();
        public List<double> EndNumber = new List<double>();
        public List<double> AllPoint = new List<double>();
        public List<double> UsedAmounts = new List<double>();
        public List<double> AllEnds = new List<double>();
        public void makeprep()
        {
            mother.Open();
            string patientidsql = "SELECT * FROM Session_Table ";
            SqlCommand fillids = new SqlCommand(patientidsql, mother);
            SqlDataReader readids = fillids.ExecuteReader();
            while (readids.Read())
            {
                if (!(PatientIDs.Contains(Convert.ToDouble(readids["ID"]))))
                {
                    PatientIDs.Add(Convert.ToDouble(readids["ID"]));
                }
                if (!(Medications.Contains(readids["Medication"].ToString())))
                {
                    Medications.Add(readids["Medication"].ToString());
                }
            }
            mother.Close();

        }
        string medicine;
        DataTable datatab = new DataTable();
        public void makethegrid()
        {

            for (int i = 1; i < 9; i++)
            {
                var column = new DataColumn();
                
                if (i == 1)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Sleep Quality";
                    datatab.Columns.Add(column);
                }
                if (i == 2)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Appetite";
                    datatab.Columns.Add(column);
                }
                if (i == 3)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Eye Contact";
                    datatab.Columns.Add(column);
                }
                if (i == 4)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Voice Tone";
                    datatab.Columns.Add(column);
                }
                if (i == 5)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Attention Span";
                    datatab.Columns.Add(column);
                }
                if (i == 6)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Score";
                    datatab.Columns.Add(column);
                }
                if (i == 7)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Avarage Number of Sessions";
                    datatab.Columns.Add(column);
                }
                if (i == 8)
                {
                    column.DataType = typeof(double);
                    column.ColumnName = "Avarage Dose";
                    datatab.Columns.Add(column);
                }
            }
            //Linear_Specific_Stat.DataSource = datatab;
        }
        public void fillthearray(string med, string attribute)
        {
            regrescon.Open();
            string sqlstring = "SELECT * FROM Session_Table WHERE Medication= '" + med + "' ORDER BY Date";
            string temp;
            double temp2;
            SqlCommand comms = new SqlCommand(sqlstring, regrescon);
            SqlDataReader redreg = comms.ExecuteReader();
            while (redreg.Read())
            {
                SessionNumber.Add(Convert.ToDouble(redreg["Session_Number"]));
                UsedAmount.Add(Convert.ToDouble(redreg["Used_Amount"]));
                Attribute.Add(Convert.ToDouble(redreg[attribute]));

            }

            regrescon.Close();

        }
        string indicate;
        public void getparameters()
        {
            double[] patientid = PatientIDs.ToArray();
            string[] mednames = Medications.ToArray();
            mother.Open();//starts here
            double getav = 0;
            double getmav = 0;
                DataRow maboi = datatab.NewRow();

            for (int y = 0; y < patientid.Length; y++)
            {
                string getsesnums = "SELECT * FROM Session_Table WHERE Medication='" + medicine + "' AND ID=" + patientid[y] + " ORDER BY Date";
                SqlCommand pointcom = new SqlCommand(getsesnums, mother);
                string countsess = "SELECT COUNT(*) FROM Session_Table WHERE Medication='" + medicine + "'" ;
                SqlCommand countseses = new SqlCommand(countsess, mother);
                int sesnums = (int)countseses.ExecuteScalar();
                Total_Session_Nums.Text = "Total number of sessions in the database with this solo medication = "+" "+sesnums;
                SqlDataReader pointread = pointcom.ExecuteReader();
                while (pointread.Read())
                {


                    UsedAmounts.Add(Convert.ToDouble(Convert.ToDouble(pointread["Used_Amount"])));
                }
                pointread.Close();

                for (int hu = 0; hu < 6; hu++)
                {

                    if (hu == 0)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Sleep Quality";
                        fillthearray(medicine, "Quality_of_Sleep");
                    }
                    if (hu == 1)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Appetite";
                        fillthearray(medicine, "Appetite");
                    }
                    if (hu == 2)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Eye Contact";
                        fillthearray(medicine, "Eye_Contact");
                    }
                    if (hu == 3)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Voice Tone";
                        fillthearray(medicine, "Voice_Tone_Consistency");
                    }
                    if (hu == 4)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Attention Span";
                        fillthearray(medicine, "Attention_Span");
                    }
                    if (hu == 5)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Score";
                        fillthearray(medicine, "Score");
                    }
                    double[] usedamount = UsedAmount.ToArray();
                    double[] sessionnumber = SessionNumber.ToArray();
                    double[] attributes = Attribute.ToArray();
                    double[,] doub = new double[sessionnumber.Length, 2];
                    double sum = UsedAmount.Sum();
                    double mean = sum / usedamount.Length;
                    double deletsum = 0;
                    for (int why = 0; why < usedamount.Length; why++)
                    {
                        deletsum += Math.Pow(usedamount[why] - mean, 2);
                    }
                    double variance = deletsum / usedamount.Length;
                    double stdeviation = Math.Sqrt(variance);
                    //var listarray = new List<double[]>();
                    for (int g = 0; g < usedamount.Length; g++)
                    {
                        doub[g, 0] = 1.0;
                        //if (g == usedamount.Length - 1 && usedamount[g] == usedamount[g - 1])
                        //{
                        //   doub[g, 1] = usedamount[g] + 0.1;
                        //}
                        //else
                        //{
                        doub[g, 1] = (usedamount[g] - mean) / stdeviation;
                        //}
                    }
                    //for(int f=0; f<attributes.Length; f++)
                    //{
                    //  doub[2, f] = attributes[f];
                    //}
                    // {4, 4, 4, 4, 4, 3, 3, 3, 2, 2}
                    //{20, 20, 20, 20, 20, 20, 20, 20, 20, 20}
                    //{1, 2, 3, 4, 5, 6 ,7 ,8 ,9 , 10 }
                    double[] x = { 4, 4, 4, 4, 4, 3, 3, 3, 2, 2 };
                    var A = DenseMatrix.OfArray(doub);

                    var b = new DenseVector(attributes);
                    var h = A.QR().Solve(b);
                    var q = h[0];
                    var q2 = h[1];


                    maboi[indicate] = Math.Round(q2, 3);
                    if (indicate == "Sleep Quality")
                    {
                        Attribute1_Lab.Text = "Sleep Quality :  " + Math.Round(q2, 2);
                    }
                    if (indicate == "Appetite")
                    {
                        Attribute2_Lab.Text = "Appetite :  " + Math.Round(q2, 2);
                    }
                    if (indicate == "Eye Contact")
                    {
                        Attribute3_Lab.Text = "Eye Contact :  " + Math.Round(q2, 2);
                    }
                    if (indicate == "Voice Tone")
                    {
                        Attribute4_Lab.Text = "Voice Tone :  " + Math.Round(q2, 2);
                    }
                    if (indicate == "Attention Span")
                    {
                        Attribute5_Lab.Text = "Attention Span :  " + Math.Round(q2, 2);
                    }
                    if (indicate == "Score")
                    {
                        Score_Lab.Text = "Score :  " + Math.Round(q2, 2);
                    }


                }
            }
            datatab.Rows.Add(maboi);
            
            
            mother.Close();

        }

        public void getavrg()
        {
            mother.Open();
            AllUsedAmounts.Clear();
            string avrgdos = "SELECT * FROM Session_Table WHERE Medication= '" + medicine + "'";
            SqlCommand avarage = new SqlCommand(avrgdos, mother);
            SqlDataReader readavrg = avarage.ExecuteReader();
            while (readavrg.Read())
            {
                AllUsedAmounts.Add(Convert.ToDouble(readavrg["Used_Amount"]));
            }
            double all = AllUsedAmounts.Sum();
            double[] avarr = AllUsedAmounts.ToArray();
            Avarage_Dose_Lab.Text = "Avarage Dose (mg) ="+" "+Math.Round((all/avarr.Length), 2);
            mother.Close();
        }

        

        private void Predict_Dose_But_Click(object sender, EventArgs e)
        {
            double otok;

            if (double.TryParse(Predict_Dose_Box.Text, out otok))
            {
                double[] patientid = PatientIDs.ToArray();
                string[] mednames = Medications.ToArray();
                mother.Open();//starts here
                double getav = 0;
                double getmav = 0;
                DataRow maboi = datatab.NewRow();

                for (int y = 0; y < patientid.Length; y++)
                {
                    string getsesnums = "SELECT * FROM Session_Table WHERE Medication='" + medicine + "' AND ID=" + patientid[y] + " ORDER BY Date";
                    SqlCommand pointcom = new SqlCommand(getsesnums, mother);
                    SqlDataReader pointread = pointcom.ExecuteReader();
                    while (pointread.Read())
                    {


                        UsedAmounts.Add(Convert.ToDouble(Convert.ToDouble(pointread["Used_Amount"])));
                    }
                    pointread.Close();
                    for (int hu = 0; hu < 6; hu++)
                    {

                        if (hu == 0)
                        {
                            UsedAmount.Clear();
                            SessionNumber.Clear();
                            Attribute.Clear();
                            indicate = "Sleep Quality";
                            fillthearray(medicine, "Quality_of_Sleep");
                        }
                        if (hu == 1)
                        {
                            UsedAmount.Clear();
                            SessionNumber.Clear();
                            Attribute.Clear();
                            indicate = "Appetite";
                            fillthearray(medicine, "Appetite");
                        }
                        if (hu == 2)
                        {
                            UsedAmount.Clear();
                            SessionNumber.Clear();
                            Attribute.Clear();
                            indicate = "Eye Contact";
                            fillthearray(medicine, "Eye_Contact");
                        }
                        if (hu == 3)
                        {
                            UsedAmount.Clear();
                            SessionNumber.Clear();
                            Attribute.Clear();
                            indicate = "Voice Tone";
                            fillthearray(medicine, "Voice_Tone_Consistency");
                        }
                        if (hu == 4)
                        {
                            UsedAmount.Clear();
                            SessionNumber.Clear();
                            Attribute.Clear();
                            indicate = "Attention Span";
                            fillthearray(medicine, "Attention_Span");
                        }
                        if (hu == 5)
                        {
                            UsedAmount.Clear();
                            SessionNumber.Clear();
                            Attribute.Clear();
                            indicate = "Score";
                            fillthearray(medicine, "Score");
                        }
                        double[] usedamount = UsedAmount.ToArray();
                        double[] sessionnumber = SessionNumber.ToArray();
                        double[] attributes = Attribute.ToArray();
                        double[,] doub = new double[sessionnumber.Length, 2];
                        // initiating standardization
                        double sum = UsedAmount.Sum();
                        double mean = sum / usedamount.Length;
                        double deletsum = 0;
                        for(int why=0; why<usedamount.Length; why++)
                        {
                            deletsum += Math.Pow(usedamount[why] - mean, 2);
                        }
                        double variance = deletsum / usedamount.Length;
                        double stdeviation = Math.Sqrt(variance);

                        //var listarray = new List<double[]>();
                        for (int g = 0; g < usedamount.Length; g++)
                        {
                            doub[g, 0] = 1.0;
                            //if (g == usedamount.Length - 1 && usedamount[g] == usedamount[g - 1])
                            //{
                            //   doub[g, 1] = usedamount[g] + 0.1;
                            //}
                            //else
                            //{
                            doub[g, 1] = (usedamount[g]-mean)/stdeviation;
                            //}
                        }
                        //for(int f=0; f<attributes.Length; f++)
                        //{
                        //  doub[2, f] = attributes[f];
                        //}
                        // {4, 4, 4, 4, 4, 3, 3, 3, 2, 2}
                        //{20, 20, 20, 20, 20, 20, 20, 20, 20, 20}
                        //{1, 2, 3, 4, 5, 6 ,7 ,8 ,9 , 10 }
                        double[] x = { 4, 4, 4, 4, 4, 3, 3, 3, 2, 2 };
                        var A = DenseMatrix.OfArray(doub);

                        var b = new DenseVector(attributes);
                        var h = A.QR().Solve(b);
                        var q = h[0];
                        var q2 = h[1];

                        double dose = Convert.ToDouble(Predict_Dose_Box.Text);
                        double superdose = (dose - mean) / stdeviation;
                        var t = q + superdose * q2;




                        maboi[indicate] = Math.Round(t, 3);
                        if(indicate !="Score" && Math.Round(t, 2) > 20)
                        {
                            t = 20;
                        }
                        if(indicate != "Score" && Math.Round(t, 2) < 1)
                        {
                            t = 1;
                        }
                        if(indicate == "Score" && Math.Round(t, 2) > 100)
                        {
                            t = 100;
                        }
                        if (indicate == "Score" && Math.Round(t, 2) <0)
                        {
                            t = 0;
                        }
                        if (indicate == "Sleep Quality")
                        {
                            ExpectedSleepBox.Text = "Expected Sleep Quality :  " + Math.Round(t, 2);
                        }
                        if (indicate == "Appetite")
                        {
                            ExpectedAppetiteBox.Text = "Expected Appetite :  " + Math.Round(t, 2);
                        }
                        if (indicate == "Eye Contact")
                        {
                            ExpectedEyeCBox.Text = "Expected Eye Contact :  " + Math.Round(t, 2);
                        }
                        if (indicate == "Voice Tone")
                        {
                            ExpectedVoiceBox.Text = "Expected Voice Tone :  " + Math.Round(t, 2);
                        }
                        if (indicate == "Attention Span")
                        {
                            ExpectedAttentionBox.Text = "Expected Attention Span :  " + Math.Round(t, 2);
                        }
                        if (indicate == "Score")
                        {
                            
                            ExpectedScore_Lab.Text = "Expected Score :  " + Math.Round(t, 2);
                        }


                    }
                }
                datatab.Rows.Add(maboi);


                mother.Close();
            }
            else
            {
                MessageBox.Show("Enter A Valid Number");
            }
        }

        private void Regression_Details_FormClosing(object sender, FormClosingEventArgs e)
        {
            Linear_Regression joh = new Linear_Regression();
            this.Hide();
            joh.Show();
        }
    }
    
}
