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
    public partial class Short_Single_Medicine_List : Form
    {
        public Short_Single_Medicine_List()
        {
            InitializeComponent();
            filllist();
            makegrid();
        }
        public static SqlConnection patcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        public List<string> Medications = new List<string>();
        public void filllist()
        {
            Medications.Clear();
            patcon.Open();
            string comm = "SELECT * FROM Patient_Medication_Historty ";
            SqlCommand noe = new SqlCommand(comm, patcon);
            SqlDataReader rodo = noe.ExecuteReader();
            while (rodo.Read())
            {
                if (!(Medications.Contains(Convert.ToString(rodo["Medication"]))))
                {
                    Medications.Add(Convert.ToString(rodo["Medication"]));
                }
            }
            patcon.Close();
        }
        DataTable datatab = new DataTable();
        public void makegrid()
        {
            var column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Medication";
            datatab.Columns.Add(column);
            
            string[] mednames = Medications.ToArray();
            for(int i=0; i<mednames.Length; i++)
            {
                DataRow maboi = datatab.NewRow();
                maboi["Medication"] = mednames[i];
                datatab.Rows.Add(maboi);
            }
            MedDataG.DataSource = datatab;
        }
    }
}
