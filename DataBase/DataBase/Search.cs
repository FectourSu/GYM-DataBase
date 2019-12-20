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
    public partial class Search : Form
    {
        public event Action<List<Control>, Boolean> Searching;
        public event Action<DateTimePicker> Before;
        public event Action<DateTimePicker> After;
        public DataGridView data;
        public Search(DataGridView data)
        {
            InitializeComponent();
            this.data = data;
        }
        //Скрываем не нужные нам элементы на форме качалка и спортзал
        public void InVisible()
        {
            panel9.Visible = false;
            comboBox2.Visible = false;
            dateTimePicker1.Visible = false;
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
            if (textBox1.Text == "" || textBox1.Text == " ")
            {
                if (comboBox2.Text == string.Empty)
                    MessageBox.Show("Заполните строку поиска!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    if (comboBox2.SelectedIndex > 0)
                    {
                        Before?.Invoke(dateTimePicker1);
                        Close();
                    }
                    else
                    {
                        After?.Invoke(dateTimePicker1);
                        Close();
                    }
                }
            }
            else
            {
                Searching?.Invoke(this.Controls.Cast<Control>().Where(c => c is TextBox).ToList(), true);
                Close();
            }
            
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!(e.KeyChar <= 58 || e.KeyChar >= 65) || 
                !(number != 9) || 
                !(number != 11) || 
                !(number != 47) || 
                !(e.KeyChar <= 33 || e.KeyChar >= 45) || 
                !(e.KeyChar <= 90 || e.KeyChar >= 97) ||
                !(e.KeyChar <= 60 || e.KeyChar >= 95)||
                !(e.KeyChar <= 122 || e.KeyChar >= 127))
            {
                e.Handled = true;
            }
        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PictureBox1_Click(sender, e);
        }

        private void ComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Add colorcb = new Add();
            colorcb.ComboColor(sender, e);
        }
    }
}
