
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PesertaLombaKaraoke
{
    public partial class Form1 : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
        "Data Source=LAPTOP-QN8CHOOI\\GHOFIQ;Initial Catalog=db_lomba_karaoke;Integrated Security=True";
        public Form1()
        {

            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        
    }

}
