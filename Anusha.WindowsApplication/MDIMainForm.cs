using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Anusha.Exam.WindowsApplication
{
    public partial class MDIMainForm : Form
    {
        private AboutBox frmAbout;
        private StudentDetail frmStudentForm;

        public MDIMainForm()
        {
            InitializeComponent();
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout = new AboutBox();
            frmAbout.MdiParent = this;
            frmAbout.Show();
        }

        private void newRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentForm = new StudentDetail();
            frmStudentForm.MdiParent = this;
            frmStudentForm.Show();
        }
    }
}
