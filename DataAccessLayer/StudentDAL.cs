using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Anusha.Exam.DAL
{
    /// <summary>
    /// Student(Handles Student Related information)
    /// </summary>
    public class StudentDAL
    {
        #region Fields
        private SqlConnection _dbConnection;
        private bool _isError;
        private int _errorNo;
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
        /// Gets the Error No.
        /// </summary>
        /// <value>
        /// ErrorNo As int
        /// </value>
        public int ErrorNo
        {
            get { return _errorNo; }
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
        /// Initializes a new instance of the <see cref="StudentDAL" /> class.
        /// </summary>
        public StudentDAL()
        {
            _dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString);
        }
        #endregion

        #region Methods
        #region Internal
        private void InitializeFields()
        {
            _isError = false;
            _errorMsg = string.Empty;
            _errorNo = int.MinValue;
        }

        protected void OpenDB()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }
            InitializeFields();
        }

        protected void SetError(SqlException Ex)
        {
            _isError = true;
            _errorMsg = Ex.Message;
            _errorNo = Ex.Number;
            switch (Ex.Number)
            {
                case 2601: _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 2627: _errorMsg = "Can not Update!." + Environment.NewLine + " Duplicate Record!";
                    break;
                case 547: _errorMsg = "Can not Delete. Alredy Assign!";
                    break;
                default: break;
            }
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
                using (SqlCommand command = new SqlCommand("GetStudent", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    OpenDB();
                    command.Parameters.AddWithValue("@StudentId", StudentId);
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter(command))
                    {
                        daAdapter.Fill(dtTable);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
            return dtTable;
        }
        
        /// <summary>
        /// Saves the student.
        /// </summary>
        /// <param name="StudentData">StudentData As DataTable</param>
        public void SaveStudent(DataRow[] StudentData)
        {
            try
            {
                OpenDB();
                using (SqlCommand command = new SqlCommand("SaveStudent", _dbConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                   
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50,"Name");
                    command.Parameters.Add("@DOB", SqlDbType.DateTime, 8,"DOB");
                    command.Parameters.Add("@GradePointAvg", SqlDbType.Decimal, 8, "GradePointAvg");
                    command.Parameters.Add("@Active", SqlDbType.Bit, 2,"Active");
                    using (SqlDataAdapter daAdapter = new SqlDataAdapter())
                    {
                        daAdapter.InsertCommand = command;
                        daAdapter.Update(StudentData);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                SetError(sqlex);
            }
            catch (Exception ex)
            {
                _isError = true;
                _errorMsg = ex.Message;
            }
            finally
            {
                _dbConnection.Close();
            }
        }        
        #endregion
        #endregion

        #region Destructor
        ~StudentDAL()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
