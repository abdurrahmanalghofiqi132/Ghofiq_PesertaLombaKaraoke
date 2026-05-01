
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

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                MessageBox.Show("Koneksi berhasil!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal: " + ex.Message);
            }
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();

                dataGridView1.Columns.Add("nomor_urut", "Nomor");
                dataGridView1.Columns.Add("nama_peserta", "Nama");
                dataGridView1.Columns.Add("asal_daerah", "Asal");
                dataGridView1.Columns.Add("kategori", "Kategori");

                string query = @"SELECT p.nomor_urut, p.nama_peserta, p.asal_daerah, k.nama_kategori
                         FROM tb_peserta p
                         JOIN tb_kategori k ON p.id_kategori = k.id_kategori";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["nomor_urut"],
                        reader["nama_peserta"],
                        reader["asal_daerah"],
                        reader["nama_kategori"]
                    );
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"INSERT INTO tb_peserta
                        (nomor_urut, nama_peserta, asal_daerah, id_kategori, id_panitia)
                        VALUES
                        (@nomor, @nama, @asal,
                        (SELECT id_kategori FROM tb_kategori WHERE nama_kategori=@kategori),
                        (SELECT id_panitia FROM tb_panitia WHERE nama_panitia=@panitia))";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nomor", txtNomorUrut.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@asal", txtAsal.Text);
                cmd.Parameters.AddWithValue("@kategori", cmbKategori.Text);
                cmd.Parameters.AddWithValue("@panitia", cmbPanitia.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data berhasil ditambahkan");
                btnLoad_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"UPDATE tb_peserta
                        SET nama_peserta=@nama,
                            asal_daerah=@asal
                        WHERE nomor_urut=@nomor";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@nomor", txtNomorUrut.Text);
                cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@asal", txtAsal.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data berhasil diupdate");
                btnLoad_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }

}
