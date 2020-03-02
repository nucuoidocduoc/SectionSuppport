using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SectionSupport
{
    public partial class HiddenFilter : Form
    {
        public HiddenFilter()
        {
            InitializeComponent();
            this.btnOk.Enabled = false;
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContent.Text)) {
                btnOk.Enabled = false;
            }
            else {
                btnOk.Enabled = true;
            }
        }
    }
}