using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English2
{
    public partial class testImage : Form
    {
        public testImage()
        {
            InitializeComponent();
            loadData();
        }

        void loadData()
        {
            DataGridViewButtonColumn nut = new DataGridViewButtonColumn();
            this.dataGridView1.Columns.Add(nut);

            //DataGridViewImageColumn nutSua = new DataGridViewImageColumn();
            //nutSua.Image = Properties.Resources.refresh;
            //this.dataGridView1.Columns.Add(nutSua);

            //DataGridViewImageColumn nutXoa = new DataGridViewImageColumn();
            //nutXoa.Image = Properties.Resources.trash;
            //this.dataGridView1.Columns.Add(nutXoa);

            
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Image refresh = Properties.Resources.refresh;
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.refresh.Width;
                var h = Properties.Resources.refresh.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(refresh, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}
