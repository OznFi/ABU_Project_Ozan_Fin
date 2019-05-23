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
    public partial class Double_Linear_Regression : Form
    {
        public Double_Linear_Regression()
        {
            InitializeComponent();
            Total_Num_Lab.Text = "";
            DoubleReg_Chart.Series[0].LegendText = "";

        }
        public static SqlConnection regrescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection mother = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public List<double> FirstUsedAmount = new List<double>();
        public List<double> Attribute = new List<double>();
        public List<string> firstmed = new List<string>();
        public List<string> secondmed = new List<string>();
        public List<double> SecondUsedAmount = new List<double>();
       

        public void fillthearray(string med1, string med2, string attribute)
        {
            FirstUsedAmount.Clear();
            SecondUsedAmount.Clear();
            firstmed.Clear();
            secondmed.Clear();
            Attribute.Clear();
            firstmed.Clear();
            secondmed.Clear();
            regrescon.Open();
            string medcom1 = med1 + "-" + med2;
            string medcom2 = med2 + "-" + med1;
            string sqlstring = "SELECT * FROM Double_Session_Table WHERE Full_Combination IN ('" + medcom1 + "', '"+medcom2+"') ORDER BY ID, Date";
            
            double temp2;
            SqlCommand comms = new SqlCommand(sqlstring, regrescon);
            SqlDataReader redreg = comms.ExecuteReader();
            while (redreg.Read())
            {
                
                FirstUsedAmount.Add(Convert.ToDouble(redreg["First_Used_Amount"]));
                SecondUsedAmount.Add(Convert.ToDouble(redreg["Second_Used_Amount"]));
                Attribute.Add(Convert.ToDouble(redreg[attribute]));
                firstmed.Add(Convert.ToString(redreg["First_Medication"]));
                secondmed.Add(Convert.ToString(redreg["Second_Medication"]));

            }

            regrescon.Close();

        }
        
        public void regression(string attribute, string proper)
        {
            regrescon.Open();
            string medcom1 = First_Med_Box.Text + "-" + Second_Med_Box.Text;
            string medcom2 = Second_Med_Box.Text + "-" + First_Med_Box.Text;
            string checkcombo = "SELECT COUNT(*) FROM Double_Session_Table WHERE Full_Combination IN ('" + medcom1 + "', '" + medcom2 + "') ";
            SqlCommand checomb = new SqlCommand(checkcombo, regrescon);
            int yo = (int)checomb.ExecuteScalar();
            regrescon.Close();
            if (yo <=0)
            {
                MessageBox.Show("This combination does not exist in the database ");
            }
            else
            {
                Total_Num_Lab.Text = "Total number of sessions with this medication combination = "+" "+yo;
                DoubleReg_Chart.Series[0].Points.Clear();
                DoubleReg_Chart.Series[0].LegendText = proper;
                fillthearray(First_Med_Box.Text, Second_Med_Box.Text, attribute);
                string[] firstmedname = firstmed.ToArray();
                string[] secondmedname = secondmed.ToArray();
                double[] firstusedamount = FirstUsedAmount.ToArray();
                double[] secondusedamount = SecondUsedAmount.ToArray();
                double[] attributes = Attribute.ToArray();
                double[,] doub = new double[firstusedamount.Length, 3];
                double sum1 = FirstUsedAmount.Sum();
                double mean1 = sum1 / firstusedamount.Length;
                double deletsum1 = 0;
                double sum2 = SecondUsedAmount.Sum();
                double mean2 = sum2 / secondusedamount.Length;
                double deletsum2 = 0;
                for (int why = 0; why < firstusedamount.Length; why++)
                {
                    deletsum1 += Math.Pow(firstusedamount[why] - mean1, 2);

                }
                for (int why = 0; why < secondusedamount.Length; why++)
                {
                    deletsum2 += Math.Pow(secondusedamount[why] - mean2, 2);
                }
                double variance1 = deletsum1 / firstusedamount.Length;
                double stdeviation1 = Math.Sqrt(variance1);
                double variance2 = deletsum2 / secondusedamount.Length;
                double stdeviation2 = Math.Sqrt(variance2);
                for (int g = 0; g < firstusedamount.Length; g++)
                {
                    doub[g, 0] = 1.0;

                    doub[g, 1] = (firstusedamount[g] - mean1) / stdeviation1;

                    doub[g, 2] = (secondusedamount[g] - mean2) / stdeviation2;

                }
                //MessageBox.Show(stdeviation1.ToString());
                //MessageBox.Show(stdeviation2.ToString());
                //MessageBox.Show(attributes.Length.ToString());
                var A = DenseMatrix.OfArray(doub);
                var b = new DenseVector(attributes);
                var h = A.QR().Solve(b);
                var q = h[0];
                var q2 = h[1];
                var q3 = h[2];
                DoubleReg_Chart.Series[0].Points.AddXY(firstmedname[0], q2);
                DoubleReg_Chart.Series[0].Points.AddXY(secondmedname[0], q3);
                
            }
        }

        private void Enter_DoubleReg_Button_Click(object sender, EventArgs e)
        {
            if(First_Med_Box.Text=="" || Second_Med_Box.Text == "")
            {
                MessageBox.Show("Please fill both of the medication fields ");
            }
            else
            {
                regression("Score", "Score");
            }
        }

        private void Sleep_Regression_Click(object sender, EventArgs e)
        {
            regression("Quality_of_Sleep", "Sleep Quality");
        }

        private void Appetite_Regression_Click(object sender, EventArgs e)
        {
            regression("Appetite","Appetite");
        }

        private void Eye_CRegression_Click(object sender, EventArgs e)
        {
            regression("Eye_Contact", "Eye Contact");
        }

        private void Voice_Regression_Click(object sender, EventArgs e)
        {
            regression("Voice_Tone", "Voice Tone");
        }

        private void Attention_Regression_Click(object sender, EventArgs e)
        {
            regression("Attention_Span", "Attention Span");
        }

        private void Score_Regression_Click(object sender, EventArgs e)
        {
            regression("Score", "Score");
        }

        private void Double_Linear_Regression_FormClosing(object sender, FormClosingEventArgs e)
        {
            Linear_Regression jog = new Linear_Regression();
            this.Hide();
            jog.Show();
        }
    }
}
