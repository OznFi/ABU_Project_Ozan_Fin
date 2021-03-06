﻿using System;
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
    public partial class View_Double_Session_Notes : Form
    {
        public View_Double_Session_Notes(string id, string med, string date)
        {
            InitializeComponent();
            idc = id;
            medc = med;
            datec = date;
            fillview();
        }
        string idc, medc, datec;
        DateTime actd;
        SqlConnection viewcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public void fillview()
        {
            viewcon.Open();
            actd = Convert.ToDateTime(datec);
            string co = "SELECT Notes FROM Double_Session_Table WHERE (ID=@ID AND Full_Combination=@Full_Combination AND Date=@Date)";
            SqlCommand viewcomm = new SqlCommand(co, viewcon);
            
            viewcomm.Parameters.AddWithValue("@ID", idc);
            viewcomm.Parameters.AddWithValue("@Full_Combination", medc);
            viewcomm.Parameters.AddWithValue("@Date", actd);
            SqlDataReader rip = viewcomm.ExecuteReader();
            while (rip.Read())
            {
                View_Rich_Box.Text = rip["Notes"].ToString();
            }
            viewcon.Close();
        }
    }
}
