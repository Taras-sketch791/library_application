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
    public partial class Start : MetroFramework.Forms.MetroForm
    {
        public Start()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con = Program.GetConnection;
            SqlDataReader dr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM пользователи WHERE логин='" + metroTextBox1.Text + "' AND пароль='" + Hash.GetHashString(metroTextBox2.Text) + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    this.Close();
                }

                else
                    MessageBox.Show("Проверьте правильность ввода логина или пароля!", "Данные введены неверно!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
                Application.Exit();
        }

        private void Start_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
                Application.Exit();
        }

        private void metroTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
                Application.Exit();
        }

        private void metroButton1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
                Application.Exit();
        }
    }
}
