using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Delete : Form
    {
        public event Action<DataGridView, Boolean> DeleteEvent2;
        public event Action<DataGridView, Boolean> DeleteEvent3;
        public event Action<DataGridView, Boolean> DeleteEvent;
        private DatabaseEntities dbdelete;
        private  DataGridView data;
        public Delete(DataGridView data, DatabaseEntities dbdelete)
        {
            InitializeComponent();
            this.data = data;
            this.dbdelete = dbdelete;
        }
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WM_LBUTTONDBLCLK = 0x00A3;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDBLCLK)
            {
                return;
            }
            switch (m.Msg)
            {

                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                        m.Result = (IntPtr)HTCAPTION;
                    return;
            }
            base.WndProc(ref m);
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
       
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            DeleteEvent?.Invoke(data, true);
            DeleteEvent2?.Invoke(data, true);
            DeleteEvent3?.Invoke(data, true);
            Close();
        }

        private void Delete_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PictureBox1_Click(sender, e);
        }
    }
}
