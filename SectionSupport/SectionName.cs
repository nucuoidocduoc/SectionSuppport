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
    public partial class SectionName : Form
    {
        private Action<SectionNameDetail> _implementRename;

        public SectionName(Action<SectionNameDetail> implementRename)
        {
            InitializeComponent();
            this.btnOk.Enabled = false;
            _implementRename += implementRename;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _implementRename.Invoke(new SectionNameDetail() { Prefix = tbxPrefix.Text, Suffiexs = tbxSuffixes.Text });
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbxPrefix_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxPrefix.Text)) {
                btnOk.Enabled = false;
            }
            else {
                btnOk.Enabled = true;
            }
        }
    }
}