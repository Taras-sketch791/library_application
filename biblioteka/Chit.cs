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
    public partial class Chit : MetroFramework.Forms.MetroForm
    {
        public Chit()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            SqlConnection myConnection1 = Program.GetConnection;
            try
            {
                string sqlQuery = @"SELECT id_читателя AS ID, ФИО, адрес AS Адрес, телефон AS Телефон
                FROM dbo.Читатели";

                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, myConnection1);
                dataAdapter.Fill(ds, "Читатели");
                dataGridView1.DataSource = ds.Tables["Читатели"];

                myConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            metroTextBox1.Text = string.Empty; ;
            metroLabel3.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void Chit_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = Program.GetConnection;
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Читатели (ФИО, адрес, телефон) 
                                                VALUES('" + metroTextBox1.Text + "', '" + metroTextBox2.Text + "', '" + metroTextBox3.Text + "')", myConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Информация добавлена!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            metroTextBox1.Text = string.Empty;
            metroLabel3.Text = string.Empty;
            LoadData();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string TextCommand = "DELETE FROM читатели WHERE id_читателя = " + metroLabel3.Text;

            SqlConnection myConnection = Program.GetConnection;
            SqlCommand Command = new SqlCommand(TextCommand, myConnection);
            Command.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Читатель удален!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            myConnection.Close();
            metroTextBox1.Text = null;
            metroTextBox2.Text = null;
            LoadData();
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            metroLabel3.Text = row.Cells[0].Value.ToString();
            metroTextBox1.Text = row.Cells[1].Value.ToString();
            metroTextBox2.Text = row.Cells[2].Value.ToString();
            metroTextBox3.Text = row.Cells[3].Value.ToString();
        }
    }
}
