using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Proje
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string tcno;
        SqlBaglantisi baglan = new SqlBaglantisi();
        private void FrmSekreterDetay_Load_1(object sender, EventArgs e)
        {
            //tc numarasini frmgiris sayfasından alalım:
            lblTC.Text = tcno;

            //Ad ve soyadı detay kısmına veri tabanından çekme:
            SqlCommand komut1 = new SqlCommand("select sekreterAdSoyad From Tbl_Sekreter where sekreterTc = @p1", baglan.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();
            }
            baglan.baglanti().Close();

            //Branşları DataGride Aktarma:
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select * from Tbl_brans", baglan.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            baglan.baglanti().Close();

            //Doktor bilgilerini dataGride Aktarma:
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar', doktorBrans from Tbl_Doktorlar", baglan.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            baglan.baglanti().Close();

            //Branşları veritanbanından comboboxa taşıma:
            SqlCommand komut2 = new SqlCommand("select bransAd from Tbl_brans", baglan.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }
            baglan.baglanti();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut_kaydet = new SqlCommand("insert into tbl_randevular (randevutarih,randevusaat,randevubrans,randevudoktor) values (@r1,@r2,@r3,@r4)", baglan.baglanti());
            komut_kaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komut_kaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komut_kaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komut_kaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);

            if (mskTarih.Text == "" || mskSaat.Text == "" || cmbBrans.Text == "" || cmbDoktor.Text == "")
            {
                MessageBox.Show("Randevu bilgilerini eksiksiz giriniz.");
            }
            else
            {
                komut_kaydet.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Randevu oluşturuldu.");
            }

           
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Seçilen branşın doktorlarını cmbDoktorlar'a veri tanbanından çağırma:
            cmbDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("select doktorAd,DoktorSoyad from Tbl_Doktorlar where Doktorbrans=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            baglan.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@p1)", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", rchDuyuru.Text);

            if(rchDuyuru.Text == "")
            {
                MessageBox.Show("Duyuru kutucuğu boş bırakılamaz.");
            }

            else
            {
                komut.ExecuteNonQuery();
                rchDuyuru.Clear();
                baglan.baglanti().Close();
                MessageBox.Show("Duyuru oluşturuldu.");
            }

            
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli fdp = new FrmDoktorPaneli();
            fdp.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans fb = new FrmBrans();
            fb.Show();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fd = new FrmDuyurular();
            fd.Show();
        }
    }
}
