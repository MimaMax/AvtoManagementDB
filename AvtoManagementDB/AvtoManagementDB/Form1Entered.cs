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
    public partial class Form1Entered : Form
    {
        SqlConnection sqlConnection;

        public Form1Entered()
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
            Form signup = new signup();
            signup.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            label15.Text = Settings.Default["LoginSave"].ToString();

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
            Form Login = new Login();
            Login.ShowDialog();
        }

        private void авторизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы точно хотите выйти из аккаунта?",
                "Подтвердите выход",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                Close();
                Form Form1 = new Form1();
                Form1.Visible = true;
            }
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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label12.Text == "Свободен")
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Orders] (Login, Model) VALUES (@Login, @Model)", sqlConnection);

                command.Parameters.AddWithValue("Login", label15.Text);
                command.Parameters.AddWithValue("Model", label2.Text);

                await command.ExecuteNonQueryAsync();

                label17.Visible = true;
                label17.Text = "Мы приняли вашу заявку, перезвоним вам в ближайшее время";

                button1.Visible = false;
            }
            else
            {
                MessageBox.Show("Данная машина занята, выберите другую", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
