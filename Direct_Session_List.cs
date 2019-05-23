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
    public partial class Direct_Session_List : Form
    {
        //THIS FORM IS NOT IN USE ANYMORE!!!

        public Direct_Session_List(int id)
        {
            InitializeComponent();
            car = id;
            fillgrid();
        }
        public static SqlConnection dirccon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        int car;
        public void fillgrid()
        {
            dirccon.Open();
            string d = "SELECT * FROM Patient_Medication_Historty WHERE ID=" + car+" AND End_Date IS NULL ORDER BY Starting_Date";
            SqlCommand dircomm = new SqlCommand(d, dirccon);
            dircomm.Parameters.AddWithValue("@ID", car);
            DataSet det = new DataSet();
            SqlDataAdapter diradp = new SqlDataAdapter(d, dirccon);
            diradp.Fill(det, "All_Sessions");
            All_Session_Grid.DataSource = det.Tables[0];
            dirccon.Close();
        }
        int selected;
        string id;
        object data1;
        object data2;
        string med;
        string passdate;
        private void All_Session_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selected = e.RowIndex;
            DataGridViewRow row = new DataGridViewRow();
            row = All_Session_Grid.Rows[selected];
            if (e.ColumnIndex == 0)
            {
                data1 = row.Cells[2].Value;
                data2 = row.Cells[1].Value;
                passdate = Convert.ToString(data1);
                med = Convert.ToString(data2);
                id = Convert.ToString(car);
                View_Session__Notes note = new View_Session__Notes(id, med, passdate);
                note.Show();
            }
        }
        string car2;
        private void All_Graph_But_Click(object sender, EventArgs e)
        {
            car2 = Convert.ToString(car);
            All_Sessions_Graph ga = new All_Sessions_Graph(car2);
            this.Hide();
            ga.Show();
        }
    }
}
