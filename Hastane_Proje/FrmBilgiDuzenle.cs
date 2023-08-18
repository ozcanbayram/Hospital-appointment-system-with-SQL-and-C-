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

using System.Data.SqlClient;

namespace Hastane_Proje
{
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        private void cmbCinsiyet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        SqlBaglantisi baglan = new SqlBaglantisi();
        public string tcNo;
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = tcNo;

            //Hastanın bilgilerini veri tabanından çekelmi:
            SqlCommand komut = new SqlCommand("Select * from Tbl_Hastalar where HastaTC = @p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTelefon.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text = dr[6].ToString();
            }
            baglan.baglanti().Close();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            //Bilgileri güncellemek için butona tıklanınca veritabanındaki değerleri değiştirelim. Where komutu çok önemlidir.
            SqlCommand komut2 = new SqlCommand("Update Tbl_Hastalar set hastaAd=@p1, HastaSoyad=@p2, Hastatelefon=@p3, HastaSifre=@p4, hastaCinsiyet=@p5 where hastaTC=@p6", baglan.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mskTelefon.Text);
            komut2.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", mskTC.Text);
            komut2.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Bilgileriniz güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
