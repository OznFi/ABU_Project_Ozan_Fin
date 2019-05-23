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
    public partial class Entrance : Form
    {
        public Entrance()
        {
            InitializeComponent();
        }
        public static SqlConnection logcon = new SqlConnection("Data Source=DESKTOP-70RCCP5\\SQLEXPRESS;Initial Catalog= Alternative Medicine;Integrated Security=True");
        string logid;
        string logpas;
        private void LoginButton_Click(object sender, EventArgs e)
        {
            string j = "SELECT COUNT(*) FROM Users WHERE (Login_ID= @Login_ID)";
            string n = "SELECT COUNT(*) FROM Users WHERE (Login_Password = @Login_Password)";
            logid = ID_LoginBox.Text;
            logpas = Password_LoginBox.Text;
            logcon.Open();
            SqlCommand logcom = new SqlCommand(j, logcon);
            SqlCommand logpascom = new SqlCommand(n, logcon);
            logcom.Parameters.AddWithValue("@Login_ID", logid);
            logpascom.Parameters.AddWithValue("@Login_Password", logpas);
            int UserExist = (int)logcom.ExecuteScalar();
            int passexist = (int)logpascom.ExecuteScalar();
            if (UserExist > 0 && passexist > 0)
            {
                LoginLabel.Text = ID_LoginBox.Text;
                this.Hide();
                Choice ch = new Choice(LoginLabel.Text);
                ch.Show();
            }
            else
            {
                ID_LoginBox.Text = "WRONG";
                Password_LoginBox.Text = "";
            }
            logcon.Close();
        }
    }
}
