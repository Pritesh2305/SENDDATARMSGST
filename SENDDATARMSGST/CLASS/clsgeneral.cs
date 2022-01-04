using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace SENDDATARMSGST
{
    class clsgeneral : clspublicvariable
    {
        private clsmssqldbfunction mssql = new clsmssqldbfunction();

        public static bool CHEKC_RMS_VALIDITY()
        {
            bool retflg;
            string str1;
            clsmssqldbfunction mssqlDB = new clsmssqldbfunction();
            DataTable dttemp = new DataTable();

            string validitytype, strupdate, strinsert;
            DateTime validitydate;
            try
            {
                retflg = true;

                str1 = "SELECT * FROM MSTTABLEINFO";

                dttemp = mssqlDB.FillDataTable(str1, "MSTTABLEINFO");

                if (dttemp.Rows.Count > 0)
                {
                    validitytype = dttemp.Rows[0]["CHECKYN"] + "".Trim();
                    DateTime.TryParse(dttemp.Rows[0]["CHECKDATE"] + "".Trim(), out validitydate);

                    if (validitytype == clspublicvariable.VALIDITYTYPEVALUE)
                    {
                        if (validitydate <= DateTime.Today.Date)
                        {
                            if (validitytype != clspublicvariable.VALIDITYTYPEVALUE)
                            {
                                strupdate = "Update MSTTABLEINFO set CHECKYN = '" + clspublicvariable.VALIDITYTYPEVALUE + "'";
                                mssqlDB.ExecuteMsSqlCommand(strupdate);
                                //retflg = false;
                            }
                            retflg = false;
                        }
                    }
                }

                else
                {
                    DateTime amcvaliditydate;
                    amcvaliditydate = DateTime.Today.Date.AddDays(clspublicvariable.AMCDAYS);
                    strinsert = "INSERT INTO MSTTABLEINFO (CHECKYN,CHECKDATE) values ('" + clspublicvariable.VALIDITYTYPEVALUE + "'" + ",'" + amcvaliditydate.Date.ToString("yyyyMMdd") + "')";
                    mssqlDB.ExecuteMsSqlCommand(strinsert);
                }

                return retflg;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in CHEKC_RMS_VALIDITY())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public static void StartOSK()
        {
            try
            {
                Version win8version = new Version(6, 2, 9200, 0);
                if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                            Environment.OSVersion.Version >= win8version)
                {

                    string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
                    string keyboardPath = Path.Combine(progFiles, "TabTip.exe");

                    Process keyboardProc = Process.Start(keyboardPath);
                    // its win8 or higher.
                }
                else
                {

                    string windir = Environment.GetEnvironmentVariable("WINDIR");
                    string osk = null;

                    if (osk == null)
                    {
                        osk = Path.Combine(Path.Combine(windir, "sysnative"), "osk.exe");
                        if (!File.Exists(osk))
                        {
                            osk = null;
                        }
                    }

                    if (osk == null)
                    {
                        osk = Path.Combine(Path.Combine(windir, "system32"), "osk.exe");
                        if (!File.Exists(osk))
                        {
                            osk = null;
                        }
                    }

                    if (osk == null)
                    {
                        osk = "osk.exe";
                    }

                    Process.Start(osk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in StartOSK())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool FillGeneralSetting()
        {
            string str1;
            DataTable dtsetting = new DataTable();
            try
            {
                GENLOCALSERVERNAME = "";
                GENLOCALDBNAME = "";
                GENLOCALLOGIN = "";
                GENLOCALPASSWORD = "";
                GENLOCALPORT = "";
                GENONLINESERVERNAME = "";
                GENONLINEDBNAME = "";
                GENONLINELOGIN = "";
                GENONLINEPASSWORD = "";
                GENONLINEPORT = "";
                GENDISPLOG = "";

                DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["STARTDATE"] + "", out GENSTARTDATE);
                GENDISPLOG = System.Configuration.ConfigurationManager.AppSettings["DISPLOG"] + "";

                str1 = "SELECT * FROM RMSSETTING";
                dtsetting = mssql.FillDataTable(str1, "RMSSETTING");

                if (dtsetting.Rows.Count > 0)
                {
                    foreach (DataRow row in dtsetting.Rows)
                    {
                        GENLOCALSERVERNAME = (row["LOCALSERVERNAME"]) + "".Trim();
                        GENLOCALDBNAME = (row["LOCALDBNAME"]) + "".Trim();
                        GENLOCALLOGIN = (row["LOCALLOGIN"]) + "".Trim();
                        GENLOCALPASSWORD = (row["LOCALPASSWORD"]) + "".Trim();
                        GENLOCALPORT = (row["LOCALPORT"]) + "".Trim();
                        GENONLINESERVERNAME = (row["ONLINESERVERNAME"]) + "".Trim();
                        GENONLINEDBNAME = (row["ONLINEDBNAME"]) + "".Trim();
                        GENONLINELOGIN = (row["ONLINELOGIN"]) + "".Trim();
                        GENONLINEPASSWORD = (row["ONLINEPASSWORD"]) + "".Trim();
                        GENONLINEPORT = (row["ONLINEPORT"]) + "".Trim();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillGeneralSetting())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public bool FillSTARTDATE()
        {
            clsmaindbfunction clsmaindb = new clsmaindbfunction();
            DataTable dtinfo1 = new DataTable();
            try
            {
                string str1 = "";
                str1 = " SELECT STARTDATE FROM MSTUSERS WHERE DBNAME = '" + clspublicvariable.GENONLINEDBNAME + "'";
                dtinfo1 = clsmaindb.FillMainDataTable(str1, "MSTUSERS");

                if (dtinfo1.Rows.Count > 0)
                {
                     DateTime.TryParse(dtinfo1.Rows[0]["STARTDATE"]+"",out GENSTARTDATE);                    
                }

                return true;
            }
            catch (Exception)
            {
                DateTime.TryParse(System.Configuration.ConfigurationManager.AppSettings["STARTDATE"] + "", out GENSTARTDATE);
                return false;
            }
        }

        public bool Check_LocalConnection(string sernm1, string dbnm1, string loginnm1, string pass1, string port1)
        {
            SqlConnection mssqllcalcon;
            string connstr1 = "";
            bool retval = false;
            try
            {
                mssqllcalcon = new SqlConnection();

                if (mssqllcalcon.State == ConnectionState.Closed)
                {
                    mssqllcalcon.Close();

                    connstr1 = "Data Source=" + sernm1 + ";Initial Catalog=" + dbnm1 + ";Persist Security Info=True;User ID=" + loginnm1 + ";Password=" + pass1;

                    mssqllcalcon.ConnectionString = connstr1;
                    mssqllcalcon.Open();

                    if (mssqllcalcon.State == System.Data.ConnectionState.Open)
                    {
                        retval = true;
                    }
                }

                return retval;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Check_LocalConnection())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public bool Check_OnlineConnection(string sernm1, string dbnm1, string loginnm1, string pass1, string port1)
        {
            SqlConnection mssqllcalcon;
            string connstr1 = "";
            bool retval = false;
            try
            {
                mssqllcalcon = new SqlConnection();

                if (mssqllcalcon.State == ConnectionState.Closed)
                {
                    mssqllcalcon.Close();

                    connstr1 = "Data Source=" + sernm1 + ";Initial Catalog=" + dbnm1 + ";Persist Security Info=True;User ID=" + loginnm1 + ";Password=" + pass1;

                    mssqllcalcon.ConnectionString = connstr1;
                    mssqllcalcon.Open();

                    if (mssqllcalcon.State == System.Data.ConnectionState.Open)
                    {
                        retval = true;
                    }
                }

                return retval;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in Check_OnlineConnection())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public void GETSTARTDATE()
        {
            try
            {


            }
            catch (Exception)
            {
            }
        }

    }
}
