using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace SENDDATARMSGST
{
    class clsupdatelocalstructure
    {
        public static SqlConnection mssqllocalcon = new SqlConnection();
        public SqlCommand mscmd = new SqlCommand();
        public SqlDataAdapter msdtplocal = new SqlDataAdapter();
        
        # region PRIVATE FUNCTION

        private bool OpenMsSqlLocalDataConnection()
        {
            string connstr1 = "";
            try
            {
                mssqllocalcon = new SqlConnection();

                if (mssqllocalcon.State == ConnectionState.Closed)
                {
                    connstr1 = "Data Source=" + clspublicvariable.GENLOCALSERVERNAME + ";Initial Catalog=" + clspublicvariable.GENLOCALDBNAME + ";Persist Security Info=True;User ID=" + clspublicvariable.GENLOCALLOGIN + ";Password=" + clspublicvariable.GENLOCALPASSWORD;
                    mssqllocalcon.ConnectionString = connstr1;
                    mssqllocalcon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlLocalDataConnection())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion

        # region PUBLIC FUNCTION

        public DataTable FillDataTableLocal(string cmdstr, string tblnm1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                this.OpenMsSqlLocalConnection();

                msdtplocal = new SqlDataAdapter(cmdstr, mssqllocalcon);
                msdtplocal.Fill(ds, tblnm1);
                dt = ds.Tables[0];

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillDataTableLocal()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return dt;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public bool OpenMsSqlLocalConnection()
        {
            try
            {

                if (string.IsNullOrEmpty(mssqllocalcon.ConnectionString))
                {
                    this.OpenMsSqlLocalDataConnection();
                }

                if (mssqllocalcon.State == ConnectionState.Closed)
                {
                    mssqllocalcon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlLocalConnection()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public bool CloseMsSqlLocalConnection()
        {
            try
            {
                if (mssqllocalcon.State == ConnectionState.Open)
                {
                    mssqllocalcon.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in CloseMsSqlLocalConnection()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        public bool ExecuteMsSqlLocalCommand_NoMsg(string cmdStr)
        {
            try
            {
                OpenMsSqlLocalConnection();
                mscmd.Connection = mssqllocalcon;
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

        #endregion       

        #region UPDATE TABLE

        public bool UPDATE_TABLE_MSTUNIT()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval= false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTUNIT SET SENDDATA = 1 WHERE ISNULL(MSTUNIT.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();
                        
                        cmd.Transaction = objTrans;
                        cmd.Connection = con;                        
                        cmd.CommandText = str1;                        
                        
                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTDEPT()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTDEPT SET SENDDATA = 1 WHERE ISNULL(MSTDEPT.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTUSERS()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTUSERS SET SENDDATA = 1 WHERE ISNULL(MSTUSERS.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTREPORTDEPT()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTREPORTDEPT SET SENDDATA = 1 WHERE ISNULL(MSTREPORTDEPT.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTHSNCODE()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTHSNCODE SET SENDDATA = 1 WHERE ISNULL(MSTHSNCODE.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTITEMGROUP()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTITEMGROUP SET SENDDATA = 1 WHERE ISNULL(MSTITEMGROUP.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTITEM()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTITEM SET SENDDATA = 1 WHERE ISNULL(MSTITEM.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTITEMPRICELIST()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTITEMPRICELIST SET SENDDATA = 1 WHERE ISNULL(MSTITEMPRICELIST.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTITEMPRICELISTDTL()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTITEMPRICELISTDTL SET SENDDATA = 1 WHERE ISNULL(MSTITEMPRICELISTDTL.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTREMARK()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTREMARK SET SENDDATA = 1 WHERE ISNULL(MSTREMARK.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTTABLE()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTTABLE SET SENDDATA = 1 WHERE ISNULL(MSTTABLE.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTEMPCAT()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTEMPCAT SET SENDDATA = 1 WHERE ISNULL(MSTEMPCAT.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTEMP()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTEMP SET SENDDATA = 1 WHERE ISNULL(MSTEMP.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTCUST()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE MSTCUST SET SENDDATA = 1 WHERE ISNULL(MSTCUST.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_KOT()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE KOT SET SENDDATA = 1 WHERE ISNULL(KOT.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_KOTDTL()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE KOTDTL SET SENDDATA = 1 WHERE ISNULL(KOTDTL.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_BILL()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE BILL SET SENDDATA = 1 WHERE ISNULL(BILL.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_BILLDTL()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = "UPDATE BILLDTL SET SENDDATA = 1 WHERE ISNULL(BILLDTL.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_SETTLEMENT()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE SETTLEMENT SET SENDDATA = 1 WHERE ISNULL(SETTLEMENT.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_CASHONHAND()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE CASHONHAND SET SENDDATA = 1 WHERE ISNULL(CASHONHAND.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTEXPENCES()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE MSTEXPENCES SET SENDDATA = 1 WHERE ISNULL(MSTEXPENCES.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTINCOME()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE MSTINCOME SET SENDDATA = 1 WHERE ISNULL(MSTINCOME.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTTIEUPCOMPANY()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE MSTTIEUPCOMPANY SET SENDDATA = 1 WHERE ISNULL(MSTTIEUPCOMPANY.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_MSTSUPPLIER()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE MSTSUPPLIER SET SENDDATA = 1 WHERE ISNULL(MSTSUPPLIER.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_TABLERESERVATION()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE TABLERESERVATION SET SENDDATA = 1 WHERE ISNULL(TABLERESERVATION.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_EXPENCES()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE EXPENCE SET SENDDATA = 1 WHERE ISNULL(EXPENCE.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        public bool UPDATE_TABLE_INCOME()
        {
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string str1 = "";
            bool retval = false;
            try
            {
                retval = true;
                str1 = " UPDATE INCOME SET SENDDATA = 1 WHERE ISNULL(INCOME.SENDDATA,0)=0";

                using (con = new SqlConnection(clspublicvariable.Connstr_LocalDb))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.Transaction = objTrans;
                        cmd.Connection = con;
                        cmd.CommandText = str1;

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception)
                        {
                            objTrans.Rollback();
                            retval = false;
                        }
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return false;
            }

            finally
            {
                con.Close();
            }
        }

        #endregion
    }
}
