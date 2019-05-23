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
    public partial class Double_Att_Graph : Form
    {
        public Double_Att_Graph(string idg, string medg, string begdat, string enddat, string patname, Boolean filler, string fillerfirstamount, string fillersecondamount, string medg1, string medg2)
        {
            InitializeComponent();
            id = idg;
            med = medg;
            begindate = begdat;
            enddate = enddat;
            medgg1 = medg1;
            medgg2 = medg2;
            firamo = fillerfirstamount;
            fillo = filler;
            secoamo = fillersecondamount;
            patnamo = patname;
            filldate();
        }

        public static SqlConnection gcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string id, med, begindate, enddate, medgg1, medgg2, firamo, secoamo, patnamo;
        Boolean fillo;

        private void Sleep_Graph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Quality of Sleep";
            string date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
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
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;

        }

        private void Appetite_Graph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Appetite";
            string date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
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
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
        }

        private void Eye_CGraph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Eye Contact";
            string date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
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
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;

        }

        private void Voice_Graph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Voice Tone";
            string date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
            }
            gcon.Open();
            SqlCommand datfilcom = new SqlCommand(date, gcon);
            SqlDataReader datred = datfilcom.ExecuteReader();
            Series dats = new Series();
            while (datred.Read())
            {
                Patient_Data_Chart.Series[0].Points.AddXY(datred["Date"], datred["Voice_Tone"]);
            }
            gcon.Close();
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
        }

        private void Attention_Graph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Attention Span";
            string date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
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
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 1;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
        }

        private void Score_Graph_Click(object sender, EventArgs e)
        {
            Patient_Data_Chart.Series[0].Points.Clear();
            Patient_Data_Chart.Series[0].LegendText = "Score";
            string date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
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
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 100;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 0;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 10;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.LineWidth = 2;
        }

        private void Double_Att_Graph_FormClosing(object sender, FormClosingEventArgs e)
        {
            Double_Session_List jo = new Double_Session_List(id, patnamo, med, fillo, begindate, enddate, firamo, secoamo, medgg1, medgg2);
            this.Hide();
            jo.Show();
        }

        DateTime actbeg, actend;
        string fullbeg, fullend;

        public void filldate()
        {
            actbeg = Convert.ToDateTime(begindate);
            fullbeg = actbeg.Year.ToString() + "-" + actbeg.Month.ToString() + "-" + actbeg.Day.ToString();
            string date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND Date >='" + fullbeg + "')  ORDER BY Date";
            if (enddate == "")
            {

            }
            else
            {
                actend = Convert.ToDateTime(enddate);
                fullend = actend.Year.ToString() + "-" + actend.Month.ToString() + "-" + actend.Day.ToString();
                date = "SELECT * FROM Double_Session_Table WHERE (ID=" + id + "AND Full_Combination='" + med + "' AND (Date BETWEEN '" + fullbeg + "' AND '" + fullend + "' )) ORDER BY Date";
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
           
            Patient_Data_Chart.ChartAreas[0].AxisY.Maximum = 100;
            Patient_Data_Chart.ChartAreas[0].AxisY.Minimum = 0;
            Patient_Data_Chart.ChartAreas[0].AxisY.Interval = 10;
            Patient_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            Patient_Data_Chart.ChartAreas[0].AxisX.LineWidth = 2;

        }
    }
}
