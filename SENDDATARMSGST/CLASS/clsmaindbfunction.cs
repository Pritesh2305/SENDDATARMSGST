using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace SENDDATARMSGST
{
    class clsmaindbfunction
    {
        public static SqlConnection mssqlmaincon = new SqlConnection();
        public SqlCommand mscmd = new SqlCommand();
        public SqlDataAdapter msmaindtp = new SqlDataAdapter();

        # region PRIVATE FUNCTION

        private bool OpenMsSqlMainDataConnection()
        {
            string connstr1 = "";
            try
            {
                mssqlmaincon = new SqlConnection();

                if (mssqlmaincon.State == ConnectionState.Closed)
                {
                    connstr1 = "Data Source=" + clspublicvariable.GENMAINSERVERNAME + ";Initial Catalog=" + clspublicvariable.GENMAINDBNAME + ";Persist Security Info=True;User ID=" + clspublicvariable.GENMAINLOGIN + ";Password=" + clspublicvariable.GENMAINPASSWORD;
                    mssqlmaincon.ConnectionString = connstr1;
                    mssqlmaincon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlMainDataConnection())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion

        # region PUBLIC FUNCTION

        public bool OpenMsSqlMainConnection()
        {
            try
            {

                if (string.IsNullOrEmpty(mssqlmaincon.ConnectionString))
                {
                    this.OpenMsSqlMainDataConnection();
                }

                if (mssqlmaincon.State == ConnectionState.Closed)
                {
                    mssqlmaincon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlMainConnection()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public bool CloseMsSqlMainConnection()
        {
            try
            {
                if (mssqlmaincon.State == ConnectionState.Open)
                {
                    mssqlmaincon.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in CloseMsSqlMainConnection()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        public bool ExecuteMsSqlMainCommand_NoMsg(string cmdStr)
        {
            try
            {
                this.OpenMsSqlMainConnection();
                mscmd.Connection = mssqlmaincon;
                mscmd.CommandText = cmdStr;
                mscmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public DataTable FillMainDataTable(string cmdstr, string tblnm1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                this.OpenMsSqlMainConnection();

                msmaindtp = new SqlDataAdapter(cmdstr, mssqlmaincon);
                msmaindtp.Fill(ds, "test");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillMainDataTable()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return dt;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        #endregion

    }
}
