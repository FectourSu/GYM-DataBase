using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Selected : Form
    {
        public event Action<List<Control>, Boolean> SelectedSection;
        private DataGridView data;
        Add obj = new Add();
        public Selected(DataGridView data)
        {
            InitializeComponent();
            this.Load += Selected_Load;
            this.data = data;
        }

        private void Selected_Load(object sender, EventArgs e)
        {
           var mass = data.SelectedRows[data.SelectedRows.Count - 1].Cells;
           textBox1.Text = mass[1].Value.ToString();
           textBox2.Text = mass[2].Value.ToString();
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
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("Все поля должны быть заполнены!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                SelectedSection?.Invoke(this.Controls.Cast<Control>().Where(c => c is TextBox).ToList(), true);
                Close();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PictureBox1_Click(sender, e);
        }
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            obj.BLOCK(sender, e);
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            obj.BLOCK2(sender, e);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox2.Focus();
        }
    }
}
