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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public string tc;
        SqlBaglantisi bagla = new SqlBaglantisi();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;

            //Hasta isim ve soyisim yazdırma.
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC = @p1", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bagla.baglanti().Close();

            //Randevu geçmişi:
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC=" + tc, bagla.baglanti()); //Data adapterde @p1.. paramrtre kullanılmaz bu yüzden bu şekilde yazdım.
            da.Fill(dt);//Tablonun içine dt değerlerini yaz, sanal tablo oluştur.
            dataGridView1.DataSource = dt; //Datagridin veri kaynağı dt den gelen değer olarak ayarlanır.

            //Branşları çekme:
            SqlCommand komut2 = new SqlCommand("select bransAd from Tbl_Brans", bagla.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            //Seçilen branşın doktorlarını listeleme:
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans = @p1", bagla.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while(dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bagla.baglanti().Close();
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fbd = new FrmBilgiDuzenle();
            fbd.tcNo = lblTC.Text;
            fbd.Show();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_Randevular set randevudurum=1 ,hastatc=@p1, hastaSikayet=@p2 where Randevuid=@p3",bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", schSikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtId.Text);
            komut.ExecuteNonQuery();
            bagla.baglanti().Close();
            MessageBox.Show("Randevu alındı.","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //doktor ve brans seçilince aktif randevuları listeleme:
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where randevuBrans='" + cmbBrans.Text + "'" + " and RandevuDoktor='" + cmbDoktor.Text + "'and randevuDurum=0", bagla.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            bagla.baglanti().Close();
        }

        private void schSikayet_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
