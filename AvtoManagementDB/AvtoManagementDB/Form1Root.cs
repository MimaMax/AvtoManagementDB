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
    public partial class Form1Root : Form
    {
        SqlConnection sqlConnection;

        public Form1Root()
        {
            InitializeComponent();
        }

        private void войтиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            Application.Exit();
        }

        private void Form1Root_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void Form1Root_Load(object sender, EventArgs e)
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

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
            {
                label7.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text) &&
                !string.IsNullOrEmpty(maskedTextBox1.Text) && !string.IsNullOrWhiteSpace(maskedTextBox1.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {

                SqlCommand command = new SqlCommand("INSERT INTO [Cars] (Model, Transmission, Year, Mileage, Number, Info)VALUES(@Model, @Transmission, CONVERT(DATE, @Year,105), @Mileage, @Number, @Info)", sqlConnection);

                command.Parameters.AddWithValue("Model", textBox1.Text);
                command.Parameters.AddWithValue("Transmission", comboBox1.Text);
                command.Parameters.AddWithValue("Year", maskedTextBox1.Text);
                command.Parameters.AddWithValue("Mileage", textBox4.Text);
                command.Parameters.AddWithValue("Number", textBox5.Text);
                command.Parameters.AddWithValue("Info", textBox6.Text);

                await command.ExecuteNonQueryAsync();

                label7.Visible = true;
                label7.ForeColor = Color.Green;
                label7.Text = "Автомобиль успешно добавлен в таблицу";

                textBox1.Text = "";
                comboBox1.Text = "";
                maskedTextBox1.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
            else
            {
                label7.Visible = true;
                label7.ForeColor = Color.Red;
                label7.Text = "Необходимо заполнить все поля!";
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

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

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label8.Visible)
            {
                label8.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {

                SqlCommand command = new SqlCommand("UPDATE [Cars] SET [Transmission]=@Transmission, [Mileage]=@Mileage, [Number]=@Number, [Info]=@Info WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox8.Text);
                command.Parameters.AddWithValue("Transmission", comboBox2.Text);
                command.Parameters.AddWithValue("Mileage", textBox7.Text);
                command.Parameters.AddWithValue("Number", textBox3.Text);
                command.Parameters.AddWithValue("Info", textBox2.Text);

                await command.ExecuteNonQueryAsync();

                label8.Visible = true;
                label8.ForeColor = Color.Green;
                label8.Text = "Информация об автомобиле успешно изменена";

                textBox8.Text = "";
                comboBox2.Text = "";
                textBox7.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
            }
            else if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                label8.Visible = true;
                label8.ForeColor = Color.Red;
                label8.Text = "Необходимо заполнить все поля!";
                
            }
            else
            {
                label8.Visible = true;
                label8.ForeColor = Color.Red;
                label8.Text = "В поле ID введите ID машины, информацию о которой вы пытаетесь заменить";
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label15.Visible)
            {
                label15.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
            {

                SqlCommand command = new SqlCommand("DELETE FROM [Cars] WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox9.Text);

                await command.ExecuteNonQueryAsync();

                label15.Visible = true;
                label15.ForeColor = Color.Green;
                label15.Text = "Удаление прошло успешно";

                textBox9.Text = "";
            }
            else
            {
                label15.Visible = true;
                label15.ForeColor = Color.Red;
                label15.Text = "Введите ID машины, которую хотите удалить";
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private async void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            listBox1.Items.Clear();

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

            listBox2.Items.Clear();

            SqlCommand command2 = new SqlCommand("SELECT * FROM [Orders]", sqlConnection);

            try
            {
                sqlReader = await command2.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox2.Items.Add(Convert.ToString(sqlReader["Id"]) + "        " + Convert.ToString(sqlReader["Login"]) + "        " + Convert.ToString(sqlReader["Model"]));
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

            listBox3.Items.Clear();

            SqlCommand command3 = new SqlCommand("SELECT * FROM [Users]", sqlConnection);

            try
            {
                sqlReader = await command3.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox3.Items.Add(Convert.ToString(sqlReader["Id"]) + "        " + Convert.ToString(sqlReader["Login"]) + "        " + Convert.ToString(sqlReader["FIO"]) + "        " + Convert.ToString(sqlReader["Birth"]) + "        " + Convert.ToString(sqlReader["Phone"]) + "        " + Convert.ToString(sqlReader["Email"]) + "        " + Convert.ToString(sqlReader["Address"]) + "        " + Convert.ToString(sqlReader["Passport"]) + "        " + Convert.ToString(sqlReader["Driverpass"]) + "        " + Convert.ToString(sqlReader["Exp"]));
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

        private async void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iselected = listBox1.SelectedItem.ToString();
            string iID;
            int k;
            k = iselected.IndexOf(' ');
            iID = iselected.Substring(0, k);
            label26.Text = iID;

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Cars] WHERE [id]=@iID", sqlConnection);

            command.Parameters.AddWithValue("iID", label26.Text);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    label27.Text = (Convert.ToString(sqlReader["Model"]));
                    label24.Text = (Convert.ToString(sqlReader["Transmission"]));
                    label22.Text = (Convert.ToString(sqlReader["Year"]));
                    label20.Text = (Convert.ToString(sqlReader["Mileage"])) + "000 км";
                    label18.Text = (Convert.ToString(sqlReader["Number"])) + " 174 РУ";
                    label16.Text = (Convert.ToString(sqlReader["Info"]));
                    if (label16.Text == "Busy")
                    {
                        label16.Text = "Занят";
                        label16.ForeColor = Color.Red;
                    }
                    else
                    {
                        label16.Text = "Свободен";
                        label16.ForeColor = Color.Green;
                    }
                    if (label24.Text == "М         ")
                    {
                        label24.Text = "Механическая";
                    }
                    else
                    {
                        label24.Text = "Автоматическая";
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
