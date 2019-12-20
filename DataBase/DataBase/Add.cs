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
    public partial class Add : Form
    {
        public event Action<List<Control>, Boolean> AddE;
        public Add()
        {
            InitializeComponent();
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
        //блочим буквы
        public void BLOCK2(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number) && number != 8 && number != 58 && number != 45) // цифры и клавиша BackSpace
                e.Handled = true;
        }
        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            BLOCK2(sender,e);
        }
        public void BLOCK(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(e.KeyChar <= 32 || e.KeyChar >= 64) ||
                !(number != 47) ||
                !(number != 92) ||
                !(number != 91) ||
                !(number != 93) ||
                !(number != 95) ||
                !(number != 96) ||
                !(number != 9) ||
                !(number != 11) ||
                !(e.KeyChar <= 122 || e.KeyChar >= 128))
            {
                e.Handled = true;
            }
        }
        //блокируем цифры
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            BLOCK(sender,e);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("Все поля должны быть заполнены!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                AddE?.Invoke(this.Controls.Cast<Control>().Where(c => c is TextBox).ToList(), true);
                Close();
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox2.Focus();
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PictureBox1_Click(sender, e);
        }
        public void ComboColor(object sender, DrawItemEventArgs e)
        {
            var combo = sender as ComboBox;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.Bounds);
                e.Graphics.DrawString(combo.Items[e.Index].ToString(),
                                 e.Font,
                                 new SolidBrush(Color.White),
                                 new Point(e.Bounds.X, e.Bounds.Y));
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                e.Graphics.DrawString(combo.Items[e.Index].ToString(),
                                 e.Font,
                                 new SolidBrush(Color.Black),
                                 new Point(e.Bounds.X, e.Bounds.Y));
            }
        }
    }
}
