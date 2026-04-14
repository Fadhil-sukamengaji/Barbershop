using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Barbershop
{
    public partial class Form1 : Form
    {
 
        string connectionString = @"Data Source=PADILSU\PADIL;Initial Catalog=DBBarbershop;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshTable();
            LoadComboBoxes();
        }

        private void RefreshTable()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT R.id_reservasi, R.nama_pelanggan, L.nama_layanan, 
                                 C.nama AS Capster, J.hari, R.status_reservasi 
                                 FROM Reservasi R
                                 JOIN Layanan L ON R.id_layanan = L.id_layanan
                                 JOIN Capster C ON R.id_capster = C.id_capster
                                 JOIN Jadwal J ON R.id_jadwal = J.id_jadwal";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void LoadComboBoxes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string queryLayanan = "SELECT id_layanan, " +
                                      "nama_layanan + ' - Rp. ' + CAST(CAST(harga AS DECIMAL(10,0)) AS VARCHAR) AS LayananHarga " +
                                      "FROM Layanan";

                SqlDataAdapter daL = new SqlDataAdapter(queryLayanan, conn);
                DataTable dtL = new DataTable();
                daL.Fill(dtL);

                comboBoxLayanan.DataSource = dtL;
                comboBoxLayanan.DisplayMember = "LayananHarga";
                comboBoxLayanan.ValueMember = "id_layanan";

                SqlDataAdapter daC = new SqlDataAdapter("SELECT id_capster, nama FROM Capster", conn);
                DataTable dtC = new DataTable(); daC.Fill(dtC);
                comboBoxCapster.DataSource = dtC;
                comboBoxCapster.DisplayMember = "nama";
                comboBoxCapster.ValueMember = "id_capster";

                SqlDataAdapter daJ = new SqlDataAdapter("SELECT id_jadwal, hari FROM Jadwal WHERE status_jadwal = 'Tersedia'", conn);
                DataTable dtJ = new DataTable(); daJ.Fill(dtJ);
                comboBoxJadwal.DataSource = dtJ;
                comboBoxJadwal.DisplayMember = "hari";
                comboBoxJadwal.ValueMember = "id_jadwal";
            }
        }


    }
}