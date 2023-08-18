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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public string tcno;
        SqlBaglantisi bagla = new SqlBaglantisi();
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            //Tc numarasini diger formdan çekme.
            mskTC.Text = tcno;

            //Tcye sahip doktor bilgilerini sayfaya yükleme:
            SqlCommand komut = new SqlCommand("select * from tbl_doktorlar where doktorTc='" + mskTC.Text + "'", bagla.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text= dr[2].ToString();
                cmbBrans.Text= dr[3].ToString();
                txtSifre.Text= dr[5].ToString();
            }
            bagla.baglanti().Close();

            //Branşlari comboboxa taşıma
            SqlCommand komut2 = new SqlCommand("select bransAd from Tbl_brans", bagla.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0].ToString());
            }
            bagla.baglanti();
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set doktorAd=@p1,DoktorSoyad=@p2,doktorbrans=@p3,doktorsifre=@p4",bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbBrans.Text);
            komut.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut.ExecuteNonQuery();
            bagla.baglanti().Close();
            MessageBox.Show("Bilgileriniz güncellendi.");
        }
    }
}
