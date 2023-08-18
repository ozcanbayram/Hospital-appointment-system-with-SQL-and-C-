using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHastaGiris hastaGirisSayfasinaGit = new FrmHastaGiris();
            hastaGirisSayfasinaGit.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris doktorGirisSayfasinaGit = new FrmDoktorGiris();
            doktorGirisSayfasinaGit.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris sekreterGirisSayfasinaGit = new FrmSekreterGiris();
            sekreterGirisSayfasinaGit.Show();
            this.Hide();
        }
    }
}
