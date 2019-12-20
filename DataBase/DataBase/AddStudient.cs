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
    public partial class AddStudient : Form
    {
        public event Action<List<Control>,Boolean> AddEvent;
        public DatabaseEntities database;
        
        public AddStudient(DatabaseEntities database)
        {
            InitializeComponent();
            var collectionSport = database.Спортзал
                .ToList();
            foreach (var item in collectionSport)
                comboBox1.Items.Add(item.Секция);

            var collectionGym = database.Качалка
               .ToList();
            foreach (var item in collectionGym)
                comboBox2.Items.Add(item.Секция);

            comboBox1.DropDownHeight = 200;
            comboBox2.DropDownHeight = 200;
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
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                MessageBox.Show("Все поля должны быть заполнены!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                AddEvent?.Invoke(this.Controls.Cast<Control>().Where(c => c is TextBox || c is DateTimePicker || c is ComboBox).ToList(), true);
                Close();
            }
        }
        public void KeyBlock(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!(e.KeyChar <= 31 || e.KeyChar >= 64) ||
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
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyBlock(sender, e);
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyBlock(sender, e);
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyBlock(sender, e);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox2.Focus();
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox3.Focus();
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                comboBox1.Focus();
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                comboBox2.Focus();
        }

        private void ComboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dateTimePicker1.Focus();
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PictureBox1_Click(sender, e);
        }
        
        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Add colorcb = new Add();
            colorcb.ComboColor(sender, e);
        }

        private void ComboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Add colorcb = new Add();
            colorcb.ComboColor(sender, e);
        }
    }
}
