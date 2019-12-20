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
    public partial class Copyright : Form
    {
        public Copyright()
        {
            InitializeComponent();
            
        }

        private void Copyright_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }
    }
}
