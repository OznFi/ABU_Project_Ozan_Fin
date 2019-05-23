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
    public partial class Medicines_Data_Graph : Form
    {
        //THIS FORM IS NOT IN USE ANYMORE!!!

        public Medicines_Data_Graph(string meds)
        {
            InitializeComponent();
            medname = meds;
            getdat = "SELECT * FROM Session_Table WHERE Medication='" + medname + "'";
            addsecondseries();
            fillgraph("Score");           
        }
        
        public static SqlConnection meddatcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string medname;
        string getdat;
        public List<int> Xaxis = new List<int>();
        public List<int> Yaxis = new List<int>();
        string firstmed;
        public  void addsecondseries()
        {
            Medicine_Data_Chart.Series.Add("Regression");
            Medicine_Data_Chart.Series[1].ChartType = SeriesChartType.Spline;
        }
        public void fillgraph(string type)
        {
            Medicine_Data_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Medicine_Data_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            Medicine_Data_Chart.Series[0].Points.Clear();
            Medicine_Data_Chart.Series[1].Points.Clear();
            Medicine_Data_Chart.Series[0].LegendText = type;
            
            meddatcon.Open();
            SqlCommand meddatcom = new SqlCommand(getdat, meddatcon);
            SqlDataReader medread = meddatcom.ExecuteReader();
            while (medread.Read())
            {
                Medicine_Data_Chart.Series[0].Points.AddXY(medread["Session_Number"], medread[type]);
                Xaxis.Add(Convert.ToInt32(medread["Session_Number"]));
                Yaxis.Add(Convert.ToInt32(medread[type]));
            }
            double minX = Xaxis.ToList().Min();
            double maxX = Xaxis.ToList().Max();
            double meanX = Xaxis.Sum() / Xaxis.Count();
            double meanY = Yaxis.Sum() / Yaxis.Count();
            double t = 0;
            double b = 0;
            for(int i=0; i<Xaxis.Count(); i++)
            {
                t += (Xaxis[i] - meanX) * (Yaxis[i] * meanY);
                b += (Xaxis[i] - meanX) * (Xaxis[i] - meanX);
            }
            double slope = t / b;

            double y0 = meanY - slope * meanX;
            Medicine_Data_Chart.Series[1].Points.AddXY(0, y0);
            Medicine_Data_Chart.Series[1].Points.AddXY(minX, y0 + minX * slope);
            Medicine_Data_Chart.Series[1].Points.AddXY(maxX, y0 + maxX * slope);
            meddatcon.Close();
            if (type == "Score")
            {
                Medicine_Data_Chart.ChartAreas[0].AxisY.Maximum = 100;
                Medicine_Data_Chart.ChartAreas[0].AxisY.Minimum = 0;
                Medicine_Data_Chart.ChartAreas[0].AxisX.Minimum = 0;
                Medicine_Data_Chart.ChartAreas[0].AxisY.Interval = 10;
                Medicine_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
                Medicine_Data_Chart.ChartAreas[0].AxisX.LineWidth = 2;
            }
            else
            {
                Medicine_Data_Chart.ChartAreas[0].AxisX.Minimum = 0;
                Medicine_Data_Chart.ChartAreas[0].AxisY.Maximum = 20;
                Medicine_Data_Chart.ChartAreas[0].AxisY.Minimum = 0;
                Medicine_Data_Chart.ChartAreas[0].AxisY.Interval = 1;
                Medicine_Data_Chart.ChartAreas[0].AxisX.Interval = 1;
            }
            Xaxis.Clear();
            Yaxis.Clear();
        }

        private void Change_Med_But_Click(object sender, EventArgs e)
        {
            getdat = "SELECT * FROM Session_Table WHERE Medication='" + Change_MedDat_Field.Text + "'";
            firstmed = Change_MedDat_Field.Text;
            fillgraph("Score");
            Add_SecondMed_But.Enabled = true;
        }

        private void Add_SecondMed_But_Click(object sender, EventArgs e)
        {
            getdat= "SELECT * FROM Session_Table WHERE Medication IN ('" + Change_MedDat_Field.Text + "', '"+ Add_SecondMed_Field.Text+"')";
            fillgraph("Score");
            Add_SecondMed_But.Enabled = false;
        }

        private void Sleep_Quality_But_Click(object sender, EventArgs e)
        {
            fillgraph("Quality_of_Sleep");
        }

        private void Appetite_But_Click(object sender, EventArgs e)
        {
            fillgraph("Appetite");
        }

        private void EyeC_But_Click(object sender, EventArgs e)
        {
            fillgraph("Eye_Contact");
        }

        private void VoiceT_But_Click(object sender, EventArgs e)
        {
            fillgraph("Voice_Tone_Consistency");
        }

        private void Attention_But_Click(object sender, EventArgs e)
        {
            fillgraph("Attention_Span");
        }

        private void Score_But_Click(object sender, EventArgs e)
        {
            fillgraph("Score");
        }
    }
}
