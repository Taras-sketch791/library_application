using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace biblioteka
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static SqlConnection GetConnection
        {
            get
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["biblioteka.Properties.Settings.BiblConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(ConnectionString);
                con.Open();
                return con;
            }
        }

        public static SqlConnection GetConnectio { get; internal set; }
    }
}
