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
    public partial class Linear_Regression : Form
    {
        public Linear_Regression()
        {
            InitializeComponent();
            makeprep();
            makethegrid();
            getparameters();

        }
        public static SqlConnection regrescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection mother = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public List<double> SessionNumber = new List<double>();
        public List<double> UsedAmount = new List<double>();
        public List<double> Attribute = new List<double>();
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
        DataTable datatab = new DataTable();
        public void makethegrid()
        {
            
            for (int i = 0; i < 7; i++)
            {
                var column = new DataColumn();
                if (i == 0)
                {
                    column.DataType = typeof(string);
                    column.ColumnName = "Medication";
                    datatab.Columns.Add(column);
                }
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

            }
            Linear_Stat_Grid.DataSource = datatab;
            Linear_Stat_Grid.Columns[2].Width = 75;
            Linear_Stat_Grid.Columns[3].Width = 75;
            Linear_Stat_Grid.Columns[4].Width = 75;
            Linear_Stat_Grid.Columns[5].Width = 75;
            Linear_Stat_Grid.Columns[6].Width = 75;
            Linear_Stat_Grid.Columns[7].Width = 75;
        }

        public void fillthearray(string med, string attribute)
        {
            regrescon.Open();
            string sqlstring = "SELECT * FROM Session_Table WHERE Medication= '" + med + "' ORDER BY ID, Date";
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
        private void button1_Click(object sender, EventArgs e)
        {
            Double_Linear_Regression jo = new Double_Linear_Regression();
            this.Hide();
            jo.Show();
        }

        public void filldoublear(double[] x, int dim)
        {
            for (int i = 0; i < dim; i++)
            {
                x[i] = i;
            }
        }
        public void getparameters()
        {
            
            double[] patientid = PatientIDs.ToArray();
            //MessageBox.Show(patientid.Length.ToString() + " yo");
            string[] mednames = Medications.ToArray();
            mother.Open();//starts here
            for (int o = 0; o < mednames.Length; o++)
            {
                
                DataRow maboi = datatab.NewRow();
                maboi["Medication"] = mednames[o];
                for (int y = 0; y < patientid.Length; y++)
                {
                    string getsesnums = "SELECT * FROM Session_Table WHERE Medication='" + mednames[o] + "' AND ID=" + patientid[y] + " ORDER BY Date";
                    SqlCommand pointcom = new SqlCommand(getsesnums, mother);
                    SqlDataReader pointread = pointcom.ExecuteReader();
                    while (pointread.Read())
                    {

                        UsedAmounts.Add(Convert.ToDouble(Convert.ToDouble(pointread["Used_Amount"])));
                    }
                    pointread.Close(); // SHIFTY STUFF, IS IT PROPER?
                
                }
                
                for (int hu = 0; hu < 6; hu++)
                {
                    
                    if (hu == 0)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Sleep Quality";
                        fillthearray(mednames[o], "Quality_of_Sleep");
                    }
                    if (hu == 1)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Appetite";
                        fillthearray(mednames[o], "Appetite");
                    }
                    if (hu == 2)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Eye Contact";
                        fillthearray(mednames[o], "Eye_Contact");
                    }
                    if (hu == 3)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Voice Tone";
                        fillthearray(mednames[o], "Voice_Tone_Consistency");
                    }
                    if (hu == 4)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Attention Span";
                        fillthearray(mednames[o], "Attention_Span");
                    }
                    if (hu == 5)
                    {
                        UsedAmount.Clear();
                        SessionNumber.Clear();
                        Attribute.Clear();
                        indicate = "Score";
                        fillthearray(mednames[o], "Score");
                    }
                    double[] usedamount = UsedAmount.ToArray();
                    double[] sessionnumber = SessionNumber.ToArray();
                    double[] attributes = Attribute.ToArray();
                    double[,] doub = new double[sessionnumber.Length, 2];
                    // initiating standardization
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
                    
                    
                    
                    maboi[indicate] = Math.Round(q2, 2);
                    
                    
                }
                datatab.Rows.Add(maboi);
                AllEnds.Clear();
                EndNumber.Clear();
                UsedAmounts.Clear();
                
            }

            mother.Close();
            Linear_Stat_Grid.Columns[0].Width = 75;
            
        }
        int selectint;
        object dat;
        private void Linear_Stat_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectint = e.RowIndex;
            DataGridViewRow row = new DataGridViewRow();
            row = Linear_Stat_Grid.Rows[selectint];
            if (e.ColumnIndex == 0)
            {
                dat = row.Cells[1].Value;
                Regression_Details rego = new Regression_Details(dat.ToString());
                this.Hide();
                rego.Show();
            }
        }
    }

}

