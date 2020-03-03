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
        private Action<ConditionHidden> _implementHidden;

        public HiddenFilter(Action<ConditionHidden> implementHidden)
        {
            InitializeComponent();
            this.btnOk.Enabled = false;
            _implementHidden = implementHidden;
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            _implementHidden.Invoke(new ConditionHidden() { Content = txtContent.Text, FilterType = GetFilterType(), IsReverse = checkBox1.Checked });
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private FilterType GetFilterType()
        {
            string value = cbxFilterType.SelectedItem as string;
            if (value.Equals(Define.StartWith)) {
                return FilterType.StartWith;
            }
            else if (value.Equals(Define.Contain)) {
                return FilterType.Contain;
            }
            else {
                return FilterType.EndWith;
            }
        }

        private void HiddenFilter_Load(object sender, EventArgs e)
        {
            cbxFilterType.Items.Add(Define.StartWith);
            cbxFilterType.Items.Add(Define.Contain);
            cbxFilterType.Items.Add(Define.EndWith);
            cbxFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxFilterType.SelectedIndex = 0;
        }
    }
}