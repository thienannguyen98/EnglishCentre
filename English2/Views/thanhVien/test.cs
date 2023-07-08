using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English2.Views.thanhVien
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            defaultToolTipController1.SetToolTip(textBox1,"123");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
