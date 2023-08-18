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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bagla = new SqlBaglantisi();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2",bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if(dr.Read())
            {
                FrmDoktorDetay fdd = new FrmDoktorDetay();
                fdd.tc = mskTC.Text;
                fdd.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya şifre girdiniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
