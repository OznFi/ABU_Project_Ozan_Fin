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

namespace Licence_Project
{
    public partial class Patient_Att_Graph : Form
    {
        public Patient_Att_Graph(string idg, string medg, string begdat, string enddat)
        {
            InitializeComponent();
            id = idg;
            med = medg;
            begindate = begdat;
            enddate = enddat;
            filldate();
        }
        public static SqlConnection gcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string id, med, begindate, enddate;
        DateTime actbeg, actend;
        string fullbeg, fullend;
        public void filldate()
        {
            actbeg = Convert.ToDateTime(begindate);
            fullbeg = actbeg.Year.ToString() + "-" + actbeg.Month.ToString() + "-" + actbeg.Day.ToString();
            string date= "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND Date >='" + fullbeg+ "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            
           
            //FOR DEBUG PURPOSES
            
            Patient_Data_Chart.Series[0].LegendText = "Score";
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Score"]);
            }
            gcon.Close();
            //label1.Text = "";
            //label2.Text = "";
            //label3.Text="";
            //label4.Text = "";
            //label5.Text = "";
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 100;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 0;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 10;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.LineWidth = 2;
            Patient_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

        }
        //NEEDS ADJUSTMENTS ON THE SQLCOMAND STRINGS
        private void Sleep_QGraph_Click(object sender, EventArgs e)
        {
            
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Quality of Sleep";
            
            string date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Quality_of_Sleep"]);
            }
            gcon.Close();
            //label1.Text = "1: 2-3 Hours of sleep";
            //label2.Text = "2: 4-5 Hours of sleep";
            //label3.Text = "3: 5-6 Hours of sleep";
            //label4.Text = "4: 7-8 Hours of sleep";
            //label5.Text = "5: 9+ Hours of sleep";
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private void Appetite_Graph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Appetite";
            string date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Appetite"]);
            }
            gcon.Close();
            //label1.Text = "1: Barely eating";
            //label2.Text = "2: Missing a few meals";
            //label3.Text = "3: Properly eating";
            //label4.Text = "4: Eating more than usual";
            //label5.Text = "5: Excessive Eating";
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private void Eye_CGraph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Eye Contact";
            string date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Eye_Contact"]);
            }
            gcon.Close();
            //label1.Text = "1: Almost no eye contact";
            //label2.Text = "2: Brief moments of eye contact";
            //label3.Text = "3: Consistent eye contact";
            //label4.Text = "4: Frequent eye contact";
            //label5.Text = "5: Constant eye contact";
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private void Voice_TGraph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Voice Tone";
            string date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Voice_Tone_Consistency"]);
            }
            gcon.Close();
            //label1.Text = "1: Monotone and/or low voice";
            //label2.Text = "2: Partially emphasizing words";
            //label3.Text = "3: Proper Toning";
            //label4.Text = "4: Slightly inconsistent tone";
            //label5.Text = "5: Inconsistent voice tone";
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }

        private void Attention_SGraph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Attention Span";
            string date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Attention_Span"]);
            }
            gcon.Close();
            //label1.Text = "1: Minimum attention";
            //label2.Text = "2: Easily distracted";
            //label3.Text = "3: Decent attention span";
            //label4.Text = "4: Consistent attention span";
            //label5.Text = "5: Completely focused";

            
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

        }

        private void Score_Graph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Score";
            string date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Session_Table WHERE (ID=" + id + "AND Medication='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Score"]);
            }
            gcon.Close();
            //label1.Text = "";
            //label2.Text = "";
            //label3.Text = "";
            //label4.Text = "";
            //label5.Text = "";
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 100;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 0;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 10;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.LineWidth = 2;
            Patient_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
        }
    }
}
