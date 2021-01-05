using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AvtoManagementDB.Properties;

namespace AvtoManagementDB
{
    public partial class Login : Form
    {
        bool vis = true;
        SqlConnection sqlConnection;

        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (vis)
            {
                textBox2.UseSystemPasswordChar = false;
                vis = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                vis = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                Settings.Default["LoginSave"] = textBox1.Text.Trim();
                Settings.Default.Save();

                string query = "SELECT * FROM Users WHERE Login ='" + textBox1.Text.Trim() + "' AND Password = '" + textBox2.Text.Trim() + "'";
                string queryroot = "SELECT * FROM Users WHERE Login ='" + textBox1.Text.Trim() + "' AND Password = '" + textBox2.Text.Trim() + "' AND Status = 'root'";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlConnection);
                SqlDataAdapter sdaroot = new SqlDataAdapter(queryroot, sqlConnection);
                DataTable dtb1 = new DataTable();
                DataTable dtb2 = new DataTable();
                sda.Fill(dtb1);
                sdaroot.Fill(dtb2);
                if (dtb2.Rows.Count == 1)
                {                    
                    Close();
                    Form Form1Root = new Form1Root();
                    Form1Root.Show();
                }
                else { 
                    if (dtb1.Rows.Count == 1)
                    {
                        Close();
                        Form Form1Entered = new Form1Entered();
                        Form1Entered.Show();
                    }
                    else
                    {
                        label4.Visible = true;
                        label4.Text = "Такой логин или пароль не найдены, проверьте правильность введеных данных";
                    }
                }
            } else
            {
                label4.Visible = true;
                label4.Text = "Необходимо заполнить все поля!";
            }
        }

        private async void Login_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\VisualStudio\VBprojects\AvtoManagementDB\AvtoManagementDB\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            Form1 Form1 = new Form1();
            Form1.Visible = true;
        }
    }
}
