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
    public partial class Admin : MetroFramework.Forms.MetroForm
    {
        public Admin()
        {
            InitializeComponent();
        }

        SqlConnection myConnection = Program.GetConnection;
        void LoadData()
        {

            try
            {
                string sqlQuery = @"SELECT dbo.пользователи.id_пользователя AS ID, dbo.пользователи.логин AS Логин, dbo.пользователи.пароль AS [Hash-пароля], dbo.роли.наименование AS [Права доступа]
                         FROM dbo.пользователи INNER JOIN
                         dbo.роли ON dbo.пользователи.ID_роли = dbo.роли.ID_роли";
                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, myConnection);
                dataAdapter.Fill(ds, "Пользователи");
                dataGridView1.DataSource = ds.Tables["Пользователи"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO пользователи (логин, пароль, ID_роли)
                VALUES ('" + metroTextBox1.Text + "', '" + Hash.GetHashString(metroTextBox2.Text)
                + "','" + metroComboBox1.SelectedValue + "')", myConnection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Информация добавлена!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
            metroTextBox1.Text = null;
            metroTextBox2.Text = null;
        }

        private void Admin_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "biblDataSet.роли". При необходимости она может быть перемещена или удалена.
            this.ролиTableAdapter.Fill(this.biblDataSet.роли);
            LoadData();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string TextCommand = "DELETE FROM пользователи WHERE id_пользователя = " + metroLabel4.Text;

            SqlConnection myConnection = Program.GetConnection;
            SqlCommand Command = new SqlCommand(TextCommand, myConnection);
            Command.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Пользователь удален!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            myConnection.Close();
            metroTextBox1.Text = null;
            metroTextBox2.Text = null;
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            metroLabel4.Text = row.Cells[0].Value.ToString();
            metroTextBox1.Text = row.Cells[1].Value.ToString();
            metroTextBox2.Text = row.Cells[2].Value.ToString();            
            metroComboBox1.Text = row.Cells[3].Value.ToString();
        }
    }
}
