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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        
        SqlBaglantisi baglan = new SqlBaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From Tbl_sekreter where SekreterTC=@p1 and sekreterSifre=@p2", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmSekreterDetay fsd = new FrmSekreterDetay();
                fsd.tcno = mskTC.Text;
                fsd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tc no veya şifre hatalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglan.baglanti().Close();
        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
