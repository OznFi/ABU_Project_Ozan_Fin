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
    public partial class All_Sessions_Graph : Form
    {
        //THIS FORM IS NOT IN USE ANYMORE!!!

        public All_Sessions_Graph(string id)
        {
            InitializeComponent();
            ide = id;
            gatherlist();
            All_Check.Checked = true; 
            filldate("Score", apostle);
            

        }
        string ide;
        string[] thelist;
        public List<double> Attribute = new List<double>();
        public List<string> Mednames = new List<string>();
        DateTime[] thetimelist;
        public static SqlConnection acon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public void gatherlist()
        {
            Mednames.Clear();
            var sublist = new List<string>();
            var subtimelist = new List<DateTime>(); 
            string getcom = "SELECT * FROM Patient_Medication_Historty WHERE ID=" + ide + "AND End_Date IS NULL";
            acon.Open();
            SqlCommand getlistcom = new SqlCommand(getcom, acon);
            SqlDataReader readlist = getlistcom.ExecuteReader();
            while (readlist.Read())
            {
                Mednames.Add(Convert.ToString(readlist["Medication"]));
                sublist.Add(readlist.GetString(2));
                subtimelist.Add(readlist.GetDateTime(3));
            }
            thelist = sublist.ToArray();
            thetimelist = subtimelist.ToArray();
            acon.Close();
            
        }
        Boolean apostle;
        string test;
        public void filldate(string type, Boolean go)
        {
            string[] thelist2 = Mednames.ToArray();
            Patient_All_Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            Patient_All_Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            Patient_All_Chart.Series[0].Color = Color.DarkBlue;
            Patient_All_Chart.Series[0].Points.Clear();
            if (go == true)
            {
                if (thelist2 == null || thelist2.Length == 0)
                {
                    MessageBox.Show("The Patient is not using any medications");
                }
                else
                {
                    string coolerdate = "SELECT * FROM Session_Table WHERE ID=" + ide + " AND End_Status IS NULL AND Medication IN ( ";
                    for (int i = 0; i < thelist2.Length; i++)
                    {
                        if (i == thelist2.Length - 1)
                        {
                            coolerdate += "'"+thelist2[i]+"' ) ORDER BY Session_Number";
                        }
                        else
                        {
                            coolerdate +="'"+ thelist2[i] + "' , ";
                        }
                    }
                    Patient_All_Chart.Series[0].LegendText = type;
                    acon.Open();
                    SqlCommand supdatfil = new SqlCommand(coolerdate, acon);
                    SqlDataReader supdatread = supdatfil.ExecuteReader();
                    Series dots = new Series();
                    while (supdatread.Read())
                    {
                        
                        Patient_All_Chart.Series[0].Points.AddXY(supdatread["Session_Number"], supdatread[type]);
                        
                    }
                    acon.Close();
                    test = coolerdate;
                }
            }
            else
            {
                string date = "SELECT * FROM Session_Table WHERE ID=" + ide+"ORDER BY Session_Number";
                Patient_All_Chart.Series[0].LegendText = type;
                acon.Open();
                SqlCommand datfilcom = new SqlCommand(date, acon);
                SqlDataReader datred = datfilcom.ExecuteReader();
                Series dats = new Series();
                while (datred.Read())
                {

                    Patient_All_Chart.Series[0].Points.AddXY(datred["Session_Number"], datred[type]);
                }
                acon.Close();
            }
            if (type == "Score")
            {
                Patient_All_Chart.ChartAreas[0].AxisY.Maximum = 100;
                Patient_All_Chart.ChartAreas[0].AxisY.Minimum = 0;
                Patient_All_Chart.ChartAreas[0].AxisY.Interval = 10;
                Patient_All_Chart.ChartAreas[0].AxisX.Interval = 1;
                Patient_All_Chart.ChartAreas[0].AxisX.LineWidth = 2;
            }
            else
            {
                Patient_All_Chart.ChartAreas[0].AxisY.Maximum = 5;
                Patient_All_Chart.ChartAreas[0].AxisY.Minimum = 1;
                Patient_All_Chart.ChartAreas[0].AxisY.Interval = 1;
                Patient_All_Chart.ChartAreas[0].AxisX.Interval = 1;
            }
        }

        private void All_Sleep_GButton_Click(object sender, EventArgs e)
        {
            Patient_All_Chart.Series[0].Points.Clear();
            filldate("Quality_of_Sleep", apostle);
        }

        private void Current_Value_Check_CheckStateChanged(object sender, EventArgs e)
        {
            
           
            if (thelist ==null || thelist.Length == 0)
            {
                All_Check.Checked = true;
                MessageBox.Show("The Patient is not using any medications");
            }
            else
            {
                apostle = true;
                filldate("Score", apostle);
                if (All_Check.Checked != false)
                {
                    All_Check.Checked = false;
                }
                
            }
        }

        private void All_Check_CheckStateChanged(object sender, EventArgs e)
        {
            apostle = false;
            filldate("Score", apostle);
            if (Current_Value_Check.Checked != false)
            {
                Current_Value_Check.Checked = false;
            }
            
            
        }

        private void All_Appetite_GButton_Click(object sender, EventArgs e)
        {
            Patient_All_Chart.Series[0].Points.Clear();
            filldate("Appetite", apostle);
        }

        private void All_Eye_GButton_Click(object sender, EventArgs e)
        {
            Patient_All_Chart.Series[0].Points.Clear();
            filldate("Eye_Contact", apostle);
        }

        private void All_Attention_GButton_Click(object sender, EventArgs e)
        {
            Patient_All_Chart.Series[0].Points.Clear();
            filldate("Attention_Span", apostle);
        }

        private void All_Voice_GButton_Click(object sender, EventArgs e)
        {
            Patient_All_Chart.Series[0].Points.Clear();
            filldate("Voice_Tone_Consistency", apostle);
        }

        private void All_Score_GButton_Click(object sender, EventArgs e)
        {
            Patient_All_Chart.Series[0].Points.Clear();
            filldate("Score", apostle);
        }
    }
}
