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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {

        }

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit hastaKayitSayfasinaGit = new FrmHastaKayit();
            hastaKayitSayfasinaGit.Show();
        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Hastalar where HastaTC = @p1 and HastaSifre = @p2", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmHastaDetay fhd  = new FrmHastaDetay();
                fhd.tc=mskTC.Text;//Bu formdaki tc kimlik numarasını HastDetay formuna taşımak için. Diğer formda bir public string tc oluşturuldu.
                fhd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC kimlik numarası veya şifre girdiniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            bagla.baglanti().Close();
        }
    }
}
