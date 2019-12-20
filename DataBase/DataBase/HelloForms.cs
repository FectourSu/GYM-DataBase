using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
    public partial class HelloForms : Form
    {
        public HelloForms() 
        {
           InitializeComponent();
        }
        //Движение формы без шапки
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
        //


        private void PictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Скрыть и закрыть форму
        private void PictureBox4_Click(object sender, EventArgs e)
        {
            HelloForms close = new HelloForms();
            Clients client = new Clients();
            Hide();
            client.Show();
            close.Close();
        }

        private void HelloForms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                HelloForms close = new HelloForms();
                Clients client = new Clients();
                Hide();
                client.Show();
                close.Close();
            }
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Права защищены
        private void Label5_Click(object sender, EventArgs e)
        {
            Copyright obj = new Copyright();
            obj.ShowDialog();
        }
    }
}
