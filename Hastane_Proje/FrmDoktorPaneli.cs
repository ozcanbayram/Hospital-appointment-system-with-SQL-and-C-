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

namespace Hastane_Proje
{
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        SqlBaglantisi baglan = new SqlBaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //Doktorların ismini veritabanından dataGridWiew'e yazdırma:
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Doktorlar ", baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.baglanti().Close();

            //Doktor panelindeki CmbBox'ta branşları listeleyelim:
            SqlCommand komut = new SqlCommand("Select BransAd from Tbl_Brans", baglan.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0].ToString());
            }
            baglan.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e) // All debuging is complated.
        {
            //Sisteme ve veri tabanına yeni doktor ekleme:
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorSifre,DoktorTc) values (@p1,@p2,@p3,@p4,@p5)", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut.Parameters.AddWithValue("@p5", mskTC.Text);

            if (txtAd.Text == "" || txtSoyad.Text == "" || cmbBrans.Text == "" || txtSifre.Text == "" || mskTC.Text == "")
            {
                MessageBox.Show("Eklenecek doktorun bilgilerini eksiksiz giriniz.");
            }

            else
            {
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Yeni Doktor sisteme başarıyla eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e) // All debuging is complated.
        {
            SqlCommand sil = new SqlCommand("Delete from Tbl_Doktorlar where doktorTC=@p1", baglan.baglanti());
            sil.Parameters.AddWithValue("@p1", mskTC.Text);

            if (mskTC.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz doktoru seçiniz ya da TC kimlik numarasını giriniz.");
            }

            else
            {
                sil.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Doktor sistemden silindi.");

            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)  // No debugg
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set Doktorad = @p1, doktorsoyad=@p2,doktorBrans=@p3, doktorSifre=@p5 where DoktorTC=@p4 ", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", mskTC.Text);
            komut.Parameters.AddWithValue("@p5", txtSifre.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Doktor bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
