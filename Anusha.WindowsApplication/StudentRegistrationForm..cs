using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Anusha.Exam.BLL;

namespace Anusha.Exam.WindowsApplication
{
    public partial class StudentRegistrationForm : Form
    {
        #region Fields
        Student objStudent;
        private string _name;
        private DateTime _dateOfBirth;
        private decimal _gradePointAvg;
        private bool _active;
        private bool _isCreate=false;       
        #endregion

        #region Properties
        public string StudentName
        {
            get { return _name; }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
        }

        public decimal GradePointAvg
        {
            get { return _gradePointAvg; }
        }

        public bool Active
        {
            get { return _active; }
        }

        public bool IsNewStudentCreate
        {
            get { return _isCreate; }
        }
        #endregion
        
        #region Form Constructor
        public StudentRegistrationForm()
        {
            InitializeComponent();
            objStudent = new Student();
        }
        #endregion

        private void StudentRegistrationForm_Load(object sender, EventArgs e)
        {
            InitControls();
            ActiveControls(false, true);
        }


        #region Form Methods
        private void ActiveControls(bool ReadOnly, bool Enable)
        {
            txtName.ReadOnly = ReadOnly;
            dtpDOB.Enabled = Enable;
            txtGradePointAVG.ReadOnly = ReadOnly;
            cbActive.Enabled = Enable;
        }

        private void InitControls()
        {
            txtName.Text = string.Empty;
            txtGradePointAVG.Text = string.Empty;
            dtpDOB.Value = DateTime.Today;
            cbActive.Checked = true;
        }

        private bool ValidateData()
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Name Can Not Be Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }

            if (dtpDOB.Value >= DateTime.Today)
            {
                MessageBox.Show("Invalid Date of Birth!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // if their is any minimum age it is able to validate here 
                dtpDOB.Focus();
                return false;
            }

            if (txtGradePointAVG.Text == string.Empty)
            {
                MessageBox.Show("Grade Point AVG Can Not Be Empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGradePointAVG.Focus();
                return false;
            }
            else
            {
                try
                {
                    Convert.ToDecimal(txtGradePointAVG.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid Grade Point AVG!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGradePointAVG.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Buttons
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            try
            {
                _isCreate = true;
                _name = txtName.Text;
                _dateOfBirth = dtpDOB.Value;
                _gradePointAvg =Convert.ToDecimal(txtGradePointAVG.Text);
                _active = cbActive.Checked;               
                this.Close();
            }
            catch
            { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            InitControls();
        }
        #endregion


    }
}
