using System.ComponentModel;
using System.Windows.Forms;

namespace Shapkin_Task_7
{
    public partial class Task7 : Form
    {
        public Task7()
        {
            InitializeComponent();
            this.HelpButtonClicked += ShapedForm1_HelpButtonClicked;
        }

        private void ShapedForm1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("TODO: description", "Лисы и куры - Информация", MessageBoxButtons.OK, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
