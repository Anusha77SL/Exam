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
    public partial class StudentDetail : Form
    {
        DataTable dtStudentData;
        Student objStudent=new Student();
        public StudentDetail()
        {
            InitializeComponent();           
        }

        private void StudentDetail_Load(object sender, EventArgs e)
        {           
            btnAddNew.Enabled = true;
            dtStudentData=objStudent.GetStudent(0);
            BindDataToGrid(dtStudentData);
            btnSave.Enabled = false;
        }

        #region Form Methods
        private void BindDataToGrid(DataTable StudentData)
        {
            dgvStudents.DataSource = StudentData;
            dgvStudents.AutoGenerateColumns = false;
            dgvStudents.Refresh();           
        }
        #endregion
        
        #region Buttons
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            StudentRegistrationForm frmStudentRegistration = new StudentRegistrationForm();
            frmStudentRegistration.ShowDialog(this);

            if (frmStudentRegistration.IsNewStudentCreate == true)
            {
                DataRow drStudentRow = dtStudentData.NewRow();
                drStudentRow["StudentId"] = -99;
                drStudentRow["Name"] = frmStudentRegistration.StudentName;
                drStudentRow["DOB"] = frmStudentRegistration.DateOfBirth;
                drStudentRow["GradePointAvg"] = frmStudentRegistration.GradePointAvg;
                drStudentRow["Active"] = frmStudentRegistration.Active;

                dtStudentData.Rows.Add(drStudentRow);
                //  dtStudentData.AcceptChanges();
                BindDataToGrid(dtStudentData);
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objStudent.SaveStudent(dtStudentData);
                if (!objStudent.IsError)
                {
                    MessageBox.Show("Data Successfuly Enterd...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show(objStudent.ErrorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            { }
        } 
        #endregion
        
    }
}
