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
    public partial class Combined_Regression : Form
    {
        public Combined_Regression()
        {
            InitializeComponent();
        }
        Boolean existence;
        Boolean dateexist;
        int existcount, dateexistcount;
        int existcounte, checkatribute;
        public static SqlConnection multiregrescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection testregrescon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public static SqlConnection secondregtest = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public List<double> InitialDose = new List<double>();
        public List<double> Attribute = new List<double>();
        public List<double> SecondDose = new List<double>();
        public List<double> OptionalDose = new List<double>();
        public List<DateTime> InitialDates = new List<DateTime>();
        public List<DateTime> LatestDate = new List<DateTime>();
        public List<double> PatientIDs = new List<double>();
        public List<DateTime> StartDates = new List<DateTime>();
        public List<DateTime> SessionDates = new List<DateTime>();
        public List<double> temporarydosesofsessions = new List<double>();
        public List<DateTime> StartDates2 = new List<DateTime>();
        public List<DateTime> EndDates = new List<DateTime>();
        public List<DateTime> EndDates2 = new List<DateTime>();
        public void checkexist(string med)
        {
            string comand = "SELECT COUNT (*) FROM Session_List WHERE Medication='"+med+"' ";
            testregrescon.Open();
            SqlCommand testit = new SqlCommand(comand, testregrescon);
            existcount = (int)testit.ExecuteScalar();
            if (existcount > 0)
            {
                existence = true;
            }
            else
            {
                existence=!true;
            }
            testregrescon.Close();
        }
        Boolean fullexistence;
        public void checkfullexist(string med, double id)
        {
            string comand = "SELECT COUNT (*) FROM Session_List WHERE Medication='" + med + "' AND ID= " + id;
            testregrescon.Open();
            SqlCommand testit = new SqlCommand(comand, testregrescon);
            existcount = (int)testit.ExecuteScalar();
            if (existcount > 0)
            {
                fullexistence = true;
            }
            else
            {
                fullexistence = !true;
            }
            testregrescon.Close();
        }
        public void dateexistence(string dat, string medname)
        {
            string comand = "SELECT COUNT (*) FROM Session_Table WHERE Date='" + dat + "' AND Medication='"+medname+"'";
            testregrescon.Open();
            SqlCommand testit = new SqlCommand(comand, testregrescon);
            dateexistcount = (int)testit.ExecuteScalar();
            if (existcount > 0)
            {
                dateexist = true;
            }
            else
            {
                dateexist = !true;
            }
            testregrescon.Close();
        }

        public void initialfill(string initialmed, string attributee)
        {
            testregrescon.Open();
            PatientIDs.Clear();
            string initi = "SELECT * FROM Patient_List ";
            SqlCommand fillfirst = new SqlCommand(initi, testregrescon);
            SqlDataReader getids = fillfirst.ExecuteReader();
            while (getids.Read())
            {
                if (!(PatientIDs.Contains(Convert.ToDouble(getids["ID"]))))
                {
                    PatientIDs.Add(Convert.ToDouble(getids["ID"]));
                    
                }
                
            }
            testregrescon.Close();
        }
        public void fillstarts2(double id)
        {
            testregrescon.Open(); // CHEEEEECK
            StartDates2.Clear();
            EndDates2.Clear();
            string iddate = "SELECT * FROM Patient_Medication_Historty WHERE ID=" + id + " AND Medication='" + Secondary_MedBox.Text + "' ORDER BY Date Starting_Date";
            SqlCommand getsdates = new SqlCommand(iddate, testregrescon);
            SqlDataReader getstars = getsdates.ExecuteReader();
            while (getstars.Read())
            {
                StartDates2.Add(Convert.ToDateTime(getstars["Starting_Date"]));

                EndDates2.Add(Convert.ToDateTime(getstars["End_Date"]));
            }
            testregrescon.Close();
        }
        public void filldoses(double id, string mednam, DateTime start, DateTime end)
        {
            SessionDates.Clear();
            temporarydosesofsessions.Clear();
            testregrescon.Open();
            string iddate = "SELECT * FROM Session_Table WHERE ID=" + id + " AND Medication='" + Secondary_MedBox.Text + "' AND Date BETWEEN '"+start+"' AND '"+end+"'";
            SqlCommand getsdates = new SqlCommand(iddate, testregrescon);
            SqlDataReader getstars = getsdates.ExecuteReader();
            while (getstars.Read())
            {
                SessionDates.Add(Convert.ToDateTime(getstars["Date"]));
                temporarydosesofsessions.Add(Convert.ToDouble(getstars["Used_Amount"]));
            }
            testregrescon.Close();
        }
        public void fillstarts(double id)
        {
            testregrescon.Open();
            StartDates.Clear();
            EndDates.Clear();
            string iddate = "SELECT * FROM Patient_Medication_Historty WHERE ID="+id+" AND Medication='"+Initial_MedBox.Text+"' ORDER BY Date Starting_Date";
            SqlCommand getsdates = new SqlCommand(iddate, testregrescon);
            SqlDataReader getstars = getsdates.ExecuteReader();
            while (getstars.Read())
            {
                StartDates.Add(Convert.ToDateTime(getstars["Starting_Date"]));

                EndDates.Add(Convert.ToDateTime(getstars["End_Date"]));
            }
            testregrescon.Close();
        }
        Boolean correspondence;
        public void checkcorrespondence(int numo)//has to be executed after fillstarts() string medo was cut
        {
            InitialDose.Clear();
            SecondDose.Clear();
            OptionalDose.Clear();// is it true???
            testregrescon.Open();
            double[] ids = PatientIDs.ToArray();
            for(int i=0; i<ids.Length; i++)
            {
                checkfullexist(Initial_MedBox.Text, ids[i]);
                if (fullexistence == true)
                {
                    fillstarts(ids[i]);
                    DateTime[] sdates = StartDates.ToArray();
                    DateTime[] enddate = EndDates.ToArray();
                    for (int y = 0; y < sdates.Length; y++)
                    {
                        if (numo == 2)
                        {
                            if (enddate[y] != null)
                            {
                                string sotwo = "SELECT COUNT (*) FROM Patient_Medication_Historty WHERE ID=" + ids[i] + " AND Medication='" + Secondary_MedBox.Text + "' AND Starting_Date BETWEEN '" + sdates[y] + "' AND '" + enddate[y] + "'";
                                SqlCommand testsecond = new SqlCommand(sotwo, testregrescon);
                                existcount = (int)testsecond.ExecuteScalar();
                                if (existcount > 0)
                                {
                                    fillstarts2(ids[i]);
                                    DateTime[] sdates2 = StartDates2.ToArray();
                                    DateTime[] enddate2 = EndDates2.ToArray();
                                    
                                    for (DateTime date = sdates[y]; date < enddate[y]; date.AddDays(1))
                                    {
                                        for (int q = 0; q < sdates2.Length; q++)
                                        {
                                            if (enddate2[q] != null)
                                            {
                                                if (sdates2[q] <= enddate[y])
                                                {
                                                    if (enddate2[q]>=enddate[y])
                                                    {

                                                    }



                                                }
                                            }
                                        }
                                        string checkattr = "SELECT COUNT (*) FROM Session_Table WHERE ID=" + ids[i] + " AND Date='" + date + "'";
                                        SqlCommand checkattrcom = new SqlCommand(checkattr, testregrescon);
                                        checkatribute = (int)checkattrcom.ExecuteScalar();
                                        if (checkatribute > 0)
                                        {
                                            dateexistence(date.ToString(), Initial_MedBox.Text);
                                            if (dateexist == true)
                                            {
                                                string firstadd = "SELECT TOP 1 * FROM Session_Table WHERE ID=" + ids[i] + " AND Medication='" + Initial_MedBox.Text + "' AND Date ='" + date + "'";
                                                SqlCommand addfirst = new SqlCommand(firstadd, testregrescon);
                                                SqlDataReader readfirst = addfirst.ExecuteReader();
                                                while (readfirst.Read())
                                                {
                                                    InitialDose.Add(Convert.ToDouble(readfirst["Used_Amount"]));
                                                }
                                                readfirst.Close();
                                            }
                                            else
                                            {
                                                InitialDose.Add(0);
                                            }

                                            dateexistence(date.ToString(), Secondary_MedBox.Text);
                                            if (dateexist == true)
                                            {
                                                string secondadd = "SELECT TOP 1 * FROM Session_Table WHERE ID=" + ids[i] + " AND Medication='" + Secondary_MedBox.Text + "' AND Date ='" + date + "'";
                                                SqlCommand addsecond = new SqlCommand(secondadd, testregrescon);
                                                SqlDataReader readsecond = addsecond.ExecuteReader();
                                                while (readsecond.Read())
                                                {
                                                    SecondDose.Add(Convert.ToDouble(readsecond["Used_Amount"]));
                                                }
                                                readsecond.Close();
                                            }
                                            else
                                            {
                                                SecondDose.Add(0);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("This combination of medications does not exist in the database");
                                }
                            }
                            //eidt here
                            if (enddate[y] == null)
                            {
                                LatestDate.Clear();
                                string latestdate = "SELECT TOP 1 * FROM Session_Table WHERE ID=" + ids[i] + " AND Medication='" + Secondary_MedBox.Text + "' AND Date>= '" + sdates[y] + "' ORDER BY Date DESC";
                                SqlCommand getlatestdate = new SqlCommand(latestdate, testregrescon);
                                SqlDataReader readlatesdate = getlatestdate.ExecuteReader();
                                while (readlatesdate.Read())
                                {
                                    LatestDate.Add(Convert.ToDateTime(readlatesdate["Date"]));
                                }
                                DateTime[] latest = LatestDate.ToArray();
                                MessageBox.Show(latest[0].ToString());
                                DateTime late = latest[0];
                                readlatesdate.Close();
                                string sotwo = "SELECT COUNT (*) FROM Session_Table WHERE ID=" + ids[i] + " AND Medication='" + Secondary_MedBox.Text + "' AND Date >= '" + sdates[y] + "'";
                                SqlCommand testsecond = new SqlCommand(sotwo, testregrescon);
                                existcount = (int)testsecond.ExecuteScalar();
                                if (existcount > 0)
                                {
                                    for (DateTime date = sdates[y]; date < late; date.AddDays(1))
                                    {
                                        string checkattr = "SELECT COUNT (*) FROM Session_Table WHERE ID=" + ids[i] + " AND Date='" + date + "'";
                                        SqlCommand checkattrcom = new SqlCommand(checkattr, testregrescon);
                                        checkatribute = (int)checkattrcom.ExecuteScalar();
                                        if (checkatribute > 0)
                                        {
                                            dateexistence(date.ToString(), Initial_MedBox.Text);
                                            if (dateexist == true)
                                            {
                                                string firstadd = "SELECT * FROM Session_Table WHERE ID=" + ids[i] + " AND Medication='" + Initial_MedBox.Text + "' AND Date >= '" + sdates[y] + "'";
                                                SqlCommand addfirst = new SqlCommand(firstadd, testregrescon);
                                                SqlDataReader readfirst = addfirst.ExecuteReader();
                                                while (readfirst.Read())
                                                {
                                                    InitialDose.Add(Convert.ToDouble(readfirst["Used_Amount"]));
                                                }

                                                readfirst.Close();
                                            }
                                            else
                                            {
                                                InitialDose.Add(0);
                                            }
                                            dateexistence(date.ToString(), Secondary_MedBox.Text);
                                            if (dateexist == true)
                                            {
                                                string secondadd = "SELECT * FROM Session_Table WHERE ID=" + ids[i] + " AND Medication='" + Secondary_MedBox.Text + "' AND Date >= '" + sdates[y] + "'";
                                                SqlCommand addsecond = new SqlCommand(secondadd, testregrescon);
                                                SqlDataReader readsecond = addsecond.ExecuteReader();
                                                while (readsecond.Read())
                                                {
                                                    SecondDose.Add(Convert.ToDouble(readsecond["Used_Amount"]));
                                                }
                                                readsecond.Close();
                                            }
                                            else
                                            {
                                                InitialDose.Add(0);
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("This combination of medications does not exist in the database");
                                }
                            }
                        }
                    }
                }
                
            }
        }

        public void fillrest(int num)
        {
            
            secondregtest.Open();
            DateTime[] datear = InitialDates.ToArray();
            if (num == 1)
            {
                for(int i=0; i<datear.Length; i++)
                {
                    string ho = datear[i].Year.ToString() + datear[i].Month.ToString() + datear[i].Day.ToString();
                    MessageBox.Show(ho);
                    dateexistence(ho, Secondary_MedBox.Text);
                    
                    if (dateexist != true)
                    {
                        SecondDose.Add(0);
                    }
                    if (dateexist == true)
                    {
                        int secondcount = dateexistcount;
                        int initialcount;
                        dateexistence(ho, Initial_MedBox.Text);
                        initialcount = dateexistcount;
                        string addlimit = "SELECT TOP "+secondcount+" * FROM Session_Table WHERE Medication='" + Secondary_MedBox.Text + "' ";
                        if (initialcount<= secondcount)
                        {
                             addlimit = "SELECT TOP "+initialcount+" * FROM Session_Table WHERE Medication='" + Secondary_MedBox.Text + "'";
                        }
                        int difference = initialcount - secondcount;
                        SqlCommand adddate = new SqlCommand(addlimit, secondregtest);
                        SqlDataReader readdates = adddate.ExecuteReader();
                        while (readdates.Read())
                        {
                            SecondDose.Add(Convert.ToDouble(readdates["Used_Amount"]));
                        }
                        if (difference > 0)
                        {
                            for(int y=0; y<difference; y++)
                            {
                                SecondDose.Add(0);
                            }
                        }
                    }
                }

            }
            if (num == 2)
            {

                for (int j = 0; j < datear.Length; j++)
                {
                    string ho = datear[j].Year.ToString() + datear[j].Month.ToString() + datear[j].Day.ToString(); dateexistence(ho, Secondary_MedBox.Text);
                    dateexistence(ho, Secondary_MedBox.Text);
                    if (dateexist != true)
                    {
                        SecondDose.Add(0);
                    }
                    if (dateexist == true)
                    {
                        int secondcount = dateexistcount;
                        int initialcount;
                        dateexistence(ho, Initial_MedBox.Text);
                        initialcount = dateexistcount;
                        string addlimit = "SELECT TOP " + secondcount + " * FROM Session_Table WHERE Medication='" + Secondary_MedBox.Text + "' ";
                        if (initialcount <= secondcount)
                        {
                            addlimit = "SELECT TOP " + initialcount + " * FROM Session_Table WHERE Medication='" + Secondary_MedBox.Text + "'";
                        }
                        int difference = initialcount - secondcount;
                        SqlCommand adddate = new SqlCommand(addlimit, secondregtest);
                        SqlDataReader readdates = adddate.ExecuteReader();
                        while (readdates.Read())
                        {
                            SecondDose.Add(Convert.ToDouble(readdates["Used_Amount"]));
                        }
                        if (difference > 0)
                        {
                            for (int y = 0; y < difference; y++)
                            {
                                SecondDose.Add(0);
                            }
                        }
                    }
                    dateexistence(ho, Tertiary_MedBox.Text);
                    if (dateexist != true)                            //LOOK HERE
                    {
                        OptionalDose.Add(0);
                    }
                    if (dateexist == true)
                    {
                        int thirdcount = dateexistcount;
                        dateexistence(ho, Initial_MedBox.Text);
                        int initialcount = dateexistcount;
                        string addlimit = "SELECT TOP " + thirdcount + " * FROM Session_Table WHERE Medication='" + Tertiary_MedBox.Text + "' ";
                        if (initialcount <= thirdcount)
                        {
                            addlimit = "SELECT TOP " + initialcount + " * FROM Session_Table WHERE Medication='" + Tertiary_MedBox.Text + "'";
                        }
                        int difference = initialcount - thirdcount;
                        SqlCommand adddateT = new SqlCommand(addlimit, secondregtest);
                        SqlDataReader readdates = adddateT.ExecuteReader();
                        while (readdates.Read())
                        {
                            OptionalDose.Add(Convert.ToDouble(readdates["Used_Amount"]));
                        }
                        if (difference > 0)
                        {
                            for (int y = 0; y < difference; y++)
                            {
                                OptionalDose.Add(0);
                            }
                        }
                    }

                }

            
            }
            secondregtest.Close();
        }

        private void Enter_Meds_Regression_Click(object sender, EventArgs e)
        {
            checkexist(Initial_MedBox.Text);
            if (existence != true)
            {
                MessageBox.Show("The Initial Medicine Doesn't Exist in The Database");
            }
            else
            {
                initialfill(Initial_MedBox.Text, "Quality_of_Sleep");
                checkexist(Secondary_MedBox.Text);
                if (existence != true)
                {
                    MessageBox.Show("The Secondary Medicine Doesn't Exist in The Database");
                }
                else
                {
                    if (!(string.IsNullOrWhiteSpace(Tertiary_MedBox.Text)))
                    {
                        checkexist(Tertiary_MedBox.Text);
                        if(existence!= true)
                        {
                            MessageBox.Show("The Tertiary Medicine Doesn't Exist in The Database");
                        }
                        else
                        {
                            testregrescon.Open();
                            checkcorrespondence(3);
                        }
                    }
                    else
                    {
                        testregrescon.Open();
                        checkcorrespondence(2);
                    }
                    //we go here
                    
                    double[] firstdose = InitialDose.ToArray();
                    double[] seconddose = SecondDose.ToArray();
                    double[] attributes = Attribute.ToArray();
                    double[] optionaldose = OptionalDose.ToArray();
                    MessageBox.Show(firstdose.Length.ToString());
                    MessageBox.Show(seconddose.Length.ToString());
                    MessageBox.Show(optionaldose.Length.ToString());
                    if (optionaldose.Length == 0)
                    {
                        double[,] doub = new double[firstdose.Length, 3];
                        for (int i = 0; i < firstdose.Length; i++)
                        {
                            doub[i, 0] = 1.0;
                            if (i == seconddose.Length - 1 && seconddose[i] == seconddose[i - 1])
                            {
                                doub[i, 1] = firstdose[i] + 0.1;
                            }
                            else
                            {
                                doub[i, 1] = firstdose[i];
                            }
                        }
                        for (int g = 0; g < seconddose.Length; g++)
                        {
                            if (g == seconddose.Length - 1 && seconddose[g] == seconddose[g - 1])
                            {
                                doub[g, 2] = seconddose[g] + 0.1;
                            }
                            else
                            {
                                doub[g, 2] = seconddose[g];
                            }
                        }
                        var A = DenseMatrix.OfArray(doub);

                        var b = new DenseVector(attributes);
                        var h = A.QR().Solve(b);
                        var q = h[0];
                        var q2 = h[1];
                        var q3 = h[2];
                        //    PUT GRAPH STUFF HERE
                    }
                    else
                    {
                        double[,] doub = new double[firstdose.Length, 4];
                        for (int i = 0; i < firstdose.Length; i++)
                        {
                            doub[i, 0] = 1.0;
                            if (i == seconddose.Length - 1 && seconddose[i] == seconddose[i - 1])
                            {
                                doub[i, 1] = firstdose[i] + 0.1;
                            }
                            else
                            {
                                doub[i, 1] = firstdose[i];
                            }
                        }
                        for (int g = 0; g < seconddose.Length; g++)
                        {
                            if (g == seconddose.Length - 1 && seconddose[g] == seconddose[g - 1])
                            {
                                doub[g, 2] = seconddose[g] + 0.1;
                            }
                            else
                            {
                                doub[g, 2] = seconddose[g];
                            }
                        }
                        for(int yo=0; yo<optionaldose.Length; yo++)
                        {
                            if (yo == seconddose.Length - 1 && seconddose[yo] == seconddose[yo - 1])
                            {
                                doub[yo, 3] = seconddose[yo] + 0.1;
                            }
                            else
                            {
                                doub[yo, 3] = seconddose[yo];
                            }
                        }
                        var A = DenseMatrix.OfArray(doub);

                        var b = new DenseVector(attributes);
                        var h = A.QR().Solve(b);
                        var q = h[0];
                        var q2 = h[1];
                        var q3 = h[2];
                        var q4 = h[4];
                        //    PUT GRAPH STUFF HERE
                    }
                    
                }
            }
        }
    }
}
