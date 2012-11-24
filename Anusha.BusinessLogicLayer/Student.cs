using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Anusha.Exam.DAL;

namespace Anusha.Exam.BLL
{
    /// <summary>
    /// Student(Handles Student Related information)
    /// </summary>
    public class Student
    {
        #region Fields
        private StudentDAL objStudent;
        private bool _isError;
        private string _errorMsg;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance Is Error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError
        {
            get { return _isError; }
        }

        /// <summary>
        /// Gets the error MSG.
        /// </summary>
        /// <value>
        /// ErrorMsg as string
        /// </value>
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Student" /> class.
        /// </summary>
        public Student()
        {
            objStudent = new StudentDAL();
        }
        #endregion

        #region Methods
        #region Internal
        protected void SetError()
        {
            _isError = objStudent.IsError;
            _errorMsg = objStudent.ErrorMsg;
        }
        #endregion

        #region External Methods
        /// <summary>
        /// Gets the student.
        /// </summary>
        /// <param name="StudentId">StudentId As int</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetStudent(int StudentId)
        {
            DataTable dtTable = new DataTable();
            try
            {
                dtTable = objStudent.GetStudent(StudentId);
                SetError();
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            return dtTable;
        }

        /// <summary>
        /// Saves the student.
        /// </summary>
        /// <param name="StudentData">StudentData As DataTable</param>
        public void SaveStudent(DataTable StudentData)
        {
            try
            {
                if (StudentData.Select("StudentId=" + -99).Length > 0)
                {
                    objStudent.SaveStudent(StudentData.Select("StudentId=" + -99));
                }               
                SetError();
            }    
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
        }

        #endregion
        #endregion

        #region Destructor
        ~Student()
        {
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
