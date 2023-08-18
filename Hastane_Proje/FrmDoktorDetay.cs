using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace Hastane_Proje
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fd = new FrmDuyurular();
            fd.Show();
        }

        public string tc;
        SqlBaglantisi bagla = new SqlBaglantisi();
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            // Doktorun tcsini diğer formdan çekme işlemi.
            lblTC.Text = tc;

            //Doktor ad ve soyadını çekme
            SqlCommand komut = new SqlCommand("select doktorAd,doktorSoyad from Tbl_Doktorlar where doktorTc=@p1", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bagla.baglanti().Close();

            //Doktora ait randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Randevular where randevuDoktor='" + lblAdSoyad.Text + "'", bagla.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bagla.baglanti().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle fr = new FrmDoktorBilgiDuzenle();
            fr.tcno = lblTC.Text;
            fr.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
