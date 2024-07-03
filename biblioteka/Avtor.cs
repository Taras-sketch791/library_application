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
    public partial class Avtor : MetroFramework.Forms.MetroForm
    {
        public Avtor()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            SqlConnection myConnection1 = Program.GetConnection;
            try
            {
                string sqlQuery = @"SELECT        id_автора AS ID, ФИО AS [ФИО автора]
FROM            dbo.Авторы1";

                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, myConnection1);
                dataAdapter.Fill(ds, "Авторы1");
                dataGridView1.DataSource = ds.Tables["Авторы1"];

                myConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            metroTextBox1.Text = string.Empty; ;
            metroLabel2.Text = string.Empty;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            metroLabel2.Text = row.Cells[0].Value.ToString();
            metroTextBox1.Text = row.Cells[1].Value.ToString();

        }

        private void Avtor_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SqlConnection myConnection = Program.GetConnection;
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Авторы1 (ФИО) 
                                                VALUES('" + metroTextBox1.Text + "')", myConnection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Информация добавлена!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            metroTextBox1.Text = string.Empty;
            metroLabel2.Text = string.Empty;
            LoadData();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string TextCommand = "DELETE FROM Авторы1 WHERE id_автора  = " + metroLabel2.Text;

            SqlConnection myConnection = Program.GetConnection;
            SqlCommand Command = new SqlCommand(TextCommand, myConnection);
            Command.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Информация удалена!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
