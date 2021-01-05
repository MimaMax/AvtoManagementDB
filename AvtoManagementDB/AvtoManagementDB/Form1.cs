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

namespace AvtoManagementDB
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            Application.Exit();
        }

        private void зарегистрироватьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form signup = new signup();
            signup.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\VisualStudio\VBprojects\AvtoManagementDB\AvtoManagementDB\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Cars]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "        " + Convert.ToString(sqlReader["Photo"]) + "        " + Convert.ToString(sqlReader["Model"]) + "        " + Convert.ToString(sqlReader["Transmission"]) + "        " + Convert.ToString(sqlReader["Year"]) + "        " + Convert.ToString(sqlReader["Mileage"]) + "        " + Convert.ToString(sqlReader["Number"]) + "        " + Convert.ToString(sqlReader["Info"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void войтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form Login = new Login();
            Login.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iselected = listBox1.SelectedItem.ToString();
            string iID;
            int k;
            k = iselected.IndexOf(' ');
            iID = iselected.Substring(0, k);
            label13.Text = iID;

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Cars] WHERE [id]=@iID", sqlConnection);

            command.Parameters.AddWithValue("iID", label13.Text);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    label2.Text = (Convert.ToString(sqlReader["Model"]));
                    label4.Text = (Convert.ToString(sqlReader["Transmission"]));
                    label6.Text = (Convert.ToString(sqlReader["Year"]));
                    label8.Text = (Convert.ToString(sqlReader["Mileage"])) + "000 км";
                    label10.Text = (Convert.ToString(sqlReader["Number"])) + " 174 РУ";
                    label12.Text = (Convert.ToString(sqlReader["Info"]));
                    if (label12.Text == "Busy")
                    {
                        label12.Text = "Занят";
                        label12.ForeColor = Color.Red;
                    }
                    else
                    {
                        label12.Text = "Свободен";
                        label12.ForeColor = Color.Green;
                    }
                    if (label4.Text == "М         ")
                    {
                        label4.Text = "Механическая";                        
                    }
                    else
                    {
                        label4.Text = "Автоматическая";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
    }
}
