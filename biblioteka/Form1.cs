using Microsoft.Office.Interop.Excel;
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
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
     
        public Form1()
        {
            InitializeComponent();
        }


        public void LoadData()
        {
            SqlConnection myConnection1 = Program.GetConnection;
            try
            {
                string sqlQuery = @"SELECT dbo.[История выдачи1].id_истории AS ID, dbo.Читатели.ФИО AS Читатель, dbo.Читатели.адрес AS Адрес, dbo.Читатели.телефон AS Телефон, dbo.Авторы1.ФИО AS Автор, dbo.Книги1.название AS Книга, 
                         dbo.Жанры.название_жанра AS Жанр, dbo.[История выдачи1].дата_выдачи AS [Дата выдачи], dbo.[История выдачи1].дата_возврата AS [Дата возврата]
                         FROM dbo.[История выдачи1] INNER JOIN
                         dbo.Читатели ON dbo.[История выдачи1].id_читателя = dbo.Читатели.id_читателя INNER JOIN
                         dbo.Книги1 ON dbo.[История выдачи1].id_книги = dbo.Книги1.id_книги INNER JOIN
                         dbo.Авторы1 ON dbo.Книги1.id_авторы = dbo.Авторы1.id_автора INNER JOIN
                         dbo.Жанры ON dbo.Книги1.id_жанры = dbo.Жанры.id_жанра";

                DataSet ds = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlQuery, myConnection1);
                dataAdapter.Fill(ds, "История");
                dataGridView1.DataSource = ds.Tables["История"];

                myConnection1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM.dd.yyyy";
            //dateTimePicker2.Format = DateTimePickerFormat.Custom;
            //dateTimePicker2.CustomFormat = "MM.dd.yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "MM.dd.yyyy";
            metroCheckBox1.Checked = true;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "biblDataSet4.Книги1". При необходимости она может быть перемещена или удалена.
            this.книги1TableAdapter.Fill(this.biblDataSet4.Книги1);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "biblDataSet4.Читатели". При необходимости она может быть перемещена или удалена.
            this.читателиTableAdapter.Fill(this.biblDataSet4.Читатели);
            Start login = new Start();
            login.ShowDialog();

            SqlConnection myConnection = Program.GetConnection;
            string sqlQuery = @"SELECT ID_роли FROM пользователи WHERE логин = '" + login.metroTextBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(sqlQuery, myConnection);
            Int32 count = (Int32)cmd.ExecuteScalar();
            myConnection.Close();

            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Tick += (o, t) => { toolStripLabel2.Text = "Текущая дата: " + DateTime.Now.ToString(); };
            timer1.Start();

            toolStripLabel1.Text = "Вход выполнен под именем: " + login.metroTextBox1.Text;

            if (count == 1)
            {
                toolStripLabel3.Text = "Права: Администратор";
            }
            else
            {
                администрированиеToolStripMenuItem.Enabled = false;                
            }

            if (count == 2)
            {
                toolStripLabel3.Text = "Права: Библиотекарь";
            }

            LoadData();
        }

        private void администрированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (Admin ad = new Admin())
                ad.ShowDialog();
            Show();
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_Load(this, new EventArgs());
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void жанрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (Zhanry zh = new Zhanry())
                zh.ShowDialog();
            Show();
        }

        private void авторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (Avtor av = new Avtor())
                av.ShowDialog();
            Show();
        }

        private void читателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (Chit ch = new Chit())
                ch.ShowDialog();
            Show();
        }

        private void книгиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (Books bk = new Books())
                bk.ShowDialog();
            Show();
        }

        

     

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            SqlConnection myConnection = Program.GetConnection;
            try
            {
                if (metroCheckBox1.Checked)
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[История выдачи1] ([id_читателя],[id_книги],[дата_выдачи],[дата_возврата]) 
                                                VALUES('" + metroComboBox1.SelectedValue + "', '" + metroComboBox2.SelectedValue + "',  '" + dateTimePicker1.Text + "', '" + dateTimePicker4.Text + "')", myConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Информация добавлена!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    myConnection.Close();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO [История выдачи1] (id_читателя, id_книги, дата_выдачи) 
                                                VALUES('" + metroComboBox1.SelectedValue + "', '" + metroComboBox2.SelectedValue + "',  '" + dateTimePicker1.Text + "')", myConnection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Информация добавлена!", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    myConnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            LoadData();
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (metroCheckBox1.Checked)
            {
                dateTimePicker4.Enabled = true;
            }
            else
            {
                dateTimePicker4.Enabled = false;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            SqlConnection myConnection = Program.GetConnection;
            if (metroCheckBox1.Checked)
            {
                string queryString = "UPDATE [История выдачи1] SET id_читателя = '" + metroComboBox1.SelectedValue +
            "', id_книги = '" + metroComboBox2.SelectedValue +
            "', дата_выдачи = '" + dateTimePicker1.Text + "', дата_возврата  = '" + dateTimePicker4.Text + "' WHERE id_истории = " + metroLabel8.Text;
                SqlCommand cmdSel = new SqlCommand(queryString, myConnection);
                cmdSel.ExecuteNonQuery();
            }
            else
            {
                string queryString = "UPDATE [История выдачи1] SET id_читателя ='" + metroComboBox1.SelectedValue +
            "', id_книги = '" + metroComboBox2.SelectedValue +
            "', дата_выдачи = '" + dateTimePicker1.Text +
            "' WHERE id_истории = " + metroLabel8.Text;
                SqlCommand cmdSel = new SqlCommand(queryString, myConnection);
                cmdSel.ExecuteNonQuery();
            }           
            
            MessageBox.Show("Данные были изменены!", "Изменение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            metroLabel8.Text = row.Cells[0].Value.ToString();
            metroComboBox1.Text = row.Cells[1].Value.ToString();
            metroComboBox2.Text = row.Cells[5].Value.ToString();
            dateTimePicker1.Text = row.Cells[7].Value.ToString();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            string TextCommand = "DELETE FROM [История выдачи1] WHERE id_истории = " + metroLabel8.Text;

            SqlConnection myConnection = Program.GetConnection;
            SqlCommand Command = new SqlCommand(TextCommand, myConnection);
            Command.ExecuteNonQuery();
            myConnection.Close();
            MessageBox.Show("Информация удалена!", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            BindingSource source1 = new BindingSource();
            source1.DataSource = dataGridView1.DataSource;
            if (metroComboBox5.SelectedIndex == 0)
            {               
                source1.Filter = "Читатель LIKE '%" + metroTextBox1.Text + "%'";

            }
            else if (metroComboBox5.SelectedIndex == 1)
            {                
                source1.Filter = "Книга LIKE '%" + metroTextBox1.Text + "%'";
            }
            else if (metroComboBox5.SelectedIndex == 2)
            {
                source1.Filter = string.Format("[Дата выдачи] = '" + dateTimePicker2.Text + "'");
            }
            else if (metroComboBox5.SelectedIndex == 3)
            {
                source1.Filter = string.Format("[Дата возврата] = '" + dateTimePicker2.Text + "'");
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application Exl = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb;

            XlReferenceStyle RefStyle = Exl.ReferenceStyle;
            Exl.Visible = true;

            String TemplatePath = System.Windows.Forms.Application.StartupPath + @"\Print.xltx";
            try
            {
                wb = Exl.Workbooks.Add(TemplatePath);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Не удалось загрузить шаблон для экспорта " + TemplatePath + "\n" + ex.Message);
            }
            Worksheet ws = wb.Worksheets.get_Item(1) as Worksheet;
            for (int j = 0; j < dataGridView1.Columns.Count; ++j)
            {
                (ws.Cells[2, j + 1] as Microsoft.Office.Interop.Excel.Range).Value2 = dataGridView1.Columns[j].HeaderText;
                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    object Val = dataGridView1.Rows[i].Cells[j].Value;
                    if (Val != null)
                        (ws.Cells[i + 3, j + 1] as Microsoft.Office.Interop.Excel.Range).Value2 = Val.ToString();
                }
            }
            ws.Columns.EntireColumn.AutoFit();
            Exl.ReferenceStyle = RefStyle;
        }
    }
}
