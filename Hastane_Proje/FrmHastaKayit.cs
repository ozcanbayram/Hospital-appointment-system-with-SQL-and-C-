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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        private void btnKayit_Click(object sender, EventArgs e)
        {
                SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (hastaad,hastasoyad,hastatc,hastatelefon,hastasifre,hastacinsiyet) values(@p1,@p2,@p3,@p4,@p5,@p6)", bagla.baglanti());
                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", mskTC.Text);
                komut.Parameters.AddWithValue("@p4", mskTelefon.Text);
                komut.Parameters.AddWithValue("@p5", txtSifre.Text);
                komut.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);

                if (txtAd.Text == "" || txtSoyad.Text == "" || mskTC.Text == "" || mskTelefon.Text == "" || txtSifre.Text == "" || cmbCinsiyet.Text == "")
                {
                    MessageBox.Show("Kayıt işlemi için bilgilerinizi eksiksiz girmelisiniz.");
                }
                else
                {
                    komut.ExecuteNonQuery();
                    bagla.baglanti().Close();
                    MessageBox.Show("Kaydınız gerçekleşmiştir. Şifreniz:" + txtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {

        }
    }
}
