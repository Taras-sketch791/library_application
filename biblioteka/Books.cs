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

namespace biblioteka
{
    public partial class Books : MetroFramework.Forms.MetroForm
    {
        public Books()
        {
            InitializeComponent();
        }


        public void LoadData()
        {
            SqlConnection myConnection1 = Program.GetConnection;
            try
            {
                string sqlQuery = @"SELECT dbo.Книги1.id_книги AS ID, dbo.Авторы1.ФИО AS Автор, dbo.Книги1.название AS Книга, dbo.Жанры.название_жанра AS Жанр, dbo.Книги1.год_издания AS [Дата издания], 
                         dbo.Книги1.количество_экземпляров AS [Кол-во экземпляров]
                         FROM dbo.Книги1 INNER JOIN
                         dbo.Авторы1 ON dbo.Книги1.id_авторы = dbo.Авторы1.id_автора INNER JOIN
                         dbo.Жанры ON dbo.Книги1.id_жанры = dbo.Жанры.id_жанра";

                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, myConnection1);
                dataAdapter.Fill(ds, "Книги1");
                dataGridView1.DataSource = ds.Tables["Книги1"];

                myConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = Program.GetConnection;
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Книги1 (название, id_авторы, id_жанры, [год_издания], количество_экземпляров) 
                                                VALUES('" + metroTextBox1.Text + "', '" + metroComboBox1.SelectedValue + "', '" + metroComboBox2.SelectedValue + "', '" + dateTimePicker1.Text + "', '" + metroTextBox4.Text + "')", myConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Информация добавлена!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            metroTextBox4.Text = string.Empty;
            metroTextBox1.Text = string.Empty;
            LoadData();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            metroLabel5.Text = row.Cells[0].Value.ToString();
            metroTextBox1.Text = row.Cells[2].Value.ToString();
            metroComboBox1.Text = row.Cells[1].Value.ToString();
            metroComboBox2.Text = row.Cells[3].Value.ToString();
            dateTimePicker1.Text = row.Cells[4].Value.ToString();
            metroTextBox4.Text = row.Cells[5].Value.ToString();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string TextCommand = "DELETE FROM Книги1 WHERE id_книги  = " + metroLabel5.Text;

            SqlConnection myConnection = Program.GetConnection;
            SqlCommand Command = new SqlCommand(TextCommand, myConnection);
            Command.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Информация удалена!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            metroTextBox4.Text = string.Empty;
            metroTextBox1.Text = string.Empty;
            LoadData();
        }

        private void Books_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "biblDataSet4.Жанры". При необходимости она может быть перемещена или удалена.
            this.жанрыTableAdapter.Fill(this.biblDataSet4.Жанры);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "biblDataSet4.Авторы1". При необходимости она может быть перемещена или удалена.
            this.авторы1TableAdapter.Fill(this.biblDataSet4.Авторы1);
            LoadData();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM.dd.yyyy";

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = Program.GetConnection;
            string queryString = "UPDATE [Книги1] SET название ='" + metroTextBox1.Text +
            "', id_авторы = '" + metroComboBox1.SelectedValue + "', id_жанры = '" + metroComboBox2.SelectedValue +
            "', [год_издания]= '" + dateTimePicker1.Text +
            "', количество_экземпляров  = '" + metroTextBox4.Text + "' WHERE id_книги = " + metroLabel5.Text;
            SqlCommand cmdSel = new SqlCommand(queryString, myConnection);
            cmdSel.ExecuteNonQuery();
            MessageBox.Show("Данные были изменены!", "Изменение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            metroTextBox4.Text = string.Empty;
            metroTextBox1.Text = string.Empty;
            LoadData();
        }
    }
}
