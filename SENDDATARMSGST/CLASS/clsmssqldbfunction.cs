using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace SENDDATARMSGST
{
    class clsmssqldbfunction
    {
        public static SqlConnection mssqlcon = new SqlConnection();
        public SqlDataAdapter msdtp = new SqlDataAdapter();
        public SqlCommand mscmd = new SqlCommand();
        public SqlDataReader msdr;
        
        # region PRIVATE FUNCTION

        private bool OpenMsSqlDataConnection()
        {
            try
            {

                // MessageBox.Show("In OpenMsSqlDataConnection() : " + ConfigurationManager.ConnectionStrings["RMS"].ConnectionString);
                mssqlcon = new SqlConnection();
                
                if (mssqlcon.State == ConnectionState.Closed)
                {
                    // mssqlcon.Close();

                    // MessageBox.Show("Cn str : " + ConfigurationManager.ConnectionStrings["RMS"].ConnectionString);

                    mssqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["SENDDATA"].ConnectionString;
                    mssqlcon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlDataConnection())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion

        # region PUBLIC FUNCTION

        public bool OpenMsSqlConnection()
        {
            try
            {
                //MessageBox.Show("in OpenMsSqlConnection()");

                if (string.IsNullOrEmpty(mssqlcon.ConnectionString))
                {
                    OpenMsSqlDataConnection();
                }

                if (mssqlcon.State == ConnectionState.Closed)
                {
                    mssqlcon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlConnection()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public bool CloseMsSqlConnection()
        {
            try
            {
                if (mssqlcon.State == ConnectionState.Open)
                {
                    mssqlcon.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in CloseMsSqlConnection()");
                return false;
            }

        }

        public bool ExecuteMsSqlCommand(string cmdStr)
        {
            try
            {
                OpenMsSqlConnection();
                mscmd.Connection = mssqlcon;
                mscmd.CommandText = cmdStr;
                mscmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in ExecuteMsSqlCommand()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public bool ExecuteMsSqlCommand_NoMsg(string cmdStr)
        {
            try
            {
                OpenMsSqlConnection();
                mscmd.Connection = mssqlcon;
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

        public object ExecuteMsSqlCommandScalar(string cmdStr)
        {
            object obj = null;

            try
            {
                OpenMsSqlConnection();
                mscmd.Connection = mssqlcon;
                mscmd.CommandText = cmdStr;
                obj = mscmd.ExecuteScalar();

                if (DBNull.Value.Equals(obj) == true)
                {
                    obj = "";
                }

                return obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in ExecuteMsSqlCommandScalar()");
                return obj;
            }

            finally
            {
                //CloseMsSqlConnection();
            }

        }

        public DataTable FillDataTable(string cmdstr, string tblnm1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                OpenMsSqlConnection();

                msdtp = new SqlDataAdapter(cmdstr, mssqlcon);
                msdtp.Fill(ds, "test");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillDataTable()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return dt;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public SqlDataReader ExecuteMsSqlDataReader(string cmdStr)
        {
            SqlDataReader functionReturnValue = default(SqlDataReader);
            functionReturnValue = msdr;

            try
            {
                OpenMsSqlConnection();
                mscmd.CommandText = cmdStr;

                if ((msdr != null))
                {
                    msdr.Close();
                }
                msdr = mscmd.ExecuteReader();
                functionReturnValue = msdr;

            }
            catch (Exception ex)
            {
                functionReturnValue = msdr;
                //CloseMsSqlConnection();
                MessageBox.Show(ex.Message.ToString() + " Error occures in ExecuteMsSqlDataReader()");
            }

            return functionReturnValue;
        }

        public bool FillMstCombo(System.Windows.Forms.ComboBox obj, string FieldName)
        {
            string item = "";
            string cmdStr = null;

            try
            {
                cmdStr = ExecuteMsSqlCommandScalar("Select FieldValue from MstCombo where FieldName='" + FieldName + "'").ToString();

                obj.Items.Clear();
                for (Int32 i = 0; i <= cmdStr.Length - 1; i++)
                {
                    if (cmdStr[i].Equals("!") || i == cmdStr.Length - 1)
                    {
                        item = item + cmdStr[i];

                        item = item.Trim().Replace("!", "");

                        if (!string.IsNullOrEmpty(item))
                        {
                            obj.Items.Add(item);
                            item = "";
                        }
                    }
                    else
                    {
                        item = item + cmdStr[i];
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillMstCombo()");
                return false;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public void FillDataCombo(object Obj, string DisplayMember1, string ValueMember1, bool AddBlankValue1)
        {
            string strSQL = null;
            SqlDataAdapter DA = new SqlDataAdapter();
            DataSet DS = new DataSet();
            object[] vArrSp = null;

            try
            {
                vArrSp = DisplayMember1.Split('|');

                strSQL = "Select " + vArrSp[0].ToString() + ", " + vArrSp[1].ToString() + " from " + ValueMember1 + "";

                if (AddBlankValue1 == true)
                {
                    strSQL = strSQL + " UNION Select 0 as " + vArrSp[0].ToString() + ", '' as " + vArrSp[1].ToString() + " from " + ValueMember1 + "";
                }


                strSQL = strSQL + " Order by " + vArrSp[1].ToString();

                DA = new SqlDataAdapter(strSQL, mssqlcon);
                DA.Fill(DS, ValueMember1);
                ((System.Windows.Forms.ComboBox)Obj).DataSource = DS.Tables[0];
                ((System.Windows.Forms.ComboBox)Obj).DisplayMember = vArrSp[1].ToString();
                ((System.Windows.Forms.ComboBox)Obj).ValueMember = vArrSp[0].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillDataCombo()");
            }
        }

        public bool Fill_DistinctCombo_Values(System.Windows.Forms.ComboBox Cmb1, string DataField1, string DataMember1)
        {
            bool functionReturnValue = false;
            string SqlStr1 = null;
            DataTable dt = default(DataTable);
            //DataRow Row = default(DataRow);

            try
            {
                functionReturnValue = true;

                SqlStr1 = "Select Distinct " + DataField1 + " From " + DataMember1;
                OpenMsSqlConnection();
                dt = FillDataTable(SqlStr1, "");

                Cmb1.Items.Clear();
                foreach (DataRow Row in dt.Rows)
                {
                    Cmb1.Items.Add(Row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Fill_DistinctCombo_Values()");
                functionReturnValue = false;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
            return functionReturnValue;
        }

        public bool FillGeneralCombobox(string strqtr1, string tblnm1, string Dispstr1, string Valuestr1, ComboBox cmb1)
        {

            DataTable dt1 = new DataTable();
            try
            {
                dt1 = FillDataTable(strqtr1, tblnm1);
                cmb1.DisplayMember = Dispstr1;
                cmb1.ValueMember = Valuestr1;
                cmb1.DataSource = dt1;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckMasterData(string Tblnm1, string fieldnm1, string Dataval1)
        {
            string str1;
            DataTable dt1 = new DataTable();
            try
            {
                str1 = "Select * From " + Tblnm1 + " where upper(" + fieldnm1 + ") = '" + Dataval1.ToUpper() + "'";
                dt1 = FillDataTable(str1, Tblnm1);

                if (dt1.Rows.Count <= 0)
                {
                    return false;
                }
                else
                {

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string GetValueFromTable(string Tblnm1, string ridfieldname, string getvalfieldnm1, string Riddataval1)
        {
            string str1;
            DataTable dt1 = new DataTable();
            string retval;
            try
            {
                retval = "";
                str1 = "Select " + getvalfieldnm1 + " From " + Tblnm1 + " where " + ridfieldname + " = '" + Riddataval1 + "'";
                dt1 = FillDataTable(str1, Tblnm1);

                if (dt1.Rows.Count > 0)
                {
                    retval = dt1.Rows[0][0] + "".Trim();
                }
                else
                {
                    retval = "";
                }

                return retval;
            }
            catch (Exception)
            {
                return "";
            }

            finally
            {
                //CloseMsSqlConnection();
            }

        }

        public object[] FillCombo(string cmdStr)
        {

            int Ctr1 = 0;

            try
            {
                OpenMsSqlConnection();
                DataTable dt = FillDataTable(cmdStr, "");
                //DataRow row = default(DataRow);

                object[] cmbItems = new object[dt.Rows.Count];

                Ctr1 = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cmbItems[Ctr1] = row[0].ToString();
                    Ctr1 = Ctr1 + 1;
                }

                return cmbItems;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillCombo()");
                return null;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public long GetTotalNumberofRecord(string qry1)
        {
            object no1;
            Int64 cnt1;
            try
            {
                OpenMsSqlDataConnection();
                mscmd.Connection = mssqlcon;
                mscmd.CommandText = qry1;
                no1 = mscmd.ExecuteScalar();
                cnt1 = Null2lng(no1);
                return cnt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetTotalNumberofRecord()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public long GetMaxRecordFromTable(string Tblnm1, string fldnm1)
        {
            long ret = 0;
            try
            {
                string str1 = "Select Max(" + fldnm1 + ") From " + Tblnm1;

                ret = GetTotalNumberofRecord(str1);
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetMaxRecordFromTable()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ret = 0;
            }

            finally
            {
                //CloseMsSqlConnection();
            }

            return ret;
        }

        public double Null2Dbl(object Numbr1)
        {
            Double Amt1;
            try
            {

                if (DBNull.Value == Numbr1)
                {
                    Amt1 = 0;
                }

                else if ((string.IsNullOrEmpty(Numbr1.ToString()) || (Numbr1.ToString().Trim() == "")))
                {
                    Amt1 = 0;
                }
                else
                {
                    Amt1 = (double)Numbr1;
                }

                return (double)Amt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Null2Dbl()");
                return 0;
            }
        }

        public long Null2lng(object Numbr1)
        {
            long Amt1;
            try
            {

                if (DBNull.Value == Numbr1)
                {
                    Amt1 = 0;
                }

                else if ((string.IsNullOrEmpty(Numbr1.ToString()) || (Numbr1.ToString().Trim() == "")))
                {
                    Amt1 = 0;
                }
                else
                {
                    Amt1 = Convert.ToInt64(Numbr1);
                }

                return Convert.ToInt64(Amt1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Null2lng()");
                return 0;
            }
        }

        public DateTime Null2Date(object Dt1)
        {
            DateTime tDt1;
            //DateTime eDt1;
            bool IsEmpDateYn;
            try
            {
                if (DBNull.Value == Dt1)
                {
                    IsEmpDateYn = true;
                }
                else if (Dt1.ToString() == "1/1/1753")
                {
                    IsEmpDateYn = true;
                }
                else if ((Dt1.ToString().Trim() == ""))
                {
                    IsEmpDateYn = true;
                }
                else
                {
                    IsEmpDateYn = false;
                }
                if ((IsEmpDateYn == true))
                {
                    tDt1 = DateTime.MinValue;
                }
                else
                {
                    tDt1 = ((DateTime)(Dt1));
                }

                return tDt1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Null2Date()");
                return (DateTime)Dt1;
            }
        }

        public string Null2Str(object Str1)
        {
            string tStr1;
            try
            {
                if (DBNull.Value == (Str1))
                {
                    tStr1 = " ";
                }
                else if (string.IsNullOrEmpty(Str1.ToString()))
                {
                    tStr1 = " ";
                }
                else
                {
                    tStr1 = Str1.ToString();
                }
                return tStr1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Null2Str()");
                return Str1.ToString();
            }
        }

        public string GetFieldType(string qry1)
        {
            object str;
            string str1;
            try
            {
                OpenMsSqlDataConnection();
                mscmd.Connection = mssqlcon;
                mscmd.CommandText = qry1;
                str = mscmd.ExecuteScalar();
                str1 = Convert.ToString(str);
                return str1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetFieldType()");
                return "";
            }
        }

        //Converts the DataGridView to DataTable
        public DataTable DataGridView2DataTable(DataGridView dgv, String tblName, int minRow = 0)
        {
            DataTable dt = new DataTable(tblName);

            try
            {

                // Header columns
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    DataColumn dc = new DataColumn(column.Name.ToString());
                    dt.Columns.Add(dc);
                }

                // Data cells
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    if (dgv.Rows[i].Visible)
                    {
                        DataGridViewRow row = dgv.Rows[i];
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                        }
                        dt.Rows.Add(dr);
                    }

                    else
                    {
                        DataGridViewRow row = dgv.Rows[i];
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            if (dgv.Columns[j].Name == "Dgvdelflg")
                            {
                                dr[j] = 1;
                            }
                            else
                            {
                                dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }

                // Related to the bug arround min size when using ExcelLibrary for export
                for (int i = dgv.Rows.Count; i < minRow; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dr[j] = "  ";
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in DataGridView2DataTable())");
                return dt;
            }
        }

        //Converts the DataGridView to DataTable
        public DataTable DataGridViewToDataTable(DataGridView dgv, String tblName, int minRow = 0)
        {

            DataTable dt = new DataTable(tblName);

            // Header columns
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                DataColumn dc = new DataColumn(column.Name.ToString());
                dt.Columns.Add(dc);
            }

            // Data cells
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow row = dgv.Rows[i];
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                dt.Rows.Add(dr);
            }

            // Related to the bug arround min size when using ExcelLibrary for export
            for (int i = dgv.Rows.Count; i < minRow; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dr[j] = "  ";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }


        #endregion
    }
}
