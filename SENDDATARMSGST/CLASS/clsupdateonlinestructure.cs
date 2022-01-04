using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace SENDDATARMSGST
{
    class clsupdateonlinestructure
    {

        private clsupdatelocalstructure clslocaldb = new clsupdatelocalstructure();
        public static SqlConnection mssqlonlinecon = new SqlConnection();
        public SqlCommand mscmd = new SqlCommand();
        public SqlDataAdapter msonlinedtp = new SqlDataAdapter();

        # region PRIVATE FUNCTION

        private bool OpenMsSqlOnlineDataConnection()
        {
            string connstr1 = "";
            try
            {
                mssqlonlinecon = new SqlConnection();

                if (mssqlonlinecon.State == ConnectionState.Closed)
                {
                    connstr1 = "Data Source=" + clspublicvariable.GENONLINESERVERNAME + ";Initial Catalog=" + clspublicvariable.GENONLINEDBNAME + ";Persist Security Info=True;User ID=" + clspublicvariable.GENONLINELOGIN + ";Password=" + clspublicvariable.GENONLINEPASSWORD;
                    mssqlonlinecon.ConnectionString = connstr1;
                    mssqlonlinecon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlOnlineDataConnection())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        #endregion

        # region PUBLIC FUNCTION

        public bool OpenMsSqlOnlineConnection()
        {
            try
            {

                if (string.IsNullOrEmpty(mssqlonlinecon.ConnectionString))
                {
                    this.OpenMsSqlOnlineDataConnection();
                }

                if (mssqlonlinecon.State == ConnectionState.Closed)
                {
                    mssqlonlinecon.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString() + " Error occures in OpenMsSqlOnlineConnection()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public bool CloseMsSqlOnlineConnection()
        {
            try
            {
                if (mssqlonlinecon.State == ConnectionState.Open)
                {
                    mssqlonlinecon.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString() + " Error occures in CloseMsSqlOnlineConnection()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

        }

        public bool ExecuteMsSqlOnlineCommand_NoMsg(string cmdStr)
        {
            try
            {
                this.OpenMsSqlOnlineConnection();
                mscmd.Connection = mssqlonlinecon;
                mscmd.CommandText = cmdStr;
                mscmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        public DataTable FillOnlineDataTable(string cmdstr, string tblnm1)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                this.OpenMsSqlOnlineConnection();

                msonlinedtp = new SqlDataAdapter(cmdstr, mssqlonlinecon);
                msonlinedtp.Fill(ds, "test");
                dt = ds.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in FillOnlineDataTable()", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return dt;
            }
            finally
            {
                //CloseMsSqlConnection();
            }
        }

        #endregion

        #region CREATE TABLE

        public bool Create_OnlineDb_Table()
        {
            string strcreate1 = "";
            try
            {
                strcreate1 = " CREATE TABLE MSTCITY( " +
                              "  CITYID bigint," +
                               " CITYNAME nvarchar(100)" +
                               ")";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = " CREATE TABLE MSTCOUNTRY(" +
                                " COUNTRYID bigint," +
                                " COUNTRYNAME nvarchar(100) " +
                            " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = " CREATE TABLE  MSTSTATE (" +
                                 " STATEID   bigint ," +
                                 " STATENAME   varchar (100)" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTTABLEINFO " +
                               " ( " +
                                    " CHECKYN NVARCHAR(100), " +
                                    " CHECKDATE DATETIME " +
                               " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTUSERS" +
                                " (" +
                                " RID BIGINT," +
                                " USERNAME NVARCHAR(50)," +
                                " PASSWORD NVARCHAR(50)," +
                                " REPASSWORD NVARCHAR(50)," +
                                " USERDESC NVARCHAR(MAX)," +
                                " AUSERID BIGINT," +
                                " ADATETIME DATETIME," +
                                " EUSERID BIGINT," +
                                " EDATETIME DATETIME," +
                                " DUSERID BIGINT," +
                                " DDATETIME DATETIME," +
                                " DELFLG BIT, " +
                                " USERCODE NVARCHAR(50)," +
                                " ISSHOWREPORT BIT, " +
                                " HIDEMSTMNU BIT, " +
                                " HIDEBANQMNU BIT, " +
                                " HIDECRMMNU BIT, " +
                                " HIDESMSMNU BIT," +
                                " HIDEPAYROLLMNU BIT," +
                                " HIDEUTILITYMNU BIT," +
                                " HIDEKOTBILLENT BIT," +
                                " HIDECASHMENOENT BIT," +
                                " HIDESETTLEMENTENT BIT," +
                                " HIDEQUICKBILLENT BIT," +
                                " HIDETABLEVIEWENT BIT," +
                                " HIDEMULTISETTLEMENTENT BIT," +
                                " HIDECASHONHANDENT BIT," +
                                " HIDESUPPLIERENT BIT," +
                                " HIDEPURCHASEENT BIT," +
                                " HIDEPAYMENYENT BIT," +
                                " HIDECHECKLISTENT BIT," +
                                " HIDEBUSINESSSUMMARY BIT," +
                                " DONTALLOWBILLEDIT BIT," +
                                " DONTALLOWBILLDELETE BIT," +
                                " DONTALLOWTBLCLEAR BIT," +
                                " HIDESTKISSUEENT BIT," +
                                " DONTALLOWBANQBOEDIT BIT," +
                                " DONTALLOWBANQBODELETE BIT," +
                                " DONTALLOWCHKLISTEDIT BIT," +
                                " DONTALLOWCHKLISTDELETE BIT," +
                                " DONTALLOWPURCHASEEDIT BIT," +
                                " DONTALLOWPURCHASEDELETE BIT," +
                                " DONTALLOWSTKISSUEEDIT BIT," +
                                " DONTALLOWSTKISSUEDELETE BIT," +
                                " HIDEPURITEMGROUPENT BIT," +
                                " HIDEPURITEMENT BIT," +
                                " DONTALLOWCHANGEDATEINKOTBILL BIT," +
                                " HIDEPURCHASEICO BIT," +
                                " ISTABLETUSER BIT," +
                                " DONTALLOWKOTEDIT BIT," +
                                " DONTALLOWKOTDELETE BIT," +
                                " DONTALLOWDISCINBILL BIT," +
                                " HIDEINVMNU BIT,SENDDATA BIT " +
                                " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTUNIT" +
                                " (" +
                                " RID BIGINT," +
                                " UNITCODE NVARCHAR(20)," +
                                " UNITNAME NVARCHAR(20)," +
                                " UNITDESC NVARCHAR(MAX)," +
                                " AUSERID BIGINT," +
                                " ADATETIME DATETIME," +
                                " EUSERID BIGINT," +
                                " EDATETIME DATETIME," +
                                " DUSERID BIGINT," +
                                " DDATETIME DATETIME," +
                                " DELFLG BIT, " +
                                " SENDDATA BIT" +
                                " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTDEPT " +
                                " (" +
                                " RID BIGINT," +
                                " DEPTCODE NVARCHAR(20)," +
                                " DEPTNAME NVARCHAR(50)," +
                                " DEPTDESC NVARCHAR(MAX)," +
                                " AUSERID BIGINT," +
                                " ADATETIME DATETIME," +
                                " EUSERID BIGINT," +
                                " EDATETIME DATETIME," +
                                " DUSERID BIGINT," +
                                " DDATETIME DATETIME," +
                                " DELFLG BIT," +
                                " ISBARDEPT BIT," +
                                " ISKITCHENDEPT BIT," +
                                " ISHUKKADEPT BIT," +
                                " SENDDATA BIT" +
                                " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTREPORTDEPT " +
                                " (" +
                                     " RID   bigint  ," +
                                     " REPORTDEPTCODE   nvarchar (20)," +
                                     " REPORTDEPTNAME   nvarchar (100)," +
                                     " REPORTDEPTDESC   nvarchar (max)," +
                                     " AUSERID   bigint  NULL," +
                                     " ADATETIME   datetime  NULL," +
                                     " EUSERID   bigint  NULL," +
                                     " EDATETIME   datetime  NULL," +
                                     " DUSERID   bigint  NULL," +
                                     " DDATETIME   datetime  NULL," +
                                     " DELFLG   bit  NULL,SENDDATA BIT " +
                                " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTHSNCODE(" +
                             " RID   bigint ," +
                             " HSNCODE  nvarchar (50)," +
                             " HSNCODEREMARK nvarchar (200)," +
                             " ACTOTGSTPER  decimal (18, 3) ," +
                             " ACCGSTPER   decimal (18, 3) ," +
                             " ACSGSTPER   decimal (18, 3) ," +
                             " ACIGSTPER   decimal (18, 3) ," +
                             " NACTOTGSTPER  decimal (18, 3) ," +
                             " NACCGSTPER   decimal (18, 3)  ," +
                             " NACSGSTPER   decimal (18, 3)  ," +
                             " NACIGSTPER   decimal (18, 3)  ," +
                             " CATOTGSTPER   decimal (18, 3)  ," +
                             " CACGSTPER   decimal (18, 3)  ," +
                             " CASGSTPER   decimal (18, 3)  ," +
                             " CAIGSTPER   decimal (18, 3)  ," +
                             " RSTOTGSTPER Decimal (18, 3)  ," +
                             " RSCGSTPER   decimal (18, 3)  ," +
                             " RSSGSTPER   Decimal (18, 3)  ," +
                             " RSIGSTPER   Decimal (18, 3)  ," +
                             " PURTOTGSTPER Decimal (18, 3)  ," +
                             " PURCGSTPER   decimal (18, 3)  ," +
                             " PURSGSTPER   decimal (18, 3)  ," +
                             " PURIGSTPER   decimal (18, 3)  ," +
                             " BANQTOTGSTPER   decimal (18, 3) ," +
                             " BANQCGSTPER   decimal (18, 3)  ," +
                             " BANQSGSTPER   decimal (18, 3)  ," +
                             " BANQIGSTPER   decimal (18, 3)  ," +
                             " AUSERID   bigint ," +
                             " ADATETIME   datetime   ," +
                             " EUSERID   bigint ," +
                             " EDATETIME   datetime   ," +
                             " DUSERID   bigint ," +
                             " DDATETIME   datetime ," +
                             " DELFLG   bit ," +
                             " SENDDATA BIT )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE MSTITEMGROUP " +
                                " (" +
                                     " RID   bigint ," +
                                     " IGCODE   nvarchar (50), " +
                                     " IGNAME   nvarchar (200)," +
                                     " IGDESC   nvarchar (max)," +
                                     " AUSERID   bigint  NULL," +
                                     " ADATETIME   datetime  NULL," +
                                     " EUSERID   bigint  NULL," +
                                     " EDATETIME   datetime  NULL," +
                                     " DUSERID   bigint  NULL," +
                                     " DDATETIME   datetime  NULL," +
                                     " DELFLG   bit ," +
                                     " NOTAPPLYDISC   bit  NULL," +
                                     " IGPNAME   nvarchar (200)," +
                                     " IGDISPORD   bigint  NULL," +
                                     " ISHIDEGROUP   bigint  NULL," +
                                     " SHOWINDIFFCOLOR   bit  NULL," +
                                     " ISHIDEGROUPKOT   bit  NULL," +
                                     " ISITEMREMGRP   bit  NULL," +
                                     " ISHIDEGROUPCASHMEMO   bit  NULL," +
                                     " HSNCODERID   bigint  NULL," +
                                     " IGBACKCOLOR   bigint  NULL," +
                                     " IGFORECOLOR   bigint  NULL," +
                                     " IGFONTNAME   nvarchar (200)," +
                                     " IGFONTSIZE   float  NULL," +
                                     " IGFONTBOLD   bit  NULL," +
                                     " IGPRINTORD   bigint  NULL," +
                                     " REGLANGIGNAME   nvarchar (200) ," +
                                    " SENDDATA BIT " +
                                   " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTITEM (" +
                                 " RID   bigint," +
                                 " ICODE   nvarchar (20)," +
                                 " INAME   nvarchar (250)," +
                                 " IIMG   image   ," +
                                 " IGRPRID   bigint   ," +
                                 " IUNITRID   bigint   ," +
                                 " IDEPTRID   bigint   ," +
                                 " IRATE   decimal (18, 2)  ," +
                                 " IDESC   nvarchar (max) ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                 " ICOMMI   decimal (18, 2)  ," +
                                 " IPNAME   nvarchar (200)," +
                                 " IMINQTY   decimal (18, 2)  ," +
                                 " IMAXQTY   decimal (18, 2)  ," +
                                 " IREQTY   decimal (18, 2)  ," +
                                 " ISITEMSTOCK   bit   ," +
                                 " REPORTDEPTRID   bigint   ," +
                                 " ISHIDEITEM   bigint   ," +
                                 " ISNOTAPPSERTAXDISC   bigint ," +
                                 " IDPRATE   decimal (18, 2)  ," +
                                 " ISRUNNINGITEM   bit   ," +
                                 " DISDIFFCOLOR   bit   ," +
                                 " ITEMTAXTYPE   nvarchar (50)," +
                                 " ISCHKRPTITEM   bit   ," +
                                 " ISNOTAPPLYVAT   bit   ," +
                                 " COUPONCODE   nvarchar(20)," +
                                 " IREWPOINT   decimal(18,3)," +
                                 " NOTAPPLYGSTTAX   bit   ," +
                                 " NOTAPPLYDISC   bit   ," +
                                 " HSNCODERID   bigint   ," +
                                 " IBACKCOLOR   bigint   ," +
                                 " IFORECOLOR   bigint   ," +
                                 " IFONTNAME   nvarchar (200)," +
                                 " IFONTSIZE   float   ," +
                                 " IFONTBOLD   bit   ," +
                                 " REGLANGNAME   nvarchar (200) ," +
                                 " IREMARK   nvarchar (2000)," +
                                 " SENDDATA BIT" +
                             " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTITEMPRICELIST" +
                                " ( " +
                                 " RID bigint," +
                                 " IPLCODE nvarchar(20)," +
                                 " IPLNAME nvarchar(50)," +
                                 " IPLDESC nvarchar (max)," +
                                 " AUSERID bigint," +
                                 " ADATETIME datetime," +
                                 " EUSERID  bigint," +
                                 " EDATETIME datetime," +
                                 " DUSERID  bigint," +
                                 " DDATETIME datetime," +
                                 " DELFLG  bigint," +
                                 " SENDDATA bit" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTITEMPRICELISTDTL (" +
                                     " RID   bigint  ," +
                                     " IPLRID   bigint  ," +
                                     " IRID   bigint  ," +
                                     " IRATE   decimal (18, 2) ," +
                                     " IPRATE   decimal (18, 2) ," +
                                     " AUSERID   bigint  ," +
                                     " ADATETIME   datetime  ," +
                                     " EUSERID   bigint  ," +
                                     " EDATETIME   datetime  ," +
                                     " DUSERID   bigint  ," +
                                     " DDATETIME   datetime  ," +
                                     " DELFLG   bigint  ," +
                                     " SENDDATA   bit  " +
                                 " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE MSTREMARK " +
                            " ( RID bigint," +
                            " REMARKCODE nvarchar(20), " +
                            " REMARKNAME nvarchar(100)," +
                            " REMARKDESC nvarchar(max)," +
                            " AUSERID bigint," +
                            " ADATETIME datetime," +
                            " EUSERID bigint," +
                            " EDATETIME datetime," +
                            " DUSERID bigint," +
                            " DDATETIME datetime," +
                            " DELFLG bit,SENDDATA BIT)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTTABLE (" +
                      " RID   bigint ," +
                      " TABLECODE   nvarchar (20)  ," +
                      " TABLENAME   nvarchar (50)  ," +
                      " TABLEDESC   nvarchar (max) ," +
                      " AUSERID   bigint   ," +
                      " ADATETIME   datetime   ," +
                      " EUSERID   bigint   ," +
                      " EDATETIME   datetime   ," +
                      " DUSERID   bigint   ," +
                      " DDATETIME   datetime   ," +
                      " DELFLG bit  ," +
                      " ISROOMTABLE   bit   ," +
                      " ISPARCELTABLE   bit   ," +
                      " ROOMNO   nvarchar (100) ," +
                      " TABDISC   decimal (18, 2)  ," +
                      " PRICELISTRID   bigint   ," +
                      " ISHOMEDELITABLE   bit   ," +
                      " ISNOTCALCCOMMI   bit   ," +
                      " ISHIDETABLE   bit   ," +
                      " TABPAX   bigint   ," +
                      " TABDISPORD   bigint  ," +
                      " GSTTAXTYPE   nvarchar (100)," +
                      " TABLEGROUP   nvarchar (500)," +
                      " SECNO   nvarchar (50)," +
                      " MSTTIEUPCOMPRID bigint," +
                      " SENDDATA BIT " +
                  " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE MSTEMPCAT (" +
                                 " RID   bigint ," +
                                 " EMPCATCODE   nvarchar (20) ," +
                                 " EMPCATNAME   nvarchar (200) ," +
                                 " EMPCATDESC   nvarchar (max) ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                " SENDDATA BIT" +
                             " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTEMP (" +
                                 " RID   bigint  ," +
                                 " EMPCODE   nvarchar (20)  ," +
                                 " EMPNAME   nvarchar (250)  ," +
                                 " EMPFATHERNAME   nvarchar (250)  ," +
                                 " EMPADD1   nvarchar (200)  ," +
                                 " EMPADD2   nvarchar (200)  ," +
                                 " EMPADD3   nvarchar (200)  ," +
                                 " EMPCATID   bigint   ," +
                                 " EMPCITYID   bigint   ," +
                                 " EMPSTATEID   bigint   ," +
                                 " EMPCOUNTRYID   bigint   ," +
                                 " EMPPIN   nvarchar (50)  ," +
                                 " EMPTELNO   nvarchar (50)  ," +
                                 " EMPMOBILENO   nvarchar (50)  ," +
                                 " EMPEMAIL   nvarchar (200)  ," +
                                 " EMPFAXNO   nvarchar (50)  ," +
                                 " EMPBIRTHDATE   datetime   ," +
                                 " EMPJOINDATE   datetime   ," +
                                 " EMPLEAVEDATE   datetime   ," +
                                 " EMPGENDER   nvarchar (50)  ," +
                                 " EMPMARITALSTATUS   nvarchar (50)  ," +
                                 " ISDISPINKOT   bit   ," +
                                 " EMPIMAGE   image   ," +
                                 " EMPDESC   nvarchar (max)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                 " EMPANNIDATE   datetime   ," +
                                 " EMPBANKNAME   nvarchar (250)  ," +
                                 " EMPBANKDETAILS   nvarchar (250)  ," +
                                 " EMPBANKACCNO   nvarchar (250)  ," +
                                 " ISNONACTIVE   bit   ," +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE  KOT (" +
                                 " RID   bigint ," +
                                 " KOTDATE   datetime   ," +
                                 " KOTTIME   datetime   ," +
                                 " KOTNO   nvarchar (100)   ," +
                                 " KOTTOKNO   nvarchar (100)   ," +
                                 " KOTORDERPERID   bigint   ," +
                                 " ISPARSELKOT   bit   ," +
                                 " KOTTABLEID   bigint   ," +
                                 " KOTTABLENAME   nvarchar (100)   ," +
                                 " KOTREMARK   nvarchar (max)   ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                 " KOTPAX   bigint   ," +
                                 " CUSTRID   bigint   ," +
                                 " CUSTNAME   nvarchar (250)   ," +
                                 " CUSTADD   nvarchar (10)   ," +
                                 " KOTINFO   nvarchar (max)   ," +
                                 " CARDNO   nvarchar (50)   ," +
                                 " REFKOTNO   nvarchar (50)   ," +
                                 " REFKOTNUM   bigint   ," +
                                 " ISCOMPKOT   bit   ," +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE KOTDTL (" +
                                 " RID   bigint," +
                                 " KOTRID  bigint ," +
                                 " IRID   bigint   ," +
                                 " INAME  nvarchar (500) ," +
                                 " IQTY   decimal (18, 2) ," +
                                 " IRATE  decimal (18, 2) ," +
                                 " IAMT   decimal (18, 2) ," +
                                 " AUSERID bigint  ," +
                                 " ADATETIME datetime ," +
                                 " EUSERID   bigint  ," +
                                 " EDATETIME datetime ," +
                                 " DUSERID   bigint  ," +
                                 " DDATETIME datetime ," +
                                 " DELFLG   bit  ," +
                                 " IREMARK  nvarchar (300)  ," +
                                 " IMODIFIER  nvarchar (max) ," +
                                 " ICOMPITEM  bit, " +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE  BILL ( " +
                              " RID   bigint  ," +
                             " BILLNO   nvarchar (50)    ," +
                             " BILLDATE   datetime   ," +
                             " BILLTYPE   nvarchar (50)    ," +
                             " CUSTRID   bigint   ," +
                             " CUSTNAME   nvarchar (250)    ," +
                             " CUSTCONTNO   nvarchar (250)    ," +
                             " TABLERID   bigint   ," +
                             " PRICELISTRID   bigint   ," +
                             " BILLPAX   bigint   ," +
                             " TOTAMOUNT   decimal (18, 2)  ," +
                             " TOTSERTAXPER   decimal (18, 2)  ," +
                             " TOTSERTAXAMOUNT   decimal (18, 2)  ," +
                             " TOTVATPER   decimal (18, 2)  ," +
                             " TOTVATAMOUNT   decimal (18, 2)  ," +
                             " TOTADDVATPER   decimal (18, 2)  ," +
                             " TOTADDVATAMOUNT   decimal (18, 2)  ," +
                             " TOTDISCPER   decimal (18, 2)  ," +
                             " TOTDISCAMOUNT   decimal (18, 2)  ," +
                             " TOTROFF   decimal (18, 2)  ," +
                             " NETAMOUNT   decimal (18, 2)  ," +
                             " BILLPREPBY   nvarchar (500)    ," +
                             " BILLREMARK   nvarchar (max)    ," +
                             " SETLETYPE   nvarchar (50)    ," +
                             " SETLEAMOUNT   decimal (18, 2)  ," +
                             " CHEQUENO   nvarchar (100)    ," +
                             " CHEQUEBANKNAME   nvarchar (100)    ," +
                             " CREDITCARDNO   nvarchar (100)    ," +
                             " CREDITHOLDERNAME   nvarchar (100)    ," +
                             " CREDITBANKNAME   nvarchar (100)    ," +
                             " AUSERID   bigint   ," +
                             " ADATETIME   datetime   ," +
                             " EUSERID   bigint   ," +
                             " EDATETIME   datetime   ," +
                             " DUSERID   bigint   ," +
                             " DDATETIME   datetime   ," +
                             " DELFLG   bit ," +
                             " REFBILLNO   nvarchar (50)    ," +
                             " REFBILLNUM   bigint   ," +
                             " custadd   nvarchar (max)    ," +
                             " TOTDISCOUNTABLEAMOUNT  decimal (18, 2)  ," +
                             " ISREVISEDBILL   bigint   ," +
                             " REVISEDBILLSRNO   bigint   ," +
                             " REVISEDBILLNO   bigint   ," +
                             " BASEBILLNO   bigint   ," +
                             " MAINBASEBILLNO   bigint   ," +
                             " LASTTABLERID   bigint   ," +
                             " LASTTABLESTATUS   bigint   ," +
                             " BILLORDERPERID   bigint   ," +
                             " BILLTIME   datetime   ," +
                             " TOTCALCVATPER   decimal (18, 2)  ," +
                             " TOTCALCVATAMOUNT   decimal (18, 2)  ," +
                             " ISBILLTOCUSTOMER   bit   ," +
                             " CNTPRINT   bigint   ," +
                             " TOKENNO   bigint   ," +
                             " ISPARCELBILL   bit   ," +
                             " ISCOMPLYBILL   bit   ," +
                             " TOTBEVVATPER   decimal (18, 3)  ," +
                             " TOTBEVVATAMT   decimal (18, 2)  ," +
                             " TOTLIQVATPER   decimal (18, 3)  ," +
                             " TOTLIQVATAMT   decimal (18, 2)  ," +
                             " REFBYRID   bigint   ," +
                             " BILLINFO   nvarchar (max)  ," +
                             " TOTSERCHRPER   decimal (18, 3)  ," +
                             " TOTSERCHRAMT   decimal (18, 3)  ," +
                             " TOTNEWTOTALAMT   decimal (18, 3)  ," +
                             " TOTADDDISCAMT   decimal (18, 3)  ," +
                             " MSTTIEUPCOMPRID   bigint   ," +
                             " COUPONNO   nvarchar (200)    ," +
                             " COUPONPERNAME   nvarchar (200)    ," +
                             " TOTKKCESSPER   decimal (18, 3)  ," +
                             " TOTKKCESSAMT   decimal (18, 3)  ," +
                             " OTHERPAYMENTBY   nvarchar (200)    ," +
                             " OTHERPAYMENTBYREMARK1   nvarchar (500)    ," +
                             " OTHERPAYMENTBYREMARK2   nvarchar (500)    ," +
                             " PAYMENT   nvarchar (50)    ," +
                             " MULTICASHAMT   decimal (18, 3)  ," +
                             " MULTICHEQUEAMT   decimal (18, 3)  ," +
                             " MULTICREDITCARDAMT   decimal (18, 3)  ," +
                             " MULTIOTHERAMT   decimal (18, 3)  ," +
                             " MULTICHQNO   nvarchar (200)    ," +
                             " MULTICHQBANKNAME   nvarchar (200)    ," +
                             " MULTICARDNO   nvarchar (200)    ," +
                             " MULTICARDBANKNAME   nvarchar (200)    ," +
                             " MULTIOTHERPAYMENTBY   nvarchar (200)    ," +
                             " MULTIOTHERREMARK1   nvarchar (500)    ," +
                             " MULTIOTHERREMARK2   nvarchar (500)    ," +
                             " MULTITIPAMT   decimal (18, 3)  ," +
                             " CARDNO   nvarchar (50)    ," +
                             " TOTBILLREWPOINT   decimal(18,3) ," +
                             " TOTITEMREWPOINT   decimal(18,3) ," +
                             " CGSTAMT   decimal (18, 3)  ," +
                             " SGSTAMT   decimal (18, 3)  ," +
                             " IGSTAMT   decimal (18, 3)  ," +
                             " TOTGSTAMT   decimal (18, 3)  ," +
                             " RECAMT   decimal (18, 3)  ," +
                             " RETAMT   decimal (18, 3)  ," +
                             " SENDDATA BIT" +
                        " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE BILLDTL (" +
                                     " RID   bigint," +
                                     " BILLRID   bigint   ," +
                                     " IRID   bigint   ," +
                                     " KOTRID   bigint   ," +
                                     " IQTY   decimal (18, 3)  ," +
                                     " IRATE   decimal (18, 2)  ," +
                                     " IPAMT   decimal (18, 2)  ," +
                                     " IAMT   decimal (18, 2)  ," +
                                     " AUSERID   bigint   ," +
                                     " ADATETIME   datetime   ," +
                                     " EUSERID   bigint   ," +
                                     " EDATETIME   datetime   ," +
                                     " DUSERID   bigint   ," +
                                     " DDATETIME   datetime   ," +
                                     " DELFLG   bit  ," +
                                     " NOTAPPLYDISC   bigint   ," +
                                     " NOTAPPLYSERTAX   bigint   ," +
                                     " SERTAXTYPE   nvarchar (50)  ," +
                                     " NOTAPPLYVAT   bit   ," +
                                     " DISCPER   decimal (18, 3)  ," +
                                     " DISCAMT   decimal (18, 2)  ," +
                                     " SERTAXPER   decimal (18, 3)  ," +
                                     " SERTAXAMT   decimal (18, 2)  ," +
                                     " FOODVATPER   decimal (18, 3)  ," +
                                     " FOODVATAMT   decimal (18, 2)  ," +
                                     " LIQVATPER   decimal (18, 3)  ," +
                                     " LIQVATAMT   decimal (18, 2)  ," +
                                     " BEVVATPER   decimal (18, 3)  ," +
                                     " BEVVATAMT   decimal (18, 2)  ," +
                                     " SERCHRPER   decimal (18, 3)  ," +
                                     " SERCHRAMT   decimal (18, 2)  ," +
                                     " NEWSERCHRPER   decimal (18, 3)  ," +
                                     " NEWSERCHRAMT   decimal (18, 3)  ," +
                                     " KKCESSPER   decimal (18, 3)  ," +
                                     " KKCESSAMT   decimal (18, 3)  ," +
                                     " IREWPOINTS   decimal(18,3)   ," +
                                     " NOTAPPLYGST   bit   ," +
                                     " CGSTPER   decimal (18, 3)  ," +
                                     " CGSTAMT   decimal (18, 3)  ," +
                                     " SGSTPER   decimal (18, 3)  ," +
                                     " SGSTAMT   decimal (18, 3)  ," +
                                     " IGSTPER   decimal (18, 3)  ," +
                                     " IGSTAMT   decimal (18, 3)  ," +
                                     " GSTPER   decimal (18, 3)  ," +
                                     " GSTAMT   decimal (18, 3)  ," +
                                     " SENDDATA BIT" +
                                " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE SETTLEMENT (" +
                                 " RID   bigint," +
                                 " SETLEDATE   datetime   ," +
                                 " BILLRID   bigint   ," +
                                 " SETLENO   nvarchar (50)  ," +
                                 " SETLETYPE   nvarchar (50)  ," +
                                 " SETLEAMOUNT   decimal (18, 2)  ," +
                                 " CHEQUENO   nvarchar (100)  ," +
                                 " CHEQUEBANKNAME   nvarchar (100)  ," +
                                 " CREDITCARDNO   nvarchar (100)  ," +
                                 " CREDITHOLDERNAME   nvarchar (100)  ," +
                                 " CREDITBANKNAME   nvarchar (100)  ," +
                                 " SETLEPREPBY   nvarchar (100)  ," +
                                 " SETLEREMARK   nvarchar (max)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bigint   ," +
                                 " CUSTRID   bigint   ," +
                                 " ADJAMT   decimal (18, 2)  ," +
                                 " TIPAMT   decimal (18, 3)  ," +
                                 " OTHERPAYMENTBY   nvarchar (200)  ," +
                                 " OTHERPAYMENTBYREMARK1   nvarchar (500)  ," +
                                 " OTHERPAYMENTBYREMARK2   nvarchar (500)  ," +
                                 " SENDDATA   bit   " +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE MSTCUST (" +
                                 " RID   bigint ," +
                                 " CUSTCODE   nvarchar (50)  ," +
                                 " CUSTNAME   nvarchar (250)  ," +
                                 " CUSTADD1   nvarchar (200)  ," +
                                 " CUSTADD2   nvarchar (200)  ," +
                                 " CUSTADD3   nvarchar (200)  ," +
                                 " CUSTCITYID   bigint   ," +
                                 " CUSTSTATEID   bigint   ," +
                                 " CUSTCOUNTRYID   bigint   ," +
                                 " CUSTPIN   nvarchar (50)  ," +
                                 " CUSTTELNO   nvarchar (50)  ," +
                                 " CUSTMOBNO   nvarchar (50)  ," +
                                 " CUSTEMAIL   nvarchar (200)  ," +
                                 " CUSTFAXNO   nvarchar (100)  ," +
                                 " CUSTBIRTHDATE   datetime   ," +
                                 " CUSTGENDER   nvarchar (50)  ," +
                                 " CUSTMARITALSTATUS   nvarchar (50)  ," +
                                 " CUSTANNIDATE   datetime   ," +
                                 " CUSTIMAGE   image   ," +
                                 " CUSTREGDATE   datetime   ," +
                                 " CUSTDESC   nvarchar (max)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit  ," +
                                 " CUSTMOBNO2   nvarchar (50)  ," +
                                 " CUSTMOBNO3   nvarchar (50)  ," +
                                 " CUSTMOBNO4   nvarchar (50)  ," +
                                 " CUSTMOBNO5   nvarchar (50)  ," +
                                 " CUSTAREA   nvarchar (100)  ," +
                                 " CUSTADD2ADD1   nvarchar (250)  ," +
                                 " CUSTADD2ADD2   nvarchar (250)  ," +
                                 " CUSTADD2ADD3   nvarchar (250)  ," +
                                 " CUSTADD2AREA   nvarchar (250)  ," +
                                 " CUSTADD2CITY   bigint   ," +
                                 " CUSTADD2PIN   nvarchar (250)  ," +
                                 " CUSTADD3ADD1   nvarchar (250)  ," +
                                 " CUSTADD3ADD2   nvarchar (250)  ," +
                                 " CUSTADD3ADD3   nvarchar (250)  ," +
                                 " CUSTADD3AREA   nvarchar (250)  ," +
                                 " CUSTADD3CITY   bigint   ," +
                                 " CUSTADD3PIN   nvarchar (250)  ," +
                                 " CUSTADD4ADD1   nvarchar (250)  ," +
                                 " CUSTADD4ADD2   nvarchar (250)  ," +
                                 " CUSTADD4ADD3   nvarchar (250)  ," +
                                 " CUSTADD4AREA   nvarchar (250)  ," +
                                 " CUSTADD4CITY   bigint   ," +
                                 " CUSTADD4PIN   nvarchar (250)  ," +
                                 " CUSTADD5ADD1   nvarchar (250)  ," +
                                 " CUSTADD5ADD2   nvarchar (250)  ," +
                                 " CUSTADD5ADD3   nvarchar (250)  ," +
                                 " CUSTADD5AREA   nvarchar (250)  ," +
                                 " CUSTADD5CITY   bigint   ," +
                                 " CUSTADD5PIN   nvarchar (250)  ," +
                                 " CUSTDISCPER   decimal (18, 2)  ," +
                                 " CUSTLANDMARK   nvarchar (250)  ," +
                                 " CUSTDOCIMAGE   image   ," +
                                 " FOODTOKEN   bit   ," +
                                 " CARDNO   nvarchar (50)  ," +
                                 " CARDACTDATE   datetime   ," +
                                 " CARDENROLLFEES   decimal (18, 3)  ," +
                                 " CARDSTATUS   nvarchar (50)  ," +
                                 " CARDEXPDATE   datetime   ," +
                                 " CARDREMARK   nvarchar (max)  ," +
                                 " GSTNO   nvarchar (50)  ," +
                                 " PANNO   nvarchar (50)  ," +
                                 " APPLYIGST   bit   ," +
                                 " VATNO   nvarchar (50)  ," +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE  RMSSETTING (" +
                                 " AUTOKOTNO   nvarchar (50)  ," +
                                 " AUTOBILLNO   nvarchar (50)  ," +
                                 " PRINTKOT   nvarchar (50)  ," +
                                 " AUTOPURNO   nvarchar (50)  ," +
                                 " AUTOPAYNO   nvarchar (50)  ," +
                                 " PRINTBILL   nvarchar (50)  ," +
                                 " THERMALPRINTERTYPE   nvarchar (50)  ," +
                                 " GENERALTITLE1   nvarchar (4000)  ," +
                                 " GENERALTITLE2   nvarchar (4000)  ," +
                                 " GENERALTITLE3   nvarchar (4000)  ," +
                                 " GENERALTITLE4   nvarchar (4000)  ," +
                                 " GENERALTITLE5   nvarchar (4000)  ," +
                                 " THERMALTITLE1   nvarchar (30)  ," +
                                 " THERMALTITLE2   nvarchar (30)  ," +
                                 " THERMALTITLE3   nvarchar (30)  ," +
                                 " THERMALTITLE4   nvarchar (30)  ," +
                                 " THERMALTITLE5   nvarchar (30)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                 " AUTOSETTNO   nvarchar (50)  ," +
                                 " generalfooteralignment   nvarchar (50)  ," +
                                 " generalfooter1   nvarchar (4000)  ," +
                                 " generalfooter2   nvarchar (4000)  ," +
                                 " generalfooter3   nvarchar (4000)  ," +
                                 " generalfooter4   nvarchar (4000)  ," +
                                 " generalfooter5   nvarchar (4000)  ," +
                                 " thermalfooteralignment   nvarchar (50)  ," +
                                 " thermalfooter1   nvarchar (50)  ," +
                                 " thermalfooter2   nvarchar (50)  ," +
                                 " thermalfooter3   nvarchar (50)  ," +
                                 " thermalfooter4   nvarchar (50)  ," +
                                 " thermalfooter5   nvarchar (50)  ," +
                                 " thermalfooterprinttype   nvarchar (50)  ," +
                                 " sertax   decimal (18, 2)  ," +
                                 " vat   decimal (18, 2)  ," +
                                 " sercharges   decimal (18, 2)  ," +
                                 " disc   decimal (18, 2)  ," +
                                 " AUTOBILL   nvarchar (50)  ," +
                                 " KITPRINTER   nvarchar (200)  ," +
                                 " BILLINGPRINTER   nvarchar (200)  ," +
                                 " REPORTPRINTER   nvarchar (200)  ," +
                                 " PRINTCUSTINFO   bit   ," +
                                 " DEPTKOT   bit   ," +
                                 " KOTTITLE1   nvarchar (50)  ," +
                                 " KOTTITLE2   nvarchar (50)  ," +
                                 " KOTTITLE3   nvarchar (50)  ," +
                                 " KOTTITLE4   nvarchar (50)  ," +
                                 " KOTTITLE5   nvarchar (50)  ," +
                                 " BACKUPLOC   nvarchar (2000)  ," +
                                 " CUTDEPTKOT   bit   ," +
                                 " ALLOWRATE   bit   ," +
                                 " PROPROVIDER   nvarchar (100)  ," +
                                 " PROUSERID   nvarchar (100)  ," +
                                 " PROPASSWORD   nvarchar (100)  ," +
                                 " TRAPROVIDER   nvarchar (100)  ," +
                                 " TRAUSERID   nvarchar (100)  ," +
                                 " TRAPASSWORD   nvarchar (100)  ," +
                                 " SMSSIGN   nvarchar (100)  ," +
                                 " SENDSMS   bigint   ," +
                                 " NOOFBILL   bigint   ," +
                                 " DONOTALLOWBILL   bit   ," +
                                 " HIDEBUSU   bit   ," +
                                 " HIDECOMMI   bit   ," +
                                 " THBILLPTYPE   nvarchar (50)  ," +
                                 " SMTPEMAILADDRESS   nvarchar (200)  ," +
                                 " SMTPEMAILPASSWORD   nvarchar (200)  ," +
                                 " SMTPADDRESS   nvarchar (200)  ," +
                                 " SMTPPORT   nvarchar (50)  ," +
                                 " NOOFKOT   bigint   ," +
                                 " AUTOTOKENNO   nvarchar (50)  ," +
                                 " THKOTPTYPE   nvarchar (100)  ," +
                                 " HIDEREPORT   bit   ," +
                                 " FILLSETTLEMENTAMT   bit   ," +
                                 " CALCVATPER   decimal (18, 2)  ," +
                                 " NOTREQSETTLEMENT   bit   ," +
                                 " ISCHECKPAPERSIZE   bit   ," +
                                 " ISLOGOUTPRINT   bit   ," +
                                 " ISSTOPUSERBILLPRINT   bit   ," +
                                 " ISFOCUSONITEMNAME   bit   ," +
                                 " ISKITDISP   bit   ," +
                                 " CASHMEMOVER   nvarchar (10)  ," +
                                 " ISPAXREQBILL   bit   ," +
                                 " ISKOTSAVEPRINT   bit   ," +
                                 " SMSRESTNAME   nvarchar (50)  ," +
                                 " PROSENDERID   nvarchar (50)  ," +
                                 " HOTELSERVER   nvarchar (500)  ," +
                                 " HOTELDATABASE   nvarchar (500)  ," +
                                 " HOTELUSERID   nvarchar (500)  ," +
                                 " HOTELPASSWORD   nvarchar (500)  ," +
                                 " ISAUTOSETTLEMENT   bit   ," +
                                 " ISROOMCREDIT   bit   ," +
                                 " ISPRINTTOKENNO   bit   ," +
                                 " BARPRINTER   nvarchar (200)  ," +
                                 " DAYENDTIME   bigint   ," +
                                 " MAXDISC   bigint   ," +
                                 " NOTREQRNDOFF   bit   ," +
                                 " NOTREQTAXINPARCEL   bit   ," +
                                 " REQCANCELKOT   bit   ," +
                                 " ISKITCASHPRINT   bit   ," +
                                 " ADDEVERYITMINCASHMEMO   bit   ," +
                                 " MANPASS   nvarchar (50)  ," +
                                 " LIQSERTAX   decimal (18, 2)  ," +
                                 " BANQSERTAX   decimal (18, 2)  ," +
                                 " ISREQREASONINKOT   bit   ," +
                                 " ISREQREASONINBILL   bit   ," +
                                 " ISCHKREPEATITEM   bit   ," +
                                 " DAYAVGPUR   bigint   ," +
                                 " LIQVAT   decimal (18, 3)  ," +
                                 " BEVERVAT   decimal (18, 3)  ," +
                                 " ISBILLSAVEPRINTMSG   bit   ," +
                                 " ALLOWMINUSQTY   bit   ," +
                                 " MUSTENTCLSTK   bit   ," +
                                 " SERCHR   decimal (18, 3)  ," +
                                 " KKCESS   decimal (18, 3)  ," +
                                 " NOTREQSERCHRINPARCEL   bit   ," +
                                 " ISAUTOAMTCALCINPUR   bit   ," +
                                 " BANQSBCESS   decimal (18, 3)  ," +
                                 " BANQKKCESS   decimal (18, 3)  ," +
                                 " NOTREQTBLORDDTL   bit   ," +
                                 " AUTOCASHNO   nvarchar (50)  ," +
                                 " NOOFKOT2   bigint   ," +
                                 " ISKOT2PRINTREQ   bit   ," +
                                 " CUTDEPTKOT2   bit   ," +
                                 " PRINTKOT2   nvarchar (200)  ," +
                                 " KIT2PRINTER   nvarchar (500)  ," +
                                 " THKOT2PTYPE   nvarchar (500)  ," +
                                 " ISCASHMEMOSTICKERPRINT   bit   ," +
                                 " LABLEPRINTER   nvarchar (500)  ," +
                                 " ISITEMMNUALPHAORD   bit   ," +
                                 " HUKKAPRINTER   nvarchar (500)  ," +
                                 " ALLOWWAITINGSMS   bit   ," +
                                 " NOOFCASHMEMOBILL   bigint   ," +
                                 " FIRSTFOCUSONITEMCODE   bit   ," +
                                 " BANQHEADER1   nvarchar (500)  ," +
                                 " BANQHEADER2   nvarchar (500)  ," +
                                 " BANQHEADER3   nvarchar (500)  ," +
                                 " BANQHEADER4   nvarchar (500)  ," +
                                 " BANQHEADER5   nvarchar (500)  ," +
                                 " BANQFOOTER1   nvarchar (500)  ," +
                                 " BANQFOOTER2   nvarchar (500)  ," +
                                 " BANQFOOTER3   nvarchar (500)  ," +
                                 " BANQFOOTER4   nvarchar (500)  ," +
                                 " BANQFOOTER5   nvarchar (500)  ," +
                                 " BILLNOBASEDON   nvarchar (50)  ," +
                                 " REWBILLAMT   bigint   ," +
                                 " REWPOINTS   bigint   ," +
                                 " KOTNOBASEDON   nvarchar (50)  ," +
                                 " GSTTAX1   decimal (18, 3)  ," +
                                 " GSTTAX2   decimal (18, 3)  ," +
                                 " GSTTAX3   decimal (18, 3)  ," +
                                 " BANQGSTTAX1   decimal (18, 3)  ," +
                                 " BANQGSTTAX2   decimal (18, 3)  ," +
                                 " BANQGSTTAX3   decimal (18, 3)  ," +
                                 " GENBILLTITLE   nvarchar (50)  ," +
                                 " GENERALTITLE6   nvarchar (1000)  ," +
                                 " GENERALTITLE7   nvarchar (1000)  ," +
                                 " GENERALTITLE8   nvarchar (1000)  ," +
                                 " THERMALTITLE6   nvarchar (30)  ," +
                                 " THERMALTITLE7   nvarchar (30)  ," +
                                 " THERMALTITLE8   nvarchar (30)  ," +
                                 " GENERALFOOTER6   nvarchar (1000)  ," +
                                 " GENERALFOOTER7   nvarchar (1000)  ," +
                                 " THERMALFOOTER6   nvarchar (30)  ," +
                                 " THERMALFOOTER7   nvarchar (30)  ," +
                                 " BANQHEADER6   nvarchar (1000)  ," +
                                 " BANQHEADER7   nvarchar (1000)  ," +
                                 " BANQHEADER8   nvarchar (1000)  ," +
                                 " BANQFOOTER6   nvarchar (1000)  ," +
                                 " BANQFOOTER7   nvarchar (1000)  ," +
                                 " BANQFOOTER8   nvarchar (1000)  ," +
                                 " ISTBLSECPRINTREQ   bit   ," +
                                 " CUTSECKOT   bit   ," +
                                 " NOOFSECKOT   bigint   ," +
                                 " PRINTSECKOT   nvarchar (100)  ," +
                                 " SECKOTTYPE   nvarchar (100)  ," +
                                 " SEC1PRINTER   nvarchar (1000)  ," +
                                 " SEC2PRINTER   nvarchar (1000)  ," +
                                 " SEC3PRINTER   nvarchar (1000)  ," +
                                 " DISPIMG   bit   ," +
                                 " AUTOSTARTSMSAPP   bit   ," +
                                 " DISPREGINM   bit   ," +
                                 " KOTSCREENVER   nvarchar (50)  ," +
                                 " REWARDPROVIDER   nvarchar (250)  ," +
                                 " REWARDUSERID   nvarchar (250)  ," +
                                 " REWARDPASSWORD   nvarchar (250)  ," +
                                 " ALLOWREWARDPOINTAPI   bit   ," +
                                 " REWARDHEADID   nvarchar (800)  ," +
                                 " DISPMULTISETTLEMENT   bit   ," +
                                 " NOTALLOW0STKITEMISSUE   bit   ," +
                                 " LOCALSERVERNAME   nvarchar (100)  ," +
                                 " LOCALDBNAME   nvarchar (100)  ," +
                                 " LOCALLOGIN   nvarchar (100)  ," +
                                 " LOCALPASSWORD   nvarchar (100)  ," +
                                 " LOCALPORT   nvarchar (100)  ," +
                                 " ONLINESERVERNAME   nvarchar (100)  ," +
                                 " ONLINEDBNAME   nvarchar (100)  ," +
                                 " ONLINELOGIN   nvarchar (100)  ," +
                                 " ONLINEPASSWORD   nvarchar (100)  ," +
                                 " ONLINEPORT   nvarchar (100)	   " +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE  CASHONHAND (" +
                                 " RID   bigint ," +
                                 " CASHDATE   datetime   ," +
                                 " CASHAMT   decimal (18, 2)  ," +
                                 " CASHSTATUS   nvarchar (50)  ," +
                                 " CASHPERSONNAME   nvarchar (200)  ," +
                                 " CASHREMARK   nvarchar (500)  ," +
                                 " CASHDESC   nvarchar (max)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit   ," +
                                 " EMPRID   bigint   ," +
                                 " CASHNO   nvarchar (100)  ," +
                                 " SENDDATA BIT ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE MSTEXPENCES (" +
                                " RID   bigint ," +
                                " EXCODE   nvarchar (50)  ," +
                                " EXNAME   nvarchar (500)  ," +
                                " EXREMARK   nvarchar (500)  ," +
                                " EXDESC   nvarchar (max)  ," +
                                " AUSERID   bigint   ," +
                                " ADATETIME   datetime   ," +
                                " EUSERID   bigint   ," +
                                " EDATETIME   datetime   ," +
                                " DUSERID   bigint   ," +
                                " DDATETIME   datetime   ," +
                                " DELFLG   bit   ," +
                                " SENDDATA BIT" +
                           " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE MSTINCOME (" +
                                " RID   bigint ," +
                                " INCODE   nvarchar (50)  ," +
                                " INNAME   nvarchar (500)  ," +
                                " INREMARK   nvarchar (500)  ," +
                                " INDESC   nvarchar (max)  ," +
                                " AUSERID   bigint   ," +
                                " ADATETIME   datetime   ," +
                                " EUSERID   bigint   ," +
                                " EDATETIME   datetime   ," +
                                " DUSERID   bigint   ," +
                                " DDATETIME   datetime   ," +
                                " DELFLG   bit   ," +
                                " SENDDATA BIT" +
                           " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE  MSTTIEUPCOMPANY (" +
                                    " RID   bigint ," +
                                    " COMPCODE   nvarchar (50)  ," +
                                    " COMPNAME   nvarchar (500)  ," +
                                    " CONTPER   nvarchar (500)  ," +
                                    " CONTNO   nvarchar (500)  ," +
                                    " COMPDISC   decimal (18, 3)  ," +
                                    " COMPREMARK   nvarchar (max)  ," +
                                    " AUSERID   bigint   ," +
                                    " ADATETIME   datetime   ," +
                                    " EUSERID   bigint   ," +
                                    " EDATETIME   datetime   ," +
                                    " DUSERID   bigint   ," +
                                    " DDATETIME   datetime   ," +
                                    " DELFLG   bit   ," +
                                    " PAYMENTBY   nvarchar (100)  ," +
                                   " SENDDATA BIT" +
                               " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE PENTABLEINFO (" +
                                    " RID   bigint  IDENTITY(1,1)," +
                                    " KOTRID   bigint   ," +
                                    " KOTDATE   datetime  ," +
                                    " KOTTIME   datetime  ," +
                                    " TABLERID   bigint  ," +
                                    " TABLENAME   nvarchar (50)," +
                                    " AUSERID   bigint   ," +
                                    " ADATETIME   datetime   ," +
                                    " EUSERID   bigint   ," +
                                    " EDATETIME   datetime   ," +
                                    " DUSERID   bigint   ," +
                                    " DDATETIME   datetime   ," +
                                    " DELFLG   bit," +
                                    " TABLESTATUS   bigint   ," +
                                    " BILLRID   bigint   ," +
                                    " ISCOMPKOT   bit   " +
                                " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE DTDESC ( " +
                                 " DTTABLENAME   nvarchar (100) ," +
                                 " DTFIELDNAME   nvarchar (100) ," +
                                 " DTDESC   nvarchar (150)" +
                            " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = "CREATE TABLE DTVIEWDESC ( " +
                                 " DTVIEWNAME   nvarchar (100) ," +
                                 " DTFIELDNAME   nvarchar (100)," +
                                 " DTDESC   nvarchar (150)," +
                                 " FLDDATATYPE   nvarchar (50)," +
                                 " ORDERNO   bigint " +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTSUPPLIER (" +
                                 " RID   bigint," +
                                 " SUPPCODE   nvarchar (50)  ," +
                                 " SUPPNAME   nvarchar (250)  ," +
                                 " SUPPADD1   nvarchar (100)  ," +
                                 " SUPPADD2   nvarchar (100)  ," +
                                 " SUPPADD3   nvarchar (100)  ," +
                                 " SUPPCITYID   bigint   ," +
                                 " SUPPSTATEID   bigint   ," +
                                 " SUPPCOUNTRYID   bigint   ," +
                                 " SUPPPIN   nvarchar (50)  ," +
                                 " SUPPTELNO   nvarchar (50)  ," +
                                 " SUPPMOBNO   nvarchar (50)  ," +
                                 " SUPPFAXNO   nvarchar (50)  ," +
                                 " SUPPCONTPERNAME1   nvarchar (250)  ," +
                                 " SUPPCONTPERNAME2   nvarchar (250)  ," +
                                 " SUPPEMAIL   nvarchar (250)  ," +
                                 " SUPPPANNO   nvarchar (50)  ," +
                                 " SUPPTINNO   nvarchar (50)  ," +
                                 " SUPPCSTNO   nvarchar (50)  ," +
                                 " SUPPGSTNO   nvarchar (50)  ," +
                                 " SUPPREMARK   nvarchar (max)  ," +
                                 " SUPPIMAGE   image   ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit  ," +
                                 " SUPPTYPE   nvarchar (1000)  ," +
                                " SENDDATA BIT" +
                             " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE TABLERESERVATION (" +
                                 " RID   bigint ," +
                                 " REVNO   nvarchar (20) ," +
                                 " BODATE   datetime  ," +
                                 " REVDATE   datetime ," +
                                 " REVTIME   datetime ," +
                                 " CUSTRID   bigint ," +
                                 " TABLERID   bigint," +
                                 " PAX   bigint ," +
                                 " FUNCNAME   nvarchar (500)," +
                                 " SPREQ   nvarchar (max)," +
                                 " REVDESC   nvarchar (max)," +
                                 " ENTRYBY   nvarchar (500)," +
                                 " AUSERID   bigint ," +
                                 " ADATETIME   datetime," +
                                 " EUSERID   bigint ," +
                                 " EDATETIME   datetime," +
                                 " DUSERID   bigint," +
                                 " DDATETIME   datetime," +
                                 " DELFLG   bit," +
                                " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE MSTTIEUPCOMPANY " +
                            " ( " +
                            " RID Bigint," +
                            " COMPCODE nvarchar(50)," +
                            " COMPNAME nvarchar(500)," +
                            " CONTPER nvarchar(500)," +
                            " CONTNO nvarchar(500)," +
                            " COMPDISC DECIMAL(18,3)," +
                            " COMPREMARK nvarchar(MAX)," +
                            " AUSERID bigint," +
                            " ADATETIME datetime," +
                            " EUSERID bigint," +
                            " EDATETIME datetime," +
                            " DUSERID bigint," +
                            " DDATETIME datetime," +
                            " DELFLG bit," +
                            " SENDDATA BIT " +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = " CREATE TABLE INCOME " +
                              " (" +
                              " RID bigint," +
                              " INCOMENO NVARCHAR(200)," +
                              " INDATE DATETIME," +
                              " INTIME DATETIME," +
                              " INRID BIGINT," +
                              " INTYPE NVARCHAR(200)," +
                              " INAMOUNT DECIMAL(18,3)," +
                              " INPERNAME NVARCHAR(200)," +
                              " INCONTNO NVARCHAR(200)," +
                              " REMARK1 NVARCHAR(500)," +
                              " REMARK2 NVARCHAR(500)," +
                              " REMARK3 NVARCHAR(500)," +
                              " INDESC NVARCHAR(MAX)," +
                              " AUSERID bigint," +
                              " ADATETIME datetime," +
                              " EUSERID bigint," +
                              " EDATETIME datetime," +
                              " DUSERID bigint," +
                              " DDATETIME datetime," +
                              " DELFLG bit," +
                              " SENDDATA BIT " +
                              " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "CREATE TABLE EXPENCE " +
                                " (" +
                                " RID bigint," +
                                " EXPENCENO NVARCHAR(200)," +
                                " EXDATE DATETIME," +
                                " EXTIME DATETIME," +
                                " EXRID BIGINT," +
                                " EXTYPE NVARCHAR(200)," +
                                " EXAMOUNT DECIMAL(18,3)," +
                                " EXPERNAME NVARCHAR(200)," +
                                " EXCONTNO NVARCHAR(200)," +
                                " REMARK1 NVARCHAR(500)," +
                                " REMARK2 NVARCHAR(500)," +
                                " REMARK3 NVARCHAR(500)," +
                                " EXDESC NVARCHAR(MAX)," +
                                " AUSERID bigint," +
                                " ADATETIME datetime," +
                                " EUSERID bigint," +
                                " EDATETIME datetime," +
                                " DUSERID bigint," +
                                " DDATETIME datetime," +
                                " DELFLG bit," +
                                " SENDDATA BIT " +
                                " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = " CREATE TABLE FEEDBACKDATA" +
                                " (" +
                                " RID bigint IDENTITY(1,1) PRIMARY KEY," +
                                " FEEDDATE DATETIME," +
                                " GUESTNAME NVARCHAR(100)," +
                                " MOBNO NVARCHAR(50)," +
                                " EMAIL NVARCHAR(200)," +
                                " OPTFOOD INT," +
                                " OPTSERVICE INT," +
                                " OPTATMO INT," +
                                " OPTOVER INT," +
                                " REMARK NVARCHAR(MAX)," +
                                " OSNM NVARCHAR(200)," +
                                " BRONM NVARCHAR(200)," +
                                " DEVICENM NVARCHAR(200)," +
                                " AUSERID bigint," +
                                " ADATETIME datetime," +
                                " EUSERID bigint," +
                                " EDATETIME datetime," +
                                " DUSERID bigint," +
                                " DDATETIME datetime," +
                                " DELFLG bit" +
                                " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                strcreate1 = "";
                strcreate1 = " CREATE TABLE OPCASH " +
                            " (  " +
                            " RID BIGINT, " +
                            " OPCASHDATE DATETIME, " +
                            " OPAMT DECIMAL(18,3), " +
                            " OPREMARK nvarchar(max), " +
                            " OPENTRYBY nvarchar(100), " +
                            " AUSERID bigint, " +
                            " ADATETIME datetime, " +
                            " EUSERID bigint, " +
                            " EDATETIME datetime, " +
                            " DUSERID bigint, " +
                            " DDATETIME datetime, " +
                            " DELFLG bit, " +
                            " SENDDATA bit " +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strcreate1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region ALTER TABLE

        public bool Alter_OnlineDb_Table()
        {
            string stralter1 = "";
            try
            {
                stralter1 = "";
                stralter1 = "Alter Table MSTTABLE ADD CUSTRID BIGINT";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTSUPPLIER ADD SUPPPAYMENT NVARCHAR(50)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTSUPPLIER ADD ITEMTYPE NVARCHAR(50)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTCUST ADD TIEUPRID BIGINT";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD IPURRATE DECIMAL(18,3)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD NUT1 NVARCHAR(200)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD NUT2 NVARCHAR(200)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD NUT3 NVARCHAR(200)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD NUT4 NVARCHAR(200)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD NUT5 NVARCHAR(200)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD NUT6 NVARCHAR(200)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTITEM ADD NUT7 NVARCHAR(200)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTEXPENCES ADD ISOPECOST BIT";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table MSTEXPENCES ADD ISFUELCOST BIT";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "ALTER TABLE MSTTIEUPCOMPANY ADD SALECOMMIPER DECIMAL(18,3)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "ALTER TABLE MSTTIEUPCOMPANY ADD COMMIPER DECIMAL(18,3)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "alter table MSTITEM Alter column IREWPOINT DECIMAL(18,3)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table BILL Alter column TOTBILLREWPOINT DECIMAL(18,3)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table BILL Alter column TOTITEMREWPOINT DECIMAL(18,3)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                stralter1 = "";
                stralter1 = "Alter table BILLDTL Alter column IREWPOINTS DECIMAL(18,3)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(stralter1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region CREATE TYPE

        private bool DeleteTYPEFromOnlineDb(string TypeName)
        {
            string sqlstr;
            try
            {
                sqlstr = " DROP TYPE " + TypeName;
                this.ExecuteMsSqlOnlineCommand_NoMsg(sqlstr);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Create_OnlineDb_TYPE()
        {
            string strtype = "";
            try
            {

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTCITY");
                this.DeleteTYPEFromOnlineDb("MSTCITY_TYPE");
                strtype = " CREATE TYPE MSTCITY_TYPE AS TABLE " +
                              " ( " +
                                " CITYID bigint NULL," +
                                " CITYNAME nvarchar(100) NULL" +
                              " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTCOUNTRY");
                this.DeleteTYPEFromOnlineDb("MSTCOUNTRY_TYPE");
                strtype = " CREATE TYPE MSTCOUNTRY_TYPE AS TABLE " +
                              " ( " +
                                " COUNTRYID bigint NULL," +
                                " COUNTRYNAME nvarchar(100) NULL" +
                              " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTSTATE");
                this.DeleteTYPEFromOnlineDb("MSTSTATE_TYPE");
                strtype = " CREATE TYPE MSTSTATE_TYPE AS TABLE " +
                              " ( " +
                                " STATEID bigint NULL," +
                                " STATENAME nvarchar(100) NULL" +
                              " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);


                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTUNIT");
                this.DeleteTYPEFromOnlineDb("MSTUNIT_TYPE");
                strtype = " CREATE TYPE MSTUNIT_TYPE AS TABLE " +
                              " ( " +
                                " RID bigint NULL," +
                                " UNITCODE nvarchar(20) NULL," +
                                " UNITNAME nvarchar(100) NULL," +
                                " UNITDESC nvarchar(max) NULL," +
                                " AUSERID bigint NULL," +
                                " ADATETIME DATETIME NULL," +
                                " EUSERID bigint NULL," +
                                " EDATETIME DATETIME NULL," +
                                " DUSERID bigint NULL," +
                                " DDATETIME DATETIME NULL," +
                                " DELFLG bit NULL,	" +
                                " SENDDATA bit NULL	" +
                              " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTDEPT");
                this.DeleteTYPEFromOnlineDb("MSTDEPT_TYPE");
                strtype = " CREATE TYPE MSTDEPT_TYPE AS TABLE " +
                              " ( " +
                                " RID BIGINT," +
                                " DEPTCODE NVARCHAR(20)," +
                                " DEPTNAME NVARCHAR(50)," +
                                " DEPTDESC NVARCHAR(MAX)," +
                                " AUSERID BIGINT," +
                                " ADATETIME DATETIME," +
                                " EUSERID BIGINT," +
                                " EDATETIME DATETIME," +
                                " DUSERID BIGINT," +
                                " DDATETIME DATETIME," +
                                " DELFLG BIT," +
                                " ISBARDEPT BIT," +
                                " ISKITCHENDEPT BIT," +
                                " ISHUKKADEPT BIT," +
                                " SENDDATA BIT" +
                              " ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTUSERS");
                this.DeleteTYPEFromOnlineDb("MSTUSERS_TYPE");
                strtype = " CREATE TYPE MSTUSERS_TYPE AS TABLE " +
                             " ( " +
                                " RID BIGINT," +
                                " USERNAME NVARCHAR(50)," +
                                " PASSWORD NVARCHAR(50)," +
                                " REPASSWORD NVARCHAR(50)," +
                                " USERDESC NVARCHAR(MAX)," +
                                " AUSERID BIGINT," +
                                " ADATETIME DATETIME," +
                                " EUSERID BIGINT," +
                                " EDATETIME DATETIME," +
                                " DUSERID BIGINT," +
                                " DDATETIME DATETIME," +
                                " DELFLG BIT, " +
                                " USERCODE NVARCHAR(50)," +
                                " ISSHOWREPORT BIT, " +
                                " HIDEMSTMNU BIT, " +
                                " HIDEBANQMNU BIT, " +
                                " HIDECRMMNU BIT, " +
                                " HIDESMSMNU BIT," +
                                " HIDEPAYROLLMNU BIT," +
                                " HIDEUTILITYMNU BIT," +
                                " HIDEKOTBILLENT BIT," +
                                " HIDECASHMENOENT BIT," +
                                " HIDESETTLEMENTENT BIT," +
                                " HIDEQUICKBILLENT BIT," +
                                " HIDETABLEVIEWENT BIT," +
                                " HIDEMULTISETTLEMENTENT BIT," +
                                " HIDECASHONHANDENT BIT," +
                                " HIDESUPPLIERENT BIT," +
                                " HIDEPURCHASEENT BIT," +
                                " HIDEPAYMENYENT BIT," +
                                " HIDECHECKLISTENT BIT," +
                                " HIDEBUSINESSSUMMARY BIT," +
                                " DONTALLOWBILLEDIT BIT," +
                                " DONTALLOWBILLDELETE BIT," +
                                " DONTALLOWTBLCLEAR BIT," +
                                " HIDESTKISSUEENT BIT," +
                                " DONTALLOWBANQBOEDIT BIT," +
                                " DONTALLOWBANQBODELETE BIT," +
                                " DONTALLOWCHKLISTEDIT BIT," +
                                " DONTALLOWCHKLISTDELETE BIT," +
                                " DONTALLOWPURCHASEEDIT BIT," +
                                " DONTALLOWPURCHASEDELETE BIT," +
                                " DONTALLOWSTKISSUEEDIT BIT," +
                                " DONTALLOWSTKISSUEDELETE BIT," +
                                " HIDEPURITEMGROUPENT BIT," +
                                " HIDEPURITEMENT BIT," +
                                " DONTALLOWCHANGEDATEINKOTBILL BIT," +
                                " HIDEPURCHASEICO BIT," +
                                " ISTABLETUSER BIT," +
                                " DONTALLOWKOTEDIT BIT," +
                                " DONTALLOWKOTDELETE BIT," +
                                " DONTALLOWDISCINBILL BIT," +
                                " HIDEINVMNU BIT,SENDDATA BIT " +
                                 " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTREPORTDEPT");
                this.DeleteTYPEFromOnlineDb("MSTREPORTDEPT_TYPE");
                strtype = " CREATE TYPE MSTREPORTDEPT_TYPE AS TABLE " +
                             " ( " +
                                     " RID   bigint ," +
                                     " REPORTDEPTCODE  nvarchar (20)," +
                                     " REPORTDEPTNAME  nvarchar (100)," +
                                     " REPORTDEPTDESC  nvarchar (max)," +
                                     " AUSERID   bigint," +
                                     " ADATETIME datetime," +
                                     " EUSERID   bigint," +
                                     " EDATETIME datetime," +
                                     " DUSERID   bigint," +
                                     " DDATETIME datetime ," +
                                     " DELFLG bit, SENDDATA BIT " +
                                " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTHSNCODE");
                this.DeleteTYPEFromOnlineDb("MSTHSNCODE_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTHSNCODE_TYPE AS TABLE (" +
                             " RID   bigint ," +
                             " HSNCODE  nvarchar (50)," +
                             " HSNCODEREMARK nvarchar (200)," +
                             " ACTOTGSTPER  decimal (18, 3) ," +
                             " ACCGSTPER   decimal (18, 3)  ," +
                             " ACSGSTPER   decimal (18, 3)  ," +
                             " ACIGSTPER   decimal (18, 3)  ," +
                             " NACTOTGSTPER  decimal (18, 3)  ," +
                             " NACCGSTPER   decimal (18, 3)  ," +
                             " NACSGSTPER   decimal (18, 3)  ," +
                             " NACIGSTPER   decimal (18, 3)  ," +
                             " CATOTGSTPER   decimal (18, 3)  ," +
                             " CACGSTPER   decimal (18, 3)  ," +
                             " CASGSTPER   decimal (18, 3)  ," +
                             " CAIGSTPER   decimal (18, 3)  ," +
                             " RSTOTGSTPER   decimal (18, 3)  ," +
                             " RSCGSTPER   decimal (18, 3)  ," +
                             " RSSGSTPER   decimal (18, 3)  ," +
                             " RSIGSTPER   decimal (18, 3)  ," +
                             " PURTOTGSTPER   decimal (18, 3)  ," +
                             " PURCGSTPER   decimal (18, 3)  ," +
                             " PURSGSTPER   decimal (18, 3)  ," +
                             " PURIGSTPER   decimal (18, 3)  ," +
                             " BANQTOTGSTPER   decimal (18, 3)  ," +
                             " BANQCGSTPER   decimal (18, 3)  ," +
                             " BANQSGSTPER   decimal (18, 3)  ," +
                             " BANQIGSTPER   decimal (18, 3)  ," +
                             " AUSERID   bigint   ," +
                             " ADATETIME   datetime   ," +
                             " EUSERID   bigint   ," +
                             " EDATETIME   datetime   ," +
                             " DUSERID   bigint   ," +
                             " DDATETIME   datetime   ," +
                             " DELFLG   bit   ," +
                             " SENDDATA BIT )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEMGROUP");
                this.DeleteTYPEFromOnlineDb("MSTITEMGROUP_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTITEMGROUP_TYPE AS TABLE " +
                                " ( " +
                                     " RID   bigint ," +
                                     " IGCODE   nvarchar (50), " +
                                     " IGNAME   nvarchar (200)," +
                                     " IGDESC   nvarchar (max)," +
                                     " AUSERID   bigint  NULL," +
                                     " ADATETIME   datetime  NULL," +
                                     " EUSERID   bigint  NULL," +
                                     " EDATETIME   datetime  NULL," +
                                     " DUSERID   bigint  NULL," +
                                     " DDATETIME   datetime  NULL," +
                                     " DELFLG   bit ," +
                                     " NOTAPPLYDISC   bit  NULL," +
                                     " IGPNAME   nvarchar (200)," +
                                     " IGDISPORD   bigint  NULL," +
                                     " ISHIDEGROUP   bigint  NULL," +
                                     " SHOWINDIFFCOLOR   bit  NULL," +
                                     " ISHIDEGROUPKOT   bit  NULL," +
                                     " ISITEMREMGRP   bit  NULL," +
                                     " ISHIDEGROUPCASHMEMO   bit  NULL," +
                                     " HSNCODERID   bigint  NULL," +
                                     " IGBACKCOLOR   bigint  NULL," +
                                     " IGFORECOLOR   bigint  NULL," +
                                     " IGFONTNAME   nvarchar (200) ," +
                                     " IGFONTSIZE   float  NULL," +
                                     " IGFONTBOLD   bit  NULL," +
                                     " IGPRINTORD   bigint  NULL," +
                                     " REGLANGIGNAME   nvarchar (200) ," +
                                    " SENDDATA BIT " +
                                   " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEM");
                this.DeleteTYPEFromOnlineDb("MSTITEM_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTITEM_TYPE AS TABLE(" +
                                 " RID   bigint," +
                                 " ICODE   nvarchar (20)," +
                                 " INAME   nvarchar (250)," +
                                 " IIMG   image   ," +
                                 " IGRPRID   bigint   ," +
                                 " IUNITRID   bigint   ," +
                                 " IDEPTRID   bigint   ," +
                                 " IRATE   decimal (18, 2)  ," +
                                 " IDESC   nvarchar (max) ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                 " ICOMMI   decimal (18, 2)  ," +
                                 " IPNAME   nvarchar (200)," +
                                 " IMINQTY   decimal (18, 2)  ," +
                                 " IMAXQTY   decimal (18, 2)  ," +
                                 " IREQTY   decimal (18, 2)  ," +
                                 " ISITEMSTOCK   bit   ," +
                                 " REPORTDEPTRID   bigint   ," +
                                 " ISHIDEITEM   bigint   ," +
                                 " ISNOTAPPSERTAXDISC   bigint   ," +
                                 " IDPRATE   decimal (18, 2)  ," +
                                 " ISRUNNINGITEM   Bit ," +
                                 " DISDIFFCOLOR   bit ," +
                                 " ITEMTAXTYPE   nvarchar (50)," +
                                 " ISCHKRPTITEM   bit   ," +
                                 " ISNOTAPPLYVAT   bit   ," +
                                 " COUPONCODE   nvarchar (20) ," +
                                 " IREWPOINT   decimal (18, 3)," +
                                 " NOTAPPLYGSTTAX   bit   ," +
                                 " NOTAPPLYDISC   bit   ," +
                                 " HSNCODERID   bigint   ," +
                                 " IBACKCOLOR   bigint   ," +
                                 " IFORECOLOR   bigint   ," +
                                 " IFONTNAME   nvarchar (200)," +
                                 " IFONTSIZE   float   ," +
                                 " IFONTBOLD   bit   ," +
                                 " REGLANGNAME   nvarchar (200)," +
                                 " IREMARK   nvarchar (2000)," +
                                 " SENDDATA BIT," +
                                 " IPURRATE DECIMAL(18,3), " +
                                 " NUT1 nvarchar (200)," +
                                 " NUT2 nvarchar (200)," +
                                 " NUT3 nvarchar (200)," +
                                 " NUT4 nvarchar (200)," +
                                 " NUT5 nvarchar (200)," +
                                 " NUT6 nvarchar (200)," +
                                 " NUT7 nvarchar (200)" +
                              " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEMPRICELIST");
                this.DeleteTYPEFromOnlineDb("MSTITEMPRICELIST_TYPE");
                strtype = "";
                strtype = "  CREATE TYPE MSTITEMPRICELIST_TYPE AS TABLE " +
                                " ( " +
                                 " RID bigint," +
                                 " IPLCODE nvarchar(20)," +
                                 " IPLNAME nvarchar(50)," +
                                 " IPLDESC nvarchar (max)," +
                                 " AUSERID bigint," +
                                 " ADATETIME datetime," +
                                 " EUSERID  bigint," +
                                 " EDATETIME datetime," +
                                 " DUSERID  bigint," +
                                 " DDATETIME datetime," +
                                 " DELFLG  bigint," +
                                 " SENDDATA bit" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEMPRICELISTDTL");
                this.DeleteTYPEFromOnlineDb("MSTITEMPRICELISTDTL_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTITEMPRICELISTDTL_TYPE AS TABLE( " +
                                     " RID  bigint ," +
                                     " IPLRID bigint," +
                                     " IRID  bigint," +
                                     " IRATE  decimal (18, 2) ," +
                                     " IPRATE decimal (18, 2) ," +
                                     " AUSERID bigint," +
                                     " ADATETIME datetime," +
                                     " EUSERID   bigint," +
                                     " EDATETIME datetime," +
                                     " DUSERID   bigint," +
                                     " DDATETIME datetime," +
                                     " DELFLG   bigint," +
                                     " SENDDATA bit  " +
                                 " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTREMARK");
                this.DeleteTYPEFromOnlineDb("MSTREMARK_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTREMARK_TYPE AS TABLE(  " +
                            " RID bigint," +
                            " REMARKCODE nvarchar(20), " +
                            " REMARKNAME nvarchar(100)," +
                            " REMARKDESC nvarchar(max)," +
                            " AUSERID bigint," +
                            " ADATETIME datetime," +
                            " EUSERID bigint," +
                            " EDATETIME datetime," +
                            " DUSERID bigint," +
                            " DDATETIME datetime," +
                            " DELFLG bit,SENDDATA BIT )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);


                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTTABLE");
                this.DeleteTYPEFromOnlineDb("MSTTABLE_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTTABLE_TYPE AS TABLE(" +
                      " RID   bigint ," +
                      " TABLECODE   nvarchar (20)  ," +
                      " TABLENAME   nvarchar (50)  ," +
                      " TABLEDESC   nvarchar (max) ," +
                      " AUSERID   bigint   ," +
                      " ADATETIME   datetime   ," +
                      " EUSERID   bigint   ," +
                      " EDATETIME   datetime   ," +
                      " DUSERID   bigint   ," +
                      " DDATETIME   datetime   ," +
                      " DELFLG  bit ," +
                      " ISROOMTABLE   bit   ," +
                      " ISPARCELTABLE   bit   ," +
                      " ROOMNO   nvarchar (100) ," +
                      " TABDISC   decimal (18, 2)  ," +
                      " PRICELISTRID   bigint   ," +
                      " ISHOMEDELITABLE   bit   ," +
                      " ISNOTCALCCOMMI   bit   ," +
                      " ISHIDETABLE   bit   ," +
                      " TABPAX   bigint   ," +
                      " TABDISPORD   bigint  ," +
                      " GSTTAXTYPE   nvarchar (100)," +
                      " TABLEGROUP   nvarchar (500)," +
                      " SECNO   nvarchar (50)," +
                      " MSTTIEUPCOMPRID bigint," +
                      " SENDDATA BIT, " +
                      " CUSTRID BIGINT " +
                  " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTEMPCAT");
                this.DeleteTYPEFromOnlineDb("MSTEMPCAT_TYPE");
                strtype = "";
                strtype = "CREATE TYPE MSTEMPCAT_TYPE AS TABLE( " +
                                 " RID   bigint ," +
                                 " EMPCATCODE   nvarchar (20) ," +
                                 " EMPCATNAME   nvarchar (200) ," +
                                 " EMPCATDESC   nvarchar (max) ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                " SENDDATA BIT" +
                             " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTEMP");
                this.DeleteTYPEFromOnlineDb("MSTEMP_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTEMP_TYPE AS TABLE(  " +
                                 " RID   bigint  ," +
                                 " EMPCODE   nvarchar (20)  ," +
                                 " EMPNAME   nvarchar (250)  ," +
                                 " EMPFATHERNAME   nvarchar (250)  ," +
                                 " EMPADD1   nvarchar (200)  ," +
                                 " EMPADD2   nvarchar (200)  ," +
                                 " EMPADD3   nvarchar (200)  ," +
                                 " EMPCATID   bigint   ," +
                                 " EMPCITYID   bigint   ," +
                                 " EMPSTATEID   bigint   ," +
                                 " EMPCOUNTRYID   bigint   ," +
                                 " EMPPIN   nvarchar (50)  ," +
                                 " EMPTELNO   nvarchar (50)  ," +
                                 " EMPMOBILENO   nvarchar (50)  ," +
                                 " EMPEMAIL   nvarchar (200)  ," +
                                 " EMPFAXNO   nvarchar (50)  ," +
                                 " EMPBIRTHDATE   datetime   ," +
                                 " EMPJOINDATE   datetime   ," +
                                 " EMPLEAVEDATE   datetime   ," +
                                 " EMPGENDER   nvarchar (50)  ," +
                                 " EMPMARITALSTATUS   nvarchar (50)  ," +
                                 " ISDISPINKOT   bit   ," +
                                 " EMPIMAGE   image   ," +
                                 " EMPDESC   nvarchar (max)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                 " EMPANNIDATE   datetime   ," +
                                 " EMPBANKNAME   nvarchar (250)  ," +
                                 " EMPBANKDETAILS   nvarchar (250)  ," +
                                 " EMPBANKACCNO   nvarchar (250)  ," +
                                 " ISNONACTIVE   bit   ," +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_KOT");
                this.DeleteTYPEFromOnlineDb("KOT_TYPE");
                strtype = "";
                strtype = " CREATE TYPE KOT_TYPE AS TABLE( " +
                                 " RID   bigint ," +
                                 " KOTDATE  datetime   ," +
                                 " KOTTIME  datetime   ," +
                                 " KOTNO   nvarchar (100)   ," +
                                 " KOTTOKNO  nvarchar (100)   ," +
                                 " KOTORDERPERID  bigint   ," +
                                 " ISPARSELKOT   bit ," +
                                 " KOTTABLEID   bigint ," +
                                 " KOTTABLENAME  nvarchar (100)   ," +
                                 " KOTREMARK   nvarchar (max)   ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit ," +
                                 " KOTPAX   bigint   ," +
                                 " CUSTRID   bigint   ," +
                                 " CUSTNAME   nvarchar (250)   ," +
                                 " CUSTADD   nvarchar (10)   ," +
                                 " KOTINFO   nvarchar (max)   ," +
                                 " CARDNO   nvarchar (50)   ," +
                                 " REFKOTNO   nvarchar (50)   ," +
                                 " REFKOTNUM   bigint   ," +
                                 " ISCOMPKOT   bit   ," +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_KOTDTL");
                this.DeleteTYPEFromOnlineDb("KOTDTL_TYPE");
                strtype = "";
                strtype = "CREATE TYPE KOTDTL_TYPE AS TABLE ( " +
                                 " RID   bigint," +
                                 " KOTRID  bigint ," +
                                 " IRID   bigint   ," +
                                 " INAME  nvarchar (500) ," +
                                 " IQTY   decimal (18, 2) ," +
                                 " IRATE  decimal (18, 2) ," +
                                 " IAMT   decimal (18, 2) ," +
                                 " AUSERID bigint  ," +
                                 " ADATETIME datetime ," +
                                 " EUSERID   bigint  ," +
                                 " EDATETIME datetime ," +
                                 " DUSERID   bigint  ," +
                                 " DDATETIME datetime ," +
                                 " DELFLG   bit  ," +
                                 " IREMARK  nvarchar (300)  ," +
                                 " IMODIFIER  nvarchar (max) ," +
                                 " ICOMPITEM  bit, " +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_BILL");
                this.DeleteTYPEFromOnlineDb("BILL_TYPE");
                strtype = "";
                strtype = "CREATE TYPE BILL_TYPE AS TABLE ( " +
                              " RID   bigint  ," +
                             " BILLNO   nvarchar (50)    ," +
                             " BILLDATE   datetime   ," +
                             " BILLTYPE   nvarchar (50)    ," +
                             " CUSTRID   bigint   ," +
                             " CUSTNAME   nvarchar (250)    ," +
                             " CUSTCONTNO   nvarchar (250)    ," +
                             " TABLERID   bigint   ," +
                             " PRICELISTRID   bigint   ," +
                             " BILLPAX   bigint   ," +
                             " TOTAMOUNT   decimal (18, 2)  ," +
                             " TOTSERTAXPER   decimal (18, 2)  ," +
                             " TOTSERTAXAMOUNT   decimal (18, 2)  ," +
                             " TOTVATPER   decimal (18, 2)  ," +
                             " TOTVATAMOUNT   decimal (18, 2)  ," +
                             " TOTADDVATPER   decimal (18, 2)  ," +
                             " TOTADDVATAMOUNT   decimal (18, 2)  ," +
                             " TOTDISCPER   decimal (18, 2)  ," +
                             " TOTDISCAMOUNT   decimal (18, 2)  ," +
                             " TOTROFF   decimal (18, 2)  ," +
                             " NETAMOUNT   decimal (18, 2)  ," +
                             " BILLPREPBY   nvarchar (500)    ," +
                             " BILLREMARK   nvarchar (max)    ," +
                             " SETLETYPE   nvarchar (50)    ," +
                             " SETLEAMOUNT   decimal (18, 2)  ," +
                             " CHEQUENO   nvarchar (100)    ," +
                             " CHEQUEBANKNAME   nvarchar (100)    ," +
                             " CREDITCARDNO   nvarchar (100)    ," +
                             " CREDITHOLDERNAME   nvarchar (100)    ," +
                             " CREDITBANKNAME   nvarchar (100)    ," +
                             " AUSERID   bigint   ," +
                             " ADATETIME   datetime   ," +
                             " EUSERID   bigint   ," +
                             " EDATETIME   datetime   ," +
                             " DUSERID   bigint   ," +
                             " DDATETIME   datetime   ," +
                             " DELFLG   bit ," +
                             " REFBILLNO   nvarchar (50)    ," +
                             " REFBILLNUM   bigint   ," +
                             " custadd   nvarchar (max)    ," +
                             " TOTDISCOUNTABLEAMOUNT  decimal (18, 2)  ," +
                             " ISREVISEDBILL   bigint   ," +
                             " REVISEDBILLSRNO   bigint   ," +
                             " REVISEDBILLNO   bigint   ," +
                             " BASEBILLNO   bigint   ," +
                             " MAINBASEBILLNO   bigint   ," +
                             " LASTTABLERID   bigint   ," +
                             " LASTTABLESTATUS   bigint   ," +
                             " BILLORDERPERID   bigint   ," +
                             " BILLTIME   datetime   ," +
                             " TOTCALCVATPER   decimal (18, 2)  ," +
                             " TOTCALCVATAMOUNT   decimal (18, 2)  ," +
                             " ISBILLTOCUSTOMER   bit   ," +
                             " CNTPRINT   bigint   ," +
                             " TOKENNO   bigint   ," +
                             " ISPARCELBILL   bit   ," +
                             " ISCOMPLYBILL   bit   ," +
                             " TOTBEVVATPER   decimal (18, 3)  ," +
                             " TOTBEVVATAMT   decimal (18, 2)  ," +
                             " TOTLIQVATPER   decimal (18, 3)  ," +
                             " TOTLIQVATAMT   decimal (18, 2)  ," +
                             " REFBYRID   bigint   ," +
                             " BILLINFO   nvarchar (max)  ," +
                             " TOTSERCHRPER   decimal (18, 3)  ," +
                             " TOTSERCHRAMT   decimal (18, 3)  ," +
                             " TOTNEWTOTALAMT   decimal (18, 3)  ," +
                             " TOTADDDISCAMT   decimal (18, 3)  ," +
                             " MSTTIEUPCOMPRID   bigint   ," +
                             " COUPONNO   nvarchar (200)    ," +
                             " COUPONPERNAME   nvarchar (200)    ," +
                             " TOTKKCESSPER   decimal (18, 3)  ," +
                             " TOTKKCESSAMT   decimal (18, 3)  ," +
                             " OTHERPAYMENTBY   nvarchar (200)    ," +
                             " OTHERPAYMENTBYREMARK1   nvarchar (500)    ," +
                             " OTHERPAYMENTBYREMARK2   nvarchar (500)    ," +
                             " PAYMENT   nvarchar (50)    ," +
                             " MULTICASHAMT   decimal (18, 3)  ," +
                             " MULTICHEQUEAMT   decimal (18, 3)  ," +
                             " MULTICREDITCARDAMT   decimal (18, 3)  ," +
                             " MULTIOTHERAMT   decimal (18, 3)  ," +
                             " MULTICHQNO   nvarchar (200)    ," +
                             " MULTICHQBANKNAME   nvarchar (200)    ," +
                             " MULTICARDNO   nvarchar (200)    ," +
                             " MULTICARDBANKNAME   nvarchar (200)    ," +
                             " MULTIOTHERPAYMENTBY   nvarchar (200)    ," +
                             " MULTIOTHERREMARK1   nvarchar (500)    ," +
                             " MULTIOTHERREMARK2   nvarchar (500)    ," +
                             " MULTITIPAMT   decimal (18, 3)  ," +
                             " CARDNO   nvarchar (50)    ," +
                             " TOTBILLREWPOINT decimal (18, 3)," +
                             " TOTITEMREWPOINT decimal (18, 3)," +
                             " CGSTAMT   decimal (18, 3) ," +
                             " SGSTAMT   decimal (18, 3) ," +
                             " IGSTAMT   decimal (18, 3) ," +
                             " TOTGSTAMT   decimal (18, 3) ," +
                             " RECAMT   decimal (18, 3)  ," +
                             " RETAMT   decimal (18, 3)  ," +
                             " SENDDATA BIT" +
                        " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_BILLDTL");
                this.DeleteTYPEFromOnlineDb("BILLDTL_TYPE");
                strtype = "";
                strtype = "CREATE TYPE BILLDTL_TYPE AS TABLE ( " +
                                     " RID   bigint," +
                                     " BILLRID   bigint   ," +
                                     " IRID   bigint   ," +
                                     " KOTRID   bigint   ," +
                                     " IQTY   decimal (18, 3)  ," +
                                     " IRATE   decimal (18, 2)  ," +
                                     " IPAMT   decimal (18, 2)  ," +
                                     " IAMT   decimal (18, 2)  ," +
                                     " AUSERID   bigint   ," +
                                     " ADATETIME   datetime   ," +
                                     " EUSERID   bigint   ," +
                                     " EDATETIME   datetime   ," +
                                     " DUSERID   bigint   ," +
                                     " DDATETIME   datetime   ," +
                                     " DELFLG   bit  ," +
                                     " NOTAPPLYDISC   bigint   ," +
                                     " NOTAPPLYSERTAX   bigint   ," +
                                     " SERTAXTYPE   nvarchar (50)  ," +
                                     " NOTAPPLYVAT   bit   ," +
                                     " DISCPER   decimal (18, 3)  ," +
                                     " DISCAMT   decimal (18, 2)  ," +
                                     " SERTAXPER   decimal (18, 3)  ," +
                                     " SERTAXAMT   decimal (18, 2)  ," +
                                     " FOODVATPER   decimal (18, 3)  ," +
                                     " FOODVATAMT   decimal (18, 2)  ," +
                                     " LIQVATPER   decimal (18, 3)  ," +
                                     " LIQVATAMT   decimal (18, 2)  ," +
                                     " BEVVATPER   decimal (18, 3)  ," +
                                     " BEVVATAMT   decimal (18, 2)  ," +
                                     " SERCHRPER   decimal (18, 3)  ," +
                                     " SERCHRAMT   decimal (18, 2)  ," +
                                     " NEWSERCHRPER   decimal (18, 3)  ," +
                                     " NEWSERCHRAMT   decimal (18, 3)  ," +
                                     " KKCESSPER   decimal (18, 3)  ," +
                                     " KKCESSAMT   decimal (18, 3)  ," +
                                     " IREWPOINTS   decimal (18, 3) ," +
                                     " NOTAPPLYGST   bit   ," +
                                     " CGSTPER   decimal (18, 3)  ," +
                                     " CGSTAMT   decimal (18, 3)  ," +
                                     " SGSTPER   decimal (18, 3)  ," +
                                     " SGSTAMT   decimal (18, 3)  ," +
                                     " IGSTPER   decimal (18, 3)  ," +
                                     " IGSTAMT   decimal (18, 3)  ," +
                                     " GSTPER   decimal (18, 3)  ," +
                                     " GSTAMT   decimal (18, 3)  ," +
                                     " SENDDATA BIT" +
                                " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_SETTLEMENT");
                this.DeleteTYPEFromOnlineDb("SETTLEMENT_TYPE");
                strtype = "";
                strtype = " CREATE TYPE SETTLEMENT_TYPE AS TABLE ( " +
                                 " RID   bigint," +
                                 " SETLEDATE datetime   ," +
                                 " BILLRID  bigint   ," +
                                 " SETLENO  nvarchar (50)  ," +
                                 " SETLETYPE nvarchar (50)  ," +
                                 " SETLEAMOUNT decimal (18, 2)  ," +
                                 " CHEQUENO  nvarchar (100)  ," +
                                 " CHEQUEBANKNAME nvarchar (100) ," +
                                 " CREDITCARDNO   nvarchar (100) ," +
                                 " CREDITHOLDERNAME nvarchar (100)," +
                                 " CREDITBANKNAME   nvarchar (100) ," +
                                 " SETLEPREPBY   nvarchar (100) ," +
                                 " SETLEREMARK   nvarchar (max) ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bigint   ," +
                                 " CUSTRID   bigint   ," +
                                 " ADJAMT   decimal (18, 2)  ," +
                                 " TIPAMT   decimal (18, 3)  ," +
                                 " OTHERPAYMENTBY   nvarchar (200)  ," +
                                 " OTHERPAYMENTBYREMARK1   nvarchar (500)  ," +
                                 " OTHERPAYMENTBYREMARK2   nvarchar (500)  ," +
                                 " SENDDATA   bit   " +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTCUST");
                this.DeleteTYPEFromOnlineDb("MSTCUST_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTCUST_TYPE AS TABLE ( " +
                                 " RID   bigint ," +
                                 " CUSTCODE   nvarchar (50)  ," +
                                 " CUSTNAME   nvarchar (250)  ," +
                                 " CUSTADD1   nvarchar (200)  ," +
                                 " CUSTADD2   nvarchar (200)  ," +
                                 " CUSTADD3   nvarchar (200)  ," +
                                 " CUSTCITYID   bigint   ," +
                                 " CUSTSTATEID   bigint   ," +
                                 " CUSTCOUNTRYID   bigint   ," +
                                 " CUSTPIN   nvarchar (50)  ," +
                                 " CUSTTELNO   nvarchar (50)  ," +
                                 " CUSTMOBNO   nvarchar (50)  ," +
                                 " CUSTEMAIL   nvarchar (200)  ," +
                                 " CUSTFAXNO   nvarchar (100)  ," +
                                 " CUSTBIRTHDATE   datetime   ," +
                                 " CUSTGENDER   nvarchar (50)  ," +
                                 " CUSTMARITALSTATUS   nvarchar (50)  ," +
                                 " CUSTANNIDATE   datetime   ," +
                                 " CUSTIMAGE   image   ," +
                                 " CUSTREGDATE   datetime   ," +
                                 " CUSTDESC   nvarchar (max)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit  ," +
                                 " CUSTMOBNO2   nvarchar (50)  ," +
                                 " CUSTMOBNO3   nvarchar (50)  ," +
                                 " CUSTMOBNO4   nvarchar (50)  ," +
                                 " CUSTMOBNO5   nvarchar (50)  ," +
                                 " CUSTAREA   nvarchar (100)  ," +
                                 " CUSTADD2ADD1   nvarchar (250)  ," +
                                 " CUSTADD2ADD2   nvarchar (250)  ," +
                                 " CUSTADD2ADD3   nvarchar (250)  ," +
                                 " CUSTADD2AREA   nvarchar (250)  ," +
                                 " CUSTADD2CITY   bigint   ," +
                                 " CUSTADD2PIN   nvarchar (250)  ," +
                                 " CUSTADD3ADD1   nvarchar (250)  ," +
                                 " CUSTADD3ADD2   nvarchar (250)  ," +
                                 " CUSTADD3ADD3   nvarchar (250)  ," +
                                 " CUSTADD3AREA   nvarchar (250)  ," +
                                 " CUSTADD3CITY   bigint   ," +
                                 " CUSTADD3PIN   nvarchar (250)  ," +
                                 " CUSTADD4ADD1   nvarchar (250)  ," +
                                 " CUSTADD4ADD2   nvarchar (250)  ," +
                                 " CUSTADD4ADD3   nvarchar (250)  ," +
                                 " CUSTADD4AREA   nvarchar (250)  ," +
                                 " CUSTADD4CITY   bigint   ," +
                                 " CUSTADD4PIN   nvarchar (250)  ," +
                                 " CUSTADD5ADD1   nvarchar (250)  ," +
                                 " CUSTADD5ADD2   nvarchar (250)  ," +
                                 " CUSTADD5ADD3   nvarchar (250)  ," +
                                 " CUSTADD5AREA   nvarchar (250)  ," +
                                 " CUSTADD5CITY   bigint   ," +
                                 " CUSTADD5PIN   nvarchar (250)  ," +
                                 " CUSTDISCPER   decimal (18, 2)  ," +
                                 " CUSTLANDMARK   nvarchar (250)  ," +
                                 " CUSTDOCIMAGE   image   ," +
                                 " FOODTOKEN   bit   ," +
                                 " CARDNO   nvarchar (50)  ," +
                                 " CARDACTDATE   datetime   ," +
                                 " CARDENROLLFEES   decimal (18, 3)  ," +
                                 " CARDSTATUS   nvarchar (50)  ," +
                                 " CARDEXPDATE   datetime   ," +
                                 " CARDREMARK   nvarchar (max)  ," +
                                 " GSTNO   nvarchar (50)  ," +
                                 " PANNO   nvarchar (50)  ," +
                                 " APPLYIGST   bit   ," +
                                 " VATNO   nvarchar (50)  ," +
                                 " TIEUPRID BIGINT, " +
                                 " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_CASHONHAND");
                this.DeleteTYPEFromOnlineDb("CASHONHAND_TYPE");
                strtype = "";
                strtype = " CREATE TYPE CASHONHAND_TYPE AS TABLE ( " +
                                 " RID   bigint ," +
                                 " CASHDATE   datetime   ," +
                                 " CASHAMT   decimal (18, 2)  ," +
                                 " CASHSTATUS   nvarchar (50)  ," +
                                 " CASHPERSONNAME   nvarchar (200)  ," +
                                 " CASHREMARK   nvarchar (500)  ," +
                                 " CASHDESC   nvarchar (max)  ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit   ," +
                                 " EMPRID   bigint   ," +
                                 " CASHNO   nvarchar (100)  ," +
                                 " SENDDATA BIT ) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);


                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTEXPENCES");
                this.DeleteTYPEFromOnlineDb("MSTEXPENCE_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTEXPENCE_TYPE AS TABLE ( " +
                                " RID   bigint ," +
                                " EXCODE   nvarchar (50)  ," +
                                " EXNAME   nvarchar (500)  ," +
                                " EXREMARK   nvarchar (500)  ," +
                                " EXDESC   nvarchar (max) ," +
                                " AUSERID   bigint   ," +
                                " ADATETIME   datetime   ," +
                                " EUSERID   bigint   ," +
                                " EDATETIME   datetime   ," +
                                " DUSERID   bigint   ," +
                                " DDATETIME   datetime   ," +
                                " DELFLG   bit   ," +
                                " SENDDATA BIT," +
                                " isopecost bit," +
                                " isfuelcost bit " +
                           " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTINCOME");
                this.DeleteTYPEFromOnlineDb("MSTINCOME_TYPE");
                strtype = "";
                strtype = " CREATE TYPE MSTINCOME_TYPE AS TABLE ( " +
                                " RID   bigint ," +
                                " INCODE   nvarchar (50)  ," +
                                " INNAME   nvarchar (500)  ," +
                                " INREMARK   nvarchar (500)  ," +
                                " INDESC   nvarchar (max)  ," +
                                " AUSERID   bigint   ," +
                                " ADATETIME   datetime   ," +
                                " EUSERID   bigint   ," +
                                " EDATETIME   datetime   ," +
                                " DUSERID   bigint   ," +
                                " DDATETIME   datetime   ," +
                                " DELFLG   bit   ," +
                                " SENDDATA BIT" +
                           " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);


                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTTIEUPCOMPANY");
                this.DeleteTYPEFromOnlineDb("MSTTIEUPCOMPANY_TYPE");
                strtype = "";
                strtype = "CREATE TYPE MSTTIEUPCOMPANY_TYPE AS TABLE (" +
                                    " RID   bigint ," +
                                    " COMPCODE   nvarchar (50)  ," +
                                    " COMPNAME   nvarchar (500)  ," +
                                    " CONTPER   nvarchar (500)  ," +
                                    " CONTNO   nvarchar (500)  ," +
                                    " COMPDISC   decimal (18, 3)  ," +
                                    " COMPREMARK   nvarchar (max)  ," +
                                    " AUSERID   bigint   ," +
                                    " ADATETIME   datetime   ," +
                                    " EUSERID   bigint   ," +
                                    " EDATETIME   datetime   ," +
                                    " DUSERID   bigint   ," +
                                    " DDATETIME   datetime   ," +
                                    " DELFLG   bit ," +
                                    " PAYMENTBY nvarchar (100)  ," +
                                    " SENDDATA BIT, " +
                                    " SALECOMMIPER DECIMAL(18,3)," +
                                    " COMMIPER DECIMAL(18,3) " +
                               " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTSUPPLIER");
                this.DeleteTYPEFromOnlineDb("MSTSUPPLIER_TYPE");
                strtype = "";
                strtype = "CREATE TYPE MSTSUPPLIER_TYPE AS TABLE(" +
                                 " RID   bigint," +
                                 " SUPPCODE   nvarchar (50)  ," +
                                 " SUPPNAME   nvarchar (250)  ," +
                                 " SUPPADD1   nvarchar (100)  ," +
                                 " SUPPADD2   nvarchar (100)  ," +
                                 " SUPPADD3   nvarchar (100)  ," +
                                 " SUPPCITYID   bigint   ," +
                                 " SUPPSTATEID   bigint   ," +
                                 " SUPPCOUNTRYID   bigint   ," +
                                 " SUPPPIN   nvarchar (50)  ," +
                                 " SUPPTELNO   nvarchar (50)  ," +
                                 " SUPPMOBNO   nvarchar (50)  ," +
                                 " SUPPFAXNO   nvarchar (50)  ," +
                                 " SUPPCONTPERNAME1   nvarchar (250)  ," +
                                 " SUPPCONTPERNAME2   nvarchar (250)  ," +
                                 " SUPPEMAIL   nvarchar (250)  ," +
                                 " SUPPPANNO   nvarchar (50)  ," +
                                 " SUPPTINNO   nvarchar (50)  ," +
                                 " SUPPCSTNO   nvarchar (50)  ," +
                                 " SUPPGSTNO   nvarchar (50)  ," +
                                 " SUPPREMARK   nvarchar (max)  ," +
                                 " SUPPIMAGE   image   ," +
                                 " AUSERID   bigint   ," +
                                 " ADATETIME   datetime   ," +
                                 " EUSERID   bigint   ," +
                                 " EDATETIME   datetime   ," +
                                 " DUSERID   bigint   ," +
                                 " DDATETIME   datetime   ," +
                                 " DELFLG   bit  ," +
                                 " SUPPTYPE   nvarchar (1000)," +
                                 " SENDDATA BIT, " +
                                 " SUPPPAYMENT nvarchar (50) ," +
                                 " ITEMTYPE nvarchar (50) " +
                             " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);


                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_TABLERESERVATION");
                this.DeleteTYPEFromOnlineDb("TABLERESERVATION_TYPE");
                strtype = "";
                strtype = "CREATE TYPE TABLERESERVATION_TYPE AS TABLE(" +
                                 " RID   bigint ," +
                                 " REVNO   nvarchar (20) ," +
                                 " BODATE   datetime  ," +
                                 " REVDATE   datetime ," +
                                 " REVTIME   datetime ," +
                                 " CUSTRID   bigint ," +
                                 " TABLERID   bigint," +
                                 " PAX   bigint ," +
                                 " FUNCNAME   nvarchar (500)," +
                                 " SPREQ   nvarchar (max)," +
                                 " REVDESC   nvarchar (max)," +
                                 " ENTRYBY   nvarchar (500)," +
                                 " AUSERID   bigint ," +
                                 " ADATETIME   datetime," +
                                 " EUSERID   bigint ," +
                                 " EDATETIME   datetime," +
                                 " DUSERID   bigint," +
                                 " DDATETIME   datetime," +
                                 " DELFLG   bit," +
                                " SENDDATA BIT" +
                            " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_INCOME");
                this.DeleteTYPEFromOnlineDb("INCOME_TYPE");
                strtype = "";
                strtype = "CREATE TYPE INCOME_TYPE AS TABLE (" +
                                    " RID bigint ," +
                                    " INCOMENO NVARCHAR(200)," +
                                    " INDATE DATETIME," +
                                    " INTIME DATETIME," +
                                    " INRID BIGINT," +
                                    " INTYPE NVARCHAR(200)," +
                                    " INAMOUNT DECIMAL(18,3)," +
                                    " INPERNAME NVARCHAR(200)," +
                                    " INCONTNO NVARCHAR(200)," +
                                    " REMARK1 NVARCHAR(500)," +
                                    " REMARK2 NVARCHAR(500)," +
                                    " REMARK3 NVARCHAR(500)," +
                                    " INDESC NVARCHAR(MAX)," +
                                    " AUSERID bigint," +
                                    " ADATETIME datetime," +
                                    " EUSERID bigint," +
                                    " EDATETIME datetime," +
                                    " DUSERID bigint," +
                                    " DDATETIME datetime," +
                                    " DELFLG bit," +
                                    " SENDDATA BIT " +
                                    " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_EXPENCE");
                this.DeleteTYPEFromOnlineDb("EXPENCE_TYPE");
                strtype = "";
                strtype = "CREATE TYPE EXPENCE_TYPE AS TABLE ( " +
                                " RID bigint," +
                                " EXPENCENO NVARCHAR(200)," +
                                " EXDATE DATETIME," +
                                " EXTIME DATETIME," +
                                " EXRID BIGINT," +
                                " EXTYPE NVARCHAR(200)," +
                                " EXAMOUNT DECIMAL(18,3)," +
                                " EXPERNAME NVARCHAR(200)," +
                                " EXCONTNO NVARCHAR(200)," +
                                " REMARK1 NVARCHAR(500)," +
                                " REMARK2 NVARCHAR(500)," +
                                " REMARK3 NVARCHAR(500)," +
                                " EXDESC NVARCHAR(MAX)," +
                                " AUSERID bigint," +
                                " ADATETIME datetime," +
                                " EUSERID bigint," +
                                " EDATETIME datetime," +
                                " DUSERID bigint," +
                                " DDATETIME datetime," +
                                " DELFLG bit," +
                                " SENDDATA BIT " +
                               " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_OPCASH");
                this.DeleteTYPEFromOnlineDb("OPCASH_TYPE");
                strtype = "";
                strtype = "CREATE TYPE OPCASH_TYPE AS TABLE (" +
                                    " RID bigint ," +
                                    " OPCASHDATE DATETIME," +
                                    " OPAMT DECIMAL(18,3)," +
                                    " OPREMARK NVARCHAR(MAX)," +
                                    " OPENTRYBY NVARCHAR(MAX)," +
                                    " AUSERID bigint," +
                                    " ADATETIME datetime," +
                                    " EUSERID bigint," +
                                    " EDATETIME datetime," +
                                    " DUSERID bigint," +
                                    " DDATETIME datetime," +
                                    " DELFLG bit," +
                                    " SENDDATA BIT " +
                                    " )";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region CREATE PROCEDURE

        private bool DeleteProcedureFromOnlineDb(string ProcName)
        {
            string sqlstr;
            try
            {
                sqlstr = "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + ProcName + "')" +
                        "Drop procedure " + ProcName;
                this.ExecuteMsSqlOnlineCommand_NoMsg(sqlstr);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Create_OnlineDb_PROCEDURE()
        {
            string strtype = "";
            try
            {

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTCITY");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTCITY " +
                                 " @TBLMSTCITY MSTCITY_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN" +
                                          " UPDATE MSTCITY " +
                                          " SET CITYID = c2.CITYID,CITYNAME = c2.CITYNAME " +
                                          " FROM MSTCITY c1" +
                                          " INNER JOIN @TBLMSTCITY c2" +
                                          " ON c1.CITYNAME = c2.CITYNAME" +
                                          " INSERT INTO MSTCITY ( CITYID, CITYNAME) " +
                                          " SELECT CITYID, CITYNAME " +
                                          " FROM @TBLMSTCITY" +
                                          " WHERE CITYNAME NOT IN(SELECT CITYNAME FROM MSTCITY)" +
                                    " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTCOUNTRY");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTCOUNTRY " +
                                 " @TBLMSTCOUNTRY MSTCOUNTRY_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN" +
                                          " UPDATE MSTCOUNTRY " +
                                          " SET COUNTRYID = c2.COUNTRYID,COUNTRYNAME = c2.COUNTRYNAME " +
                                          " FROM MSTCOUNTRY c1" +
                                          " INNER JOIN @TBLMSTCOUNTRY c2" +
                                          " ON c1.COUNTRYNAME = c2.COUNTRYNAME" +
                                          " INSERT INTO MSTCOUNTRY ( COUNTRYID, COUNTRYNAME) " +
                                          " SELECT COUNTRYID, COUNTRYNAME " +
                                          " FROM @TBLMSTCOUNTRY " +
                                          " WHERE COUNTRYNAME NOT IN(SELECT COUNTRYNAME FROM MSTCOUNTRY)" +
                                    " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTSTATE");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTSTATE " +
                                 " @TBLMSTSTATE MSTSTATE_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN" +
                                          " UPDATE MSTSTATE " +
                                          " SET STATEID = c2.STATEID,STATENAME = c2.STATENAME " +
                                          " FROM MSTSTATE c1" +
                                          " INNER JOIN @TBLMSTSTATE c2" +
                                          " ON c1.STATENAME = c2.STATENAME" +
                                          " INSERT INTO MSTSTATE ( STATEID, STATENAME) " +
                                          " SELECT STATEID, STATENAME " +
                                          " FROM @TBLMSTSTATE " +
                                          " WHERE STATENAME NOT IN(SELECT STATENAME FROM MSTSTATE)" +
                                    " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);


                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTUNIT");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTUNIT " +
                                 " @TBLMSTUNIT MSTUNIT_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN" +
                                          " UPDATE MSTUNIT " +
                                          " SET RID = c2.RID,UNITCODE = c2.UNITCODE,UNITNAME=c2.UNITNAME,UNITDESC=c2.UNITDESC,AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME," +
                                              " EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                          " FROM MSTUNIT c1" +
                                          " INNER JOIN @TBLMSTUNIT c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTUNIT ( RID, UNITCODE, UNITNAME,UNITDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA )" +
                                          " SELECT RID, UNITCODE, UNITNAME,UNITDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA" +
                                          " FROM @TBLMSTUNIT" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTUNIT)" +
                                    " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTDEPT");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTDEPT " +
                                 " @TBLMSTDEPT MSTDEPT_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN" +
                                          " UPDATE MSTDEPT " +
                                          " SET RID = c2.RID,DEPTCODE = c2.DEPTCODE,DEPTNAME=c2.DEPTNAME,DEPTDESC=c2.DEPTDESC,AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME," +
                                              " EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,ISBARDEPT=c2.ISBARDEPT, " +
                                              " ISKITCHENDEPT=c2.ISKITCHENDEPT,ISHUKKADEPT=c2.ISHUKKADEPT," +
                                              " SENDDATA=c2.SENDDATA" +
                                          " FROM MSTDEPT c1" +
                                          " INNER JOIN @TBLMSTDEPT c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTDEPT (RID, DEPTCODE, DEPTNAME,DEPTDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,ISBARDEPT,ISKITCHENDEPT,ISHUKKADEPT,SENDDATA )" +
                                          " SELECT RID, DEPTCODE, DEPTNAME,DEPTDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,ISBARDEPT,ISKITCHENDEPT,ISHUKKADEPT,SENDDATA" +
                                          " FROM @TBLMSTDEPT" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTDEPT)" +
                                    " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTUSERS");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTUSERS " +
                                 " @TBLMSTUSERS MSTUSERS_TYPE READONLY " +
                                   " AS " +
                                    " BEGIN" +
                                          " UPDATE MSTUSERS " +
                                          " SET USERNAME = c2.USERNAME,PASSWORD=c2.PASSWORD,REPASSWORD=c2.REPASSWORD,AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME," +
                                              " EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG, " +
                                              " USERCODE=c2.USERCODE,ISSHOWREPORT=c2.ISSHOWREPORT,HIDEMSTMNU=c2.HIDEMSTMNU,HIDEBANQMNU=c2.HIDEBANQMNU,HIDECRMMNU=c2.HIDECRMMNU," +
                                              " HIDESMSMNU=c2.HIDESMSMNU,HIDEPAYROLLMNU=c2.HIDEPAYROLLMNU,HIDEUTILITYMNU=c2.HIDEUTILITYMNU,HIDEKOTBILLENT=c2.HIDEKOTBILLENT,HIDECASHMENOENT=c2.HIDECASHMENOENT,HIDESETTLEMENTENT=c2.HIDESETTLEMENTENT," +
                                              " HIDEQUICKBILLENT=c2.HIDEQUICKBILLENT,HIDETABLEVIEWENT=c2.HIDETABLEVIEWENT,HIDEMULTISETTLEMENTENT=c2.HIDEMULTISETTLEMENTENT,HIDECASHONHANDENT=c2.HIDECASHONHANDENT,HIDESUPPLIERENT=c2.HIDESUPPLIERENT,HIDEPURCHASEENT=c2.HIDEPURCHASEENT," +
                                              " HIDEPAYMENYENT=c2.HIDEPAYMENYENT,HIDECHECKLISTENT=c2.HIDECHECKLISTENT,HIDEBUSINESSSUMMARY=c2.HIDEBUSINESSSUMMARY,DONTALLOWBILLEDIT=c2.DONTALLOWBILLEDIT,DONTALLOWBILLDELETE=c2.DONTALLOWBILLDELETE,DONTALLOWTBLCLEAR=c2.DONTALLOWTBLCLEAR," +
                                              " HIDESTKISSUEENT=c2.HIDESTKISSUEENT,DONTALLOWBANQBOEDIT=c2.DONTALLOWBANQBOEDIT,DONTALLOWBANQBODELETE=c2.DONTALLOWBANQBODELETE,DONTALLOWCHKLISTEDIT=c2.DONTALLOWCHKLISTEDIT,DONTALLOWCHKLISTDELETE=c2.DONTALLOWCHKLISTDELETE,DONTALLOWPURCHASEEDIT=c2.DONTALLOWPURCHASEEDIT," +
                                              " DONTALLOWPURCHASEDELETE=c2.DONTALLOWPURCHASEDELETE,DONTALLOWSTKISSUEEDIT=c2.DONTALLOWSTKISSUEEDIT,DONTALLOWSTKISSUEDELETE=c2.DONTALLOWSTKISSUEDELETE,HIDEPURITEMGROUPENT=c2.HIDEPURITEMGROUPENT,HIDEPURITEMENT=c2.HIDEPURITEMENT,DONTALLOWCHANGEDATEINKOTBILL=c2.DONTALLOWCHANGEDATEINKOTBILL," +
                                              " HIDEPURCHASEICO=c2.HIDEPURCHASEICO,ISTABLETUSER=c2.ISTABLETUSER,DONTALLOWKOTEDIT=c2.DONTALLOWKOTEDIT,DONTALLOWKOTDELETE=c2.DONTALLOWKOTDELETE,DONTALLOWDISCINBILL=c2.DONTALLOWDISCINBILL,HIDEINVMNU=c2.HIDEINVMNU," +
                                              " SENDDATA=c2.SENDDATA" +
                                          " FROM MSTUSERS c1" +
                                          " INNER JOIN @TBLMSTUSERS c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTUSERS ( RID, USERNAME, PASSWORD,REPASSWORD,USERDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,USERCODE,ISSHOWREPORT,HIDEMSTMNU,HIDEBANQMNU,HIDECRMMNU,HIDESMSMNU,HIDEPAYROLLMNU,HIDEUTILITYMNU," +
                                          " HIDEKOTBILLENT,HIDECASHMENOENT,HIDESETTLEMENTENT,HIDEQUICKBILLENT,HIDETABLEVIEWENT,HIDEMULTISETTLEMENTENT,HIDECASHONHANDENT,HIDESUPPLIERENT,HIDEPURCHASEENT,HIDEPAYMENYENT,HIDECHECKLISTENT,HIDEBUSINESSSUMMARY," +
                                          " DONTALLOWBILLEDIT,DONTALLOWBILLDELETE,DONTALLOWTBLCLEAR,HIDESTKISSUEENT,DONTALLOWBANQBOEDIT,DONTALLOWBANQBODELETE,DONTALLOWCHKLISTEDIT,DONTALLOWCHKLISTDELETE,DONTALLOWPURCHASEEDIT,DONTALLOWPURCHASEDELETE,DONTALLOWSTKISSUEEDIT," +
                                          " DONTALLOWSTKISSUEDELETE,HIDEPURITEMGROUPENT,HIDEPURITEMENT,DONTALLOWCHANGEDATEINKOTBILL,HIDEPURCHASEICO,ISTABLETUSER,DONTALLOWKOTEDIT,DONTALLOWKOTDELETE,DONTALLOWDISCINBILL,HIDEINVMNU,SENDDATA ) " +
                                          " SELECT RID, USERNAME, PASSWORD,REPASSWORD,USERDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,USERCODE,ISSHOWREPORT,HIDEMSTMNU,HIDEBANQMNU,HIDECRMMNU,HIDESMSMNU,HIDEPAYROLLMNU,HIDEUTILITYMNU," +
                                          " HIDEKOTBILLENT,HIDECASHMENOENT,HIDESETTLEMENTENT,HIDEQUICKBILLENT,HIDETABLEVIEWENT,HIDEMULTISETTLEMENTENT,HIDECASHONHANDENT,HIDESUPPLIERENT,HIDEPURCHASEENT,HIDEPAYMENYENT,HIDECHECKLISTENT,HIDEBUSINESSSUMMARY," +
                                          " DONTALLOWBILLEDIT,DONTALLOWBILLDELETE,DONTALLOWTBLCLEAR,HIDESTKISSUEENT,DONTALLOWBANQBOEDIT,DONTALLOWBANQBODELETE,DONTALLOWCHKLISTEDIT,DONTALLOWCHKLISTDELETE,DONTALLOWPURCHASEEDIT,DONTALLOWPURCHASEDELETE,DONTALLOWSTKISSUEEDIT," +
                                          " DONTALLOWSTKISSUEDELETE,HIDEPURITEMGROUPENT,HIDEPURITEMENT,DONTALLOWCHANGEDATEINKOTBILL,HIDEPURCHASEICO,ISTABLETUSER,DONTALLOWKOTEDIT,DONTALLOWKOTDELETE,DONTALLOWDISCINBILL,HIDEINVMNU,SENDDATA " +
                                          " FROM @TBLMSTUSERS" +
                                          " WHERE RID NOT IN (SELECT RID FROM MSTUSERS)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTREPORTDEPT");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTREPORTDEPT " +
                                 " @TBLMSTREPORTDEPT MSTREPORTDEPT_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN" +
                                          " UPDATE MSTREPORTDEPT " +
                                          " SET RID = c2.RID,REPORTDEPTCODE = c2.REPORTDEPTCODE,REPORTDEPTNAME=c2.REPORTDEPTNAME,REPORTDEPTDESC=c2.REPORTDEPTDESC, " +
                                            " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                          " FROM MSTREPORTDEPT c1" +
                                          " INNER JOIN @TBLMSTREPORTDEPT c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTREPORTDEPT ( RID, REPORTDEPTCODE, REPORTDEPTNAME,REPORTDEPTDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA )" +
                                          " SELECT RID, REPORTDEPTCODE, REPORTDEPTNAME,REPORTDEPTDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA" +
                                          " FROM @TBLMSTREPORTDEPT" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTREPORTDEPT)" +
                                    " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTHSNCODE");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTHSNCODE " +
                                 " @TBLMSTHSNCODE MSTHSNCODE_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTHSNCODE " +
                                    " SET RID=c2.RID,HSNCODE=c2.HSNCODE,HSNCODEREMARK=c2.HSNCODEREMARK,ACTOTGSTPER=c2.ACTOTGSTPER,ACCGSTPER=c2.ACCGSTPER,ACSGSTPER=c2.ACSGSTPER,ACIGSTPER=c2.ACIGSTPER," +
                                    " NACTOTGSTPER=c2.NACTOTGSTPER,NACCGSTPER=c2.NACCGSTPER,NACSGSTPER=c2.NACSGSTPER,NACIGSTPER=c2.NACIGSTPER,CATOTGSTPER=c2.CATOTGSTPER,CACGSTPER=c2.CACGSTPER,CASGSTPER=c2.CASGSTPER,CAIGSTPER=c2.CAIGSTPER," +
                                    " RSTOTGSTPER=c2.RSTOTGSTPER,RSCGSTPER=c2.RSCGSTPER,RSSGSTPER=c2.RSSGSTPER,RSIGSTPER=c2.RSIGSTPER,PURTOTGSTPER=c2.PURTOTGSTPER,PURCGSTPER=c2.PURCGSTPER,PURSGSTPER=c2.PURSGSTPER,PURIGSTPER=c2.PURIGSTPER," +
                                    " BANQTOTGSTPER=c2.BANQTOTGSTPER,BANQCGSTPER=c2.BANQCGSTPER,BANQSGSTPER=c2.BANQSGSTPER,BANQIGSTPER=c2.BANQIGSTPER," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTHSNCODE c1" +
                                          " INNER JOIN @TBLMSTHSNCODE c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTHSNCODE ( RID,HSNCODE,HSNCODEREMARK,ACTOTGSTPER,ACCGSTPER,ACSGSTPER,ACIGSTPER," +
                                                        " NACTOTGSTPER,NACCGSTPER,NACSGSTPER,NACIGSTPER,CATOTGSTPER,CACGSTPER,CASGSTPER,CAIGSTPER," +
                                                        " RSTOTGSTPER,RSCGSTPER,RSSGSTPER,RSIGSTPER,PURTOTGSTPER,PURCGSTPER,PURSGSTPER,PURIGSTPER," +
                                                        " BANQTOTGSTPER,BANQCGSTPER,BANQSGSTPER,BANQIGSTPER," +
                                                        " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA )" +
                                          " SELECT RID,HSNCODE,HSNCODEREMARK,ACTOTGSTPER,ACCGSTPER,ACSGSTPER,ACIGSTPER," +
                                                        " NACTOTGSTPER,NACCGSTPER,NACSGSTPER,NACIGSTPER,CATOTGSTPER,CACGSTPER,CASGSTPER,CAIGSTPER," +
                                                        " RSTOTGSTPER,RSCGSTPER,RSSGSTPER,RSIGSTPER,PURTOTGSTPER,PURCGSTPER,PURSGSTPER,PURIGSTPER," +
                                                        " BANQTOTGSTPER,BANQCGSTPER,BANQSGSTPER,BANQIGSTPER," +
                                                        " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA " +
                                          " FROM @TBLMSTHSNCODE" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTHSNCODE)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEMGROUP");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTITEMGROUP " +
                                 " @TBLMSTITEMGROUP MSTITEMGROUP_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTITEMGROUP " +
                                    " SET igcode=c2.igcode, " +
                                    "  igname = c2.igname, igdesc = c2.igdesc, " +
                                    "  notapplydisc=c2.notapplydisc, " +
                                    "  igpname=c2.igpname,igdispord=c2.igdispord, " +
                                    "  ISHIDEGROUP=c2.ISHIDEGROUP," +
                                    "  showindiffcolor=c2.showindiffcolor,ishidegroupkot=c2.ishidegroupkot,ISITEMREMGRP=c2.ISITEMREMGRP," +
                                    "  ishidegroupcashmemo=c2.ishidegroupcashmemo," +
                                    "  hsncoderid=c2.hsncoderid,igbackcolor=c2.igbackcolor,igforecolor=c2.igforecolor,igfontname=c2.igfontname,igfontsize=c2.igfontsize,igfontbold=c2.igfontbold, " +
                                    "  igprintord=c2.igprintord,reglangigname=c2.reglangigname, " +
                                    "  AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTITEMGROUP c1 " +
                                          " INNER JOIN @TBLMSTITEMGROUP c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTITEMGROUP ( RID,IGCODE,IGNAME,IGDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " NOTAPPLYDISC,IGPNAME,IGDISPORD,ISHIDEGROUP,SHOWINDIFFCOLOR, " +
                                                                    " ISHIDEGROUPKOT,ISITEMREMGRP,ISHIDEGROUPCASHMEMO," +
                                                                    " HSNCODERID,IGBACKCOLOR,IGFORECOLOR,IGFONTNAME,IGFONTSIZE,IGFONTBOLD, " +
                                                                    " IGPRINTORD,REGLANGIGNAME,SENDDATA )" +
                                          " SELECT RID,IGCODE,IGNAME,IGDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " NOTAPPLYDISC,IGPNAME,IGDISPORD,ISHIDEGROUP,SHOWINDIFFCOLOR, " +
                                                                    " ISHIDEGROUPKOT,ISITEMREMGRP,ISHIDEGROUPCASHMEMO," +
                                                                    " HSNCODERID,IGBACKCOLOR,IGFORECOLOR,IGFONTNAME,IGFONTSIZE,IGFONTBOLD, " +
                                                                    " IGPRINTORD,REGLANGIGNAME,SENDDATA " +
                                          " FROM @TBLMSTITEMGROUP" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTITEMGROUP)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEM");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTITEM " +
                                 " @TBLMSTITEM MSTITEM_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTITEM " +
                                    " SET icode=c2.icode, " +
                                    "  iname = c2.iname, icommi=c2.icommi,ipname=c2.ipname," +
                                    "  iimg = c2.iimg," +
                                    "  igrprid = c2.igrprid,iunitrid = c2.iunitrid," +
                                    "  ideptrid = c2.ideptrid,irate = c2.irate," +
                                    "  idesc = c2.idesc, " +
                                    " iminqty = c2.iminqty,imaxqty = c2.imaxqty,ireqty=c2.ireqty," +
                                    " isitemstock=c2.isitemstock," +
                                    " reportdeptrid=c2.reportdeptrid," +
                                    " ISHIDEITEM=c2.ISHIDEITEM,ISNOTAPPSERTAXDISC=c2.ISNOTAPPSERTAXDISC," +
                                    " idprate=c2.idprate,isrunningitem=c2.isrunningitem," +
                                    " disdiffcolor = c2.disdiffcolor, itemtaxtype=c2.itemtaxtype,ischkrptitem=c2.ischkrptitem," +
                                    " isnotapplyvat=c2.isnotapplyvat,couponcode=c2.couponcode," +
                                    " irewpoint = c2.irewpoint,notapplygsttax=c2.notapplygsttax," +
                                    " hsncoderid=c2.hsncoderid,notapplydisc=c2.notapplydisc,ibackcolor=c2.ibackcolor,iforecolor=c2.iforecolor,ifontname=c2.ifontname,ifontsize=c2.ifontsize,ifontbold=c2.ifontbold, " +
                                    " reglangname=c2.reglangname,iremark=c2.iremark," +
                                    " IPURRATE=C2.IPURRATE,NUT1=C2.NUT1,NUT2=C2.NUT2,NUT3=C2.NUT3,NUT4=C2.NUT4,NUT5=C2.NUT5,NUT6=C2.NUT6,NUT7=C2.NUT7," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTITEM c1 " +
                                          " INNER JOIN @TBLMSTITEM c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTITEM (RID,icode,iname,IIMG,IGRPRID,IUNITRID,IDEPTRID,IRATE,IDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " icommi,ipname,IMINQTY,IMAXQTY,IREQTY,ISITEMSTOCK,REPORTDEPTRID,ISHIDEITEM,ISNOTAPPSERTAXDISC,IDPRATE,ISRUNNINGITEM,DISDIFFCOLOR," +
                                          " ITEMTAXTYPE,ISCHKRPTITEM,ISNOTAPPLYVAT,COUPONCODE,IREWPOINT,NOTAPPLYGSTTAX,NOTAPPLYDISC,HSNCODERID,IBACKCOLOR,IFORECOLOR,IFONTNAME," +
                                          " IFONTSIZE,IFONTBOLD,REGLANGNAME,IREMARK,SENDDATA, " +
                                          " IPURRATE,NUT1,NUT2,NUT3,NUT4,NUT5,NUT6,NUT7 " +
                                          " )" +
                                          " SELECT RID,icode,iname,IIMG,IGRPRID,IUNITRID,IDEPTRID,IRATE,IDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " icommi,ipname,IMINQTY,IMAXQTY,IREQTY,ISITEMSTOCK,REPORTDEPTRID,ISHIDEITEM,ISNOTAPPSERTAXDISC,IDPRATE,ISRUNNINGITEM,DISDIFFCOLOR," +
                                          " ITEMTAXTYPE,ISCHKRPTITEM,ISNOTAPPLYVAT,COUPONCODE,IREWPOINT,NOTAPPLYGSTTAX,NOTAPPLYDISC,HSNCODERID,IBACKCOLOR,IFORECOLOR,IFONTNAME," +
                                          " IFONTSIZE,IFONTBOLD,REGLANGNAME,IREMARK,SENDDATA," +
                                          " IPURRATE,NUT1,NUT2,NUT3,NUT4,NUT5,NUT6,NUT7 " +
                                          " FROM @TBLMSTITEM" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTITEM)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEMPRICELIST");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTITEMPRICELIST " +
                                 " @TBLMSTITEMPRICELIST MSTITEMPRICELIST_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTITEMPRICELIST " +
                                    " SET IPLCODE=c2.IPLCODE, " +
                                    " IPLNAME = c2.IPLNAME, IPLDESC=c2.IPLDESC," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTITEMPRICELIST c1 " +
                                          " INNER JOIN @TBLMSTITEMPRICELIST c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTITEMPRICELIST ( RID,IPLCODE,IPLNAME,IPLDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA )" +
                                          " SELECT RID,IPLCODE,IPLNAME,IPLDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA" +
                                          " FROM @TBLMSTITEMPRICELIST" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTITEMPRICELIST)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTITEMPRICELISTDTL");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTITEMPRICELISTDTL " +
                                 " @TBLMSTITEMPRICELISTDTL MSTITEMPRICELISTDTL_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTITEMPRICELISTDTL " +
                                    " SET IPLRID=c2.IPLRID, " +
                                    " IRID = c2.IRID, IRATE=c2.IRATE,IPRATE=c2.IPRATE," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTITEMPRICELISTDTL c1 " +
                                          " INNER JOIN @TBLMSTITEMPRICELISTDTL c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTITEMPRICELISTDTL (RID,IPLRID,IRID,IRATE,IPRATE," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA )" +
                                          " SELECT RID,IPLRID,IRID,IRATE,IPRATE," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA" +
                                          " FROM @TBLMSTITEMPRICELISTDTL" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTITEMPRICELISTDTL)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTREMARK");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTREMARK " +
                                 " @TBLMSTREMARK MSTREMARK_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTREMARK " +
                                    " SET REMARKCODE=c2.REMARKCODE, " +
                                    " REMARKNAME = c2.REMARKNAME, REMARKDESC=c2.REMARKDESC," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTREMARK c1 " +
                                          " INNER JOIN @TBLMSTREMARK c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTREMARK (RID,REMARKCODE,REMARKNAME,REMARKDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA )" +
                                          " SELECT RID,REMARKCODE,REMARKNAME,REMARKDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA " +
                                          " FROM @TBLMSTREMARK " +
                                          " WHERE RID NOT IN (SELECT RID FROM MSTREMARK)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTTABLE");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTTABLE " +
                                 " @TBLMSTTABLE MSTTABLE_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTTABLE " +
                                    " SET TABLECODE=c2.TABLECODE, " +
                                    " TABLENAME = c2.TABLENAME, TABLEDESC=c2.TABLEDESC," +
                                    " ISROOMTABLE = c2.ISROOMTABLE,ISPARCELTABLE=c2.ISPARCELTABLE,ROOMNO=c2.ROOMNO,TABDISC=c2.TABDISC,PRICELISTRID=c2.PRICELISTRID,ISHOMEDELITABLE=c2.ISHOMEDELITABLE,ISNOTCALCCOMMI=c2.ISNOTCALCCOMMI," +
                                    " ISHIDETABLE=c2.ISHIDETABLE,TABPAX=c2.TABPAX,TABDISPORD=c2.TABDISPORD,GSTTAXTYPE=c2.GSTTAXTYPE,TABLEGROUP=c2.TABLEGROUP,SECNO=c2.SECNO,MSTTIEUPCOMPRID=c2.MSTTIEUPCOMPRID, " +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTTABLE c1 " +
                                          " INNER JOIN @TBLMSTTABLE c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTTABLE ( RID,TABLECODE,TABLENAME,TABLEDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " ISROOMTABLE,ISPARCELTABLE,ROOMNO,TABDISC,PRICELISTRID,ISHOMEDELITABLE,ISNOTCALCCOMMI,ISHIDETABLE,TABPAX,TABDISPORD,GSTTAXTYPE,SECNO,MSTTIEUPCOMPRID," +
                                          " SENDDATA )" +
                                          " SELECT RID,TABLECODE,TABLENAME,TABLEDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " ISROOMTABLE,ISPARCELTABLE,ROOMNO,TABDISC,PRICELISTRID,ISHOMEDELITABLE,ISNOTCALCCOMMI,ISHIDETABLE,TABPAX,TABDISPORD,GSTTAXTYPE,SECNO,MSTTIEUPCOMPRID," +
                                          " SENDDATA" +
                                          " FROM @TBLMSTTABLE" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTTABLE)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTEMPCAT");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTEMPCAT " +
                                 " @TBLMSTEMPCAT MSTEMPCAT_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTEMPCAT " +
                                    " SET EMPCATCODE=c2.EMPCATCODE, " +
                                    " EMPCATNAME = c2.EMPCATNAME, EMPCATDESC=c2.EMPCATDESC," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA" +
                                    " FROM MSTEMPCAT c1 " +
                                          " INNER JOIN @TBLMSTEMPCAT c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTEMPCAT ( RID,EMPCATCODE,EMPCATNAME,EMPCATDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " SENDDATA )" +
                                          " SELECT RID,EMPCATCODE,EMPCATNAME,EMPCATDESC," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " SENDDATA" +
                                          " FROM @TBLMSTEMPCAT" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTEMPCAT)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTEMP");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTEMP " +
                                 " @TBLMSTEMP MSTEMP_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTEMP " +
                                    " SET EMPCODE=c2.EMPCODE, " +
                                    " EMPNAME = c2.EMPNAME, EMPFATHERNAME=c2.EMPFATHERNAME,EMPADD1=c2.EMPADD1,EMPADD2=c2.EMPADD2,EMPADD3=c2.EMPADD3,EMPCATID=c2.EMPCATID,EMPCITYID=c2.EMPCITYID,EMPSTATEID=c2.EMPSTATEID," +
                                    " EMPCOUNTRYID=c2.EMPCOUNTRYID,EMPPIN=c2.EMPPIN,EMPTELNO=c2.EMPTELNO,EMPMOBILENO=c2.EMPMOBILENO,EMPEMAIL=c2.EMPEMAIL,EMPFAXNO=c2.EMPFAXNO,EMPBIRTHDATE=c2.EMPBIRTHDATE,EMPJOINDATE=c2.EMPJOINDATE," +
                                    " EMPLEAVEDATE=c2.EMPLEAVEDATE,EMPGENDER=c2.EMPGENDER,EMPMARITALSTATUS=c2.EMPMARITALSTATUS,ISDISPINKOT=c2.ISDISPINKOT,EMPIMAGE=c2.EMPIMAGE,EMPDESC=c2.EMPDESC," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG, " +
                                    " EMPANNIDATE=c2.EMPANNIDATE,EMPBANKNAME=c2.EMPBANKNAME,EMPBANKDETAILS=c2.EMPBANKDETAILS,EMPBANKACCNO=c2.EMPBANKACCNO,ISNONACTIVE=c2.ISNONACTIVE," +
                                    " SENDDATA=c2.SENDDATA" +
                                    " FROM MSTEMP c1 " +
                                          " INNER JOIN @TBLMSTEMP c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTEMP (RID,empcode,empname,empfathername,empadd1,empadd2,empadd3,empcatid,empcityid,empstateid,empcountryid, " +
                                              " emppin,emptelno,empmobileno,empemail,empfaxno,empbirthdate,empjoindate,empleavedate,empgender,empmaritalstatus,isdispinkot, " +
                                              " empimage,empdesc," +
                                              " empbankname,empbankdetails,empbankaccno,isnonactive, " +
                                              " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                              " SENDDATA ) " +
                                          " SELECT RID,empcode,empname,empfathername,empadd1,empadd2,empadd3,empcatid,empcityid,empstateid,empcountryid, " +
                                              " emppin,emptelno,empmobileno,empemail,empfaxno,empbirthdate,empjoindate,empleavedate,empgender,empmaritalstatus,isdispinkot, " +
                                              " empimage,empdesc," +
                                              " empbankname,empbankdetails,empbankaccno,isnonactive, " +
                                              " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                              " SENDDATA " +
                                          " FROM @TBLMSTEMP" +
                                          " WHERE RID NOT IN (SELECT RID FROM MSTEMP)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_KOT");
                strtype = " CREATE PROCEDURE sp_SENDDATA_KOT " +
                                 " @TBLKOT KOT_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE KOT " +
                                    " SET KOTDATE=c2.KOTDATE, " +
                                    " KOTTIME = c2.KOTTIME, KOTNO=c2.KOTNO,KOTTOKNO=c2.KOTTOKNO,KOTORDERPERID=c2.KOTORDERPERID,ISPARSELKOT=c2.ISPARSELKOT,KOTTABLEID=c2.KOTTABLEID,KOTTABLENAME=c2.KOTTABLENAME,KOTREMARK=c2.KOTREMARK," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG, " +
                                    " KOTPAX=c2.KOTPAX,CUSTRID=c2.CUSTRID,CUSTNAME=c2.CUSTNAME,CUSTADD=c2.CUSTADD,KOTINFO=c2.KOTINFO,CARDNO=c2.CARDNO,REFKOTNO=c2.REFKOTNO,REFKOTNUM=c2.REFKOTNUM,ISCOMPKOT=c2.ISCOMPKOT," +
                                    " SENDDATA=c2.SENDDATA" +
                                    " FROM KOT c1 " +
                                          " INNER JOIN @TBLKOT c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO KOT (RID,KOTDATE,KOTTIME,KOTNO,KOTTOKNO,KOTORDERPERID,ISPARSELKOT,KOTTABLEID,KOTTABLENAME,KOTREMARK, " +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " KOTPAX,CUSTRID,CUSTNAME,CUSTADD,KOTINFO,CARDNO,REFKOTNO,REFKOTNUM,ISCOMPKOT," +
                                          " SENDDATA )" +
                                          " SELECT RID,KOTDATE,KOTTIME,KOTNO,KOTTOKNO,KOTORDERPERID,ISPARSELKOT,KOTTABLEID,KOTTABLENAME,KOTREMARK, " +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " KOTPAX,CUSTRID,CUSTNAME,CUSTADD,KOTINFO,CARDNO,REFKOTNO,REFKOTNUM,ISCOMPKOT," +
                                          " SENDDATA " +
                                          " FROM @TBLKOT" +
                                          " WHERE RID NOT IN(SELECT RID FROM KOT)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_KOTDTL");
                strtype = " CREATE PROCEDURE sp_SENDDATA_KOTDTL " +
                                 " @TBLKOTDTL KOTDTL_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE KOTDTL " +
                                    " SET KOTRID=c2.KOTRID, " +
                                    " IRID = c2.IRID, INAME=c2.INAME,IQTY=c2.IQTY,IRATE=c2.IRATE,IAMT=c2.IAMT, " +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG, " +
                                    " IREMARK=c2.IREMARK,IMODIFIER=c2.IMODIFIER,ICOMPITEM=c2.ICOMPITEM, " +
                                    " SENDDATA=c2.SENDDATA " +
                                    " FROM KOTDTL c1 " +
                                          " INNER JOIN @TBLKOTDTL c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO KOTDTL (RID,KOTRID,IRID,INAME,IQTY,IRATE,IAMT, " +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " IREMARK,IMODIFIER,ICOMPITEM, " +
                                          " SENDDATA ) " +
                                          " SELECT RID,KOTRID,IRID,INAME,IQTY,IRATE,IAMT, " +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                          " IREMARK,IMODIFIER,ICOMPITEM, " +
                                          " SENDDATA " +
                                          " FROM @TBLKOTDTL" +
                                          " WHERE RID NOT IN(SELECT RID FROM KOTDTL)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_BILL");
                strtype = " CREATE PROCEDURE sp_SENDDATA_BILL " +
                                 " @TBLBILL BILL_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE BILL " +
                                    " SET billno=c2.billno,billdate=c2.billdate,billtype=c2.billtype,custname=c2.custname,custcontno=c2.custcontno,tablerid=c2.tablerid,pricelistrid=c2.pricelistrid,billpax=c2.billpax," +
                                                          "totamount=c2.totamount,totsertaxper=c2.totsertaxper,totsertaxamount=c2.totsertaxamount,totvatper=c2.totvatper,totvatamount=c2.totvatamount,totaddvatper=c2.totaddvatper,totaddvatamount=c2.totaddvatamount," +
                                                          "totdiscper=c2.totdiscper,totdiscamount=c2.totdiscamount,totroff=c2.totroff,netamount=c2.netamount,billprepby=c2.billprepby,billremark=c2.billremark," +
                                                          "setletype=c2.setletype,setleamount=c2.setleamount,chequeno=c2.chequeno,chequebankname=c2.chequebankname,creditcardno=c2.creditcardno,creditholdername=c2.creditholdername,creditbankname=c2.creditbankname," +
                                                          " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG," +
                                                          " REFBILLNO=c2.REFBILLNO,REFBILLNUM=c2.REFBILLNUM,custadd=c2.custadd,TOTDISCOUNTABLEAMOUNT=c2.TOTDISCOUNTABLEAMOUNT,ISREVISEDBILL=c2.ISREVISEDBILL,REVISEDBILLSRNO=c2.REVISEDBILLSRNO,REVISEDBILLNO=c2.REVISEDBILLNO,BASEBILLNO=c2.BASEBILLNO,MAINBASEBILLNO=c2.MAINBASEBILLNO,LASTTABLERID=c2.LASTTABLERID,LASTTABLESTATUS=c2.LASTTABLESTATUS, " +
                                                          " BILLORDERPERID=c2.BILLORDERPERID,BILLTIME=c2.BILLTIME,TOTCALCVATPER=c2.TOTCALCVATPER,TOTCALCVATAMOUNT=c2.TOTCALCVATAMOUNT,ISBILLTOCUSTOMER=c2.ISBILLTOCUSTOMER,CNTPRINT=c2.CNTPRINT,TOKENNO=c2.TOKENNO,ISPARCELBILL=c2.ISPARCELBILL,ISCOMPLYBILL=c2.ISCOMPLYBILL,TOTBEVVATPER=c2.TOTBEVVATPER,TOTBEVVATAMT=c2.TOTBEVVATAMT,TOTLIQVATPER=c2.TOTLIQVATPER,TOTLIQVATAMT=c2.TOTLIQVATAMT," +
                                                          " REFBYRID=c2.REFBYRID,BILLINFO=c2.BILLINFO,TOTSERCHRPER=c2.TOTSERCHRPER,TOTSERCHRAMT=c2.TOTSERCHRAMT,TOTNEWTOTALAMT=c2.TOTNEWTOTALAMT,TOTADDDISCAMT=c2.TOTADDDISCAMT,MSTTIEUPCOMPRID=c2.MSTTIEUPCOMPRID,COUPONNO=c2.COUPONNO,COUPONPERNAME=c2.COUPONPERNAME,TOTKKCESSPER=c2.TOTKKCESSPER,TOTKKCESSAMT=c2.TOTKKCESSAMT,OTHERPAYMENTBY=c2.OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1=c2.OTHERPAYMENTBYREMARK1," +
                                                          " OTHERPAYMENTBYREMARK2=c2.OTHERPAYMENTBYREMARK2,PAYMENT=c2.PAYMENT,MULTICASHAMT=c2.MULTICASHAMT,MULTICHEQUEAMT=c2.MULTICHEQUEAMT,MULTICREDITCARDAMT=c2.MULTICREDITCARDAMT,MULTIOTHERAMT=c2.MULTIOTHERAMT,MULTICHQNO=c2.MULTICHQNO,MULTICHQBANKNAME=c2.MULTICHQBANKNAME,MULTICARDNO=c2.MULTICARDNO,MULTICARDBANKNAME=c2.MULTICARDBANKNAME,MULTIOTHERPAYMENTBY=c2.MULTIOTHERPAYMENTBY,MULTIOTHERREMARK1=c2.MULTIOTHERREMARK1," +
                                                          " MULTIOTHERREMARK2=c2.MULTIOTHERREMARK2,MULTITIPAMT=c2.MULTITIPAMT,CARDNO=c2.CARDNO,TOTBILLREWPOINT=c2.TOTBILLREWPOINT,TOTITEMREWPOINT=c2.TOTITEMREWPOINT,CGSTAMT=c2.CGSTAMT,SGSTAMT=c2.SGSTAMT,IGSTAMT=c2.IGSTAMT,TOTGSTAMT=c2.TOTGSTAMT,RECAMT=c2.RECAMT,RETAMT=c2.RETAMT," +
                                                    " SENDDATA=c2.SENDDATA" +
                                                " FROM BILL c1 " +
                                            " INNER JOIN @TBLBILL c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO BILL ( RID,billno,billdate,billtype,custrid,custname,custcontno,tablerid,pricelistrid,billpax," +
                                                          "totamount,totsertaxper,totsertaxamount,totvatper,totvatamount,totaddvatper,totaddvatamount," +
                                                          "totdiscper,totdiscamount,totroff,netamount,billprepby,billremark," +
                                                          "setletype,setleamount,chequeno,chequebankname,creditcardno,creditholdername,creditbankname," +
                                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                                          " REFBILLNO,REFBILLNUM,custadd,TOTDISCOUNTABLEAMOUNT,ISREVISEDBILL,REVISEDBILLSRNO,REVISEDBILLNO,BASEBILLNO,MAINBASEBILLNO,LASTTABLERID,LASTTABLESTATUS, " +
                                                          " BILLORDERPERID,BILLTIME,TOTCALCVATPER,TOTCALCVATAMOUNT,ISBILLTOCUSTOMER,CNTPRINT,TOKENNO,ISPARCELBILL,ISCOMPLYBILL,TOTBEVVATPER,TOTBEVVATAMT,TOTLIQVATPER,TOTLIQVATAMT," +
                                                          " REFBYRID,BILLINFO,TOTSERCHRPER,TOTSERCHRAMT,TOTNEWTOTALAMT,TOTADDDISCAMT,MSTTIEUPCOMPRID,COUPONNO,COUPONPERNAME,TOTKKCESSPER,TOTKKCESSAMT,OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1," +
                                                          " OTHERPAYMENTBYREMARK2,PAYMENT,MULTICASHAMT,MULTICHEQUEAMT,MULTICREDITCARDAMT,MULTIOTHERAMT,MULTICHQNO,MULTICHQBANKNAME,MULTICARDNO,MULTICARDBANKNAME,MULTIOTHERPAYMENTBY,MULTIOTHERREMARK1," +
                                                          " MULTIOTHERREMARK2,MULTITIPAMT,CARDNO,TOTBILLREWPOINT,TOTITEMREWPOINT,CGSTAMT,SGSTAMT,IGSTAMT,TOTGSTAMT,RECAMT,RETAMT," +
                                                          " SENDDATA )" +
                                          " SELECT RID,billno,billdate,billtype,custrid,custname,custcontno,tablerid,pricelistrid,billpax," +
                                                          "totamount,totsertaxper,totsertaxamount,totvatper,totvatamount,totaddvatper,totaddvatamount," +
                                                          "totdiscper,totdiscamount,totroff,netamount,billprepby,billremark," +
                                                          "setletype,setleamount,chequeno,chequebankname,creditcardno,creditholdername,creditbankname," +
                                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                                          " REFBILLNO,REFBILLNUM,custadd,TOTDISCOUNTABLEAMOUNT,ISREVISEDBILL,REVISEDBILLSRNO,REVISEDBILLNO,BASEBILLNO,MAINBASEBILLNO,LASTTABLERID,LASTTABLESTATUS, " +
                                                          " BILLORDERPERID,BILLTIME,TOTCALCVATPER,TOTCALCVATAMOUNT,ISBILLTOCUSTOMER,CNTPRINT,TOKENNO,ISPARCELBILL,ISCOMPLYBILL,TOTBEVVATPER,TOTBEVVATAMT,TOTLIQVATPER,TOTLIQVATAMT," +
                                                          " REFBYRID,BILLINFO,TOTSERCHRPER,TOTSERCHRAMT,TOTNEWTOTALAMT,TOTADDDISCAMT,MSTTIEUPCOMPRID,COUPONNO,COUPONPERNAME,TOTKKCESSPER,TOTKKCESSAMT,OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1," +
                                                          " OTHERPAYMENTBYREMARK2,PAYMENT,MULTICASHAMT,MULTICHEQUEAMT,MULTICREDITCARDAMT,MULTIOTHERAMT,MULTICHQNO,MULTICHQBANKNAME,MULTICARDNO,MULTICARDBANKNAME,MULTIOTHERPAYMENTBY,MULTIOTHERREMARK1," +
                                                          " MULTIOTHERREMARK2,MULTITIPAMT,CARDNO,TOTBILLREWPOINT,TOTITEMREWPOINT,CGSTAMT,SGSTAMT,IGSTAMT,TOTGSTAMT,RECAMT,RETAMT," +
                                                          " SENDDATA " +
                                          " FROM @TBLBILL" +
                                          " WHERE RID NOT IN(SELECT RID FROM BILL)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_BILLDTL");
                strtype = " CREATE PROCEDURE sp_SENDDATA_BILLDTL " +
                                 " @TBLBILLDTL BILLDTL_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE BILLDTL " +
                                    " SET BILLRID=c2.BILLRID,IRID=c2.IRID,KOTRID=c2.KOTRID,IQTY=c2.IQTY,IRATE=c2.IRATE,IPAMT=c2.IPAMT,IAMT=c2.IAMT, " +
                                                          " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG," +
                                                          " NOTAPPLYDISC=c2.NOTAPPLYDISC,NOTAPPLYSERTAX=c2.NOTAPPLYSERTAX,SERTAXTYPE=c2.SERTAXTYPE,NOTAPPLYVAT=c2.NOTAPPLYVAT,DISCPER=c2.DISCPER,DISCAMT=C2.DISCAMT,SERTAXPER=C2.SERTAXPER,SERTAXAMT=C2.SERTAXAMT, " +
                                                          " FOODVATPER=C2.FOODVATPER,FOODVATAMT=C2.FOODVATAMT,LIQVATPER=C2.LIQVATPER,LIQVATAMT=C2.LIQVATAMT,BEVVATPER=C2.BEVVATPER,BEVVATAMT=C2.BEVVATAMT,SERCHRPER=C2.SERCHRPER,SERCHRAMT=C2.SERCHRAMT,NEWSERCHRPER=C2.NEWSERCHRPER," +
                                                          " NEWSERCHRAMT=C2.NEWSERCHRAMT,KKCESSPER=C2.KKCESSPER,KKCESSAMT=C2.KKCESSAMT,IREWPOINTS=C2.IREWPOINTS,NOTAPPLYGST=C2.NOTAPPLYGST,CGSTPER=C2.CGSTPER,CGSTAMT=C2.CGSTAMT,SGSTPER=C2.SGSTPER,SGSTAMT=C2.SGSTAMT,IGSTPER=C2.IGSTPER,IGSTAMT=C2.IGSTAMT," +
                                                          " GSTPER=C2.GSTPER,GSTAMT=C2.GSTAMT," +
                                                          " SENDDATA=c2.SENDDATA" +
                                                          " FROM BILLDTL c1 " +
                                                          " INNER JOIN @TBLBILLDTL c2" +
                                                          " ON c1.RID = c2.RID" +
                                                          " INSERT INTO BILLDTL (RID,BILLRID,IRID,KOTRID,IQTY,IRATE,IPAMT,IAMT," +
                                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                                          " NOTAPPLYDISC,NOTAPPLYSERTAX,SERTAXTYPE,NOTAPPLYVAT,DISCPER,DISCAMT,SERTAXPER,SERTAXAMT,FOODVATPER,FOODVATAMT,LIQVATPER,LIQVATAMT,BEVVATPER," +
                                                          " BEVVATAMT,SERCHRPER,SERCHRAMT,NEWSERCHRPER,NEWSERCHRAMT,KKCESSPER,KKCESSAMT,IREWPOINTS,NOTAPPLYGST,CGSTPER,CGSTAMT,SGSTPER,SGSTAMT,IGSTPER,IGSTAMT,GSTPER,GSTAMT, " +
                                                          " SENDDATA )" +
                                                          " SELECT RID,BILLRID,IRID,KOTRID,IQTY,IRATE,IPAMT,IAMT," +
                                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG," +
                                                          " NOTAPPLYDISC,NOTAPPLYSERTAX,SERTAXTYPE,NOTAPPLYVAT,DISCPER,DISCAMT,SERTAXPER,SERTAXAMT,FOODVATPER,FOODVATAMT,LIQVATPER,LIQVATAMT,BEVVATPER," +
                                                          " BEVVATAMT,SERCHRPER,SERCHRAMT,NEWSERCHRPER,NEWSERCHRAMT,KKCESSPER,KKCESSAMT,IREWPOINTS,NOTAPPLYGST,CGSTPER,CGSTAMT,SGSTPER,SGSTAMT,IGSTPER,IGSTAMT,GSTPER,GSTAMT, " +
                                                          " SENDDATA " +
                                          " FROM @TBLBILLDTL" +
                                          " WHERE RID NOT IN(SELECT RID FROM BILLDTL)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_SETTLEMENT");
                strtype = " CREATE PROCEDURE sp_SENDDATA_SETTLEMENT " +
                                 " @TBLSETTLEMENT SETTLEMENT_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE SETTLEMENT " +
                                    " SET SETLEDATE=c2.SETLEDATE, " +
                                    " BILLRID = c2.BILLRID, SETLENO=c2.SETLENO,SETLETYPE=c2.SETLETYPE,SETLEAMOUNT=c2.SETLEAMOUNT,CHEQUENO=c2.CHEQUENO,CHEQUEBANKNAME=c2.CHEQUEBANKNAME,CREDITCARDNO=c2.CREDITCARDNO,CREDITHOLDERNAME=c2.CREDITHOLDERNAME," +
                                    " CREDITBANKNAME=c2.CREDITBANKNAME,SETLEPREPBY=c2.SETLEPREPBY,SETLEREMARK=c2.SETLEREMARK," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA," +
                                    " CUSTRID=c2.CUSTRID,ADJAMT=c2.ADJAMT,TIPAMT=c2.TIPAMT,OTHERPAYMENTBY=c2.OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1=c2.OTHERPAYMENTBYREMARK1,OTHERPAYMENTBYREMARK2=c2.OTHERPAYMENTBYREMARK2 " +
                                    " FROM SETTLEMENT c1 " +
                                          " INNER JOIN @TBLSETTLEMENT c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO SETTLEMENT ( RID,SETLEDATE,BILLRID,SETLENO,SETLETYPE,SETLEAMOUNT,CHEQUENO,CHEQUEBANKNAME,CREDITCARDNO,CREDITHOLDERNAME,CREDITBANKNAME,SETLEPREPBY,SETLEREMARK," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                          " CUSTRID,ADJAMT,TIPAMT,OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1,OTHERPAYMENTBYREMARK2, " +
                                          " SENDDATA )" +
                                          " SELECT RID,SETLEDATE,BILLRID,SETLENO,SETLETYPE,SETLEAMOUNT,CHEQUENO,CHEQUEBANKNAME,CREDITCARDNO,CREDITHOLDERNAME,CREDITBANKNAME,SETLEPREPBY,SETLEREMARK," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                          " CUSTRID,ADJAMT,TIPAMT,OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1,OTHERPAYMENTBYREMARK2, " +
                                          " SENDDATA " +
                                          " FROM @TBLSETTLEMENT" +
                                          " WHERE RID NOT IN(SELECT RID FROM SETTLEMENT)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTCUST");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTCUST " +
                                 " @TBLMSTCUST MSTCUST_TYPE READONLY" +
                                   " AS " +
                                    " BEGIN " +
                                    " UPDATE MSTCUST " +
                                    " SET custcode=c2.custcode, " +
                                    " custname = c2.custname, custadd1=c2.custadd1,custadd2=c2.custadd2,custadd3=c2.custadd3,custcityid=c2.custcityid,custstateid=c2.custstateid,custcountryid=c2.custcountryid,custpin=c2.custpin," +
                                    " custtelno=c2.custtelno,custmobno=c2.custmobno,custemail=c2.custemail,custfaxno=c2.custfaxno,custbirthdate=c2.custbirthdate,custgender=c2.custgender,custmaritalstatus=c2.custmaritalstatus,custannidate=c2.custannidate," +
                                    " custimage=c2.custimage,custregdate=c2.custregdate,custdesc=c2.custdesc,custmobno2=c2.custmobno2,custmobno3=c2.custmobno3,custmobno4=c2.custmobno4,custmobno5=c2.custmobno5," +
                                    " CUSTAREA=c2.CUSTAREA,CUSTADD2ADD1=c2.CUSTADD2ADD1,CUSTADD2ADD2=c2.CUSTADD2ADD2,CUSTADD2ADD3=c2.CUSTADD2ADD3,CUSTADD2AREA=c2.CUSTADD2AREA,CUSTADD2CITY=c2.CUSTADD2CITY,CUSTADD2PIN=c2.CUSTADD2PIN," +
                                    " CUSTADD3ADD1=c2.CUSTADD3ADD1,CUSTADD3ADD2=c2.CUSTADD3ADD2,CUSTADD3ADD3=c2.CUSTADD3ADD3,CUSTADD3AREA=c2.CUSTADD3AREA,CUSTADD3CITY=c2.CUSTADD3CITY,CUSTADD3PIN=c2.CUSTADD3PIN," +
                                    " CUSTADD4ADD1=c2.CUSTADD4ADD1,CUSTADD4ADD2=c2.CUSTADD4ADD2,CUSTADD4ADD3=c2.CUSTADD4ADD3,CUSTADD4AREA=c2.CUSTADD4AREA,CUSTADD4CITY=c2.CUSTADD4CITY,CUSTADD4PIN=c2.CUSTADD4PIN," +
                                    " CUSTADD5ADD1=c2.CUSTADD5ADD1,CUSTADD5ADD2=c2.CUSTADD5ADD2,CUSTADD5ADD3=c2.CUSTADD5ADD3,CUSTADD5AREA=c2.CUSTADD5AREA,CUSTADD5CITY=c2.CUSTADD5CITY,CUSTADD5PIN=c2.CUSTADD5PIN," +
                                    " CUSTDISCPER=c2.CUSTDISCPER,CUSTLANDMARK=c2.CUSTLANDMARK,CUSTDOCIMAGE=c2.CUSTDOCIMAGE,FOODTOKEN=c2.FOODTOKEN," +
                                    " CARDNO=c2.CARDNO,CARDACTDATE=c2.CARDACTDATE,CARDENROLLFEES=c2.CARDENROLLFEES,CARDSTATUS=c2.CARDSTATUS,CARDEXPDATE=c2.CARDEXPDATE,CARDREMARK=c2.CARDREMARK,gstno=c2.gstno,panno=c2.panno,applyigst=c2.applyigst,vatno=c2.vatno,TIEUPRID=c2.TIEUPRID," +
                                    " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                    " FROM MSTCUST c1 " +
                                          " INNER JOIN @TBLMSTCUST c2" +
                                          " ON c1.RID = c2.RID" +
                                          " INSERT INTO MSTCUST ( rid,custcode,custname,custadd1,custadd2,custadd3,custcityid,custstateid,custcountryid,  " +
                                                " custpin,custtelno,custmobno,custemail,custfaxno,custbirthdate,custgender,custmaritalstatus,custannidate, " +
                                                " custimage,custregdate,custdesc, " +
                                                " custmobno2,custmobno3,custmobno4,custmobno5," +
                                                " CUSTAREA,CUSTADD2ADD1,CUSTADD2ADD2,CUSTADD2ADD3,CUSTADD2AREA,CUSTADD2CITY,CUSTADD2PIN," +
                                                " CUSTADD3ADD1,CUSTADD3ADD2,CUSTADD3ADD3,CUSTADD3AREA,CUSTADD3CITY,CUSTADD3PIN," +
                                                " CUSTADD4ADD1,CUSTADD4ADD2,CUSTADD4ADD3,CUSTADD4AREA,CUSTADD4CITY,CUSTADD4PIN," +
                                                " CUSTADD5ADD1,CUSTADD5ADD2,CUSTADD5ADD3,CUSTADD5AREA,CUSTADD5CITY,CUSTADD5PIN," +
                                                " CUSTDISCPER,CUSTLANDMARK,CUSTDOCIMAGE,FOODTOKEN," +
                                                " CARDNO,CARDACTDATE,CARDENROLLFEES,CARDSTATUS,CARDEXPDATE,CARDREMARK,gstno,panno,applyigst,vatno,TIEUPRID," +
                                                " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                " SENDDATA )" +
                                          " SELECT rid,custcode,custname,custadd1,custadd2,custadd3,custcityid,custstateid,custcountryid,  " +
                                                " custpin,custtelno,custmobno,custemail,custfaxno,custbirthdate,custgender,custmaritalstatus,custannidate, " +
                                                " custimage,custregdate,custdesc, " +
                                                " custmobno2,custmobno3,custmobno4,custmobno5," +
                                                " CUSTAREA,CUSTADD2ADD1,CUSTADD2ADD2,CUSTADD2ADD3,CUSTADD2AREA,CUSTADD2CITY,CUSTADD2PIN," +
                                                " CUSTADD3ADD1,CUSTADD3ADD2,CUSTADD3ADD3,CUSTADD3AREA,CUSTADD3CITY,CUSTADD3PIN," +
                                                " CUSTADD4ADD1,CUSTADD4ADD2,CUSTADD4ADD3,CUSTADD4AREA,CUSTADD4CITY,CUSTADD4PIN," +
                                                " CUSTADD5ADD1,CUSTADD5ADD2,CUSTADD5ADD3,CUSTADD5AREA,CUSTADD5CITY,CUSTADD5PIN," +
                                                " CUSTDISCPER,CUSTLANDMARK,CUSTDOCIMAGE,FOODTOKEN," +
                                                " CARDNO,CARDACTDATE,CARDENROLLFEES,CARDSTATUS,CARDEXPDATE,CARDREMARK,gstno,panno,applyigst,vatno,TIEUPRID," +
                                          " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                          " SENDDATA " +
                                          " FROM @TBLMSTCUST" +
                                          " WHERE RID NOT IN(SELECT RID FROM MSTCUST)" +
                                    " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_CASHONHAND");
                strtype = " CREATE PROCEDURE sp_SENDDATA_CASHONHAND " +
                                  " @TBLCASHONHAND CASHONHAND_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE CASHONHAND " +
                                     " SET CASHDATE=c2.CASHDATE, " +
                                     " CASHAMT = c2.CASHAMT, CASHSTATUS=c2.CASHSTATUS,CASHPERSONNAME=c2.CASHPERSONNAME,CASHREMARK=c2.CASHREMARK,CASHDESC=c2.CASHDESC, " +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA," +
                                     " EMPRID=c2.EMPRID,CASHNO=c2.CASHNO " +
                                     " FROM CASHONHAND c1 " +
                                           " INNER JOIN @TBLCASHONHAND c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO CASHONHAND ( RID,CASHDATE,CASHAMT,CASHSTATUS,CASHPERSONNAME,CASHREMARK,CASHDESC, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " EMPRID,CASHNO, " +
                                           " SENDDATA )" +
                                           " SELECT RID,CASHDATE,CASHAMT,CASHSTATUS,CASHPERSONNAME,CASHREMARK,CASHDESC, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " EMPRID,CASHNO, " +
                                           " SENDDATA " +
                                           " FROM @TBLCASHONHAND" +
                                           " WHERE RID NOT IN(SELECT RID FROM CASHONHAND)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTEXPENCES");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTEXPENCES " +
                                  " @TBLMSTEXPENCE MSTEXPENCE_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE MSTEXPENCES " +
                                     " SET EXCODE=c2.EXCODE,EXNAME=C2.EXNAME,EXREMARK=C2.EXREMARK,EXDESC=C2.EXDESC,isopecost=c2.isopecost,isfuelcost=c2.isfuelcost, " +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM MSTEXPENCES c1 " +
                                           " INNER JOIN @TBLMSTEXPENCE c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO MSTEXPENCES ( RID,EXCODE,EXNAME,EXREMARK,EXDESC,isopecost,isfuelcost, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " SENDDATA )" +
                                           " SELECT RID,EXCODE,EXNAME,EXREMARK,EXDESC,isopecost,isfuelcost, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " SENDDATA " +
                                           " FROM @TBLMSTEXPENCE" +
                                           " WHERE RID NOT IN(SELECT RID FROM MSTEXPENCES)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTINCOME");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTINCOME " +
                                  " @TBLMSTINCOME MSTINCOME_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE MSTINCOME " +
                                     " SET INCODE=c2.INCODE,INNAME=C2.INNAME,INREMARK=C2.INREMARK,INDESC=C2.INDESC, " +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM MSTINCOME c1 " +
                                           " INNER JOIN @TBLMSTINCOME c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO MSTINCOME ( RID,INCODE,INNAME,INREMARK,INDESC, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " SENDDATA )" +
                                           " SELECT RID,INCODE,INNAME,INREMARK,INDESC, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " SENDDATA " +
                                           " FROM @TBLMSTINCOME" +
                                           " WHERE RID NOT IN(SELECT RID FROM MSTINCOME)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTTIEUPCOMPANY");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTTIEUPCOMPANY " +
                                  " @TBLMSTTIEUPCOMPANY MSTTIEUPCOMPANY_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE MSTTIEUPCOMPANY " +
                                     " SET COMPCODE=c2.COMPCODE,COMPNAME=C2.COMPNAME,CONTPER=C2.CONTPER,CONTNO=C2.CONTNO,COMPDISC=c2.COMPDISC,COMPREMARK=c2.COMPREMARK,PAYMENTBY=c2.PAYMENTBY, " +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM MSTTIEUPCOMPANY c1 " +
                                           " INNER JOIN @TBLMSTTIEUPCOMPANY c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO MSTTIEUPCOMPANY ( RID,COMPCODE,COMPNAME,CONTPER,CONTNO,COMPDISC,COMPREMARK,PAYMENTBY, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " SENDDATA )" +
                                           " SELECT RID,COMPCODE,COMPNAME,CONTPER,CONTNO,COMPDISC,COMPREMARK,PAYMENTBY, " +
                                           " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                           " SENDDATA " +
                                           " FROM @TBLMSTTIEUPCOMPANY" +
                                           " WHERE RID NOT IN(SELECT RID FROM MSTTIEUPCOMPANY)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_MSTSUPPLIER");
                strtype = " CREATE PROCEDURE sp_SENDDATA_MSTSUPPLIER " +
                                  " @TBLMSTSUPPLIER MSTSUPPLIER_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE MSTSUPPLIER " +
                                     " SET SUPPCODE=c2.SUPPCODE,SUPPNAME=C2.SUPPNAME,SUPPADD1=C2.SUPPADD1,SUPPADD2=C2.SUPPADD2,SUPPADD3=c2.SUPPADD3,SUPPCITYID=c2.SUPPCITYID,SUPPSTATEID=c2.SUPPSTATEID, " +
                                     " SUPPCOUNTRYID=c2.SUPPCOUNTRYID,SUPPPIN=c2.SUPPPIN,SUPPTELNO=c2.SUPPTELNO,SUPPMOBNO=c2.SUPPMOBNO,SUPPFAXNO=c2.SUPPFAXNO,SUPPCONTPERNAME1=c2.SUPPCONTPERNAME1,SUPPCONTPERNAME2=c2.SUPPCONTPERNAME2," +
                                     " SUPPEMAIL=c2.SUPPEMAIL,SUPPPANNO=c2.SUPPPANNO,SUPPTINNO=c2.SUPPTINNO,SUPPCSTNO=c2.SUPPCSTNO,SUPPGSTNO=c2.SUPPGSTNO,SUPPREMARK=c2.SUPPREMARK,SUPPIMAGE=c2.SUPPIMAGE,SUPPTYPE=c2.SUPPTYPE," +
                                     " SUPPPAYMENT=c2.SUPPPAYMENT,ITEMTYPE=c2.ITEMTYPE," +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM MSTSUPPLIER c1 " +
                                           " INNER JOIN @TBLMSTSUPPLIER c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO MSTSUPPLIER (RID,SUPPCODE,SUPPNAME,SUPPADD1,SUPPADD2,SUPPADD3,SUPPCITYID,SUPPSTATEID,SUPPCOUNTRYID,SUPPPIN,SUPPTELNO,SUPPMOBNO,SUPPFAXNO,SUPPCONTPERNAME1,SUPPCONTPERNAME2,SUPPEMAIL,SUPPPANNO, " +
                                                                    " SUPPTINNO,SUPPCSTNO,SUPPGSTNO,SUPPREMARK,SUPPIMAGE,SUPPTYPE,SUPPPAYMENT,ITEMTYPE," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA )" +
                                           " SELECT RID,SUPPCODE,SUPPNAME,SUPPADD1,SUPPADD2,SUPPADD3,SUPPCITYID,SUPPSTATEID,SUPPCOUNTRYID,SUPPPIN,SUPPTELNO,SUPPMOBNO,SUPPFAXNO,SUPPCONTPERNAME1,SUPPCONTPERNAME2,SUPPEMAIL,SUPPPANNO, " +
                                                                    " SUPPTINNO,SUPPCSTNO,SUPPGSTNO,SUPPREMARK,SUPPIMAGE,SUPPTYPE,SUPPPAYMENT,ITEMTYPE," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA " +
                                           " FROM @TBLMSTSUPPLIER" +
                                           " WHERE RID NOT IN(SELECT RID FROM MSTSUPPLIER)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_TABLERESERVATION");
                strtype = " CREATE PROCEDURE sp_SENDDATA_TABLERESERVATION " +
                                  " @TBLTABLERESERVATION TABLERESERVATION_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE TABLERESERVATION " +
                                     " SET REVNO=c2.REVNO,BODATE=C2.BODATE,REVDATE=C2.REVDATE,REVTIME=C2.REVTIME,CUSTRID=c2.CUSTRID,TABLERID=C2.TABLERID,PAX=C2.PAX,FUNCNAME=C2.FUNCNAME,SPREQ=C2.SPREQ,REVDESC=C2.REVDESC,ENTRYBY=C2.ENTRYBY," +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM TABLERESERVATION c1 " +
                                           " INNER JOIN @TBLTABLERESERVATION c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO TABLERESERVATION (RID,REVNO,BODATE,REVDATE,REVTIME,CUSTRID,TABLERID,PAX,FUNCNAME,SPREQ,REVDESC,ENTRYBY," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA )" +
                                           " SELECT RID,REVNO,BODATE,REVDATE,REVTIME,CUSTRID,TABLERID,PAX,FUNCNAME,SPREQ,REVDESC,ENTRYBY," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA " +
                                           " FROM @TBLTABLERESERVATION" +
                                           " WHERE RID NOT IN(SELECT RID FROM TABLERESERVATION)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_INCOME");
                strtype = " CREATE PROCEDURE sp_SENDDATA_INCOME" +
                                  " @TBLINCOME INCOME_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE INCOME " +
                                     " SET INCOMENO=c2.INCOMENO,INDATE=C2.INDATE,INTIME=C2.INTIME,INRID=C2.INRID,INTYPE=c2.INTYPE,INAMOUNT=C2.INAMOUNT,INPERNAME=C2.INPERNAME,INCONTNO=C2.INCONTNO,REMARK1=C2.REMARK1,REMARK2=C2.REMARK2,REMARK3=C2.REMARK3,INDESC=C2.INDESC," +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM INCOME c1 " +
                                           " INNER JOIN @TBLINCOME c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO INCOME (RID,INCOMENO,INDATE,INTIME,INRID,INTYPE,INAMOUNT,INPERNAME,INCONTNO,REMARK1,REMARK2,REMARK3,INDESC," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA )" +
                                           " SELECT RID,INCOMENO,INDATE,INTIME,INRID,INTYPE,INAMOUNT,INPERNAME,INCONTNO,REMARK1,REMARK2,REMARK3,INDESC," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA " +
                                           " FROM @TBLINCOME" +
                                           " WHERE RID NOT IN(SELECT RID FROM INCOME)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_EXPENCE");
                strtype = " CREATE PROCEDURE sp_SENDDATA_EXPENCE" +
                                  " @TBLEXPENCE EXPENCE_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE EXPENCE " +
                                     " SET EXPENCENO=c2.EXPENCENO,EXDATE=C2.EXDATE,EXTIME=C2.EXTIME,EXRID=C2.EXRID,EXTYPE=c2.EXTYPE,EXAMOUNT=C2.EXAMOUNT,EXPERNAME=C2.EXPERNAME,EXCONTNO=C2.EXCONTNO,REMARK1=C2.REMARK1,REMARK2=C2.REMARK2,REMARK3=C2.REMARK3," +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM EXPENCE c1 " +
                                           " INNER JOIN @TBLEXPENCE c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO EXPENCE (RID,EXPENCENO,EXDATE,EXTIME,EXRID,EXTYPE,EXAMOUNT,EXPERNAME,EXCONTNO,REMARK1,REMARK2,REMARK3,EXDESC," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA )" +
                                           " SELECT RID,EXPENCENO,EXDATE,EXTIME,EXRID,EXTYPE,EXAMOUNT,EXPERNAME,EXCONTNO,REMARK1,REMARK2,REMARK3,EXDESC," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA " +
                                           " FROM @TBLEXPENCE" +
                                           " WHERE RID NOT IN(SELECT RID FROM EXPENCE)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("sp_SENDDATA_OPCASH");
                strtype = " CREATE PROCEDURE sp_SENDDATA_OPCASH" +
                                  " @TBLOPCASH OPCASH_TYPE READONLY" +
                                    " AS " +
                                     " BEGIN " +
                                     " UPDATE OPCASH " +
                                     " SET OPCASHDATE=c2.OPCASHDATE,OPAMT=C2.OPAMT,OPREMARK=C2.OPREMARK,OPENTRYBY=C2.OPENTRYBY, " +
                                     " AUSERID=c2.AUSERID,ADATETIME=c2.ADATETIME,EUSERID=c2.EUSERID,EDATETIME=c2.EDATETIME,DUSERID=c2.DUSERID,DDATETIME=c2.DDATETIME,DELFLG=c2.DELFLG,SENDDATA=c2.SENDDATA " +
                                     " FROM OPCASH c1 " +
                                           " INNER JOIN @TBLOPCASH c2" +
                                           " ON c1.RID = c2.RID" +
                                           " INSERT INTO OPCASH (RID,OPCASHDATE,OPAMT,OPREMARK,OPENTRYBY, " +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA )" +
                                           " SELECT RID,OPCASHDATE,OPAMT,OPREMARK,OPENTRYBY," +
                                                                    " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                                    " SENDDATA " +
                                           " FROM @TBLOPCASH" +
                                           " WHERE RID NOT IN(SELECT RID FROM OPCASH)" +
                                     " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                this.DeleteProcedureFromOnlineDb("SP_FEEDBACKDATA");
                strtype = " CREATE PROCEDURE SP_FEEDBACKDATA " +
                            " ( " +
                            " @p_mode as int, " +
                            " @p_rid bigint, " +
                            " @p_feeddate datetime," +
                            " @p_guestname nvarchar(100), " +
                            " @p_mobno nvarchar(50), " +
                            " @p_email nvarchar(200), " +
                            " @p_optfood int, " +
                            " @p_optservice int, " +
                            " @p_optatmo int, " +
                            " @p_optover int, " +
                            " @p_remark nvarchar(max)," +
                            " @p_osnm NVARCHAR(200), " +
                            " @p_bronm NVARCHAR(200)," +
                            " @p_devicenm NVARCHAR(200), " +
                            " @p_userid bigint,   " +
                            " @p_errstr as nvarchar(max) out,  " +
                            " @p_retval as int out,  " +
                            " @p_id as bigint out  " +
                            " ) as  " +
                            " Begin " +
                            " try " +
                            " begin " +
                              " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                            " if (@p_mode=0)  " +
                            " begin " +
                                    " insert into FEEDBACKDATA (feeddate,guestname,mobno,email,optfood,optservice,optatmo,optover,remark,osnm,bronm,devicenm, " +
                                                                " auserid,adatetime,DelFlg) " +
                                                     " values (@p_feeddate,@p_guestname,@p_mobno,@p_email,@p_optfood,@p_optservice,@p_optatmo,@p_optover,@p_remark,@p_osnm,@p_bronm,@p_devicenm," +
                                                                " @p_userid,getdate(),0) " +
                                " set @p_id=SCOPE_IDENTITY() " +
                                " End " +
                            " else if (@p_mode=1)  " +
                                " begin " +
                            " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                    " update FEEDBACKDATA set feeddate=@p_feeddate,guestname=@p_guestname,mobno=@p_mobno,email=@p_email,optfood=@p_optfood,optservice=@p_optservice,optatmo=@p_optatmo,optover=@p_optover," +
                                    " remark=@p_remark,osnm=@p_osnm,bronm=@p_bronm,devicenm=@p_devicenm, " +
                                                " euserid = @p_userid,edatetime = getdate() " +
                                    " where rid = @p_rid  " +
                                " End  " +
                            " End  " +
                            " end  " +
                            " try   " +
                                " begin catch " +
                                " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage " +
                                 " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                " Return   " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strtype);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region CREATE VIEW

        public bool Create_OnlineDb_VIEW()
        {
            string str1;
            try
            {
                str1 = "DROP VIEW MSTUNITLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTUNITLIST AS SELECT RID, UNITCODE,UNITNAME FROM MSTUNIT WHERE ISNULL(DELFLG, 0) = 0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTDEPTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTDEPTLIST AS SELECT RID, DEPTCODE,DEPTNAME FROM MSTDEPT WHERE ISNULL(DELFLG, 0) = 0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW ITEMPRICELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "   CREATE VIEW ITEMPRICELIST AS SELECT RID, IPLCODE, IPLNAME FROM MSTITEMPRICELIST WHERE (ISNULL(DELFLG, 0) <> 1) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTUSERSLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view MSTUSERSLIST AS select rid,usercode,username from mstusers where isnull(delflg,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTCUSTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view MSTCUSTLIST AS  " +
                    " SELECT  MSTCUST.RID, MSTCUST.CUSTCODE, MSTCUST.CUSTNAME,MSTCUST.CARDNO,MSTCUST.CUSTREGDATE, MSTCUST.CUSTMOBNO, MSTCUST.CUSTTELNO,MSTCUST.CUSTBIRTHDATE,MSTCUST.CUSTANNIDATE," +
                    " MSTCUST.CUSTGENDER, MSTCUST.CUSTMARITALSTATUS, MSTCUST.CUSTADD1, MSTCITY.CITYNAME, MSTSTATE.STATENAME, MSTCOUNTRY.COUNTRYNAME, " +
                   " MSTCUST.CUSTEMAIL " +
                    " FROM   MSTCUST  " +
                    " LEFT JOIN MSTCITY ON (MSTCITY.CITYID = MSTCUST.CUSTCITYID) " +
                    " LEFT JOIN MSTCOUNTRY ON (MSTCOUNTRY.COUNTRYID = MSTCUST.CUSTCOUNTRYID) " +
                    " LEFT JOIN MSTSTATE ON (MSTSTATE.STATEID = MSTCUST.CUSTSTATEID) " +
                    " WHERE (ISNULL(MSTCUST.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTTABLELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTTABLELIST AS " +
                        " SELECT  RID, TABLECODE, TABLENAME,GSTTAXTYPE,SECNO,TABLEGROUP,TABDISC,ISPARCELTABLE,ISROOMTABLE,ROOMNO,ISHIDETABLE,TABDISPORD " +
                            " FROM MSTTABLE " +
                            " WHERE (ISNULL(MSTTABLE.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view pendingbillinfo";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view pendingbillinfo AS " +
                        "SELECT     BILL.RID, BILL.CUSTRID,BILL.BILLNO, BILL.REFBILLNO,BILL.BILLDATE,BILL.NETAMOUNT, (ISNULL(BILL.NETAMOUNT, 0) - ISNULL(setleinfo.setleamount, 0)) AS PENDINGAMT " +
                           " FROM  BILL LEFT OUTER JOIN " +
                        " (SELECT     (SUM(ISNULL(SETLEAMOUNT,0)) + SUM(ISNULL(ADJAMT,0)))  AS setleamount, BILLRID " +
                        "   FROM       SETTLEMENT " +
                        "   WHERE      (ISNULL(DELFLG, 0) = 0) " +
                        "   GROUP BY BILLRID) AS setleinfo ON setleinfo.BILLRID = BILL.RID " +
                       " WHERE  (ISNULL(BILL.DELFLG, 0) = 0) AND (ISNULL(BILL.ISREVISEDBILL, 0) = 0) AND (ISNULL(BILL.NETAMOUNT, 0) - ISNULL(setleinfo.setleamount, 0) > 0) " +
                       " UNION " +
                       " SELECT     0 AS rid, 0 AS CUSTRID,'' AS billno,'' AS REFBILLNO,'' as BILLDATE, 0 AS NETAMOUNT, 0 AS PENDINGAMT " +
                       " FROM   dbo.MSTUSERS ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view KOTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW KOTLIST AS SELECT KOT.RID,KOT.KOTDATE,convert(char(5), KOT.KOTTIME, 108) as KOTTIME," +
                        " KOT.KOTNO, KOT.REFKOTNO, KOT.KOTTOKNO, KOT.KOTTABLENAME, MSTEMPLIST.EMPNAME" +
                        " FROM KOT " +
                        " LEFT JOIN MSTEMPLIST ON (MSTEMPLIST.RID = KOT.KOTORDERPERID)" +
                        " WHERE (ISNULL(KOT.DELFLG, 0) = 0) AND (ISNULL(KOT.ISCOMPKOT, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view KOTCOMPLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW KOTCOMPLIST AS SELECT KOT.RID,KOT.KOTDATE,convert(char(5), KOT.KOTTIME, 108) as KOTTIME," +
                        " KOT.KOTNO, KOT.REFKOTNO, KOT.KOTTOKNO, KOT.KOTTABLENAME, MSTEMPLIST.EMPNAME" +
                        " FROM KOT " +
                        " LEFT JOIN MSTEMPLIST ON (MSTEMPLIST.RID = KOT.KOTORDERPERID)" +
                        " WHERE (ISNULL(KOT.DELFLG, 0) = 0) AND (ISNULL(KOT.ISCOMPKOT, 0) = 1)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTREMARKLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view MSTREMARKLIST AS SELECT RID, REMARKCODE, REMARKNAME FROM MSTREMARK WHERE (ISNULL(DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTFEEDBACKLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTFEEDBACKLIST AS " +
                        " SELECT MSTFEEDBACK.RID,MSTFEEDBACK.FEEDNO,MSTFEEDBACK.FEEDDATE,MSTCUSTLIST.CUSTNAME,MSTFEEDBACK.CUSTCONTNO, " +
                        " MSTFEEDBACK.FOODFLAVOUR,MSTFEEDBACK.FOODPRESENTATION,MSTFEEDBACK.FOODVALUEFORMONEY,MSTFEEDBACK.FOODFRESHNESS,MSTFEEDBACK.FOODCHOICEOFMENU,MSTFEEDBACK.SERVICEFRIENDLY,MSTFEEDBACK.SERVICEPROFESSIONAL, " +
                        " MSTFEEDBACK.SERVICEEXPLATION,MSTFEEDBACK.SERVICETIMETAKEN,MSTFEEDBACK.SERVICEACCOUNT,MSTFEEDBACK.VENUEATMOSPHERE,MSTFEEDBACK.VENUECLEANLINESS,MSTFEEDBACK.VENUESTAFF,MSTFEEDBACK.GENERALFOOD,MSTFEEDBACK.GENERALOVERALLSERVICE, " +
                        " MSTFEEDBACK.GENERALCLEANLINESS,MSTFEEDBACK.GENERALORDER,MSTFEEDBACK.GENERALSPEED,MSTFEEDBACK.GENERALVALUE,MSTFEEDBACK.GENERALOVREXP,MSTFEEDBACK.GENERALOVRRATING,MSTFEEDBACK.WDHEARABOUTUS " +
                        " FROM MSTFEEDBACK " +
                        " LEFT JOIN MSTCUSTLIST ON (MSTCUSTLIST.RID=MSTFEEDBACK.CUSTRID)" +
                        " WHERE ISNULL(MSTFEEDBACK.DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view BIRTHDATESMSALERTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view BIRTHDATESMSALERTLIST as  " +
                        " SELECT RID,'Customer' as FLAG, CUSTBIRTHDATE AS DOB,custname AS ENAME,custmobno AS MOBNO,CUSTEMAIL as EMAIL " +
                        " FROM mstcust " +
                        " WHERE DATEPART(d, CUSTBIRTHDATE) = DATEPART(d, GETDATE()) " +
                        " AND DATEPART(m, CUSTBIRTHDATE) = DATEPART(m, GETDATE()) " +
                        " and  isnull(delflg,0)=0 " +
                        " union " +
                        " SELECT RID,'Employee' as FLAG, EMPBIRTHDATE AS DOB,EMPNAME as ENAME,EMPMOBILENO AS MOBNO,EMPEMAIL as EMAIL " +
                        " FROM mstemp " +
                        " WHERE DATEPART(d, EMPBIRTHDATE) = DATEPART(d, GETDATE()) " +
                        " AND DATEPART(m, EMPBIRTHDATE) = DATEPART(m, GETDATE()) " +
                        " and isnull(delflg,0)=0  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view ANNIDATESMSALERTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view ANNIDATESMSALERTLIST as " +
                       " SELECT RID,'Customer' as FLAG,CUSTANNIDATE AS DOB,custname AS ENAME,custmobno AS MOBNO,CUSTEMAIL as EMAIL " +
                       " FROM mstcust " +
                       " WHERE DATEPART(d, CUSTANNIDATE) = DATEPART(d, GETDATE()) " +
                       " AND DATEPART(m, CUSTANNIDATE) = DATEPART(m, GETDATE()) " +
                       " and  isnull(delflg,0)=0 " +
                       " union " +
                       " SELECT RID,'Employee' as FLAG,EMPANNIDATE AS DOB,EMPNAME as ENAME,EMPMOBILENO AS MOBNO,EMPEMAIL as EMAIL " +
                       " FROM mstemp " +
                       " WHERE DATEPART(d, EMPANNIDATE) = DATEPART(d, GETDATE()) " +
                       " AND DATEPART(m, EMPANNIDATE) = DATEPART(m, GETDATE()) " +
                       " and isnull(delflg,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view BIRTHDATESMSALERTMONTHLYLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view BIRTHDATESMSALERTMONTHLYLIST as  " +
                        " SELECT RID,'Customer' as FLAG, CUSTBIRTHDATE AS DOB,custname AS ENAME,custmobno AS MOBNO,CUSTEMAIL as EMAIL " +
                        " FROM mstcust " +
                        " WHERE " +
                        " DATEPART(m, CUSTBIRTHDATE) = DATEPART(m, GETDATE()) " +
                        " and  isnull(delflg,0)=0 " +
                        " union " +
                        " SELECT RID,'Employee' as FLAG, EMPBIRTHDATE AS DOB,EMPNAME as ENAME,EMPMOBILENO AS MOBNO,EMPEMAIL as EMAIL " +
                        " FROM mstemp " +
                        " WHERE " +
                        " DATEPART(m, EMPBIRTHDATE) = DATEPART(m, GETDATE()) " +
                        " and isnull(delflg,0)=0  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view ANNIDATESMSALERTMONTHLYLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view ANNIDATESMSALERTMONTHLYLIST as " +
                       " SELECT RID,'Customer' as FLAG,CUSTANNIDATE AS DOB,custname AS ENAME,custmobno AS MOBNO,CUSTEMAIL as EMAIL " +
                       " FROM mstcust " +
                       " WHERE " +
                       " DATEPART(m, CUSTANNIDATE) = DATEPART(m, GETDATE()) " +
                       " and  isnull(delflg,0)=0 " +
                       " union " +
                       " SELECT RID,'Employee' as FLAG,EMPANNIDATE AS DOB,EMPNAME as ENAME,EMPMOBILENO AS MOBNO,EMPEMAIL as EMAIL " +
                       " FROM mstemp " +
                       " WHERE  " +
                       " DATEPART(m, EMPANNIDATE) = DATEPART(m, GETDATE()) " +
                       " and isnull(delflg,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view billlist";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Create View BILLLIST AS  " +
                        " SELECT  BILL.RID, BILL.BILLNO, BILL.BILLDATE, convert(char(5), BILL.BILLTIME, 108) AS BILLTIME, " +
                        " BILL.CUSTNAME, MSTTABLELIST.TABLENAME, BILL.BILLPAX, " +
                        " (CASE WHEN ISNULL(BILL.ISPARCELBILL,0)=1 THEN 'PARCLE' ELSE 'BILL' END) AS BILLINFO, " +
                        " BILL.NETAMOUNT, BILL.BILLPREPBY " +
                        " FROM BILL  " +
                        " LEFT OUTER JOIN MSTTABLELIST ON MSTTABLELIST.RID = BILL.TABLERID " +
                        " WHERE (ISNULL(BILL.DELFLG, 0) = 0) AND (BILL.BILLTYPE Not in ('CASH','QUICKBILL')) ";
                //if (clsgeneral.GENBILLNOBASEDON == "REFBILLNO")
                //{
                //    str1 = " Create View BILLLIST AS  " +
                //        " SELECT  BILL.RID, BILL.REFBILLNO AS BILLNO, BILL.BILLDATE, convert(char(5), BILL.BILLTIME, 108) AS BILLTIME, " +
                //        " BILL.CUSTNAME, MSTTABLELIST.TABLENAME, BILL.BILLPAX, " +
                //        " (CASE WHEN ISNULL(BILL.ISPARCELBILL,0)=1 THEN 'PARCLE' ELSE 'BILL' END) AS BILLINFO, " +
                //        " BILL.NETAMOUNT, BILL.BILLPREPBY " +
                //        " FROM BILL  " +
                //        " LEFT OUTER JOIN MSTTABLELIST ON MSTTABLELIST.RID = BILL.TABLERID " +
                //        " WHERE (ISNULL(BILL.DELFLG, 0) = 0) AND (BILL.BILLTYPE Not in ('CASH','QUICKBILL')) ";
                //}
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW allbillinfo";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW allbillinfo AS " +
                        " SELECT  BILL.RID,  BILL.BILLNO, BILL.NETAMOUNT, " +
                        " ISNULL(BILL.NETAMOUNT, 0) - ISNULL(setleinfo.setleamount, 0) AS pendingamt,BILL.REFBILLNO" +
                        " FROM  BILL " +
                        " LEFT JOIN (SELECT SUM(SETLEAMOUNT) AS setleamount,BILLRID " +
                                    " FROM SETTLEMENT" +
                                    " WHERE (ISNULL(DELFLG, 0) = 0)" +
                                    " GROUP BY BILLRID" +
                                  " )AS setleinfo ON setleinfo.BILLRID =  BILL.RID" +
                        " WHERE ISNULL(BILL.DELFLG, 0) = 0" +
                        " UNION" +
                        " SELECT 0 AS rid, '' AS billno, 0 AS netamount, 0 AS pendingamt, '' AS REFBILLNO" +
                        " FROM MSTUSERS";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view QUICKBILLLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "Create View QUICKBILLLIST AS SELECT BILL.RID, BILL.BILLNO, BILL.REFBILLNO,BILL.BILLDATE, BILL.CUSTNAME, " +
                        " MSTTABLELIST.TABLENAME, BILL.BILLPAX, " +
                        " (CASE WHEN ISNULL(BILL.ISPARCELBILL,0)=1 THEN 'PARCLE' ELSE 'BILL' END) AS BILLINFO, " +
                        " BILL.NETAMOUNT,  " +
                        " BILL.BILLPREPBY " +
                        " FROM BILL  " +
                        " LEFT OUTER JOIN MSTTABLELIST ON MSTTABLELIST.RID = BILL.TABLERID " +
                        " WHERE (ISNULL(BILL.DELFLG, 0) = 0) AND (BILL.BILLTYPE = 'QUICKBILL') ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop View MSTBANQLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTBANQLIST AS SELECT MSTBANQ.RID,MSTBANQ.BANQCODE,MSTBANQ.BANQNAME FROM MSTBANQ WHERE(ISNULL(MSTBANQ.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTITEMGROUPLIST ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTITEMGROUPLIST AS SELECT RID, IGCODE, IGNAME,IGPNAME,IGDISPORD,IGPRINTORD,ISHIDEGROUP,SHOWINDIFFCOLOR,ISHIDEGROUPKOT,ISITEMREMGRP,ISHIDEGROUPCASHMEMO,REGLANGIGNAME FROM MSTITEMGROUP WHERE (ISNULL(DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTINVITEMLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTINVITEMLIST AS SELECT RID,ICODE,INAME FROM MSTINVITEM WHERE (ISNULL(MSTINVITEM.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTBANQITEMLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTBANQITEMLIST AS SELECT RID,BQICODE,BQINAME,BQIPNAME,BQIRATE FROM MSTBANQITEM WHERE (ISNULL(MSTBANQITEM.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop View MSTITEMLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTITEMLIST AS " +
                        " SELECT MSTITEM.RID, MSTITEMGROUP.IGNAME, MSTITEM.ICODE, MSTITEM.INAME,MSTHSNCODE.HSNCODE,MSTREPORTDEPT.REPORTDEPTNAME,MSTUNIT.UNITNAME," +
                        " MSTDEPT.DEPTNAME, MSTITEM.IRATE,MSTITEM.ISHIDEITEM,ISNULL(MSTITEM.ICOMMI,0) AS ICOMMI,MSTITEMGROUP.ISHIDEGROUPCASHMEMO,MSTHSNCODE.RID MSTHSNCODERID, " +
                        " MSTITEM.IBACKCOLOR,MSTITEM.IFORECOLOR,MSTITEM.IFONTNAME,MSTITEM.IFONTSIZE,MSTITEM.IFONTBOLD " +
                        " FROM MSTITEM " +
                        " LEFT JOIN MSTITEMGROUP ON (MSTITEMGROUP.RID = MSTITEM.IGRPRID)" +
                        " LEFT JOIN MSTUNIT ON (MSTUNIT.RID = MSTITEM.IUNITRID)" +
                        " LEFT JOIN MSTDEPT ON (MSTDEPT.RID = MSTITEM.IDEPTRID)" +
                        " LEFT JOIN MSTHSNCODE ON (MSTHSNCODE.RID = MSTITEM.HSNCODERID)" +
                        " LEFT JOIN MSTREPORTDEPT ON (MSTREPORTDEPT.RID = MSTITEM.REPORTDEPTRID)" +
                        " WHERE (ISNULL(MSTITEM.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop View BANQBOOKINGLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW BANQBOOKINGLIST AS " +
                        " SELECT BANQBOOKING.RID,BANQBOOKING.BONO,BANQBOOKING.BODATE,substring(CONVERT(VARCHAR, BOTIME, 114),1,5) AS BOTIME,MSTBANQ.BANQNAME,BANQBOOKING.BOAMT,BANQBOOKING.BOREGDATE, " +
                        " BANQBOOKING.BONOOFPER,BANQBOOKING.BOCONF,BANQBOOKING.BOTYPEOFFUNC,MSTCUST.CUSTNAME " +
                        " FROM BANQBOOKING " +
                        " LEFT JOIN MSTCUST ON (MSTCUST.RID=BANQBOOKING.CUSTRID) " +
                        " LEFT JOIN MSTBANQ ON (MSTBANQ.RID=BANQBOOKING.BANQRID) " +
                        " WHERE ISNULL(BANQBOOKING.DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop view SETTLEMENTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW SETTLEMENTLIST AS SELECT SETTLEMENT.RID, SETTLEMENT.SETLEDATE, SETTLEMENT.SETLENO, SETTLEMENT.SETLEAMOUNT, SETTLEMENT.SETLETYPE," +
                        " BILL.BILLNO FROM SETTLEMENT " +
                        " LEFT JOIN BILL ON (BILL.RID=SETTLEMENT.BILLRID)" +
                        " WHERE (ISNULL(SETTLEMENT.DELFLG, 0) = 0)";
                //if (clsgeneral.GENBILLNOBASEDON == "REFBILLNO")
                //{
                //    str1 = "CREATE VIEW SETTLEMENTLIST AS SELECT SETTLEMENT.RID, SETTLEMENT.SETLEDATE, SETTLEMENT.SETLENO, SETTLEMENT.SETLEAMOUNT, SETTLEMENT.SETLETYPE," +
                //            " BILL.REFBILLNO AS BILLNO FROM SETTLEMENT " +
                //            " LEFT JOIN BILL ON (BILL.RID=SETTLEMENT.BILLRID)" +
                //            " WHERE (ISNULL(SETTLEMENT.DELFLG, 0) = 0)";
                //}
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop view PENDINGBOOKINGBILLINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW PENDINGBOOKINGBILLINFO AS " +
                        " SELECT BANQBOOKING.RID, BANQBOOKING.CUSTRID, BANQBOOKING.BONO, BANQBOOKING.BODATE, BANQBOOKING.BOAMT, ISNULL(BANQBOOKING.BOAMT, 0) - ISNULL(BOSETLEMENT.SETLEAMT, 0)  " +
                        " AS PENDINGAMT " +
                            " FROM BANQBOOKING LEFT OUTER JOIN " +
                            " (SELECT SUM(BQBILLAMOUNT) AS SETLEAMT, BORID " +
                            " FROM BANQBILLING " +
                            " WHERE (ISNULL(DELFLG, 0) = 0)" +
                            " GROUP BY BORID) AS BOSETLEMENT ON BOSETLEMENT.BORID = BANQBOOKING.RID " +
                            " WHERE (ISNULL(BANQBOOKING.DELFLG, 0) = 0) AND (ISNULL(BANQBOOKING.BOAMT, 0) - ISNULL(BOSETLEMENT.SETLEAMT, 0) > 0) " +
                            " UNION " +
                            " SELECT     0 AS rid, 0 AS CUSTRID, '' AS BONO, '' AS BODATE, 0 AS BOAMT, 0 AS PENDINGAMT " +
                            " FROM  MSTUSERS";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop View BANQBILLINGLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW BANQBILLINGLIST AS " +
                        " SELECT  BANQBILLING.RID, BANQBILLING.BQBILLDATE,BANQBILLING.BQBILLNO, " +
                        " BANQBILLING.BQBILLAMOUNT, BANQBILLING.BQBILLTYPE, " +
                        " BANQBOOKING.BONO FROM BANQBILLING " +
                        " LEFT JOIN BANQBOOKING ON (BANQBOOKING.RID = BANQBILLING.BORID)" +
                        " WHERE  (ISNULL(BANQBILLING.DELFLG, 0) = 0) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "";
                str1 = "drop View ALLBOBILLINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW ALLBOBILLINFO AS " +
                        " SELECT     BANQBOOKING.RID, BANQBOOKING.CUSTRID, BANQBOOKING.BONO, BANQBOOKING.BODATE, BANQBOOKING.BOAMT, " +
                            " ISNULL(BANQBOOKING.BOAMT, 0) - ISNULL(BOSETLEMENT.SETLEAMT, 0) AS PENDINGAMT " +
                            " FROM  BANQBOOKING " +
                            " LEFT JOIN " +
                            " (SELECT     SUM(BQBILLAMOUNT) AS SETLEAMT, BORID " +
                            " FROM  BANQBILLING " +
                            " WHERE (ISNULL(DELFLG, 0) = 0) " +
                            " GROUP BY BORID) AS BOSETLEMENT ON BOSETLEMENT.BORID = dbo.BANQBOOKING.RID " +
                            " WHERE     (ISNULL(dbo.BANQBOOKING.DELFLG, 0) = 0)  " +
                            " UNION " +
                            " SELECT     0 AS rid, 0 AS CUSTRID, '' AS BONO, '' AS BODATE, 0 AS BOAMT, 0 AS PENDINGAMT " +
                            " FROM MSTUSERS ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "";
                str1 = "Drop View BILLCASHLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE  VIEW BILLCASHLIST AS " +
                        " SELECT  BILL.RID, BILL.BILLNO,BILL.BILLDATE, BILL.TOKENNO,BILL.CUSTNAME, " +
                        " (CASE WHEN ISNULL(BILL.ISPARCELBILL,0)=1 THEN 'PARCLE' ELSE 'BILL' END) AS TABLENAME, " +
                        " BILL.BILLPAX, BILL.NETAMOUNT, " +
                        " BILL.BILLPREPBY, MSTTIEUPCOMPANY.COMPNAME AS TIEUPCOMPANY,BILL.COUPONNO AS ORDERNO " +
                        " FROM BILL " +
                        " LEFT JOIN MSTTABLELIST ON (MSTTABLELIST.RID = BILL.TABLERID) " +
                        " LEFT JOIN MSTTIEUPCOMPANY ON (MSTTIEUPCOMPANY.RID=BILL.MSTTIEUPCOMPRID) " +
                        " WHERE (ISNULL(BILL.DELFLG, 0) = 0) AND (BILL.BILLTYPE = 'CASH')";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                //if (clsgeneral.GENBILLNOBASEDON == "REFBILLNO")
                //{
                //    str1 = "";
                //    str1 = "Drop View BILLCASHLIST";
                //    this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                //    str1 = "CREATE  VIEW BILLCASHLIST AS " +
                //       " SELECT  BILL.RID,BILL.REFBILLNO AS BILLNO,BILL.BILLDATE, BILL.TOKENNO,BILL.CUSTNAME, " +
                //       " (CASE WHEN ISNULL(BILL.ISPARCELBILL,0)=1 THEN 'PARCLE' ELSE 'BILL' END) AS TABLENAME, " +
                //       " BILL.BILLPAX, BILL.NETAMOUNT, " +
                //       " BILL.BILLPREPBY,MSTTIEUPCOMPANY.COMPNAME AS TIEUPCOMPANY,BILL.COUPONNO AS ORDERNO  " +
                //       " FROM BILL " +
                //       " LEFT JOIN MSTTABLELIST ON (MSTTABLELIST.RID = BILL.TABLERID) " +
                //       " LEFT JOIN MSTTIEUPCOMPANY ON (MSTTIEUPCOMPANY.RID=BILL.MSTTIEUPCOMPRID) " +
                //       " WHERE (ISNULL(BILL.DELFLG, 0) = 0) AND (BILL.BILLTYPE = 'CASH')";
                //    this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                //}

                str1 = "drop view MSTREPORTDEPTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "create view MSTREPORTDEPTLIST AS SELECT RID, REPORTDEPTCODE, REPORTDEPTNAME FROM MSTREPORTDEPT WHERE (ISNULL(DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view CHECKITEMSTOCKLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW CHECKITEMSTOCKLIST AS SELECT CHKIRID,SUM(ISNULL(CHKIPLUS,0)) AS IPLUS,SUM(ISNULL(CHKIMINS,0)) AS IMINS, " +
                        " (SUM(ISNULL(CHKIPLUS,0)) - SUM(ISNULL(CHKIMINS,0))) AS ISTOCK, " +
                         " SUM(ISNULL(CHKIAMOUNT,0)) AS IAMOUNT " +
                        " FROM CHECKLISTITEMDTL WHERE ISNULL(CHECKLISTITEMDTL.DELFLG,0)=0 " +
                        " GROUP BY CHKIRID ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MSTCHECKLISTITEMLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTCHECKLISTITEMLIST AS " +
                            " SELECT MSTCHECKLISTITEM.RID,MSTCHECKLISTITEM.CHKCODE,MSTCHECKLISTITEM.CHKNAME," +
                            " CHECKITEMSTOCKLIST.ISTOCK," +
                            " MSTCHECKLISTITEM.CHKSUPPLIER,MSTCHECKLISTITEM.CHKCOMPANY,MSTCHECKLISTITEM.CHKRATE " +
                            " FROM MSTCHECKLISTITEM " +
                            " LEFT JOIN CHECKITEMSTOCKLIST ON (CHECKITEMSTOCKLIST.CHKIRID = MSTCHECKLISTITEM.RID)" +
                            " WHERE ISNULL(MSTCHECKLISTITEM.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view CHECKLISTITEMINFOLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW CHECKLISTITEMINFOLIST AS SELECT RID,CHKLSTDATE,CHKLSTPREPBY FROM CHECKLISTITEM WHERE ISNULL(DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view EMPDLYATDLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW EMPDLYATDLIST AS " +
                            " SELECT EMPDLYATD.RID,EMPDLYATD.EMPRID,EMPNAME,EMPDLYATD.ATDDATE,EMPDLYATD.ATDSTATUS, " +
                            " CONVERT(char(5),EMPDLYATD.PUNCHINTIME,108) AS PUNCHINTIME," +
                            " CONVERT(char(5),EMPDLYATD.PUNCHOUTTIME,108)AS PUNCHOUTTIME," +
                            " CONVERT(char(5),EMPDLYATD.EVEPUNCHINTIME,108)AS EVEPUNCHINTIME," +
                            " CONVERT(char(5),EMPDLYATD.EVEPUNCHOUTTIME ,108)AS EVEPUNCHOUTTIME" +
                            " from EMPDLYATD" +
                            " LEFT JOIN MSTEMP ON (MSTEMP.RID = EMPDLYATD.EMPRID)" +
                            " where isnull(EMPDLYATD.DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view MULTIBILLSETTLEMENT";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "Create View MULTIBILLSETTLEMENT as " +
                        " Select billrid, count(billrid) as cntbill from settlement " +
                        " Where isnull(delflg,0)=0 group by billrid";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "drop view CASHONHANDLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW CASHONHANDLIST " +
                        " AS " +
                        " SELECT RID,CASHNO,CASHDATE,CASHAMT,CASHSTATUS,CASHPERSONNAME,CASHREMARK  " +
                        " FROM CASHONHAND WHERE (ISNULL(CASHONHAND.DELFLG, 0) = 0) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop view PENDINGPURCHASEINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW PENDINGPURCHASEINFO AS " +
                        " SELECT ITEMPURCHASE.RID,ITEMPURCHASE.PURNO,ITEMPURCHASE.NETAMOUNT," +
                            " ITEMPURCHASE.SUPPRID,ITEMPURCHASE.PURDATE,ISNULL(ITEMPURCHASE.NETAMOUNT, 0) - ISNULL(payinfo.payamount, 0) AS PENDINGAMT" +
                        " FROM ITEMPURCHASE " +
                        " LEFT JOIN (SELECT SUM(ISNULL(PAYMENTINFO.PAYAMOUNT,0)) AS payamount,PAYMENTINFO.PURRID" +
                           "  FROM PAYMENTINFO	" +
                            " WHERE (ISNULL(PAYMENTINFO.DELFLG, 0) = 0)" +
                            " GROUP BY PAYMENTINFO.PURRID) AS payinfo " +
                                                   "  ON (payinfo.PURRID = ITEMPURCHASE.RID)" +
                        " WHERE (ISNULL(ITEMPURCHASE.DELFLG, 0) = 0) " +
                        " AND (ISNULL(ITEMPURCHASE.NETAMOUNT, 0) - ISNULL(payinfo.payamount, 0) > 0)" +
                        " UNION " +
                        "  SELECT     0 AS rid, '' AS PURNO, 0 AS netamount, 0 AS SUPPRID, '' AS PURDATE,0 AS PENDINGAMT" +
                        " FROM  MSTUSERS";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop view MSTINGREDIANTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTINGREDIANTLIST AS  " +
                        " SELECT MSTINGREDIANT.RID,MSTINGREDIANT.INGCODE,MSTINGREDIANT.INGNAME FROM MSTINGREDIANT " +
                        " WHERE ISNULL(MSTINGREDIANT.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop view ITEMRECIPELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW ITEMRECIPELIST AS  " +
                        " SELECT ITEMRECIPE.RID,ITEMRECIPE.ITEMRID,MSTITEMLIST.ICODE,MSTITEMLIST.INAME,MSTITEMLIST.IGNAME," +
                            " MSTITEMLIST.IRATE,ITEMRECIPE.PROFITAMT,ITEMRECIPE.PREPTIME,ITEMRECIPE.ITEMWGT " +
                            " FROM ITEMRECIPE" +
                            " INNER JOIN MSTITEMLIST ON (MSTITEMLIST.RID=ITEMRECIPE.ITEMRID) " +
                            " WHERE ISNULL(ITEMRECIPE.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop view MSTSUPPLIERLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTSUPPLIERLIST AS  " +
                        " SELECT MSTSUPPLIER.RID, MSTSUPPLIER.SUPPCODE,MSTSUPPLIER.SUPPNAME, MSTSUPPLIER.SUPPADD1, MSTSUPPLIER.SUPPTELNO, MSTCITY.CITYNAME, " +
                        " MSTSTATE.STATENAME, MSTCOUNTRY.COUNTRYNAME, MSTSUPPLIER.SUPPMOBNO, MSTSUPPLIER.SUPPCONTPERNAME1, " +
                        " MSTSUPPLIER.SUPPEMAIL,MSTSUPPLIER.SUPPTYPE " +
                        " FROM  MSTSUPPLIER  " +
                        " LEFT JOIN MSTCITY ON (MSTCITY.CITYID = MSTSUPPLIER.SUPPCITYID) " +
                        " LEFT JOIN MSTCOUNTRY ON (MSTCOUNTRY.COUNTRYID = MSTSUPPLIER.SUPPCOUNTRYID) " +
                        " LEFT JOIN MSTSTATE ON (MSTSTATE.STATEID = MSTSUPPLIER.SUPPSTATEID) " +
                        " WHERE     (ISNULL(MSTSUPPLIER.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop View INGBASEINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW INGBASEINFO AS " +
                            " SELECT INGRESULT.* FROM (" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE1.INGGRM1," +
                            " ING1.INGNAME as INGNAME1,ITEMRECIPE1.INGUNIT1,ING1.RID AS ING1RID," +
                            " (case when ISNULL(ITEMRECIPE1.INGUNIT1,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE1.INGGRM1 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE1.INGUNIT1,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE1.INGGRM1 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE1.INGGRM1 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE1 ON (ITEMRECIPE1.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING1 ON (ING1.RID=ITEMRECIPE1.INMNM1)" +
                           " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING1.INGNAME,'')<>'' AND ISNULL(ITEMRECIPE1.DELFLG,0)=0  " +
                                       "  UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE2.INGGRM2," +
                            " ING2.INGNAME as INGNAME2,ITEMRECIPE2.INGUNIT2,ING2.RID AS ING2RID," +
                            " (case when ISNULL(ITEMRECIPE2.INGUNIT2,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE2.INGGRM2*BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE2.INGUNIT2,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE2.INGGRM2*BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE2.INGGRM2 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE2 ON (ITEMRECIPE2.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING2 ON (ING2.RID=ITEMRECIPE2.INMNM2)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING2.INGNAME,'')<>'' AND ISNULL(ITEMRECIPE2.DELFLG,0)=0  " +
                                       "  UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE3.INGGRM3," +
                            " ING3.INGNAME as INGNAME3,ITEMRECIPE3.INGUNIT3,ING3.RID AS ING3RID," +
                            " (case when ISNULL(ITEMRECIPE3.INGUNIT3,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE3.INGGRM3*BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE3.INGUNIT3,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE3.INGGRM3*BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE3.INGGRM3 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE3 ON (ITEMRECIPE3.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING3 ON (ING3.RID=ITEMRECIPE3.INMNM3)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING3.INGNAME,'')<>'' AND ISNULL(ITEMRECIPE3.DELFLG,0)=0   " +
                                   "  UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE4.INGGRM4," +
                            " ING4.INGNAME as INGNAME4,ITEMRECIPE4.INGUNIT4,ING4.RID AS ING4RID," +
                            " (case when ISNULL(ITEMRECIPE4.INGUNIT4,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE4.INGGRM4 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE4.INGUNIT4,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE4.INGGRM4 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE4.INGGRM4 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE4 ON (ITEMRECIPE4.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING4 ON (ING4.RID=ITEMRECIPE4.INMNM4)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING4.INGNAME,'')<>'' AND ISNULL(ITEMRECIPE4.DELFLG,0)=0  " +
                            " UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE5.INGGRM5," +
                            " ING5.INGNAME as INGNAME5,ITEMRECIPE5.INGUNIT5,ING5.RID AS ING5RID," +
                            " (case when ISNULL(ITEMRECIPE5.INGUNIT5,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE5.INGGRM5 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE5.INGUNIT5,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE5.INGGRM5 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE5.INGGRM5 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE5 ON (ITEMRECIPE5.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING5 ON (ING5.RID=ITEMRECIPE5.INMNM5)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING5.INGNAME,'')<>'' AND ISNULL(ITEMRECIPE5.DELFLG,0)=0  " +
                            " UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE6.INGGRM6," +
                            " ING6.INGNAME as INGNAME6,ITEMRECIPE6.INGUNIT6,ING6.RID AS ING6RID," +
                            " (case when ISNULL(ITEMRECIPE6.INGUNIT6,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE6.INGGRM6 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE6.INGUNIT6,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE6.INGGRM6 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE6.INGGRM6 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE6 ON (ITEMRECIPE6.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING6 ON (ING6.RID=ITEMRECIPE6.INMNM6)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING6.INGNAME,'')<>''  AND ISNULL(ITEMRECIPE6.DELFLG,0)=0  " +
                                        " UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE7.INGGRM7," +
                            " ING7.INGNAME as INGNAME7,ITEMRECIPE7.INGUNIT7,ING7.RID AS ING7RID," +
                            " (case when ISNULL(ITEMRECIPE7.INGUNIT7,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE7.INGGRM7 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE7.INGUNIT7,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE7.INGGRM7 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE7.INGGRM7 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE7 ON (ITEMRECIPE7.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING7 ON (ING7.RID=ITEMRECIPE7.INMNM7)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING7.INGNAME,'')<>''  AND ISNULL(ITEMRECIPE7.DELFLG,0)=0 " +
                            " UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE8.INGGRM8," +
                            " ING8.INGNAME as INGNAME8,ITEMRECIPE8.INGUNIT8,ING8.RID AS ING8RID," +
                            " (case when ISNULL(ITEMRECIPE8.INGUNIT8,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE8.INGGRM8 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE8.INGUNIT8,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE8.INGGRM8 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE8.INGGRM8 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE8 ON (ITEMRECIPE8.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING8 ON (ING8.RID=ITEMRECIPE8.INMNM8)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING8.INGNAME,'')<>''  AND ISNULL(ITEMRECIPE8.DELFLG,0)=0  " +
                            " UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE9.INGGRM9," +
                            " ING9.INGNAME as INGNAME9,ITEMRECIPE9.INGUNIT9,ING9.RID AS ING9RID," +
                            " (case when ISNULL(ITEMRECIPE9.INGUNIT9,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE9.INGGRM9 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE9.INGUNIT9,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE9.INGGRM9 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE9.INGGRM9 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE9 ON (ITEMRECIPE9.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING9 ON (ING9.RID=ITEMRECIPE9.INMNM9)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING9.INGNAME,'')<>''  AND ISNULL(ITEMRECIPE9.DELFLG,0)=0  " +
                            "       UNION ALL" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,ITEMRECIPE10.INGGRM10," +
                            " ING10.INGNAME as INGNAME10,ITEMRECIPE10.INGUNIT10,ING10.RID AS ING10RID," +
                            " (case when ISNULL(ITEMRECIPE10.INGUNIT10,'') = 'KG' then CAST(CAST((ISNULL((ITEMRECIPE10.INGGRM10 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMRECIPE10.INGUNIT10,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMRECIPE10.INGGRM10 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMRECIPE10.INGGRM10 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMRECIPE ITEMRECIPE10 ON (ITEMRECIPE10.ITEMRID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTINGREDIANT ING10 ON (ING10.RID=ITEMRECIPE10.INMNM10)" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   AND ISNULL(ING10.INGNAME,'')<>''  AND ISNULL(ITEMRECIPE10.DELFLG,0)=0  " +
                            " )INGRESULT";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);


                str1 = "Drop View STOCKISSUELIST ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW STOCKISSUELIST AS " +
                            " SELECT STOCKISSUE.RID,STOCKISSUE.ISSUENO,STOCKISSUE.ISSUEDATE,MSTDEPT.DEPTNAME,MSTEMP.EMPNAME AS ENTRYBY," +
                            " STOCKISSUE.ISSUEREFNO,STOCKISSUE.ISSUEREFDATE" +
                            " FROM STOCKISSUE " +
                            " LEFT JOIN MSTDEPT ON (MSTDEPT.RID=STOCKISSUE.ISSUEDEPTRID)" +
                            " LEFT JOIN MSTEMP ON (MSTEMP.RID=STOCKISSUE.ISSUEFROMPERRID)" +
                            " WHERE ISNULL(STOCKISSUE.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop View PENDINGPAYMENTINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW PENDINGPAYMENTINFO AS " +
                            " SELECT ITEMPURCHASE.RID,ITEMPURCHASE.PURDATE,ITEMPURCHASE.PURNO,ITEMPURCHASE.DOCNO,ITEMPURCHASE.DOCDATE," +
                            " ITEMPURCHASE.SUPPRID,ITEMPURCHASE.NETAMOUNT," +
                            " ISNULL(PAYINFO.PAYAMT,0) AS PAYAMT,MSTSUPPLIER.SUPPNAME," +
                            " (ITEMPURCHASE.NETAMOUNT - ISNULL(PAYINFO.PAYAMT,0)) AS PENDINGAMT" +
                            " FROM ITEMPURCHASE" +
                            " LEFT JOIN " +
                            " (" +
                               "  SELECT SUM(ISNULL(PAYMENTINFO.PAYAMOUNT,0)) AS PAYAMT,PAYMENTINFO.PURRID" +
                                    "  FROM PAYMENTINFO WHERE ISNULL(PAYMENTINFO.DELFLG,0)=0" +
                                    " GROUP BY PAYMENTINFO.PURRID" +
                            " ) AS PAYINFO ON (PAYINFO.PURRID = ITEMPURCHASE.RID)" +
                            " LEFT JOIN MSTSUPPLIER ON (MSTSUPPLIER.RID=ITEMPURCHASE.SUPPRID)" +
                            " WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0 AND (ITEMPURCHASE.NETAMOUNT - ISNULL(PAYINFO.PAYAMT,0)) >0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "Drop View OPENCASHDRAWERLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW OPENCASHDRAWERLIST AS " +
                            " SELECT OPENCASHDRAWER.RID,OPENCASHDRAWER.REASONDATE,OPENCASHDRAWER.REASON,MSTUSERS.USERNAME AS ENTRYUSER," +
                            " OPENCASHDRAWER.ADATETIME AS ENTRYDATE" +
                            " FROM OPENCASHDRAWER " +
                            " LEFT JOIN MSTUSERS ON (MSTUSERS.RID=OPENCASHDRAWER.AUSERID)" +
                            " WHERE ISNULL(OPENCASHDRAWER.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW ITEMWISEPURCHASELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW ITEMWISEPURCHASELIST AS SELECT ITEMWISEPURCHASE.RID,MSTITEM.INAME FROM ITEMWISEPURCHASE " +
                           "  LEFT JOIN MSTITEM ON (MSTITEM.RID=ITEMWISEPURCHASE.ITEMRID) " +
                            " WHERE ISNULL(ITEMWISEPURCHASE.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW ITEMWISEPURCHASEBASEINFO ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW ITEMWISEPURCHASEBASEINFO AS " +
                            " SELECT ITEMWISEPURCHASEINFO.* FROM (" +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                            " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                            " ITEMWISEPURCHASE1.PURITEMNM1," +
                            " ITEMWISEPURCHASE1.PURITEMNM1RID," +
                            " ITEMWISEPURCHASE1.PURGRM1," +
                            " ITEMWISEPURCHASE1.PURUNIT1," +
                            " (case when ISNULL(ITEMWISEPURCHASE1.PURUNIT1,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE1.PURGRM1 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else case when ISNULL(ITEMWISEPURCHASE1.PURUNIT1,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE1.PURGRM1 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                            " else CAST(CAST((ISNULL((ITEMWISEPURCHASE1.PURGRM1 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                            " FROM BILL" +
                            " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                            " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                            " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE1 ON (ITEMWISEPURCHASE1.ITEMRID = BILLDTL.IRID)" +
                            " WHERE " +
                            " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                            " AND ISNULL(ITEMWISEPURCHASE1.PURITEMNM1,'')<>'' AND ISNULL(ITEMWISEPURCHASE1.DELFLG,0)=0" +
                            " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE2.PURITEMNM2," +
                                " ITEMWISEPURCHASE2.PURITEMNM2RID," +
                                " ITEMWISEPURCHASE2.PURGRM2," +
                                " ITEMWISEPURCHASE2.PURUNIT2," +
                                " (case when ISNULL(ITEMWISEPURCHASE2.PURUNIT2,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE2.PURGRM2 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE2.PURUNIT2,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE2.PURGRM2 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE2.PURGRM2 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE2 ON (ITEMWISEPURCHASE2.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE2.PURITEMNM2,'')<>'' AND ISNULL(ITEMWISEPURCHASE2.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE3.PURITEMNM3," +
                                " ITEMWISEPURCHASE3.PURITEMNM3RID," +
                                " ITEMWISEPURCHASE3.PURGRM3," +
                                " ITEMWISEPURCHASE3.PURUNIT3," +
                                " (case when ISNULL(ITEMWISEPURCHASE3.PURUNIT3,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE3.PURGRM3 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE3.PURUNIT3,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE3.PURGRM3 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE3.PURGRM3 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE3 ON (ITEMWISEPURCHASE3.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE3.PURITEMNM3,'')<>'' AND ISNULL(ITEMWISEPURCHASE3.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE4.PURITEMNM4," +
                                " ITEMWISEPURCHASE4.PURITEMNM4RID," +
                                " ITEMWISEPURCHASE4.PURGRM4," +
                                " ITEMWISEPURCHASE4.PURUNIT4," +
                                " (case when ISNULL(ITEMWISEPURCHASE4.PURUNIT4,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE4.PURGRM4 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE4.PURUNIT4,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE4.PURGRM4 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE4.PURGRM4 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE4 ON (ITEMWISEPURCHASE4.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0) " +
                                " AND ISNULL(ITEMWISEPURCHASE4.PURITEMNM4,'')<>'' AND ISNULL(ITEMWISEPURCHASE4.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE5.PURITEMNM5," +
                                " ITEMWISEPURCHASE5.PURITEMNM5RID," +
                                " ITEMWISEPURCHASE5.PURGRM5," +
                                " ITEMWISEPURCHASE5.PURUNIT5," +
                                " (case when ISNULL(ITEMWISEPURCHASE5.PURUNIT5,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE5.PURGRM5 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE5.PURUNIT5,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE5.PURGRM5 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE5.PURGRM5 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE5 ON (ITEMWISEPURCHASE5.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE5.PURITEMNM5,'')<>'' AND ISNULL(ITEMWISEPURCHASE5.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE6.PURITEMNM6," +
                                " ITEMWISEPURCHASE6.PURITEMNM6RID," +
                                " ITEMWISEPURCHASE6.PURGRM6," +
                                " ITEMWISEPURCHASE6.PURUNIT6," +
                                " (case when ISNULL(ITEMWISEPURCHASE6.PURUNIT6,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE6.PURGRM6 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE6.PURUNIT6,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE6.PURGRM6 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE6.PURGRM6 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE6 ON (ITEMWISEPURCHASE6.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE6.PURITEMNM6,'')<>'' AND ISNULL(ITEMWISEPURCHASE6.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE7.PURITEMNM7," +
                                " ITEMWISEPURCHASE7.PURITEMNM7RID," +
                                " ITEMWISEPURCHASE7.PURGRM7," +
                                " ITEMWISEPURCHASE7.PURUNIT7," +
                                " (case when ISNULL(ITEMWISEPURCHASE7.PURUNIT7,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE7.PURGRM7 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE7.PURUNIT7,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE7.PURGRM7 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE7.PURGRM7 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE7 ON (ITEMWISEPURCHASE7.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE7.PURITEMNM7,'')<>'' AND ISNULL(ITEMWISEPURCHASE7.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE8.PURITEMNM8," +
                                " ITEMWISEPURCHASE8.PURITEMNM8RID," +
                                " ITEMWISEPURCHASE8.PURGRM8," +
                                " ITEMWISEPURCHASE8.PURUNIT8," +
                                " (case when ISNULL(ITEMWISEPURCHASE8.PURUNIT8,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE8.PURGRM8 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE8.PURUNIT8,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE8.PURGRM8 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE8.PURGRM8 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE8 ON (ITEMWISEPURCHASE8.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE8.PURITEMNM8,'')<>'' AND ISNULL(ITEMWISEPURCHASE8.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE9.PURITEMNM9," +
                                " ITEMWISEPURCHASE9.PURITEMNM9RID," +
                                " ITEMWISEPURCHASE9.PURGRM9," +
                                " ITEMWISEPURCHASE9.PURUNIT9," +
                                " (case when ISNULL(ITEMWISEPURCHASE9.PURUNIT9,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE9.PURGRM9 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE9.PURUNIT9,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE9.PURGRM9 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE9.PURGRM9 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE9 ON (ITEMWISEPURCHASE9.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE9.PURITEMNM9,'')<>'' AND ISNULL(ITEMWISEPURCHASE9.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE," +
                                " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY," +
                                " ITEMWISEPURCHASE10.PURITEMNM10," +
                                " ITEMWISEPURCHASE10.PURITEMNM10RID," +
                                " ITEMWISEPURCHASE10.PURGRM10," +
                                " ITEMWISEPURCHASE10.PURUNIT10," +
                                " (case when ISNULL(ITEMWISEPURCHASE10.PURUNIT10,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE10.PURGRM10 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else case when ISNULL(ITEMWISEPURCHASE10.PURUNIT10,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE10.PURGRM10 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))" +
                                " else CAST(CAST((ISNULL((ITEMWISEPURCHASE10.PURGRM10 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM" +
                                " FROM BILL" +
                                " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)" +
                                " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)" +
                                " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE10 ON (ITEMWISEPURCHASE10.ITEMRID = BILLDTL.IRID)" +
                                " WHERE " +
                                " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)  " +
                                " AND ISNULL(ITEMWISEPURCHASE10.PURITEMNM10,'')<>'' AND ISNULL(ITEMWISEPURCHASE10.DELFLG,0)=0" +
                                " AND ISNULL(BILLDTL.IQTY,0)>0 " +
                            " UNION ALL " +
                              " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE, " +
                              " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY, " +
                              " ITEMWISEPURCHASE11.PURITEMNM11, " +
                              " ITEMWISEPURCHASE11.PURITEMNM11RID, " +
                              " ITEMWISEPURCHASE11.PURGRM11, " +
                              " ITEMWISEPURCHASE11.PURUNIT11, " +
                              " (case when ISNULL(ITEMWISEPURCHASE11.PURUNIT11,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE11.PURGRM11 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                              " else case when ISNULL(ITEMWISEPURCHASE11.PURUNIT11,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE11.PURGRM11 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                              " else CAST(CAST((ISNULL((ITEMWISEPURCHASE11.PURGRM11 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM " +
                              " FROM BILL " +
                              " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID) " +
                              " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID) " +
                              " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE11 ON (ITEMWISEPURCHASE11.ITEMRID = BILLDTL.IRID) " +
                              " WHERE  " +
                              " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)   " +
                              " AND ISNULL(ITEMWISEPURCHASE11.PURITEMNM11,'')<>'' AND ISNULL(ITEMWISEPURCHASE11.DELFLG,0)=0 " +
                              " AND ISNULL(BILLDTL.IQTY,0)>0  " +
                              " UNION ALL  " +
                                  " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE,  " +
                                  " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,  " +
                                  " ITEMWISEPURCHASE12.PURITEMNM12,  " +
                                  " ITEMWISEPURCHASE12.PURITEMNM12RID,  " +
                                  " ITEMWISEPURCHASE12.PURGRM12,  " +
                                  " ITEMWISEPURCHASE12.PURUNIT12,  " +
                                  " (case when ISNULL(ITEMWISEPURCHASE12.PURUNIT12,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE12.PURGRM12 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else case when ISNULL(ITEMWISEPURCHASE12.PURUNIT12,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE12.PURGRM12 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else CAST(CAST((ISNULL((ITEMWISEPURCHASE12.PURGRM12 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM  " +
                                  " FROM BILL  " +
                                  " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)  " +
                                  " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)  " +
                                  " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE12 ON (ITEMWISEPURCHASE12.ITEMRID = BILLDTL.IRID)  " +
                                  " WHERE   " +
                                  " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)    " +
                                  " AND ISNULL(ITEMWISEPURCHASE12.PURITEMNM12,'')<>'' AND ISNULL(ITEMWISEPURCHASE12.DELFLG,0)=0  " +
                                  " AND ISNULL(BILLDTL.IQTY,0)>0   " +
                              " UNION ALL  " +
                                   " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE,  " +
                                  " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,  " +
                                  " ITEMWISEPURCHASE13.PURITEMNM13,  " +
                                  " ITEMWISEPURCHASE13.PURITEMNM13RID,  " +
                                  " ITEMWISEPURCHASE13.PURGRM13,  " +
                                  " ITEMWISEPURCHASE13.PURUNIT13,  " +
                                  " (case when ISNULL(ITEMWISEPURCHASE13.PURUNIT13,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE13.PURGRM13 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else case when ISNULL(ITEMWISEPURCHASE13.PURUNIT13,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE13.PURGRM13 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else CAST(CAST((ISNULL((ITEMWISEPURCHASE13.PURGRM13 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM  " +
                                  " FROM BILL  " +
                                  " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)  " +
                                  " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)  " +
                                  " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE13 ON (ITEMWISEPURCHASE13.ITEMRID = BILLDTL.IRID)  " +
                                  " WHERE   " +
                                  " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)    " +
                                  " AND ISNULL(ITEMWISEPURCHASE13.PURITEMNM13,'')<>'' AND ISNULL(ITEMWISEPURCHASE13.DELFLG,0)=0  " +
                                  " AND ISNULL(BILLDTL.IQTY,0)>0   " +
                              " UNION ALL  " +
                                  " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE,  " +
                                  " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,  " +
                                  " ITEMWISEPURCHASE14.PURITEMNM14,  " +
                                  " ITEMWISEPURCHASE14.PURITEMNM14RID,  " +
                                  " ITEMWISEPURCHASE14.PURGRM14,  " +
                                  " ITEMWISEPURCHASE14.PURUNIT14,  " +
                                  " (case when ISNULL(ITEMWISEPURCHASE14.PURUNIT14,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE14.PURGRM14 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else case when ISNULL(ITEMWISEPURCHASE14.PURUNIT14,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE14.PURGRM14 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else CAST(CAST((ISNULL((ITEMWISEPURCHASE14.PURGRM14 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM  " +
                                  " FROM BILL  " +
                                  " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)  " +
                                  " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)  " +
                                  " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE14 ON (ITEMWISEPURCHASE14.ITEMRID = BILLDTL.IRID)  " +
                                  " WHERE   " +
                                  " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)    " +
                                  " AND ISNULL(ITEMWISEPURCHASE14.PURITEMNM14,'')<>'' AND ISNULL(ITEMWISEPURCHASE14.DELFLG,0)=0  " +
                                  " AND ISNULL(BILLDTL.IQTY,0)>0   " +
                              " UNION ALL  " +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.BILLDATE,  " +
                                  " BILLDTL.IRID,MSTITEM.INAME,BILLDTL.IQTY,  " +
                                  " ITEMWISEPURCHASE15.PURITEMNM15,  " +
                                  " ITEMWISEPURCHASE15.PURITEMNM15RID,  " +
                                  " ITEMWISEPURCHASE15.PURGRM15,  " +
                                  " ITEMWISEPURCHASE15.PURUNIT15,  " +
                                  " (case when ISNULL(ITEMWISEPURCHASE15.PURUNIT15,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE15.PURGRM15 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else case when ISNULL(ITEMWISEPURCHASE15.PURUNIT15,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE15.PURGRM15 * BILLDTL.IQTY),0)/1000) AS FLOAT) AS decimal(16,3))  " +
                                  " else CAST(CAST((ISNULL((ITEMWISEPURCHASE15.PURGRM15 * BILLDTL.IQTY),0)) AS FLOAT) AS decimal(16,3)) END END ) AS OPGRAM  " +
                                  " FROM BILL  " +
                                  " INNER JOIN BILLDTL ON (BILL.RID = BILLDTL.BILLRID)  " +
                                  " INNER JOIN MSTITEM ON (BILLDTL.IRID = MSTITEM.RID)  " +
                                  " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE15 ON (ITEMWISEPURCHASE15.ITEMRID = BILLDTL.IRID)  " +
                                  " WHERE   " +
                                  " ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILLDTL.DELFLG,0)=0 AND (isnull(BILL.ISREVISEDBILL,0)=0)    " +
                                  " AND ISNULL(ITEMWISEPURCHASE15.PURITEMNM15,'')<>'' AND ISNULL(ITEMWISEPURCHASE15.DELFLG,0)=0  " +
                                  " AND ISNULL(BILLDTL.IQTY,0)>0   " +
                            " )ITEMWISEPURCHASEINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW ITEMWISERECIPEBASEINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW ITEMWISERECIPEBASEINFO " +
                            " AS" +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE1.PURITEMNM1RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE1.PURUNIT1,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE1.PURGRM1 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE1.PURUNIT1,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE1.PURGRM1 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE1.PURGRM1 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE1 ON (ITEMWISEPURCHASE1.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE1.PURITEMNM1RID)" +
                        " WHERE ISNULL(ITEMWISEPURCHASE1.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE1.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE1.PURITEMNM1RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE2.PURITEMNM2RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE2.PURUNIT2,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE2.PURGRM2 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE2.PURUNIT2,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE2.PURGRM2 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE2.PURGRM2 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE2 ON (ITEMWISEPURCHASE2.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE2.PURITEMNM2RID)" +
                        " WHERE ISNULL(ITEMWISEPURCHASE2.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE2.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE2.PURITEMNM2RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE3.PURITEMNM3RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE3.PURUNIT3,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE3.PURGRM3 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE3.PURUNIT3,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE3.PURGRM3 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE3.PURGRM3 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE3 ON (ITEMWISEPURCHASE3.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE3.PURITEMNM3RID)" +
                        " WHERE ISNULL(ITEMWISEPURCHASE3.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE3.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE3.PURITEMNM3RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE4.PURITEMNM4RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE4.PURUNIT4,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE4.PURGRM4 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE4.PURUNIT4,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE4.PURGRM4 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE4.PURGRM4 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE4 ON (ITEMWISEPURCHASE4.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE4.PURITEMNM4RID)" +
                        " WHERE ISNULL(ITEMWISEPURCHASE4.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE4.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE4.PURITEMNM4RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE5.PURITEMNM5RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE5.PURUNIT5,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE5.PURGRM5 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE5.PURUNIT5,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE5.PURGRM5 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE5.PURGRM5 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE5 ON (ITEMWISEPURCHASE5.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE5.PURITEMNM5RID)" +
                        " WHERE ISNULL(ITEMWISEPURCHASE5.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE5.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE5.PURITEMNM5RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE6.PURITEMNM6RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE6.PURUNIT6,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE6.PURGRM6 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE6.PURUNIT6,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE6.PURGRM6 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE6.PURGRM6 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE6 ON (ITEMWISEPURCHASE6.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE6.PURITEMNM6RID)" +
                        " WHERE ISNULL(ITEMWISEPURCHASE6.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE6.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE6.PURITEMNM6RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE7.PURITEMNM7RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE7.PURUNIT7,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE7.PURGRM7 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE7.PURUNIT7,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE7.PURGRM7 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE7.PURGRM7 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE7 ON (ITEMWISEPURCHASE7.ITEMRID = MSTITEM.RID)" +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE7.PURITEMNM7RID) " +
                        " WHERE ISNULL(ITEMWISEPURCHASE7.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE7.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE7.PURITEMNM7RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE8.PURITEMNM8RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE8.PURUNIT8,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE8.PURGRM8 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE8.PURUNIT8,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE8.PURGRM8 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE8.PURGRM8 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE8 ON (ITEMWISEPURCHASE8.ITEMRID = MSTITEM.RID)" +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE8.PURITEMNM8RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE8.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE8.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE8.PURITEMNM8RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE9.PURITEMNM9RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE9.PURUNIT9,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE9.PURGRM9 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE9.PURUNIT9,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE9.PURGRM9 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE9.PURGRM9 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE9 ON (ITEMWISEPURCHASE9.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE9.PURITEMNM9RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE9.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE9.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE9.PURITEMNM9RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE10.PURITEMNM10RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE10.PURUNIT10,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE10.PURGRM10 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE10.PURUNIT10,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE10.PURGRM10 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE10.PURGRM10 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE10 ON (ITEMWISEPURCHASE10.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE10.PURITEMNM10RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE10.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE10.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE10.PURITEMNM10RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE11.PURITEMNM11RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE11.PURUNIT11,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE11.PURGRM11 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE11.PURUNIT11,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE11.PURGRM11 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE11.PURGRM11 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE11 ON (ITEMWISEPURCHASE11.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE11.PURITEMNM11RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE11.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE11.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE11.PURITEMNM11RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE12.PURITEMNM12RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE12.PURUNIT12,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE12.PURGRM12 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE12.PURUNIT12,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE12.PURGRM12 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE12.PURGRM12 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE12 ON (ITEMWISEPURCHASE12.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE12.PURITEMNM12RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE12.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE12.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE12.PURITEMNM12RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE13.PURITEMNM13RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE13.PURUNIT13,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE13.PURGRM13 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE13.PURUNIT13,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE13.PURGRM13 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE13.PURGRM13 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE13 ON (ITEMWISEPURCHASE13.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE13.PURITEMNM13RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE13.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE13.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE13.PURITEMNM13RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE14.PURITEMNM14RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE14.PURUNIT14,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE14.PURGRM14 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE14.PURUNIT14,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE14.PURGRM14 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE14.PURGRM14 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE14 ON (ITEMWISEPURCHASE14.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE14.PURITEMNM14RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE14.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE14.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE14.PURITEMNM14RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0" +
                        " UNION ALL " +
                        " SELECT MSTITEM.RID AS IRID,MSTITEM.INAME,ITEMWISEPURCHASE15.PURITEMNM15RID,MSTPURITEM.PURINAME AS PURINAME1," +
                        " (case when ISNULL(ITEMWISEPURCHASE15.PURUNIT15,'') = 'KG' then CAST(CAST((ISNULL((ITEMWISEPURCHASE15.PURGRM15 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else case when ISNULL(ITEMWISEPURCHASE15.PURUNIT15,'') = 'LTR' then  CAST(CAST((ISNULL((ITEMWISEPURCHASE15.PURGRM15 * 1),0)/1000) AS FLOAT) AS decimal(16,3)) " +
                        " else CAST(CAST((ISNULL((ITEMWISEPURCHASE15.PURGRM15 * 1),0)) AS FLOAT) AS decimal(16,3)) END END ) AS INGGRAM " +
                        " FROM MSTITEM " +
                        " LEFT JOIN ITEMWISEPURCHASE ITEMWISEPURCHASE15 ON (ITEMWISEPURCHASE15.ITEMRID = MSTITEM.RID) " +
                        " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMWISEPURCHASE15.PURITEMNM15RID)  " +
                        " WHERE ISNULL(ITEMWISEPURCHASE15.ITEMRID,0)>0 AND ISNULL(ITEMWISEPURCHASE15.DELFLG,0)=0 " +
                        " AND ISNULL(ITEMWISEPURCHASE15.PURITEMNM15RID,0)>0 AND ISNULL(MSTITEM.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW PURCHASEITEMPURCHASESTOCKINFO ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW PURCHASEITEMPURCHASESTOCKINFO" +
                            " AS" +
                            " SELECT ITEMPURCHASEDTL.INAME,ITEMPURCHASEDTL.IUNIT,SUM(ISNULL(ITEMPURCHASEDTL.IQTY,0)) AS PURQTY" +
                                    " FROM ITEMPURCHASE " +
                                    " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID) " +
                                    " WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0 AND ISNULL(ITEMPURCHASEDTL.DELFLG,0)=0  " +
                                    " GROUP BY ITEMPURCHASEDTL.INAME,ITEMPURCHASEDTL.IUNIT ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW PURCAHSEITEMUSAGESTOCKINFO";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW PURCAHSEITEMUSAGESTOCKINFO " +
                            " AS " +
                                " SELECT ITEMWISEPURCHASEBASEINFO.PURITEMNM1RID,ITEMWISEPURCHASEBASEINFO.PURITEMNM1,ITEMWISEPURCHASEBASEINFO.PURUNIT1," +
                                " SUM(ITEMWISEPURCHASEBASEINFO.OPGRAM) AS USAGEQTY" +
                                " FROM ITEMWISEPURCHASEBASEINFO" +
                                " GROUP BY ITEMWISEPURCHASEBASEINFO.PURITEMNM1RID,ITEMWISEPURCHASEBASEINFO.PURITEMNM1,ITEMWISEPURCHASEBASEINFO.PURUNIT1";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW PURCHASEITEMSTOCK";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " create VIEW PURCHASEITEMSTOCK AS " +
                        " SELECT PURCHASEITEMPURCHASESTOCKINFO.INAME AS PURCHASEITEMNAME," +
                           "  CAST(CAST(ISNULL(PURCHASEITEMPURCHASESTOCKINFO.PURQTY,0) AS FLOAT) AS DECIMAL(16,3)) AS PURCHASEQTY," +
                               "  ISNULL(PURCAHSEITEMUSAGESTOCKINFO.USAGEQTY,0) AS USEQTY, 	" +
                                " (" +
                                  "   CAST(CAST(ISNULL(PURCHASEITEMPURCHASESTOCKINFO.PURQTY,0) AS FLOAT) AS DECIMAL(16,3)) - ISNULL(PURCAHSEITEMUSAGESTOCKINFO.USAGEQTY,0)" +
                                " ) AS STOCKQTY," +
                                " PURCHASEITEMPURCHASESTOCKINFO.IUNIT AS UNIT" +
                         " FROM PURCHASEITEMPURCHASESTOCKINFO" +
                        " LEFT JOIN PURCAHSEITEMUSAGESTOCKINFO ON " +
                          "   (PURCAHSEITEMUSAGESTOCKINFO.PURITEMNM1 = PURCHASEITEMPURCHASESTOCKINFO.INAME " +
                            "     AND PURCAHSEITEMUSAGESTOCKINFO.PURUNIT1=PURCHASEITEMPURCHASESTOCKINFO.IUNIT) ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW BANQBILLINFOLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW BANQBILLINFOLIST AS" +
                            " SELECT BANQBILLINFO.RID,BANQBILLINFO.BANQBILLNO,BANQBILLINFO.BANQREFNO,BANQBILLINFO.BANQBILLDATE," +
                            " MSTBANQ.BANQNAME,BANQBILLINFO.BANQFUNCNAME,MSTCUST.CUSTNAME,BANQBILLINFO.NETAMOUNT" +
                             " FROM BANQBILLINFO" +
                            " LEFT JOIN MSTBANQ ON (MSTBANQ.RID=BANQBILLINFO.BANQRID)" +
                            " LEFT JOIN MSTCUST ON (MSTCUST.RID=BANQBILLINFO.CUSTRID)" +
                            " WHERE ISNULL(BANQBILLINFO.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW BANQBOFOLLOWUPLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW BANQBOFOLLOWUPLIST  AS " +
                        " SELECT BANQBOFOLLOWUP.RID, BANQBOFOLLOWUP.BANQBORID,BANQBOFOLLOWUP.FOLLOWUPDT,BANQBOFOLLOWUP.CONTPERNAME,BANQBOFOLLOWUP.NEXTFOLLOWUPDT,BANQBOFOLLOWUP.FOLLOWUPBY " +
                        " FROM BANQBOFOLLOWUP WHERE ISNULL(BANQBOFOLLOWUP.DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW BANQINQUIRYLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW BANQINQUIRYLIST AS " +
                            " SELECT  BANQINQUIRY.RID,BANQINQUIRY.INQDATE,MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO," +
                            " MSTBANQ.BANQNAME,BANQINQUIRY.TYPEOFFUNC" +
                             " FROM BANQINQUIRY" +
                            " LEFT JOIN MSTCUST on (MSTCUST.RID = BANQINQUIRY.GRID)" +
                            " LEFT JOIN MSTBANQ ON (MSTBANQ.RID=BANQINQUIRY.VENUE)" +
                            " WHERE ISNULL(BANQINQUIRY.DELFLG,0)=0    ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW PURCHASEITEMLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW PURCHASEITEMLIST AS SELECT DISTINCT INAME From ITEMPURCHASEDTL ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);


                str1 = "DROP VIEW MSTPURCHASEITEMLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTPURCHASEITEMLIST AS " +
                            " SELECT MSTPURITEM.RID,MSTPURITEM.PURINAME AS ITEMNAME,MSTPURITEM.PURIUNIT,MSTPURITEMGROUP.PURIGNAME,MSTPURITEM.MINRATE,MSTPURITEM.MAXRATE,MSTPURITEM.AVGRATE," +
                                " MSTPURITEM.MINQTY,MSTPURITEM.MAXQTY,MSTPURITEM.AVGQTY " +
                                " FROM MSTPURITEM " +
                                " LEFT JOIN MSTPURITEMGROUP on (MSTPURITEMGROUP.RID=MSTPURITEM.PURIGRID)" +
                                " WHERE ISNULL(MSTPURITEM.DELFLG,0)=0  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW BQBOOKINGLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW BQBOOKINGLIST AS " +
                                " SELECT BQBOOKING.RID,BQBOOKING.BQDATE,BQBOOKING.BQBONO,BQBOOKING.BODATE,MSTBANQ.BANQNAME,MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO,BQBOOKING.BOTYPEOFFUNC,BQBOOKING.BONOOFPER,BQBOOKING.BQBOSTATUS" +
                                " FROM BQBOOKING " +
                                " LEFT JOIN MSTBANQ ON (MSTBANQ.RID=BQBOOKING.BANQRID)" +
                                " LEFT JOIN MSTCUST ON (MSTCUST.RID=BQBOOKING.CUSTRID)" +
                                " WHERE ISNULL(BQBOOKING.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);


                str1 = "DROP VIEW MSTPURITEMGROUPLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTPURITEMGROUPLIST AS " +
                            " SELECT MSTPURITEMGROUP.RID,MSTPURITEMGROUP.PURIGCODE,MSTPURITEMGROUP.PURIGNAME,MSTPURITEMGROUP.ISAUTOISSUE" +
                            " FROM MSTPURITEMGROUP WHERE ISNULL(MSTPURITEMGROUP.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW STOCKCLOSINGLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW STOCKCLOSINGLIST AS  " +
                            " SELECT STOCKCLOSING.RID,STOCKCLOSING.STOCKCLNO,STOCKCLOSING.CLDATE,MSTDEPT.DEPTNAME,STOCKCLOSING.ENTRYBY " +
                            " FROM STOCKCLOSING " +
                            " LEFT JOIN MSTDEPT ON (MSTDEPT.RID=STOCKCLOSING.DEPTRID) " +
                            " WHERE ISNULL(STOCKCLOSING.DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTPURITEMDISTINCT";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTPURITEMDISTINCT AS " +
                        " SELECT DISTINCT ITEMPURCHASEDTL.INAME,ITEMPURCHASEDTL.IUNIT " +
                        " FROM ITEMPURCHASE" +
                        " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID = ITEMPURCHASE.RID)" +
                        " WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0 AND  ISNULL(ITEMPURCHASEDTL.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW STOCKWASTAGELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW STOCKWASTAGELIST AS " +
                        " SELECT STOCKWASTAGE.RID,STOCKWASTAGE.WSTNO,STOCKWASTAGE.WSTDATE,MSTDEPT.DEPTNAME,STOCKWASTAGE.ENTRYBY FROM STOCKWASTAGE " +
                        " LEFT JOIN MSTDEPT ON (MSTDEPT.RID=STOCKWASTAGE.DEPTRID) " +
                        "WHERE ISNULL(STOCKWASTAGE.DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTREFBYTYPELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTREFBYTYPELIST AS SELECT RID,REFBYTYPECODE,REFBYTYPENAME FROM MSTREFBYTYPE WHERE ISNULL(DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTREFBYLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTREFBYLIST AS SELECT MSTREFBY.RID,MSTREFBY.REFBYCODE,MSTREFBY.REFBYNAME,MSTREFBYTYPE.REFBYTYPENAME " +
                            " FROM MSTREFBY " +
                            " LEFT JOIN MSTREFBYTYPE ON (MSTREFBYTYPE.RID=MSTREFBY.REFBYTYPERID)" +
                            " WHERE ISNULL(MSTREFBY.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTTIEUPCOMPANYLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTTIEUPCOMPANYLIST AS " +
                            " SELECT RID,COMPCODE,COMPNAME,CONTPER,CONTNO,COMPDISC  " +
                            " FROM MSTTIEUPCOMPANY " +
                            " WHERE ISNULL(DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW TABLERESERVATIONLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW TABLERESERVATIONLIST AS " +
                            " SELECT TABLERESERVATION.RID,TABLERESERVATION.REVNO,TABLERESERVATION.REVDATE,convert(char(5), TABLERESERVATION.REVTIME, 108) as REVTIME, " +
                            " MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO," +
                            " MSTTABLE.TABLENAME,TABLERESERVATION.PAX,TABLERESERVATION.FUNCNAME " +
                            " FROM TABLERESERVATION" +
                            " LEFT JOIN MSTCUST ON (MSTCUST.RID = TABLERESERVATION.CUSTRID)" +
                            " LEFT JOIN MSTTABLE ON (MSTTABLE.RID = TABLERESERVATION.TABLERID)" +
                            " WHERE ISNULL(TABLERESERVATION.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW TABLEWAITINGLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW TABLEWAITINGLIST AS " +
                        " SELECT TBLWAIT.RID,TBLWAIT.WAITDATE,TBLWAIT.WAITTIME,MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO," +
                        " TBLWAIT.QNO,TBLWAIT.PAX,TBLWAIT.WAITREMARK,TBLWAIT.WAITSTATUS,TBLWAIT.SMSSTATUS " +
                        " FROM TBLWAIT " +
                        " LEFT JOIN MSTCUST ON (MSTCUST.RID=TBLWAIT.CUSTRID)" +
                        " WHERE ISNULL(TBLWAIT.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTEXPENCESLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTEXPENCESLIST AS" +
                        " SELECT MSTEXPENCES.RID,MSTEXPENCES.EXCODE,MSTEXPENCES.EXNAME " +
                        " FROM MSTEXPENCES " +
                        " WHERE(ISNULL(MSTEXPENCES.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTINCOMELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTINCOMELIST AS" +
                        " SELECT MSTINCOME.RID,MSTINCOME.INCODE,MSTINCOME.INNAME " +
                        " FROM MSTINCOME " +
                        " WHERE(ISNULL(MSTINCOME.DELFLG, 0) = 0)";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW INCOMELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW INCOMELIST AS " +
                            " SELECT INCOME.RID,INCOME.INCOMENO,INCOME.INDATE,MSTINCOME.INNAME,INCOME.INTYPE,INCOME.INAMOUNT " +
                            " FROM INCOME " +
                            " LEFT JOIN MSTINCOME ON (MSTINCOME.RID=INCOME.INRID) " +
                            " WHERE ISNULL(INCOME.DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW EXPENCELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW EXPENCELIST AS " +
                            " SELECT EXPENCE.RID,EXPENCE.EXPENCENO,EXPENCE.EXDATE,MSTEXPENCES.EXNAME,EXPENCE.EXTYPE,EXPENCE.EXAMOUNT " +
                            " FROM EXPENCE " +
                            " LEFT JOIN MSTEXPENCES ON (MSTEXPENCES.RID=EXPENCE.EXRID) " +
                            " WHERE ISNULL(EXPENCE.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTHSNCODELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTHSNCODELIST AS  " +
                        " SELECT MSTHSNCODE.RID,HSNCODE,HSNCODEREMARK FROM  MSTHSNCODE WHERE ISNULL(DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTIMPDOCLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = "CREATE VIEW MSTIMPDOCLIST AS " +
                        " SELECT MSTIMPDOC.RID,MSTIMPDOC.DOCNAME,MSTIMPDOC.DOCTYPE,MSTIMPDOC.DOCREGNO,MSTIMPDOC.DOCAGENCYNAME,MSTIMPDOC.VALIDFROM,MSTIMPDOC.VALIDTO,MSTIMPDOC.CONTPER,MSTIMPDOC.CONTNO" +
                        " FROM MSTIMPDOC WHERE ISNULL(MSTIMPDOC.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW PHYSTOCKLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW PHYSTOCKLIST AS" +
                            " SELECT RID,PHYSTKNO,PHYDATE FROM PHYSTOCK" +
                            " WHERE ISNULL(PHYSTOCK.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MSTMEALTYPELIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MSTMEALTYPELIST " +
                        " AS  " +
                        " SELECT RID,MTCODE,MTNAME,MTRATE,MTTAX1,MTTAX2,MTTAX3,MTTAX4,MTTAX5 FROM MSTMEALTYPE " +
                        " WHERE ISNULL(DELFLG,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MEALRESERLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MEALRESERLIST " +
                        " AS  " +
                        " SELECT MEALRESER.RID,MEALRESER.REGDATE,MEALRESER.RESERNO,MEALRESER.REFNO," +
                        " MSTCUST.CUSTNAME,MEALRESER.SDT,MEALRESER.EDT,MEALRESER.RESTYPE,MEALRESER.MEALTOTAMT" +
                        " FROM MEALRESER" +
                        " LEFT JOIN MSTCUST ON (MSTCUST.RID=MEALRESER.CUSTRID)" +
                        " WHERE ISNULL(MEALRESER.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MEALBILLLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MEALBILLLIST AS " +
                            " SELECT MEALBILL.RID, MEALBILL.MEALRESERRID,MEALBILL.MEALBILLNO,MEALBILL.MEALBILLDATE,MEALBILL.MEALBILLTIME," +
                            " MEALBILL.REFNO,MSTCUST.CUSTNAME,MEALBILL.MEALBILLTOTAMT" +
                            " FROM MEALBILL" +
                            " LEFT JOIN MSTCUST ON (MSTCUST.RID=MEALBILL.CUSTRID)" +
                            " WHERE ISNULL(MEALBILL.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "DROP VIEW MESSPAYMENTLIST";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);
                str1 = " CREATE VIEW MESSPAYMENTLIST AS " +
                            " SELECT MESSPAYMENT.RID,MESSPAYMENT.PAYDATE,MESSPAYMENT.PAYMENTNO,MSTCUST.CUSTNAME,MESSPAYMENT.TOTAMT,MESSPAYMENT.PREPBY " +
                            " FROM MESSPAYMENT " +
                            " LEFT JOIN MSTCUST ON (MSTCUST.RID=MESSPAYMENT.CUSTRID) " +
                            " WHERE ISNULL(MESSPAYMENT.DELFLG,0)=0";
                this.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region CREATE STORED PROCEDURE

        public bool Create_OnlineDb_STOREDPROCEDURE()
        {
            string strproc;
            try
            {

                this.DeleteProcedureFromOnlineDb("sp_MSTCITY");
                strproc = "  Create Procedure sp_MSTCITY( " +
                            "  @p_CityId as bigint, " +
                             "  @p_CityName as nvarchar(100), " +
                             "  @p_Errstr as nvarchar(max) out,  @p_Retval as int out) " +
                            "  As " +
                            "  Begin Try " +
                               "    Begin " +
                              "  set @p_Errstr =''  set @p_Retval =0 " +
                                 "  Begin " +
                             "  Insert Into mstCity (CityId,CityName) Values(@p_CityId,@p_CityName) " +
                                "   End " +
                                 "  End " +
                            "  End Try " +
                            "  Begin catch  SELECT   " +
                                     "    ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                      "   set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()  " +
                                 "  Return " +
                            "  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTCOUNTRY");
                strproc = " CREATE Procedure sp_MSTCOUNTRY(  " +
                             " @p_CountryId as bigint, " +
                             " @p_CountryName as nvarchar(100), " +
                             " @p_Errstr as nvarchar(max) out,  @p_Retval as int out) " +
                            " As " +
                            " Begin Try " +
                                "  Begin " +
                              " set @p_Errstr =''  set @p_Retval =0 " +
                                "  Begin " +
                             " Insert Into mstCountry (CountryId,CountryName) Values(@p_CountryId,@p_CountryName) " +
                               "   End " +
                                "  End " +
                            " End Try " +
                            " Begin catch  SELECT    " +
                                     "   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; " +
                                     "   set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()  " +
                                 " Return " +
                            " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTSTATE");
                strproc = " Create Procedure sp_MSTSTATE( " +
                             " @p_StateId as bigint, " +
                             " @p_StateName as nvarchar(100), " +
                             " @p_Errstr as nvarchar(max) out,  @p_Retval as int out) " +
                            " As " +
                            " Begin Try " +
                                "  Begin " +
                              " set @p_Errstr =''  set @p_Retval =0  " +
                                "  Begin " +
                             " Insert Into mstState (StateId,StateName) Values(@p_StateId,@p_StateName) " +
                               "   End " +
                                "  End " +
                            " End Try " +
                            " Begin catch  SELECT    " +
                                     "   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; " +
                                     "   set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()  " +
                                 " Return " +
                            " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_RMSSETTING");
                strproc = "";
                strproc = "Create procedure sp_RMSSETTING " +
                          " (  @p_mode as int,  " +
                          " @p_autokotno nvarchar(50), " +
                          " @p_autobillno nvarchar(50), " +
                          " @p_autosettno nvarchar(50)," +
                          " @p_printkot nvarchar(50)," +
                          " @p_printbill nvarchar(50)," +
                          " @p_thermalprintertype nvarchar(50)," +
                         " @p_generaltitle1 nvarchar(4000)," +
                        " @p_generaltitle2 nvarchar(4000)," +
                        " @p_generaltitle3 nvarchar(4000)," +
                        " @p_generaltitle4 nvarchar(4000)," +
                        " @p_generaltitle5 nvarchar(4000)," +
                        " @p_thermaltitle1 nvarchar(30)," +
                        " @p_thermaltitle2 nvarchar(30)," +
                        " @p_thermaltitle3 nvarchar(30)," +
                        " @p_thermaltitle4 nvarchar(30)," +
                        " @p_thermaltitle5 nvarchar(30)," +
                        " @p_autopurno nvarchar(30)," +
                        " @p_autopayno nvarchar(30)," +
                        " @p_generalfooteralignment nvarchar(30)," +
                        " @p_generalfooter1 nvarchar(4000)," +
                        " @p_generalfooter2 nvarchar(4000)," +
                        " @p_generalfooter3 nvarchar(4000)," +
                        " @p_generalfooter4 nvarchar(4000)," +
                        " @p_generalfooter5 nvarchar(4000)," +
                        " @p_thermalfooteralignment nvarchar(30)," +
                        " @p_thermalfooter1 nvarchar(50)," +
                        " @p_thermalfooter2 nvarchar(50)," +
                        " @p_thermalfooter3 nvarchar(50),	" +
                        " @p_thermalfooter4 nvarchar(50)," +
                        " @p_thermalfooter5 nvarchar(50)," +
                        " @p_thermalfooterprinttype nvarchar(50)," +
                        " @p_sertax decimal(18,2)," +
                        " @p_vat decimal(18,2)," +
                        " @p_sercharges decimal(18,2)," +
                        " @p_disc decimal(18,2)," +
                        " @p_autobill nvarchar(50)," +
                        " @p_kitprinter nvarchar(200)," +
                        " @p_billingprinter nvarchar(200)," +
                        " @p_reportprinter nvarchar(200)," +
                        " @p_printcustinfo bit," +
                        " @p_deptkot bit," +
                        " @p_kottitle1 nvarchar(30)," +
                        " @p_kottitle2 nvarchar(30)," +
                        " @p_kottitle3 nvarchar(30)," +
                        " @p_kottitle4 nvarchar(30)," +
                        " @p_kottitle5 nvarchar(30)," +
                        " @p_backuploc nvarchar(2000)," +
                        " @p_cutdeptkot bit," +
                        " @p_allowrate bit," +
                        " @p_proprovider nvarchar(100)," +
                        " @p_prouserid nvarchar(100)," +
                        " @p_propassword nvarchar(100)," +
                        " @p_traprovider nvarchar(100)," +
                        " @p_trauserid nvarchar(100)," +
                        " @p_trapassword nvarchar(100)," +
                        " @p_smssign nvarchar(100)," +
                        " @p_noofbill bigint," +
                        " @p_donotallowbill bit," +
                        " @p_hidebusu bit," +
                        " @p_hidecommi bit," +
                        " @p_thbillptype nvarchar(50)," +
                        " @p_smtpemailaddress nvarchar(200)," +
                        " @p_smtpemailpassword nvarchar(200)," +
                        " @p_smtpaddress nvarchar(200)," +
                        " @p_smtpport nvarchar(50)," +
                        " @p_noofkot bigint," +
                        " @p_autotokenno nvarchar(100)," +
                        " @p_thkotptype nvarchar(100)," +
                        " @p_hidereport bit," +
                        " @p_fillsettlementamt bit," +
                        " @p_calcvatper decimal(18,2)," +
                        " @p_NOTREQSETTLEMENT bit," +
                        " @p_ISCHECKPAPERSIZE bit," +
                        " @p_ISLOGOUTPRINT bit," +
                        " @p_ISSTOPUSERBILLPRINT bit," +
                        " @p_ISFOCUSONITEMNAME bit," +
                        " @p_ISKITDISP BIT," +
                        " @p_CASHMEMOVER nvarchar(10)," +
                        " @p_ISPAXREQBILL BIT," +
                        " @p_ISKOTSAVEPRINT bit," +
                        " @p_SMSRESTNAME nvarchar(50)," +
                        " @p_PROSENDERID nvarchar(50)," +
                        " @p_HOTELSERVER nvarchar(500)," +
                        " @p_HOTELDATABASE nvarchar(500)," +
                        " @p_HOTELUSERID nvarchar(500)," +
                        " @p_HOTELPASSWORD nvarchar(500)," +
                        " @p_ISAUTOSETTLEMENT bit," +
                        " @p_ISROOMCREDIT bit," +
                        " @p_ISPRINTTOKENNO bit," +
                        " @p_barprinter nvarchar(200)," +
                        " @p_dayendtime bigint," +
                        " @p_maxdisc bigint," +
                        " @p_notreqrndoff bit," +
                        " @p_notreqtaxinparcel bit," +
                        " @p_reqcancelkot bit," +
                        " @p_iskitcashprint bit," +
                        " @p_addeveryitmincashmemo bit," +
                        " @p_manpass nvarchar(50)," +
                        " @p_liqsertax decimal(18,2)," +
                        " @p_banqsertax decimal(18,2)," +
                        " @p_isreqreasoninkot bit," +
                        " @p_isreqreasoninbill bit," +
                        " @p_ischkrepeatitem bit," +
                        " @p_dayavgpur bigint," +
                        " @p_liqvat decimal(18,3)," +
                        " @p_bevervat decimal(18,3)," +
                        " @p_isbillsaveprintmsg bit," +
                        " @p_ALLOWMINUSQTY bit," +
                        " @p_MUSTENTCLSTK bit," +
                        " @p_serchr decimal(18,3)," +
                        " @p_kkcess decimal(18,3)," +
                        " @p_notreqserchrinparcel bit," +
                        " @p_isautoamtcalcinpur bit," +
                        " @p_banqsbcess decimal(18,3)," +
                        " @p_banqkkcess decimal(18,3)," +
                        " @p_notreqtblorddtl bit," +
                        " @p_autocashno nvarchar(50)," +
                        " @p_noofkot2 bigint, " +
                        " @p_iskot2printreq bit, " +
                        " @p_cutdeptkot2 bit, " +
                        " @p_printkot2 nvarchar(200)," +
                        " @p_kit2printer nvarchar(500)," +
                        " @p_thkot2ptype nvarchar(500)," +
                        " @p_iscashmemostickerprint bit, " +
                        " @p_lableprinter nvarchar(500)," +
                        " @p_isitemmnualphaord Bit," +
                        " @p_hukkaprinter nvarchar(500)," +
                        " @p_allowwaitingsms bit," +
                        " @p_noofcashmemobill bigint, " +
                        " @p_firstfocusonitemcode bit," +
                        " @p_banqheader1 nvarchar(500)," +
                        " @p_banqheader2 nvarchar(500)," +
                        " @p_banqheader3 nvarchar(500)," +
                        " @p_banqheader4 nvarchar(500)," +
                        " @p_banqheader5 nvarchar(500)," +
                        " @p_banqfooter1 nvarchar(500)," +
                        " @p_banqfooter2 nvarchar(500)," +
                        " @p_banqfooter3 nvarchar(500)," +
                        " @p_banqfooter4 nvarchar(500)," +
                        " @p_banqfooter5 nvarchar(500)," +
                        " @p_billnobasedon nvarchar(50)," +
                        " @p_rewbillamt bigint," +
                        " @p_rewpoints bigint," +
                        " @p_kotnobasedon nvarchar(50)," +
                        " @p_gsttax1 decimal(18,3)," +
                        " @p_gsttax2 decimal(18,3)," +
                        " @p_gsttax3 decimal(18,3)," +
                        " @p_banqgsttax1 decimal(18,3)," +
                        " @p_banqgsttax2 decimal(18,3)," +
                        " @p_banqgsttax3 decimal(18,3)," +
                        " @p_GENBILLTITLE nvarchar(50)," +
                        " @p_generaltitle6 nvarchar(500)," +
                        " @p_generaltitle7 nvarchar(500)," +
                        " @p_generaltitle8 nvarchar(500)," +
                        " @p_thermaltitle6 nvarchar(30)," +
                        " @p_thermaltitle7 nvarchar(30)," +
                        " @p_thermaltitle8 nvarchar(30)," +
                        " @p_generalfooter6 nvarchar(500)," +
                        " @p_generalfooter7 nvarchar(500)," +
                        " @p_thermalfooter6 nvarchar(500)," +
                        " @p_thermalfooter7 nvarchar(500)," +
                        " @p_banqheader6 nvarchar(500)," +
                        " @p_banqheader7 nvarchar(500)," +
                        " @p_banqheader8 nvarchar(500)," +
                        " @p_banqfooter6 nvarchar(500)," +
                        " @p_banqfooter7 nvarchar(500)," +
                        " @p_banqfooter8 nvarchar(500)," +
                        " @p_istblsecprintreq bit," +
                        " @p_cutseckot bit," +
                        " @p_noofseckot bigint," +
                        " @p_printseckot nvarchar(100)," +
                        " @p_seckottype nvarchar(100)," +
                        " @p_sec1printer nvarchar(1000)," +
                        " @p_sec2printer nvarchar(1000)," +
                        " @p_sec3printer nvarchar(1000)," +
                        " @p_dispimg bit," +
                        " @p_autostartsmsapp bit," +
                        " @p_dispreginm bit," +
                        " @p_kotscreenver nvarchar(50)," +
                        " @p_rewardprovider nvarchar(250)," +
                        " @p_rewarduserid nvarchar(250)," +
                        " @p_rewardpassword nvarchar(250)," +
                        " @p_allowrewardpointapi bit, " +
                        " @p_rewardheadid nvarchar(800)," +
                        " @p_dispmultisettlement bit," +
                        " @p_notallow0stkitemissue bit," +
                        " @p_localservername nvarchar(100)," +
                        " @p_localdbname nvarchar(100)," +
                        " @p_locallogin nvarchar(100)," +
                        " @p_localpassword nvarchar(100)," +
                        " @p_localport nvarchar(100)," +
                        " @p_onlineservername nvarchar(100)," +
                        " @p_onlinedbname nvarchar(100)," +
                        " @p_onlinelogin nvarchar(100)," +
                        " @p_onlinepassword nvarchar(100)," +
                        " @p_onlineport nvarchar(100)," +
                        " @p_userid bigint, " +
                        " @p_errstr as nvarchar(MAX) out, " +
                         " @p_retval as int out " +
                      "   ) as " +
                      "   begin " +
                        " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 " +
                            "	if (@p_mode=0) " +
                                    " begin 	" +
                                        " Insert Into rmssetting (autokotno,autobillno,autosettno,printkot,printbill,thermalprintertype,generaltitle1,generaltitle2,generaltitle3,generaltitle4,generaltitle5,thermaltitle1,thermaltitle2,thermaltitle3,thermaltitle4,thermaltitle5,autopurno,autopayno," +
                                                                " generalfooteralignment,generalfooter1,generalfooter2,generalfooter3,generalfooter4,generalfooter5," +
                                                                " thermalfooteralignment,thermalfooter1,thermalfooter2,thermalfooter3,thermalfooter4,thermalfooter5," +
                                                                " thermalfooterprinttype,sertax,vat,sercharges,disc,autobill,kitprinter,billingprinter,reportprinter, " +
                                                                " printcustinfo,deptkot," +
                                                                " kottitle1,kottitle2,kottitle3,kottitle4,kottitle5,backuploc,cutdeptkot,allowrate," +
                                                                " proprovider,prouserid,propassword,traprovider,trauserid,trapassword,smssign,noofbill," +
                                                                " donotallowbill,hidebusu,hidecommi,thbillptype," +
                                                                " SMTPEMAILADDRESS,SMTPEMAILPASSWORD,SMTPADDRESS,SMTPPORT,noofkot, " +
                                                                " Autotokenno,thkotptype,hidereport,fillsettlementamt,calcvatper," +
                                                                " NOTREQSETTLEMENT,ISCHECKPAPERSIZE,ISLOGOUTPRINT,ISSTOPUSERBILLPRINT,ISFOCUSONITEMNAME,ISKITDISP," +
                                                                " CASHMEMOVER, ISPAXREQBILL,ISKOTSAVEPRINT,SMSRESTNAME,PROSENDERID," +
                                                                " HOTELSERVER,HOTELDATABASE,HOTELUSERID,HOTELPASSWORD,ISAUTOSETTLEMENT,ISROOMCREDIT,ISPRINTTOKENNO," +
                                                                " barprinter,dayendtime,maxdisc,notreqrndoff,notreqtaxinparcel,reqcancelkot," +
                                                                " iskitcashprint,addeveryitmincashmemo,manpass,liqsertax,banqsertax," +
                                                                " isreqreasoninkot, isreqreasoninbill,ischkrepeatitem, dayavgpur,liqvat,bevervat," +
                                                                " isbillsaveprintmsg,ALLOWMINUSQTY,MUSTENTCLSTK,serchr,kkcess,notreqserchrinparcel,isautoamtcalcinpur,banqsbcess,banqkkcess,notreqtblorddtl, " +
                                                                " autocashno,noofkot2,iskot2printreq,cutdeptkot2,printkot2,kit2printer,thkot2ptype," +
                                                                " iscashmemostickerprint,lableprinter,isitemmnualphaord,hukkaprinter,allowwaitingsms," +
                                                                " noofcashmemobill,firstfocusonitemcode," +
                                                                " banqheader1,banqheader2,banqheader3,banqheader4,banqheader5," +
                                                                " banqfooter1,banqfooter2,banqfooter3,banqfooter4,banqfooter5," +
                                                                " billnobasedon,rewbillamt,rewpoints,kotnobasedon, " +
                                                                " gsttax1,gsttax2,gsttax3,banqgsttax1,banqgsttax2,banqgsttax3," +
                                                                " GENBILLTITLE," +
                                                                " generaltitle6,generaltitle7,generaltitle8,thermaltitle6,thermaltitle7,thermaltitle8,generalfooter6,generalfooter7,thermalfooter6,thermalfooter7," +
                                                                " banqheader6,banqheader7,banqheader8,banqfooter6,banqfooter7,banqfooter8," +
                                                                " istblsecprintreq,cutseckot,noofseckot,printseckot,seckottype,sec1printer,sec2printer,sec3printer,dispimg,autostartsmsapp,dispreginm,kotscreenver," +
                                                                " rewardprovider,rewarduserid,rewardpassword,allowrewardpointapi,rewardheadid,dispmultisettlement,notallow0stkitemissue," +
                                                                " localservername,localdbname,locallogin,localpassword,localport,onlineservername,onlinedbname,onlinelogin,onlinepassword,onlineport," +
                                                                " auserid,adatetime,DelFlg) " +
                                        " Values (@p_autokotno,@p_autobillno,@p_autosettno,@p_printkot,@p_printbill,@p_thermalprintertype,@p_generaltitle1,@p_generaltitle2,@p_generaltitle3,@p_generaltitle4,@p_generaltitle5,@p_thermaltitle1,@p_thermaltitle2,@p_thermaltitle3,@p_thermaltitle4,@p_thermaltitle5," +
                                                " @p_autopurno,@p_autopayno," +
                                                " @p_generalfooteralignment,@p_generalfooter1,@p_generalfooter2,@p_generalfooter3,@p_generalfooter4,@p_generalfooter5," +
                                                " @p_thermalfooteralignment,@p_thermalfooter1,@p_thermalfooter2,@p_thermalfooter3,@p_thermalfooter4,@p_thermalfooter5," +
                                                " @p_thermalfooterprinttype,@p_sertax,@p_vat,@p_sercharges,@p_disc,@p_autobill,@p_kitprinter,@p_billingprinter,@p_reportprinter," +
                                                " @p_printcustinfo,@p_deptkot," +
                                                " @p_kottitle1,@p_kottitle2,@p_kottitle3,@p_kottitle4,@p_kottitle5,@p_backuploc,@p_cutdeptkot,@p_allowrate," +
                                                " @p_proprovider,@p_prouserid,@p_propassword,@p_traprovider,@p_trauserid,@p_trapassword,@p_smssign,@p_noofbill," +
                                                " @p_donotallowbill,@p_hidebusu,@p_hidecommi,@p_thbillptype," +
                                                " @p_SMTPEMAILADDRESS,@p_SMTPEMAILPASSWORD,@p_SMTPADDRESS,@p_SMTPPORT,@p_noofkot, @p_autotokenno,@p_thkotptype," +
                                                " @p_hidereport,@p_fillsettlementamt,@p_calcvatper," +
                                                " @p_NOTREQSETTLEMENT,@p_ISCHECKPAPERSIZE,@p_ISLOGOUTPRINT,@p_ISSTOPUSERBILLPRINT,@p_ISFOCUSONITEMNAME,@p_ISKITDISP," +
                                                " @p_CASHMEMOVER, @p_ISPAXREQBILL,@p_ISKOTSAVEPRINT,@p_SMSRESTNAME,@p_PROSENDERID," +
                                                " @p_HOTELSERVER,@p_HOTELDATABASE,@p_HOTELUSERID,@p_HOTELPASSWORD,@p_ISAUTOSETTLEMENT,@p_ISROOMCREDIT,@p_ISPRINTTOKENNO," +
                                                " @p_barprinter,@p_dayendtime,@p_maxdisc,@p_notreqrndoff,@p_notreqtaxinparcel,@p_reqcancelkot," +
                                                " @p_iskitcashprint,@p_addeveryitmincashmemo,@p_manpass,@p_liqsertax,@p_banqsertax, " +
                                                " @p_isreqreasoninkot, @p_isreqreasoninbill,@p_ischkrepeatitem, @p_dayavgpur,@p_liqvat,@p_bevervat," +
                                                " @p_isbillsaveprintmsg ,@p_ALLOWMINUSQTY,@p_MUSTENTCLSTK,@p_serchr,@p_kkcess,@p_notreqserchrinparcel,@p_isautoamtcalcinpur,@p_banqsbcess,@p_banqkkcess,@p_notreqtblorddtl, " +
                                                " @p_autocashno,@p_noofkot2,@p_iskot2printreq,@p_cutdeptkot2,@p_printkot2,@p_kit2printer,@p_thkot2ptype," +
                                                " @p_iscashmemostickerprint,@p_lableprinter,@p_isitemmnualphaord,@p_hukkaprinter,@p_allowwaitingsms," +
                                                " @p_noofcashmemobill,@p_firstfocusonitemcode, " +
                                                " @p_banqheader1,@p_banqheader2,@p_banqheader3,@p_banqheader4,@p_banqheader5," +
                                                " @p_banqfooter1,@p_banqfooter2,@p_banqfooter3,@p_banqfooter4,@p_banqfooter5," +
                                                " @p_billnobasedon,@p_rewbillamt,@p_rewpoints,@p_kotnobasedon, " +
                                                " @p_gsttax1,@p_gsttax2,@p_gsttax3,@p_banqgsttax1,@p_banqgsttax2,@p_banqgsttax3," +
                                                " @p_GENBILLTITLE," +
                                                " @p_generaltitle6,@p_generaltitle7,@p_generaltitle8,@p_thermaltitle6,@p_thermaltitle7,@p_thermaltitle8,@p_generalfooter6,@p_generalfooter7,@p_thermalfooter6,@p_thermalfooter7," +
                                                " @p_banqheader6,@p_banqheader7,@p_banqheader8,@p_banqfooter6,@p_banqfooter7,@p_banqfooter8," +
                                                " @p_istblsecprintreq,@p_cutseckot,@p_noofseckot,@p_printseckot,@p_seckottype,@p_sec1printer,@p_sec2printer,@p_sec3printer,@p_dispimg,@p_autostartsmsapp,@p_dispreginm,@p_kotscreenver," +
                                                " @p_rewardprovider,@p_rewarduserid,@p_rewardpassword,@p_allowrewardpointapi,@p_rewardheadid,@p_dispmultisettlement,@p_notallow0stkitemissue," +
                                                " @p_localservername,@p_localdbname,@p_locallogin,@p_localpassword,@p_localport,@p_onlineservername,@p_onlinedbname,@p_onlinelogin,@p_onlinepassword,@p_onlineport," +
                                                " @p_userid,getdate(),0)" +
                                        " End    " +
                                "  else if (@p_mode=1)     " +
                                    " begin" +
                                    " set @p_Errstr=''  set @p_Retval=0 " +
                                     " update rmssetting set autokotno= @p_autokotno,autobillno=@p_autobillno,autosettno=@p_autosettno," +
                                                            "printkot = @p_printkot,printbill=@p_printbill,thermalprintertype=@p_thermalprintertype," +
                                                           " generaltitle1 = @p_generaltitle1, generaltitle2 = @p_generaltitle2, generaltitle3 = @p_generaltitle3, generaltitle4 = @p_generaltitle4, generaltitle5 = @p_generaltitle5," +
                                                            "thermaltitle1=@p_thermaltitle1,thermaltitle2=@p_thermaltitle2,thermaltitle3=@p_thermaltitle3,thermaltitle4=@p_thermaltitle4,thermaltitle5=@p_thermaltitle5," +
                                                            "generalfooteralignment = @p_generalfooteralignment,generalfooter1=@p_generalfooter1,generalfooter2=@p_generalfooter2,generalfooter3=@p_generalfooter3,generalfooter4=@p_generalfooter4,generalfooter5=@p_generalfooter5," +
                                                            "thermalfooteralignment=@p_thermalfooteralignment,thermalfooter1=@p_thermalfooter1,thermalfooter2=@p_thermalfooter2,thermalfooter3=@p_thermalfooter3,thermalfooter4=@p_thermalfooter4,thermalfooter5=@p_thermalfooter5," +
                                                            "autopurno=@p_autopurno,autopayno=@p_autopayno,thermalfooterprinttype=@p_thermalfooterprinttype," +
                                                            "sertax=@p_sertax,vat=@p_vat,sercharges=@p_sercharges,disc=@p_disc,autobill=@p_autobill," +
                                                            "kitprinter=@p_kitprinter,billingprinter=@p_billingprinter,reportprinter=@p_reportprinter," +
                                                            "printcustinfo = @p_printcustinfo,deptkot = @p_deptkot," +
                                                            "kottitle1=@p_kottitle1,kottitle2=@p_kottitle2,kottitle3=@p_kottitle3,kottitle4=@p_kottitle4,kottitle5=@p_kottitle5,backuploc=@p_backuploc," +
                                                            "cutdeptkot=@p_cutdeptkot,allowrate=@p_allowrate," +
                                                            "proprovider=@p_proprovider,prouserid=@p_prouserid,propassword=@p_propassword," +
                                                            "traprovider=@p_traprovider,trauserid=@p_trauserid,trapassword=@p_trapassword," +
                                                            "smssign=@p_smssign,noofbill=@p_noofbill," +
                                                            " donotallowbill=@p_donotallowbill,hidebusu=@p_hidebusu,hidecommi=@p_hidecommi,thbillptype=@p_thbillptype," +
                                                            " SMTPEMAILADDRESS=@p_SMTPEMAILADDRESS,SMTPEMAILPASSWORD=@p_SMTPEMAILPASSWORD,SMTPADDRESS=@p_SMTPADDRESS,SMTPPORT=@p_SMTPPORT, " +
                                                            " noofkot=@p_noofkot,Autotokenno=@p_Autotokenno," +
                                                            " thkotptype=@p_thkotptype,hidereport=@p_hidereport," +
                                                            " fillsettlementamt=@p_fillsettlementamt,calcvatper=@p_calcvatper," +
                                                            " NOTREQSETTLEMENT = @p_NOTREQSETTLEMENT,ISCHECKPAPERSIZE=@p_ISCHECKPAPERSIZE," +
                                                            " ISLOGOUTPRINT=@p_ISLOGOUTPRINT,ISSTOPUSERBILLPRINT=@p_ISSTOPUSERBILLPRINT," +
                                                            " ISFOCUSONITEMNAME=@p_ISFOCUSONITEMNAME,ISKITDISP=@p_ISKITDISP," +
                                                            " CASHMEMOVER=@p_CASHMEMOVER,ISPAXREQBILL=@p_ISPAXREQBILL,ISKOTSAVEPRINT=@p_ISKOTSAVEPRINT," +
                                                            " SMSRESTNAME=@p_SMSRESTNAME,PROSENDERID=@p_PROSENDERID," +
                                                            " HOTELSERVER=@p_HOTELSERVER,HOTELDATABASE=@p_HOTELDATABASE,HOTELUSERID=@p_HOTELUSERID,HOTELPASSWORD=@p_HOTELPASSWORD," +
                                                            " ISAUTOSETTLEMENT=@p_ISAUTOSETTLEMENT,ISROOMCREDIT=@p_ISROOMCREDIT,ISPRINTTOKENNO=@p_ISPRINTTOKENNO," +
                                                            " barprinter = @p_barprinter, dayendtime=@p_dayendtime,maxdisc=@p_maxdisc, notreqrndoff=@p_notreqrndoff, " +
                                                            " notreqtaxinparcel = @p_notreqtaxinparcel,reqcancelkot = @p_reqcancelkot," +
                                                            " iskitcashprint = @p_iskitcashprint,addeveryitmincashmemo=@p_addeveryitmincashmemo,manpass=@p_manpass, " +
                                                            " liqsertax=@p_liqsertax,banqsertax=@p_banqsertax," +
                                                            " isreqreasoninkot=@p_isreqreasoninkot, isreqreasoninbill=@p_isreqreasoninbill,ischkrepeatitem=@p_ischkrepeatitem," +
                                                            " dayavgpur=@p_dayavgpur,liqvat=@p_liqvat,bevervat=@p_bevervat," +
                                                            " isbillsaveprintmsg=@p_isbillsaveprintmsg,ALLOWMINUSQTY=@p_ALLOWMINUSQTY,MUSTENTCLSTK=@p_MUSTENTCLSTK," +
                                                            " serchr=@p_serchr,kkcess=@p_kkcess,notreqserchrinparcel=@p_notreqserchrinparcel,isautoamtcalcinpur=@p_isautoamtcalcinpur,banqsbcess=@p_banqsbcess,banqkkcess=@p_banqkkcess," +
                                                            " notreqtblorddtl=@p_notreqtblorddtl,autocashno=@p_autocashno," +
                                                            " noofkot2=@p_noofkot2,iskot2printreq=@p_iskot2printreq,cutdeptkot2=@p_cutdeptkot2,printkot2=@p_printkot2,kit2printer=@p_kit2printer,thkot2ptype=@p_thkot2ptype," +
                                                            " iscashmemostickerprint=@p_iscashmemostickerprint,lableprinter=@p_lableprinter,isitemmnualphaord=@p_isitemmnualphaord," +
                                                            " hukkaprinter=@p_hukkaprinter,allowwaitingsms=@p_allowwaitingsms,noofcashmemobill=@p_noofcashmemobill," +
                                                            " firstfocusonitemcode=@p_firstfocusonitemcode," +
                                                            " banqheader1=@p_banqheader1,banqheader2=@p_banqheader2,banqheader3=@p_banqheader3,banqheader4=@p_banqheader4,banqheader5=@p_banqheader5," +
                                                            " banqfooter1=@p_banqfooter1,banqfooter2=@p_banqfooter2,banqfooter3=@p_banqfooter3,banqfooter4=@p_banqfooter4,banqfooter5=@p_banqfooter5," +
                                                            " billnobasedon = @p_billnobasedon,rewbillamt=@p_rewbillamt,rewpoints=@p_rewpoints,kotnobasedon=@p_kotnobasedon, " +
                                                            " gsttax1=@p_gsttax1,gsttax2=@p_gsttax2,gsttax3=@p_gsttax3,banqgsttax1=@p_banqgsttax1,banqgsttax2=@p_banqgsttax2,banqgsttax3=@p_banqgsttax3," +
                                                            " GENBILLTITLE=@p_GENBILLTITLE," +
                                                            " generaltitle6=@p_generaltitle6,generaltitle7=@p_generaltitle7,generaltitle8=@p_generaltitle8,thermaltitle6=@p_thermaltitle6,thermaltitle7=@p_thermaltitle7,thermaltitle8=@p_thermaltitle8,generalfooter6=@p_generalfooter6,generalfooter7=@p_generalfooter7,thermalfooter6=@p_thermalfooter6,thermalfooter7=@p_thermalfooter7," +
                                                            " banqheader6=@p_banqheader6,banqheader7=@p_banqheader7,banqheader8=@p_banqheader8,banqfooter6=@p_banqfooter6,banqfooter7=@p_banqfooter7,banqfooter8=@p_banqfooter8," +
                                                            " istblsecprintreq=@p_istblsecprintreq,cutseckot=@p_cutseckot,noofseckot=@p_noofseckot,printseckot=@p_printseckot,seckottype=@p_seckottype,sec1printer=@p_sec1printer,sec2printer=@p_sec2printer,sec3printer=@p_sec3printer,dispimg=@p_dispimg,autostartsmsapp=@p_autostartsmsapp,dispreginm=@p_dispreginm,kotscreenver=@p_kotscreenver," +
                                                            " rewardprovider=@p_rewardprovider,rewarduserid=@p_rewarduserid,rewardpassword=@p_rewardpassword,allowrewardpointapi=@p_allowrewardpointapi,rewardheadid=@p_rewardheadid,dispmultisettlement=@p_dispmultisettlement,notallow0stkitemissue=@p_notallow0stkitemissue, " +
                                                            " localservername=@p_localservername,localdbname=@p_localdbname,locallogin=@p_locallogin,localpassword=@p_localpassword,localport=@p_localport,onlineservername=@p_onlineservername,onlinedbname=@p_onlinedbname,onlinelogin=@p_onlinelogin,onlinepassword=@p_onlinepassword,onlineport=@p_onlineport," +
                                                            " euserid = @p_userid,edatetime = getdate()" +
                                                     " End " +
                                                     " End" +
                                                    " End	" +
                                                    " try  " +
                                                " begin catch    " +
                                                 " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; " +
                                                 " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()" +
                                                 " Return  " +
                                                 " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTUNIT");
                strproc = " CREATE procedure sp_MSTUNIT" +
                                    " (" +
                                    " @p_mode as int," +
                                    " @p_rid bigint," +
                                    " @p_unitcode nvarchar(20)," +
                                    " @p_unitname nvarchar(100)," +
                                    " @p_unitdesc nvarchar(max)," +
                                    " @p_userid bigint," +
                                    " @p_errstr as nvarchar(max) out," +
                                    " @p_retval as int out," +
                                    " @p_id bigint out" +
                                    " ) as" +
                                    " begin" +
                                    " try" +
                                    " begin" +
                                    " if (@p_mode=0)" +
                                     " begin 	     " +
                                            " declare @codeRowCount as int " +
                                            " set @p_Errstr=''  set @p_Retval=0  set @p_id=0  " +
                                            " select @codeRowCount = (select count(*) from mstunit where unitcode = @p_unitcode and ISNULL(DelFlg,0)=0)    " +
                                            " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Unit Code Already exits.'    " +
                                                " Return   " +
                                            " End	   " +
                                      " Begin " +
                                        "  declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0   set @p_id=0   " +
                                        "  Select  @nameRowCount = (select count(*) from mstunit where unitname = @p_unitname and ISNULL(DelFlg,0)=0)    " +
                                         " if ( @nameRowCount > 0)    " +
                                         " begin   " +
                                         " set @p_Retval = 1 set @p_Errstr ='Unit Name Already exits.'   " +
                                         " Return " +
                                          " End  " +
                                        " End " +
                                            " Begin" +
                                            " Insert Into mstunit (unitcode,unitname,unitdesc,SENDDATA,auserid,adatetime,DelFlg)" +
                                            " Values (@p_unitcode,@p_unitname,@p_unitdesc,0,@p_userid,getdate(),0)  " +
                                            " set @p_id=SCOPE_IDENTITY()   " +
                                            " End  End " +
                                        " Else if (@p_mode=1) " +
                                            "  begin   " +
                                            "  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0   set @p_id=0   " +
                                            "  select @codeRowCount1 = (select count(*) from mstunit where unitcode = @p_unitcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )    " +
                                            "  if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Unit Code Already exits.'     " +
                                             " Return   End " +
                                     " Begin " +
                                       "   declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0   set @p_id=0  " +
                                        "  select  @nameRowCount1 = (select count(*) from mstunit where unitname = @p_unitname and rid <> @p_rid and ISNULL(DelFlg,0)=0)    " +
                                        "  if ( @nameRowCount1 > 0)  " +
                                         " begin   " +
                                         " set @p_Retval = 1 set @p_Errstr ='Unit Name Already exits.'   " +
                                         " Return " +
                                      " End  END" +
                                     " begin " +
                                             " Update mstunit set unitcode=@p_unitcode," +
                                             " unitname = @p_unitname, unitdesc = @p_unitdesc, SENDDATA=0," +
                                             " euserid = @p_userid,edatetime = getdate()   " +
                                             " where rid = @p_rid    " +
                                             " End  End  End  End" +
                                             " try  begin catch   " +
                                             " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;    " +
                                               "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()   " +
                                            " Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                //---
                //                

                this.DeleteProcedureFromOnlineDb("sp_BILL");
                strproc = "";
                strproc = " Create procedure sp_BILL " +
                        " ( " +
                         " @p_mode as int, " +
                         " @p_rid as bigint," +
                         " @p_billno nvarchar(50)," +
                         " @p_billdate datetime,		" +
                         " @p_billtype nvarchar(50)," +
                         " @p_custrid bigint," +
                         " @p_custname nvarchar(250)," +
                         " @p_custcontno nvarchar(50)," +
                         " @p_tablerid bigint," +
                         " @p_pricelistrid bigint," +
                         " @p_billpax bigint," +
                         " @p_totamount decimal(18,2)," +
                         " @p_totdiscountableamount decimal(18,2)," +
                         " @p_totsertaxper decimal(18,2)," +
                         " @p_totsertaxamount decimal(18,2)," +
                         " @p_totvatper decimal(18,2)," +
                         " @p_totvatamount decimal(18,2)," +
                         " @p_totaddvatper decimal(18,2)," +
                         " @p_totaddvatamount decimal(18,2)," +
                         " @p_totdiscper decimal(18,2)," +
                         " @p_totdiscamount decimal(18,2)," +
                         " @P_totroff decimal(18,2)," +
                         " @p_netamount decimal(18,2)," +
                         " @p_billprepby nvarchar(100)," +
                         " @p_billremark nvarchar(max)," +
                         " @p_setletype nvarchar(50)," +
                         " @p_setleamount decimal(18,2)," +
                         " @p_chequeno nvarchar(100)," +
                         " @p_chequebankname nvarchar(100)," +
                         " @p_creditcardno nvarchar(100)," +
                         " @p_creditholdername nvarchar(100)," +
                         " @p_creditbankname nvarchar(100)," +
                         " @p_refbillno nvarchar(50)," +
                         " @p_refbillnum bigint," +
                         " @p_custadd nvarchar(max)," +
                         " @p_billtime datetime," +
                         " @p_billorderperid bigint," +
                         " @p_tokenno bigint," +
                         " @p_totcalcvatper decimal(18,2)," +
                         " @p_totcalcvatamount decimal(18,2)," +
                         " @p_isbilltocustomer bit," +
                         " @p_cntprint bigint," +
                         " @p_isparcelbill bit," +
                         " @p_totbevvatper decimal(18,3)," +
                         " @p_totbevvatamt decimal(18,2)," +
                         " @p_totliqvatper decimal(18,3)," +
                         " @p_totliqvatamt decimal(18,2)," +
                         " @p_refbyrid bigint," +
                         " @p_totserchrper decimal(18,3)," +
                         " @p_totserchramt decimal(18,3)," +
                         " @p_totnewtotalamt decimal(18,3)," +
                         " @p_totadddiscamt decimal(18,3)," +
                         " @p_msttieupcomprid bigint," +
                         " @p_couponno nvarchar(200)," +
                         " @p_couponpername nvarchar(200)," +
                         " @p_totkkcessper decimal(18,3)," +
                         " @p_totkkcessamt decimal(18,3)," +
                         " @p_otherpaymentby nvarchar(200)," +
                         " @p_otherpaymentbyremark1 nvarchar(500)," +
                         " @p_otherpaymentbyremark2 nvarchar(500)," +
                         " @p_payment nvarchar(50)," +
                         " @p_multicashamt decimal(18,3)," +
                         " @p_multichequeamt decimal(18,3)," +
                         " @p_multicreditcardamt decimal(18,3)," +
                         " @p_multiotheramt decimal(18,3)," +
                         " @p_multichqno nvarchar(200)," +
                         " @p_multichqbankname nvarchar(200)," +
                         " @p_multicardno nvarchar(200)," +
                         " @p_multicardbankname nvarchar(200)," +
                         " @p_multiotherpaymentby nvarchar(200)," +
                         " @p_multiotherremark1 nvarchar(200)," +
                         " @p_multiotherremark2 nvarchar(200)," +
                         " @p_multitipamt decimal(18,3)," +
                         " @p_cardno nvarchar(50)," +
                         " @p_totbillrewpoint decimal(18,3)," +
                         " @p_totitemrewpoint decimal(18,3)," +
                         " @p_cgstamt decimal(18,3)," +
                         " @p_sgstamt decimal(18,3)," +
                         " @p_igstamt decimal(18,3)," +
                         " @p_totgstamt decimal(18,3)," +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         " begin " +
                         " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                " if (@p_mode=0) " +
                                    " begin 		" +
                                        " Insert Into bill (billno,billdate,billtype,custrid,custname,custcontno,tablerid,pricelistrid,billpax," +
                                                          "totamount,totdiscountableamount,totsertaxper,totsertaxamount,totvatper,totvatamount,totaddvatper,totaddvatamount," +
                                                          "totdiscper,totdiscamount,totroff,netamount,billprepby,billremark," +
                                                          "setletype,setleamount,chequeno,chequebankname,creditcardno,creditholdername,creditbankname," +
                                                          "refbillno,refbillnum,custadd,billtime,billorderperid,tokenno," +
                                                          "totcalcvatper,totcalcvatamount,isbilltocustomer,cntprint,isparcelbill," +
                                                          "totbevvatper,totbevvatamt,totliqvatper,totliqvatamt,refbyrid, " +
                                                          "totserchrper,totserchramt,totnewtotalamt,totadddiscamt,msttieupcomprid,couponno,couponpername, " +
                                                          " totkkcessper,totkkcessamt," +
                                                          " otherpaymentby,otherpaymentbyremark1,otherpaymentbyremark2," +
                                                          " payment,multicashamt,multichequeamt,multicreditcardamt,multiotheramt,multichqno,multichqbankname,multicardno,multicardbankname,multiotherpaymentby,multiotherremark1,multiotherremark2,multitipamt," +
                                                          " cardno,totbillrewpoint,totitemrewpoint,cgstamt,sgstamt,igstamt,totgstamt," +
                                                          " senddata,auserid,adatetime,DelFlg) " +
                                                  " Values (" +
                                                          " @p_billno,@p_billdate,@p_billtype,@p_custrid,@p_custname,@p_custcontno,@p_tablerid,@p_pricelistrid,@p_billpax," +
                                                          " @p_totamount,@p_totdiscountableamount,@p_totsertaxper,@p_totsertaxamount,@p_totvatper,@p_totvatamount,@p_totaddvatper,@p_totaddvatamount," +
                                                          " @p_totdiscper,@p_totdiscamount,@p_totroff,@p_netamount,@p_billprepby,@p_billremark," +
                                                          " @p_setletype,@p_setleamount,@p_chequeno,@p_chequebankname, @p_creditcardno,@p_creditholdername,@p_creditbankname," +
                                                          " @p_refbillno,@p_refbillnum,@p_custadd,@p_billtime,@p_billorderperid,@p_tokenno," +
                                                          " @p_totcalcvatper,@p_totcalcvatamount,@p_isbilltocustomer,@p_cntprint,@p_isparcelbill," +
                                                          " @p_totbevvatper,@p_totbevvatamt,@p_totliqvatper,@p_totliqvatamt,@p_refbyrid, " +
                                                          " @p_totserchrper,@p_totserchramt,@p_totnewtotalamt,@p_totadddiscamt, " +
                                                          " @p_msttieupcomprid,@p_couponno,@p_couponpername, " +
                                                          " @p_totkkcessper,@p_totkkcessamt," +
                                                          " @p_otherpaymentby,@p_otherpaymentbyremark1,@p_otherpaymentbyremark2," +
                                                          " @p_payment,@p_multicashamt,@p_multichequeamt,@p_multicreditcardamt,@p_multiotheramt,@p_multichqno,@p_multichqbankname,@p_multicardno,@p_multicardbankname,@p_multiotherpaymentby,@p_multiotherremark1,@p_multiotherremark2,@p_multitipamt," +
                                                          " @p_cardno,@p_totbillrewpoint,@p_totitemrewpoint,@p_cgstamt,@p_sgstamt,@p_igstamt,@p_totgstamt," +
                                                          " 0,@p_userid,getdate(),0" +
                                                          " )" +
                                        " set @p_id=SCOPE_IDENTITY()" +
                                        " End    " +
                                 " else if (@p_mode=1)  " +
                                    " begin" +
                                       " set @p_Errstr=''  set @p_Retval=0 set @p_id=0" +
                                         " Update Bill " +
                                        " set billno=@p_billno,billdate=@p_billdate,billtype=@p_billtype,custrid=@p_custrid,custname=@p_custname,custcontno=@p_custcontno,tablerid=@p_tablerid,pricelistrid=@p_pricelistrid,billpax=@p_billpax," +
                                                          " totamount=@p_totamount,totdiscountableamount=@p_totdiscountableamount,totsertaxper=@p_totsertaxper,totsertaxamount=@p_totsertaxamount,totvatper=@p_totvatper,totvatamount=@p_totvatamount,totaddvatper=@p_totaddvatper,totaddvatamount=@p_totaddvatamount," +
                                                          " totdiscper=@p_totdiscper,totdiscamount=@p_totdiscamount,totroff=@p_totroff,netamount=@p_netamount,billprepby=@p_billprepby,billremark=@p_billremark," +
                                                           " setletype=@p_setletype,setleamount=@p_setleamount,chequeno=@p_chequeno,chequebankname=@p_chequebankname," +
                                                           " creditcardno=@p_creditcardno,creditholdername=@p_creditholdername,creditbankname=@p_creditbankname," +
                                                           " refbillno=@p_refbillno,refbillnum=@p_refbillnum,custadd=@p_custadd,billtime=@p_billtime,billorderperid=@p_billorderperid," +
                                                           " tokenno=@p_tokenno,totcalcvatper=@p_totcalcvatper,totcalcvatamount=@p_totcalcvatamount," +
                                                           " isbilltocustomer=@p_isbilltocustomer,cntprint=@p_cntprint,isparcelbill=@p_isparcelbill," +
                                                           " totbevvatper=@p_totbevvatper,totbevvatamt=@p_totbevvatamt,totliqvatper=@p_totliqvatper,totliqvatamt=@p_totliqvatamt, " +
                                                           " refbyrid=@p_refbyrid,totserchrper=@p_totserchrper,totserchramt=@p_totserchramt,totnewtotalamt=@p_totnewtotalamt,totadddiscamt=@p_totadddiscamt, " +
                                                           " msttieupcomprid=@p_msttieupcomprid,couponno=@p_couponno,couponpername=@p_couponpername, " +
                                                           " totkkcessper=@p_totkkcessper,totkkcessamt=@p_totkkcessamt," +
                                                           " otherpaymentby=@p_otherpaymentby,otherpaymentbyremark1=@p_otherpaymentbyremark1,otherpaymentbyremark2=@p_otherpaymentbyremark2," +
                                                           " payment=@p_payment,multicashamt=@p_multicashamt,multichequeamt=@p_multichequeamt,multicreditcardamt=@p_multicreditcardamt,multiotheramt=@p_multiotheramt,multichqno=@p_multichqno,multichqbankname=@p_multichqbankname,multicardno=@p_multicardno,multicardbankname=@p_multicardbankname,multiotherpaymentby=@p_multiotherpaymentby,multiotherremark1=@p_multiotherremark1,multiotherremark2=@p_multiotherremark2,multitipamt=@p_multitipamt," +
                                                           " cardno=@p_cardno,totbillrewpoint=@p_totbillrewpoint,totitemrewpoint=@p_totitemrewpoint," +
                                                           " cgstamt=@p_cgstamt,sgstamt=@p_sgstamt,igstamt=@p_igstamt,totgstamt=@p_totgstamt," +
                                                           " senddata=0,euserid = @p_userid,edatetime = getdate()  " +
                                                           " where rid = @p_rid  " +
                                                         " End " +
                                                      " End" +
                                                    " end	" +
                                                    " try  " +
                                                        " begin catch    " +
                                                          " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                                          " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                                          " Return  " +
                                                          " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                //---
                //
                this.DeleteProcedureFromOnlineDb("sp_MSTUSERS");
                strproc = "";
                strproc = " create procedure sp_MSTUSERS  " +
                             " (  " +
                            " @p_mode as int, " +
                            " @p_rid bigint, " +
                            " @p_usercode nvarchar(50),  " +
                            " @p_username nvarchar(50), " +
                            " @p_password nvarchar(50), " +
                            " @p_repassword nvarchar(50), " +
                            " @p_userdesc nvarchar(max), " +
                            " @p_isshowreport bit," +
                            " @p_hidemstmnu bit," +
                            " @p_hidebanqmnu bit," +
                            " @p_hidecrmmnu bit," +
                            " @p_hidesmsmnu bit," +
                            " @p_hidepayrollmnu bit," +
                            " @p_hideutilitymnu bit," +
                            " @p_hidekotbillent bit," +
                            " @p_hidecashmenoent bit," +
                            " @p_hidesettlementent bit," +
                            " @p_hidequickbillent bit," +
                            " @p_hidetableviewent bit," +
                            " @p_hidemultisettlementent bit," +
                            " @p_hidecashonhandent bit," +
                            " @p_hidesupplierent bit," +
                            " @p_hidepurchaseent bit," +
                            " @p_hidepaymenyent bit," +
                            " @p_hidechecklistent bit," +
                            " @p_hidebusinesssummary bit," +
                            " @p_dontallowbilledit bit," +
                            " @p_dontallowbilldelete bit," +
                            " @p_dontallowtblclear bit," +
                            " @p_hidestkissueent bit," +
                            " @p_dontallowbanqboedit bit," +
                            " @p_dontallowbanqbodelete bit," +
                            " @p_dontallowchklistedit bit," +
                            " @p_dontallowchklistdelete bit," +
                            " @p_dontallowpurchaseedit bit," +
                            " @p_dontallowpurchasedelete bit," +
                            " @p_dontallowstkissueedit bit," +
                            " @p_dontallowstkissuedelete bit," +
                            " @p_hidepuritemgroupent bit," +
                            " @p_hidepuritement bit," +
                            " @p_dontallowchangedateinkotbill bit," +
                            " @p_hidepurchaseico bit," +
                            " @p_istabletuser bit," +
                            " @p_dontallowkotedit bit," +
                            " @p_dontallowkotdelete bit," +
                            " @P_dontallowdiscinbill bit," +
                            " @p_hideinvmnu bit," +
                            " @p_userid bigint, " +
                            " @p_errstr as nvarchar(max) out, " +
                            " @p_retval as int out, " +
                            " @p_id bigint out " +
                            " ) as " +
                            " begin " +
                            " try  " +
                            " begin  " +
                            " if (@p_mode=0) " +
                             " begin 	   " +
                                    " declare @codeRowCount as int  " +
                                    " set @p_Errstr=''  set @p_Retval=0  set @p_id=0   " +
                                    " select @codeRowCount = (select count(*) from MSTUSERS where USERCODE = @p_usercode and ISNULL(DelFlg,0)=0)    " +
                                    " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'User Code Already exits.'    " +
                                        " Return    " +
                                    " End	    " +
                              " Begin  " +
                                 " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0   set @p_id=0   " +
                                 " select  @nameRowCount = (select count(*) from MSTUSERS where Username = @p_username and ISNULL(DelFlg,0)=0)    " +
                                 " if ( @nameRowCount > 0)     " +
                                 " begin    " +
                                 " set @p_Retval = 1 set @p_Errstr ='User Name Already exits.'    " +
                                 " Return  " +
                              " End   " +
                            " end " +
                                        " begin " +
                                         " Insert Into MSTUSERS (USERCODE,USERNAME,PASSWORD,REPASSWORD,USERDESC,isshowreport, " +
                                         " hidemstmnu,hidebanqmnu,hidecrmmnu,hidesmsmnu,hidepayrollmnu, " +
                                         " hideutilitymnu,hidekotbillent,hidecashmenoent,hidesettlementent,hidequickbillent, " +
                                         " hidetableviewent,hidemultisettlementent,hidecashonhandent,hidesupplierent,hidepurchaseent,hidepaymenyent,hidechecklistent,hidebusinesssummary, " +
                                         " dontallowbilledit,dontallowbilldelete,dontallowtblclear," +
                                         " hidestkissueent,dontallowbanqboedit,dontallowbanqbodelete,dontallowchklistedit,dontallowchklistdelete," +
                                         " dontallowpurchaseedit,dontallowpurchasedelete,dontallowstkissueedit,dontallowstkissuedelete," +
                                         " hidepuritemgroupent,hidepuritement,dontallowchangedateinkotbill,hidepurchaseico,istabletuser,dontallowkotedit,dontallowkotdelete,dontallowdiscinbill," +
                                         " hideinvmnu,senddata, " +
                                         " auserid,adatetime,DelFlg) " +
                                      " Values (@p_USERCODE,@p_USERNAME,@p_PASSWORD,@p_REPASSWORD,@p_USERDESC,@p_isshowreport, " +
                                         " @p_hidemstmnu,@p_hidebanqmnu,@p_hidecrmmnu,@p_hidesmsmnu,@p_hidepayrollmnu, " +
                                         " @p_hideutilitymnu,@p_hidekotbillent,@p_hidecashmenoent,@p_hidesettlementent,@p_hidequickbillent, " +
                                         " @p_hidetableviewent,@p_hidemultisettlementent,@p_hidecashonhandent,@p_hidesupplierent,@p_hidepurchaseent,@p_hidepaymenyent,@p_hidechecklistent,@p_hidebusinesssummary, " +
                                         " @p_dontallowbilledit,@p_dontallowbilldelete,@p_dontallowtblclear," +
                                         " @p_hidestkissueent,@p_dontallowbanqboedit,@p_dontallowbanqbodelete,@p_dontallowchklistedit,@p_dontallowchklistdelete," +
                                         " @p_dontallowpurchaseedit,@p_dontallowpurchasedelete,@p_dontallowstkissueedit,@p_dontallowstkissuedelete," +
                                         " @p_hidepuritemgroupent,@p_hidepuritement,@p_dontallowchangedateinkotbill,@p_hidepurchaseico,@p_istabletuser,@p_dontallowkotedit,@p_dontallowkotdelete,@p_dontallowdiscinbill," +
                                         " @p_hideinvmnu,0," +
                                         " @p_userid,getdate(),0) " +
                                         " set @p_id=SCOPE_IDENTITY()    " +
                                         " End  End  " +
                                " else if (@p_mode=1)  " +
                                    " begin    " +
                                     " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0   set @p_id=0    " +
                                     " select @codeRowCount1 = (select count(*) from MSTUSERS where usercode = @p_usercode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )     " +
                                     " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'User Code Already exits.'      " +
                                     " Return   End  " +
                             " Begin  " +
                                " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0   set @p_id=0   " +
                                " select  @nameRowCount1 = (select count(*) from MSTUSERS where username = @p_username and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                                 " if ( @nameRowCount1 > 0)     " +
                                 " begin    " +
                                 " set @p_Retval = 1 set @p_Errstr ='User Name Already exits.'    " +
                                 " Return  " +
                              " End  END  " +
                             " begin  " +
                                  "  UPDATE MSTUSERS SET  " +
                                    " Usercode = @p_Usercode,USERNAME = @p_USERNAME, PASSWORD = @p_PASSWORD,REPASSWORD = @p_REPASSWORD,USERDESC = @p_USERDESC, " +
                                    " isshowreport = @p_isshowreport," +
                                     " hidemstmnu=@p_hidemstmnu,hidebanqmnu=@p_hidebanqmnu,hidecrmmnu=@p_hidecrmmnu,hidesmsmnu=@p_hidesmsmnu,hidepayrollmnu=@p_hidepayrollmnu, " +
                                     " hideutilitymnu=@p_hideutilitymnu,hidekotbillent=@p_hidekotbillent,hidecashmenoent=@p_hidecashmenoent,hidesettlementent=@p_hidesettlementent,hidequickbillent=@p_hidequickbillent, " +
                                     " hidetableviewent=@p_hidetableviewent,hidemultisettlementent=@p_hidemultisettlementent,hidecashonhandent=@p_hidecashonhandent,hidesupplierent=@p_hidesupplierent,hidepurchaseent=@p_hidepurchaseent,hidepaymenyent=@p_hidepaymenyent, " +
                                     " hidechecklistent=@p_hidechecklistent,hidebusinesssummary=@p_hidebusinesssummary, " +
                                     " dontallowbilledit=@p_dontallowbilledit,dontallowbilldelete=@p_dontallowbilldelete,dontallowtblclear=@p_dontallowtblclear, " +
                                     " hidestkissueent=@p_hidestkissueent,dontallowbanqboedit=@p_dontallowbanqboedit,dontallowbanqbodelete=@p_dontallowbanqbodelete,dontallowchklistedit=@p_dontallowchklistedit,dontallowchklistdelete=@p_dontallowchklistdelete," +
                                     " dontallowpurchaseedit=@p_dontallowpurchaseedit,dontallowpurchasedelete=@p_dontallowpurchasedelete,dontallowstkissueedit=@p_dontallowstkissueedit,dontallowstkissuedelete=@p_dontallowstkissuedelete," +
                                     " hidepuritemgroupent=@p_hidepuritemgroupent,hidepuritement=@p_hidepuritement,dontallowchangedateinkotbill=@p_dontallowchangedateinkotbill," +
                                     " hidepurchaseico=@p_hidepurchaseico,istabletuser=@p_istabletuser,dontallowkotedit=@p_dontallowkotedit,dontallowkotdelete=@p_dontallowkotdelete," +
                                     " dontallowdiscinbill=@p_dontallowdiscinbill,hideinvmnu=@p_hideinvmnu,senddata=0," +
                                     " euserid = @p_userid,edatetime = getdate() " +
                                     " WHERE rid = @p_rid  " +
                                     " End  End  End  End  " +
                                     " try  begin catch    " +
                                     " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()    " +
                                     " Return  END CATCH  ";

                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                // ---
                //
                this.DeleteProcedureFromOnlineDb("sp_MSTCUST");
                strproc = "";
                strproc = "Create procedure sp_MSTCUST  " +
                        " (  " +
                        " @p_mode as int, " +
                        " @p_rid bigint, " +
                        " @p_custcode nvarchar(50),  " +
                        " @p_custname nvarchar(250),	 " +
                        " @p_custadd1 nvarchar(200), " +
                        " @p_custadd2 nvarchar(200), " +
                        " @p_custadd3 nvarchar(200),  " +
                        " @p_custcityid bigint, " +
                        " @p_custstateid bigint, " +
                        " @p_custcountryid bigint, " +
                        " @p_custpin nvarchar(50), " +
                        " @p_custtelno nvarchar(50), " +
                        " @p_custmobno nvarchar(50), " +
                        " @p_custemail nvarchar(200), " +
                        " @p_custfaxno nvarchar(50), " +
                        " @p_custbirthdate datetime, " +
                        " @p_custgender nvarchar(50), " +
                        " @p_custmaritalstatus nvarchar(50),  " +
                        " @p_custannidate datetime,	 " +
                        " @p_custimage image, " +
                        " @p_custregdate datetime, " +
                        " @p_custdesc nvarchar(max)," +
                        " @p_custmobno2 nvarchar(50)," +
                        " @p_custmobno3 nvarchar(50)," +
                        " @p_custmobno4 nvarchar(50)," +
                        " @p_custmobno5 nvarchar(50)," +
                        " @p_custarea nvarchar(100)," +
                        " @p_custadd2add1 nvarchar(250)," +
                        " @p_custadd2add2 nvarchar(250)," +
                        " @p_custadd2add3 nvarchar(250)," +
                        " @p_custadd2area nvarchar(250)," +
                        " @p_custadd2city bigint," +
                        " @p_custadd2pin nvarchar(250)," +
                        " @p_custadd3add1 nvarchar(250)," +
                        " @p_custadd3add2 nvarchar(250)," +
                        " @p_custadd3add3 nvarchar(250)," +
                        " @p_custadd3area nvarchar(250)," +
                        " @p_custadd3city bigint," +
                        " @p_custadd3pin nvarchar(250)," +
                        " @p_custadd4add1 nvarchar(250)," +
                        " @p_custadd4add2 nvarchar(250)," +
                        " @p_custadd4add3 nvarchar(250)," +
                        " @p_custadd4area nvarchar(250)," +
                        " @p_custadd4city bigint," +
                        " @p_custadd4pin nvarchar(250)," +
                        " @p_custadd5add1 nvarchar(250)," +
                        " @p_custadd5add2 nvarchar(250)," +
                        " @p_custadd5add3 nvarchar(250)," +
                        " @p_custadd5area nvarchar(250)," +
                        " @p_custadd5city bigint," +
                        " @p_custadd5pin nvarchar(250)," +
                        " @p_custdiscper decimal(18,2)," +
                        " @p_custlandmark nvarchar(250)," +
                        " @p_custdocimage image," +
                        " @p_foodtoken bit," +
                        " @p_cardno nvarchar(50)," +
                        " @p_cardactdate datetime," +
                        " @p_cardenrollfees decimal(18,3)," +
                        " @p_cardstatus nvarchar(50)," +
                        " @p_cardexpdate datetime," +
                        " @p_cardremark nvarchar(max)," +
                        " @p_gstno nvarchar(50)," +
                        " @p_panno nvarchar(50)," +
                        " @p_vatno nvarchar(50)," +
                        " @p_applyigst bit," +
                        " @p_tieuprid bigint," +
                        " @p_userid bigint,  " +
                        " @p_errstr as nvarchar(max) out,  " +
                        " @p_retval as int out, " +
                        " @p_id as bigint out " +
                        " ) as  " +
                        " begin  " +
                        " try  " +
                        " begin  " +
                        " if (@p_mode=0)  " +
                        " begin   " +
                        " declare @codeRowCount as int   " +
                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                        " select @codeRowCount = (select count(*) from mstcust where custcode = @p_custcode and ISNULL(DelFlg,0)=0)  " +
                        " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Customer Code Already exits.' " +
                             " Return    " +
                         " End	  " +
                            " begin  " +
                            " Insert Into mstcust (custcode,custname,custadd1,custadd2,custadd3,custcityid,custstateid,custcountryid,  " +
                                                " custpin,custtelno,custmobno,custemail,custfaxno,custbirthdate,custgender,custmaritalstatus,custannidate, " +
                                                " custimage,custregdate,custdesc, " +
                                                " custmobno2,custmobno3,custmobno4,custmobno5," +
                                                " CUSTAREA,CUSTADD2ADD1,CUSTADD2ADD2,CUSTADD2ADD3,CUSTADD2AREA,CUSTADD2CITY,CUSTADD2PIN," +
                                                " CUSTADD3ADD1,CUSTADD3ADD2,CUSTADD3ADD3,CUSTADD3AREA,CUSTADD3CITY,CUSTADD3PIN," +
                                                " CUSTADD4ADD1,CUSTADD4ADD2,CUSTADD4ADD3,CUSTADD4AREA,CUSTADD4CITY,CUSTADD4PIN," +
                                                " CUSTADD5ADD1,CUSTADD5ADD2,CUSTADD5ADD3,CUSTADD5AREA,CUSTADD5CITY,CUSTADD5PIN," +
                                                " CUSTDISCPER,CUSTLANDMARK,CUSTDOCIMAGE,FOODTOKEN," +
                                                " CARDNO,CARDACTDATE,CARDENROLLFEES,CARDSTATUS,CARDEXPDATE,CARDREMARK,gstno,panno,applyigst,vatno,tieuprid," +
                                                " SENDDATA,auserid,adatetime,DelFlg)	 " +
                                        " Values (@p_custcode,@p_custname,@p_custadd1,@p_custadd2,@p_custadd3,@p_custcityid,@p_custstateid,@p_custcountryid, " +
                                            " @p_custpin,@p_custtelno,@p_custmobno,@p_custemail,@p_custfaxno,@p_custbirthdate,@p_custgender,@p_custmaritalstatus,@p_custannidate, " +
                                            " @p_custimage,@p_custregdate,@p_custdesc," +
                                            " @p_custmobno2,@p_custmobno3,@p_custmobno4,@p_custmobno5," +
                                            " @p_CUSTAREA,@p_CUSTADD2ADD1,@p_CUSTADD2ADD2,@p_CUSTADD2ADD3,@p_CUSTADD2AREA,@p_CUSTADD2CITY,@p_CUSTADD2PIN," +
                                            " @p_CUSTADD3ADD1,@p_CUSTADD3ADD2,@p_CUSTADD3ADD3,@p_CUSTADD3AREA,@p_CUSTADD3CITY,@p_CUSTADD3PIN," +
                                            " @p_CUSTADD4ADD1,@p_CUSTADD4ADD2,@p_CUSTADD4ADD3,@p_CUSTADD4AREA,@p_CUSTADD4CITY,@p_CUSTADD4PIN," +
                                            " @p_CUSTADD5ADD1,@p_CUSTADD5ADD2,@p_CUSTADD5ADD3,@p_CUSTADD5AREA,@p_CUSTADD5CITY,@p_CUSTADD5PIN," +
                                            " @p_CUSTDISCPER,@p_CUSTLANDMARK,@p_CUSTDOCIMAGE,@p_foodtoken," +
                                            " @p_CARDNO,@p_CARDACTDATE,@p_CARDENROLLFEES,@p_CARDSTATUS,@p_CARDEXPDATE,@p_CARDREMARK,@p_gstno,@p_panno,@p_applyigst,@p_vatno,@p_tieuprid," +
                                            " 0,@p_userid,getdate(),0) " +
                        "	set @p_id=SCOPE_IDENTITY() " +
                                       "   End  End   " +
                                " else if (@p_mode=1)  " +
                                   "   begin     " +
                                     " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0   " +
                                    "  select @codeRowCount1 = (select count(*) from mstcust where custcode = @p_custcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )   " +
                                    "  if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Customer Code Already exits.'    " +
                                     " Return   End   " +
                                     "  begin   " +
                                     " update mstcust set custcode=@p_custcode,  " +
                                     " custname = @p_custname,  " +
                                     "  custadd1 = @p_custadd1,custadd2 = @p_custadd2,custadd3 = @p_custadd3 ,custcityid = @p_custcityid,custstateid = @p_custstateid,custcountryid = @p_custcountryid, " +
                                     "  custpin = @p_custpin,custtelno = @p_custtelno,custmobno = @p_custmobno,custemail = @p_custemail,custfaxno = @p_custfaxno,custbirthdate = @p_custbirthdate,custgender = @p_custgender,custmaritalstatus = @p_custmaritalstatus,custannidate = @p_custannidate, " +
                                     " custimage=@p_custimage,custregdate = @p_custregdate, " +
                                     " custdesc=@p_custdesc, " +
                                     " custmobno2 = @p_custmobno2,custmobno3 = @p_custmobno3,custmobno4 = @p_custmobno4,custmobno5 = @p_custmobno5," +
                                     " CUSTAREA=@p_CUSTAREA,CUSTADD2ADD1=@p_CUSTADD2ADD1,CUSTADD2ADD2=@p_CUSTADD2ADD2,CUSTADD2ADD3=@p_CUSTADD2ADD3,CUSTADD2AREA=@p_CUSTADD2AREA,CUSTADD2CITY=@p_CUSTADD2CITY,CUSTADD2PIN=@p_CUSTADD2PIN, " +
                                                          " CUSTADD3ADD1=@p_CUSTADD3ADD1,CUSTADD3ADD2=@p_CUSTADD3ADD2,CUSTADD3ADD3=@p_CUSTADD3ADD3,CUSTADD3AREA=@p_CUSTADD3AREA,CUSTADD3CITY=@p_CUSTADD3CITY,CUSTADD3PIN=@p_CUSTADD3PIN, " +
                                                          " CUSTADD4ADD1=@p_CUSTADD4ADD1,CUSTADD4ADD2=@p_CUSTADD4ADD2,CUSTADD4ADD3=@p_CUSTADD4ADD3,CUSTADD4AREA=@p_CUSTADD4AREA,CUSTADD4CITY=@p_CUSTADD4CITY,CUSTADD4PIN=@p_CUSTADD4PIN, " +
                                                          " CUSTADD5ADD1=@p_CUSTADD5ADD1,CUSTADD5ADD2=@p_CUSTADD5ADD2,CUSTADD5ADD3=@p_CUSTADD5ADD3,CUSTADD5AREA=@p_CUSTADD5AREA,CUSTADD5CITY=@p_CUSTADD5CITY,CUSTADD5PIN=@p_CUSTADD5PIN, " +
                                                          " CUSTDISCPER = @p_CUSTDISCPER,CUSTLANDMARK=@p_CUSTLANDMARK,CUSTDOCIMAGE=@p_CUSTDOCIMAGE,foodtoken=@p_foodtoken," +
                                                          " CARDNO=@p_CARDNO,CARDACTDATE=@p_CARDACTDATE,CARDENROLLFEES=@p_CARDENROLLFEES,CARDSTATUS=@p_CARDSTATUS,CARDEXPDATE=@p_CARDEXPDATE,CARDREMARK=@p_CARDREMARK,gstno=@p_gstno,panno=@p_panno,applyigst=@p_applyigst," +
                                                          " vatno=@p_vatno,tieuprid=@p_tieuprid," +
                                    " SENDDATA=0,euserid = @p_userid,edatetime = getdate() 	 " +
                                     " where rid = @p_rid     " +
                                     " End  End  End   " +
                                 "	End   " +
                                    "  try  begin catch  " +
                                     " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;      " +
                                    "     set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                   "  Return  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                //
                this.DeleteProcedureFromOnlineDb("sp_SETTLEMENT");
                strproc = "";
                strproc = "Create  procedure sp_SETTLEMENT " +
                         "(  " +
                        " @p_mode as int, " +
                        " @p_rid as bigint,	" +
                        " @p_setledate datetime," +
                       "  @p_billrid bigint," +
                        " @p_setleno nvarchar(50), " +
                        " @p_setletype nvarchar(50)," +
                        " @p_setleamount decimal(18,2)," +
                         " @p_chequeno nvarchar(100), " +
                        " @p_chequebankname nvarchar(100)," +
                        " @p_creditcardno nvarchar(100)," +
                        " @p_creditholdername nvarchar(100)," +
                        " @p_creditbankname nvarchar(100)," +
                        " @p_setleprepby nvarchar(100)," +
                        " @p_setleremark nvarchar(max)," +
                        " @p_custrid bigint, " +
                        " @p_adjamt decimal(18,2)," +
                        " @p_tipamt decimal(18,3)," +
                        " @p_otherpaymentby nvarchar(200)," +
                        " @p_otherpaymentbyremark1 nvarchar(500)," +
                        " @p_otherpaymentbyremark2 nvarchar(500)," +
                       "  @p_userid bigint, " +
                       "  @p_errstr as nvarchar(max) out, " +
                       "  @p_retval as int out," +
                        " @p_id as bigint out " +
                        " ) as " +
                        " begin " +
                        " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0) " +
                                    " begin 	" +
                                        " Insert Into settlement (setledate,billrid,setleno,setletype,setleamount,chequeno,chequebankname, " +
                                                                " creditcardno,creditholdername,creditbankname,setleprepby,setleremark," +
                                                                " custrid,adjamt,tipamt,otherpaymentby,otherpaymentbyremark1,otherpaymentbyremark2," +
                                                               " senddata,auserid,adatetime,DelFlg) " +
                                                "  Values ( @p_setledate,@p_billrid,@p_setleno,@p_setletype,@p_setleamount,@p_chequeno,@p_chequebankname, " +
                                                        " @p_creditcardno,@p_creditholdername,@p_creditbankname,@p_setleprepby,@p_setleremark," +
                                                        " @p_custrid,@p_adjamt,@p_tipamt,@p_otherpaymentby,@p_otherpaymentbyremark1,@p_otherpaymentbyremark2," +
                                                        " 0,@p_userid,getdate(),0" +
                                                        "   )" +
                                        " set @p_id=SCOPE_IDENTITY()" +
                                        " End   " +
                                " else if (@p_mode=1)    " +
                                "	begin " +
                                      " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                      "	Update settlement " +
                                       " SET " +
                                            " setledate=@p_setledate,billrid=@p_billrid,setleno=@p_setleno,setletype=@p_setletype,setleamount=@p_setleamount,chequeno=@p_chequeno,chequebankname=@p_chequebankname," +
                                            " creditcardno=@p_creditcardno,creditholdername=@p_creditholdername,creditbankname=@p_creditbankname,setleprepby=@p_setleprepby,setleremark=@p_setleremark,	" +
                                            " custrid=@p_custrid, adjamt=@p_adjamt,tipamt=@p_tipamt," +
                                            " otherpaymentby=@p_otherpaymentby,otherpaymentbyremark1=@p_otherpaymentbyremark1,otherpaymentbyremark2=@p_otherpaymentbyremark2," +
                                            " senddata=0,euserid = @p_userid,edatetime = getdate()  " +
                                            " where rid = @p_rid  " +
                                   "  End " +
                                "  End " +
                            "	end	" +
                            "	try  " +
                                    " begin catch    " +
                                   "   SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                    "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                   "   Return  " +
                                    "  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                //MSTITEMGTOUP
                this.DeleteProcedureFromOnlineDb("sp_MSTITEMGROUP");
                strproc = "";
                strproc = " CREATE procedure sp_MSTITEMGROUP " +
                        " ( " +
                       "  @p_mode as int, " +
                       "  @p_rid bigint, " +
                       "  @p_igcode nvarchar(20), " +
                       "  @p_igname nvarchar(100), " +
                       "  @p_igdesc nvarchar(max)," +
                       "  @p_notapplydisc bit, " +
                       "  @p_igpname nvarchar(100), " +
                       "  @p_igdispord bigint, " +
                       "  @p_ISHIDEGROUP Bigint, " +
                       "  @p_showindiffcolor bit," +
                       "  @p_ishidegroupkot bit," +
                       "  @p_isitemremgrp bit," +
                       "  @p_ishidegroupcashmemo bit," +
                       "  @p_hsncoderid bigint, " +
                       "  @p_igbackcolor bigint, " +
                       "  @p_igforecolor bigint, " +
                       "  @p_igfontname nvarchar(200)," +
                       "  @p_igfontsize float," +
                       "  @p_igfontbold bit," +
                       "  @p_igprintord bigint, " +
                       "  @p_reglangigname nvarchar(200)," +
                       "  @p_userid bigint, " +
                       "  @p_errstr as nvarchar(max) out, " +
                        " @p_retval as int out," +
                        " @p_id as bigint out " +
                        " ) as " +
                        " begin " +
                       "  try " +
                        " begin " +
                        " if (@p_mode=0) " +
                       "  begin  " +
                       "  declare @codeRowCount as int  " +
                        " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                       "  select @codeRowCount = (select count(*) from mstitemgroup where igcode = @p_igcode and ISNULL(DelFlg,0)=0)  " +
                       "  if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Group Code Already exits.'  " +
                          "   Return    " +
                       "  End	 " +
                  " Begin  " +
                " SET NOCOUNT ON " +
                    "  declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                     " select  @nameRowCount = (select count(*) from mstitemgroup where igname = @p_igname and ISNULL(DelFlg,0)=0)   " +
                    "  if ( @nameRowCount > 0)   " +
                   "   begin    " +
                    "  set @p_Retval = 1 set @p_Errstr ='Item Group Name Already exits.'   " +
                     " Return  " +
                            "   End   " +
                          "   end " +
                                        "  begin " +
                                        "  Insert Into mstitemgroup (igcode,igname,igdesc,notapplydisc,igpname,igdispord,ISHIDEGROUP,showindiffcolor, " +
                                                                    " ishidegroupkot,isitemremgrp,ishidegroupcashmemo," +
                                                                    " hsncoderid,igbackcolor,igforecolor,igfontname,igfontsize,igfontbold, " +
                                                                    " igprintord,reglangigname, " +
                                                                    " senddata,auserid,adatetime,DelFlg) " +
                                        "  Values (@p_igcode,@p_igname,@p_igdesc,@p_notapplydisc,@p_igpname,@p_igdispord,@p_ISHIDEGROUP,@p_showindiffcolor," +
                                                   " @p_ishidegroupkot,@p_isitemremgrp,@p_ishidegroupcashmemo, " +
                                                   " @p_hsncoderid,@p_igbackcolor,@p_igforecolor,@p_igfontname,@p_igfontsize,@p_igfontbold, " +
                                                   " @p_igprintord,@p_reglangigname, " +
                                                   " 0, @p_userid,getdate(),0) " +
                                        "	set @p_id=SCOPE_IDENTITY()	" +
                                        "  End  End  " +
                                " else if (@p_mode=1) " +
                                    "  begin   " +
                                     " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0  " +
                                    "  select @codeRowCount1 = (select count(*) from mstitemgroup where igcode = @p_igcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                    "  if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Group Code Already exits.' " +
                                   "   Return   End  " +
                             " Begin  " +
                              "    declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0     " +
                                "  select  @nameRowCount1 = (select count(*) from mstitemgroup where igname = @p_igname and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                                "  if ( @nameRowCount1 > 0)     " +
                                 " begin   " +
                                 " set @p_Retval = 1 set @p_Errstr ='Item Group Name Already exits.' " +
                               "   Return  " +
                             "  End  END " +
                            "  begin  " +
                                    "  UPDATE mstitemgroup set igcode=@p_igcode, " +
                                    "  igname = @p_igname, igdesc = @p_igdesc, " +
                                    "  notapplydisc=@p_notapplydisc, " +
                                    "  igpname=@p_igpname,igdispord=@p_igdispord, " +
                                    "  ISHIDEGROUP=@p_ISHIDEGROUP," +
                                    "  showindiffcolor=@p_showindiffcolor,ishidegroupkot=@p_ishidegroupkot,ISITEMREMGRP=@p_ISITEMREMGRP," +
                                    "  ishidegroupcashmemo=@p_ishidegroupcashmemo," +
                                    "  hsncoderid=@p_hsncoderid,igbackcolor=@p_igbackcolor,igforecolor=@p_igforecolor,igfontname=@p_igfontname,igfontsize=@p_igfontsize,igfontbold=@p_igfontbold, " +
                                    "  igprintord=@p_igprintord,reglangigname=@p_reglangigname, " +
                                    "  senddata=0,euserid = @p_userid,edatetime = getdate() " +
                                    "  where rid = @p_rid " +
                                    "  End  End  End  " +
                                    "  End  " +
                                    "  try  begin catch    " +
                                    "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                    "  set  @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                    "  Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                //
                this.DeleteProcedureFromOnlineDb("sp_BILLDTL");
                strproc = "";
                strproc = "create  procedure sp_BILLDTL " +
                         "( " +
                        " @p_mode as int, " +
                        " @p_rid as bigint," +
                        " @p_billrid bigint," +
                        " @p_irid bigint," +
                        " @p_kotrid bigint," +
                        " @p_iqty decimal(18,3)," +
                        " @p_irate decimal(18,2)," +
                        " @p_ipamt decimal(18,2)," +
                        " @p_iamt decimal(18,2)," +
                        " @p_notapplydisc bigint," +
                        " @p_notapplysertax bigint," +
                        " @p_sertaxtype nvarchar(50)," +
                        " @p_notapplyvat bit," +
                        " @p_discper decimal(18,3)," +
                        " @p_discamt decimal(18,2)," +
                        " @p_sertaxper decimal(18,3)," +
                        " @p_sertaxamt decimal(18,2)," +
                        " @p_foodvatper decimal(18,3)," +
                        " @p_foodvatamt decimal(18,2)," +
                        " @p_liqvatper decimal(18,3)," +
                        " @p_liqvatamt decimal(18,2)," +
                        " @p_bevvatper decimal(18,3)," +
                        " @p_bevvatamt decimal(18,2)," +
                        " @p_serchrper decimal(18,3)," +
                        " @p_serchramt decimal(18,2)," +
                        " @p_NEWSERCHRPER decimal(18,3)," +
                        " @p_NEWSERCHRAMT decimal(18,3)," +
                        " @p_kkcessper decimal(18,3)," +
                        " @p_kkcessamt decimal(18,3)," +
                        " @p_irewpoints  decimal(18,3)," +
                        " @p_notapplygst bit," +
                        " @p_cgstper decimal(18,3)," +
                        " @p_cgstamt decimal(18,3)," +
                        " @p_sgstper decimal(18,3)," +
                        " @p_sgstamt decimal(18,3)," +
                        " @p_igstper decimal(18,3)," +
                        " @p_igstamt decimal(18,3)," +
                        " @p_gstper decimal(18,3)," +
                        " @p_gstamt decimal(18,3)," +
                        " @p_userid bigint, " +
                        " @p_errstr as nvarchar(max) out, " +
                        " @p_retval as int out," +
                        " @p_id as bigint out " +
                        " ) as " +
                        " begin " +
                        " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                 " if (@p_mode=0)  " +
                                    " begin " +
                                    "	Insert Into billdtl (billrid,irid,kotrid,iqty,irate,ipamt,iamt,notapplydisc," +
                                                            "notapplysertax,sertaxtype," +
                                                            " notapplyvat,discper,discamt,sertaxper,sertaxamt,foodvatper,foodvatamt,liqvatper,liqvatamt,bevvatper,bevvatamt, " +
                                                            " serchrper,serchramt,NEWSERCHRPER,NEWSERCHRAMT,kkcessper,kkcessamt,irewpoints," +
                                                            " notapplygst,cgstper,cgstamt,sgstper,sgstamt,igstper,igstamt,gstper,gstamt," +
                                                            " senddata,auserid,adatetime,DelFlg) " +
                                                "  Values ( " +
                                                            " @p_billrid,@p_irid,@p_kotrid,@p_iqty,@p_irate,@p_ipamt,@p_iamt,@p_notapplydisc, " +
                                                            " @p_notapplysertax,@p_sertaxtype," +
                                                            " @p_notapplyvat,@p_discper,@p_discamt,@p_sertaxper,@p_sertaxamt,@p_foodvatper,@p_foodvatamt,@p_liqvatper,@p_liqvatamt,@p_bevvatper,@p_bevvatamt, " +
                                                            " @p_serchrper,@p_serchramt,@p_NEWSERCHRPER,@p_NEWSERCHRAMT,@p_kkcessper,@p_kkcessamt,@p_irewpoints," +
                                                            " @p_notapplygst,@p_cgstper,@p_cgstamt,@p_sgstper,@p_sgstamt,@p_igstper,@p_igstamt,@p_gstper,@p_gstamt," +
                                                            " 0,@p_userid,getdate(),0 " +
                                                            "  ) " +
                                        " set @p_id=SCOPE_IDENTITY()" +
                                        " End  " +
                                " else if (@p_mode=1)    " +
                                    " begin " +
                                      " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                        " Update billdtl " +
                                       " set irid=@p_irid,kotrid = @p_kotrid,iqty=@p_iqty,irate=@p_irate,ipamt=@p_ipamt,iamt=@p_iamt, " +
                                                                  "  notapplydisc=@p_notapplydisc,notapplysertax=@p_notapplysertax,sertaxtype=@p_sertaxtype," +
                                                                  "  notapplyvat=@p_notapplyvat,discper=@p_discper,discamt=@p_discamt,sertaxper=@p_sertaxper,sertaxamt=@p_sertaxamt,foodvatper=@p_foodvatper,foodvatamt=@p_foodvatamt,liqvatper=@p_liqvatper,liqvatamt=@p_liqvatamt,bevvatper=@p_bevvatper,bevvatamt=@p_bevvatamt, " +
                                                                  "  serchrper=@p_serchrper,serchramt=@p_serchramt, " +
                                                                  "  NEWSERCHRPER=@p_NEWSERCHRPER,NEWSERCHRAMT=@p_NEWSERCHRAMT,kkcessper=@p_kkcessper,kkcessamt=@p_kkcessamt," +
                                                                  "  irewpoints=@p_irewpoints," +
                                                                  "  notapplygst=@p_notapplygst,cgstper=@p_cgstper,cgstamt=@p_cgstamt,sgstper=@p_sgstper,sgstamt=@p_sgstamt,igstper=@p_igstper,igstamt=@p_igstamt,gstper=@p_gstper,gstamt=@p_gstamt," +
                                                                  "  senddata=0,euserid = @p_userid,edatetime = getdate()    " +
                                                                  "  where rid = @p_rid and billrid=@p_billrid " +
                                  "  End " +
                                 " End " +
                                " end	" +
                                " try  " +
                                    " begin catch  " +
                                    "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                    "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                    "  Return  " +
                                    "  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                //// KOTDTL
                this.DeleteProcedureFromOnlineDb("sp_KOTDTL");
                strproc = "";
                strproc = "CREATE procedure sp_KOTDTL " +
                        " ( @p_mode as int, " +
                        " @p_rid as bigint, " +
                        " @p_kotrid bigint,	" +
                        " @p_irid bigint, " +
                        " @p_iname nvarchar(500)," +
                        " @p_iqty decimal(18,2)," +
                        " @p_irate decimal(18,2), " +
                        " @p_iamt decimal(18,2), " +
                        " @p_iremark nvarchar(300),	" +
                        " @p_imodifier nvarchar(max)," +
                        " @p_icompitem bit," +
                        " @p_userid bigint, " +
                        " @p_errstr as nvarchar(max) out, " +
                        " @p_retval as int out, " +
                        " @p_id as bigint out " +
                        " ) as " +
                        " begin " +
                        " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                " if (@p_mode=0) " +
                                    " begin " +
                                        " Insert Into kotdtl (kotrid,irid,iname,iqty,irate,iamt,iremark,imodifier,icompitem,senddata,auserid,adatetime,DelFlg) " +
                                        " Values (@p_kotrid,@p_irid,@p_iname,@p_iqty,@p_irate,@p_iamt,@p_iremark,@p_imodifier,@p_icompitem,0,@p_userid,getdate(),0) " +
                                        " set @p_id=SCOPE_IDENTITY() " +
                                        " End   " +
                                " else if (@p_mode=1)  " +
                                    " begin " +
                                    " set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                                    " Update KOTDTL SET IRID=@p_IRID,INAME=@p_INAME, " +
                                        " IQTY=@p_IQTY,IRATE=@p_IRATE,IAMT=@p_IAMT,iremark=@p_iremark,imodifier=@p_imodifier,icompitem=@p_icompitem," +
                                        " senddata=0,euserid = @p_userid,edatetime = getdate()" +
                                        " where rid = @p_rid and KOTRID=@p_KOTRID " +
                                   "  End " +
                                 " End " +
                                " end	" +
                                " try  " +
                                    " begin catch " +
                                    "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                     " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                    "  Return  " +
                                    "  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                // CUSTOMEROUTSTANDING
                this.DeleteProcedureFromOnlineDb("sp_CUSTOMEROUTSTANDING");
                strproc = "";
                strproc = "create procedure sp_CUSTOMEROUTSTANDING " +
                        " (@p_fromdate datetime,@p_todate datetime) " +
                            " AS BEGIN " +
                           " select mstcust.rid as Custrid,mstcust.CUSTNAME,isnull(Billinfo.BillAmount,0) as BillAmount,isnull(Billinfo.TotbillAmount,0) as TotBillAmount," +
                                                   " isnull(setleinfo.setleamount,0) as SetleAmount," +
                           " (isnull(Billinfo.BillAmount,0) - isnull(setleinfo.setleamount,0)) As PendingAmt " +
                          "  from mstcust " +
                           " LEFT JOIN " +
                                 " (SELECT SUM(BILL.NETAMOUNT) AS BillAmount,SUM(BILL.TOTAMOUNT) AS TotbillAmount,BILL.CUSTRID " +
                                  "  FROM  BILL " +
                                  "  WHERE (ISNULL(DELFLG, 0) = 0 and (isnull(ISREVISEDBILL,0)=0) and isnull(CUSTRID,0)>0 " +
                                  "  And  bill.billDATE between @p_fromdate and @p_todate) " +
                                   " GROUP BY BILL.CUSTRID ) AS Billinfo ON (Billinfo.CUSTRID = mstcust.rid) " +
                           " LEFT JOIN " +
                                 " (SELECT SUM(ISNULL(SETLEAMOUNT,0) + ISNULL(ADJAMT,0)) AS setleamount,SETTLEMENT.CUSTRID " +
                                  "  FROM  SETTLEMENT " +
                                  "  WHERE (ISNULL(DELFLG, 0) = 0 and isnull(CUSTRID,0)>0 " +
                                           " And  SETTLEMENT.SETLEDATE between @p_fromdate and @p_todate) " +
                                  "  GROUP BY CUSTRID) AS setleinfo ON (setleinfo.CUSTRID = mstcust.rid) " +
                           " where (isnull(Billinfo.BillAmount,0) - isnull(setleinfo.setleamount,0))>0 " +
                           " ORDER BY mstcust.CUSTNAME,mstcust.rid " +
                           " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_KOTEDITREG");
                strproc = "";
                strproc = " create procedure sp_KOTEDITREG " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " as " +
                                " begin " +
                                " select kot.rid,kot.kotdate,kot.kottime,kot.kotno,kot.KOTTABLENAME,kot.EUSERID,MSTUSERS.USERNAME,kot.edatetime  " +
                                " from kot " +
                                " left join MSTUSERS on (MSTUSERS.rid = kot.EUSERID) " +
                                " WHERE  " +
                                " KOT.KOTDATE between @p_fromdate and @p_todate  " +
                                " and isnull(kot.delflg,0)=0 and kot.EUSERID is not null " +
                                " order by KOT.KOTDATE,kot.kottime,MSTUSERS.USERNAME " +
                                " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_KOTDELETEREG");
                strproc = "";
                strproc = " create procedure sp_KOTDELREG " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " as " +
                                " begin " +
                                " select KOT.rid,KOT.kotdate,KOT.kottime,KOT.kotno,KOT.KOTTABLENAME,KOT.EUSERID,MSTUSERS.USERNAME,KOT.DDATETIME  " +
                                " From KOT " +
                                " left join MSTUSERS on (MSTUSERS.rid = kot.EUSERID) " +
                                " WHERE " +
                                " KOT.KOTDATE between @p_fromdate and @p_todate and isnull(kot.delflg,0)=1 AND KOT.DUSERID is not null  " +
                                " order by KOT.KOTDATE,kot.kottime,MSTUSERS.USERNAME " +
                                " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_KOTREG");
                strproc = "";
                strproc = " CREATE procedure sp_KOTREG" +
                            " (@p_fromdate datetime,@p_todate datetime)" +
                            " as " +
                            " begin" +
                            " SELECT KOT.Rid,KOT.kotdate,KOT.kottime,KOT.kotno,KOT.KOTTABLENAME,MSTEMP.EMPNAME, " +
                            " (CASE WHEN KOT.ISCOMPKOT=1 THEN 'COMPLIMENTRY' ELSE '' END) AS COMP " +
                            " FROM KOT " +
                            " left join MSTEMP on (MSTEMP.rid=kot.KOTORDERPERID)" +
                            " WHERE  KOT.KOTDATE between @p_fromdate and @p_todate and isnull(kot.delflg,0)=0" +
                            " order by KOT.KOTDATE" +
                            " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_KOTREGISTER");
                strproc = "";
                strproc = " CREATE procedure sp_KOTREGISTER  " +
                            " (@p_fromdate datetime,@p_todate datetime)  " +
                            " as  " +
                            " Begin  " +
                            " Select KOT.RID,KOT.KOTDATE,KOT.KOTTIME,KOT.KOTNO,KOT.KOTTABLENAME, " +
                            " (CASE WHEN KOT.ISCOMPKOT=1 THEN 'COMPLIMENTRY' ELSE '' END) AS COMP, " +
                            " KOTDTL.RID AS KOTDTLRID,KOTDTL.INAME,KOTDTL.IQTY " +
                            " From KOT  " +
                            " LEFT JOIN KOTDTL ON (KOTDTL.KOTRID = KOT.RID)  " +
                            " WHERE KOT.KOTDATE between @p_fromdate and @p_todate   " +
                            " And isnull(kot.delflg,0)=0 and isnull(kotdtl.delflg,0)=0  " +
                            " Order by KOT.KOTDATE  " +
                            " end  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_BILLEDITREG");
                strproc = "";
                strproc = " create procedure sp_BILLEDITREG  " +
                            "(@p_fromdate datetime,@p_todate datetime) as  " +
                            " begin  " +
                            " select bill.rid,bill.billno,bill.REFBILLNO,bill.BILLTIME,bill.billdate,bill.billtype,bill.billpax,MSTTABLE.TABLENAME,  " +
                            " bill.totamount,bill.totvatamount, bill.totaddvatper as totsercharge,bill.totdiscamount,bill.netamount,  " +
                            " bill.EUSERID,MSTUSERS.USERNAME,bill.edatetime  " +
                            " from bill  " +
                            " left join MSTTABLE on (MSTTABLE.rid = bill.tablerid) " +
                            " left join MSTUSERS on (MSTUSERS.rid = bill.EUSERID) " +
                            " WHERE (bill.billDATE between @p_fromdate and @p_todate) " +
                            " and isnull(bill.delflg,0)=0 and bill.EUSERID is not null " +
                            " order by bill.billDATE,bill.rid  " +
                            " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLDELREG");
                strproc = "";
                strproc = "create procedure sp_BILLDELREG " +
                            " (@p_fromdate datetime,@p_todate datetime) as " +
                            " begin " +
                            " Select bill.rid,bill.billno,bill.REFBILLNO,bill.billdate,bill.BILLTIME,bill.billtype,bill.billpax,MSTTABLE.TABLENAME,bill.netamount, " +
                            " mstitem.rid as irid, mstitem.INAME,billdtl.IQTY,billdtl.IRATE,billdtl.IAMT,billdtl.DDATETIME,MSTUSERS.USERNAME" +
                             " from bill " +
                             " left join billdtl on (billdtl.BILLRID = bill.rid) " +
                             " left join MSTTABLE on (MSTTABLE.rid = bill.tablerid) " +
                             " left join MSTUSERS on (MSTUSERS.rid = billdtl.DUSERID) " +
                             " left join mstitem on (mstitem.rid = billdtl.IRID)" +
                             " WHERE " +
                            " (bill.billDATE between @p_fromdate and @p_todate) and " +
                            " (isnull(billdtl.delflg,0)=1 or isnull(bill.delflg,0)=1) " +
                            " order by bill.billDATE,billdtl.RID,bill.rid" +
                            " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_KOT");
                strproc = "";
                strproc = "create  procedure sp_KOT " +
                         " (  " +
                        " @p_mode as int, " +
                        " @p_rid as bigint, " +
                       "  @p_kotdate datetime, " +
                        " @p_kottime datetime," +
                        " @p_kotno nvarchar(100)," +
                       "  @p_kottokno nvarchar(100)," +
                        " @p_kotorderperid bigint," +
                        " @p_isparselkot bit," +
                        " @p_kottableid bigint," +
                        " @p_kottablename nvarchar(100)," +
                        " @p_kotremark nvarchar(max),	" +
                        " @p_kotpax bigint," +
                        " @p_custrid bigint," +
                        " @p_custname nvarchar(250)," +
                        " @p_custadd nvarchar(100)," +
                        " @p_cardno nvarchar(50)," +
                        " @p_refkotno nvarchar(50)," +
                        " @p_refkotnum bigint," +
                        " @p_iscompkot bit," +
                       "  @p_userid bigint, " +
                       "  @p_errstr as nvarchar(max) out, " +
                       "  @p_retval as int out," +
                        " @p_id as bigint out " +
                       "  ) as " +
                      "   begin " +
                       "  try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                " if (@p_mode=0)  " +
                                "	begin 	" +
                                    "	Insert Into kot (kotdate,kottime,kotno,kottokno,kotorderperid,isparselkot,kottableid,kottablename,kotremark,kotpax," +
                                        " custrid,custname,custadd,cardno,refkotno,refkotnum,iscompkot,senddata, " +
                                        " auserid,adatetime,DelFlg) " +
                                        " Values (@p_kotdate,@p_kottime,@p_kotno,@p_kottokno,@p_kotorderperid,@p_isparselkot,@p_kottableid,@p_kottablename,@p_kotremark,@p_kotpax," +
                                        " @p_custrid,@p_custname,@p_custadd,@p_cardno,@p_refkotno,@p_refkotnum,@p_iscompkot, " +
                                        " 0,@p_userid,getdate(),0) " +
                                        " set @p_id=SCOPE_IDENTITY() " +
                                        " End   " +
                                " else if (@p_mode=1)  " +
                                "	begin " +
                                    " set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                                        " UPDATE KOT SET kotdate = @p_kotdate,kottime=@p_kottime,kotno=@p_kotno,kottokno=@p_kottokno," +
                                                " kotorderperid=@p_kotorderperid,isparselkot=@p_isparselkot,kottableid=@p_kottableid," +
                                                " kottablename=@p_kottablename,kotremark=@p_kotremark,kotpax=@p_kotpax,custrid=@p_custrid," +
                                                " custname=@p_custname,custadd=@p_custadd,cardno=@p_cardno," +
                                                " refkotno=@p_refkotno,refkotnum=@p_refkotnum,iscompkot=@p_iscompkot, " +
                                                " senddata=0,EUSERID=@p_userid,EDATETIME=getdate() " +
                                                " where RID=@p_rid " +
                                            " End " +
                                        "  End " +
                                        " end " +
                                        " try   " +
                                            " begin catch     " +
                                             "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                            "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                            "  Return  " +
                                             " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_ITEMSALES");
                strproc = "";
                //strproc = "create procedure SP_ITEMSALES " +
                //   " (@p_fromdate datetime,@p_todate datetime) " +
                //    " AS BEGIN " +
                //    " Select MSTITEM.rid as itemrid, " +
                //    " MSTITEMGROUP.IGCODE,MSTITEMGROUP.IGNAME, " +
                //    " MSTITEM.ICODE,MSTITEM.Iname, " +
                //    " sum(abs(billdtl.iqty)) as TOTQTY,sum(abs(billdtl.IAMT)) as TOTIAMT, " +
                //    " SUM(CASE WHEN billdtl.iqty > 0 THEN billdtl.iqty ELSE 0 END) AS IQTY, " +
                //    " SUM(CASE WHEN billdtl.iqty < 0 THEN billdtl.iqty ELSE 0 END) AS DISQTY, " +
                //    " SUM(CASE WHEN billdtl.IAMT > 0 THEN billdtl.IAMT ELSE 0 END) AS IAMT " +
                //    " FROM BILL " +
                //    " inner join billdtl on (billdtl.BILLRID = bill.rid) " +
                //    " inner join MSTITEM on (MSTITEM.rid = billdtl.IRID) " +
                //    " left join MSTITEMGROUP on (MSTITEMGROUP.rid = MSTITEM.IGRPRID) " +
                //    " where (isnull(bill.delflg,0)=0 and isnull(billdtl.delflg,0)=0) and isnull(BILL.ISREVISEDBILL,0)=0 " +
                //    " and bill.billDATE between @p_fromdate and @p_todate " +
                //    " group by MSTITEMGROUP.IGCODE,MSTITEMGROUP.IGNAME,MSTITEM.rid,MSTITEM.ICODE,MSTITEM.Iname " +
                //    " order by MSTITEMGROUP.IGNAME, MSTITEM.Iname " +
                //    " end ";
                strproc = "create procedure SP_ITEMSALES " +
                   " (@p_fromdate datetime,@p_todate datetime) " +
                    " AS BEGIN " +
                    " SELECT MSTITEM.RID AS IRID,MSTITEMGROUP.IGCODE,MSTITEMGROUP.IGNAME,MSTITEM.ICODE,MSTITEM.INAME," +
                                " (ISNULL(BILLINFO.TOTQTY,0) + ISNULL(COMPINFO.COMPQTY,0)) AS TOTALQTY," +
                                " ISNULL(BILLINFO.TOTQTY,0) AS TOTQTY,ISNULL(BILLINFO.TOTIAMT,0) AS TOTIAMT,ISNULL(BILLINFO.IQTY,0) AS IQTY," +
                                " ABS(ISNULL(BILLINFO.DISQTY,0)) AS DISQTY," +
                                " ISNULL(COMPINFO.COMPQTY,0) AS COMPQTY,ISNULL(COMPINFO.COMPAMT,0) AS COMPAMT," +
                                " ISNULL(BILLINFO.IAMT,0) AS IAMT" +
                                " FROM MSTITEM" +
                                " LEFT JOIN MSTITEMGROUP ON (MSTITEMGROUP.RID = MSTITEM.IGRPRID)" +
                                " LEFT JOIN ( " +
                                       "  SELECT MSTITEM.RID as IRID, " +
                                       "  SUM(KOTDTL.IQTY) AS COMPQTY," +
                                       "  SUM(ISNULL(KOTDTL.IAMT,0)) AS COMPAMT" +
                                       "  FROM KOT " +
                                       "  LEFT JOIN KOTDTL ON (KOTDTL.KOTRID = KOT.RID)" +
                                       "  LEFT JOIN MSTITEM on (MSTITEM.rid = KOTDTL.IRID) " +
                                       "  WHERE ISNULL(KOT.DELFLG,0) = 0 AND ISNULL(KOTDTL.DELFLG,0) = 0 " +
                                           "  AND ISNULL(KOT.ISCOMPKOT,0)=1 AND ISNULL(KOTDTL.ICOMPITEM,0)=1" +
                                           "  AND KOT.KOTDATE BETWEEN @p_fromdate and @p_todate  " +
                                           "  GROUP BY MSTITEM.RID" +
                                            " ) AS COMPINFO ON (COMPINFO.IRID = MSTITEM.RID) " +
                                " LEFT JOIN (	 " +
                                            " SELECT" +
                                              "   BILLDTL.IRID, " +
                                                "   SUM(ABS(BILLDTL.IQTY)) as TOTQTY," +
                                                  " SUM(ABS(BILLDTL.IAMT)) as TOTIAMT,  " +
                                                  " SUM(CASE WHEN BILLDTL.IQTY > 0 THEN BILLDTL.IQTY ELSE 0 END) AS IQTY,  " +
                                                  " SUM(CASE WHEN BILLDTL.IQTY < 0 THEN BILLDTL.IQTY ELSE 0 END) AS DISQTY, " +
                                                  " SUM(CASE WHEN BILLDTL.IAMT > 0 THEN BILLDTL.IAMT ELSE 0 END) AS IAMT  " +
                                             " FROM BILL" +
                                               "  LEFT JOIN BILLDTL ON (BILLDTL.BILLRID = BILL.RID)  	" +
                                                " WHERE (ISNULL(BILL.DELFLG,0)=0 and ISNULL(BILLDTL.DELFLG,0)=0) " +
                                                      "   AND ISNULL(BILL.ISREVISEDBILL,0)=0  " +
                                                       "  AND BILL.BILLDATE BETWEEN @p_fromdate and @p_todate		" +
                                                " GROUP BY BILLDTL.IRID  " +
                                                " ) AS BILLINFO ON (BILLINFO.IRID = MSTITEM.RID)" +
                         " WHERE  (ISNULL(BILLINFO.TOTQTY,0) + ISNULL(COMPINFO.COMPQTY,0))> 0" +
                         " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTREMARK");
                strproc = "";
                strproc = "create  procedure sp_MSTREMARK  " +
                        " ( " +
                        " @p_mode as int, " +
                        " @p_rid bigint, " +
                        " @p_remarkcode nvarchar(20), " +
                        " @p_remarkname nvarchar(100), " +
                       "  @p_remarkdesc nvarchar(max), " +
                       "  @p_userid bigint, " +
                        " @p_errstr as nvarchar(max) out, " +
                      "   @p_retval as int out," +
                        " @p_id as bigint out " +
                         " ) as  " +
                        " begin " +
                       "  try " +
                       "  begin  " +
                        " if (@p_mode=0)  " +
                        " begin  " +
                        " declare @codeRowCount as int  " +
                       "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                        " select @codeRowCount = (select count(*) from mstremark where remarkcode = @p_remarkcode and ISNULL(DelFlg,0)=0) " +
                       "  if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Remark Code Already exits.'     " +
                          "   Return    " +
                      "   End	 " +
                  " Begin " +
                   "   declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                     " select  @nameRowCount = (select count(*) from mstremark where remarkname = @p_remarkname and ISNULL(DelFlg,0)=0)  " +
                   "   if ( @nameRowCount > 0)   " +
                   "   begin    " +
                     " set @p_Retval = 1 set @p_Errstr ='Remark Name Already exits.'  " +
                    "  Return  " +
                              " End  " +
                          "   end " +
                                    " begin " +
                                        "  Insert Into mstremark (remarkcode,remarkname,remarkdesc,senddata,auserid,adatetime,DelFlg) " +
                                        "  Values (@p_remarkcode,@p_remarkname,@p_remarkdesc,0,@p_userid,getdate(),0) " +
                                        "	set @p_id=SCOPE_IDENTITY()	" +
                                         " End  End  " +
                                " else if (@p_mode=1) " +
                                      " begin " +
                                     " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0  " +
                                    "  select @codeRowCount1 = (select count(*) from mstremark where remarkcode = @p_remarkcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 ) " +
                                    "  if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Remark Code Already exits.' " +
                                    "  Return   End " +
                            "  Begin  " +
                                "  declare  @nameRowCount1 int  set @p_Errstr=''  set @p_Retval=0    " +
                                "  select  @nameRowCount1 = (select count(*) from mstremark where remarkname = @p_remarkname and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                               "   if ( @nameRowCount1 > 0)  " +
                                "  begin    " +
                                 " set @p_Retval = 1 set @p_Errstr ='Remark Name Already exits.' " +
                                "  Return  " +
                               " End  END " +
                            "  begin  " +
                                    "  update mstremark set remarkcode=@p_remarkcode, " +
                                     " remarkname = @p_remarkname, remarkdesc = @p_remarkdesc, " +
                                    "  senddata = 0,euserid = @p_userid,edatetime = getdate()    " +
                                   "   where rid = @p_rid    " +
                                   "   End  End  End	" +
                                    "	End  " +
                                    "  try  begin catch  " +
                                     " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                      "   set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                    " Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_MSTFEEDBACK");
                strproc = "";
                strproc = "create procedure sp_MSTFEEDBACK " +
                        " ( " +
                            " @p_mode as int, @p_rid bigint,@p_feedno nvarchar(100)," +
                            " @p_FEEDDATE datetime,@p_CUSTRID bigint,@p_CUSTADD nvarchar(max)," +
                            " @p_CUSTCONTNO nvarchar(50),@p_FOODFLAVOUR nvarchar(20),@p_FOODPRESENTATION nvarchar(20), " +
                            " @p_FOODVALUEFORMONEY nvarchar(20),@p_FOODFRESHNESS nvarchar(20),@p_FOODCHOICEOFMENU nvarchar(20), " +
                            " @p_SERVICEFRIENDLY nvarchar(20),@p_SERVICEPROFESSIONAL nvarchar(20),@p_SERVICEEXPLATION nvarchar(20), " +
                            " @p_SERVICETIMETAKEN nvarchar(20),@p_SERVICEACCOUNT nvarchar(20),@p_VENUEATMOSPHERE nvarchar(20), " +
                            " @p_VENUECLEANLINESS nvarchar(20),@p_VENUESTAFF nvarchar(20),@p_GENERALFOOD nvarchar(20), " +
                            " @p_GENERALOVERALLSERVICE nvarchar(20),@p_GENERALCLEANLINESS nvarchar(20),@p_GENERALORDER nvarchar(20), " +
                            " @p_GENERALSPEED nvarchar(20),@p_GENERALVALUE nvarchar(20),@p_GENERALOVREXP nvarchar(20), " +
                            " @p_GENERALOVRRATING nvarchar(20),@p_WDHEARABOUTUS nvarchar(50),@p_WDHEARABOUTUSOTHER nvarchar(2000), " +
                            " @p_SUGG nvarchar(max), " +
                            " @p_userid bigint,@p_errstr as nvarchar(max) out,  " +
                            " @p_retval as int out, @p_id as bigint out " +
                            " ) as " +
                            " begin " +
                            " try   " +
                            " begin   " +
                            " if (@p_mode=0) " +
                            " begin " +
                            " declare @codeRowCount as int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                            " select @codeRowCount = (select count(*) from MSTFEEDBACK where FEEDNO = @p_FEEDNO and ISNULL(DelFlg,0)=0)    " +
                            " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Feedback No Already exits.'  Return     End	 " +
                            " begin    " +
                            " Insert Into MSTFEEDBACK (FEEDNO,FEEDDATE,CUSTRID,CUSTADD,CUSTCONTNO,FOODFLAVOUR,FOODPRESENTATION, " +
                            " FOODVALUEFORMONEY,FOODFRESHNESS,FOODCHOICEOFMENU,SERVICEFRIENDLY,SERVICEPROFESSIONAL,SERVICEEXPLATION, " +
                            " SERVICETIMETAKEN,SERVICEACCOUNT,VENUEATMOSPHERE,VENUECLEANLINESS,VENUESTAFF,GENERALFOOD,GENERALOVERALLSERVICE, " +
                            " GENERALCLEANLINESS,GENERALORDER,GENERALSPEED,GENERALVALUE,GENERALOVREXP,GENERALOVRRATING,WDHEARABOUTUS,WDHEARABOUTUSOTHER,SUGG,	 " +
                            " AUSERID,ADATETIME,DELFLG)	" +
                            " Values (@p_FEEDNO,@p_FEEDDATE,@p_CUSTRID,@p_CUSTADD,@p_CUSTCONTNO,@p_FOODFLAVOUR,@p_FOODPRESENTATION, " +
                            " @p_FOODVALUEFORMONEY,@p_FOODFRESHNESS,@p_FOODCHOICEOFMENU,@p_SERVICEFRIENDLY,@p_SERVICEPROFESSIONAL,@p_SERVICEEXPLATION, " +
                            " @p_SERVICETIMETAKEN,@p_SERVICEACCOUNT,@p_VENUEATMOSPHERE,@p_VENUECLEANLINESS,@p_VENUESTAFF,@p_GENERALFOOD,@p_GENERALOVERALLSERVICE, " +
                            " @p_GENERALCLEANLINESS,@p_GENERALORDER,@p_GENERALSPEED,@p_GENERALVALUE,@p_GENERALOVREXP,@p_GENERALOVRRATING,@p_WDHEARABOUTUS,@p_WDHEARABOUTUSOTHER,@p_SUGG, " +
                            " @p_userid,getdate(),0) " +
                            " set @p_id=SCOPE_IDENTITY()    " +
                            " End  End    else if (@p_mode=1)  " +
                            " begin declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0   " +
                            " select @codeRowCount1 = (select count(*) from MSTFEEDBACK " +
                            " where FEEDNO = @p_FEEDNO and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                            " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Feedback No Already exits.'   " +
                            " Return   End " +
                            " begin " +
                            " Update MSTFEEDBACK set FEEDNO=@p_FEEDNO,FEEDDATE=@p_FEEDDATE,CUSTRID=@p_CUSTRID, " +
                            " CUSTADD=@p_CUSTADD,CUSTCONTNO=@p_CUSTCONTNO,FOODFLAVOUR = @p_FOODFLAVOUR,FOODPRESENTATION=@p_FOODPRESENTATION, " +
                            " FOODVALUEFORMONEY = @p_FOODVALUEFORMONEY,FOODFRESHNESS = @p_FOODFRESHNESS, " +
                            " FOODCHOICEOFMENU = @p_FOODCHOICEOFMENU,SERVICEFRIENDLY=@p_SERVICEFRIENDLY,SERVICEPROFESSIONAL=@p_SERVICEPROFESSIONAL," +
                            " SERVICEEXPLATION=@p_SERVICEEXPLATION,SERVICETIMETAKEN=@p_SERVICETIMETAKEN,SERVICEACCOUNT=@p_SERVICEACCOUNT, " +
                            " VENUEATMOSPHERE=@p_VENUEATMOSPHERE,VENUECLEANLINESS=@p_VENUECLEANLINESS,VENUESTAFF=@p_VENUESTAFF, " +
                            " GENERALFOOD=@p_GENERALFOOD,GENERALOVERALLSERVICE=@p_GENERALOVERALLSERVICE,GENERALCLEANLINESS=@p_GENERALCLEANLINESS, " +
                            " GENERALORDER=@p_GENERALORDER,GENERALSPEED=@p_GENERALSPEED,GENERALVALUE=@p_GENERALVALUE,GENERALOVREXP=@p_GENERALOVREXP, " +
                            " GENERALOVRRATING=@p_GENERALOVRRATING,WDHEARABOUTUS=@p_WDHEARABOUTUS,WDHEARABOUTUSOTHER=@p_WDHEARABOUTUSOTHER,SUGG=@p_SUGG, " +
                            " euserid = @p_userid,edatetime = getdate() where rid = @p_rid  End  End  End  End  " +
                             " try  begin catch   SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0    Return  END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_BILLREGISTER");
                strproc = "";
                strproc = "CREATE procedure sp_BILLREGISTER " +
                            " (@p_fromdate datetime,@p_todate datetime) " +
                            " as " +
                            " begin   " +
                            " select BILL.RID,BILL.BILLDATE,BILL.BILLNO,BILL.BILLTYPE,BILL.BILLPAX,BILL.BILLTIME, " +
                            " MSTTABLE.TABLENAME,BILL.TOTAMOUNT,BILL.TOTSERTAXAMOUNT,BILL.TOTVATAMOUNT,bill.TOTBEVVATAMT,bill.TOTLIQVATAMT, " +
                            " BILL.TOTADDVATAMOUNT,BILL.TOTDISCAMOUNT,BILL.NETAMOUNT,bill.TOTROFF,bill.REFBILLNO,bill.REFBILLNUM," +
                            " BILLDTL.RID AS BILLDTLRID,BILL.TOTADDDISCAMT,BILL.TOTSERCHRAMT,BILL.TOTKKCESSAMT,BILL.TOTKKCESSPER,BILL.TOTGSTAMT, " +
                            " MSTITEM.INAME,BILLDTL.IQTY,BILLDTL.IRATE,BILLDTL.IPAMT,BILLDTL.IAMT, " +
                            " BILLDTL.CGSTPER,BILLDTL.CGSTAMT,BILLDTL.SGSTPER,BILLDTL.SGSTAMT,BILLDTL.IGSTPER,BILLDTL.IGSTAMT,BILLDTL.GSTPER,BILLDTL.GSTAMT,BILLDTL.DISCPER,BILLDTL.DISCAMT " +
                            " FROM BILL " +
                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID = BILL.RID) " +
                            " LEFT JOIN MSTTABLE ON (MSTTABLE.RID = BILL.TABLERID) " +
                            " LEFT JOIN MSTITEM ON (MSTITEM.RID = BILLDTL.IRID) " +
                            " WHERE " +
                            " BILL.BILLDATE between @p_fromdate and @p_todate and  " +
                            " isnull(BILL.delflg,0)=0 and isnull(BILLDTL.delflg,0)=0" +
                            " and isnull(BILL.ISREVISEDBILL,0)=0 " +
                            " ORDER BY BILL.BILLDATE " +
                            " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLREG");
                strproc = "";
                strproc = " Create procedure sp_BILLREG " +
                        " (@p_fromdate datetime,@p_todate datetime)" +
                        " AS " +
                        " begin " +
                        " SELECT BILL.rid,BILL.billno,BILL.billdate,bill.BILLTIME,bill.billtype,bill.billpax,bill.REFBILLNO,bill.REFBILLNUM,MSTTABLE.TABLENAME, " +
                        " BILL.totamount,bill.totsertaxamount,BILL.totvatamount, BILL.totaddvatper as totsercharge,BILL.totdiscamount,BILL.TOTBEVVATAMT,BILL.TOTLIQVATAMT, " +
                        " BILL.TOTADDDISCAMT,BILL.TOTSERCHRAMT,BILL.netamount,BILL.TOTKKCESSAMT,BILL.TOTADDVATAMOUNT,BILL.TOTROFF, " +
                        " BILL.TOTGSTAMT,BILL.CGSTAMT,BILL.SGSTAMT,BILL.IGSTAMT " +
                        " FROM BILL " +
                        " LEFT JOIN MSTTABLE ON (MSTTABLE.rid = BILL.tablerid) " +
                        " WHERE bill.billDATE between @p_fromdate and @p_todate  " +
                        " AND isnull(bill.delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0 " +
                        " ORDER BY bill.billDATE,bill.rid " +
                        " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLREVISED");
                strproc = "";
                strproc = "create procedure sp_BILLREVISED  " +
                            " (@p_mode as int, @p_rid as bigint, @p_billno nvarchar(50), @p_billdate datetime,	 " +
                            " @p_billtype nvarchar(50), @p_custrid bigint, @p_custname nvarchar(250), " +
                            " @p_custcontno nvarchar(50), @p_tablerid bigint, @p_pricelistrid bigint, " +
                            " @p_billpax bigint, @p_totamount decimal(18,2), @p_totdiscountableamount decimal(18,2), " +
                            " @p_totsertaxper decimal(18,2), @p_totsertaxamount decimal(18,2), @p_totvatper decimal(18,2), " +
                            " @p_totvatamount decimal(18,2), @p_totaddvatper decimal(18,2), @p_totaddvatamount decimal(18,2), " +
                            " @p_totdiscper decimal(18,2), @p_totdiscamount decimal(18,2), @P_totroff decimal(18,2), " +
                            " @p_netamount decimal(18,2), @p_billprepby nvarchar(100), @p_billremark nvarchar(max), " +
                            " @p_setletype nvarchar(50), @p_setleamount decimal(18,2), @p_chequeno nvarchar(100), " +
                            " @p_chequebankname nvarchar(100), @p_creditcardno nvarchar(100), @p_creditholdername nvarchar(100), " +
                            " @p_creditbankname nvarchar(100), @p_refbillno nvarchar(50), @p_refbillnum bigint, @p_custadd nvarchar(max), " +
                            " @p_isrevisedbill bigint,@p_revisedbillsrno bigint,@p_revisedbillno bigint,@p_basebillno bigint,@p_mainbasebillno bigint, " +
                            " @p_billtime datetime,@p_billorderperid bigint, @p_tokenno bigint,@p_totcalcvatper decimal(18,2), " +
                            " @p_totcalcvatamount decimal(18,2),@p_isbilltocustomer bit,@p_cntprint bigint,@p_isparcelbill bit,@p_totbevvatper decimal(18,3),@p_totbevvatamt decimal(18,2), " +
                            " @p_totliqvatper decimal(18,3), " +
                            " @p_totliqvatamt decimal(18,2)," +
                            " @p_totserchrper decimal(18,3),@p_totserchramt decimal(18,3),@p_totnewtotalamt decimal(18,3), @p_totadddiscamt decimal(18,3),@p_totkkcessper decimal(18,3),@p_totkkcessamt decimal(18,3)," +
                            " @p_cgstamt decimal(18,3),@p_sgstamt decimal(18,3),@p_igstamt decimal(18,3), @p_totgstamt decimal(18,3)," +
                            " @p_userid bigint,  @p_errstr as nvarchar(max) out, @p_retval as int out, @p_id as bigint out ) as " +
                            " begin  " +
                            " try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   if (@p_mode=0)  " +
                            " begin " +
                            " Insert Into Bill (billno,billdate,billtype,custrid,custname,custcontno,tablerid,pricelistrid,billpax,totamount, " +
                             " totdiscountableamount,totsertaxper,totsertaxamount,totvatper,totvatamount,totaddvatper,totaddvatamount,totdiscper, " +
                            " totdiscamount,totroff,netamount,billprepby,billremark,setletype,setleamount,chequeno,chequebankname,creditcardno, " +
                            " creditholdername,creditbankname,refbillno,refbillnum,custadd, " +
                            " isrevisedbill,revisedbillsrno,revisedbillno,basebillno,mainbasebillno," +
                            " billtime,billorderperid,tokenno,totcalcvatper,totcalcvatamount,isbilltocustomer,cntprint,isparcelbill,totbevvatper,totbevvatamt,totliqvatper,totliqvatamt, " +
                            " totserchrper,totserchramt,totnewtotalamt,totadddiscamt,totkkcessper,totkkcessamt," +
                            " cgstamt,sgstamt,igstamt,totgstamt," +
                            " auserid,adatetime,DelFlg)  " +
                            " Values ( @p_billno,@p_billdate,@p_billtype,@p_custrid,@p_custname,@p_custcontno,@p_tablerid, " +
                            " @p_pricelistrid,@p_billpax, @p_totamount,@p_totdiscountableamount,@p_totsertaxper, " +
                            " @p_totsertaxamount,@p_totvatper,@p_totvatamount,@p_totaddvatper,@p_totaddvatamount, " +
                            " @p_totdiscper,@p_totdiscamount,@p_totroff,@p_netamount,@p_billprepby,@p_billremark, " +
                            " @p_setletype,@p_setleamount,@p_chequeno,@p_chequebankname, @p_creditcardno,@p_creditholdername, " +
                            " @p_creditbankname, @p_refbillno,@p_refbillnum,@p_custadd, " +
                            " @p_isrevisedbill,@p_revisedbillsrno,@p_revisedbillno,@p_basebillno,@p_mainbasebillno, " +
                            " @p_billtime,@p_billorderperid,@p_tokenno," +
                            " @p_totcalcvatper,@p_totcalcvatamount,@p_isbilltocustomer,@p_cntprint,@p_isparcelbill," +
                            " @p_totbevvatper,@p_totbevvatamt,@p_totliqvatper,@p_totliqvatamt, " +
                            " @p_totserchrper,@p_totserchramt,@p_totnewtotalamt,@p_totadddiscamt,@p_totkkcessper,@p_totkkcessamt," +
                            " @p_cgstamt,@p_sgstamt,@p_igstamt,@p_totgstamt," +
                            " @p_userid,getdate(),0 )  " +
                            " set @p_id=SCOPE_IDENTITY() End  " +
                            " else if (@p_mode=1)    " +
                            " begin set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                            " End " +
                            " End " +
                            " End " +
                            " Try " +
                            " begin catch  " +
                            " SELECT ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage      " +
                            " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                            " Return   " +
                            " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_DATEWISEBILLINGREG");
                strproc = "";
                strproc = "create procedure sp_DATEWISEBILLINGREG  " +
                          " (@p_fromdate datetime,@p_todate datetime) as " +
                          " BEGIN  " +
                          " select bill.BILLDATE, " +
                          " sum(case when bill.billDATE between @p_fromdate and @p_todate  then isnull(Netamount,0) else 0 end )as Billing, " +
                          " sum(case when settlement1.SETLEAMOUNT > 0 then isnull(settlement1.SETLEAMOUNT,0) else 0 end )as Settlement " +
                          " from bill " +
                          " left join (select BILLRID, sum(isnull(SETLEAMOUNT,0)) as SETLEAMOUNT from settlement where isnull(settlement.delflg,0)=0 " +
                                        " And settlement.setledate between @p_fromdate and @p_todate " +
                                        " group by BILLRID) as settlement1 on (settlement1.BILLRID = bill.rid) " +
                          " where isnull(bill.delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0 and bill.billDATE between @p_fromdate and @p_todate" +
                          " group by bill.BILLDATE " +
                          " order by bill.BILLDATE " +
                          " END  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_REVBILLREG");
                strproc = "";
                strproc = "create procedure sp_REVBILLREG  " +
                            " (@p_fromdate datetime,@p_todate datetime) as  " +
                            " begin  select bill.rid,bill.billno,bill.REFBILLNO,bill.billdate,bill.billtype,bill.billpax, " +
                            " bill.REVISEDBILLSRNO,bill.REVISEDBILLNO,bill.BASEBILLNO,bill.MAINBASEBILLNO," +
                            " MSTTABLE.TABLENAME, " +
                            " bill.totamount,bill.totsertaxamount,bill.totvatamount, bill.totaddvatper as totsercharge, BILL.TOTGSTPER,BILL.TOTGSTAMT," +
                            " bill.totdiscamount,bill.netamount " +
                            " FROM Bill  " +
                            " left join MSTTABLE on (MSTTABLE.rid =bill.tablerid)  " +
                            " WHERE bill.billDATE between @p_fromdate and @p_todate " +
                            " and isnull(bill.delflg,0)=0 And isnull(REVISEDBILLSRNO,0)>0 " +
                            " order by bill.billDATE,bill.rid  end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTBANQ");
                strproc = "";
                strproc = "CREATE  procedure sp_MSTBANQ" +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid bigint, " +
                         " @p_banqcode nvarchar(20), " +
                         " @p_banqname nvarchar(250),	" +
                         " @p_banqdesc nvarchar(max), " +
                        " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out,  " +
                        "  @p_retval as int out, " +
                        " @p_id as bigint out " +
                        " ) as  " +
                        "  begin " +
                        " try " +
                        " begin " +
                        "  if (@p_mode=0) " +
                        "  begin  " +
                        "  declare @codeRowCount as int " +
                        " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                        " select @codeRowCount = (select count(*) from mstbanq where banqcode = @p_banqcode and ISNULL(DelFlg,0)=0)  " +
                        " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Banquate Code Already exits.'     " +
                           "  Return    " +
                        " End	" +
                  " Begin  " +
                     " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                     " select  @nameRowCount = (select count(*) from mstbanq where banqname = @p_banqname and ISNULL(DelFlg,0)=0) " +
                     "  if ( @nameRowCount > 0)    " +
                     " begin    " +
                     " set @p_Retval = 1 set @p_Errstr ='Banquate Name Already exits.'" +
                     " Return  " +
                              " End  " +
                           "  end " +
                                         " begin " +
                                          " Insert Into mstbanq (banqcode,banqname,banqdesc,auserid,adatetime,DelFlg) " +
                                         " Values (@p_banqcode,@p_banqname,@p_banqdesc,@p_userid,getdate(),0)" +
                                            " set @p_id=SCOPE_IDENTITY() " +
                                        "  End  End  " +
                                " else if (@p_mode=1) " +
                                  "    begin    " +
                                     " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0   " +
                                     " select @codeRowCount1 = (select count(*) from mstbanq where banqcode = @p_banqcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                     " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Banquate Code Already exits.'      " +
                                    "  Return   End  " +
                             " Begin  " +
                                 " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0 " +
                                 " select  @nameRowCount1 = (select count(*) from mstbanq where banqname = @p_banqname and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                                  " if ( @nameRowCount1 > 0)    " +
                                 " begin   " +
                                  " set @p_Retval = 1 set @p_Errstr ='Banquate Name Already exits.' " +
                                  " Return  " +
                               " End  END " +
                            "  begin  " +
                                     " update mstbanq set banqcode=@p_banqcode, " +
                                     " banqname = @p_banqname, " +
                                    "  banqdesc = @p_banqdesc, " +
                                    "  euserid = @p_userid,edatetime = getdate()  " +
                                     " where rid = @p_rid    " +
                                   "   End  End  End  " +
                                    "	End  " +
                                   "   try  begin catch  " +
                                     " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                       "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                    " Return  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTITEM");
                strproc = "";
                strproc = "create  procedure sp_MSTITEM  " +
                       " ( " +
                        " @p_mode as int, " +
                        " @p_rid bigint, " +
                        " @p_icode nvarchar(20), " +
                        " @p_iname nvarchar(250)," +
                        " @p_icommi decimal(18,2)," +
                        " @p_ipname nvarchar(200)," +
                        " @p_iimg image," +
                        " @p_igrprid bigint," +
                        " @p_iunitrid bigint," +
                        " @p_ideptrid bigint," +
                        " @p_irate decimal(18,2)," +
                        " @p_idesc nvarchar(max), " +
                        " @p_iminqty decimal(18,2)," +
                        " @p_imaxqty decimal(18,2)," +
                        " @p_ireqty decimal(18,2)," +
                        " @p_isitemstock bit," +
                        " @p_reportdeptrid bigint," +
                        " @p_ISHIDEITEM bigint," +
                        " @p_ISNOTAPPSERTAXDISC BIGINT," +
                        " @p_idprate decimal(18,2)," +
                        " @p_isrunningitem bit," +
                        " @p_disdiffcolor bit," +
                        " @p_itemtaxtype nvarchar(50)," +
                        " @p_ischkrptitem bit," +
                        " @p_isnotapplyvat bit," +
                        " @p_couponcode nvarchar(20)," +
                        " @p_irewpoint decimal(18,3), " +
                        " @p_notapplygsttax bit," +
                        " @p_hsncoderid bigint, " +
                        " @p_notapplydisc bit," +
                        " @p_ibackcolor bigint, " +
                        " @p_iforecolor bigint, " +
                        " @p_ifontname nvarchar(200)," +
                        " @p_ifontsize float," +
                        " @p_ifontbold bit," +
                        " @p_reglangname nvarchar(200)," +
                        " @p_iremark nvarchar(2000)," +
                        " @p_ipurrate decimal(18,3)," +
                        " @p_nut1 nvarchar(200)," +
                        " @p_nut2 nvarchar(200)," +
                        " @p_nut3 nvarchar(200)," +
                        " @p_nut4 nvarchar(200)," +
                        " @p_nut5 nvarchar(200)," +
                        " @p_nut6 nvarchar(200)," +
                        " @p_nut7 nvarchar(200)," +
                        " @p_userid bigint, " +
                        " @p_errstr as nvarchar(max) out, " +
                        " @p_retval as int out," +
                        " @p_id as bigint out" +
                        " ) as " +
                        " begin " +
                        " try " +
                        " begin " +
                       "  if (@p_mode=0) " +
                       "  begin  " +
                       "  declare @codeRowCount as int  " +
                        " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                        " select @codeRowCount = (select count(*) from mstitem where icode = @p_icode and ISNULL(DelFlg,0)=0)   " +
                       "  if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Code Already exits.' " +
                           "  Return    " +
                       "  End " +
                  " Begin" +
                     " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                     " select  @nameRowCount = (select count(*) from mstitem where iname = @p_iname and ISNULL(DelFlg,0)=0)  " +
                    "  if ( @nameRowCount > 0)   " +
                     " begin    " +
                     " set @p_Retval = 1 set @p_Errstr ='Item Name Already exits.'    " +
                    "  Return  " +
                             "  End   " +
                            " end " +
                                         " begin " +
                                         " Insert Into mstitem (icode,iname,icommi,ipname,iimg,igrprid,iunitrid,ideptrid,irate,idesc,iminqty,imaxqty,ireqty,isitemstock, " +
                                            " reportdeptrid,ishideitem,ISNOTAPPSERTAXDISC, " +
                                            " idprate,isrunningitem,disdiffcolor,itemtaxtype,ischkrptitem, isnotapplyvat,couponcode,irewpoint,notapplygsttax," +
                                            " hsncoderid,notapplydisc,ibackcolor,iforecolor,ifontname,ifontsize,ifontbold,reglangname,iremark, " +
                                            " IPURRATE,NUT1,NUT2,NUT3,NUT4,NUT5,NUT6,NUT7," +
                                            " senddata,auserid,adatetime,DelFlg) " +
                                                "  Values (@p_icode,@p_iname,@p_icommi,@p_ipname,@p_iimg,@p_igrprid,@p_iunitrid,@p_ideptrid,@p_irate,@p_idesc, " +
                                                    " @p_iminqty,@p_imaxqty,@p_ireqty,@p_isitemstock," +
                                                    " @p_reportdeptrid,@p_ISHIDEITEM,@p_ISNOTAPPSERTAXDISC,@p_idprate,@p_isrunningitem,@p_disdiffcolor," +
                                                    " @p_itemtaxtype,@p_ischkrptitem, @p_isnotapplyvat,@p_couponcode,@p_irewpoint,@p_notapplygsttax," +
                                                    " @p_hsncoderid,@p_notapplydisc,@p_ibackcolor,@p_iforecolor,@p_ifontname,@p_ifontsize,@p_ifontbold,@p_reglangname,@p_iremark, " +
                                                    " @p_IPURRATE,@p_NUT1,@p_NUT2,@p_NUT3,@p_NUT4,@p_NUT5,@p_NUT6,@p_NUT7," +
                                                    " 0,@p_userid,getdate(),0)" +
                                            " set @p_id=SCOPE_IDENTITY()" +
                                         " End  End  " +
                                " else if (@p_mode=1) " +
                                     " begin  " +
                                    "  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0 " +
                                     " select @codeRowCount1 = (select count(*) from mstitem where icode = @p_icode and rid <> @p_rid and ISNULL(DelFlg,0)=0 ) " +
                                     " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Code Already exits.'   " +
                                     " Return   End " +
                            "  Begin  " +
                                 "  declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0      " +
                                 " select  @nameRowCount1 = (select count(*) from mstitem where iname = @p_iname and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                                 " if ( @nameRowCount1 > 0)   " +
                                 " begin    " +
                                 " set @p_Retval = 1 set @p_Errstr ='Item Name Already exits.' " +
                                 " Return  " +
                              " End  END " +
                             " begin  " +
                                      " Update mstitem set icode=@p_icode, " +
                                     "  iname = @p_iname, icommi=@p_icommi,ipname=@p_ipname," +
                                     "  iimg = @p_iimg," +
                                     "  igrprid = @p_igrprid,iunitrid = @p_iunitrid," +
                                     "  ideptrid = @p_ideptrid,irate = @p_irate," +
                                     "  idesc = @p_idesc, " +
                                     " iminqty = @p_iminqty,imaxqty = @p_imaxqty,ireqty=@p_ireqty," +
                                     " isitemstock=@p_isitemstock," +
                                     " reportdeptrid=@p_reportdeptrid," +
                                     " ISHIDEITEM=@p_ISHIDEITEM,ISNOTAPPSERTAXDISC=@p_ISNOTAPPSERTAXDISC," +
                                     " idprate=@p_idprate,isrunningitem=@p_isrunningitem," +
                                     " disdiffcolor = @p_disdiffcolor, itemtaxtype=@p_itemtaxtype,ischkrptitem=@p_ischkrptitem," +
                                     " isnotapplyvat=@p_isnotapplyvat,couponcode=@p_couponcode," +
                                     " irewpoint = @p_irewpoint,notapplygsttax=@p_notapplygsttax," +
                                     " hsncoderid=@p_hsncoderid,notapplydisc=@p_notapplydisc,ibackcolor=@p_ibackcolor,iforecolor=@p_iforecolor,ifontname=@p_ifontname,ifontsize=@p_ifontsize,ifontbold=@p_ifontbold, " +
                                     " reglangname=@p_reglangname,iremark=@p_iremark," +
                                     " IPURRATE=@p_IPURRATE,NUT1=@p_NUT1,NUT2=@p_NUT2,NUT3=@p_NUT3,NUT4=@p_NUT4,NUT5=@p_NUT5,NUT6=@p_NUT6,NUT7=@p_NUT7," +
                                     " senddata=0,euserid = @p_userid,edatetime = getdate() " +
                                     " where rid = @p_rid  " +
                                     "  End  End  End	" +
                                     "	End  " +
                                     " try  begin catch  " +
                                     "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; " +
                                       "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                    " Return  END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTINVITEM");
                strproc = "";
                strproc = "CREATE procedure sp_MSTINVITEM " +
                            " ( @p_mode as int,   @p_rid bigint,   @p_icode nvarchar(50),   @p_iname nvarchar(200)," +
                            " @p_idesc nvarchar(max),  @p_userid bigint,   @p_errstr as nvarchar(max) out,  @p_retval as int out, " +
                            " @p_id as bigint out  ) as  begin   try  begin  if (@p_mode=0)   begin    declare @codeRowCount as int   set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                            " select @codeRowCount = (select count(*) from MSTINVITEM where icode = @p_icode and ISNULL(DelFlg,0)=0)  " +
                            " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Code Already exits.'     " +
                            " Return      End	  " +
                            " Begin   SET NOCOUNT ON   declare  @nameRowCount int   " +
                            " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                            " select  @nameRowCount = (select count(*) from MSTINVITEM where iname = @p_iname and ISNULL(DelFlg,0)=0)    " +
                            " if ( @nameRowCount > 0)      begin      set @p_Retval = 1 set @p_Errstr ='Item Name Already exits.'   " +
                            " Return  End  end " +
                            " begin   Insert Into MSTINVITEM (icode,iname,idesc,auserid,adatetime,DelFlg)   " +
                            " Values (@p_icode,@p_iname,@p_idesc,@p_userid,getdate(),0) 	" +
                            " set @p_id=SCOPE_IDENTITY()	End  End   " +
                            " else if (@p_mode=1)   " +
                            " begin    declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0      " +
                            " select @codeRowCount1 = (select count(*) from MSTINVITEM where icode = @p_icode and rid <> @p_rid  and ISNULL(DelFlg,0)=0) " +
                            " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Code Already exits.'    Return   End   " +
                            " Begin      declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0   " +
                            " select  @nameRowCount1 = (select count(*) from MSTINVITEM where iname = @p_iname and rid <> @p_rid and ISNULL(DelFlg,0)=0)     " +
                             " if ( @nameRowCount1 > 0)     " +
                            " begin    set @p_Retval = 1 set @p_Errstr ='Item Name Already exits.'    Return    End  END  " +
                            " begin    update MSTINVITEM set icode=@p_icode,   iname = @p_iname, idesc = @p_idesc,euserid = @p_userid,edatetime = getdate()     " +
                            " where rid = @p_rid      End  End  End    End     " +
                            " try  begin catch      SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                            " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BANQBOOKING");
                strproc = "";
                strproc = "CREATE  procedure sp_BANQBOOKING " +
                            " (@p_mode as int,  @p_rid bigint, @p_boregdate datetime, @p_bono nvarchar(100),@p_borefno nvarchar(50),@p_banqrid bigint,	 " +
                            " @p_bodate datetime,@p_botime datetime,@p_boamt decimal(18,2),@p_bodeposit decimal(18,2),@p_bonoofper bigint," +
                            " @p_boconf nvarchar(50),@p_botypeoffunc nvarchar(2000),@p_custrid bigint, @p_custmobno nvarchar(50)," +
                            " @p_boremark nvarchar(max),@p_perpaxrate decimal(18,2),@p_refby nvarchar(500),@p_menutype nvarchar(500)," +
                            " @p_boothercharges nvarchar(max),@p_decoinfo nvarchar(max),@p_spinst nvarchar(max),@p_followupdt datetime,@p_botakenby nvarchar(200),@p_boentryby nvarchar(200), " +
                            " @p_userid bigint,  @p_errstr as nvarchar(max) out,  @p_retval as int out,  @p_id as bigint out ) as " +
                            " begin  " +
                            " try  " +
                            " begin   " +
                             " if (@p_mode=0)   " +
                                " begin   declare @codeRowCount as int  set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                                " Insert Into BANQBOOKING (BOREGDATE,BONO,BOREFNO,BANQRID,BODATE,BOTIME,BOAMT,BODEPOSIT,BONOOFPER,BOCONF,BOTYPEOFFUNC," +
                                " CUSTRID,CUSTMOBNO,BOREMARK,PERPAXRATE,REFBY,MENUTYPE," +
                                " boothercharges,decoinfo,spinst,followupdt,botakenby,boentryby," +
                                " auserid,adatetime,DelFlg)  " +
                                " Values (@p_BOREGDATE,@p_BONO,@p_borefno,@p_BANQRID,@p_BODATE,@p_BOTIME,@p_BOAMT,@p_BODEPOSIT,@p_BONOOFPER,@p_BOCONF,@p_BOTYPEOFFUNC," +
                                " @p_CUSTRID,@p_CUSTMOBNO,@p_BOREMARK," +
                                " @p_PERPAXRATE,@p_REFBY,@p_MENUTYPE," +
                                " @p_boothercharges,@p_decoinfo,@p_spinst,@p_followupdt,@p_botakenby,@p_boentryby," +
                                " @p_userid,getdate(),0) " +
                                " set @p_id=SCOPE_IDENTITY()   " +
                                " End  " +
                            " else if (@p_mode=1)     " +
                                " begin  " +
                                " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0    " +
                                " update BANQBOOKING set BOREGDATE=@p_BOREGDATE,BONO=@p_BONO,BOREFNO=@p_borefno,BANQRID=@p_BANQRID,BODATE=@p_BODATE,BOTIME=@p_BOTIME,BOAMT=@p_BOAMT,BODEPOSIT=@p_BODEPOSIT,BONOOFPER=@p_BONOOFPER,BOCONF=@p_BOCONF,BOTYPEOFFUNC=@p_BOTYPEOFFUNC," +
                                " CUSTRID=@p_CUSTRID,CUSTMOBNO=@p_CUSTMOBNO,BOREMARK=@p_BOREMARK,   " +
                                " PERPAXRATE=@p_PERPAXRATE,REFBY=@p_REFBY,MENUTYPE=@p_MENUTYPE," +
                                " boothercharges=@p_boothercharges,decoinfo=@p_decoinfo,spinst=@p_spinst,followupdt=@p_followupdt,botakenby=@p_botakenby,boentryby=@p_boentryby," +
                                " euserid = @p_userid,edatetime = getdate() where rid = @p_rid       " +
                                " End  " +
                            " end  " +
                            " end    " +
                            " try  begin catch   SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                            " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return  " +
                            " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BANQBOOKINGDTL");
                strproc = "";
                strproc = "create  procedure sp_BANQBOOKINGDTL" +
                        " ( @p_mode as int,  @p_rid as bigint, @p_borid bigint, @p_banqirid bigint, @p_idesc nvarchar(MAX)," +
                        " @p_userid bigint,  @p_errstr as nvarchar(max) out,  @p_retval as int out, @p_id as bigint out  ) as  " +
                        " begin  " +
                        " try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   if (@p_mode=0)   " +
                        " begin 	Insert Into BANQBOOKINGDTL (borid,banqirid,idesc, auserid,adatetime,DelFlg)   " +
                        " Values ( @p_borid,@p_banqirid,@p_idesc, @p_userid,getdate(),0 )  " +
                        " set @p_id=SCOPE_IDENTITY() End   " +
                        " else if (@p_mode=1)     " +
                        " begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                        " update BANQBOOKINGDTL  set borid=@p_borid,banqirid = @p_banqirid,idesc=@p_idesc, euserid = @p_userid,edatetime = getdate()     " +
                        " where rid = @p_rid and borid=@p_borid  End  End  end	" +
                        " try   begin catch    SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;    " +
                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0   Return    END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTBANQITEM");
                strproc = "";
                strproc = "CREATE  procedure sp_MSTBANQITEM   " +
                            " ( @p_mode as int,  @p_rid bigint,  @p_bqicode nvarchar(20),  " +
                            " @p_bqiname nvarchar(250), @p_bqipname nvarchar(200), " +
                            " @p_bqirate decimal(18,2), @p_bqidesc nvarchar(max),  @p_bqiimg image, " +
                            " @p_hsncoderid bigint," +
                            " @p_userid bigint,  @p_errstr as nvarchar(max) out,  @p_retval as int out, @p_id as bigint out ) as  " +
                            " begin  try  begin   if (@p_mode=0)  " +
                            " begin    declare @codeRowCount as int   set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                            " select @codeRowCount = (select count(*) from MSTBANQITEM where BQICODE = @p_BQICODE and ISNULL(DelFlg,0)=0)   " +
                            " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Code Already exits.'   Return      End  " +
                            " Begin declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                            " select  @nameRowCount = (select count(*) from MSTBANQITEM where BQINAME = @p_bqiname and ISNULL(DelFlg,0)=0)    " +
                            " if ( @nameRowCount > 0)    begin     set @p_Retval = 1 set @p_Errstr ='Item Name Already exits.'  " +
                            " Return    End    end  " +
                            " begin  Insert Into MSTBANQITEM (BQICODE,BQINAME,BQIPNAME,BQIRATE,BQIDESC,BQIIMG,hsncoderid,auserid,adatetime,DelFlg)   " +
                            " Values (@p_BQICODE,@p_BQINAME,@p_BQIPNAME,@p_BQIRATE,@p_BQIDESC,@p_BQIIMG,@p_hsncoderid,@p_userid,getdate(),0) " +
                            " set @p_id=SCOPE_IDENTITY() End  End   " +
                            " else if (@p_mode=1)  begin  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0 " +
                            " select @codeRowCount1 = (select count(*) from MSTBANQITEM where BQICODE = @p_BQICODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                            " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Item Code Already exits.'   Return   End  " +
                            " Begin    declare  @nameRowCount1 int   set @p_Errstr=''  set @p_Retval=0       " +
                            " select  @nameRowCount1 = (select count(*) from MSTBANQITEM where BQINAME = @p_BQINAME and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                            " if ( @nameRowCount1 > 0)    begin     set @p_Retval = 1 set @p_Errstr ='Item Name Already exits.'  Return   End  END  " +
                            " begin   " +
                            " Update MSTBANQITEM set BQICODE=@p_BQICODE, BQINAME = @p_BQINAME, BQIPNAME=@p_BQIPNAME," +
                            " BQIRATE=@p_BQIRATE, BQIDESC=@p_BQIDESC, BQIIMG = @p_BQIIMG,hsncoderid=@p_hsncoderid, " +
                            " euserid = @p_userid,edatetime = getdate() where rid = @p_rid  End  End  End End  " +
                             " try  begin catch    SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                            " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return  " +
                            " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_CAPTAINCOMMIREGISTER");
                strproc = "";
                strproc = " CREATE procedure sp_CAPTAINCOMMIREGISTER (@p_fromdate datetime,@p_todate datetime)  AS  " +
                            " begin " +
                            " select BILL.RID,BILL.BILLDATE,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLTYPE,BILL.BILLPAX,  " +
                            " MSTTABLE.TABLENAME,BILL.TOTAMOUNT,BILL.TOTSERTAXAMOUNT,BILL.TOTVATAMOUNT, " +
                            " BILL.TOTADDVATAMOUNT,BILL.TOTDISCAMOUNT,BILL.NETAMOUNT,  BILLDTL.RID AS BILLDTLRID,  " +
                            " MSTITEM.INAME,BILLDTL.IQTY,BILLDTL.IRATE,BILLDTL.IPAMT,BILLDTL.IAMT," +
                            " ((ISNULL(BILLDTL.IQTY,0)) * (ISNULL(BILLDTL.IPAMT,0))) AS COMMIAMT,ISNULL(MSTEMP.EMPNAME,'')AS CAPTAINNAME,ISNULL(MSTEMP.RID,0)AS CAPTAINRID" +
                            " From BILL  " +
                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID = BILL.RID)  " +
                            " LEFT JOIN MSTTABLE ON (MSTTABLE.RID = BILL.TABLERID) " +
                            " LEFT JOIN MSTITEM ON (MSTITEM.RID = BILLDTL.IRID)" +
                            " LEFT JOIN MSTEMP ON (MSTEMP.RID = BILL.BILLORDERPERID)" +
                            " WHERE " +
                            " BILL.BILLDATE between @p_fromdate and @p_todate and " +
                            " isnull(BILL.delflg,0)=0 and isnull(BILLDTL.delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0 and ISNULL(BILL.BILLORDERPERID,0)>0 " +
                            " and  ((ISNULL(BILLDTL.IQTY,0)) * (ISNULL(BILLDTL.IPAMT,0))) > 0" +
                            " order by MSTEMP.EMPNAME,BILL.BILLDATE" +
                            " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_PENTABLEINFO");
                strproc = "";
                strproc = "CREATE procedure sp_PENTABLEINFO" +
                         "(" +
                         " @p_mode int," +
                         " @p_rid as bigint," +
                         " @p_kotrid bigint," +
                         " @p_kotdate datetime," +
                         " @p_kottime datetime,	" +
                         " @p_tablerid bigint," +
                         " @p_tablename nvarchar(200)," +
                         " @p_tablestatus bigint," +
                         " @p_billrid bigint, " +
                         " @p_iscompkot bit, " +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         "  begin " +
                         " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0) " +
                                    " begin 	" +
                                        " Insert Into PENTABLEINFO (kotrid,kotdate,kottime,tablerid,tablename,tablestatus,billrid,iscompkot,auserid,adatetime,DelFlg) " +
                                        " Values (@p_kotrid,@p_kotdate,@p_kottime,@p_tablerid,@p_tablename,@p_tablestatus,@p_billrid,@p_iscompkot,@p_userid,getdate(),0)" +
                                        " set @p_id=SCOPE_IDENTITY()" +
                                        " End    " +
                                 " else if (@p_mode=1)   " +
                                    " begin" +
                                    " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                     " End " +
                                  " End" +
                                " end	" +
                                " try  " +
                                    " begin catch " +
                                      " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                      " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                      " Return  " +
                                      " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BANQBILLING");
                strproc = "";
                strproc = "CREATE procedure sp_BANQBILLING" +
                           " (@p_mode as int,  @p_rid as bigint, @p_bqbilldate datetime,  @p_borid bigint, @p_bqbillno nvarchar(100),  " +
                           "  @p_bqbilltype nvarchar(100), @p_bqbillamount decimal(18,2), @p_chequeno nvarchar(100),  @p_chequebankname nvarchar(100), " +
                            " @p_creditcardno nvarchar(100), @p_creditholdername nvarchar(100), @p_creditbankname nvarchar(100), @p_bqbillprepby nvarchar(100), " +
                            " @p_bqbillremark nvarchar(max), @p_custrid bigint, @p_userid bigint,   " +
                            " @p_errstr as nvarchar(max) out,   @p_retval as int out, @p_id as bigint out) as " +
                            " begin  try  begin  set @p_Errstr='' set @p_Retval=0 set @p_id=0   " +
                            " if (@p_mode=0)  " +
                            " begin 	 " +
                            " Insert Into BANQBILLING (BQBILLDATE,BORID,BQBILLNO,BQBILLTYPE,BQBILLAMOUNT,chequeno,chequebankname, creditcardno,creditholdername,creditbankname," +
                            " BQBILLPREPBY,BQBILLREMARK, custrid, auserid,adatetime,DelFlg)   " +
                            " Values ( @p_BQBILLDATE,@p_BORID,@p_BQBILLNO,@p_BQBILLTYPE,@p_BQBILLAMOUNT,@p_chequeno,@p_chequebankname, " +
                            " @p_creditcardno,@p_creditholdername,@p_creditbankname,@p_BQBILLPREPBY,@p_BQBILLREMARK,  @p_custrid,  @p_userid,getdate(),0) " +
                            " set @p_id=SCOPE_IDENTITY() End  " +
                            " else if (@p_mode=1)  " +
                            " begin  set @p_Errstr='' set @p_Retval=0 set @p_id=0 	" +
                            " update BANQBILLING  set  BQBILLDATE=@p_BQBILLDATE,BORID=@p_BORID,BQBILLNO=@p_BQBILLNO,BQBILLTYPE=@p_BQBILLTYPE," +
                            " BQBILLAMOUNT=@p_BQBILLAMOUNT,chequeno=@p_chequeno,chequebankname=@p_chequebankname,	" +
                            " creditcardno=@p_creditcardno,creditholdername=@p_creditholdername,creditbankname=@p_creditbankname," +
                            " BQBILLPREPBY=@p_BQBILLPREPBY,BQBILLREMARK=@p_BQBILLREMARK," +
                            " custrid=@p_custrid, euserid = @p_userid,edatetime = getdate() " +
                            " where rid = @p_rid " +
                            " End  End end	" +
                            " try  begin catch  " +
                            " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                            " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  Return " +
                            " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLWISESALESSUMMARY");
                strproc = "";
                strproc = "create procedure sp_BILLWISESALESSUMMARY " +
                            " (@p_fromdate datetime,@p_todate datetime) AS BEGIN " +
                            " Select bill.rid,bill.billno,bill.REFBILLNO,bill.BILLDATE, " +
                            " isnull(msttable.TABLENAME,'') as Tablename,bill.totamount,bill.totdiscamount, " +
                            " ((isnull(bill.totamount,0)-isnull(bill.totdiscamount,0)) + isnull(bill.TOTSERCHRAMT,0))  as subtotal, " +
                            " bill.totsertaxamount,bill.totsertaxper,bill.totroff,bill.TOTBEVVATAMT,bill.TOTLIQVATAMT,bill.TOTADDVATAMOUNT,bill.TOTVATAMOUNT, " +
                            " bill.Netamount,setle.SETLETYPE,setle.Setleamount,Setle.AdjAmt, " +
                            " bill.TOTSERCHRAMT,bill.TOTADDDISCAMT,bill.TOTKKCESSAMT,BILL.TOTGSTAMT " +
                            " FROM BILL " +
                            " left join (Select min(settlement.rid) as Setlerid,settlement.billrid,isnull(settlement.SETLETYPE,'') as SETLETYPE, " +
                            " Sum(settlement.setleamount) as Setleamount,Sum(settlement.ADJAMT) as AdjAmt  " +
                                        " From settlement where ISNULL(settlement.DELFLG, 0) = 0 AND settlement.SETLETYPE NOT IN ('CUSTOMER CREDIT')" +
                                        " Group by settlement.billrid,settlement.SETLETYPE " +
                                        " ) setle on (setle.billrid=bill.rid) " +
                            " left join msttable on (msttable.rid=bill.tablerid) " +
                            " where (ISNULL(dbo.BILL.DELFLG, 0) = 0) " +
                            " AND (ISNULL(dbo.BILL.ISREVISEDBILL, 0) = 0) " +
                            " and bill.billDATE between @p_fromdate and @p_todate " +
                            " order by bill.rid " +
                            " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BANQBOOKINGREG");
                strproc = "";
                strproc = "CREATE procedure sp_BANQBOOKINGREG(@p_fromdate datetime,@p_todate datetime) as  " +
                        " begin  " +
                        " select BANQBOOKING.rid,BANQBOOKING.BONO," +
                        " BANQBOOKING.BODATE,BANQBOOKING.BOTIME,MSTBANQ.BANQNAME," +
                        " MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO,MSTCUST.CUSTEMAIL," +
                        " BANQBOOKING.BOAMT,BANQBOOKING.BONOOFPER,BANQBOOKING.BOCONF," +
                        " BANQBOOKING.BOTYPEOFFUNC" +
                        " from BANQBOOKING  " +
                        " left join MSTBANQ on (MSTBANQ.rid=BANQBOOKING.BANQRID)" +
                        " LEFT JOIN MSTCUST ON (MSTCUST.RID=BANQBOOKING.CUSTRID)" +
                        " WHERE BANQBOOKING.BODATE between @p_fromdate and @p_todate " +
                        " and isnull(BANQBOOKING.delflg,0)=0 " +
                        " order by BANQBOOKING.BODATE,BANQBOOKING.rid  end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_ITEMGROUPSALES");
                strproc = "";
                strproc = "create procedure SP_ITEMGROUPSALES  " +
                                " (@p_fromdate datetime,@p_todate datetime) AS BEGIN  " +
                                " Select MSTITEMGROUP.RID,MSTITEMGROUP.IGCODE,MSTITEMGROUP.IGNAME," +
                                " sum(billdtl.iqty) as IGQTY,sum(billdtl.IAMT) as IGAMT " +
                                " from bill  " +
                                " inner join billdtl on (billdtl.BILLRID = bill.rid)  " +
                                " inner join MSTITEM on (MSTITEM.rid = billdtl.IRID) " +
                                " left join MSTITEMGROUP on (MSTITEMGROUP.rid = MSTITEM.IGRPRID)  " +
                                " where isnull(bill.delflg,0)=0 and isnull(billdtl.delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0  " +
                                " and bill.billDATE between @p_fromdate and @p_todate  " +
                                " GROUP BY MSTITEMGROUP.RID,MSTITEMGROUP.IGCODE,MSTITEMGROUP.IGNAME  " +
                                " order by MSTITEMGROUP.IGNAME  " +
                                " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_SMSHISTORY");
                strproc = "";
                strproc = " create procedure sp_SMSHISTORY  " +
                           " ( @p_mode as int,  @p_rid as bigint, @p_smsevent nvarchar(100),  " +
                             " @p_smseventid bigint,@p_mobno nvarchar(20), @p_smstext nvarchar(200),  " +
                             " @p_sendflg bigint, @p_smspername nvarchar(100), @p_smsaccuserid nvarchar(100), " +
                             " @p_smstype nvarchar(50), @p_resid nvarchar(200),  " +
                             " @p_tobesenddatetime datetime,@p_rmsid nvarchar(100),  " +
                             " @p_userid bigint,  @p_errstr as nvarchar(max) out,   " +
                             " @p_retval as int out, @p_id as bigint out )  " +
                             " as  begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   if (@p_mode=0)   " +
                             " begin 		  " +
                                 " Insert Into SMSHISTORY (SMSEVENT,SMSEVENTID,MOBNO,SMSTEXT,SENDFLG,SMSPERNAME,SMSACCUSERID,  " +
                                                     " 	SMSTYPE,RESID,TOBESENDDATETIME,RMSID,AUSERID,ADATETIME,DELFLG)   " +
                                     " Values ( @p_SMSEVENT,@p_SMSEVENTID,@p_MOBNO,@p_SMSTEXT,@p_SENDFLG,@p_SMSPERNAME,@p_SMSACCUSERID, " +
                                             " @p_SMSTYPE,@p_RESID,@p_TOBESENDDATETIME,@p_RMSID,@p_userid,getdate(),0 )  " +
                                             " set @p_id=SCOPE_IDENTITY() End      " +
                                 " else if (@p_mode=1)    " +
                                     " begin set @p_Errstr=''  set @p_Retval=0 set @p_id=0 end " +
                                   " End end " +
                                     " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;    " +
                                     " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return    " +
                                     " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_VATREGISTER");
                strproc = "";
                strproc = " Create Procedure sp_VATREGISTER  " +
                           " (@p_fromdate datetime,@p_todate datetime)  " +
                            " as  begin    " +
                            " select BILL.RID,BILL.BILLDATE,BILL.BILLNO,BILL.REFBILLNUM,BILL.BILLTYPE,ISNULL(BILL.TOTAMOUNT,0) AS INCTOTAMOUNT,BILL.NETAMOUNT," +
                            " ISNULL(BILL.TOTCALCVATPER,0) AS TOTCALCVATPER," +
                            " ISNULL(BILL.TOTCALCVATAMOUNT,0) AS TOTCALCVATAMOUNT,  (ISNULL(BILL.TOTAMOUNT,0) - ISNULL(BILL.TOTCALCVATAMOUNT,0)) AS EXCTOTAMOUNT " +
                            " From BILL " +
                            " WHERE BILL.BILLDATE between @p_fromdate and @p_todate " +
                            " and isnull(BILL.DELFLG,0)=0  " +
                            " and isnull(BILL.ISREVISEDBILL,0)=0 " +
                            " and isnull(BILL.TOTCALCVATAMOUNT,0)>0 " +
                            " order by BILL.BILLDATE  end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTREPORTDEPT");
                strproc = "";
                strproc = "create  procedure sp_MSTREPORTDEPT " +
                        " ( " +
                        " @p_mode as int, " +
                        " @p_rid bigint, " +
                        " @p_reportdeptcode nvarchar(20), " +
                        " @p_reportdeptname nvarchar(100), " +
                       "  @p_reportdeptdesc nvarchar(max), " +
                       "  @p_userid bigint, " +
                        " @p_errstr as nvarchar(max) out, " +
                      "   @p_retval as int out," +
                        " @p_id as bigint out " +
                         " ) as  " +
                        " begin " +
                       "  try " +
                       "  begin  " +
                        " if (@p_mode=0)  " +
                        " begin  " +
                        " declare @codeRowCount as int  " +
                       "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                        " select @codeRowCount = (select count(*) from mstreportdept where reportdeptcode = @p_reportdeptcode and ISNULL(DelFlg,0)=0) " +
                       "  if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'     " +
                          "   Return    " +
                      "   End	 " +
                  " Begin " +
                   "   declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                     " select  @nameRowCount = (select count(*) from mstreportdept where reportdeptname = @p_reportdeptname and ISNULL(DelFlg,0)=0)  " +
                   "   if ( @nameRowCount > 0)   " +
                   "   begin    " +
                     " set @p_Retval = 1 set @p_Errstr ='Name Already exits.'  " +
                    "  Return  " +
                              " End  " +
                          "   end " +
                                    " begin " +
                                        "  Insert Into mstreportdept (reportdeptcode,reportdeptname,reportdeptdesc,senddata,auserid,adatetime,DelFlg) " +
                                        "  Values (@p_reportdeptcode,@p_reportdeptname,@p_reportdeptdesc,0,@p_userid,getdate(),0) " +
                                        "	set @p_id=SCOPE_IDENTITY()	" +
                                         " End  End  " +
                                " else if (@p_mode=1) " +
                                      " begin " +
                                     " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0  " +
                                    "  select @codeRowCount1 = (select count(*) from mstreportdept where reportdeptcode = @p_reportdeptcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 ) " +
                                    "  if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.' " +
                                    "  Return   End " +
                            "  Begin  " +
                                "  declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0    " +
                                "  select  @nameRowCount1 = (select count(*) from mstreportdept where reportdeptname = @p_reportdeptname and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                               "   if ( @nameRowCount1 > 0)  " +
                                "  Begin " +
                                 " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                                "  Return  " +
                               " End  END " +
                            "  begin  " +
                                     "  Update mstreportdept set reportdeptcode=@p_reportdeptcode, " +
                                     "  reportdeptname = @p_reportdeptname, reportdeptdesc = @p_reportdeptdesc,senddata=0," +
                                     "  euserid = @p_userid,edatetime = getdate()    " +
                                     "  where rid = @p_rid    " +
                                     "  End  End  End	" +
                                     "	End  " +
                                     "  try  begin catch  " +
                                     " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                      "   set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                    " Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_REPORTDEPARTMENTSALES");
                strproc = "";
                strproc = "CREATE procedure SP_REPORTDEPARTMENTSALES " +
                          " (@p_fromdate datetime,@p_todate datetime) AS " +
                            " BEGIN  " +
                            " Select MSTREPORTDEPT.RID,MSTREPORTDEPT.REPORTDEPTNAME," +
                            " MSTITEM.RID AS IRID,MSTITEM.INAME,MSTITEMGROUP.RID AS IGRPRID,MSTITEMGROUP.IGNAME," +
                            " sum(billdtl.iqty) as IRQTY1," +
                            " sum(billdtl.IAMT) as IRAMT1," +
                            " SUM(CASE WHEN billdtl.iqty > 0 THEN billdtl.iqty ELSE 0 END) AS IRQTY, " +
                            " SUM(CASE WHEN billdtl.IAMT > 0 THEN billdtl.IAMT ELSE 0 END) AS IRAMT, " +
                            " SUM(CASE WHEN billdtl.iqty < 0 THEN billdtl.iqty ELSE 0 END) AS IMQTY, " +
                            " SUM(CASE WHEN billdtl.IAMT < 0 THEN billdtl.IAMT ELSE 0 END) AS IMAMT " +
                            " From bill  " +
                            " Inner Join Billdtl on (billdtl.BILLRID = bill.rid)   " +
                            " Inner Join MSTITEM on (MSTITEM.rid = billdtl.IRID)  " +
                            " Left  Join MSTREPORTDEPT on (MSTREPORTDEPT.rid = MSTITEM.REPORTDEPTRID)  " +
                            " Left  Join MSTITEMGROUP on (MSTITEMGROUP.rid = MSTITEM.IGRPRID)  " +
                            " Where isnull(bill.delflg,0)=0 and isnull(billdtl.delflg,0)=0 " +
                            " and isnull(BILL.ISREVISEDBILL,0)=0 AND ISNULL(MSTITEM.REPORTDEPTRID,0)>0  " +
                            " and bill.billDATE between @p_fromdate and @p_todate   " +
                            " GROUP BY MSTREPORTDEPT.RID,MSTREPORTDEPT.REPORTDEPTNAME,MSTITEM.RID,MSTITEM.INAME,MSTITEMGROUP.RID,MSTITEMGROUP.IGNAME " +
                            " order by MSTREPORTDEPT.REPORTDEPTNAME,MSTREPORTDEPT.RID,MSTITEMGROUP.IGNAME,MSTITEMGROUP.RID,MSTITEM.INAME,MSTITEM.RID " +
                            " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MSTCHECKLISTITEM");
                strproc = "";
                strproc = "create procedure SP_MSTCHECKLISTITEM" +
                            " (" +
                            " @p_mode as int," +
                            " @p_rid bigint," +
                            " @p_chkcode nvarchar(20)," +
                            " @p_chkname nvarchar(100)," +
                            " @p_chksupplier nvarchar(100)," +
                            " @p_chkcompany nvarchar(100)," +
                            " @p_chkrate decimal(18,2)," +
                            " @p_chkpurdate datetime," +
                            " @p_chkisvisible bit," +
                            " @p_chkdesc nvarchar(max)," +
                            " @p_userid bigint," +
                            " @p_errstr as nvarchar(max) out," +
                            " @p_retval as int out," +
                            " @p_id bigint out" +
                            " ) as" +
                            " begin" +
                            " try" +
                            " begin" +
                            " if (@p_mode=0)" +
                             " begin 	     " +
                                    " declare @codeRowCount as int " +
                                    " set @p_Errstr=''  set @p_Retval=0  set @p_id=0  " +
                                    " select @codeRowCount = (select count(*) from MSTCHECKLISTITEM where CHKCODE = @p_CHKCODE and ISNULL(DelFlg,0)=0)   " +
                                    " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Check List Item Code Already exits.'    " +
                                        " Return   " +
                                    " End	   " +
                              " Begin " +
                                 " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0   set @p_id=0   " +
                                 " select  @nameRowCount = (select count(*) from MSTCHECKLISTITEM where CHKNAME = @p_CHKNAME and ISNULL(DelFlg,0)=0)   " +
                                 " if ( @nameRowCount > 0)    " +
                                 " begin   " +
                                 " set @p_Retval = 1 set @p_Errstr ='Check List Item Name Already exits.'  " +
                                 " Return " +
                              " End  " +
                            " end " +
                                        "  begin " +
                                         " Insert Into MSTCHECKLISTITEM (CHKCODE,CHKNAME,CHKSUPPLIER,CHKCOMPANY,CHKRATE,CHKPURDATE,CHKISVISIBLE,CHKDESC,auserid,adatetime,DelFlg)" +
                                         " Values (@p_CHKCODE,@p_CHKNAME,@p_CHKSUPPLIER,@p_CHKCOMPANY,@p_CHKRATE,@p_CHKPURDATE,@p_CHKISVISIBLE,@p_CHKDESC,@p_userid,getdate(),0)" +
                                         " set @p_id=SCOPE_IDENTITY()   " +
                                         " End  End " +
                                " else if (@p_mode=1) " +
                                    "  begin   " +
                                     " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0   set @p_id=0   " +
                                     " select @codeRowCount1 = (select count(*) from MSTCHECKLISTITEM where CHKCODE = @p_CHKCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 ) " +
                                     " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Check List Item Code Already exits.' " +
                                     " Return   End " +
                             " Begin " +
                                "  declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0   set @p_id=0 " +
                                 " select  @nameRowCount1 = (select count(*) from MSTCHECKLISTITEM where CHKNAME = @p_CHKNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                                 " if ( @nameRowCount1 > 0)    " +
                                 " begin   " +
                                 " set @p_Retval = 1 set @p_Errstr ='Check List Item Name Already exits.'   " +
                                 " Return " +
                              " End  END " +
                             " begin " +
                                    "  update MSTCHECKLISTITEM set CHKCODE=@p_CHKCODE," +
                                     " CHKNAME = @p_CHKNAME, CHKSUPPLIER = @p_CHKSUPPLIER,CHKCOMPANY=@p_CHKCOMPANY,CHKRATE=@p_CHKRATE," +
                                    " CHKPURDATE=@p_CHKPURDATE,CHKISVISIBLE=@p_CHKISVISIBLE,CHKDESC=@p_CHKDESC," +
                                     " euserid = @p_userid,edatetime = getdate()   " +
                                     " where rid = @p_rid    " +
                                     " End  End  End  End " +
                                     " try  begin catch  " +
                                     " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()   " +
                                    " Return  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_CHECKLISTITEM");
                strproc = "";
                strproc = " create procedure sp_CHECKLISTITEM  " +
                            " ( @p_mode as int,  @p_rid as bigint, @p_chklstdate datetime,  " +
                              "  @p_chklsttime datetime,@p_chklstprepby nvarchar(100),@p_chklstdesc nvarchar(max),  " +
                               " @p_userid bigint,  @p_errstr as nvarchar(max) out,   " +
                               " @p_retval as int out, @p_id as bigint out)  " +
                               " as  " +
                                " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0)   " +
                               " begin 		  " +
                                 "   Insert Into CHECKLISTITEM (CHKLSTDATE,CHKLSTTIME,CHKLSTPREPBY,CHKLSTDESC,AUSERID,ADATETIME,DELFLG)   " +
                                   "                    Values (@p_CHKLSTDATE,@p_CHKLSTTIME,@p_CHKLSTPREPBY,@p_CHKLSTDESC,@p_userid,getdate(),0 )  " +
                                     "           set @p_id=SCOPE_IDENTITY() End      " +
                                   " else if (@p_mode=1)    " +
                                     "   begin set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                        " update CHECKLISTITEM  set  CHKLSTDATE=@p_CHKLSTDATE,CHKLSTTIME=@p_CHKLSTTIME,CHKLSTPREPBY=@p_CHKLSTPREPBY,CHKLSTDESC=@p_CHKLSTDESC, " +
                                          " euserid = @p_userid,edatetime = getdate()  " +
                                          " where rid = @p_rid  " +
                                        " end " +
                                     " End end " +
                                      "  try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                       " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return    " +
                                       " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_CHECKLISTITEMDTL");
                strproc = "";
                strproc = " create  procedure sp_CHECKLISTITEMDTL  " +
                         "  (  " +
                          " @p_mode as int,  " +
                          " @p_rid as bigint, " +
                          " @p_chklstrid bigint, " +
                          " @p_chkirid bigint, " +
                          " @p_chkicode nvarchar(50)," +
                          " @p_chkiname nvarchar(100)," +
                          " @p_chkirate decimal(18,2), " +
                          " @p_chkiplus decimal(18,2), " +
                          " @p_chkiplusremark nvarchar(max)," +
                          " @p_chkimins decimal(18,2)," +
                          " @p_chkiminsremark nvarchar(max)," +
                          " @p_chkistock decimal(18,2)," +
                          " @p_chkiamount decimal(18,2)," +
                          " @p_userid bigint,  " +
                          " @p_errstr as nvarchar(max) out,  " +
                          " @p_retval as int out, " +
                          " @p_id as bigint out  " +
                          " ) as  " +
                          " begin  " +
                          " try  " +
                            "   begin  " +
                              "     set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                "    if (@p_mode=0)   " +
                                  "     begin  " +
                                    "  	Insert Into CHECKLISTITEMDTL (CHKLSTRID,CHKIRID,CHKICODE,CHKINAME,CHKIRATE,CHKIPLUS,CHKIPLUSREMARK,CHKIMINS,CHKIMINSREMARK," +
                                        " 							 CHKISTOCK,CHKIAMOUNT,auserid,adatetime,DelFlg)  " +
                                          "          Values (@p_CHKLSTRID,@p_CHKIRID,@p_CHKICODE,@p_CHKINAME,@p_CHKIRATE,@p_CHKIPLUS,@p_CHKIPLUSREMARK,@p_CHKIMINS,@p_CHKIMINSREMARK," +
                                            " 						 @p_CHKISTOCK,@p_CHKIAMOUNT,  " +
                                                " 					@p_userid,getdate(),0  " +
                                                  "           )  " +
                                          " set @p_id=SCOPE_IDENTITY() " +
                                          " End   " +
                                  " else if (@p_mode=1)    " +
                                    "   begin  " +
                                      "   set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                        "   update CHECKLISTITEMDTL  " +
                                         " set CHKLSTRID=@p_CHKLSTRID,CHKIRID = @p_CHKIRID,CHKICODE = @p_CHKICODE,CHKINAME=@p_CHKINAME,CHKIRATE=@p_CHKIRATE,CHKIPLUS=@p_chkiplus,CHKIPLUSREMARK=@p_CHKIPLUSREMARK,CHKIMINS=@p_CHKIMINS,CHKIMINSREMARK=@p_CHKIMINSREMARK," +
                                            " 						 CHKISTOCK=@p_CHKISTOCK,CHKIAMOUNT=@p_CHKIAMOUNT," +
                                              "                       euserid = @p_userid,edatetime = getdate()     " +
                                                "                     where rid = @p_rid and CHKLSTRID=@p_CHKLSTRID  " +
                                      " End  " +
                                   " End  " +
                                  " end	 " +
                                  " try   " +
                                    "   begin catch   " +
                                     "   SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;      " +
                                      "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                       " Return   " +
                                       " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_CHECKITEMSTOCKLISTREG");
                strproc = "";
                strproc = " CREATE procedure sp_CHECKITEMSTOCKLISTREG  " +
                            " (@p_fromdate datetime,@p_todate datetime) As   " +
                            " Begin  " +
                            " SELECT CHECKLISTITEMDTL.CHKIRID,MSTCHECKLISTITEM.CHKCODE,  " +
                            " MSTCHECKLISTITEM.CHKNAME,MSTCHECKLISTITEM.CHKSUPPLIER,MSTCHECKLISTITEM.CHKCOMPANY, " +
                            " MSTCHECKLISTITEM.CHKPURDATE, " +
                            " SUM(ISNULL(CHKIPLUS, 0)) AS IPLUS,  " +
                            " SUM(ISNULL(CHKIMINS, 0)) AS IMINS,  " +
                            " SUM(ISNULL(CHKIPLUS, 0)) - SUM(ISNULL(CHKIMINS, 0)) AS ISTOCK, " +
                            " SUM(ISNULL(CHKIAMOUNT, 0)) AS IAMOUNT  " +
                            " FROM  CHECKLISTITEMDTL  " +
                            " LEFT JOIN CHECKLISTITEM ON (CHECKLISTITEM.RID=CHECKLISTITEMDTL.CHKLSTRID)  " +
                            " LEFT JOIN MSTCHECKLISTITEM ON (MSTCHECKLISTITEM.RID=CHECKLISTITEMDTL.CHKIRID)  " +
                            " WHERE (ISNULL(CHECKLISTITEMDTL.DELFLG, 0) = 0)  " +
                            " AND CHECKLISTITEM.CHKLSTDATE between @p_fromdate and @p_todate  " +
                            " GROUP BY CHECKLISTITEMDTL.CHKIRID,MSTCHECKLISTITEM.CHKCODE,   " +
                            " MSTCHECKLISTITEM.CHKNAME,MSTCHECKLISTITEM.CHKSUPPLIER,MSTCHECKLISTITEM.CHKCOMPANY,  " +
                            " MSTCHECKLISTITEM.CHKPURDATE  " +
                            " ORDER BY CHECKLISTITEMDTL.CHKIRID  " +
                            " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_KITCHDISPINFO");
                strproc = "";
                strproc = "CREATE procedure sp_KITCHDISPINFO  " +
                            " ( @p_mode as int,  @p_rid as bigint,  " +
                            " @p_kotrid bigint,@p_kotdate datetime,@p_kottime datetime,@p_tablerid bigint,@p_tablename nvarchar(50)," +
                            " @p_kotorderper BIGINT,@p_billrid bigint,@p_tablestatus bigint," +
                            " @p_irid bigint,  @p_iname nvarchar(500),  @p_iqty bigint,@p_iremark nvarchar(300),@p_genremark nvarchar(max),@p_deliyn bigint,@p_imodifier nvarchar(max)," +
                            " @p_userid bigint,  @p_errstr as nvarchar(max) out,  @p_retval as int out,  @p_id as bigint out  ) " +
                            " as  begin  try    " +
                            " begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                            " if (@p_mode=0)  " +
                            " begin    " +
                            " Insert Into KITCHDISPINFO (kotrid,kotdate,kottime,tablerid,tablename,kotordper,billrid,tablestatus,irid,iname,iqty, " +
                                                        " iremark,genremark,deliyn,imodifier,auserid,adatetime)   " +
                            " Values (@p_kotrid,@p_kotdate," +
                            " @p_kottime,@p_tablerid,@p_tablename,@p_kotorderper,@p_billrid,@p_tablestatus," +
                            " @p_irid,@p_iname,@p_iqty,@p_iremark,@p_genremark,@p_deliyn,@p_imodifier," +
                            " @p_userid,getdate()) set @p_id=SCOPE_IDENTITY()  End  " +
                            " else if (@p_mode=1)    " +
                            " begin    set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                            " update KITCHDISPINFO " +
                            " Set  kotrid=@p_kotrid,kotdate=@p_kotdate,kottime=@p_kottime," +
                            " tablerid=@p_tablerid,tablename=@p_tablename,kotordper=@p_kotorderper,billrid=@p_billrid,tablestatus=@p_tablestatus," +
                            " IRID=@p_irid,INAME=@p_iname," +
                            " IQTY=@p_iqty,IREMARK=@p_iremark, genREMARK=@p_genremark," +
                            " deliyn=@p_deliyn,imodifier=@p_imodifier, " +
                            " EUSERID=@p_userid,eDATETIME=getdate() " +
                            " where kotrid=@p_kotrid and irid=@p_irid  End  End  end	 " +
                            " try    begin catch   SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                            " set   @p_Retval = ERROR_NUMBER()    set @p_Errstr = ERROR_MESSAGE() set @p_id=0     " +
                            " Return    END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_TABLERUNNINGSUMMARY");
                strproc = "";
                strproc = " CREATE PROCEDURE SP_TABLERUNNINGSUMMARY " +
                                " (@p_fromdate datetime,@p_todate datetime) AS  " +
                                " BEGIN " +
                                " Select kotinfo.billrid,kotinfo.kotrid,kot.KOTTIME,bill.billtime,bill.BILLNO,bill.REFBILLNO,bill.CUSTNAME,BILL.BILLPAX, BILL.NETAMOUNT, " +
                                " ISNULL(MSTEMP.EMPNAME,'') AS EMPNAME,msttable.TABLENAME, " +
                                " ( '' " +
                                    " /* + CAST(DATEDIFF(second, kot.KOTTIME,bill.billtime) / 60 / 60 / 24 / 7 AS NVARCHAR(50)) + ' Weeks, ' + CAST(DATEDIFF(second, kot.KOTTIME,bill.billtime) / 60 / 60 / 24 % 7 AS NVARCHAR(50)) + ' Days, ' " +
                                    " */ + CAST(DATEDIFF(second, kot.KOTTIME,bill.billtime) / 60 / 60 % 24  AS NVARCHAR(50)) + ' Hours, ' " +
                                    " + CAST(DATEDIFF(second, kot.KOTTIME,bill.billtime) / 60 % 60 AS NVARCHAR(50)) + ' Min. and ' " +
                                    " + CAST(DATEDIFF(second, kot.KOTTIME,bill.billtime) % 60 AS NVARCHAR(50)) + ' Sec.') as TableRuntime " +
                                " from kot " +
                                " left join ( " +
                                        " select bill.rid as billrid,min(billdtl.KOTRID) as kotrid from bill " +
                                        " left join billdtl on (billdtl.BILLRID = bill.rid) " +
                                        " where billdtl.kotrid >0  AND ISNULL(BILL.DELFLG,0)=0 AND BILL.billDATE between @p_fromdate and @p_todate " +
                                        " group by bill.rid) kotinfo on  (kotinfo.kotrid = kot.rid) " +
                                " left join bill on (bill.rid = kotinfo.billrid) " +
                                " left join MSTEMP on (MSTEMP.rid = bill.BILLORDERPERID) " +
                                " left join msttable on (msttable.rid = bill.TABLERID) " +
                                " where isnull(kotinfo.billrid,0)>0 AND ISNULL(BILL.DELFLG,0)=0 " +
                                " AND BILL.billDATE between @p_fromdate and @p_todate " +
                                " order by kotinfo.billrid " +
                                " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_MSTEMP");
                strproc = "";
                strproc = " CREATE  procedure sp_MSTEMP  " +
                         " (  " +
                         " @p_mode as int,  " +
                         " @p_rid bigint,  " +
                         " @p_empcode nvarchar(20),  " +
                         " @p_empname nvarchar(250), " +
                         " @p_empfathername nvarchar(250), " +
                         " @p_empadd1 nvarchar(200), " +
                         " @p_empadd2 nvarchar(200), " +
                         " @p_empadd3 nvarchar(200), " +
                         " @p_empcatid bigint, " +
                         " @p_empcityid bigint, " +
                         " @p_empstateid bigint, " +
                         " @p_empcountryid bigint, " +
                         " @p_emppin nvarchar(50), " +
                         " @p_emptelno nvarchar(50), " +
                         " @p_empmobileno nvarchar(50), " +
                         " @p_empemail nvarchar(200), " +
                         " @p_empfaxno nvarchar(50), " +
                         " @p_empbirthdate datetime, " +
                         " @p_empjoindate datetime, " +
                         " @p_empleavedate datetime, " +
                         " @p_empgender nvarchar(50), " +
                         " @p_empmaritalstatus nvarchar(50),  " +
                         " @p_isdispinkot bit, " +
                         " @p_empimage image, " +
                         " @p_empdesc nvarchar(max),	 " +
                         " @p_empbankname nvarchar(250),  " +
                         " @p_empbankdetails nvarchar(250),  " +
                         " @p_empbankaccno nvarchar(250),  " +
                         " @p_isnonactive bit,  " +
                         " @p_userid bigint,  " +
                         " @p_errstr as nvarchar(max) out,  " +
                         " @p_retval as int out, " +
                         " @p_id as bigint out " +
                         " ) as  " +
                        "  begin  " +
                         " try  " +
                        "  begin  " +
                        "  if (@p_mode=0)  " +
                       "   begin   " +
                        "  declare @codeRowCount as int   " +
                        "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                       "   select @codeRowCount = (select count(*) from mstemp where empcode = @p_empcode and ISNULL(DelFlg,0)=0)    " +
                        "  if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Employee Code Already exits.'      " +
                          "    Return     " +
                        "  End	  " +
                  "  Begin   " +
                     "  declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0       " +
                     "  select  @nameRowCount = (select count(*) from mstemp where empname = @p_empname and ISNULL(DelFlg,0)=0)     " +
                     "  if ( @nameRowCount > 0)     " +
                     "  begin     " +
                     "  set @p_Retval = 1 set @p_Errstr ='Employee Name Already exits.'    " +
                     "  Return   " +
                            "    End    " +
                           "   end  " +
                                       "    begin  " +
                                       "    Insert Into mstemp (empcode,empname,empfathername,empadd1,empadd2,empadd3,empcatid,empcityid,empstateid,empcountryid, " +
                                                           "    emppin,emptelno,empmobileno,empemail,empfaxno,empbirthdate,empjoindate,empleavedate,empgender,empmaritalstatus,isdispinkot, " +
                                                            "   empimage,empdesc," +
                                                            "  empbankname,empbankdetails,empbankaccno,isnonactive,senddata," +
                                                            " auserid,adatetime,DelFlg)	 " +
                                                    "   Values (@p_empcode,@p_empname,@p_empfathername,@p_empadd1,@p_empadd2,@p_empadd3,@p_empcatid,@p_empcityid,@p_empstateid,@p_empcountryid, " +
                                                                " @p_emppin,@p_emptelno,@p_empmobileno,@p_empemail,@p_empfaxno,@p_empbirthdate,@p_empjoindate,@p_empleavedate,@p_empgender,@p_empmaritalstatus,@p_isdispinkot, " +
                                                                " @p_empimage,@p_empdesc, " +
                                                                " @p_empbankname,@p_empbankdetails,@p_empbankaccno,@p_isnonactive,0," +
                                                                " @p_userid,getdate(),0) " +
                                            " set @p_id=SCOPE_IDENTITY() " +
                                         "  End  End   " +
                                "  else if (@p_mode=1)  " +
                                    "   begin     " +
                                    "   declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0      " +
                                    "   select @codeRowCount1 = (select count(*) from mstemp where empcode = @p_empcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )   " +
                                    "   if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Employee Code Already exits.'       " +
                                   "    Return   End   " +
                             "  Begin   " +
                                  " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0    " +
                                 "  select  @nameRowCount1 = (select count(*) from mstemp where empname = @p_empname and rid <> @p_rid and ISNULL(DelFlg,0)=0)      " +
                                 "  if ( @nameRowCount1 > 0)     " +
                                "   begin     " +
                                "   set @p_Retval = 1 set @p_Errstr ='Employee Name Already exits.'  " +
                                "   Return   " +
                             "   End  END  " +
                             "  begin   " +
                                    " update mstemp set empcode=@p_empcode,  " +
                                    " empname = @p_empname,empfathername = @p_empfathername,  " +
                                    " empadd1 = @p_empadd1,empadd2 = @p_empadd2 ,empadd3 = @p_empadd3,empcatid = @p_empcatid,empcityid = @p_empcityid,empstateid = @p_empstateid,empcountryid = @p_empcountryid, " +
                                    " emppin = @p_emppin,emptelno = @p_emptelno,empmobileno = @p_empmobileno,empemail = @p_empemail,empfaxno = @p_empfaxno,empbirthdate = @p_empbirthdate,empjoindate = @p_empjoindate,empleavedate = @p_empleavedate, " +
                                    " empgender = @p_empgender,empmaritalstatus = @p_empmaritalstatus,isdispinkot = @p_isdispinkot, " +
                                    " empimage=@p_empimage,empdesc=@p_empdesc,  " +
                                    " empbankname = @p_empbankname,empbankdetails = @p_empbankdetails,empbankaccno = @p_empbankaccno,isnonactive = @p_isnonactive,senddata=0," +
                                    " euserid = @p_userid,edatetime = getdate()  " +
                                    "  where rid = @p_rid     " +
                                    " End  End  End   " +
                                    " End   " +
                                     "  try  begin catch     " +
                                     "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;      " +
                                    "      set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                    "  Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_EMPDLYATD");
                strproc = "";
                strproc = " CREATE PROCEDURE SP_EMPDLYATD" +
                            " (" +
                            " @p_mode as int," +
                            " @p_rid bigint," +
                            " @p_emprid bigint," +
                            " @p_atddate datetime," +
                            " @p_atdstatus nvarchar(10)," +
                            " @p_punchintime datetime," +
                            " @p_punchouttime datetime," +
                            " @p_genremark nvarchar(max)," +
                            " @p_evepunchintime datetime," +
                            " @p_evepunchouttime datetime," +
                            " @p_userid bigint,  " +
                            " @p_errstr as nvarchar(max) out, " +
                            " @p_retval as int out, " +
                            " @p_id as bigint out " +
                            " ) as  " +
                            " Begin" +
                            " try" +
                            " begin" +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                            " if (@p_mode=0) " +
                            " begin" +
                                    " insert into EMPDLYATD(emprid,ATDDATE,ATDSTATUS,PUNCHINTIME,PUNCHOUTTIME,GENREMARK,EVEPUNCHINTIME,EVEPUNCHOUTTIME," +
                                                            " auserid,adatetime,DelFlg)" +
                                    " values (@p_emprid,@p_ATDDATE,@p_ATDSTATUS,@p_PUNCHINTIME,@p_PUNCHOUTTIME,@p_GENREMARK, " +
                                    " @p_evepunchintime,@p_evepunchouttime," +
                                    " @p_userid,getdate(),0)" +
                                " set @p_id=SCOPE_IDENTITY()" +
                                " End" +
                            " else if (@p_mode=1) " +
                                " begin" +
                            " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                    " update EMPDLYATD set emprid=@p_emprid, " +
                                    " ATDDATE = @p_ATDDATE, ATDSTATUS = @p_ATDSTATUS, " +
                                    " PUNCHINTIME=@p_PUNCHINTIME, " +
                                    " PUNCHOUTTIME=@p_PUNCHOUTTIME,GENREMARK=@p_GENREMARK, " +
                                    " EVEPUNCHINTIME = @p_evepunchintime,EVEPUNCHOUTTIME = @p_evepunchouttime," +
                                    " euserid = @p_userid,edatetime = getdate() " +
                                    " where rid = @p_rid   " +
                                " End " +
                            " End " +
                            " end " +
                            " try  " +
                                " begin catch    " +
                                " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage" +
                                 " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                " Return  " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_SUPPLISERWISEPURDTL");
                strproc = "";
                strproc = " CREATE PROCEDURE SP_SUPPLISERWISEPURDTL " +
                               "  (@p_fromdate datetime,@p_todate datetime)  " +
                                " AS BEGIN " +
                                " SELECT ITEMPURCHASE.RID as PURRID,MSTSUPPLIER.RID AS SUPPRID,MSTSUPPLIER.SUPPNAME,MSTSUPPLIER.SUPPADD1,MSTSUPPLIER.SUPPADD2,MSTSUPPLIER.SUPPADD3," +
                                " MSTSUPPLIER.SUPPCONTPERNAME1,MSTSUPPLIER.SUPPEMAIL,MSTSUPPLIER.SUPPPANNO,MSTSUPPLIER.SUPPTINNO,MSTSUPPLIER.SUPPCSTNO,MSTSUPPLIER.SUPPGSTNO," +
                                " ITEMPURCHASE.PURNO,ITEMPURCHASE.PURDATE,ITEMPURCHASE.DOCNO,ITEMPURCHASE.DOCDATE," +
                                " MSTSUPPLIER.SUPPMOBNO +',' + MSTSUPPLIER.SUPPTELNO AS SUPPCONTNO,ITEMPURCHASE.TOTAMOUNT," +
                                " ITEMPURCHASEDTL.RID AS PURDTLRID, MSTPURITEM.PURINAME AS INAME, " +
                                " ITEMPURCHASEDTL.IQTY,ITEMPURCHASEDTL.IRATE, MSTPURITEM.PURIUNIT AS IUNIT,ITEMPURCHASEDTL.IAMT " +
                                " FROM ITEMPURCHASE " +
                                " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID) " +
                                " LEFT JOIN MSTSUPPLIER ON (MSTSUPPLIER.RID=ITEMPURCHASE.SUPPRID) " +
                                " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMPURCHASEDTL.IRID) " +
                                " WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0 AND ISNULL(ITEMPURCHASEDTL.DELFLG,0)=0 " +
                                " and ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate " +
                                " ORDER BY MSTSUPPLIER.SUPPNAME,ITEMPURCHASE.PURDATE" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_ITEMWISEPURDTL");
                strproc = "";
                strproc = " CREATE PROCEDURE SP_ITEMWISEPURDTL " +
                                " (@p_fromdate datetime,@p_todate datetime)  " +
                                 " AS BEGIN  " +
                                " SELECT ITEMPURCHASE.RID as PURRID,MSTSUPPLIER.RID AS SUPPRID,MSTSUPPLIER.SUPPNAME," +
                                " MSTSUPPLIER.SUPPADD1,MSTSUPPLIER.SUPPADD2,MSTSUPPLIER.SUPPADD3, MSTSUPPLIER.SUPPCONTPERNAME1," +
                                " MSTSUPPLIER.SUPPEMAIL,MSTSUPPLIER.SUPPPANNO,MSTSUPPLIER.SUPPTINNO,MSTSUPPLIER.SUPPCSTNO," +
                                " MSTSUPPLIER.SUPPGSTNO, ITEMPURCHASE.PURNO,ITEMPURCHASE.PURDATE,ITEMPURCHASE.DOCNO,ITEMPURCHASE.DOCDATE, " +
                                " MSTSUPPLIER.SUPPMOBNO +',' + MSTSUPPLIER.SUPPTELNO AS SUPPCONTNO,ITEMPURCHASE.TOTAMOUNT, ITEMPURCHASEDTL.RID AS PURDTLRID,MSTPURITEM.PURINAME AS INAME," +
                                " ITEMPURCHASEDTL.IQTY,ITEMPURCHASEDTL.IRATE,MSTPURITEM.PURIUNIT AS IUNIT,ITEMPURCHASEDTL.IAMT  " +
                                " FROM ITEMPURCHASE  LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID)  " +
                                " LEFT JOIN MSTSUPPLIER ON (MSTSUPPLIER.RID=ITEMPURCHASE.SUPPRID)  " +
                                " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMPURCHASEDTL.IRID) " +
                                " WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0 AND ISNULL(ITEMPURCHASEDTL.DELFLG,0)=0  " +
                                " and ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate  " +
                                " ORDER BY ITEMPURCHASEDTL.INAME,ITEMPURCHASE.PURDATE " +
                                " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_DATEWISEBILLINFO");
                strproc = "";
                strproc = " CREATE procedure sp_DATEWISEBILLINFO  " +
                                " (@p_fromdate datetime,@p_todate datetime) as  " +
                                " BEGIN  " +
                                " SELECT BILL.BILLDATE,SUM(ISNULL(TOTAMOUNT,0)) AS TOTAMOUNT, SUM(ISNULL(TOTSERTAXAMOUNT,0)) AS TOTSERTAXAMOUNT, " +
                                " SUM(ISNULL(TOTVATAMOUNT,0)) AS TOTVATAMOUNT, SUM(ISNULL(TOTADDVATAMOUNT,0)) AS TOTADDVATAMOUNT, " +
                                " SUM(ISNULL(TOTDISCAMOUNT,0)) AS TOTDISCAMOUNT, SUM(ISNULL(TOTROFF,0)) AS TOTROFF, " +
                                " SUM(ISNULL(TOTBEVVATAMT,0)) AS TOTBEVVATAMT,SUM(ISNULL(TOTLIQVATAMT,0)) AS TOTLIQVATAMT," +
                                " SUM(ISNULL(TOTADDDISCAMT,0)) AS TOTADDDISCAMT,SUM(ISNULL(TOTSERCHRAMT,0)) AS TOTSERCHRAMT,SUM(ISNULL(TOTKKCESSAMT,0)) AS TOTKKCESSAMT, " +
                                " SUM(ISNULL(CGSTAMT,0)) AS CGSTAMT,SUM(ISNULL(SGSTAMT,0)) AS SGSTAMT,SUM(ISNULL(IGSTAMT,0)) AS IGSTAMT," +
                                " SUM(ISNULL(NETAMOUNT,0)) AS NETAMOUNT " +
                                " FROM BILL " +
                                " WHERE ISNULL(BILL.DELFLG,0)=0 " +
                                " and isnull(BILL.ISREVISEDBILL,0)=0 " +
                                " and BILL.billDATE between @p_fromdate and @p_todate " +
                                " group by BILL.BILLDATE  " +
                                " order by BILL.BILLDATE  " +
                                " END  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_ITEMPURCHASE");
                strproc = "";
                strproc = " CREATE procedure sp_ITEMPURCHASE " +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid as bigint," +
                         " @p_purno nvarchar(50)," +
                         " @p_purdate datetime,	" +
                         " @p_docno nvarchar(50)," +
                         " @p_docdate datetime, " +
                         " @p_supprid bigint," +
                         " @p_suppcontno nvarchar(50),     " +
                         " @p_totamount decimal(18,2)," +
                         " @p_totsertaxper decimal(18,2)," +
                         " @p_totsertaxamount decimal(18,2)," +
                         " @p_totvatper decimal(18,2)," +
                         " @p_totvatamount decimal(18,2)," +
                         " @p_totaddvatper decimal(18,2)," +
                         " @p_totaddvatamount decimal(18,2)," +
                         " @p_totdiscper decimal(18,2)," +
                         " @p_totdiscamount decimal(18,2)," +
                         " @P_totroff decimal(18,2)," +
                         " @p_netamount decimal(18,2),     " +
                         " @p_purremark nvarchar(max)," +
                         " @p_purimg image," +
                         " @p_purimgremark nvarchar(max)," +
                         " @p_entryby nvarchar(50)," +
                         " @p_grnno nvarchar(200)," +
                         " @p_lfno nvarchar(200)," +
                         " @p_cgstamt decimal(18,3)," +
                         " @p_sgstamt decimal(18,3)," +
                         " @p_igstamt decimal(18,3)," +
                         " @p_cessamt decimal(18,3)," +
                         " @p_vatamt decimal(18,3)," +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         " begin " +
                         " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0) " +
                                    " begin 		" +
                                        " Insert Into ITEMPURCHASE (PURNO,PURDATE,DOCNO,DOCDATE,SUPPRID,SUPPCONTNO," +
                                                          " totamount,totsertaxper,totsertaxamount,totvatper,totvatamount,totaddvatper,totaddvatamount," +
                                                          " totdiscper,totdiscamount,totroff,netamount,PURREMARK,PURIMG,PURIMGREMARK,entryby,grnno,lfno," +
                                                          " cgstamt,sgstamt,igstamt,cessamt,vatamt," +
                                                          " auserid,adatetime,DelFlg) " +
                                                  " Values (" +
                                                         "  @p_PURNO,@p_PURDATE,@p_DOCNO,@p_DOCDATE,@p_SUPPRID,@p_SUPPCONTNO," +
                                                          " @p_totamount,@p_totsertaxper,@p_totsertaxamount,@p_totvatper,@p_totvatamount,@p_totaddvatper,@p_totaddvatamount," +
                                                          " @p_totdiscper,@p_totdiscamount,@p_totroff,@p_netamount,@p_purremark,@p_purimg,@p_purimgremark,@p_entryby,@p_grnno,@p_lfno," +
                                                          " @p_cgstamt,@p_sgstamt,@p_igstamt,@p_cessamt,@p_vatamt," +
                                                          " @p_userid,getdate(),0" +
                                                           " )" +
                                        " set @p_id=SCOPE_IDENTITY()" +
                                        " End   " +
                                 " else if (@p_mode=1)      " +
                                    " begin" +
                                      "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0" +
                                        " update ITEMPURCHASE " +
                                        " set PURNO=@p_PURNO,PURDATE=@p_PURDATE,DOCNO=@p_DOCNO,DOCDATE=@p_DOCDATE,SUPPRID=@p_SUPPRID,SUPPCONTNO=@p_SUPPCONTNO," +
                                                         "  totamount=@p_totamount,totsertaxper=@p_totsertaxper,totsertaxamount=@p_totsertaxamount,totvatper=@p_totvatper,totvatamount=@p_totvatamount,totaddvatper=@p_totaddvatper,totaddvatamount=@p_totaddvatamount," +
                                                          " totdiscper=@p_totdiscper,totdiscamount=@p_totdiscamount,totroff=@p_totroff,netamount=@p_netamount,PURREMARK=@p_PURREMARK," +
                                                          " purimg=@p_purimg,purimgremark=@p_purimgremark,entryby=@p_entryby,grnno=@p_grnno,lfno=@p_lfno," +
                                                          " cgstamt=@p_cgstamt,sgstamt=@p_sgstamt,igstamt=@p_igstamt,cessamt=@p_cessamt,vatamt=@p_vatamt," +
                                                           " euserid = @p_userid,edatetime = getdate()    " +
                                                           " where rid = @p_rid  " +
                                     " End " +
                                  " End" +
                                " end	" +
                                " try  " +
                                    " begin catch  " +
                                      " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;    " +
                                      " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                      " Return  " +
                                      " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_ITEMPURCHASEDTL");
                strproc = "";
                strproc = "CREATE  procedure sp_ITEMPURCHASEDTL " +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid as bigint," +
                         " @p_purrid bigint," +
                         " @p_iname nvarchar(100),	" +
                         " @p_iqty decimal(18,3)," +
                         " @p_irate decimal(18,2)," +
                         " @p_iunit nvarchar(50)," +
                         " @p_iamt decimal(18,2),	" +
                         " @p_idesc nvarchar(2000),	" +
                         " @p_irid bigint," +
                         " @p_discper decimal(18,3)," +
                         " @p_discamt decimal(18,3)," +
                         " @p_cgstper decimal(18,3)," +
                         " @p_cgstamt decimal(18,3)," +
                         " @p_sgstper decimal(18,3)," +
                         " @p_sgstamt decimal(18,3)," +
                         " @p_igstper decimal(18,3)," +
                         " @p_igstamt decimal(18,3)," +
                         " @p_cessper decimal(18,3)," +
                         " @p_cessamt decimal(18,3)," +
                         " @p_vatper decimal(18,3)," +
                         " @p_vatamt decimal(18,3)," +
                         " @p_istockqty decimal(18,3)," +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         " begin " +
                         " try " +
                            " begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0) " +
                                    " begin 		" +
                                        " Insert Into ITEMPURCHASEDTL (PURRID,iname,iqty,irate,iunit,iamt,idesc,irid," +
                                                                    " discper,discamt,cgstper,cgstamt,sgstper,sgstamt,igstper,igstamt," +
                                                                    " cessper,cessamt,vatper,vatamt,istockqty," +
                                                                    " auserid,adatetime,DelFlg) " +
                                                    " Values (" +
                                                    " @p_purrid,@p_iname,@p_iqty,@p_irate,@p_iunit,@p_iamt,@p_idesc,@p_irid," +
                                                    " @p_discper,@p_discamt,@p_cgstper,@p_cgstamt,@p_sgstper,@p_sgstamt,@p_igstper,@p_igstamt," +
                                                    " @p_cessper,@p_cessamt,@p_vatper,@p_vatamt,@p_istockqty," +
                                                    " @p_userid,getdate(),0" +
                                                    " )" +
                                        " set @p_id=SCOPE_IDENTITY()" +
                                        " End    " +
                                 " else if (@p_mode=1)      " +
                                    " begin" +
                                      "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0" +
                                        " Update ITEMPURCHASEDTL " +
                                        " set iname=@p_iname,iqty=@p_iqty,irate=@p_irate,iunit=@p_iunit,iamt=@p_iamt,idesc=@p_idesc,irid=@p_irid," +
                                        " discper=@p_discper,discamt=@p_discamt,cgstper=@p_cgstper,cgstamt=@p_cgstamt,sgstper=@p_sgstper,sgstamt=@p_sgstamt,igstper=@p_igstper,igstamt=@p_igstamt," +
                                        " cessper=@p_cessper,cessamt=@p_cessamt,vatper=@p_vatper,vatamt=@p_vatamt,istockqty=@p_istockqty," +
                                        " euserid = @p_userid,edatetime = getdate()    " +
                                        " where rid = @p_rid and purrid=@p_purrid" +
                                     " End " +
                                  " End" +
                                " End " +
                                " try  " +
                                    " begin catch " +
                                      " SELECT ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                      " SET @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                     "  Return  " +
                                     "  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_PURCHASEBILLINFO");
                strproc = "";
                strproc = " CREATE PROCEDURE sp_PURCHASEBILLINFO (@p_fromdate datetime,@p_todate datetime) " +
                                "  AS BEGIN  " +
                                "  Select ITEMPURCHASE.RID,MSTSUPPLIER.RID AS SUPPRID,MSTSUPPLIER.SUPPNAME," +
                                "  ITEMPURCHASE.PURNO,ITEMPURCHASE.PURDATE,ITEMPURCHASE.DOCNO,ITEMPURCHASE.DOCDATE," +
                                "  ISNULL(ITEMPURCHASE.TOTAMOUNT,0) AS TOTAMOUNT,ISNULL(ITEMPURCHASE.TOTSERTAXAMOUNT,0) AS TOTSERTAXAMOUNT," +
                                "  ISNULL(ITEMPURCHASE.TOTVATAMOUNT,0) AS TOTVATAMOUNT,ISNULL(ITEMPURCHASE.TOTADDVATAMOUNT,0) AS TOTADDVATAMOUNT," +
                                "  ISNULL(ITEMPURCHASE.TOTDISCAMOUNT,0) AS TOTDISCAMOUNT,ISNULL(ITEMPURCHASE.TOTROFF,0) AS TOTROFF," +
                                "  ISNULL(ITEMPURCHASE.CGSTAMT,0) AS CGSTAMT,ISNULL(ITEMPURCHASE.SGSTAMT,0) AS SGSTAMT,ISNULL(ITEMPURCHASE.IGSTAMT,0) AS IGSTAMT," +
                                "  ISNULL(ITEMPURCHASE.NETAMOUNT,0) AS NETAMOUNT" +
                                "  From ITEMPURCHASE" +
                                "  LEFT JOIN MSTSUPPLIER ON (MSTSUPPLIER.RID = ITEMPURCHASE.SUPPRID)" +
                                "  WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0" +
                                "  and ITEMPURCHASE.PURDATE Between @p_fromdate And @p_todate  " +
                                "  ORDER BY ITEMPURCHASE.PURDATE,ITEMPURCHASE.RID" +
                                "  END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_ITEMWISEPURINFO");
                strproc = "";
                strproc = " CREATE PROCEDURE SP_ITEMWISEPURINFO(@p_fromdate datetime,@p_todate datetime)" +
                            " AS BEGIN  " +
                            " SELECT  MSTPURITEM.RID, MSTPURITEM.PURINAME AS INAME,MSTPURITEM.PURIUNIT AS IUNIT,SUM(ISNULL(ITEMPURCHASEDTL.IQTY,0)) AS TOTQTY,SUM(ISNULL(ITEMPURCHASEDTL.IAMT,0)) TOTAMT  FROM ITEMPURCHASE " +
                            " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID) " +
                            " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID=ITEMPURCHASEDTL.IRID) " +
                            " WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0 AND ISNULL(ITEMPURCHASEDTL.DELFLG,0)=0   " +
                            " and ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate  " +
                            " GROUP BY MSTPURITEM.RID,MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT " +
                            " ORDER BY MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT, MSTPURITEM.RID " +
                            " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_MSTTABLE");
                strproc = "";
                strproc = " CREATE  procedure sp_MSTTABLE  " +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid bigint, " +
                         " @p_tablecode nvarchar(20), " +
                         " @p_tablename nvarchar(250)," +
                         " @p_tabledesc nvarchar(max), " +
                         " @p_isparceltable bit," +
                         " @p_isroomtable bit, " +
                         " @p_roomno nvarchar(100), " +
                         " @p_tabdisc decimal(18,2)," +
                         " @p_pricelistrid bigint, " +
                         " @p_ishomedelitable bit," +
                         " @p_isnotcalccommi bit," +
                         " @p_ishidetable bit," +
                         " @p_tabpax Bigint," +
                         " @p_gsttaxtype nvarchar(100)," +
                         " @p_tabdispord bigint, " +
                         " @p_tablegroup nvarchar(500)," +
                         " @p_secno nvarchar(50)," +
                         " @p_msttieupcompanyrid bigint," +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         " begin " +
                         " try " +
                         " begin " +
                         " if (@p_mode=0) " +
                         " begin  " +
                         " declare @codeRowCount as int  " +
                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                         " select @codeRowCount = (select count(*) from msttable where tablecode = @p_tablecode and ISNULL(DelFlg,0)=0)  " +
                         " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Table Code Already exits.'     " +
                           "   Return    " +
                         " End	" +
                   " Begin  " +
                     "  declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                      " select  @nameRowCount = (select count(*) from msttable where tablename = @p_tablename and ISNULL(DelFlg,0)=0)  " +
                      " if ( @nameRowCount > 0)    " +
                      " begin    " +
                      " set @p_Retval = 1 set @p_Errstr ='Table Name Already exits.'    " +
                      " Return  " +
                             "   End   " +
                             " end " +
                                         "  begin " +
                                         "  Insert Into msttable (tablecode,tablename,tabledesc,isparceltable,isroomtable,roomno,tabdisc,pricelistrid, " +
                                                " ishomedelitable,isnotcalccommi,ishidetable,tabpax,gsttaxtype,tabdispord,tablegroup,secno,msttieupcompanyrid,senddata,auserid,adatetime,DelFlg) " +
                                          " Values (@p_tablecode,@p_tablename,@p_tabledesc,@p_isparceltable,@p_isroomtable,@p_roomno,@p_tabdisc,@p_pricelistrid,@p_ishomedelitable,@p_isnotcalccommi,@p_ishidetable,@p_tabpax,@p_gsttaxtype,@p_tabdispord, " +
                                                    " @p_tablegroup,@p_secno,@p_msttieupcompanyrid," +
                                                    " 0,@p_userid,getdate(),0)" +
                                            " set @p_id=SCOPE_IDENTITY()" +
                                          " End  End  " +
                                 " else if (@p_mode=1) " +
                                      " begin    " +
                                      " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0     " +
                                      " select @codeRowCount1 = (select count(*) from msttable where tablecode = @p_tablecode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )" +
                                      " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Table Code Already exits.'     " +
                                      " Return   End  " +
                              " Begin  " +
                                "   declare  @nameRowCount1 int set @p_Errstr=''  set @p_Retval=0     " +
                                  " select  @nameRowCount1 = (select count(*) from msttable where tablename = @p_tablename and rid <> @p_rid and ISNULL(DelFlg,0)=0)  " +
                                  " if ( @nameRowCount1 > 0)    " +
                                  " begin    " +
                                  " set @p_Retval = 1 set @p_Errstr ='Table Name Already exits.' " +
                                  " Return  " +
                               " End  END " +
                              " begin  " +
                                      " Update msttable set tablecode=@p_tablecode, " +
                                      " tablename = @p_tablename, " +
                                      " tabledesc = @p_tabledesc, " +
                                      " isparceltable = @p_isparceltable,isroomtable = @p_isroomtable, " +
                                      " roomno = @p_roomno,tabdisc=@p_tabdisc,pricelistrid=@p_pricelistrid," +
                                      " ishomedelitable=@p_ishomedelitable,isnotcalccommi=@p_isnotcalccommi,ishidetable=@p_ishidetable," +
                                      " tabpax=@p_tabpax,gsttaxtype=@p_gsttaxtype,tabdispord=@p_tabdispord,tablegroup=@p_tablegroup,secno=@p_secno,msttieupcompanyrid=@p_msttieupcompanyrid," +
                                      " senddata=0,euserid = @p_userid,edatetime = getdate()  " +
                                      " where rid = @p_rid    " +
                                      " End  End  End  " +
                                        " End  " +
                                      " try  begin catch    " +
                                      " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                        "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                     " Return  END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_ROOMMAINTAINCE");
                strproc = "";
                strproc = " CREATE PROCEDURE SP_ROOMMAINTAINCE " +
                            " ( " +
                            " @p_mode as int, " +
                            " @p_rid bigint, " +
                            " @p_maindate datetime, " +
                            " @p_mainno nvarchar(50), " +
                            " @p_roomrid bigint, " +
                            " @p_mainby nvarchar(500), " +
                            " @p_mainbymobileno nvarchar(50), " +
                            " @p_mainstartdate datetime, " +
                            " @p_mainstarttime datetime, " +
                            " @p_mainenddate datetime, " +
                            " @p_mainendtime datetime, " +
                            " @p_mainroomstatus nvarchar(50), " +
                            " @p_otherstatus nvarchar(100), " +
                            " @p_mainmessage nvarchar(max), " +
                            " @p_mainremark nvarchar(max), " +
                            " @p_roomisfree bit, " +
                            " @p_userid bigint,  " +
                            " @p_errstr as nvarchar(max) out, " +
                            " @p_retval as int out, " +
                            " @p_id as bigint out " +
                            " ) As " +
                            " Begin " +
                             " try " +
                             " begin " +
                                "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                             " if (@p_mode=0)  " +
                             " begin " +
                                   "   Insert Into ROOMMAINTAINCE(MAINDATE,MAINNO,ROOMRID,MAINBY,MAINBYMOBILENO,MAINSTARTDATE,MAINSTARTTIME,MAINENDDATE,MAINENDTIME, " +
                                                                "  MAINROOMSTATUS,OTHERSTATUS,MAINMESSAGE,MAINREMARK,ROOMISFREE, " +
                                                                " auserid,adatetime,DelFlg) " +
                                                        " values (@p_MAINDATE,@p_MAINNO,@p_ROOMRID,@p_MAINBY,@p_MAINBYMOBILENO,@p_MAINSTARTDATE,@p_MAINSTARTTIME,@p_MAINENDDATE,@p_MAINENDTIME, " +
                                                                "  @p_MAINROOMSTATUS,@p_OTHERSTATUS,@p_MAINMESSAGE,@p_MAINREMARK,@p_ROOMISFREE, " +
                                                                " @p_userid,getdate(),0) " +
                                 " set @p_id=SCOPE_IDENTITY() " +
                                 " End " +
                             " else if (@p_mode=1)  " +
                                "  begin " +
                             " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                     " Update ROOMMAINTAINCE set MAINDATE=@p_MAINDATE,  " +
                                    " MAINNO=@p_MAINNO,ROOMRID=@p_ROOMRID,MAINBY=@p_MAINBY,MAINBYMOBILENO=@p_MAINBYMOBILENO,MAINSTARTDATE=@p_MAINSTARTDATE, " +
                                    " MAINSTARTTIME=@p_MAINSTARTTIME,MAINENDDATE=@p_MAINENDDATE,MAINENDTIME=@p_MAINENDTIME, " +
                                    " MAINROOMSTATUS=@p_MAINROOMSTATUS,OTHERSTATUS=@p_OTHERSTATUS,MAINMESSAGE=@p_MAINMESSAGE,MAINREMARK=@p_MAINREMARK,ROOMISFREE=@p_ROOMISFREE, " +
                                    " euserid = @p_userid,edatetime = getdate()  " +
                                     " where rid = @p_rid   " +
                                 " End " +
                             " End " +
                             " end " +
                             " try  " +
                                 " begin catch   " +
                                 " SELECT  ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage " +
                                  " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                 " Return  " +
                                 " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_CASHONHAND");
                strproc = " CREATE PROCEDURE SP_CASHONHAND " +
                                " (" +
                               "  @p_mode as int," +
                                " @p_rid bigint," +
                                " @p_cashdate datetime," +
                                " @p_cashamt decimal(18,2)," +
                               "  @p_cashstatus nvarchar(100)," +
                               "  @p_cashpersonname nvarchar(200)," +
                               "  @p_cashremark nvarchar(500)," +
                               "  @p_cashdesc nvarchar(max)," +
                               "  @p_emprid bigint, " +
                               "  @p_cashno nvarchar(100)," +
                               "  @p_userid bigint, " +
                                " @p_errstr as nvarchar(max) out, " +
                                " @p_retval as int out," +
                               "  @p_id as bigint out" +
                               "  ) as " +
                               "  begin " +
                               "  try " +
                                "  begin " +
                                    "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                     " if (@p_mode=0) " +
                                        "  begin 		" +
                                           "   Insert Into CASHONHAND (CASHDATE,CASHAMT,CASHSTATUS,CASHPERSONNAME,CASHREMARK,CASHDESC,EMPRID,CASHNO," +
                                                                  "    senddata,auserid,adatetime,DelFlg) " +
                                                     "   Values (@p_CASHDATE,@p_CASHAMT,@p_CASHSTATUS,@p_CASHPERSONNAME,@p_CASHREMARK,@p_CASHDESC,@p_emprid,@p_cashno," +
                                                              "  0,@p_userid,getdate(),0" +
                                                             "   )" +
                                          "    set @p_id=SCOPE_IDENTITY()" +
                                            "  End    " +
                                    "   else if (@p_mode=1)  " +
                                         " begin" +
                                            " set @p_Errstr=''  set @p_Retval=0 set @p_id=0" +
                                            " Update CASHONHAND " +
                                            " Set CASHDATE=@p_CASHDATE,CASHAMT=@p_CASHAMT,CASHSTATUS=@p_CASHSTATUS," +
                                               "  CASHPERSONNAME=@p_CASHPERSONNAME,CASHREMARK=@p_CASHREMARK,CASHDESC=@p_CASHDESC,emprid=@p_emprid,cashno=@p_cashno," +
                                               "  senddata=0,euserid = @p_userid,edatetime = getdate()  " +
                                               "  where rid = @p_rid  " +
                                         "  End " +
                                       " End" +
                                     " end	" +
                                    "  try  " +
                                         " begin catch    " +
                                         "   SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                         "   set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                         "   Return  " +
                                         "   END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_CASHONHANDREGSITER");
                strproc = " CREATE procedure sp_CASHONHANDREGSITER " +
                            " (@p_fromdate datetime,@p_todate datetime)  " +
                            " as " +
                            " begin " +
                            " select CASHONHAND.RID,CASHONHAND.CASHDATE,CASHONHAND.CASHAMT,CASHONHAND.CASHSTATUS,CASHONHAND.CASHPERSONNAME,CASHONHAND.CASHREMARK," +
                            " MSTEMP.EMPNAME " +
                            " FROM CASHONHAND" +
                            " LEFT JOIN MSTEMP ON (MSTEMP.RID=CASHONHAND.EMPRID)" +
                            " WHERE CASHONHAND.CASHDATE between @p_fromdate and @p_todate and isnull(CASHONHAND.delflg,0)=0 " +
                            " order by CASHONHAND.CASHDATE,CASHONHAND.RID" +
                            " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_SUPPLIEROUTSTANDING");
                strproc = " CREATE PROCEDURE sp_SUPPLIEROUTSTANDING " +
                            " (@p_fromdate datetime,@p_todate datetime)  AS " +
                            " BEGIN  " +
                            " select MSTSUPPLIER.rid as SUPPLIERRID,MSTSUPPLIER.SUPPNAME," +
                            " isnull(Purchaseinfo.PurchaseAmount,0) as PurchaseAmount," +
                            " isnull(Payinfo.PayAmount,0) as PaymentAmount, " +
                            " (isnull(Purchaseinfo.PurchaseAmount,0) - isnull(Payinfo.PayAmount,0)) As PendingPayAmount   " +
                            " from MSTSUPPLIER  " +
                            " LEFT JOIN  " +
                                " (SELECT SUM(ITEMPURCHASE.NETAMOUNT) AS PurchaseAmount,ITEMPURCHASE.SUPPRID   " +
                                    " FROM  ITEMPURCHASE  WHERE (ISNULL(ITEMPURCHASE.DELFLG, 0) = 0 " +
                                    " and isnull(ITEMPURCHASE.SUPPRID,0)>0   " +
                                    " And  ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate) " +
                                     " GROUP BY ITEMPURCHASE.SUPPRID) AS Purchaseinfo " +
                            " ON (Purchaseinfo.SUPPRID = MSTSUPPLIER.rid)  " +
                            " LEFT JOIN " +
                                " (SELECT SUM(ISNULL(PAYMENTINFO.PAYAMOUNT,0)) AS PayAmount,ITEMPURCHASE.SUPPRID" +
                                    " FROM  PAYMENTINFO " +
                                    " LEFT JOIN ITEMPURCHASE on (ITEMPURCHASE.RID=PAYMENTINFO.PURRID)" +
                                    " WHERE (ISNULL(PAYMENTINFO.DELFLG, 0) = 0 and isnull(ITEMPURCHASE.SUPPRID,0)>0  " +
                                    " And  PAYMENTINFO.PAYDATE between @p_fromdate and @p_todate)   " +
                                    " GROUP BY ITEMPURCHASE.SUPPRID) AS Payinfo " +
                                " ON (Payinfo.SUPPRID = MSTSUPPLIER.rid)  " +
                            " where (isnull(Purchaseinfo.PurchaseAmount,0) - isnull(Payinfo.PayAmount,0))>0  " +
                            " ORDER BY MSTSUPPLIER.SUPPNAME,MSTSUPPLIER.rid  END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLGIVETOCUSTOMERREGISTER");
                strproc = " Create procedure sp_BILLGIVETOCUSTOMERREGISTER " +
                            " (@p_fromdate datetime,@p_todate datetime)  " +
                            " as  begin    " +
                            " Select BILL.RID,BILL.BILLDATE,BILL.BILLNO,BILL.BILLTYPE,BILL.BILLPAX, " +
                            " MSTTABLE.TABLENAME,BILL.TOTAMOUNT,BILL.TOTSERTAXAMOUNT,BILL.TOTVATAMOUNT, " +
                            " BILL.TOTADDVATAMOUNT,BILL.TOTDISCAMOUNT,BILL.NETAMOUNT, bill.REFBILLNO,bill.REFBILLNUM, " +
                            " BILLDTL.RID AS BILLDTLRID, MSTITEM.INAME,BILLDTL.IQTY,BILLDTL.IRATE,BILLDTL.IPAMT,BILLDTL.IAMT, " +
                            " BILL.TOTLIQVATAMT,BILL.TOTBEVVATAMT,BILL.TOTROFF, " +
                            " BILL.TOTADDDISCAMT,BILL.TOTSERCHRAMT,BILL.TOTGSTPER,BILL.TOTGSTAMT " +
                            " FROM BILL  " +
                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID = BILL.RID)  " +
                            " LEFT JOIN MSTTABLE ON (MSTTABLE.RID = BILL.TABLERID)  " +
                            " LEFT JOIN MSTITEM ON (MSTITEM.RID = BILLDTL.IRID)  " +
                            " WHERE BILL.BILLDATE between @p_fromdate and @p_todate and isnull(BILL.ISBILLTOCUSTOMER,0)=1 " +
                            " AND isnull(BILL.delflg,0)=0 and isnull(BILLDTL.delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0 " +
                             " order by BILL.BILLDATE  end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_MSTDEPT");
                strproc = " Create Procedure sp_MSTDEPT " +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid bigint, " +
                         " @p_deptcode nvarchar(20)," +
                         " @p_deptname nvarchar(100)," +
                         " @p_deptdesc nvarchar(max)," +
                         " @p_isbardept bit, " +
                         " @p_iskitchendept bit," +
                         " @p_ishukkadept bit," +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         " begin " +
                         " try " +
                         " begin " +
                         " if (@p_mode=0) " +
                         " begin  " +
                         " declare @codeRowCount as int  " +
                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                         " select @codeRowCount = (select count(*) from mstdept where deptcode = @p_deptcode and ISNULL(DelFlg,0)=0)  " +
                         " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Department Code Already exits.'     " +
                           "   Return    " +
                         " End	" +
                   " Begin  " +
                     "  declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                      " select  @nameRowCount = (select count(*) from mstdept where deptname = @p_deptname and ISNULL(DelFlg,0)=0)     " +
                      " if ( @nameRowCount > 0)   " +
                      " begin    " +
                      " set @p_Retval = 1 set @p_Errstr ='Department Name Already exits.' " +
                      " Return  " +
                              "  End   " +
                             " end " +
                                         "  begin " +
                                          " Insert Into mstdept (deptcode,deptname,deptdesc,isbardept,iskitchendept,ishukkadept,SENDDATA,auserid,adatetime,DelFlg) " +
                                          " Values (@p_deptcode,@p_deptname,@p_deptdesc,@p_isbardept, @p_iskitchendept,@p_ishukkadept,0," +
                                                    " @p_userid,getdate(),0)" +
                                            " set @p_id=SCOPE_IDENTITY()		" +
                                          " End  End  " +
                                 " else if (@p_mode=1) " +
                                   "    begin    " +
                                     "  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0     " +
                                      " select @codeRowCount1 = (select count(*) from mstdept where deptcode = @p_deptcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                      " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Department Code Already exits.'      " +
                                      " Return   End  " +
                              " Begin  " +
                                "   declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0  " +
                                 "  select  @nameRowCount1 = (select count(*) from mstdept where deptname = @p_deptname and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                                  " if ( @nameRowCount1 > 0)   " +
                                  " begin    " +
                                  " set @p_Retval = 1 set @p_Errstr ='Department Name Already exits.' " +
                                  " Return  " +
                               " End  END " +
                              " begin  " +
                                      " Update mstdept set deptcode=@p_deptcode, " +
                                      " deptname = @p_deptname, deptdesc = @p_deptdesc, isbardept=@p_isbardept,iskitchendept=@p_iskitchendept, " +
                                      " ishukkadept=@p_ishukkadept,senddata=0," +
                                      " euserid = @p_userid,edatetime = getdate()    " +
                                      " where rid = @p_rid  " +
                                      " End  End  End  " +
                                        " End  " +
                                      " try  begin catch   " +
                                      " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; " +
                                        "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                     " Return  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_MSTINGREDIANT");
                strproc = "create procedure sp_MSTINGREDIANT" +
                        " (" +
                        " @p_mode as int," +
                        " @p_rid bigint," +
                        " @p_ingcode nvarchar(50)," +
                        " @p_ingname nvarchar(250)," +
                        " @p_ingdesc nvarchar(max)," +
                        " @p_ingimage image," +
                        " @p_userid bigint," +
                        " @p_errstr as nvarchar(max) out," +
                        " @p_retval as int out,@p_id as bigint out " +
                        " ) as" +
                        " begin" +
                        " try" +
                        " begin" +
                        " if (@p_mode=0)" +
                       " begin " +
                        " declare @codeRowCount as int " +
                        " set @p_Errstr=''  set @p_Retval=0  set @p_id=0 " +
                        " select @codeRowCount = (select count(*) from MSTINGREDIANT where INGCODE = @p_ingcode and ISNULL(DelFlg,0)=0)  " +
                        " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'  " +
                          "   Return   " +
                        " End	   " +
                  " Begin " +
                    "  declare  @nameRowCount as int   set @p_Errstr=''  set @p_Retval=0   set @p_id=0  " +
                     " select  @nameRowCount = (select count(*) from MSTINGREDIANT where INGNAME = @p_ingname and ISNULL(DelFlg,0)=0)" +
                     " if ( @nameRowCount > 0)   " +
                     " begin   " +
                     " set @p_Retval = 1 set @p_Errstr ='Name Already exits.'   " +
                     " Return " +
                             "  End  " +
                            " end " +
                                         " begin" +
                                         " Insert Into MSTINGREDIANT (INGCODE,INGNAME,INGIMAGE,INGDESC,auserid,adatetime,DelFlg)" +
                                         " Values (@p_INGCODE,@p_INGNAME,@p_INGIMAGE,@p_INGDESC,@p_userid,getdate(),0)  " +
                                         " End  set @p_id=SCOPE_IDENTITY() End " +
                                " else if (@p_mode=1)" +
                                  "    begin   " +
                                    "  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0   set @p_id=0  " +
                                     " select @codeRowCount1 = (select count(*) from MSTINGREDIANT where INGCODE = @p_INGCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                                     " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'     " +
                                     " Return   End " +
                             " Begin " +
                               "   declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0  set @p_id=0   " +
                                 " select  @nameRowCount1 = (select count(*) from MSTINGREDIANT where INGNAME = @p_INGNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                                 " if ( @nameRowCount1 > 0)   " +
                                 " begin   " +
                                 " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                                 " Return " +
                              " End  END" +
                             " begin " +
                                     " Update MSTINGREDIANT set INGCODE=@p_INGCODE," +
                                     " INGNAME = @p_INGNAME, INGIMAGE = @p_INGIMAGE,INGDESC=@p_INGDESC," +
                                     " euserid = @p_userid,edatetime = getdate()   " +
                                     " where rid = @p_rid   " +
                                     " End  End  End  End " +
                                     " try  begin catch   " +
                                     " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                       "  set  @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                    " Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_ITEMRECIPE");
                strproc = " CREATE PROCEDURE SP_ITEMRECIPE " +
                                    " ( " +
                                     " @p_mode as int, " +
                                     " @p_rid bigint, " +
                                     " @p_itemrid bigint, " +
                                     " @p_preptime decimal(18,2), " +
                                     " @p_profitamt decimal(18,2), " +
                                     " @p_itemwgt decimal(18,2), " +
                                     " @p_itemmethod nvarchar(max), " +
                                     " @p_inmnm1 bigint, " +
                                     " @p_inggrm1 decimal(18,2), " +
                                     " @p_ingunit1 nvarchar(50), " +
                                     " @p_inmnm2 bigint, " +
                                     " @p_inggrm2 decimal(18,2), " +
                                     " @p_ingunit2 nvarchar(50), " +
                                     " @p_inmnm3 bigint, " +
                                     " @p_inggrm3 decimal(18,2), " +
                                     " @p_ingunit3 nvarchar(50), " +
                                     " @p_inmnm4 bigint, " +
                                     " @p_inggrm4 decimal(18,2), " +
                                     " @p_ingunit4 nvarchar(50), " +
                                     " @p_inmnm5 bigint, " +
                                     " @p_inggrm5 decimal(18,2), " +
                                     " @p_ingunit5 nvarchar(50), " +
                                     " @p_inmnm6 bigint, " +
                                     " @p_inggrm6 decimal(18,2), " +
                                     " @p_ingunit6 nvarchar(50), " +
                                     " @p_inmnm7 bigint, " +
                                     " @p_inggrm7 decimal(18,2), " +
                                     " @p_ingunit7 nvarchar(50), " +
                                     " @p_inmnm8 bigint, " +
                                     " @p_inggrm8 decimal(18,2), " +
                                     " @p_ingunit8 nvarchar(50), " +
                                     " @p_inmnm9 bigint, " +
                                     " @p_inggrm9 decimal(18,2), " +
                                     " @p_ingunit9 nvarchar(50), " +
                                     " @p_inmnm10 bigint, " +
                                     " @p_inggrm10 decimal(18,2), " +
                                     " @p_ingunit10 nvarchar(50), " +
                                     " @p_userid bigint,  " +
                                     " @p_errstr as nvarchar(max) out,  " +
                                     " @p_retval as int out, " +
                                     " @p_id as bigint out " +
                                     " ) as  " +
                                     " begin  " +
                                     " try  " +
                                      " begin  " +
                                         "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                          " if (@p_mode=0)  " +
                                             "  begin 		 " +
                                                 "  Insert Into ITEMRECIPE (ITEMRID,PREPTIME,PROFITAMT,ITEMWGT,ITEMMETHOD,INMNM1,INGGRM1,INGUNIT1, " +
                                                                           "  INMNM2,INGGRM2,INGUNIT2,INMNM3,INGGRM3,INGUNIT3,INMNM4,INGGRM4,INGUNIT4, " +
                                                                            " INMNM5,INGGRM5,INGUNIT5,INMNM6,INGGRM6,INGUNIT6,INMNM7,INGGRM7,INGUNIT7, " +
                                                                            " INMNM8,INGGRM8,INGUNIT8,INMNM9,INGGRM9,INGUNIT9,INMNM10,INGGRM10,INGUNIT10,	 " +
                                                                            " auserid,adatetime,DelFlg)  " +
                                                           "  Values (@p_ITEMRID,@p_PREPTIME,@p_PROFITAMT,@p_ITEMWGT,@p_ITEMMETHOD,@p_INMNM1,@p_INGGRM1,@p_INGUNIT1, " +
                                                                           "  @p_INMNM2,@p_INGGRM2,@p_INGUNIT2,@p_INMNM3,@p_INGGRM3,@p_INGUNIT3,@p_INMNM4,@p_INGGRM4,@p_INGUNIT4, " +
                                                                            " @p_INMNM5,@p_INGGRM5,@p_INGUNIT5,@p_INMNM6,@p_INGGRM6,@p_INGUNIT6,@p_INMNM7,@p_INGGRM7,@p_INGUNIT7, " +
                                                                            " @p_INMNM8,@p_INGGRM8,@p_INGUNIT8,@p_INMNM9,@p_INGGRM9,@p_INGUNIT9,@p_INMNM10,@p_INGGRM10,@p_INGUNIT10, " +
                                                                            " @p_userid,getdate(),0 " +
                                                                    " ) " +
                                                  " set @p_id=SCOPE_IDENTITY() " +
                                                  " End     " +
                                           " else if (@p_mode=1)   " +
                                             "  begin " +
                                                "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                                  "  Update ITEMRECIPE  " +
                                                        " set ITEMRID=@p_ITEMRID,PREPTIME=@p_PREPTIME,PROFITAMT=@p_PROFITAMT,ITEMWGT=@p_ITEMWGT,ITEMMETHOD=@p_ITEMMETHOD, " +
                                                                            " INMNM1=@p_INMNM1,INGGRM1=@p_INGGRM1,INGUNIT1=@p_INGUNIT1, " +
                                                                            " INMNM2=@p_INMNM2,INGGRM2=@p_INGGRM2,INGUNIT2=@p_INGUNIT2,INMNM3=@p_INMNM3,INGGRM3=@p_INGGRM3, " +
                                                                            " INGUNIT3=@p_INGUNIT3,INMNM4=@p_INMNM4,INGGRM4=@p_INGGRM4,INGUNIT4=@p_INGUNIT4, " +
                                                                            " INMNM5=@p_INMNM5,INGGRM5=@p_INGGRM5,INGUNIT5=@p_INGUNIT5,INMNM6=@p_INMNM6,INGGRM6=@p_INGGRM6,INGUNIT6=@p_INGUNIT6, " +
                                                                            " INMNM7=@p_INMNM7,INGGRM7=@p_INGGRM7,INGUNIT7=@p_INGUNIT7, " +
                                                                            " INMNM8=@p_INMNM8,INGGRM8=@p_INGGRM8,INGUNIT8=@p_INGUNIT8,INMNM9=@p_INMNM9,INGGRM9=@p_INGGRM9,INGUNIT9=@p_INGUNIT9, " +
                                                                            " INMNM10=@p_INMNM10,INGGRM10=@p_INGGRM10,INGUNIT10=@p_INGUNIT10, " +
                                                                            " euserid = @p_userid,edatetime = getdate() " +
                                                     " where rid = @p_rid   " +
                                               " End  " +
                                            " End " +
                                          " end	 " +
                                          " try   " +
                                             "  begin catch     " +
                                               "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                                " Return   " +
                                                " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_MSTSUPPLIER");
                strproc = " CREATE procedure sp_MSTSUPPLIER " +
                            " ( " +
                                " @p_mode int," +
                                " @p_rid bigint," +
                                " @p_suppcode nvarchar(50)," +
                                " @p_suppname nvarchar(100)," +
                                " @p_suppadd1 nvarchar(100)," +
                                " @p_suppadd2 nvarchar(100)," +
                                " @p_suppadd3 nvarchar(100)," +
                                " @p_suppcityid bigint," +
                                " @p_suppstateid bigint," +
                                " @p_suppcountryid bigint," +
                                " @p_supppin nvarchar(50)," +
                                " @p_supptelno nvarchar(50)," +
                                " @p_suppmobno nvarchar(50)," +
                                " @p_suppfaxno nvarchar(50)," +
                                " @p_suppcontpername1 nvarchar(250)," +
                                " @p_suppcontpername2 nvarchar(250)," +
                                " @p_suppemail nvarchar(250)," +
                                " @p_supppanno nvarchar(50)," +
                                " @p_supptinno nvarchar(50)," +
                                " @p_suppcstno nvarchar(50)," +
                                " @p_suppgstno nvarchar(50)," +
                                " @p_suppremark nvarchar(max)," +
                                " @p_suppimage image," +
                                " @p_supptype nvarchar(1000)," +
                                " @p_supppayment nvarchar(50)," +
                                " @p_itemtype nvarchar(50)," +
                                " @p_userid bigint, " +
                                " @p_errstr as nvarchar(max) out, " +
                                " @p_retval as int out," +
                                " @p_id as bigint out" +
                                 " ) as " +
                                 " begin" +
                            " try" +
                                " begin" +
                                " if (@p_mode=0) " +
                                 " begin  " +
                                 " declare @codeRowCount as int " +
                                 " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                 " select @codeRowCount = (select count(*) from mstsupplier where suppcode = @p_suppcode and ISNULL(DelFlg,0)=0) " +
                                 " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Supplier Code Already exits.'     " +
                                   "   Return    " +
                                 " End	  " +
                            " Begin  " +
                            " SET NOCOUNT ON" +
                              " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                              " select  @nameRowCount = (select count(*) from mstsupplier where suppname = @p_suppname and ISNULL(DelFlg,0)=0)   " +
                              " if ( @nameRowCount > 0)    " +
                              " begin    " +
                              " set @p_Retval = 1 set @p_Errstr ='Supplier Name Already exits.'  " +
                              " Return  " +
                                     "   End   " +
                                     " end " +
                                                "   begin " +
                                                  " Insert Into mstsupplier (suppcode,SUPPNAME,SUPPADD1,SUPPADD2,SUPPADD3,SUPPCITYID,SUPPSTATEID," +
                                                                            " SUPPCOUNTRYID,SUPPPIN,SUPPTELNO,SUPPMOBNO,SUPPFAXNO," +
                                                                            " SUPPCONTPERNAME1,SUPPCONTPERNAME2,SUPPEMAIL,SUPPPANNO,SUPPTINNO," +
                                                                            " SUPPCSTNO,SUPPGSTNO,SUPPREMARK,SUPPIMAGE," +
                                                                            " supptype,supppayment,itemtype, " +
                                                                            " SENDDATA,auserid,adatetime,DelFlg)					" +
                                                              " Values (@p_suppcode,@p_SUPPNAME,@p_SUPPADD1,@p_SUPPADD2,@p_SUPPADD3,@p_SUPPCITYID,@p_SUPPSTATEID," +
                                                                            " @p_SUPPCOUNTRYID,@p_SUPPPIN,@p_SUPPTELNO,@p_SUPPMOBNO,@p_SUPPFAXNO," +
                                                                            " @p_SUPPCONTPERNAME1,@p_SUPPCONTPERNAME2,@p_SUPPEMAIL,@p_SUPPPANNO,@p_SUPPTINNO," +
                                                                            " @p_SUPPCSTNO,@p_SUPPGSTNO,@p_SUPPREMARK,@p_SUPPIMAGE," +
                                                                            " @p_supptype,@p_supppayment,@p_itemtype, " +
                                                                            " 0,@p_userid,getdate(),0)" +
                                                    " set @p_id=SCOPE_IDENTITY()" +
                                                  " End  End  " +
                                         " else if (@p_mode=1) " +
                                           "    begin    " +
                                             "  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0     " +
                                              " select @codeRowCount1 = (select count(*) from MSTSUPPLIER where SUPPCODE = @p_SUPPCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                              " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Supplier Code Already exits.'  " +
                                              " Return   End  " +
                                      " Begin  " +
                                        "   declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0  " +
                                         "  select  @nameRowCount1 = (select count(*) from MSTSUPPLIER where SUPPNAME = @p_SUPPNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                                          " if ( @nameRowCount1 > 0) " +
                                          " begin    " +
                                          " set @p_Retval = 1 set @p_Errstr ='Supplier Name Already exits.' " +
                                          " Return  " +
                                       " End  END " +
                                      " begin  " +
                                              " Update mstsupplier set SUPPCODE=@p_SUPPCODE, " +
                                              " SUPPNAME = @p_SUPPNAME,SUPPADD1 = @p_SUPPADD1, " +
                                              " SUPPADD2 = @p_SUPPADD2,SUPPADD3 = @p_SUPPADD3," +
                                              " SUPPCITYID = @p_SUPPCITYID,SUPPSTATEID = @p_SUPPSTATEID,SUPPCOUNTRYID = @p_SUPPCOUNTRYID," +
                                              " SUPPPIN = @p_SUPPPIN," +
                                              " SUPPTELNO = @p_SUPPTELNO,SUPPMOBNO = @p_SUPPMOBNO,SUPPFAXNO = @p_SUPPFAXNO," +
                                             " SUPPCONTPERNAME1 = @p_SUPPCONTPERNAME1,SUPPCONTPERNAME2 = @p_SUPPCONTPERNAME2," +
                                             " SUPPEMAIL = @p_SUPPEMAIL,SUPPPANNO = @p_SUPPPANNO,SUPPTINNO = @p_SUPPTINNO," +
                                              " SUPPCSTNO = @p_SUPPCSTNO,SUPPGSTNO = @p_SUPPGSTNO,SUPPREMARK = @p_SUPPREMARK," +
                                              " SUPPIMAGE=@p_SUPPIMAGE, supptype=@p_supptype,supppayment=@p_supppayment,itemtype=@p_itemtype, " +
                                              " SENDDATA=0,euserid = @p_userid,edatetime = getdate() " +
                                              " where rid = @p_rid    " +
                                              " End  End  End  " +
                                            " End  " +
                                             " try  begin catch  " +
                                              " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;    " +
                                                "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() " +
                                                 " set @p_id=0" +
                                             " Return  END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_INGWISESUMMARY");
                strproc = " CREATE PROCEDURE SP_INGWISESUMMARY " +
                            " (@p_fromdate datetime,@p_todate datetime ) AS " +
                            " BEGIN" +
                            " SELECT INGBASEINFO.INGNAME1,INGBASEINFO.INGUNIT1,INGBASEINFO.ING1RID," +
                            " SUM(INGBASEINFO.IQTY) AS QTY,SUM(INGBASEINFO.INGGRM1) AS GRM,SUM(INGBASEINFO.OPGRAM) AS RESULTGRAM" +
                            " FROM INGBASEINFO" +
                            " WHERE INGBASEINFO.BILLDATE between @p_fromdate and @p_todate" +
                            " GROUP BY INGBASEINFO.INGNAME1,INGBASEINFO.INGUNIT1,INGBASEINFO.ING1RID" +
                            " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_INGMWISEDETAILS");
                strproc = " CREATE PROCEDURE SP_INGMWISEDETAILS" +
                               " (@p_fromdate datetime,@p_todate datetime) AS " +
                                " BEGIN" +
                                " SELECT INGBASEINFO.IRID,INGBASEINFO.INAME," +
                                " SUM(INGBASEINFO.IQTY) AS QTY,SUM(INGBASEINFO.INGGRM1) AS GRM," +
                                " INGBASEINFO.INGNAME1,INGBASEINFO.INGUNIT1," +
                                " INGBASEINFO.ING1RID," +
                                " SUM(INGBASEINFO.OPGRAM) AS OPGRAM" +
                                " from INGBASEINFO" +
                                " WHERE INGBASEINFO.BILLDATE between @p_fromdate and @p_todate" +
                                " GROUP BY INGBASEINFO.IRID,INGBASEINFO.INAME,INGBASEINFO.INGNAME1,INGBASEINFO.INGUNIT1,INGBASEINFO.ING1RID" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_DEPARTMENTWISESALES");
                strproc = " create procedure SP_DEPARTMENTWISESALES  " +
                               "  (@p_fromdate datetime,@p_todate datetime) AS BEGIN  " +
                                 " Select MSTDEPT.RID,MSTDEPT.DEPTCODE,MSTDEPT.DEPTNAME, " +
                                  " MSTITEM.RID AS IRID,MSTITEM.INAME," +
                                 " sum(billdtl.iqty) as DEPTQTY,sum(billdtl.IAMT) as DEPTAMT " +
                                 " from bill  " +
                                 " inner join billdtl on (billdtl.BILLRID = bill.rid)  " +
                                 " inner join MSTITEM on (MSTITEM.rid = billdtl.IRID) " +
                                 " left join MSTDEPT on (MSTDEPT.rid = MSTITEM.IDEPTRID)  " +
                                 " where isnull(bill.delflg,0)=0 " +
                                 " and isnull(billdtl.delflg,0)=0 " +
                                 " and isnull(BILL.ISREVISEDBILL,0)=0  " +
                                 " and isnull(MSTDEPT.RID,0)>0 " +
                                 " and bill.billDATE between @p_fromdate and @p_todate  " +
                                 " GROUP BY MSTDEPT.RID,MSTDEPT.DEPTCODE,MSTDEPT.DEPTNAME,MSTITEM.RID,MSTITEM.INAME  " +
                                 " order by MSTDEPT.RID  " +
                                 " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLREGWITHPARCLE");
                strproc = " CREATE procedure sp_BILLREGWITHPARCLE   " +
                            " (@p_fromdate datetime,@p_todate datetime) as   " +
                            " begin   " +
                            " select bill.rid,bill.billno,bill.REFBILLNO,bill.billdate,bill.billpax, " +
                            " MSTTABLE.TABLENAME, ISNULL(MSTTABLE.ISPARCELTABLE,0) AS ISPARCELTABLE, " +
                            " bill.totamount,(CASE WHEN ISNULL(MSTTABLE.ISPARCELTABLE,0)=1 THEN bill.totamount ELSE 0 END) AS TOTAMOUNTPARCLE, " +
                            " bill.totsertaxamount,(CASE WHEN ISNULL(MSTTABLE.ISPARCELTABLE,0)=1 THEN bill.totsertaxamount ELSE 0 END) AS totsertaxamountparcle, " +
                            " bill.totvatamount, bill.totaddvatper as totsercharge,bill.totdiscamount,bill.TOTROFF, " +
                            " bill.TOTLIQVATAMT,bill.TOTBEVVATAMT,bill.TOTADDDISCAMT,bill.TOTSERCHRAMT,bill.TOTKKCESSAMT,bill.TOTADDVATAMOUNT," +
                            " bill.Netamount,(CASE WHEN ISNULL(MSTTABLE.ISPARCELTABLE,0)=1 THEN Bill.Netamount ELSE 0 END) AS netamountparcle " +
                            " from bill   " +
                            " Left Join MSTTABLE ON (MSTTABLE.rid=bill.tablerid)   " +
                            " WHERE bill.billDATE between @p_fromdate and @p_todate  " +
                            " And isnull(bill.Delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0   " +
                            " order by bill.billDATE,bill.rid " +
                            " end  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_ITEMMASTERREPORT");
                strproc = " CREATE PROCEDURE SP_ITEMMASTERREPORT  " +
                            " (@p_fromdate datetime) " +
                            " AS BEGIN " +
                            " SELECT MSTITEM.RID,MSTITEM.ICODE,MSTITEM.INAME,IRATE,MSTITEMGROUP.RID AS IGRID,MSTITEMGROUP.IGNAME FROM MSTITEM" +
                            " LEFT JOIN MSTITEMGROUP ON (MSTITEMGROUP.RID = MSTITEM.IGRPRID)" +
                            " WHERE ISNULL(MSTITEM.DELFLG,0)=0 AND ISNULL(MSTITEMGROUP.DELFLG,0)=0 AND MSTITEM.ADATETIME > @p_fromdate" +
                            " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLREGDATEWISE");
                strproc = "  CREATE procedure sp_BILLREGDATEWISE " +
                                 " (@p_fromdate datetime,@p_todate datetime) as  " +
                                " begin Select 'RESTAURANT' AS BILLTYPE,bill.billdate,SUM(isnull(bill.billpax,0)) AS BILLPAX," +
                                " SUM(isnull(bill.totamount,0)) AS TotAmount," +
                                " SUM(isnull(bill.TOTSERCHRAMT,0)) AS TOTSERCHRAMT," +
                                " sum(isnull(bill.totsertaxamount,0)) as TotSerTaxAmount," +
                                " sum(isnull(bill.totvatamount,0)) as TotVatAmount, " +
                                " sum(isnull(bill.totaddvatper,0)) As TotSerCharge," +
                                " sum(isnull(bill.totdiscamount,0)) As TotDiscAmount," +
                                " sum(isnull(bill.TOTROFF,0)) As  TotRoff," +
                                " sum(isnull(bill.TOTLIQVATAMT,0)) as TOTLIQVATAMT, " +
                                " sum(isnull(bill.TOTBEVVATAMT,0)) as TOTBEVVATAMT, " +
                                " SUM(isnull(bill.TOTADDDISCAMT,0)) AS TOTADDDISCAMT," +
                                " SUM(isnull(bill.TOTKKCESSAMT,0)) AS TOTKKCESSAMT," +
                                " SUM(isnull(bill.TOTGSTAMT,0)) AS TOTGSTAMT," +
                                " sum(isnull(bill.netamount,0)) as TotNetAmount " +
                                " From bill  " +
                                " WHERE bill.billDATE between @p_fromdate and @p_todate" +
                                " and isnull(bill.delflg,0)=0 And isnull(BILL.ISREVISEDBILL,0)=0 AND isnull(BILL.ISPARCELBILL,0)=0 " +
                                " GROUP BY bill.billdate  " +
                                " UNION ALL " +
                                " SELECT 'PARCLE' AS BILLTYPE,bill.billdate,SUM(isnull(bill.billpax,0)) AS BILLPAX," +
                                " SUM(isnull(bill.totamount,0)) AS TotAmount, " +
                                " SUM(isnull(bill.TOTSERCHRAMT,0)) AS TOTSERCHRAMT," +
                                " sum(isnull(bill.totsertaxamount,0)) as TotSerTaxAmount, " +
                                " sum(isnull(bill.totvatamount,0)) as TotVatAmount,  sum(isnull(bill.totaddvatper,0)) As TotSerCharge, " +
                                " sum(isnull(bill.totdiscamount,0)) As TotDiscAmount, sum(isnull(bill.TOTROFF,0)) As  TotRoff, " +
                                " sum(isnull(bill.TOTLIQVATAMT,0)) as TOTLIQVATAMT, " +
                                " sum(isnull(bill.TOTBEVVATAMT,0)) as TOTBEVVATAMT, " +
                                " SUM(isnull(bill.TOTADDDISCAMT,0)) AS TOTADDDISCAMT," +
                                " SUM(isnull(bill.TOTKKCESSAMT,0)) AS TOTKKCESSAMT," +
                                " SUM(isnull(bill.TOTGSTAMT,0)) AS TOTGSTAMT," +
                                " sum(isnull(bill.netamount,0)) as TotNetAmount  " +
                                " From Bill " +
                                " WHERE " +
                                " bill.billDATE between @p_fromdate and @p_todate and " +
                                " isnull(bill.delflg,0)=0 And isnull(BILL.ISREVISEDBILL,0)=0 " +
                                " AND isnull(BILL.ISPARCELBILL,0)=1" +
                                " GROUP BY bill.billdate" +
                                " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_STOCKISSUE");
                strproc = " create procedure sp_STOCKISSUE" +
                             " ( @p_mode as int,  @p_rid as bigint,@p_issueno nvarchar(50)," +
                                " @p_issuedate datetime,@p_issuerefno nvarchar(50),@p_issuerefdate datetime, " +
                                " @p_issuedeptrid bigint,@p_issuefromperrid bigint,@p_issuetoperrid bigint," +
                                " @p_issueremark nvarchar(max)," +
                                " @p_userid bigint,  @p_errstr as nvarchar(max) out," +
                                " @p_retval as int out, @p_id as bigint out)  " +
                            " as  " +
                            " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                            " if (@p_mode=0)   " +
                            " begin 		  " +
                            " Insert Into STOCKISSUE (ISSUENO,ISSUEDATE,ISSUEREFNO,ISSUEREFDATE,ISSUEDEPTRID,ISSUEFROMPERRID,ISSUETOPERRID,ISSUREMARK," +
                                                    " AUSERID,ADATETIME,DELFLG) " +
                                               " Values (@p_issueno,@p_issuedate,@p_issuerefno,@p_issuerefdate," +
                                                        " @p_issuedeptrid,@p_issuefromperrid,@p_issuetoperrid,@p_issueremark," +
                                                        " @p_userid,getdate(),0 )  " +
                                        " set @p_id=SCOPE_IDENTITY() End      " +
                            " else if (@p_mode=1)    " +
                              "   begin set @p_Errstr=''  set @p_Retval=0 set @p_id=0" +
                                "  update STOCKISSUE set ISSUENO=@p_issueno,ISSUEDATE=@p_issuedate,ISSUEREFNO=@p_issuerefno," +
                                            " ISSUEREFDATE=@p_issuerefdate,ISSUEDEPTRID=@p_issuedeptrid,ISSUEFROMPERRID=@p_issuefromperrid," +
                                            " ISSUETOPERRID=@p_issuetoperrid,ISSUREMARK=@p_issueremark," +
                                            " euserid = @p_userid,edatetime = getdate()  " +
                                            " where rid = @p_rid  " +
                                 " end " +
                              " End end " +
                                " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return    " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_STOCKISSUEDTL");
                strproc = " create  procedure sp_STOCKISSUEDTL" +
                           " (  " +
                           " @p_mode as int,  " +
                           " @p_rid as bigint, " +
                           " @p_issuerid bigint, " +
                           " @p_irid bigint," +
                           " @p_iname nvarchar(100)," +
                           " @p_iqty decimal(18,3)," +
                           " @p_iunit nvarchar(50)," +
                           " @p_irate decimal(18,2)," +
                           " @p_userid bigint,  " +
                           " @p_errstr as nvarchar(max) out,  " +
                           " @p_retval as int out, " +
                           " @p_id as bigint out  " +
                           " ) as  " +
                           " begin  " +
                           " try  " +
                               " begin  " +
                                   " set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                                    " if (@p_mode=0)   " +
                                       " begin  " +
                                        " Insert Into STOCKISSUEDTL (ISSUERID,IRID,INAME,IQTY,IUNIT,IRATE,auserid,adatetime,DelFlg) " +
                                                            " Values (@p_ISSUERID,@p_irid,@p_INAME,@p_IQTY,@p_IUNIT,@p_irate,@p_userid,getdate(),0 )" +
                                                    " set @p_id=SCOPE_IDENTITY() " +
                                                " End   " +
                                   " else if (@p_mode=1)    " +
                                       " begin  " +
                                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                           " update STOCKISSUEDTL  " +
                                          " set INAME=@p_INAME,irid=@p_irid,IQTY=@p_IQTY,IUNIT=@p_IUNIT,IRATE=@p_irate," +
                                                                    "  euserid = @p_userid,edatetime = getdate()    " +
                                                                     " where rid = @p_rid and ISSUERID=@p_ISSUERID  " +
                                       " End  " +
                                    " End  " +
                                   " end	 " +
                                   " try   " +
                                       " begin catch   " +
                                        " SELECT   ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;    " +
                                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                        " Return   " +
                                        " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_PURISSUEDTLREG");
                strproc = " CREATE PROCEDURE SP_PURISSUEDTLREG" +
                            " (@p_fromdate datetime,@p_todate datetime) " +
                             " as " +
                             " begin   " +
                             " select STOCKISSUE.RID,STOCKISSUE.ISSUENO,STOCKISSUE.ISSUEDATE,STOCKISSUE.ISSUEREFNO,STOCKISSUE.ISSUEREFDATE,STOCKISSUE.ISSUREMARK," +
                                   "  MSTDEPT.RID AS DEPTRID,MSTDEPT.DEPTNAME,MSTEMP.EMPNAME AS ISSUEFROM,MSTEMP1.EMPNAME AS ISSUETO," +
                                   "  STOCKISSUEDTL.RID AS DTLRID,MSTPURITEM.PURINAME AS INAME,STOCKISSUEDTL.IQTY,STOCKISSUEDTL.IRATE, " +
                                   "  CAST(CAST(STOCKISSUEDTL.IQTY * STOCKISSUEDTL.IRATE AS FLOAT) AS decimal(16,2)) AS IAMT, " +
                                   "  MSTPURITEM.PURIUNIT AS IUNIT " +
                             " from STOCKISSUE " +
                             " LEFT JOIN STOCKISSUEDTL ON (STOCKISSUEDTL.ISSUERID = STOCKISSUE.RID) " +
                             " LEFT JOIN MSTPURITEM ON (STOCKISSUEDTL.IRID = MSTPURITEM.RID) " +
                             " LEFT JOIN MSTDEPT ON (MSTDEPT.RID = STOCKISSUE.ISSUEDEPTRID) " +
                             " LEFT JOIN MSTEMP ON (MSTEMP.RID = STOCKISSUE.ISSUEFROMPERRID) " +
                             " LEFT JOIN MSTEMP AS MSTEMP1 ON (MSTEMP1.RID = STOCKISSUE.ISSUETOPERRID) " +
                             " WHERE  " +
                             " STOCKISSUE.ISSUEDATE between @p_fromdate and @p_todate and  " +
                             " isnull(STOCKISSUE.delflg,0)=0 and isnull(STOCKISSUEDTL.delflg,0)=0 " +
                             " order by  STOCKISSUE.ISSUEDATE" +
                             " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_PURISSUEITEMSUMMARY");
                strproc = " CREATE PROCEDURE SP_PURISSUEITEMSUMMARY " +
                            " (@p_fromdate datetime,@p_todate datetime)  " +
                            " as  " +
                            " begin    " +
                            " select MSTPURITEM.RID,MSTPURITEM.PURINAME AS INAME,SUM(ISNULL(STOCKISSUEDTL.IQTY,0)) AS TOTQTY,MSTPURITEM.PURIUNIT AS IUNIT, " +
                            " CAST(CAST(SUM(ISNULL(STOCKISSUEDTL.IRATE,0))/ COUNT(MSTPURITEM.RID) AS FLOAT) AS decimal(16,2)) AS AVGRATE " +
                            " from STOCKISSUE  " +
                            " LEFT JOIN STOCKISSUEDTL ON (STOCKISSUEDTL.ISSUERID = STOCKISSUE.RID)  " +
                            " LEFT JOIN MSTPURITEM ON (STOCKISSUEDTL.IRID = MSTPURITEM.RID) " +
                            " WHERE  STOCKISSUE.ISSUEDATE between @p_fromdate and @p_todate and   " +
                            " isnull(STOCKISSUE.delflg,0)=0 and isnull(STOCKISSUEDTL.delflg,0)=0  " +
                            " GROUP BY MSTPURITEM.RID, MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT " +
                            " ORDER BY MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT,MSTPURITEM.RID " +
                            " end  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_PURISSUEITEMDEPTSUMMARY");
                strproc = " CREATE PROCEDURE SP_PURISSUEITEMDEPTSUMMARY " +
                            " (@p_fromdate datetime,@p_todate datetime) " +
                             " as " +
                             " begin   " +
                             " select MSTPURITEM.RID,MSTPURITEM.PURINAME AS INAME, MSTPURITEM.PURIUNIT AS IUNIT," +
                                    " SUM(ISNULL(STOCKISSUEDTL.IQTY,0)) AS TOTQTY," +
                                    " SUM(ISNULL(STOCKISSUEDTL.IRATE,0)) as IRATE," +
                                    " CAST(CAST(SUM(ISNULL(STOCKISSUEDTL.IRATE,0))/ COUNT(MSTPURITEM.RID) AS FLOAT) AS decimal(16,2)) AS AVGRATE," +
                                    " COUNT(MSTPURITEM.RID) AS TOTCNT,MSTDEPT.DEPTNAME,MSTDEPT.RID AS DEPTRID " +
                             " from STOCKISSUE " +
                             " LEFT JOIN STOCKISSUEDTL ON (STOCKISSUEDTL.ISSUERID = STOCKISSUE.RID)  " +
                             " LEFT JOIN MSTPURITEM ON (MSTPURITEM.RID = STOCKISSUEDTL.IRID) " +
                             " LEFT JOIN MSTDEPT ON (MSTDEPT.RID = STOCKISSUE.ISSUEDEPTRID)      " +
                             " WHERE  STOCKISSUE.ISSUEDATE between @p_fromdate and @p_todate and  " +
                             " isnull(STOCKISSUE.delflg,0)=0 and isnull(STOCKISSUEDTL.delflg,0)=0 " +
                             " GROUP BY  MSTPURITEM.RID,MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT,MSTDEPT.DEPTNAME,MSTDEPT.RID" +
                             " HAVING COUNT(MSTPURITEM.RID)>0 " +
                             " ORDER BY MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT,MSTPURITEM.RID" +
                             " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_PURCHASEISSUESTOCKINFO");
                strproc = " CREATE PROCEDURE sp_PURCHASEISSUESTOCKINFO" +
                           " (@p_fromdate datetime,@p_todate datetime) " +
                           " AS BEGIN  " +
                           " SELECT MSTPURITEM.RID,MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT, " +
                            " OPPURDTL.TOTQTY AS OPPURQTY,OPPURDTL.TOTAMT AS OPPURAMT, " +
                            " OPISSUEDTL.TOTQTY AS OPISSQTY,OPISSUEDTL.TOTAMT AS OPISSAMT, " +
                            " ISNULL(OPPHYSTKDTL.TOTQTY,0) AS OPPHYQTY,ISNULL(OPPHYSTKDTL.TOTAMT,0) AS OPPHYAMT, " +
                            " ((ISNULL(OPPURDTL.TOTQTY,0) - ISNULL(OPISSUEDTL.TOTQTY,0)) + ISNULL(OPPHYSTKDTL.TOTQTY,0)) AS OPQTY, " +
                            " ((ISNULL(OPPURDTL.TOTAMT,0) - ISNULL(OPISSUEDTL.TOTAMT,0)) + ISNULL(OPPHYSTKDTL.TOTAMT,0)) AS OPAMT, " +
                            " ISNULL(PURDTL.TOTQTY,0) AS PURQTY,ISNULL(PURDTL.TOTAMT,0) AS PURAMT," +
                            " ISNULL(ISSUEDTL.TOTQTY,0) AS ISSQTY,ISNULL(ISSUEDTL.TOTAMT,0) AS ISSAMT," +
                            " ISNULL(PHYSTKDTL.TOTQTY,0) AS PHYSTKQTY, ISNULL(PHYSTKDTL.TOTAMT,0) AS PHYSTKAMT," +
                            " ((ISNULL(PURDTL.TOTQTY,0) - ISNULL(ISSUEDTL.TOTQTY,0)) + ISNULL(PHYSTKDTL.TOTQTY,0)) AS STOCKQTY," +
                            " ((ISNULL(PURDTL.TOTAMT,0) - ISNULL(ISSUEDTL.TOTAMT,0)) + ISNULL(PHYSTKDTL.TOTAMT,0)) AS STOCKAMT, " +
                            " ((ISNULL(OPPURDTL.TOTQTY,0) + ISNULL(PURDTL.TOTQTY,0)) - (ISNULL(ISSUEDTL.TOTQTY,0) + ISNULL(OPISSUEDTL.TOTQTY,0)) + (ISNULL(PHYSTKDTL.TOTQTY,0) + ISNULL(OPPHYSTKDTL.TOTQTY,0))) AS CLQTY, " +
                            " ((ISNULL(OPPURDTL.TOTAMT,0) + ISNULL(PURDTL.TOTAMT,0)) - (ISNULL(ISSUEDTL.TOTAMT,0) + ISNULL(OPISSUEDTL.TOTAMT,0)) + (ISNULL(PHYSTKDTL.TOTAMT,0) + ISNULL(OPPHYSTKDTL.TOTAMT,0))) AS CLAMT " +
                            " FROM MSTPURITEM " +
                        " LEFT JOIN (  " +
                                    " SELECT  ITEMPURCHASEDTL.IRID,SUM(ISNULL(ITEMPURCHASEDTL.IQTY,0)) AS TOTQTY, " +
                                        " SUM(ISNULL(ITEMPURCHASEDTL.IAMT,0)) AS TOTAMT " +
                                        " FROM ITEMPURCHASE " +
                                        " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID) " +
                                        " WHERE  ITEMPURCHASE.PURDATE  < @p_fromdate AND	 " +
                                        " isnull(ITEMPURCHASE.delflg,0)=0 and isnull(ITEMPURCHASEDTL.delflg,0)=0 " +
                                        " GROUP BY ITEMPURCHASEDTL.IRID " +
                                    " ) OPPURDTL ON (OPPURDTL.IRID = MSTPURITEM.RID) " +
                    " LEFT JOIN ( " +
                                    " SELECT STOCKISSUEDTL.IRID,(SUM(ISNULL(STOCKISSUEDTL.IQTY,0))) AS TOTQTY, " +
                                    " CAST(CAST(SUM(ISNULL(STOCKISSUEDTL.IQTY,0) * ISNULL(STOCKISSUEDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                    " FROM STOCKISSUE " +
                                    " LEFT JOIN STOCKISSUEDTL ON (STOCKISSUEDTL.ISSUERID=STOCKISSUE.RID) " +
                                    " WHERE  STOCKISSUE.ISSUEDATE < @p_fromdate AND	 " +
                                    " isnull(STOCKISSUE.delflg,0)=0 AND isnull(STOCKISSUEDTL.delflg,0)=0 " +
                                     " GROUP BY STOCKISSUEDTL.IRID " +
                                     " ) OPISSUEDTL ON (OPISSUEDTL.IRID = MSTPURITEM.RID) " +
                    " LEFT JOIN ( " +
                                    " SELECT PHYSTOCKDTL.IRID,(SUM(ISNULL(PHYSTOCKDTL.CLOSINGQTY,0))) AS TOTQTY," +
                                    " CAST(CAST(SUM(ISNULL(PHYSTOCKDTL.CLOSINGQTY,0) * ISNULL(PHYSTOCKDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT  " +
                                    " FROM PHYSTOCK  " +
                                    " LEFT JOIN PHYSTOCKDTL ON (PHYSTOCKDTL.PHYRID=PHYSTOCK.RID)  " +
                                    " WHERE  PHYSTOCK.PHYDATE < @p_fromdate AND " +
                                    " isnull(PHYSTOCK.delflg,0)=0 AND isnull(PHYSTOCKDTL.delflg,0)=0  " +
                                    "  GROUP BY PHYSTOCKDTL.IRID  " +
                                    " ) OPPHYSTKDTL ON (OPPHYSTKDTL.IRID = MSTPURITEM.RID)" +
                    " LEFT JOIN (  " +
                                    " SELECT  ITEMPURCHASEDTL.IRID,SUM(ISNULL(ITEMPURCHASEDTL.IQTY,0)) AS TOTQTY, " +
                                        " SUM(ISNULL(ITEMPURCHASEDTL.IAMT,0)) AS TOTAMT " +
                                        " FROM ITEMPURCHASE " +
                                        " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID) " +
                                        " WHERE ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate and  " +
                                        " isnull(ITEMPURCHASE.delflg,0)=0 and isnull(ITEMPURCHASEDTL.delflg,0)=0 " +
                                        " GROUP BY ITEMPURCHASEDTL.IRID " +
                                    " ) PURDTL ON (PURDTL.IRID = MSTPURITEM.RID) " +
                        " LEFT JOIN ( " +
                                    " SELECT  STOCKISSUEDTL.IRID,(SUM(ISNULL(STOCKISSUEDTL.IQTY,0))) AS TOTQTY, " +
                                    " CAST(CAST(SUM(ISNULL(STOCKISSUEDTL.IQTY,0) * ISNULL(STOCKISSUEDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                    " FROM STOCKISSUE " +
                                    " LEFT JOIN STOCKISSUEDTL ON (STOCKISSUEDTL.ISSUERID=STOCKISSUE.RID) " +
                                    " WHERE STOCKISSUE.ISSUEDATE between @p_fromdate and @p_todate and  " +
                                    " isnull(STOCKISSUE.delflg,0)=0 AND isnull(STOCKISSUEDTL.delflg,0)=0 " +
                                     " GROUP BY STOCKISSUEDTL.IRID " +
                                     " ) ISSUEDTL ON (ISSUEDTL.IRID = MSTPURITEM.RID) " +
                        " LEFT JOIN (   " +
                                   " SELECT PHYSTOCKDTL.IRID,SUM(ISNULL(PHYSTOCKDTL.CLOSINGQTY,0)) AS TOTQTY,  " +
                                   " CAST(CAST(SUM(ISNULL(PHYSTOCKDTL.CLOSINGQTY,0) * ISNULL(PHYSTOCKDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT  " +
                                    " FROM PHYSTOCK  " +
                                    " LEFT JOIN PHYSTOCKDTL ON (PHYSTOCKDTL.PHYRID=PHYSTOCK.RID) " +
                                    " WHERE PHYSTOCK.PHYDATE between @p_fromdate and @p_todate AND " +
                                    " isnull(PHYSTOCK.delflg,0)=0 and isnull(PHYSTOCKDTL.delflg,0)=0  " +
                                    " GROUP BY PHYSTOCKDTL.IRID " +
                                   " ) PHYSTKDTL ON (PHYSTKDTL.IRID = MSTPURITEM.RID)" +
                            " WHERE ISNULL(MSTPURITEM.DELFLG,0)=0 " +
                            " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BILLREGWITHCOMPLYMENT");
                strproc = " CREATE procedure sp_BILLREGWITHCOMPLYMENT " +
                            " (@p_fromdate datetime,@p_todate datetime) as " +
                            " begin " +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE,BILL.BILLTIME,BILL.BILLTYPE,BILL.BILLPAX," +
                            " BILL.TOTAMOUNT,BILL.TOTSERTAXAMOUNT,BILL.TOTVATAMOUNT,BILL.TOTADDVATAMOUNT,BILL.TOTGSTPER,BILL.TOTGSTAMT," +
                            " BILL.TOTDISCAMOUNT,BILL.TOTROFF,BILL.NETAMOUNT," +
                            " MSTCUST.CUSTNAME,MSTTABLE.TABLENAME" +
                            " FROM BILL " +
                            " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID)" +
                            " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID)" +
                            " WHERE bill.billDATE between @p_fromdate and @p_todate AND isnull(BILL.Delflg,0)=0 " +
                            " And isnull(BILL.ISREVISEDBILL,0)=0  " +
                            " And isnull(BILL.ISCOMPLYBILL,0)=1" +
                            " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BILLREGBILLTYPE");
                strproc = " CREATE PROCEDURE SP_BILLREGBILLTYPE (@p_fromdate datetime,@p_todate datetime)  " +
                                " AS BEGIN " +
                                " SELECT BILL.RID,BILL.BILLDATE,BILL.BILLNO,BILL.REFBILLNO,(CASE WHEN BILL.BILLTYPE='CASH' THEN 'Thal' ELSE 'Rest' END ) AS BILLTYPE," +
                                " ISNULL(MSTTABLE.TABLENAME,'-') AS TABLENAME,ISNULL(BILL.TOTAMOUNT,0) AS TOTAMOUNT,ISNULL(BILL.TOTDISCAMOUNT,0) AS TOTDISCAMOUNT, " +
                                " ((isnull(BILL.TOTAMOUNT,0) - isnull(BILL.TOTDISCAMOUNT,0)) + ISNULL(BILL.TOTSERCHRAMT,0)) AS BILLNETAMT," +
                                " ISNULL(BILL.TOTSERTAXAMOUNT,0) AS TOTSERTAXAMOUNT,ISNULL(BILL.TOTVATAMOUNT,0) AS TOTVATAMOUNT, ISNULL(BILL.TOTLIQVATAMT,0) AS TOTLIQVATAMT, ISNULL(BILL.TOTBEVVATAMT,0) AS TOTBEVVATAMT, " +
                                " ISNULL(BILL.TOTADDVATAMOUNT,0) AS TOTADDVATAMOUNT, ISNULL(BILL.TOTROFF,0) AS TOTROFF," +
                                " ((isnull(BILL.TOTAMOUNT,0) - isnull(BILL.TOTDISCAMOUNT,0)) + ISNULL(BILL.TOTSERCHRAMT,0) + ISNULL(BILL.TOTSERTAXAMOUNT,0) + ISNULL(BILL.TOTVATAMOUNT,0) + ISNULL(BILL.TOTADDVATAMOUNT,0) + ISNULL(BILL.TOTKKCESSAMT,0) + ISNULL(BILL.TOTROFF,0) - ISNULL(BILL.TOTADDDISCAMT,0) ) AS BILLAMT," +
                                " ISNULL(BILL.TOTADDDISCAMT,0) AS TOTADDDISCAMT, ISNULL(BILL.TOTSERCHRAMT,0) AS TOTSERCHRAMT,ISNULL(BILL.TOTKKCESSAMT,0) AS TOTKKCESSAMT, " +
                                " ISNULL(BILL.TOTGSTAMT,0) AS TOTGSTAMT " +
                                " FROM BILL" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID = BILL.TABLERID)" +
                                " WHERE BILL.BILLDATE between @p_fromdate and @p_todate and isnull(BILL.delflg,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0" +
                                " ORDER BY BILL.BILLDATE " +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_OPENCASHDRAWER");
                strproc = " CREATE PROCEDURE SP_OPENCASHDRAWER ( " +
                                    " @p_mode as int," +
                                    " @p_rid as bigint," +
                                    " @p_reason nvarchar(500)," +
                                    " @p_reasondate datetime," +
                                    " @p_userid bigint, " +
                                    " @p_errstr as nvarchar(max) out, " +
                                    " @p_retval as int out," +
                                    " @p_id as bigint out " +
                                    " ) as" +
                                    " begin" +
                                    " try" +
                                    " begin" +
                                    " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                    " if (@p_mode=0)" +
                                    " begin" +
                                    " insert into OPENCASHDRAWER (REASON,reasondate,auserid,adatetime,DelFlg)" +
                                    " VALUES ( @p_reason,@p_reasondate,@p_userid,getdate(),0)" +
                                    " SET @p_id=SCOPE_IDENTITY()" +
                                    " End  " +
                                     " else if (@p_mode=1) " +
                                       "  Begin " +
                                         "   Set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                           "  Update OPENCASHDRAWER " +
                                            " Set " +
                                                 " REASON=@p_reason,reasondate=@p_reasondate,euserid = @p_userid,edatetime = getdate()  " +
                                                 " where rid = @p_rid  " +
                                         " End " +
                                      " End " +
                                    " end	" +
                                    " try  " +
                                        "  begin catch " +
                                          " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; " +
                                          " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                          " Return  " +
                                          " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_OPENCASHDRAWERREG");
                strproc = " CREATE PROCEDURE SP_OPENCASHDRAWERREG" +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN " +
                                " SELECT OPENCASHDRAWER.RID,OPENCASHDRAWER.REASONDATE,OPENCASHDRAWER.REASON,OPENCASHDRAWER.ADATETIME,MSTUSERS.USERNAME" +
                                " FROM OPENCASHDRAWER" +
                                " LEFT JOIN MSTUSERS ON (MSTUSERS.RID=OPENCASHDRAWER.AUSERID)" +
                                " WHERE OPENCASHDRAWER.REASONDATE between @p_fromdate and @p_todate" +
                                           "  AND ISNULL(OPENCASHDRAWER.DELFLG,0)=0" +
                                " ORDER BY OPENCASHDRAWER.REASONDATE,OPENCASHDRAWER.RID" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_ITEMWISEPURCHASE");
                strproc = " CREATE PROCEDURE SP_ITEMWISEPURCHASE " +
                            " ( " +
                            " @p_mode as int, " +
                            " @p_rid bigint, " +
                            " @p_itemrid bigint, " +
                            " @p_puritemnm1 nvarchar(200), " +
                            " @p_purgrm1 decimal(18,3), " +
                            " @p_purunit1 nvarchar(50), " +
                            " @p_purrate1 decimal(18,3)," +
                            " @p_puritemnm2 nvarchar(200)," +
                            " @p_purgrm2 decimal(18,3), " +
                            " @p_purunit2 nvarchar(50), " +
                            " @p_purrate2 decimal(18,3)," +
                            " @p_puritemnm3 nvarchar(200), " +
                            " @p_purgrm3 decimal(18,3), " +
                            " @p_purunit3 nvarchar(50)," +
                            " @p_purrate3 decimal(18,3)," +
                            " @p_puritemnm4 nvarchar(200), " +
                            " @p_purgrm4 decimal(18,3), " +
                            " @p_purunit4 nvarchar(50), " +
                            " @p_purrate4 decimal(18,3)," +
                            " @p_puritemnm5 nvarchar(200), " +
                            " @p_purgrm5 decimal(18,3), " +
                            " @p_purunit5 nvarchar(50), " +
                            " @p_purrate5 decimal(18,3)," +
                            " @p_puritemnm6 nvarchar(200), " +
                            " @p_purgrm6 decimal(18,3), " +
                            " @p_purunit6 nvarchar(50), " +
                            " @p_purrate6 decimal(18,3)," +
                            " @p_puritemnm7 nvarchar(200), " +
                            " @p_purgrm7 decimal(18,3), " +
                            " @p_purunit7 nvarchar(50), " +
                            " @p_purrate7 decimal(18,3)," +
                            " @p_puritemnm8 nvarchar(200), " +
                            " @p_purgrm8 decimal(18,3), " +
                            " @p_purunit8 nvarchar(50), " +
                            " @p_purrate8 decimal(18,3)," +
                            " @p_puritemnm9 nvarchar(200), " +
                            " @p_purgrm9 decimal(18,3), " +
                            " @p_purunit9 nvarchar(50), " +
                            " @p_purrate9 decimal(18,3)," +
                            " @p_puritemnm10 nvarchar(200), " +
                            " @p_purgrm10 decimal(18,3), " +
                            " @p_purunit10 nvarchar(50), " +
                            " @p_purrate10 decimal(18,3), " +
                            " @p_puritemnm1rid bigint, " +
                            " @p_puritemnm2rid bigint, " +
                            " @p_puritemnm3rid bigint, " +
                            " @p_puritemnm4rid bigint, " +
                            " @p_puritemnm5rid bigint, " +
                            " @p_puritemnm6rid bigint, " +
                            " @p_puritemnm7rid bigint, " +
                            " @p_puritemnm8rid bigint, " +
                            " @p_puritemnm9rid bigint, " +
                            " @p_puritemnm10rid bigint, " +
                            " @p_puritemnm11 nvarchar(200), " +
                            " @p_puritemnm11rid bigint, " +
                            " @p_purgrm11 decimal(18,3), " +
                            " @p_purunit11 nvarchar(50), " +
                            " @p_purrate11 decimal(18,3), " +
                            " @p_puritemnm12 nvarchar(200), " +
                            " @p_puritemnm12rid bigint, " +
                            " @p_purgrm12 decimal(18,3), " +
                            " @p_purunit12 nvarchar(50), " +
                            " @p_purrate12 decimal(18,3), " +
                            " @p_puritemnm13 nvarchar(200), " +
                            " @p_puritemnm13rid bigint, " +
                            " @p_purgrm13 decimal(18,3), " +
                            " @p_purunit13 nvarchar(50), " +
                            " @p_purrate13 decimal(18,3), " +
                            " @p_puritemnm14 nvarchar(200), " +
                            " @p_puritemnm14rid bigint, " +
                            " @p_purgrm14 decimal(18,3), " +
                            " @p_purunit14 nvarchar(50), " +
                            " @p_purrate14 decimal(18,3), " +
                            " @p_puritemnm15 nvarchar(200), " +
                            " @p_puritemnm15rid bigint, " +
                            " @p_purgrm15 decimal(18,3), " +
                            " @p_purunit15 nvarchar(50), " +
                            " @p_purrate15 decimal(18,3), " +
                            " @p_userid bigint,  " +
                            " @p_errstr as nvarchar(max) out,  " +
                            " @p_retval as int out, " +
                            " @p_id as bigint out " +
                            " ) as  " +
                            " begin  " +
                            " try  " +
                            " begin " +
                               " set @p_Errstr='' Set @p_Retval=0 set @p_id=0 " +
                               " if (@p_mode=0)  " +
                                   " begin 	" +
                                       " Insert Into ITEMWISEPURCHASE ( ITEMRID,PURITEMNM1,PURGRM1,PURUNIT1,PURRATE1," +
                                                                  " PURITEMNM2,PURGRM2,PURUNIT2,PURRATE2,PURITEMNM3,PURGRM3,PURUNIT3,PURRATE3," +
                                                                  " PURITEMNM4,PURGRM4,PURUNIT4,PURRATE4,PURITEMNM5,PURGRM5,PURUNIT5,PURRATE5," +
                                                                  " PURITEMNM6,PURGRM6,PURUNIT6,PURRATE6,PURITEMNM7,PURGRM7,PURUNIT7,PURRATE7," +
                                                                  " PURITEMNM8,PURGRM8,PURUNIT8,PURRATE8,PURITEMNM9,PURGRM9,PURUNIT9,PURRATE9," +
                                                                  " PURITEMNM10,PURGRM10,PURUNIT10,PURRATE10,   " +
                                                                  " PURITEMNM1RID,PURITEMNM2RID,PURITEMNM3RID,PURITEMNM4RID,PURITEMNM5RID," +
                                                                  " PURITEMNM6RID,PURITEMNM7RID,PURITEMNM8RID,PURITEMNM9RID,PURITEMNM10RID," +
                                                                  " PURITEMNM11RID,PURITEMNM11,PURGRM11,PURUNIT11,PURRATE11," +
                                                                  " PURITEMNM12RID,PURITEMNM12,PURGRM12,PURUNIT12,PURRATE12," +
                                                                  " PURITEMNM13RID,PURITEMNM13,PURGRM13,PURUNIT13,PURRATE13," +
                                                                  " PURITEMNM14RID,PURITEMNM14,PURGRM14,PURUNIT14,PURRATE14," +
                                                                  " PURITEMNM15RID,PURITEMNM15,PURGRM15,PURUNIT15,PURRATE15," +
                                                                  " auserid,adatetime,DelFlg)  " +
                                                 " Values (@p_ITEMRID,@p_PURITEMNM1,@p_PURGRM1,@p_PURUNIT1,@p_PURRATE1, " +
                                                                  " @p_PURITEMNM2,@p_PURGRM2,@p_PURUNIT2,@p_PURRATE2,@p_PURITEMNM3,@p_PURGRM3,@p_PURUNIT3,@p_PURRATE3," +
                                                                  " @p_PURITEMNM4,@p_PURGRM4,@p_PURUNIT4,@p_PURRATE4,@p_PURITEMNM5,@p_PURGRM5,@p_PURUNIT5,@p_PURRATE5," +
                                                                  " @p_PURITEMNM6,@p_PURGRM6,@p_PURUNIT6,@p_PURRATE6,@p_PURITEMNM7,@p_PURGRM7,@p_PURUNIT7,@p_PURRATE7," +
                                                                  " @p_PURITEMNM8,@p_PURGRM8,@p_PURUNIT8,@p_PURRATE8,@p_PURITEMNM9,@p_PURGRM9,@p_PURUNIT9,@p_PURRATE9," +
                                                                  " @p_PURITEMNM10,@p_PURGRM10,@p_PURUNIT10,@p_PURRATE10," +
                                                                  " @p_PURITEMNM1RID,@p_PURITEMNM2RID,@p_PURITEMNM3RID,@p_PURITEMNM4RID,@p_PURITEMNM5RID," +
                                                                  " @p_PURITEMNM6RID,@p_PURITEMNM7RID,@p_PURITEMNM8RID,@p_PURITEMNM9RID,@p_PURITEMNM10RID," +
                                                                  " @p_PURITEMNM11RID,@p_PURITEMNM11,@p_PURGRM11,@p_PURUNIT11,@p_PURRATE11, " +
                                                                  " @p_PURITEMNM12RID,@p_PURITEMNM12,@p_PURGRM12,@p_PURUNIT12,@p_PURRATE12, " +
                                                                  " @p_PURITEMNM13RID,@p_PURITEMNM13,@p_PURGRM13,@p_PURUNIT13,@p_PURRATE13, " +
                                                                  " @p_PURITEMNM14RID,@p_PURITEMNM14,@p_PURGRM14,@p_PURUNIT14,@p_PURRATE14, " +
                                                                  " @p_PURITEMNM15RID,@p_PURITEMNM15,@p_PURGRM15,@p_PURUNIT15,@p_PURRATE15, " +
                                                                  " @p_userid,getdate(),0 " +
                                                         " ) " +
                                       " set @p_id=SCOPE_IDENTITY() " +
                                       " End     " +
                                " else if (@p_mode=1)   " +
                                   " begin " +
                                      " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                        " Update ITEMWISEPURCHASE   " +
                                             " set ITEMRID=@p_ITEMRID, " +
                                                                 " PURITEMNM1=@p_PURITEMNM1,PURGRM1=@p_PURGRM1,PURUNIT1=@p_PURUNIT1,PURRATE1=@p_PURRATE1, " +
                                                                "  PURITEMNM2=@p_PURITEMNM2,PURGRM2=@p_PURGRM2,PURUNIT2=@p_PURUNIT2,PURRATE2=@p_PURRATE2, " +
                                                                   "  PURITEMNM3=@p_PURITEMNM3,PURGRM3=@p_PURGRM3,PURUNIT3=@p_PURUNIT3,PURRATE3=@p_PURRATE3, " +
                                                                    " PURITEMNM4=@p_PURITEMNM4,PURGRM4=@p_PURGRM4,PURUNIT4=@p_PURUNIT4,PURRATE4=@p_PURRATE4, " +
                                                                    " PURITEMNM5=@p_PURITEMNM5,PURGRM5=@p_PURGRM5,PURUNIT5=@p_PURUNIT5,PURRATE5=@p_PURRATE5, " +
                                                                    " PURITEMNM6=@p_PURITEMNM6,PURGRM6=@p_PURGRM6,PURUNIT6=@p_PURUNIT6,PURRATE6=@p_PURRATE6, " +
                                                                    " PURITEMNM7=@p_PURITEMNM7,PURGRM7=@p_PURGRM7,PURUNIT7=@p_PURUNIT7,PURRATE7=@p_PURRATE7," +
                                                                    " PURITEMNM8=@p_PURITEMNM8,PURGRM8=@p_PURGRM8,PURUNIT8=@p_PURUNIT8,PURRATE8=@p_PURRATE8," +
                                                                    " PURITEMNM9=@p_PURITEMNM9,PURGRM9=@p_PURGRM9,PURUNIT9=@p_PURUNIT9,PURRATE9=@p_PURRATE9," +
                                                                    " PURITEMNM10=@p_PURITEMNM10,PURGRM10=@p_PURGRM10,PURUNIT10=@p_PURUNIT10,PURRATE10=@p_PURRATE10," +
                                                                    " PURITEMNM1RID=@p_PURITEMNM1RID,PURITEMNM2RID=@p_PURITEMNM2RID,PURITEMNM3RID=@p_PURITEMNM3RID,PURITEMNM4RID=@p_PURITEMNM4RID,PURITEMNM5RID=@p_PURITEMNM5RID," +
                                                                    " PURITEMNM6RID=@p_PURITEMNM6RID,PURITEMNM7RID=@p_PURITEMNM7RID,PURITEMNM8RID=@p_PURITEMNM8RID,PURITEMNM9RID=@p_PURITEMNM9RID,PURITEMNM10RID=@p_PURITEMNM10RID," +
                                                                    " PURITEMNM11RID=@p_PURITEMNM11RID,PURITEMNM11=@p_PURITEMNM11,PURGRM11=@p_PURGRM11,PURUNIT11=@p_PURUNIT11,PURRATE11=@p_PURRATE11, " +
                                                                    " PURITEMNM12RID=@p_PURITEMNM12RID,PURITEMNM12=@p_PURITEMNM12,PURGRM12=@p_PURGRM12,PURUNIT12=@p_PURUNIT12,PURRATE12=@p_PURRATE12, " +
                                                                    " PURITEMNM13RID=@p_PURITEMNM13RID,PURITEMNM13=@p_PURITEMNM13,PURGRM13=@p_PURGRM13,PURUNIT13=@p_PURUNIT13,PURRATE13=@p_PURRATE13, " +
                                                                    " PURITEMNM14RID=@p_PURITEMNM14RID,PURITEMNM14=@p_PURITEMNM14,PURGRM14=@p_PURGRM14,PURUNIT14=@p_PURUNIT14,PURRATE14=@p_PURRATE14, " +
                                                                    " PURITEMNM15RID=@p_PURITEMNM15RID,PURITEMNM15=@p_PURITEMNM15,PURGRM15=@p_PURGRM15,PURUNIT15=@p_PURUNIT15,PURRATE15=@p_PURRATE15, " +
                                                                    " euserid = @p_userid,edatetime = getdate() " +
                                                                    " where rid = @p_rid   " +
                                                                " End  " +
                                                             " End " +
                                                           " end	" +
                                                           " try   " +
                                                               " begin catch  " +
                                                                 " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                                                 " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                                                 " Return   " +
                                                                 " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_ITEMPURCHASEUSAGESUMMARY");
                strproc = "  CREATE PROCEDURE SP_ITEMPURCHASEUSAGESUMMARY " +
                                 " (@p_fromdate datetime,@p_todate datetime ) AS  " +
                                 " BEGIN " +
                                    " SELECT ITEMWISEPURCHASEBASEINFO.PURITEMNM1,ITEMWISEPURCHASEBASEINFO.PURUNIT1, " +
                                     " SUM(ITEMWISEPURCHASEBASEINFO.IQTY) AS QTY,SUM(ITEMWISEPURCHASEBASEINFO.PURGRM1) AS GRM," +
                                     " SUM(ITEMWISEPURCHASEBASEINFO.OPGRAM) AS RESULTGRAM " +
                                     " FROM ITEMWISEPURCHASEBASEINFO " +
                                     " WHERE ITEMWISEPURCHASEBASEINFO.BILLDATE between @p_fromdate and @p_todate " +
                                     " GROUP BY ITEMWISEPURCHASEBASEINFO.PURITEMNM1,ITEMWISEPURCHASEBASEINFO.PURUNIT1" +
                                     " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_ITEMWISEPURCHASEITEMUSED");
                strproc = " CREATE PROCEDURE SP_ITEMWISEPURCHASEITEMUSED  " +
                               " (@p_fromdate datetime,@p_todate datetime) AS  " +
                            " BEGIN " +
                               "  SELECT ITEMWISEPURCHASEBASEINFO.IRID,ITEMWISEPURCHASEBASEINFO.INAME,SUM(ITEMWISEPURCHASEBASEINFO.IQTY) AS IQTY, " +
                                " ITEMWISEPURCHASEBASEINFO.PURITEMNM1,SUM(ITEMWISEPURCHASEBASEINFO.PURGRM1) AS PURGRM1, " +
                                " ITEMWISEPURCHASEBASEINFO.PURUNIT1,SUM(ITEMWISEPURCHASEBASEINFO.OPGRAM) AS OPGRAM " +
                                " FROM ITEMWISEPURCHASEBASEINFO " +
                                " WHERE ITEMWISEPURCHASEBASEINFO.BILLDATE between @p_fromdate and @p_todate " +
                                " GROUP BY ITEMWISEPURCHASEBASEINFO.IRID, " +
                                " ITEMWISEPURCHASEBASEINFO.INAME,ITEMWISEPURCHASEBASEINFO.PURITEMNM1,ITEMWISEPURCHASEBASEINFO.PURUNIT1 " +
                            " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_banqbillinfo");
                strproc = " CREATE PROCEDURE sp_banqbillinfo " +
                            " ( " +
                            " @p_mode as int, " +
                            " @p_rid bigint, " +
                            " @p_banqbillno nvarchar(50), " +
                            " @p_banqrefno nvarchar(50), " +
                            " @p_banqbilldate datetime, " +
                            " @p_banqbilltime datetime, " +
                            " @p_banqrid bigint, " +
                            " @p_banqborid bigint, " +
                            " @p_banqbodate datetime, " +
                            " @p_banqbotime datetime, " +
                            " @p_banqfuncname nvarchar(2000), " +
                            " @p_banqnoofpax bigint, " +
                            " @p_custrid bigint, " +
                            " @p_custnm nvarchar(250), " +
                            " @p_prepby nvarchar(200), " +
                            " @p_totamount decimal(18,2), " +
                            " @p_totdiscper decimal(18,2), " +
                            " @p_totdiscamount decimal(18,2), " +
                            " @p_totsertaxper decimal(18,2), " +
                            " @p_totsertaxamount decimal(18,2), " +
                            " @p_totvatper decimal(18,2), " +
                            " @p_totvatamount decimal(18,2), " +
                            " @p_totroff decimal(18,2), " +
                            " @p_netamount decimal(18,2), " +
                            " @p_setletype nvarchar(50), " +
                            " @p_chequeno nvarchar(100), " +
                            " @p_chequebankname nvarchar(100), " +
                            " @p_creditcardno nvarchar(100), " +
                            " @p_creditholdername nvarchar(100), " +
                            " @p_creditbankname nvarchar(100), " +
                            " @p_billremark nvarchar(max), " +
                            " @p_totdepoamt decimal(18,2), " +
                            " @p_totsbcesstaxper decimal (18,3)," +
                            " @p_totsbcesstaxamt decimal (18,3)," +
                            " @p_totkkcesstaxper decimal (18,3)," +
                            " @p_totkkcesstaxamt decimal (18,3)," +
                            " @p_perpaxrate decimal (18,3)," +
                            " @p_cgstamt decimal (18,3)," +
                            " @p_sgstamt decimal (18,3)," +
                            " @p_igstamt decimal (18,3)," +
                            " @p_totgstamt decimal (18,3)," +
                            " @p_userid bigint,   " +
                            " @p_errstr as nvarchar(max) out,  " +
                            " @p_retval as int out,  " +
                            " @p_id as bigint out  " +
                            " ) as   " +
                            " begin   " +
                            " try   " +
                            " begin  " +
                             " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                "  if (@p_mode=0)   " +
                                    " begin  " +
                                        "   Insert Into BANQBILLINFO ( BANQBILLNO,BANQREFNO,BANQBILLDATE,BANQBILLTIME,BANQRID,BANQBORID,BANQBODATE,BANQBOTIME, " +
                                                                    "  BANQFUNCNAME,BANQNOOFPAX,CUSTRID,CUSTNM,PREPBY,TOTAMOUNT,TOTDISCPER,TOTDISCAMOUNT, " +
                                                                    " TOTSERTAXPER,TOTSERTAXAMOUNT,TOTVATPER,TOTVATAMOUNT,TOTROFF,NETAMOUNT,SETLETYPE, " +
                                                                    " CHEQUENO,CHEQUEBANKNAME,CREDITCARDNO,CREDITHOLDERNAME,CREDITBANKNAME,billremark,totdepoamt, " +
                                                                    " totsbcesstaxper,totsbcesstaxamt,totkkcesstaxper,totkkcesstaxamt,perpaxrate," +
                                                                    " cgstamt,sgstamt,igstamt,totgstamt," +
                                                                    " auserid,adatetime,DelFlg)    " +
                                                    " Values (@p_BANQBILLNO,@p_BANQREFNO,@p_BANQBILLDATE,@p_BANQBILLTIME,@p_BANQRID,@p_BANQBORID,@p_BANQBODATE,@p_BANQBOTIME, " +
                                                        "  @p_BANQFUNCNAME,@p_BANQNOOFPAX,@p_CUSTRID,@p_CUSTNM,@p_PREPBY,@p_TOTAMOUNT,@p_TOTDISCPER,@p_TOTDISCAMOUNT, " +
                                                        "  @p_TOTSERTAXPER,@p_TOTSERTAXAMOUNT,@p_TOTVATPER,@p_TOTVATAMOUNT,@p_TOTROFF,@p_NETAMOUNT,@p_SETLETYPE, " +
                                                        "  @p_CHEQUENO,@p_CHEQUEBANKNAME,@p_CREDITCARDNO,@p_CREDITHOLDERNAME,@p_CREDITBANKNAME,@p_billremark,@p_totdepoamt, " +
                                                        "  @p_totsbcesstaxper,@p_totsbcesstaxamt,@p_totkkcesstaxper,@p_totkkcesstaxamt,@p_perpaxrate," +
                                                        "  @p_cgstamt,@p_sgstamt,@p_igstamt,@p_totgstamt," +
                                                        "  @p_userid,getdate(),0   " +
                                                            "  )   " +
                                        " set @p_id=SCOPE_IDENTITY()   " +
                                        " End       " +
                                    " else if (@p_mode=1)    " +
                                    "  begin   " +
                                        "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                        "  Update BANQBILLINFO  " +
                                                "  set BANQBILLNO=@p_BANQBILLNO,BANQREFNO=@p_BANQREFNO,BANQBILLDATE=@p_BANQBILLDATE,BANQBILLTIME=@p_BANQBILLTIME,BANQRID=@p_BANQRID,BANQBORID=@p_BANQBORID, " +
                                                        " 	BANQBODATE=@p_BANQBODATE,BANQBOTIME=@p_BANQBOTIME, " +
                                                            " BANQFUNCNAME=@p_BANQFUNCNAME,BANQNOOFPAX=@p_BANQNOOFPAX,CUSTRID=@p_CUSTRID,CUSTNM=@p_CUSTNM,PREPBY=@p_PREPBY, " +
                                                        " TOTAMOUNT=@p_TOTAMOUNT,TOTDISCPER=@p_TOTDISCPER,TOTDISCAMOUNT=@p_TOTDISCAMOUNT, " +
                                                            " TOTSERTAXPER=@p_TOTSERTAXPER,TOTSERTAXAMOUNT=@p_TOTSERTAXAMOUNT,TOTVATPER=@p_TOTVATPER, " +
                                                            " TOTVATAMOUNT=@p_TOTVATAMOUNT,TOTROFF=@p_TOTROFF,NETAMOUNT=@p_NETAMOUNT,SETLETYPE=@p_SETLETYPE, " +
                                                            " CHEQUENO=@p_CHEQUENO,CHEQUEBANKNAME=@p_CHEQUEBANKNAME,CREDITCARDNO=@p_CREDITCARDNO,CREDITHOLDERNAME=@p_CREDITHOLDERNAME, " +
                                                            " CREDITBANKNAME=@p_CREDITBANKNAME,billremark=@p_billremark,totdepoamt=@p_totdepoamt, " +
                                                            " totsbcesstaxper=@p_totsbcesstaxper,totsbcesstaxamt=@p_totsbcesstaxamt,totkkcesstaxper=@p_totkkcesstaxper,totkkcesstaxamt=@p_totkkcesstaxamt," +
                                                            " perpaxrate = @p_perpaxrate, cgstamt=@p_cgstamt,sgstamt=@p_sgstamt,igstamt=@p_igstamt,totgstamt=@p_totgstamt," +
                                                            " euserid = @p_userid,edatetime = getdate()   " +
                                            "  where rid = @p_rid    " +
                                    "  End    " +
                                    " End   " +
                                " end	  " +
                                " try    " +
                                    " begin catch  " +
                                        " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0   " +
                                        " Return     " +
                                        " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_banqbillinfodetail");
                strproc = " CREATE PROCEDURE sp_banqbillinfodetail" +
                                " (" +
                                " @p_mode as int," +
                                " @p_rid bigint," +
                                " @p_banqbillrid bigint," +
                                " @p_banqirid bigint," +
                                " @p_idesc nvarchar(max)," +
                                " @p_banqqty decimal(18,3)," +
                                " @p_banqrate decimal(18,3)," +
                                " @p_banqamount decimal(18,3)," +
                                " @p_idiscper decimal(18,3)," +
                                " @p_idiscamt decimal(18,3)," +
                                " @p_icgstper decimal(18,3)," +
                                " @p_icgstamt decimal(18,3)," +
                                " @p_isgstper decimal(18,3)," +
                                " @p_isgstamt decimal(18,3)," +
                                " @p_iigstper decimal(18,3)," +
                                " @p_iigstamt decimal(18,3)," +
                                " @p_userid bigint,  " +
                                " @p_errstr as nvarchar(max) out,  " +
                                " @p_retval as int out, " +
                                " @p_id as bigint out " +
                                " ) as  " +
                                " begin  " +
                                " try  " +
                                " begin " +
                                 " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                " if (@p_mode=0)   " +
                                    "  begin " +
                                        "  Insert Into BANQBILLINFODETAIL ( BANQBILLRID,BANQIRID,IDESC,banqqty,banqrate,banqamount, " +
                                                                        " idiscper,idiscamt,icgstper,icgstamt,isgstper,isgstamt,iigstper,iigstamt, " +
                                                                            " auserid,adatetime,DelFlg)  " +
                                                    "  Values ( @p_BANQBILLRID,@p_BANQIRID,@p_IDESC,@p_banqqty,@p_banqrate,@p_banqamount," +
                                                    " @p_idiscper,@p_idiscamt,@p_icgstper,@p_icgstamt,@p_isgstper,@p_isgstamt,@p_iigstper,@p_iigstamt, " +
                                                    " @p_userid,getdate(),0)  " +
                                        " set @p_id=SCOPE_IDENTITY()  " +
                                        " End      " +
                                    " else if (@p_mode=1)   " +
                                    "  begin  " +
                                        "   set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                        "   Update BANQBILLINFODETAIL    " +
                                             " set BANQBILLRID=@p_BANQBILLRID,BANQIRID=@p_BANQIRID,IDESC=@p_IDESC,banqqty=@p_banqqty,banqrate=@p_banqrate,banqamount=@p_banqamount, " +
                                             " idiscper=@p_idiscper,idiscamt=@p_idiscamt,icgstper=@p_icgstper,icgstamt=@p_icgstamt,isgstper=@p_isgstper,isgstamt=@p_isgstamt,iigstper=@p_iigstper,iigstamt=@p_iigstamt, " +
                                             " euserid = @p_userid,edatetime = getdate()  " +
                                            "  where rid = @p_rid and BANQBILLRID=@p_BANQBILLRID" +
                                        " End " +
                                    " End  " +
                                " end	 " +
                                " try  " +
                                    " begin catch " +
                                        " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                        "  Return    " +
                                        " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BANQBILLINGINFOREG");
                strproc = " CREATE PROCEDURE SP_BANQBILLINGINFOREG " +
                                " (@p_fromdate datetime,@p_todate datetime)  " +
                                " AS BEGIN " +
                                " SELECT BANQBILLINFO.RID,BANQBILLINFO.BANQBILLNO,BANQBILLINFO.BANQREFNO,BANQBILLINFO.BANQBILLDATE,BANQBILLINFO.BANQBILLTIME, " +
                                " MSTBANQ.BANQNAME,BANQBILLINFO.BANQFUNCNAME,BANQBILLINFO.BANQNOOFPAX,BANQBILLINFO.CUSTNM, " +
                                " BANQBILLINFO.TOTAMOUNT,BANQBILLINFO.TOTDISCPER,BANQBILLINFO.TOTDISCAMOUNT,BANQBILLINFO.TOTSERTAXPER,BANQBILLINFO.TOTSBCESSTAXAMT, " +
                                " BANQBILLINFO.TOTSERTAXAMOUNT,BANQBILLINFO.TOTVATPER,BANQBILLINFO.TOTVATAMOUNT,BANQBILLINFO.TOTROFF,BANQBILLINFO.TOTDEPOAMT,BANQBILLINFO.NETAMOUNT, " +
                                " BANQBILLINFO.SETLETYPE,BANQBILLINFO.TOTKKCESSTAXAMT,BANQBILLINFO.TOTGSTAMT,BANQBILLINFO.CGSTAMT,BANQBILLINFO.SGSTAMT,BANQBILLINFO.IGSTAMT " +
                                " FROM BANQBILLINFO " +
                                " INNER JOIN MSTBANQ ON (MSTBANQ.RID=BANQBILLINFO.BANQRID) " +
                                " WHERE ISNULL(BANQBILLINFO.DELFLG,0)=0 AND BANQBILLINFO.BANQBILLDATE between @p_fromdate and @p_todate " +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_DAYSUMMARYPAXWISE");
                strproc = "CREATE PROCEDURE SP_DAYSUMMARYPAXWISE  " +
                            " (@p_fromdate datetime,@p_todate datetime)  " +
                            " AS BEGIN  " +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE," +
                                " ISNULL(MSTTABLE.RID,0) AS TABLERID,ISNULL(MSTTABLE.TABLENAME,'') AS TABLENO," +
                                " ISNULL(BILL.ISPARCELBILL,0) AS ISPARCLE," +
                                " (CASE WHEN ISNULL(BILL.ISPARCELBILL,0) = 1 THEN 'PARCLE' ELSE 'REST' END) AS BILLTYPE," +
                                " BILL.BILLPAX," +
                                " BILL.TOTAMOUNT,BILL.TOTDISCAMOUNT,(ISNULL(BILL.TOTAMOUNT,0)-ISNULL(BILL.TOTDISCAMOUNT,0)) AS TAXABLEAMOUNT," +
                                " BILL.TOTSERTAXAMOUNT," +
                                " (CASE WHEN ISNULL(BILL.BILLPAX,0)>0 THEN CAST(CAST((ISNULL(BILL.TOTAMOUNT,0) / ISNULL(BILL.BILLPAX,1)) AS FLOAT) AS decimal(16,2)) " +
                                            " ELSE (ISNULL(BILL.TOTAMOUNT,0)) END) AS APC," +
                                " BILL.TOTROFF,BILL.NETAMOUNT" +
                                " FROM BILL" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID)" +
                                " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID)" +
                                " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL (BILL.ISREVISEDBILL,0)=0  " +
                                " And  bill.billDATE between @p_fromdate and @p_todate" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BANQBOFOLLOWUP");
                strproc = " CREATE PROCEDURE SP_BANQBOFOLLOWUP" +
                                    " (" +
                                     " @p_mode as int," +
                                     " @p_rid bigint," +
                                     " @p_followupno nvarchar(200)," +
                                     " @p_banqborid bigint," +
                                     " @p_followupdt datetime," +
                                     " @p_nextfollowupdt datetime," +
                                     " @p_contpername nvarchar(200)," +
                                     " @p_followupby nvarchar(200)," +
                                     " @p_followupremark nvarchar(max)," +
                                     " @p_userid bigint,  " +
                                     " @p_errstr as nvarchar(max) out, " +
                                     " @p_retval as int out, " +
                                     " @p_id as bigint out " +
                                     " ) as " +
                                     " Begin" +
                                     " try" +
                                     " begin" +
                                        "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                     " if (@p_mode=0) " +
                                     " begin" +
                                             " insert into BANQBOFOLLOWUP(BANQBORID,FOLLOWUPNO,FOLLOWUPDT,NEXTFOLLOWUPDT,CONTPERNAME,FOLLOWUPBY,FOLLOWUPREMARK," +
                                                       "  auserid,adatetime,DelFlg)" +
                                             " values (@p_BANQBORID,@p_FOLLOWUPNO,@p_FOLLOWUPDT,@p_NEXTFOLLOWUPDT,@p_CONTPERNAME,@p_FOLLOWUPBY,@p_FOLLOWUPREMARK," +
                                                 "    @p_userid,getdate(),0)" +
                                         " set @p_id=SCOPE_IDENTITY()" +
                                         " End" +
                                     " else if (@p_mode=1) " +
                                         " begin" +
                                     " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                             " update BANQBOFOLLOWUP set BANQBORID=@p_BANQBORID, " +
                                             " FOLLOWUPDT = @p_FOLLOWUPDT, FOLLOWUPNO = @p_FOLLOWUPNO,NEXTFOLLOWUPDT = @p_NEXTFOLLOWUPDT, " +
                                             " CONTPERNAME=@p_CONTPERNAME, " +
                                             " FOLLOWUPBY=@p_FOLLOWUPBY,FOLLOWUPREMARK=@p_FOLLOWUPREMARK,     " +
                                             " euserid = @p_userid,edatetime = getdate() " +
                                             " where rid = @p_rid   " +
                                         " End " +
                                     " End " +
                                     " end " +
                                     " try  " +
                                        "  begin catch" +
                                         " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage" +
                                          " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                         " Return  " +
                                         " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BANQINQUIRY");
                strproc = " CREATE PROCEDURE SP_BANQINQUIRY" +
                                " (" +
                                " @p_mode as int," +
                                " @p_rid bigint," +
                                " @p_inqdate datetime," +
                                " @p_grid bigint," +
                                " @p_dtfunc datetime," +
                                " @p_venue bigint," +
                                " @p_typeoffunc nvarchar(max)," +
                                " @p_nopax bigint," +
                                " @p_foodpackage nvarchar(max)," +
                                " @p_decoremark nvarchar(max)," +
                                " @p_spreq nvarchar(max)," +
                                " @p_followupdt datetime," +
                                " @p_inqby nvarchar(200)," +
                                " @p_canclereason nvarchar(max)," +
                                " @p_inqno nvarchar(50)," +
                                " @p_inqstatus nvarchar(50)," +
                                " @p_inqtype nvarchar(50)," +
                                " @p_userid bigint, " +
                                " @p_errstr as nvarchar(max) out, " +
                                " @p_retval as int out, " +
                                " @p_id as bigint out " +
                                " ) as  " +
                                " Begin" +
                                " try" +
                                " begin" +
                                  " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                " if (@p_mode=0) " +
                                " begin" +
                                " insert into BANQINQUIRY (INQDATE,GRID,DTFUNC,VENUE,TYPEOFFUNC,NOPAX,FOODPACKAGE,DECOREMARK,SPREQ,FOLLOWUPDT,INQBY,CANCLEREASON,INQNO, " +
                                                             " INQSTATUS,INQTYPE," +
                                                            " auserid,adatetime,DelFlg) " +
                                                " values  (@p_INQDATE,@p_GRID,@p_DTFUNC,@p_VENUE,@p_TYPEOFFUNC,@p_NOPAX,@p_FOODPACKAGE,@p_DECOREMARK,@p_SPREQ,@p_FOLLOWUPDT,@p_INQBY,@p_CANCLEREASON, @p_inqno, " +
                                                             " @p_inqstatus,@p_inqtype," +
                                                        " @p_userid,getdate(),0)" +
                                                " set @p_id=SCOPE_IDENTITY()" +
                                " End " +
                                " else if (@p_mode=1) " +
                                " begin" +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                " UPDATE BANQINQUIRY set INQDATE=@p_INQDATE,GRID=@p_GRID,DTFUNC=@p_DTFUNC,VENUE=@p_VENUE,TYPEOFFUNC=@p_TYPEOFFUNC,NOPAX=@p_NOPAX," +
                                " FOODPACKAGE=@p_FOODPACKAGE,DECOREMARK=@p_DECOREMARK,SPREQ=@p_SPREQ,FOLLOWUPDT=@p_FOLLOWUPDT,INQBY=@p_INQBY," +
                                " CANCLEREASON=@p_CANCLEREASON, INQNO=@p_inqno,INQSTATUS=@p_inqstatus,inqtype=@p_inqtype " +
                                 " where rid = @p_rid   " +
                                 " End " +
                                " End " +
                                " end " +
                                " try " +
                                  " begin catch " +
                                  " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage" +
                                   " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                  " Return  " +
                                  " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_KOTREMARKREG");
                strproc = " CREATE PROCEDURE SP_KOTREMARKREG  " +
                             " (@p_fromdate datetime,@p_todate datetime)  " +
                             " AS BEGIN  " +
                             " SELECT KOT.RID,KOTDATE,KOT.KOTTIME,KOT.KOTNO,MSTTABLE.TABLENAME,KOT.KOTREMARK," +
                                " MSTUSERS.USERNAME AS ENTERBY, " +
                                " MSTUSERS1.USERNAME AS EDITUSER,MSTUSERS2.USERNAME AS DELETEUSER,		" +
                                " (CASE WHEN ISNULL(KOT.DELFLG,0)= 1 THEN 'KOT-DELETED' ELSE ' ' END) AS INFO " +
                                    " FROM KOT" +
                                    " LEFT JOIN MSTUSERS ON (MSTUSERS.RID=KOT.AUSERID)" +
                                    " LEFT JOIN MSTUSERS MSTUSERS1 ON (MSTUSERS1.RID=KOT.EUSERID)" +
                                    " LEFT JOIN MSTUSERS MSTUSERS2 ON (MSTUSERS2.RID=KOT.DUSERID)" +
                                    " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=KOT.KOTTABLEID)" +
                                 " WHERE RTRIM(LTRIM(ISNULL(KOT.KOTREMARK,'')))<>'' " +
                                 " And  KOT.KOTDATE between @p_fromdate and @p_todate" +
                                 " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BILLREMARKREG");
                strproc = "CREATE PROCEDURE SP_BILLREMARKREG  " +
                            " (@p_fromdate datetime,@p_todate datetime)  " +
                             " AS BEGIN  " +
                             " SELECT BILL.RID,BILL.BILLDATE,BILL.BILLTIME," +
                                    " BILL.BILLNO,BILL.REFBILLNO,MSTTABLE.TABLENAME,BILL.BILLREMARK," +
                                " MSTUSERS.USERNAME AS ENTERBY, " +
                                " MSTUSERS1.USERNAME AS EDITUSER," +
                                " MSTUSERS2.USERNAME AS DELETEUSER,		" +
                                " (CASE WHEN ISNULL(BILL.DELFLG,0)= 1 THEN 'BILL-DELETED' ELSE ' ' END) AS INFO " +
                                    " FROM BILL" +
                                    " LEFT JOIN MSTUSERS ON (MSTUSERS.RID=BILL.AUSERID)" +
                                    " LEFT JOIN MSTUSERS MSTUSERS1 ON (MSTUSERS1.RID=BILL.EUSERID)" +
                                    " LEFT JOIN MSTUSERS MSTUSERS2 ON (MSTUSERS2.RID=BILL.DUSERID)" +
                                    " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID)" +
                                 " WHERE RTRIM(LTRIM(ISNULL(BILL.BILLREMARK,'')))<>''" +
                                 " And  BILL.BILLDATE between @p_fromdate and @p_todate" +
                                 " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MSTPURITEM");
                strproc = " CREATE PROCEDURE SP_MSTPURITEM" +
                                 " (" +
                                 " @p_mode as int," +
                                 " @p_rid bigint," +
                                 " @p_puriname nvarchar(500)," +
                                 " @p_puriunit nvarchar(50)," +
                                 " @p_minrate decimal(18,2)," +
                                 " @p_maxrate decimal(18,2)," +
                                 " @p_avgrate decimal(18,2)," +
                                 " @p_minqty decimal(18,2)," +
                                 " @p_maxqty decimal(18,2)," +
                                 " @p_avgqty decimal(18,2)," +
                                 " @p_purigrid bigint," +
                                 " @p_ishidepuritem bit," +
                                 " @p_supprid bigint," +
                                 " @p_HSNCODERID bigint," +
                                 " @p_userid bigint," +
                                 " @p_errstr as nvarchar(max) out," +
                                 " @p_retval as int out," +
                                 " @p_id as bigint out" +
                                 " ) as begin" +
                                 " try" +
                                 " begin" +
                                 " if (@p_mode=0) " +
                                  " begin  " +
                                 " declare @codeRowCount as int  " +
                                 " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                 " select @codeRowCount = (select count(*) from MSTPURITEM where PURINAME = @p_PURINAME and ISNULL(DelFlg,0)=0)  " +
                                 " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Purchase Item Name Already exits.'  " +
                                    "  Return " +
                                    " end " +
                                 " begin 	 " +
                                    "  Insert Into MSTPURITEM (PURINAME,puriunit,MINRATE,MAXRATE,AVGRATE,MINQTY,MAXQTY,AVGQTY,PURIGRID,ishidepuritem,supprid,HSNCODERID, " +
                                                                " auserid,adatetime,DelFlg)" +
                                      " Values (@p_puriname,@p_puriunit,@p_minrate,@p_maxrate,@p_avgrate,@p_minqty,@p_maxqty,@p_avgqty,@p_PURIGRID,@p_ishidepuritem,@p_supprid," +
                                                " @p_HSNCODERID," +
                                                " @p_userid,getdate(),0)    " +
                                      " set @p_id=SCOPE_IDENTITY()" +
                                      " End  End" +
                                 " else if (@p_mode=1) " +
                                  " begin  " +
                                " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0 " +
                                " select @codeRowCount1 = (select count(*) from MSTPURITEM where PURINAME=@p_puriname and rid <> @p_rid and ISNULL(DelFlg,0)=0 ) " +
                                " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Purchase Item Name Already exits.'" +
                                " Return   End  end" +
                                 " begin" +
                                  " set @p_Errstr='' set @p_Retval=0 " +
                                     "  Update MSTPURITEM SET PURINAME=@p_puriname,puriunit=@p_puriunit,MINRATE=@p_MINRATE,MAXRATE=@p_MAXRATE,AVGRATE=@p_AVGRATE," +
                                                 " MINQTY=@p_MINQTY,MAXQTY=@p_MAXQTY,AVGQTY=@p_AVGQTY,PURIGRID=@p_PURIGRID,ishidepuritem=@p_ishidepuritem,supprid=@p_supprid," +
                                                 " HSNCODERID = @p_HSNCODERID," +
                                                 " euserid = @p_userid,edatetime = getdate()" +
                                                 " where rid=@p_rid " +
                                          " End" +
                                       " End" +
                                      " end" +
                                      " try  " +
                                         "  begin catch   " +
                                            " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;    " +
                                             " set @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE()" +
                                           " Return " +
                                           " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_BQBOOKING");
                strproc = " create procedure sp_BQBOOKING " +
                                " (" +
                                " @p_mode as int," +
                                " @p_rid as bigint," +
                                " @p_bqdate datetime," +
                                " @p_bqbono nvarchar(50)," +
                                " @p_inqrid bigint," +
                                " @p_custrid bigint," +
                                " @p_bodate datetime," +
                                " @p_botime datetime," +
                                " @p_bonoofper bigint," +
                                " @p_banqrid bigint," +
                                " @p_botypeoffunc nvarchar(1000)," +
                                " @p_perpaxrate decimal(18,2)," +
                                " @p_boamt decimal(18,2)," +
                                " @p_bodeposit decimal(18,2)," +
                                " @p_boentryby nvarchar(1000)," +
                                " @p_menutype nvarchar(1000)," +
                                " @p_spinst nvarchar(max)," +
                                " @p_decoinfo nvarchar(max)," +
                                " @p_boremark nvarchar(max)," +
                                " @p_bqbostatus nvarchar(50)," +
                                " @p_userid bigint," +
                                " @p_errstr as nvarchar(max) out," +
                                " @p_retval as int out," +
                                " @p_id as bigint out ) " +
                                " as " +
                                " begin" +
                                " try " +
                                " begin " +
                                 " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                 " if (@p_mode=0) " +
                                    "  begin" +
                                " insert into BQBOOKING(BQDATE,BQBONO,INQRID,CUSTRID,BODATE,BOTIME,BONOOFPER,BANQRID,BOTYPEOFFUNC,PERPAXRATE,BOAMT,BODEPOSIT,BOENTRYBY," +
                                                        " MENUTYPE,SPINST,DECOINFO,BOREMARK,BQBOSTATUS,auserid,adatetime,DelFlg)" +
                                              " values(@p_BQDATE,@p_BQBONO,@p_INQRID,@p_CUSTRID,@p_BODATE,@p_BOTIME,@p_BONOOFPER,@p_BANQRID,@p_BOTYPEOFFUNC,@p_PERPAXRATE," +
                                                    " @p_BOAMT,@p_BODEPOSIT,@p_BOENTRYBY,@p_MENUTYPE,@p_SPINST,@p_DECOINFO,@p_BOREMARK,@p_BQBOSTATUS,@p_userid,getdate(),0)" +
                                " set  @p_id=SCOPE_IDENTITY()" +
                                " end" +
                                 " else if (@p_mode=1)" +
                                " begin" +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0" +
                                " Update BQBOOKING set BQDATE = @p_BQDATE,BQBONO=@p_BQBONO,INQRID=@p_INQRID,CUSTRID=@p_CUSTRID,BODATE=@p_BODATE,BOTIME=@p_BOTIME," +
                                                    " BONOOFPER=@p_BONOOFPER,BANQRID=@p_BANQRID,BOTYPEOFFUNC=@p_BOTYPEOFFUNC,PERPAXRATE=@p_PERPAXRATE,BOAMT=@p_BOAMT," +
                                                    " BODEPOSIT=@p_BODEPOSIT,BOENTRYBY=@p_BOENTRYBY,MENUTYPE=@p_MENUTYPE,SPINST=@p_SPINST,DECOINFO=@p_DECOINFO," +
                                                    " BOREMARK=@p_BOREMARK,BQBOSTATUS=@p_BQBOSTATUS,euserid = @p_userid,edatetime = getdate()" +
                                                    " where rid = @p_rid" +
                                " end" +
                                " end" +
                                " end" +
                                 " try  " +
                                 " begin catch   " +
                                   " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;" +
                                   " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                   " Return  " +
                                   " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_BQBOOKINGDTL");
                strproc = " create procedure sp_BQBOOKINGDTL" +
                                 " (" +
                                 " @p_mode as int," +
                                 " @p_rid bigint," +
                                 " @p_bqborid bigint," +
                                 " @p_banqirid bigint," +
                                 " @p_idesc nvarchar(max)," +
                                 " @p_userid bigint,  " +
                                 " @p_errstr as nvarchar(max) out,  " +
                                 " @p_retval as int out, " +
                                 " @p_id as bigint out " +
                                 " ) as  " +
                                 " begin  " +
                                 " try  " +
                                 " begin " +
                                  " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                 " if (@p_mode=0)   " +
                                     "  begin " +
                                         "  Insert Into BQBOOKINGDTL (bqborid,BANQIRID,IDESC,auserid,adatetime,DelFlg)  " +
                                                    "   Values ( @p_bqborid,@p_BANQIRID,@p_IDESC,@p_userid,getdate(),0)  " +
                                         " set @p_id=SCOPE_IDENTITY()  " +
                                         " End      " +
                                     " else if (@p_mode=1)" +
                                      " begin  " +
                                          "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                           " Update BQBOOKINGDTL    " +
                                                 "  set BANQIRID=@p_BANQIRID,IDESC=@p_IDESC,euserid = @p_userid,edatetime = getdate()  " +
                                              " where rid = @p_rid  and BQBORID=@p_BQBORID" +
                                         " End " +
                                     " End  " +
                                 " end	 " +
                                 " try  " +
                                    "  begin catch " +
                                        "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                         " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                          " Return    " +
                                         " END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BQBOOKINGINFOREG");
                strproc = " CREATE PROCEDURE SP_BQBOOKINGINFOREG " +
                                 " (@p_fromdate datetime,@p_todate datetime) " +
                                 " AS BEGIN  " +
                                 " SELECT BQBOOKING.RID,BQBOOKING.BQBONO,BQBOOKING.BODATE,BQBOOKING.BOTIME,MSTBANQ.BANQNAME,MSTCUST.CUSTNAME," +
                                        " BQBOOKING.BONOOFPER,BQBOOKING.BOTYPEOFFUNC,BQBOOKING.BOAMT,BQBOOKING.BODEPOSIT,BQBOOKING.BQBOSTATUS" +
                                 " FROM BQBOOKING " +
                                 " INNER JOIN MSTBANQ ON (MSTBANQ.RID=BQBOOKING.BANQRID) " +
                                 " INNER JOIN MSTCUST ON (MSTCUST.RID=BQBOOKING.CUSTRID) " +
                                 " WHERE ISNULL(BQBOOKING.DELFLG,0)=0 AND BQBOOKING.BQBOSTATUS='Confirm'" +
                                        " AND BQBOOKING.BODATE between @p_fromdate and @p_todate" +
                                 " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BQINQUIRYINFOREG");
                strproc = " CREATE PROCEDURE SP_BQINQUIRYINFOREG " +
                                 " (@p_fromdate datetime,@p_todate datetime) " +
                                 " AS BEGIN  " +
                                 " SELECT BANQINQUIRY.INQNO,BANQINQUIRY.INQDATE,BANQINQUIRY.INQSTATUS,BANQINQUIRY.INQTYPE, " +
                                        " BANQINQUIRY.DTFUNC,MSTBANQ.BANQNAME,BANQINQUIRY.TYPEOFFUNC,BANQINQUIRY.NOPAX, " +
                                        " BANQINQUIRY.INQBY,MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO, " +
                                        " BANQINQUIRY.FOODPACKAGE,BANQINQUIRY.DECOREMARK,BANQINQUIRY.SPREQ " +
                                 " FROM BANQINQUIRY  " +
                                 " INNER JOIN MSTBANQ ON (MSTBANQ.RID=BANQINQUIRY.VENUE)  " +
                                 " INNER JOIN MSTCUST ON (MSTCUST.RID=BANQINQUIRY.GRID)  " +
                                 " WHERE ISNULL(BANQINQUIRY.DELFLG,0)=0 " +
                                        " AND BANQINQUIRY.INQDATE between @p_fromdate and @p_todate " +
                                 " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MSTPURITEMGROUP");
                strproc = " CREATE PROCEDURE SP_MSTPURITEMGROUP " +
                                " ( @p_mode as int, @p_rid bigint,  " +
                                " @p_purigcode nvarchar(50),@p_purigname nvarchar(200), " +
                                " @p_purigdesc nvarchar(max),@p_ISAUTOISSUE bit, " +
                                " @p_userid bigint,  " +
                                " @p_errstr as nvarchar(max) out,  " +
                                " @p_retval as int out, " +
                                " @p_id as bigint out  " +
                                " ) as  " +
                                " begin " +
                                " try " +
                                " begin " +
                                " if (@p_mode=0) " +
                                " begin  " +
                                " declare @codeRowCount as int  " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                " select @codeRowCount = (select count(*) from MSTPURITEMGROUP where PURIGCODE = @p_PURIGCODE and ISNULL(DelFlg,0)=0)  " +
                                " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Purchase Item Group Code Already exits.'  " +
                                " Return    " +
                                " End	 " +
                                " Begin  " +
                                " SET NOCOUNT ON " +
                                " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                " select  @nameRowCount = (select count(*) from MSTPURITEMGROUP where PURIGNAME = @p_PURIGNAME and ISNULL(DelFlg,0)=0)   " +
                                " if ( @nameRowCount > 0)   " +
                                " begin    " +
                                " set @p_Retval = 1 set @p_Errstr ='Purchase Item Group Name Already exits.'   " +
                                " Return  " +
                                " End   " +
                                " end " +
                                " begin " +
                                " Insert Into MSTPURITEMGROUP (PURIGCODE,PURIGNAME,PURIGDESC,ISAUTOISSUE,auserid,adatetime,DelFlg) " +
                                                "  Values (@p_PURIGCODE,@p_PURIGNAME,@p_PURIGDESC,@p_ISAUTOISSUE,@p_userid,getdate(),0) " +
                                " set @p_id=SCOPE_IDENTITY()	" +
                                " End  End  " +
                                " else if (@p_mode=1) " +
                                " begin   " +
                                " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0  " +
                                " select @codeRowCount1 = (select count(*) from MSTPURITEMGROUP where PURIGCODE = @p_PURIGCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Purchase Item Group Code Already exits.' " +
                                " Return   End  " +
                                " Begin  " +
                                " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0     " +
                                " select  @nameRowCount1 = (select count(*) from MSTPURITEMGROUP where PURIGNAME = @p_PURIGNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                                " if ( @nameRowCount1 > 0)     " +
                                " begin   " +
                                " set @p_Retval = 1 set @p_Errstr ='Purchase Item Group Name Already exits.' " +
                                " Return  " +
                                " End  END " +
                                " begin  " +
                                " Update MSTPURITEMGROUP set PURIGCODE=@p_PURIGCODE, " +
                                " PURIGNAME = @p_PURIGNAME, PURIGDESC = @p_PURIGDESC, " +
                                " ISAUTOISSUE=@p_ISAUTOISSUE, " +
                                " euserid = @p_userid,edatetime = getdate() " +
                                " where rid = @p_rid    " +
                                " End  End  End  " +
                                " End  " +
                                " try  begin catch    " +
                                " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                " Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_STOCKCLOSING");
                strproc = " CREATE PROCEDURE SP_STOCKCLOSING " +
                                " (@p_mode as int,  @p_rid as bigint,@p_stockclno nvarchar(50), @p_cldate datetime,@p_entryby nvarchar(100), " +
                                " @p_deptrid bigint,@p_clremark nvarchar(max), " +
                                " @p_userid bigint,@p_errstr as nvarchar(max) out,@p_retval as int out, @p_id as bigint out " +
                                " ) " +
                                " as   " +
                                " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0)   " +
                                " begin 		  " +
                                " Insert Into STOCKCLOSING(stockclno,cldate,entryby,deptrid,clremark,AUSERID,ADATETIME,DELFLG) " +
                                                  " Values (@p_stockclno,@p_cldate,@p_entryby,@p_deptrid,@p_clremark,@p_userid,getdate(),0)  " +
                                " set @p_id=SCOPE_IDENTITY() End      " +
                                " else if (@p_mode=1)    " +
                                " begin set @p_Errstr='' set @p_Retval=0 set @p_id=0" +
                                " update STOCKCLOSING set stockclno=@p_stockclno,cldate=@p_cldate,entryby=@p_entryby," +
                                        " 	deptrid=@p_deptrid,clremark=@p_clremark," +
                                            " euserid = @p_userid,edatetime = getdate()  " +
                                            " where rid = @p_rid  " +
                                " end " +
                                " End end " +
                                " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return    " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_STOCKCLOSINGDTL");
                strproc = " create  procedure sp_STOCKCLOSINGDTL" +
                           " (  " +
                           " @p_mode as int,  " +
                           " @p_rid as bigint, " +
                           " @p_clrid bigint, " +
                           " @p_irid bigint," +
                           " @p_iname nvarchar(100)," +
                           " @p_iqty decimal(18,3)," +
                           " @p_iunit nvarchar(50)," +
                           " @p_irate decimal(18,2)," +
                           " @p_userid bigint,  " +
                           " @p_errstr as nvarchar(max) out,  " +
                           " @p_retval as int out, " +
                           " @p_id as bigint out  " +
                           " ) as  " +
                           " begin  " +
                           " try  " +
                               " begin  " +
                                   " set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                                    " if (@p_mode=0)   " +
                                       " begin  " +
                                        " Insert Into STOCKCLOSINGDTL (clrid,irid,INAME,IQTY,IUNIT,IRATE,auserid,adatetime,DelFlg) " +
                                                            " Values (@p_clrid,@p_irid,@p_INAME,@p_IQTY,@p_IUNIT,@p_irate,@p_userid,getdate(),0 )" +
                                                    " set @p_id=SCOPE_IDENTITY() " +
                                                " End   " +
                                   " else if (@p_mode=1)    " +
                                       " begin  " +
                                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                           " update STOCKCLOSINGDTL  " +
                                          " set irid=@p_irid,INAME=@p_INAME,IQTY=@p_IQTY,IUNIT=@p_IUNIT,IRATE=@p_irate," +
                                                                    "  euserid = @p_userid,edatetime = getdate() " +
                                                                     " where rid = @p_rid and clrid=@p_clrid " +
                                       " End  " +
                                    " End  " +
                                   " end	 " +
                                   " try   " +
                                       " begin catch   " +
                                        " SELECT   ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage; " +
                                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                        " Return   " +
                                        " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_STOCKWASTAGE");
                strproc = " CREATE PROCEDURE sp_STOCKWASTAGE " +
                                " (@p_mode as int,  @p_rid as bigint,@p_wstno nvarchar(50), @p_wstdate datetime,@p_entryby nvarchar(100), " +
                                " @p_deptrid bigint,@p_wstremark nvarchar(max), " +
                                " @p_userid bigint,@p_errstr as nvarchar(max) out,@p_retval as int out, @p_id as bigint out " +
                                " ) " +
                                " as   " +
                                " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0)   " +
                                " begin 		  " +
                                " Insert Into STOCKWASTAGE(wstno,wstdate,entryby,deptrid,wstremark,AUSERID,ADATETIME,DELFLG) " +
                                                  " Values (@p_wstno,@p_wstdate,@p_entryby,@p_deptrid,@p_wstremark,@p_userid,getdate(),0)  " +
                                " set @p_id=SCOPE_IDENTITY() End      " +
                                " else if (@p_mode=1)    " +
                                " begin set @p_Errstr='' set @p_Retval=0 set @p_id=0" +
                                " update STOCKWASTAGE set wstno=@p_wstno,wstdate=@p_wstdate,entryby=@p_entryby," +
                                        " 	deptrid=@p_deptrid,wstremark=@p_wstremark," +
                                            " euserid = @p_userid,edatetime = getdate()  " +
                                            " where rid = @p_rid  " +
                                " end " +
                                " End end " +
                                " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return    " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_STOCKWASTAGEDTL");
                strproc = " create  procedure sp_STOCKWASTAGEDTL" +
                           " (  " +
                           " @p_mode as int,  " +
                           " @p_rid as bigint, " +
                           " @p_wstrid bigint, " +
                           " @p_irid bigint," +
                           " @p_iname nvarchar(100)," +
                           " @p_iqty decimal(18,4)," +
                           " @p_iunit nvarchar(50)," +
                           " @p_irate decimal(18,4)," +
                           " @p_userid bigint,  " +
                           " @p_errstr as nvarchar(max) out,  " +
                           " @p_retval as int out, " +
                           " @p_id as bigint out  " +
                           " ) as  " +
                           " begin  " +
                           " try  " +
                               " begin  " +
                                   " set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                                    " if (@p_mode=0)   " +
                                       " begin  " +
                                        " Insert Into STOCKWASTAGEDTL (wstrid,irid,INAME,IQTY,IUNIT,IRATE,auserid,adatetime,DelFlg) " +
                                                            " Values  (@p_wstrid,@p_irid,@p_INAME,@p_IQTY,@p_IUNIT,@p_irate,@p_userid,getdate(),0)" +
                                                    " set @p_id=SCOPE_IDENTITY() " +
                                                " End   " +
                                   " else if (@p_mode=1)    " +
                                       " begin  " +
                                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                           " Update STOCKWASTAGEDTL  " +
                                          " set irid=@p_irid,INAME=@p_INAME,IQTY=@p_IQTY,IUNIT=@p_IUNIT,IRATE=@p_irate," +
                                                                    "  euserid = @p_userid,edatetime = getdate() " +
                                                                     " where rid = @p_rid and wstrid=@p_wstrid " +
                                       " End  " +
                                    " End  " +
                                   " end	 " +
                                   " try   " +
                                       " begin catch   " +
                                        " SELECT   ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage; " +
                                        " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                        " Return   " +
                                        " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_ITEMPURCHASEREGISTER");
                strproc = " CREATE procedure sp_ITEMPURCHASEREGISTER" +
                                " (@p_fromdate datetime,@p_todate datetime)" +
                                " as" +
                                " begin" +
                                " select ITEMPURCHASE.RID,ITEMPURCHASE.PURDATE,ITEMPURCHASE.PURNO,ITEMPURCHASE.DOCNO,ITEMPURCHASE.DOCDATE," +
                                " MSTSUPPLIERLIST.SUPPNAME,ITEMPURCHASE.SUPPCONTNO,ITEMPURCHASE.NETAMOUNT" +
                                " from ITEMPURCHASE" +
                                " LEFT JOIN MSTSUPPLIERLIST ON (MSTSUPPLIERLIST.RID = ITEMPURCHASE.SUPPRID)" +
                                " WHERE ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate and isnull(ITEMPURCHASE.delflg,0)=0" +
                                " order by ITEMPURCHASE.PURDATE" +
                                " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTREFBYTYPE");
                strproc = " CREATE procedure sp_MSTREFBYTYPE" +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid bigint, " +
                         " @p_refbytypecode nvarchar(20), " +
                         " @p_refbytypename nvarchar(200), " +
                         " @p_refbytypedesc nvarchar(max),    " +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out " +
                         " ) as " +
                         " begin " +
                         " try " +
                         " begin " +
                         " if (@p_mode=0) " +
                         " begin  " +
                         " declare @codeRowCount as int  " +
                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                         " select @codeRowCount = (select count(*) from MSTREFBYTYPE where REFBYTYPECODE = @p_REFBYTYPECODE and ISNULL(DelFlg,0)=0)  " +
                         " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'  " +
                            "  Return    " +
                         " End	 " +
                   " Begin  " +
                 " SET NOCOUNT ON " +
                    "   declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                      " select  @nameRowCount = (select count(*) from MSTREFBYTYPE where REFBYTYPENAME = @p_REFBYTYPENAME and ISNULL(DelFlg,0)=0)   " +
                      " if ( @nameRowCount > 0)   " +
                      " begin    " +
                      " set @p_Retval = 1 set @p_Errstr ='Name Already exits.'   " +
                      " Return  " +
                          "      End   " +
                           "   end " +
                                        "   begin " +
                                        "   Insert Into MSTREFBYTYPE (REFBYTYPECODE,REFBYTYPENAME,REFBYTYPEDESC,auserid,adatetime,DelFlg) " +
                                        "   Values (@p_REFBYTYPECODE,@p_REFBYTYPENAME,@p_REFBYTYPEDESC,@p_userid,getdate(),0) " +
                                        " 	set @p_id=SCOPE_IDENTITY()	" +
                                         "  End  End  " +
                                 " else if (@p_mode=1) " +
                                   "    begin   " +
                                    "   declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0  " +
                                     "  select @codeRowCount1 = (select count(*) from MSTREFBYTYPE where REFBYTYPECODE = @p_REFBYTYPECODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                      " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.' " +
                                      " Return   End  " +
                              " Begin  " +
                                "   declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0     " +
                                 "  select  @nameRowCount1 = (select count(*) from MSTREFBYTYPE where REFBYTYPENAME = @p_REFBYTYPENAME and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                                  " if ( @nameRowCount1 > 0)     " +
                                  " begin   " +
                                  " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                                  " Return  " +
                               " End  END " +
                              " begin  " +
                                     "  Update MSTREFBYTYPE set REFBYTYPECODE=@p_REFBYTYPECODE, " +
                                      " REFBYTYPENAME = @p_REFBYTYPENAME, REFBYTYPEDESC = @p_REFBYTYPEDESC,  " +
                                      " euserid = @p_userid,edatetime = getdate() " +
                                      " where rid = @p_rid " +
                                      " End  End  End  " +
                                      " End  " +
                                      " try  begin catch    " +
                                      " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                      " set  @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                      " Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTREFBY");
                strproc = "CREATE procedure sp_MSTREFBY " +
                            " ( " +
                            " @p_mode as int, " +
                            " @p_rid bigint, " +
                            " @p_refbycode nvarchar(20), " +
                            " @p_refbyname nvarchar(200), " +
                            " @p_refbytyperid bigint," +
                            " @p_refbydesc nvarchar(max)," +
                            " @p_userid bigint, " +
                            " @p_errstr as nvarchar(max) out, " +
                            " @p_retval as int out," +
                            " @p_id as bigint out " +
                            " ) as " +
                            " begin " +
                            " try " +
                            " begin " +
                            " if (@p_mode=0) " +
                            " begin  " +
                            " declare @codeRowCount as int  " +
                            " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                            " select @codeRowCount = (select count(*) from MSTREFBY where REFBYCODE = @p_REFBYCODE and ISNULL(DelFlg,0)=0)  " +
                            " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'  " +
                            " Return    " +
                            " End	 " +
                            " Begin  " +
                            " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                            " select  @nameRowCount = (select count(*) from MSTREFBY where REFBYNAME = @p_REFBYNAME and ISNULL(DelFlg,0)=0)   " +
                            " if ( @nameRowCount > 0)   " +
                            " begin    " +
                            " set @p_Retval = 1 set @p_Errstr ='Name Already exits.'   " +
                            " Return  " +
                            " End   " +
                            " end " +
                                     "  begin " +
                                      " Insert Into MSTREFBY (REFBYCODE,REFBYNAME,REFBYTYPERID,REFBYDESC,auserid,adatetime,DelFlg) " +
                                      " Values (@p_REFBYCODE,@p_REFBYNAME,@p_REFBYTYPERID,@p_REFBYDESC,@p_userid,getdate(),0) " +
                                        " set @p_id=SCOPE_IDENTITY()	" +
                                      " End  End  " +
                             " else if (@p_mode=1) " +
                                 "  begin   " +
                                 "  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0  " +
                                 "  select @codeRowCount1 = (select count(*) from MSTREFBY where REFBYCODE = @p_REFBYCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                 "  if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.' " +
                                  " Return   End  " +
                            " Begin  " +
                              " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0     " +
                              " select  @nameRowCount1 = (select count(*) from MSTREFBY where REFBYNAME = @p_REFBYNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0)   " +
                              " if ( @nameRowCount1 > 0)     " +
                              " begin   " +
                              " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                              " Return  " +
                            " End  END " +
                            " begin  " +
                                 "  Update MSTREFBY set REFBYCODE=@p_REFBYCODE, " +
                                  " REFBYNAME = @p_REFBYNAME, REFBYTYPERID = @p_REFBYTYPERID,REFBYDESC=@p_REFBYDESC, " +
                                  " euserid = @p_userid,edatetime = getdate() " +
                                  " where rid = @p_rid " +
                                  " End  End  End  " +
                                  " End  " +
                                  " try  begin catch    " +
                                  " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                  " set  @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                  " Return  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_REFBYINFO");
                strproc = " create procedure SP_REFBYINFO " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN " +
                                " select bill.rid, bill.BILLDATE,bill.BILLTIME,bill.BILLNO,bill.REFBILLNO,bill.BILLPAX,bill.TOTDISCAMOUNT,bill.NETAMOUNT," +
                                " mstcust.CUSTNAME,MSTREFBYTYPE.RID AS MSTREFBYTYPERID,MSTREFBYTYPE.REFBYTYPENAME, " +
                                " MSTREFBY.RID AS MSTREFBYRID,MSTREFBY.REFBYNAME,MSTUSERS.USERNAME " +
                                " from bill " +
                                " left join mstcust on (mstcust.rid=bill.CUSTRID)" +
                                " left join MSTREFBY on (MSTREFBY.rid=bill.REFBYRID)" +
                                " left join MSTREFBYTYPE on (MSTREFBYTYPE.rid=MSTREFBY.REFBYTYPERID)" +
                                " left join MSTUSERS on (MSTUSERS.rid=bill.AUSERID)" +
                                " where isnull(bill.delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0" +
                                " and isnull(bill.REFBYRID,0)>0 and bill.billDATE between @p_fromdate and @p_todate " +
                                " ORDER BY MSTREFBYTYPE.REFBYTYPENAME,MSTREFBY.REFBYNAME,bill.BILLDATE,bill.BILLTIME" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_KOTHISTORY");
                strproc = " CREATE PROCEDURE SP_KOTHISTORY " +
                                    " (@p_mode as int,   " +
                                    " @p_rid as bigint, " +
                                    " @p_kotrid bigint, " +
                                    " @p_kotinfo nvarchar(max)," +
                                    " @p_billrid bigint," +
                                    " @p_userid bigint,  " +
                                    " @p_errstr as nvarchar(max) out,  " +
                                    " @p_retval as int out, " +
                                    " @p_id as bigint out  " +
                                    " ) as  " +
                                    " begin  " +
                                    " try  " +
                                    " begin  " +
                                    " set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                                     " if (@p_mode=0)   " +
                                       "  begin  " +
                                         " Insert Into KOTHISTORY (KOTRID,KOTINFO,BILLRID,AUSERRID,adatetime) " +
                                                            "  Values  (@p_KOTRID,@p_KOTINFO,@p_BILLRID,@p_userid,getdate())" +
                                                     " set @p_id=SCOPE_IDENTITY() " +
                                                 " End   " +
                                    " else if (@p_mode=1)   " +
                                      "   begin  " +
                                        "   set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                          "   Update KOTHISTORY  " +
                                           " set KOTRID=@p_KOTRID,KOTINFO=@p_KOTINFO,BILLRID=@p_BILLRID," +
                                             "  EUSERRID = @p_userid,edatetime = getdate() " +
                                             "  where KOTRID = @p_KOTRID and billrid=@p_billrid" +
                                        " End  " +
                                     " End  " +
                                    " end	 " +
                                    " try   " +
                                      "   begin catch   " +
                                        "  SELECT   ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage; " +
                                         " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                         " Return   " +
                                         " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_KOTEDITDELETEINFO");
                strproc = " CREATE PROCEDURE SP_KOTEDITDELETEINFO " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN  " +
                                " SELECT KOT.RID,KOT.KOTDATE,KOT.KOTNO,KOT.KOTTIME,MSTTABLE.TABLENAME," +
                                " MSTEMP.EMPNAME AS ORDERBY,ADDUSER.USERNAME," +
                                " KOTREMARK,KOTINFO" +
                                " FROM KOT" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=KOT.KOTTABLEID)" +
                                " LEFT JOIN MSTEMP ON (MSTEMP.RID=KOT.KOTORDERPERID)" +
                                " LEFT JOIN MSTUSERS ADDUSER ON (ADDUSER.RID=KOT.AUSERID)" +
                                " WHERE RTRIM(LTRIM(KOT.KOTINFO))!='' AND KOT.KOTDATE between @p_fromdate and @p_todate" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BILLEDITDELETEINFO");
                strproc = " CREATE PROCEDURE SP_BILLEDITDELETEINFO " +
                                " (@p_fromdate datetime,@p_todate datetime)  " +
                                " AS BEGIN   " +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE,BILLTIME,BILL.CUSTNAME, " +
                                " MSTTABLE.TABLENAME,BILL.NETAMOUNT,BILL.BILLREMARK,BILL.BILLINFO," +
                                " BILL.BILLPREPBY,ADDUSER.USERNAME" +
                                " FROM BILL" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID)" +
                                " LEFT JOIN MSTUSERS ADDUSER ON (ADDUSER.RID=BILL.AUSERID)" +
                                " WHERE RTRIM(LTRIM(BILL.BILLINFO))!='' AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_KOTMACHWITHBILLINFO");
                strproc = " CREATE PROCEDURE SP_KOTMACHWITHBILLINFO " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN  " +
                                " select KOTHISTORY.BILLRID,KOTHISTORY.KOTRID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE,BILL.BILLTIME, " +
                                " KOT.KOTDATE,KOT.KOTTIME,KOT.KOTNO,KOT.REFKOTNO,BILLUSER.USERNAME AS BILLADDUSER,KOTUSER.USERNAME AS KOTADDUSER," +
                                " KOT.KOTREMARK,KOT.KOTINFO,MSTTABLE.TABLENAME AS BILLTABLE,BILL.CUSTNAME,BILL.NETAMOUNT," +
                                " BILL.BILLINFO,BILL.BILLREMARK" +
                                " from KOTHISTORY" +
                                " LEFT JOIN BILL ON (BILL.RID=KOTHISTORY.BILLRID)" +
                                " LEFT JOIN KOT ON (KOT.RID=KOTHISTORY.KOTRID)" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID)" +
                                " LEFT JOIN MSTUSERS BILLUSER ON (BILLUSER.RID=BILL.AUSERID)" +
                                " LEFT JOIN MSTUSERS KOTUSER ON (KOTUSER.RID=KOT.AUSERID)" +
                                " WHERE ISNULL(BILLNO,'')!=''" +
                                " AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                " ORDER BY BILL.BILLDATE,BILL.BILLNO" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BILLTIMEWISEITEMINFO");
                strproc = " create procedure SP_BILLTIMEWISEITEMINFO  " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN  " +
                                " select billinfo.info,sum(billinfo.IQTY) as totiqty,billinfo.INAME,billinfo.IRID  " +
                                " from( " +
                                    " select bill.billdate,bill.billtime," +
                                    " billdtl.IRID,billdtl.IQTY," +
                                    " mstitem.INAME," +
                                    " DATEPART(hour,bill.billtime) as hours, " +
                                    " '01-09' as info " +
                                    " from bill " +
                                    " left join billdtl on (billdtl.BILLRID = bill.rid) " +
                                    " left join mstitem on (mstitem.rid = billdtl.IRID) " +
                                    " where isnull(bill.delflg,0)=0  and isnull(billdtl.delflg,0)=0 " +
                                    " and DATEPART(hour,bill.billtime) in ('0','1','2','3','4','5','6','7','8')" +
                                    " and billdtl.IQTY>0 " +
                                    " AND BILL.BILLDATE between @p_fromdate and @p_todate " +
                                    " union all " +
                                    " select bill.billdate,bill.billtime," +
                                    " billdtl.IRID,billdtl.IQTY,mstitem.INAME, " +
                                    " DATEPART(hour,bill.billtime) as hours, " +
                                    " '09-12' as info " +
                                    " from bill " +
                                    " left join billdtl on (billdtl.BILLRID = bill.rid) " +
                                    " left join mstitem on (mstitem.rid = billdtl.IRID) " +
                                    " where isnull(bill.delflg,0)=0  and isnull(billdtl.delflg,0)=0 " +
                                    " and DATEPART(hour,bill.billtime) in ('9','10','11') " +
                                    " and billdtl.IQTY>0 " +
                                    " AND BILL.BILLDATE between @p_fromdate and @p_todate " +
                                    " union all" +
                                    " select bill.billdate,bill.billtime, " +
                                    " billdtl.IRID,billdtl.IQTY,mstitem.INAME, " +
                                    " DATEPART(hour,bill.billtime) as hours, " +
                                    " '12-15' as info " +
                                    " from bill " +
                                    " left join billdtl on (billdtl.BILLRID = bill.rid) " +
                                    " left join mstitem on (mstitem.rid = billdtl.IRID) " +
                                    " where isnull(bill.delflg,0)=0  and isnull(billdtl.delflg,0)=0 " +
                                    " and DATEPART(hour,bill.billtime) in ('12','13','14') " +
                                    " and billdtl.IQTY>0 " +
                                    " AND BILL.BILLDATE between @p_fromdate and @p_todate " +
                                    " union all " +
                                    " select bill.billdate,bill.billtime, " +
                                    " billdtl.IRID,billdtl.IQTY,mstitem.INAME, " +
                                    " DATEPART(hour,bill.billtime) as hours," +
                                    " '15-18' as info" +
                                    " from bill" +
                                    " left join billdtl on (billdtl.BILLRID = bill.rid)" +
                                    " left join mstitem on (mstitem.rid = billdtl.IRID)" +
                                    " where isnull(bill.delflg,0)=0  and isnull(billdtl.delflg,0)=0" +
                                " and billdtl.IQTY>0" +
                                    " and DATEPART(hour,bill.billtime) in ('15','16','17')" +
                                " AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                    " union all" +
                                    " select bill.billdate,bill.billtime," +
                                    " billdtl.IRID,billdtl.IQTY,mstitem.INAME," +
                                    " DATEPART(hour,bill.billtime) as hours," +
                                    " '18-21' as info" +
                                    " from bill" +
                                    " left join billdtl on (billdtl.BILLRID = bill.rid)" +
                                    " left join mstitem on (mstitem.rid = billdtl.IRID)" +
                                    " where isnull(bill.delflg,0)=0  and isnull(billdtl.delflg,0)=0" +
                                " and billdtl.IQTY>0" +
                                    " and DATEPART(hour,bill.billtime) in ('18','19','20')" +
                                " AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                    " union all" +
                                    " select bill.billdate,bill.billtime," +
                                    " billdtl.IRID,billdtl.IQTY,mstitem.INAME," +
                                    " DATEPART(hour,bill.billtime) as hours," +
                                    " '21-24' as info" +
                                    " from bill" +
                                    " left join billdtl on (billdtl.BILLRID = bill.rid)" +
                                    " left join mstitem on (mstitem.rid = billdtl.IRID)" +
                                    " where isnull(bill.delflg,0)=0  and isnull(billdtl.delflg,0)=0" +
                                    " and DATEPART(hour,bill.billtime) in ('21','22','23')" +
                                " and billdtl.IQTY>0" +
                                    " AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                " ) billinfo" +
                                " group by billinfo.info,billinfo.INAME,billinfo.IRID" +
                                " order by billinfo.info" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MSTTIEUPCOMPANY");
                strproc = " CREATE PROCEDURE SP_MSTTIEUPCOMPANY " +
                            " ( @p_mode as int, @p_rid bigint,   " +
                             " @p_compcode nvarchar(50),@p_compname nvarchar(500), " +
                             " @p_contper nvarchar(500),@p_contno nvarchar(500), " +
                             " @p_compdisc decimal(18,3),@p_compremark nvarchar(max)," +
                             " @p_paymentby nvarchar(100)," +
                             " @p_SALECOMMIPER decimal(18,3)," +
                             " @p_COMMIPER decimal(18,3)," +
                             " @p_userid bigint,  " +
                             " @p_errstr as nvarchar(max) out,  " +
                             " @p_retval as int out, " +
                             " @p_id as bigint out  " +
                             " ) as  " +
                             " begin " +
                             " try " +
                             " begin " +
                             " if (@p_mode=0) " +
                             " begin  " +
                             " declare @codeRowCount as int  " +
                             " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                             " select @codeRowCount = (select count(*) from MSTTIEUPCOMPANY where COMPCODE = @p_COMPCODE and ISNULL(DelFlg,0)=0)  " +
                             " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Company Code Already exits.'  " +
                             " Return    " +
                             " End	 " +
                             " Begin  " +
                             " SET NOCOUNT ON " +
                             " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                             " select  @nameRowCount = (select count(*) from MSTTIEUPCOMPANY where COMPNAME = @p_COMPNAME and ISNULL(DelFlg,0)=0)   " +
                             " if ( @nameRowCount > 0)   " +
                             " begin    " +
                             " set @p_Retval = 1 set @p_Errstr ='Company Name Already exits.'   " +
                             " Return  " +
                             " End   " +
                             " end " +
                             " begin " +
                             " Insert Into MSTTIEUPCOMPANY (COMPCODE,COMPNAME,CONTPER,CONTNO,COMPDISC,COMPREMARK,paymentby,SALECOMMIPER,COMMIPER,senddata,auserid,adatetime,DelFlg) " +
                                               " Values (@p_COMPCODE,@p_COMPNAME,@p_CONTPER,@p_CONTNO,@p_COMPDISC,@p_COMPREMARK,@p_paymentby,@p_SALECOMMIPER,@p_COMMIPER,0,@p_userid,getdate(),0) " +
                             " set @p_id=SCOPE_IDENTITY()	" +
                             " End  End  " +
                             " else if (@p_mode=1) " +
                             " begin   " +
                             " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0 " +
                             " select @codeRowCount1 = (select count(*) from MSTTIEUPCOMPANY where COMPCODE = @p_COMPCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                             " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Company Code Already exits.'" +
                             " Return   End  " +
                             " Begin  " +
                             " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0  " +
                             " select  @nameRowCount1 = (select count(*) from MSTTIEUPCOMPANY where COMPNAME = @p_COMPNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0)  " +
                             " if ( @nameRowCount1 > 0) " +
                             " begin   " +
                             " set @p_Retval = 1 set @p_Errstr ='Company Name Already exits.' " +
                             " Return  " +
                             " End  END " +
                             " Begin " +
                             " Update MSTTIEUPCOMPANY set COMPCODE=@p_COMPCODE, " +
                             " COMPNAME = @p_COMPNAME, CONTPER = @p_CONTPER, " +
                             " CONTNO=@p_CONTNO, COMPDISC=@p_COMPDISC,COMPREMARK=@p_COMPREMARK,paymentby=@p_paymentby,SALECOMMIPER=@p_SALECOMMIPER,COMMIPER=@p_COMMIPER," +
                             " Senddata=0,euserid=@p_userid,edatetime = getdate() " +
                             " where rid = @p_rid   " +
                             " End  End  End  " +
                             " End  " +
                             " try  begin catch  " +
                             " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                             " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                             " Return  END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_TABLERESERVATION");
                strproc = " CREATE PROCEDURE SP_TABLERESERVATION" +
                        " ( @p_mode as int, @p_rid bigint,  " +
                        " @p_revno nvarchar(20),@p_bodate datetime," +
                        " @p_revdate datetime,@p_revtime datetime," +
                        " @p_custrid bigint, @p_tablerid bigint," +
                        " @p_pax bigint,@p_funcname nvarchar(500),@p_spreq nvarchar(max)," +
                        " @p_revdesc nvarchar(max),@p_entryby nvarchar(500)," +
                        " @p_userid bigint, " +
                        " @p_errstr as nvarchar(max) out, " +
                        " @p_retval as int out," +
                        " @p_id as bigint out " +
                        " ) as " +
                        " begin" +
                        " try" +
                            " begin" +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                                " if (@p_mode=0)" +
                                    " begin" +
                                        " Insert Into TABLERESERVATION (REVNO,BODATE,REVDATE,REVTIME,CUSTRID,TABLERID,PAX,FUNCNAME,SPREQ,REVDESC," +
                                                        " ENTRYBY,auserid,adatetime,DelFlg)" +
                                       " Values (@p_REVNO,@p_BODATE,@p_REVDATE,@p_REVTIME,@p_CUSTRID,@p_TABLERID,@p_PAX,@p_FUNCNAME," +
                                                " @p_SPREQ,@p_REVDESC,@p_ENTRYBY,@p_userid,getdate(),0)" +
                                        " set @p_id=SCOPE_IDENTITY()	 " +
                                    " end" +
                                " else if (@p_mode=1)" +
                                    " begin  " +
                                        " set @p_Errstr='' set @p_Retval=0  set @p_id=0" +
                                        " Update TABLERESERVATION set REVNO=@p_REVNO," +
                                        " BODATE = @p_BODATE, REVDATE = @p_REVDATE,REVTIME=@p_REVTIME,CUSTRID=@p_CUSTRID," +
                                        " TABLERID=@p_TABLERID, PAX=@p_PAX,FUNCNAME=@p_FUNCNAME, SPREQ=@p_SPREQ,REVDESC=@p_REVDESC," +
                                        " ENTRYBY=@p_ENTRYBY,euserid = @p_userid,edatetime = getdate()" +
                                        " where rid = @p_rid  " +
                                    " End " +
                            " End " +
                        " end" +
                        " try begin " +
                            " catch " +
                                " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                    " set @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                    " Return  " +
                            " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_TIEUPCOMPANYBILLINFO");
                strproc = " CREATE PROCEDURE SP_TIEUPCOMPANYBILLINFO" +
                            " (@p_fromdate datetime,@p_todate datetime)" +
                            " AS BEGIN" +
                            " select MSTTIEUPCOMPANY.RID AS MSTTIEUPCOMPANYRID,MSTTIEUPCOMPANY.COMPNAME," +
                            " bill.BILLNO,bill.REFBILLNO,bill.BILLDATE,bill.BILLTIME,MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO," +
                            " bill.COUPONNO,bill.COUPONPERNAME,bill.TOTDISCAMOUNT,bill.TOTADDDISCAMT,(isnull(-1 * billdtl.CouponDisc,0)) as CouponDisc,bill.NETAMOUNT " +
                            " from bill " +
                            " left join " +
                                     "(  select billdtl.BILLRID,sum(billdtl.IAMT) as CouponDisc from billdtl " +
                                        " where billdtl.IAMT<0 and isnull(billdtl.delflg,0)=0" +
                                        " group by billdtl.BILLRID" +
                                     " ) billdtl on (bill.rid=billdtl.BILLRID)" +
                            " left join MSTTIEUPCOMPANY on (MSTTIEUPCOMPANY.rid=bill.MSTTIEUPCOMPRID)" +
                            " left join MSTCUST on (MSTCUST.rid=bill.CUSTRID)" +
                            " where isnull(bill.delflg,0)=0 and isnull(BILL.ISREVISEDBILL,0)=0 and isnull(bill.MSTTIEUPCOMPRID,0)>0" +
                            " And bill.billDATE between @p_fromdate and @p_todate " +
                            " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_BILLWISEDETAILSUMMARY");
                strproc = "CREATE PROCEDURE SP_BILLWISEDETAILSUMMARY" +
                                " (@p_fromdate datetime,@p_todate datetime)" +
                                " AS BEGIN" +
                                " SELECT '0' AS DISPORD, BILL.RID,BILL.BILLNO,BILL.REFBILLNO,ISNULL(MSTTABLE.TABLENAME,'') AS TABLENAME,BILL.BILLPAX," +
                                " (CASE WHEN BILL.ISPARCELBILL='1' THEN 'PARCEL' ELSE 'DINE IN' END ) AS PARCLE,   " +
                                " (ISNULL(BILL.TOTAMOUNT,0) + ISNULL(BILLDTL.COUPONDISC,0)) AS TOTAMOUNT, " +
                                " ISNULL(BILL.TOTSERCHRAMT,0) AS TOTSERCHRAMT, " +
                                " BILL.TOTDISCAMOUNT,ISNULL(BILLDTL.COUPONDISC,0) AS COUPONDISC,(ISNULL(BILL.TOTAMOUNT,0) - ISNULL(BILL.TOTDISCAMOUNT,0)) AS TAXABLEAMT, " +
                                " (ISNULL(BILL.TOTVATAMOUNT,0) + ISNULL(BILL.TOTLIQVATAMT,0) + ISNULL(BILL.TOTBEVVATAMT,0)) AS VATAMOUNT, " +
                                " (ISNULL(BILL.TOTKKCESSAMT,0) + ISNULL(BILL.TOTADDVATAMOUNT,0) + ISNULL(BILL.TOTSERTAXAMOUNT,0)) AS SERTAXAMT, ISNULL(BILL.TOTGSTAMT,0) AS GSTAMT, " +
                                " (BILL.TOTADDDISCAMT) AS OTHERDISC," +
                                " BILL.NETAMOUNT," +
                                " SETTINFO.SETLETYPE,SETTINFO.OTHERPAYMENTBY,SETTINFO.SETLEAMOUNT" +
                                " FROM BILL" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID) " +
                                    " LEFT JOIN ( SELECT BILLDTL.BILLRID,((SUM(ISNULL(BILLDTL.IAMT,0))) * -1) AS COUPONDISC  " +
                                                    " FROM BILLDTL  " +
                                                    " WHERE ISNULL(BILLDTL.DELFLG,0)=0 AND ISNULL(BILLDTL.IQTY,0)<0 " +
                                                    " GROUP BY BILLDTL.BILLRID " +
                                            " ) BILLDTL ON (BILLDTL.BILLRID=BILL.RID)" +
                                " LEFT JOIN	( " +
                                                " SELECT SETTLEMENT.BILLRID,SETTLEMENT.SETLETYPE,SETTLEMENT.OTHERPAYMENTBY,SETTLEMENT.SETLEAMOUNT " +
                                                " FROM SETTLEMENT" +
                                                " WHERE ISNULL(SETTLEMENT.DELFLG,0)=0 " +
                                            " ) AS SETTINFO ON (SETTINFO.BILLRID = BILL.RID)" +
                                " WHERE ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0" +
                                " AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                " UNION " +
                                " SELECT '1'AS DISPORD, BILL.RID,BILL.BILLNO,BILL.REFBILLNO,MSTTABLE.TABLENAME AS TABLENAME, BILL.BILLPAX," +
                                " (CASE WHEN BILL.ISPARCELBILL='1' THEN 'PARCEL' ELSE 'DINE IN' END ) AS PARCLE,   " +
                                " (ISNULL(BILL.TOTAMOUNT,0) + ISNULL(BILLDTL.COUPONDISC,0)) AS TOTAMOUNT, " +
                                " ISNULL(BILL.TOTSERCHRAMT,0) AS TOTSERCHRAMT, " +
                                " BILL.TOTDISCAMOUNT,ISNULL(BILLDTL.COUPONDISC,0) AS COUPONDISC,(ISNULL(BILL.TOTAMOUNT,0) - ISNULL(BILL.TOTDISCAMOUNT,0)) AS TAXABLEAMT, " +
                                " (ISNULL(BILL.TOTVATAMOUNT,0) + ISNULL(BILL.TOTLIQVATAMT,0) + ISNULL(BILL.TOTBEVVATAMT,0)) AS VATAMOUNT, " +
                                " (ISNULL(BILL.TOTKKCESSAMT,0) + ISNULL(BILL.TOTADDVATAMOUNT,0) + ISNULL(BILL.TOTSERTAXAMOUNT,0)) AS SERTAXAMT, ISNULL(BILL.TOTGSTAMT,0) AS GSTAMT, " +
                                " (BILL.TOTADDDISCAMT) AS OTHERDISC," +
                                " BILL.NETAMOUNT," +
                                " 'CUSTOMER CREDIT' AS SETLETYPE,'' AS OTHERPAYMENTBY,(BILL.NETAMOUNT - SETTINFO.CREDITSETLEAMOUNT) AS SETLEAMOUNT" +
                                " FROM BILL" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID) " +
                                " LEFT JOIN ( SELECT BILLDTL.BILLRID,((SUM(ISNULL(BILLDTL.IAMT,0))) * -1) AS COUPONDISC  " +
                                                " FROM BILLDTL  " +
                                                " WHERE ISNULL(BILLDTL.DELFLG,0)=0 AND ISNULL(BILLDTL.IQTY,0)<0 " +
                                                " GROUP BY BILLDTL.BILLRID " +
                                            " ) BILLDTL ON (BILLDTL.BILLRID=BILL.RID) " +
                                " LEFT JOIN	( " +
                                                " SELECT SETTLEMENT.BILLRID,SUM(SETTLEMENT.SETLEAMOUNT) AS CREDITSETLEAMOUNT	" +
                                                " FROM SETTLEMENT" +
                                                " WHERE ISNULL(SETTLEMENT.DELFLG,0)=0 " +
                                                " GROUP BY SETTLEMENT.BILLRID" +
                                            " ) AS SETTINFO ON (SETTINFO.BILLRID = BILL.RID)" +
                                " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILL.ISREVISEDBILL,0)=0" +
                                " AND (BILL.NETAMOUNT - SETTINFO.CREDITSETLEAMOUNT)>0" +
                                " AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                " END";

                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("sp_OUTPUTVATREPORT");
                strproc = " CREATE PROCEDURE sp_OUTPUTVATREPORT " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN" +
                                " SELECT BILL.BILLDATE,COUNT(BILL.RID) AS TOTBILL," +
                                " SUM(BILL.NETAMOUNT + BILL.TOTADDDISCAMT) AS NETAMOUNT," +
                                " MIN(ISNULL(GSTINFO.GSTPER,0)) AS GSTPER," +
                                " AVG(ISNULL(GSTINFO.SALEGST,0)) AS SALEGST," +
                                " AVG(ISNULL(GSTINFO.GSTAMT,0)) AS GSTAMT," +
                                " MIN(ISNULL(SERTAXINFO.SERTAXPER,0)) AS SERTAXPER," +
                                " AVG(ISNULL(SERTAXINFO.SALESERTAX,0)) AS SALESERTAX," +
                                " AVG(ISNULL(SERTAXINFO.SERTAXAMT,0)) AS SERTAXAMT," +
                                " MIN(ISNULL(SBCESSINFO.SERCHRPER,0)) AS SBCESSTAXPER, " +
                                " AVG(ISNULL(SBCESSINFO.SALESBCESS,0)) AS SALESBCESS, " +
                                " AVG(ISNULL(SBCESSINFO.SBCESSAMT,0)) AS SBCESSAMT, " +
                                " MIN(ISNULL(KKCESSINFO.KKCESSPER,0)) AS KKCESSTAXPER," +
                                " AVG(ISNULL(KKCESSINFO.SALEKKCESS,0)) AS SALEKKCESS," +
                                " AVG(ISNULL(KKCESSINFO.KKCESSAMT,0)) AS KKCESSAMT," +
                                " MIN(ISNULL(FOODINFO.FOODVATPER,0)) AS FOODVATPER," +
                                " AVG(ISNULL(FOODINFO.SALEFOOD,0)) AS SALEFOOD,AVG(ISNULL(FOODINFO.FOODVAT,0)) AS FOODVAT," +
                                " MIN(ISNULL(BEVINFO.BEVVATPER,0)) AS BEVVATPER," +
                                " AVG(ISNULL(BEVINFO.SALEBEV,0)) AS SALEBEV,AVG(ISNULL(BEVINFO.BEVVAT,0)) AS BEVVAT," +
                                " MIN(ISNULL(LIQINFO.LIQVATPER,0)) AS LIQVATPER," +
                                " AVG(ISNULL(LIQINFO.SALELIQ,0)) AS SALELIQ,AVG(ISNULL(LIQINFO.LIQVAT,0)) AS LIQVAT" +
                                " FROM BILL" +
                                " LEFT JOIN (" +
                                    " SELECT BILL.BILLDATE,BILLDTL.FOODVATPER,(SUM(BILLDTL.IAMT) - SUM(ISNULL(BILLDTL.DISCAMT,0))) AS SALEFOOD,SUM(BILLDTL.FOODVATAMT)FOODVAT FROM BILL" +
                                    " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID=BILL.RID)" +
                                    " WHERE isnull(bill.delflg,0)=0 and isnull(billdtl.delflg,0)=0 and  isnull(bill.ISREVISEDBILL,0)=0" +
                                    " AND BILLDTL.SERTAXTYPE='FOOD' AND FOODVATPER>0" +
                                    " AND bill.billDATE between @p_fromdate and @p_todate " +
                                    " GROUP BY BILLDTL.FOODVATPER,BILLDTL.SERTAXTYPE,BILL.BILLDATE" +
                                        " ) FOODINFO ON (FOODINFO.BILLDATE=BILL.BILLDATE) " +
                                " LEFT JOIN (" +
                                " SELECT BILL.BILLDATE,BILLDTL.BEVVATPER,(SUM(BILLDTL.IAMT) - SUM(ISNULL(BILLDTL.DISCAMT,0))) AS SALEBEV,SUM(BILLDTL.BEVVATAMT)BEVVAT FROM BILL" +
                                " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID=BILL.RID)" +
                                " WHERE isnull(bill.delflg,0)=0 and isnull(billdtl.delflg,0)=0 and  isnull(bill.ISREVISEDBILL,0)=0" +
                                " AND BILLDTL.SERTAXTYPE='BEVERAGES' AND BILLDTL.BEVVATPER>0" +
                                " AND bill.billDATE between @p_fromdate and @p_todate " +
                                " GROUP BY BILLDTL.BEVVATPER,BILLDTL.SERTAXTYPE,BILL.BILLDATE" +
                                " ) BEVINFO ON (BEVINFO.BILLDATE=BILL.BILLDATE) " +
                                  " LEFT JOIN ( " +
                                            " SELECT BILL.BILLDATE,BILLDTL.LIQVATPER,(SUM(BILLDTL.IAMT) - SUM(ISNULL(BILLDTL.DISCAMT,0))) AS SALELIQ,SUM(BILLDTL.LIQVATAMT)LIQVAT FROM BILL" +
                                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID=BILL.RID)" +
                                            " WHERE isnull(bill.delflg,0)=0 and isnull(billdtl.delflg,0)=0 and  isnull(bill.ISREVISEDBILL,0)=0" +
                                            " AND BILLDTL.SERTAXTYPE='LIQUER' AND BILLDTL.LIQVATPER>0 " +
                                            " AND bill.billDATE between @p_fromdate and @p_todate " +
                                            " GROUP BY BILLDTL.LIQVATPER,BILLDTL.SERTAXTYPE,BILL.BILLDATE" +
                                            " ) LIQINFO ON (LIQINFO.BILLDATE=BILL.BILLDATE) " +
                                " LEFT JOIN ( " +
                                            " SELECT BILL.BILLDATE,BILLDTL.SERTAXPER," +
                                            " (SUM((BILLDTL.IAMT) + (BILLDTL.NEWSERCHRAMT)) - SUM(ISNULL(BILLDTL.DISCAMT,0))) AS SALESERTAX," +
                                            " SUM(BILLDTL.SERTAXAMT) AS SERTAXAMT" +
                                            " FROM BILL" +
                                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID=BILL.RID)" +
                                            " WHERE isnull(bill.delflg,0)=0 and isnull(billdtl.delflg,0)=0 and  isnull(bill.ISREVISEDBILL,0)=0" +
                                            " AND BILLDTL.SERTAXPER>0 " +
                                            " AND bill.billDATE between @p_fromdate and @p_todate " +
                                            " GROUP BY BILLDTL.SERTAXPER,BILL.BILLDATE	" +
                                            " )SERTAXINFO ON (SERTAXINFO.BILLDATE=BILL.BILLDATE) " +
                                " LEFT JOIN (" +
                                            " SELECT BILL.BILLDATE,BILLDTL.SERCHRPER," +
                                            " (SUM((BILLDTL.IAMT) + (BILLDTL.NEWSERCHRAMT)) - SUM(ISNULL(BILLDTL.DISCAMT,0))) AS SALESBCESS," +
                                            " SUM(BILLDTL.SERCHRAMT) AS SBCESSAMT " +
                                            " FROM BILL " +
                                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID=BILL.RID)" +
                                            " WHERE isnull(BILL.delflg,0)=0 and isnull(BILLDTL.delflg,0)=0 and  isnull(BILL.ISREVISEDBILL,0)=0" +
                                            " AND BILLDTL.SERCHRPER>0 " +
                                            " AND bill.billDATE between @p_fromdate and @p_todate " +
                                            " GROUP BY BILLDTL.SERCHRPER,BILL.BILLDATE " +
                                            " )SBCESSINFO ON (SBCESSINFO.BILLDATE=BILL.BILLDATE) " +
                                " LEFT JOIN ( " +
                                            " SELECT BILL.BILLDATE,BILLDTL.KKCESSPER," +
                                            " (SUM((BILLDTL.IAMT) + (BILLDTL.NEWSERCHRAMT)) - SUM(ISNULL(BILLDTL.DISCAMT,0))) AS SALEKKCESS," +
                                            " SUM(BILLDTL.KKCESSAMT) AS KKCESSAMT " +
                                            " FROM BILL" +
                                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID=BILL.RID) " +
                                            " WHERE isnull(BILL.delflg,0)=0 and isnull(BILLDTL.delflg,0)=0 and  isnull(BILL.ISREVISEDBILL,0)=0" +
                                            " AND BILLDTL.KKCESSPER>0 " +
                                            " AND bill.billDATE between @p_fromdate and @p_todate " +
                                            " GROUP BY BILLDTL.KKCESSPER,BILL.BILLDATE " +
                                            " )KKCESSINFO ON (KKCESSINFO.BILLDATE=BILL.BILLDATE) " +
                               " LEFT JOIN ( " +
                                            " SELECT BILL.BILLDATE,BILLDTL.GSTPER," +
                                            " (SUM((BILLDTL.IAMT) + (BILLDTL.NEWSERCHRAMT)) - SUM(ISNULL(BILLDTL.DISCAMT,0))) AS SALEGST," +
                                            " SUM(BILLDTL.GSTAMT) AS GSTAMT " +
                                            " FROM BILL" +
                                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID=BILL.RID) " +
                                            " WHERE isnull(BILL.delflg,0)=0 and isnull(BILLDTL.delflg,0)=0 and  isnull(BILL.ISREVISEDBILL,0)=0" +
                                            " AND BILLDTL.GSTPER>0 " +
                                            " AND bill.billDATE between @p_fromdate and @p_todate " +
                                            " GROUP BY BILLDTL.GSTPER,BILL.BILLDATE " +
                                            " ) GSTINFO ON (GSTINFO.BILLDATE=BILL.BILLDATE) " +
                                " WHERE bill.billDATE between @p_fromdate and @p_todate AND isnull(BILL.Delflg,0)=0 and isnull(bill.ISREVISEDBILL,0)=0" +
                                " GROUP BY BILL.BILLDATE" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_PURCHASESTOCKREG");
                strproc = " CREATE PROCEDURE SP_PURCHASESTOCKREG" +
                            " (@p_fromdate datetime,@p_todate datetime)" +
                            " as begin  " +
                            " SELECT MSTPURITEM.RID,MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT," +
                            " ISNULL(OPPURDTL.TOTQTY,0) AS OPPURQTY," +
                            " ISNULL(OPISSUEDTL.TOTQTY,0) AS OPISSQTY," +
                            " ISNULL(OPWASTAGEDTL.TOTQTY,0) AS OPWASTAGEQTY," +
                            " ISNULL(OPUSAGEDTL.TOTQTY,0) AS OPUSAGEQTY," +
                            " (" +
                            " ISNULL(OPPURDTL.TOTQTY,0) - " +
                            " (ISNULL(OPWASTAGEDTL.TOTQTY,0) + ISNULL(OPUSAGEDTL.TOTQTY,0))" +
                            " ) AS OPSTOCKQTY," +
                            " ISNULL(PURDTL.TOTQTY,0) AS PURQTY," +
                            " ISNULL(ISSUEDTL.TOTQTY,0) AS ISSQTY," +
                            " ISNULL(WASTAGEDTL.TOTQTY,0) AS WASTAGEQTY," +
                            " ISNULL(USAGEDTL.TOTQTY,0) AS USAGEQTY," +
                            " (" +
                            " ISNULL(PURDTL.TOTQTY,0) - " +
                            " (ISNULL(WASTAGEDTL.TOTQTY,0) + ISNULL(USAGEDTL.TOTQTY,0))" +
                            " ) AS STOCKQTY," +
                            " (" +
                            " (ISNULL(OPPURDTL.TOTQTY,0) + ISNULL(PURDTL.TOTQTY,0)) -" +
                            " ( " +
                                " (ISNULL(OPWASTAGEDTL.TOTQTY,0) + ISNULL(WASTAGEDTL.TOTQTY,0)) " +
                                        " + " +
                                " (ISNULL(OPUSAGEDTL.TOTQTY,0)  +  ISNULL(USAGEDTL.TOTQTY,0))" +
                            " )" +
                            " ) AS CLSTOCKQTY," +
                            " (" +
                            " ISNULL(OPISSUEDTL.TOTQTY,0) + ISNULL(ISSUEDTL.TOTQTY,0)" +
                            " ) AS CLISSUEQTY" +
                            " FROM MSTPURITEM" +
                            " LEFT JOIN (" +
                                        " SELECT  ITEMPURCHASEDTL.IRID,SUM(ISNULL(ITEMPURCHASEDTL.IQTY,0)) AS TOTQTY, " +
                                         " SUM(ISNULL(ITEMPURCHASEDTL.IAMT,0)) AS TOTAMT " +
                                         " FROM ITEMPURCHASE " +
                                         " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID) " +
                                         " WHERE  " +
                                            " ITEMPURCHASE.PURDATE < @p_fromdate and  " +
                                         " isnull(ITEMPURCHASE.delflg,0)=0 and isnull(ITEMPURCHASEDTL.delflg,0)=0 " +
                                         " GROUP BY ITEMPURCHASEDTL.IRID " +
                                       " ) OPPURDTL ON (OPPURDTL.IRID = MSTPURITEM.RID) " +
                            " LEFT JOIN ( " +
                                         " SELECT STOCKISSUEDTL.IRID,(SUM(ISNULL(STOCKISSUEDTL.IQTY,0))) AS TOTQTY, " +
                                         " CAST(CAST(SUM(ISNULL(STOCKISSUEDTL.IQTY,0) * ISNULL(STOCKISSUEDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                         " FROM STOCKISSUE " +
                                         " LEFT JOIN STOCKISSUEDTL ON (STOCKISSUEDTL.ISSUERID=STOCKISSUE.RID) " +
                                         " WHERE " +
                                            " STOCKISSUE.ISSUEDATE < @p_fromdate and " +
                                            " isnull(STOCKISSUE.delflg,0)=0 AND isnull(STOCKISSUEDTL.delflg,0)=0 " +
                                          " GROUP BY STOCKISSUEDTL.IRID " +
                                          " ) OPISSUEDTL ON (OPISSUEDTL.IRID = MSTPURITEM.RID) " +
                            " LEFT JOIN ( " +
                                         " SELECT STOCKWASTAGEDTL.IRID,(SUM(ISNULL(STOCKWASTAGEDTL.IQTY,0))) AS TOTQTY, " +
                                         " CAST(CAST(SUM(ISNULL(STOCKWASTAGEDTL.IQTY,0) * ISNULL(STOCKWASTAGEDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                         " FROM STOCKWASTAGE " +
                                         " LEFT JOIN STOCKWASTAGEDTL ON (STOCKWASTAGEDTL.WSTRID=STOCKWASTAGE.RID) " +
                                         " WHERE " +
                                            " STOCKWASTAGE.WSTDATE < @p_fromdate and " +
                                            " isnull(STOCKWASTAGE.delflg,0)=0 AND isnull(STOCKWASTAGEDTL.delflg,0)=0 " +
                                          " GROUP BY STOCKWASTAGEDTL.IRID " +
                                          " ) OPWASTAGEDTL ON (OPWASTAGEDTL.IRID = MSTPURITEM.RID) " +
                            " LEFT JOIN ( " +
                                         " SELECT (ITEMWISEPURCHASEBASEINFO.PURITEMNM1RID) AS IRID,(SUM(ISNULL(ITEMWISEPURCHASEBASEINFO.OPGRAM,0))) AS TOTQTY, " +
                                         " CAST(CAST(SUM(ISNULL(ITEMWISEPURCHASEBASEINFO.OPGRAM,0) * 1) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                         " FROM ITEMWISEPURCHASEBASEINFO " +
                                         " WHERE ITEMWISEPURCHASEBASEINFO.BILLDATE < @p_fromdate " +
                                          " GROUP BY ITEMWISEPURCHASEBASEINFO.PURITEMNM1RID " +
                                          " ) OPUSAGEDTL ON (OPUSAGEDTL.IRID = MSTPURITEM.RID) " +
                            " LEFT JOIN ( " +
                                        " SELECT  ITEMPURCHASEDTL.IRID,SUM(ISNULL(ITEMPURCHASEDTL.IQTY,0)) AS TOTQTY," +
                                         " SUM(ISNULL(ITEMPURCHASEDTL.IAMT,0)) AS TOTAMT " +
                                         " FROM ITEMPURCHASE " +
                                         " LEFT JOIN ITEMPURCHASEDTL ON (ITEMPURCHASEDTL.PURRID=ITEMPURCHASE.RID) " +
                                         " WHERE  " +
                                            " ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate and  " +
                                         " isnull(ITEMPURCHASE.delflg,0)=0 and isnull(ITEMPURCHASEDTL.delflg,0)=0 " +
                                         " GROUP BY ITEMPURCHASEDTL.IRID " +
                                       " ) PURDTL ON (PURDTL.IRID = MSTPURITEM.RID) " +
                            " LEFT JOIN ( " +
                                         " SELECT STOCKISSUEDTL.IRID,(SUM(ISNULL(STOCKISSUEDTL.IQTY,0))) AS TOTQTY, " +
                                         " CAST(CAST(SUM(ISNULL(STOCKISSUEDTL.IQTY,0) * ISNULL(STOCKISSUEDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                         " FROM STOCKISSUE " +
                                         " LEFT JOIN STOCKISSUEDTL ON (STOCKISSUEDTL.ISSUERID=STOCKISSUE.RID) " +
                                         " WHERE " +
                                            " STOCKISSUE.ISSUEDATE between @p_fromdate and @p_todate and " +
                                            " isnull(STOCKISSUE.delflg,0)=0 AND isnull(STOCKISSUEDTL.delflg,0)=0 " +
                                          " GROUP BY STOCKISSUEDTL.IRID " +
                                          " ) ISSUEDTL ON (ISSUEDTL.IRID = MSTPURITEM.RID) " +
                            " LEFT JOIN ( " +
                                         " SELECT STOCKWASTAGEDTL.IRID,(SUM(ISNULL(STOCKWASTAGEDTL.IQTY,0))) AS TOTQTY, " +
                                         " CAST(CAST(SUM(ISNULL(STOCKWASTAGEDTL.IQTY,0) * ISNULL(STOCKWASTAGEDTL.IRATE,0)) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                         " FROM STOCKWASTAGE " +
                                         " LEFT JOIN STOCKWASTAGEDTL ON (STOCKWASTAGEDTL.WSTRID=STOCKWASTAGE.RID) " +
                                         " WHERE " +
                                            " STOCKWASTAGE.WSTDATE between @p_fromdate and @p_todate and " +
                                            " isnull(STOCKWASTAGE.delflg,0)=0 AND isnull(STOCKWASTAGEDTL.delflg,0)=0 " +
                                          " GROUP BY STOCKWASTAGEDTL.IRID " +
                                          " ) WASTAGEDTL ON (WASTAGEDTL.IRID = MSTPURITEM.RID) " +
                            " LEFT JOIN ( " +
                                         " SELECT (ITEMWISEPURCHASEBASEINFO.PURITEMNM1RID) AS IRID,(SUM(ISNULL(ITEMWISEPURCHASEBASEINFO.OPGRAM,0))) AS TOTQTY, " +
                                         " CAST(CAST(SUM(ISNULL(ITEMWISEPURCHASEBASEINFO.OPGRAM,0) * 1) AS FLOAT) AS decimal(16,2)) AS TOTAMT " +
                                         " FROM ITEMWISEPURCHASEBASEINFO " +
                                         " WHERE ITEMWISEPURCHASEBASEINFO.BILLDATE between @p_fromdate and @p_todate " +
                                         " GROUP BY ITEMWISEPURCHASEBASEINFO.PURITEMNM1RID" +
                                          " ) USAGEDTL ON (USAGEDTL.IRID = MSTPURITEM.RID)" +
                                    " WHERE ISNULL(MSTPURITEM.DELFLG,0)=0" +
                                    " ORDER BY MSTPURITEM.PURINAME,MSTPURITEM.PURIUNIT,MSTPURITEM.RID" +
                                    " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_TEMPBILLREMARK");
                strproc = "CREATE PROCEDURE SP_TEMPBILLREMARK" +
                            " (" +
                            " @p_mode as int," +
                            " @p_billrid as bigint," +
                            " @p_irid as bigint," +
                            " @p_iname nvarchar(500)," +
                            " @p_iprint nvarchar(max)" +
                            " )as " +
                            " begin" +
                            " INSERT INTO TEMPBILLREMARK (BILLRID,IRID,INAME,IPRINT) " +
                            " VALUES (@p_billrid,@p_irid,@p_iname,@p_iprint)" +
                            " end ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_PRINTBILLREMARK");
                strproc = " CREATE PROCEDURE SP_PRINTBILLREMARK " +
                          " ( @p_billrid1 bigint " +
                          " ) " +
                          " as Begin  " +
                          " SELECT RID,BILLRID,IRID,INAME,IPRINT FROM TEMPBILLREMARK WHERE BILLRID = @p_billrid1" +
                          " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_TBLWAIT");
                strproc = " CREATE procedure sp_TBLWAIT " +
                          " ( " +
                          " @p_mode as int, " +
                          " @p_rid as bigint," +
                          " @p_waitno nvarchar(100)," +
                          " @p_waitdate datetime,	" +
                          " @p_waittime datetime,	" +
                          " @p_custrid bigint," +
                          " @p_pax bigint," +
                          " @p_waitremark nvarchar(500)," +
                          " @p_waitdesc nvarchar(max)," +
                          " @p_entryby nvarchar(50)," +
                          " @p_qno bigint," +
                          " @p_waitstatus nvarchar(50)," +
                          " @p_smsstatus nvarchar(50)," +
                          " @p_isreadyforsms bit," +
                          " @p_userid bigint, " +
                          " @p_errstr as nvarchar(max) out, " +
                          " @p_retval as int out," +
                          " @p_id as bigint out" +
                          " ) as " +
                          " begin " +
                          " try " +
                             " begin " +
                                 " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                 " if (@p_mode=0) " +
                                     " begin 		" +
                                         " Insert Into TBLWAIT (WAITNO,WAITDATE,WAITTIME,CUSTRID,PAX,WAITREMARK,WAITDESC,ENTRYBY,QNO,WAITSTATUS,SMSSTATUS,ISREADYFORSMS," +
                                                           " auserid,adatetime,DelFlg) " +
                                                   " Values (" +
                                                         "   @p_WAITNO,@p_WAITDATE,@p_WAITTIME,@p_CUSTRID,@p_PAX,@p_WAITREMARK,@p_WAITDESC,@p_ENTRYBY,@p_QNO,@p_WAITSTATUS,@p_SMSSTATUS,@p_ISREADYFORSMS," +
                                                           " @p_userid,getdate(),0" +
                                                            " )" +
                                         " set @p_id=SCOPE_IDENTITY()" +
                                         " End   " +
                                  " else if (@p_mode=1)      " +
                                    "  begin" +
                                       "  set @p_Errstr=''  set @p_Retval=0 set @p_id=0" +
                                         " update TBLWAIT " +
                                         " set WAITNO=@p_WAITNO,WAITDATE=@p_WAITDATE,WAITTIME=@p_WAITTIME,CUSTRID=@p_CUSTRID,PAX=@p_PAX,WAITREMARK=@p_WAITREMARK,WAITDESC=@p_WAITDESC,ENTRYBY=@p_ENTRYBY,QNO=@p_QNO,WAITSTATUS=@p_WAITSTATUS,SMSSTATUS=@p_SMSSTATUS,ISREADYFORSMS=@p_ISREADYFORSMS," +
                                                           "  euserid = @p_userid,edatetime = getdate()    " +
                                                           "  where rid = @p_rid  " +
                                      " End " +
                                   " End" +
                                 " end	" +
                                 " try  " +
                                     " begin catch  " +
                                       " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;    " +
                                       " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                       " Return  " +
                                       " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTEXPENCES");
                strproc = "CREATE PROCEDURE sp_MSTEXPENCES " +
                             " (@p_mode as int, @p_rid bigint, " +
                              " @p_excode nvarchar(50),@p_exname nvarchar(500), " +
                              " @p_exremark nvarchar(500),@p_exdesc nvarchar(max)," +
                              " @p_isopecost bit," +
                              " @p_isfuelcost bit," +
                              " @p_userid bigint,  " +
                              " @p_errstr as nvarchar(max) out,  " +
                              " @p_retval as int out, " +
                              " @p_id as bigint out  " +
                              " ) as  " +
                              " begin " +
                              " try " +
                              " begin " +
                              " if (@p_mode=0) " +
                              " begin  " +
                              " declare @codeRowCount as int  " +
                              " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                              " select @codeRowCount = (select count(*) from MSTEXPENCES where EXCODE = @p_EXCODE and ISNULL(DelFlg,0)=0)  " +
                              " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'  " +
                              " Return  " +
                              " End	 " +
                              " Begin  " +
                              " SET NOCOUNT ON " +
                              " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                              " select  @nameRowCount = (select count(*) from MSTEXPENCES where EXNAME = @p_EXNAME and ISNULL(DelFlg,0)=0)   " +
                              " if ( @nameRowCount > 0)  " +
                              " begin    " +
                              " set @p_Retval = 1 set @p_Errstr ='Name Already exits.'   " +
                              " Return " +
                              " End  " +
                              " end " +
                              " begin " +
                              " Insert Into MSTEXPENCES (EXCODE,EXNAME,EXREMARK,EXDESC,isopecost,isfuelcost,senddata,auserid,adatetime,DelFlg) " +
                                               " Values (@p_EXCODE,@p_EXNAME,@p_EXREMARK,@p_EXDESC,@p_isopecost,@p_isfuelcost,0,@p_userid,getdate(),0) " +
                              " set @p_id=SCOPE_IDENTITY()	" +
                              " End  End  " +
                              " else if (@p_mode=1) " +
                              " begin   " +
                              " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0 " +
                              " select @codeRowCount1 = (select count(*) from MSTEXPENCES where EXCODE = @p_EXCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                              " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'" +
                              " Return   End  " +
                              " Begin  " +
                              " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0 " +
                              " select  @nameRowCount1 = (select count(*) from MSTEXPENCES where EXNAME = @p_EXNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0)" +
                              " if ( @nameRowCount1 > 0)" +
                              " begin   " +
                              " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                              " Return  " +
                              " End  END " +
                              " begin " +
                              " Update MSTEXPENCES set EXCODE=@p_EXCODE, " +
                              " EXNAME = @p_EXNAME, EXREMARK = @p_EXREMARK, " +
                              " EXDESC=@p_EXDESC,isopecost=@p_isopecost,isfuelcost=@p_isfuelcost," +
                              " senddata=0,euserid = @p_userid,edatetime = getdate() " +
                              " where rid = @p_rid   " +
                              " End  End  End  " +
                              " End  " +
                              " try  begin catch  " +
                              " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage; " +
                              " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                              " Return  END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTINCOME");
                strproc = "CREATE PROCEDURE sp_MSTINCOME " +
                            " (@p_mode as int, @p_rid bigint," +
                              " @p_incode nvarchar(50),@p_inname nvarchar(500), " +
                              " @p_inremark nvarchar(500),@p_indesc nvarchar(max)," +
                              " @p_userid bigint,  " +
                              " @p_errstr as nvarchar(max) out,  " +
                              " @p_retval as int out, " +
                              " @p_id as bigint out  " +
                              " ) as  " +
                              " begin " +
                              " try " +
                              " begin " +
                              " if (@p_mode=0) " +
                              " begin  " +
                              " declare @codeRowCount as int  " +
                              " set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                              " select @codeRowCount = (select count(*) from MSTINCOME where inCODE = @p_inCODE and ISNULL(DelFlg,0)=0)  " +
                              " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'  " +
                              " Return  " +
                              " End	 " +
                              " Begin  " +
                              " SET NOCOUNT ON " +
                              " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0 " +
                              " select  @nameRowCount = (select count(*) from MSTINCOME where inNAME = @p_inNAME and ISNULL(DelFlg,0)=0)   " +
                              " if ( @nameRowCount > 0)   " +
                              " begin    " +
                              " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                              " Return  " +
                              " End   " +
                              " end " +
                              " begin " +
                              " Insert Into MSTINCOME (inCODE,inNAME,inREMARK,inDESC,SENDDATA,auserid,adatetime,DelFlg) " +
                                               " Values (@p_inCODE,@p_inNAME,@p_inREMARK,@p_inDESC,0,@p_userid,getdate(),0) " +
                              " set @p_id=SCOPE_IDENTITY()	" +
                              " End  End  " +
                              " else if (@p_mode=1) " +
                              " begin   " +
                              " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0 " +
                              " select @codeRowCount1 = (select count(*) from MSTINCOME where inCODE = @p_inCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                              " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'" +
                              " Return   End  " +
                              " Begin  " +
                              " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0  " +
                              " select  @nameRowCount1 = (select count(*) from MSTINCOME where inNAME = @p_inNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0)  " +
                              " if ( @nameRowCount1 > 0) " +
                              " begin   " +
                              " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                              " Return  " +
                              " End  END " +
                              " begin  " +
                              " Update MSTINCOME set inCODE=@p_inCODE, " +
                              " inNAME = @p_inNAME, inREMARK = @p_inREMARK," +
                              " inDESC=@p_inDESC,SENDDATA=0," +
                              " euserid = @p_userid,edatetime = getdate() " +
                              " where rid = @p_rid   " +
                              " End  End  End  " +
                              " End  " +
                              " try  begin catch  " +
                              " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                              " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                              " Return  END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_INCOME");
                strproc = " CREATE PROCEDURE SP_INCOME" +
                                 " (" +
                                  " @p_mode as int," +
                                  " @p_rid bigint," +
                                  " @p_incomeno nvarchar(200)," +
                                  " @p_inrid bigint," +
                                  " @p_indate datetime," +
                                  " @p_intime datetime," +
                                  " @p_intype nvarchar(200)," +
                                  " @p_inamount decimal(18,3)," +
                                  " @p_inpername nvarchar(200)," +
                                  " @p_incontno nvarchar(200)," +
                                  " @p_remark1 nvarchar(500)," +
                                  " @p_remark2 nvarchar(500)," +
                                  " @p_remark3 nvarchar(500)," +
                                  " @p_indesc nvarchar(max)," +
                                  " @p_userid bigint,  " +
                                  " @p_errstr as nvarchar(max) out, " +
                                  " @p_retval as int out, " +
                                  " @p_id as bigint out " +
                                  " ) as " +
                                  " Begin" +
                                  " try" +
                                  " begin" +
                                    " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                  " if (@p_mode=0) " +
                                  " begin" +
                                          " insert into INCOME (INCOMENO,INDATE,INTIME,INRID,INTYPE,INAMOUNT,INPERNAME,INCONTNO,REMARK1,REMARK2,REMARK3,INDESC," +
                                                     " auserid,adatetime,DelFlg)" +
                                          " values (@p_INCOMENO,@p_INDATE,@p_INTIME,@p_INRID,@p_INTYPE,@p_INAMOUNT,@p_INPERNAME,@p_INCONTNO,@p_REMARK1,@p_REMARK2,@p_REMARK3,@p_INDESC," +
                                                "  @p_userid,getdate(),0)" +
                                      " set @p_id=SCOPE_IDENTITY()" +
                                      " End" +
                                  " else if (@p_mode=1) " +
                                    "   begin" +
                                  " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                          " update INCOME set INCOMENO=@p_INCOMENO,INDATE=@p_INDATE,INTIME=@p_INTIME,INRID=@p_INRID,INTYPE=@p_INTYPE," +
                                            " INAMOUNT=@p_INAMOUNT,INPERNAME=@p_INPERNAME,INCONTNO=@p_INCONTNO,REMARK1=@p_REMARK1,REMARK2=@p_REMARK2," +
                                            " REMARK3=@p_REMARK3,INDESC=@p_INDESC," +
                                          " euserid = @p_userid,edatetime = getdate()" +
                                          " where rid = @p_rid   " +
                                      " End " +
                                  " End " +
                                  " end " +
                                  " try  " +
                                     "  begin catch" +
                                     "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage" +
                                      "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                      " Return  " +
                                      " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_EXPENCE");
                strproc = " CREATE PROCEDURE SP_EXPENCE" +
                                 " (" +
                                  " @p_mode as int," +
                                  " @p_rid bigint," +
                                  " @p_expenceno nvarchar(200)," +
                                  " @p_exrid bigint," +
                                  " @p_exdate datetime," +
                                  " @p_extime datetime," +
                                  " @p_extype nvarchar(200)," +
                                  " @p_examount decimal(18,3)," +
                                  " @p_expername nvarchar(200)," +
                                  " @p_excontno nvarchar(200)," +
                                  " @p_remark1 nvarchar(500)," +
                                  " @p_remark2 nvarchar(500)," +
                                  " @p_remark3 nvarchar(500)," +
                                  " @p_exdesc nvarchar(max)," +
                                  " @p_userid bigint,  " +
                                  " @p_errstr as nvarchar(max) out, " +
                                  " @p_retval as int out, " +
                                  " @p_id as bigint out " +
                                  " ) as " +
                                  " Begin" +
                                  " try" +
                                  " begin" +
                                    " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                  " if (@p_mode=0) " +
                                  " begin" +
                                          " insert into EXPENCE (EXPENCENO,EXDATE,EXTIME,EXRID,EXTYPE,EXAMOUNT,EXPERNAME,EXCONTNO,REMARK1,REMARK2,REMARK3,EXDESC," +
                                                     " auserid,adatetime,DelFlg)" +
                                          " values (@p_EXPENCENO,@p_EXDATE,@p_EXTIME,@p_EXRID,@p_EXTYPE,@p_EXAMOUNT,@p_EXPERNAME,@p_EXCONTNO,@p_REMARK1,@p_REMARK2,@p_REMARK3,@p_EXDESC," +
                                                "  @p_userid,getdate(),0)" +
                                      " set @p_id=SCOPE_IDENTITY()" +
                                      " End" +
                                  " else if (@p_mode=1) " +
                                    "   begin" +
                                  " set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                          " update EXPENCE set EXPENCENO=@p_EXPENCENO,EXDATE=@p_EXDATE,EXTIME=@p_EXTIME,EXRID=@p_EXRID,EXTYPE=@p_EXTYPE," +
                                            " EXAMOUNT=@p_EXAMOUNT,EXPERNAME=@p_EXPERNAME,EXCONTNO=@p_EXCONTNO,REMARK1=@p_REMARK1,REMARK2=@p_REMARK2," +
                                            " REMARK3=@p_REMARK3,EXDESC=@p_EXDESC," +
                                          " euserid = @p_userid,edatetime = getdate()" +
                                          " where rid = @p_rid   " +
                                      " End " +
                                  " End " +
                                  " end " +
                                  " try  " +
                                     "  begin catch" +
                                     "  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage" +
                                      "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 " +
                                      " Return  " +
                                      " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_OTHERSETTLEMENTREG");
                strproc = "CREATE PROCEDURE SP_OTHERSETTLEMENTREG " +
                            " (@p_fromdate datetime,@p_todate datetime)" +
                            " as begin " +
                            " SELECT SETTLEMENT.RID,SETTLEMENT.SETLEDATE,SETTLEMENT.SETLENO,SETTLEMENT.SETLEAMOUNT,BILL.BILLNO,BILL.REFBILLNO," +
                            " SETTLEMENT.OTHERPAYMENTBY,SETTLEMENT.OTHERPAYMENTBYREMARK1,SETTLEMENT.OTHERPAYMENTBYREMARK2 " +
                            " FROM SETTLEMENT" +
                            " LEFT JOIN BILL ON (BILL.RID=SETTLEMENT.BILLRID)" +
                            " WHERE ISNULL(SETTLEMENT.DELFLG,0)=0 AND SETTLEMENT.SETLETYPE='OTHER' " +
                            " AND SETTLEMENT.SETLEDATE BETWEEN @p_fromdate and @p_todate " +
                            " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BASEINCOMEEXPENCESUMMARYRPT");
                strproc = " CREATE PROCEDURE SP_BASEINCOMEEXPENCESUMMARYRPT " +
                                " (@p_fromdate datetime,@p_todate datetime) AS	" +
                                " BEGIN" +
                                " SELECT SUM(TOTEXPENCE.EXPENCEAMT) AS TOTEXP,SUM(TOTINCOME.INCOMEAMT) AS TOTINC,'PL' AS LINK " +
                                " FROM" +
                                " (" +
                                " SELECT SUM(ITEMPURCHASE.NETAMOUNT) AS EXPENCEAMT,'PURCHASE' AS INFO,'PL' AS LINK " +
                                                " FROM ITEMPURCHASE" +
                                                " WHERE isnull(ITEMPURCHASE.DELFLG,0)=0 " +
                                                " AND ITEMPURCHASE.PURDATE  between  @p_fromdate and @p_todate" +
                                                " UNION ALL" +
                                                " SELECT SUM((CASHONHAND.CASHAMT)*-1) AS CASHAMT,'CASH ENTRY' AS INFO,'PL' AS LINK  " +
                                                " FROM CASHONHAND" +
                                                " WHERE ISNULL(CASHONHAND.DELFLG,0)=0 AND CASHONHAND.CASHSTATUS='MINUS'" +
                                                " AND CASHONHAND.CASHDATE  between  @p_fromdate and @p_todate" +
                                                " GROUP BY CASHONHAND.RID" +
                                                " UNION ALL" +
                                                " SELECT SUM(EXPENCE.EXAMOUNT) AS EXPENCEAMT,'EXPENCE' AS INFO,'PL' AS LINK  " +
                                                " FROM EXPENCE" +
                                                " LEFT JOIN MSTEXPENCES ON (MSTEXPENCES.RID=EXPENCE.EXRID)" +
                                                " WHERE ISNULL(EXPENCE.DELFLG,0)=0" +
                                                " AND EXPENCE.EXDATE  between  @p_fromdate and @p_todate" +
                                                " GROUP BY MSTEXPENCES.RID,MSTEXPENCES.EXNAME " +
                                " ) AS TOTEXPENCE" +
                                " LEFT JOIN " +
                                " (" +
                                " SELECT SUM(BILL.NETAMOUNT) AS INCOMEAMT,'PL' AS LINK " +
                                                " FROM BILL" +
                                                " WHERE isnull(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0" +
                                                " AND BILL.BILLDATE  between  @p_fromdate and @p_todate" +
                                                " UNION ALL" +
                                                " SELECT SUM(CASHONHAND.CASHAMT) AS CASHAMT,'PL' AS LINK  " +
                                                " FROM CASHONHAND" +
                                                " WHERE ISNULL(CASHONHAND.DELFLG,0)=0 AND CASHONHAND.CASHSTATUS='PLUS'" +
                                                " AND CASHONHAND.CASHDATE between  @p_fromdate and @p_todate" +
                                                " GROUP BY CASHONHAND.RID" +
                                                " UNION ALL" +
                                                " SELECT SUM(INCOME.INAMOUNT) AS INCOMEAMT,'PL' AS LINK  " +
                                                " FROM INCOME" +
                                                " LEFT JOIN MSTINCOME ON (MSTINCOME.RID=INCOME.INRID)" +
                                                " WHERE ISNULL(INCOME.DELFLG,0)=0" +
                                                " AND INCOME.INDATE  between @p_fromdate and @p_todate" +
                                                " GROUP BY MSTINCOME.RID,MSTINCOME.INNAME" +
                                " ) AS TOTINCOME ON (TOTINCOME.LINK = TOTEXPENCE.LINK)" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);


                this.DeleteProcedureFromOnlineDb("SP_INCOMESUMMARYRPT");
                strproc = " CREATE PROCEDURE SP_INCOMESUMMARYRPT " +
                                " (@p_fromdate datetime,@p_todate datetime) AS	 " +
                                " BEGIN" +
                                " SELECT 0 AS RID,'SALES' AS INNAME,SUM(BILL.NETAMOUNT) AS INCOMEAMT,'SALES' AS INFO,'PL' AS LINK " +
                                                " FROM BILL" +
                                                " WHERE isnull(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0" +
                                                " AND BILL.BILLDATE  between @p_fromdate and @p_todate" +
                                                " UNION ALL" +
                                                " SELECT MSTINCOME.RID,MSTINCOME.INNAME,SUM(INCOME.INAMOUNT) AS INCOMEAMT,'INCOME' AS INFO,'PL' AS LINK  " +
                                                " FROM INCOME" +
                                                " LEFT JOIN MSTINCOME ON (MSTINCOME.RID=INCOME.INRID)" +
                                                " WHERE ISNULL(INCOME.DELFLG,0)=0" +
                                                " AND INCOME.INDATE  between @p_fromdate and @p_todate" +
                                                " GROUP BY MSTINCOME.RID,MSTINCOME.INNAME" +
                                                " UNION ALL" +
                                                " SELECT 0 AS RID,'OTHER INCOME' AS INAME,SUM(CASHONHAND.CASHAMT) AS CASHAMT,'CASH ENTRY' AS INFO,'PL' AS LINK  " +
                                                " FROM CASHONHAND" +
                                                " WHERE ISNULL(CASHONHAND.DELFLG,0)=0 AND CASHONHAND.CASHSTATUS='PLUS'" +
                                                " AND CASHONHAND.CASHDATE  between @p_fromdate and @p_todate" +
                                                " GROUP BY CASHONHAND.RID" +
                                        " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_EXPENCESUMMARYRPT");
                strproc = " CREATE PROCEDURE SP_EXPENCESUMMARYRPT" +
                                " (@p_fromdate datetime,@p_todate datetime) AS	" +
                                " BEGIN" +
                                " SELECT 0 AS RID,'PURCHASE' AS EXNAME,SUM(ITEMPURCHASE.NETAMOUNT) AS EXPENCEAMT,'PURCHASE' AS INFO,'PL' AS LINK " +
                                    " FROM ITEMPURCHASE" +
                                    " WHERE isnull(ITEMPURCHASE.DELFLG,0)=0 " +
                                    " AND ITEMPURCHASE.PURDATE  BETWEEN @p_fromdate and @p_todate" +
                                    " UNION ALL" +
                                    " SELECT MSTEXPENCES.RID,MSTEXPENCES.EXNAME,SUM(EXPENCE.EXAMOUNT) AS EXPENCEAMT,'EXPENCE' AS INFO,'PL' AS LINK " +
                                    " FROM EXPENCE" +
                                    " LEFT JOIN MSTEXPENCES ON (MSTEXPENCES.RID=EXPENCE.EXRID)" +
                                    " WHERE ISNULL(EXPENCE.DELFLG,0)=0" +
                                    " AND EXPENCE.EXDATE  between @p_fromdate and @p_todate" +
                                    " GROUP BY MSTEXPENCES.RID,MSTEXPENCES.EXNAME" +
                                    " UNION ALL" +
                                    " SELECT 0 AS RID,'OTHER EXPENCE' AS INAME,SUM((CASHONHAND.CASHAMT)*-1) AS CASHAMT,'CASH ENTRY' AS INFO,'PL' AS LINK  " +
                                    " FROM CASHONHAND" +
                                    " WHERE ISNULL(CASHONHAND.DELFLG,0)=0 AND CASHONHAND.CASHSTATUS='MINUS'" +
                                    " AND CASHONHAND.CASHDATE  between @p_fromdate and @p_todate" +
                                    " GROUP BY CASHONHAND.RID" +
                                 " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_SETTLEMENTREG");
                strproc = " CREATE procedure sp_SETTLEMENTREG " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " as " +
                                " begin " +
                                " select settlement.rid,settlement.setledate,settlement.setleno, " +
                                " isnull(bill.billno,'')as billno,bill.REFBILLNO,isnull(bill.billtype,'')as billtype, " +
                                " settlement.setletype, " +
                                " settlement.setleamount " +
                                " from settlement " +
                                " left join bill on (bill.rid =settlement.billrid ) " +
                                " WHERE  settlement.setledate between @p_fromdate and @p_todate  " +
                                " and isnull(settlement.delflg,0)=0 " +
                                " order by settlement.setledate,settlement.rid " +
                                " end";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_TABLEWISESALESREPORT");
                strproc = " CREATE PROCEDURE SP_TABLEWISESALESREPORT " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN " +
                                " SELECT ISNULL(MSTTABLE.RID,0) AS RID,ISNULL(MSTTABLE.TABLENAME,'CASH/MEMO') AS TABLENAME, " +
                                " SUM(CASE WHEN SETTLEMENT.SETLETYPE ='CASH' THEN (ISNULL(SETTLEMENT.SETLEAMOUNT,0) + ISNULL(SETTLEMENT.ADJAMT,0))  ELSE 0 END) AS CASHAMT, " +
                                " SUM(CASE WHEN SETTLEMENT.SETLETYPE ='CHEQUE' THEN (ISNULL(SETTLEMENT.SETLEAMOUNT,0) + ISNULL(SETTLEMENT.ADJAMT,0)) ELSE 0 END) AS CHEQUEAMT, " +
                                " SUM(CASE WHEN SETTLEMENT.SETLETYPE ='CREDIT CARD' THEN (ISNULL(SETTLEMENT.SETLEAMOUNT,0) + ISNULL(SETTLEMENT.ADJAMT,0))  ELSE 0 END) AS CREDITCARDAMT, " +
                                " SUM(CASE WHEN SETTLEMENT.SETLETYPE ='COMPLEMENTARY' THEN (ISNULL(SETTLEMENT.SETLEAMOUNT,0) + ISNULL(SETTLEMENT.ADJAMT,0)) ELSE 0 END) AS COMPLEMENTARYAMT, " +
                                " SUM(CASE WHEN SETTLEMENT.SETLETYPE ='ROOM CREDIT' THEN (ISNULL(SETTLEMENT.SETLEAMOUNT,0) + ISNULL(SETTLEMENT.ADJAMT,0))   ELSE 0 END) AS ROOMCREDITAMT, " +
                                " SUM(CASE WHEN SETTLEMENT.SETLETYPE ='OTHER' THEN (ISNULL(SETTLEMENT.SETLEAMOUNT,0) + ISNULL(SETTLEMENT.ADJAMT,0))  ELSE 0 END) AS OTHERAMT " +
                                " FROM BILL  " +
                                " LEFT JOIN MSTTABLE ON (BILL.TABLERID=MSTTABLE.RID) " +
                                " LEFT JOIN SETTLEMENT ON (SETTLEMENT.BILLRID=BILL.RID) " +
                                " WHERE ISNULL(BILL.DELFLG,0)=0  " +
                                      " AND ISNULL(BILL.ISREVISEDBILL,0)=0 " +
                                      " AND ISNULL(SETTLEMENT.DELFLG,0)=0 " +
                                      " AND ISNULL(MSTTABLE.DELFLG,0)=0 " +
                                      " AND BILL.BILLDATE between @p_fromdate and @p_todate " +
                                " GROUP BY MSTTABLE.RID,MSTTABLE.TABLENAME  " +
                                " ORDER BY MSTTABLE.RID,MSTTABLE.TABLENAME  " +
                                " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_CUSTOMERWISEBILLREPORT");
                strproc = " CREATE PROCEDURE SP_CUSTOMERWISEBILLREPORT " +
                            " (@p_fromdate datetime,@p_todate datetime) " +
                            " AS BEGIN " +
                            " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,MSTCUST.RID AS CUSTRID,MSTCUST.CUSTNAME, " +
                            " BILL.BILLDATE,BILL.BILLPAX,BILL.BILLREMARK,BILL.NETAMOUNT,BILL.TOTDISCAMOUNT, " +
                            " BILL.TOTADDDISCAMT, " +
                            " SUM(CASE WHEN ISNULL(BILLDTL.IQTY,0)< 0 THEN (ISNULL(BILLDTL.IAMT,0)*-1) ELSE 0 END) AS 'COUPONDISC' " +
                            " FROM BILL " +
                            " LEFT JOIN BILLDTL ON (BILLDTL.BILLRID = BILL.RID) " +
                            " LEFT JOIN MSTCUST ON (MSTCUST.RID = BILL.CUSTRID) " +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILL.ISREVISEDBILL,0)=0 " +
                            " AND BILL.BILLDATE between @p_fromdate and @p_todate " +
                            " AND BILL.CUSTRID >0 " +
                            " GROUP BY BILL.RID,BILL.BILLNO,BILL.REFBILLNO,MSTCUST.RID,MSTCUST.CUSTNAME,BILL.BILLDATE,BILL.BILLPAX,BILL.BILLREMARK,BILL.NETAMOUNT,BILL.TOTDISCAMOUNT, " +
                            " BILL.TOTADDDISCAMT " +
                            " order by bill.REFBILLNO,BILL.BILLDATE " +
                            " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_SUPPLIERWISEPENDINGPAYMENTDETAILS");
                strproc = " CREATE procedure SP_SUPPLIERWISEPENDINGPAYMENTDETAILS " +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS  BEGIN" +
                                " SELECT ITEMPURCHASE.RID,ITEMPURCHASE.PURDATE,ITEMPURCHASE.PURNO,ITEMPURCHASE.DOCNO,ITEMPURCHASE.DOCDATE, " +
                                " ITEMPURCHASE.SUPPRID,ITEMPURCHASE.NETAMOUNT, " +
                                " ISNULL(PAYINFO.PAYAMT,0) AS PAYAMT,MSTSUPPLIER.SUPPNAME, " +
                                " (ITEMPURCHASE.NETAMOUNT - ISNULL(PAYINFO.PAYAMT,0)) AS PENDINGAMT " +
                                " FROM ITEMPURCHASE " +
                                " LEFT JOIN  " +
                                " ( " +
                                    " SELECT SUM(ISNULL(PAYMENTINFO.PAYAMOUNT,0)) AS PAYAMT,PAYMENTINFO.PURRID " +
                                    " FROM PAYMENTINFO WHERE ISNULL(PAYMENTINFO.DELFLG,0)=0 " +
                                    " GROUP BY PAYMENTINFO.PURRID " +
                                " ) AS PAYINFO ON (PAYINFO.PURRID = ITEMPURCHASE.RID) " +
                                " LEFT JOIN MSTSUPPLIER ON (MSTSUPPLIER.RID=ITEMPURCHASE.SUPPRID) " +
                                " WHERE ISNULL(ITEMPURCHASE.DELFLG,0)=0 AND ITEMPURCHASE.PURDATE between @p_fromdate and @p_todate" +
                                " AND (ITEMPURCHASE.NETAMOUNT - ISNULL(PAYINFO.PAYAMT,0)) >0" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTHSNCODE");
                strproc = " CREATE procedure sp_MSTHSNCODE " +
                                " (" +
                                " @p_mode as int, @p_rid bigint,@p_hsncode nvarchar(50),@p_hsncoderemark nvarchar(200), " +
                                " @p_actotgstper decimal(18,3),@p_accgstper decimal(18,3),@p_acsgstper decimal(18,3),@p_acigstper decimal(18,3)," +
                                " @p_nactotgstper decimal(18,3),@p_naccgstper decimal(18,3),@p_nacsgstper decimal(18,3),@p_nacigstper decimal(18,3)," +
                                " @p_catotgstper decimal(18,3),@p_cacgstper decimal(18,3), @p_casgstper decimal(18,3),@p_caigstper decimal(18,3),  " +
                                " @p_rstotgstper decimal(18,3),@p_rscgstper decimal(18,3), @p_rssgstper decimal(18,3),@p_rsigstper decimal(18,3),  " +
                                " @p_purtotgstper decimal(18,3),@p_purcgstper decimal(18,3), @p_pursgstper decimal(18,3),@p_purigstper decimal(18,3),  " +
                                " @p_banqtotgstper decimal(18,3),@p_banqcgstper decimal(18,3), @p_banqsgstper decimal(18,3),@p_banqigstper decimal(18,3),  " +
                                " @p_userid bigint, @p_errstr as nvarchar(max) out,  @p_retval as int out,  " +
                                " @p_id as bigint out  " +
                                " ) as  " +
                                " begin   " +
                                " try   " +
                                " begin    " +
                                " if (@p_mode=0)    " +
                                " begin declare @codeRowCount as int  set @p_Errstr=''  set @p_Retval=0 set @p_id=0     " +
                                " select @codeRowCount = (select count(*) from MSTHSNCODE where HSNCODE = @p_HSNCODE and ISNULL(DelFlg,0)=0) " +
                                " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'HSN Code Already exits.'   " +
                                " Return  End" +
                                " Insert Into MSTHSNCODE (HSNCODE,HSNCODEREMARK,ACTOTGSTPER,ACCGSTPER,ACSGSTPER,ACIGSTPER," +
                                                        " NACTOTGSTPER,NACCGSTPER,NACSGSTPER,NACIGSTPER,CATOTGSTPER,CACGSTPER,CASGSTPER,CAIGSTPER," +
                                                        " RSTOTGSTPER,RSCGSTPER,RSSGSTPER,RSIGSTPER,PURTOTGSTPER,PURCGSTPER,PURSGSTPER,PURIGSTPER," +
                                                        " BANQTOTGSTPER,BANQCGSTPER,BANQSGSTPER,BANQIGSTPER,SENDDATA," +
                                                        " auserid,adatetime,DelFlg)    " +
                                " Values (@p_HSNCODE,@p_HSNCODEREMARK,@p_ACTOTGSTPER,@p_ACCGSTPER,@p_ACSGSTPER,@p_ACIGSTPER," +
                                                        " @p_NACTOTGSTPER,@p_NACCGSTPER,@p_NACSGSTPER,@p_NACIGSTPER,@p_CATOTGSTPER,@p_CACGSTPER,@p_CASGSTPER,@p_CAIGSTPER," +
                                                        " @p_RSTOTGSTPER,@p_RSCGSTPER,@p_RSSGSTPER,@p_RSIGSTPER,@p_PURTOTGSTPER,@p_PURCGSTPER,@p_PURSGSTPER,@p_PURIGSTPER," +
                                                        " @p_BANQTOTGSTPER,@p_BANQCGSTPER,@p_BANQSGSTPER,@p_BANQIGSTPER,0," +
                                                        " @p_userid,getdate(),0) 	   " +
                                " set @p_id=SCOPE_IDENTITY()    " +
                                " End   " +
                                " else if (@p_mode=1)      " +
                                " begin declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0 " +
                                " select @codeRowCount = (select count(*) from MSTHSNCODE where HSNCODE = @p_HSNCODE AND rid<>@p_rid and ISNULL(DelFlg,0)=0) " +
                                " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'HSN Code Already exits.'   " +
                                " Return  End" +
                                " Update MSTHSNCODE set " +
                                    " HSNCODE=@p_HSNCODE,HSNCODEREMARK=@p_HSNCODEREMARK,ACTOTGSTPER=@p_ACTOTGSTPER,ACCGSTPER=@p_ACCGSTPER,ACSGSTPER=@p_ACSGSTPER,ACIGSTPER=@p_ACIGSTPER," +
                                    " NACTOTGSTPER=@p_NACTOTGSTPER,NACCGSTPER=@p_NACCGSTPER,NACSGSTPER=@p_NACSGSTPER,NACIGSTPER=@p_NACIGSTPER,CATOTGSTPER=@p_CATOTGSTPER,CACGSTPER=@p_CACGSTPER,CASGSTPER=@p_CASGSTPER,CAIGSTPER=@p_CAIGSTPER," +
                                    " RSTOTGSTPER=@p_RSTOTGSTPER,RSCGSTPER=@p_RSCGSTPER,RSSGSTPER=@p_RSSGSTPER,RSIGSTPER=@p_RSIGSTPER,PURTOTGSTPER=@p_PURTOTGSTPER,PURCGSTPER=@p_PURCGSTPER,PURSGSTPER=@p_PURSGSTPER,PURIGSTPER=@p_PURIGSTPER," +
                                    " BANQTOTGSTPER=@p_BANQTOTGSTPER,BANQCGSTPER=@p_BANQCGSTPER,BANQSGSTPER=@p_BANQSGSTPER,BANQIGSTPER=@p_BANQIGSTPER," +
                                    " SENDDATA=0,euserid = @p_userid,edatetime = getdate() where rid = @p_rid        " +
                                " End   " +
                                " end   " +
                                " end   " +
                                " try  begin catch SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;      " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return   " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_DATEWISEBILLSUMMARY");
                strproc = " CREATE PROCEDURE SP_DATEWISEBILLSUMMARY " +
                            " (@p_fromdate datetime,@p_todate datetime) " +
                            " AS  BEGIN" +
                            " SELECT BILL.BILLDATE,COUNT(BILL.RID) AS TOTBILL," +
                            " MIN(BILL.REFBILLNUM)AS MINREFBILLNO,MAX(BILL.REFBILLNUM) AS MAXREDBILLNO, " +
                            " MIN(CONVERT(int,substring(BILL.BILLNO,CHARINDEX('-', BILL.BILLNO)+1,4))) AS MINBILLNO,MAX(CONVERT(int,substring(BILL.BILLNO,CHARINDEX('-', BILL.BILLNO)+1,4))) AS MAXBILLNO," +
                            " SUM(TOTAMOUNT) AS TOTAMOUNT,SUM(TOTSERCHRAMT) AS TOTSERCHRAMT,SUM(TOTDISCAMOUNT) AS TOTDISCAMOUNT," +
                            " SUM(TOTLIQVATAMT) AS TOTLIQVATAMT,SUM(CGSTAMT) AS CGSTAMT,SUM(SGSTAMT) AS SGSTAMT,SUM(TOTROFF) AS TOTROFF,SUM(TOTADDDISCAMT) as TOTADDDISCAMT,SUM(NETAMOUNT) AS NETAMOUNT" +
                            " FROM BILL" +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0" +
                            " AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                            " GROUP BY BILL.BILLDATE" +
                            " ORDER BY BILL.BILLDATE" +
                            " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BILLREGWITHSETTINFOSUB");
                strproc = " CREATE PROCEDURE SP_BILLREGWITHSETTINFOSUB" +
                            " AS" +
                            " SELECT BILL.RID," +
                            " SETTLEMENT.SETLETYPE,SETTLEMENT.OTHERPAYMENTBY,(SETTLEMENT.PAYAMOUNT) AS ACTPAYAMOUNT, " +
                            " (CASE WHEN SETTLEMENT.SETLETYPE='CUSTOMER CREDIT' THEN BILL.NETAMOUNT ELSE SETTLEMENT.PAYAMOUNT END ) AS PAYAMT " +
                            " FROM BILL  " +
                            " LEFT JOIN ( " +
                                      " SELECT SETTLEMENT.BILLRID,SETTLEMENT.SETLETYPE,ISNULL(SETTLEMENT.OTHERPAYMENTBY,'') AS OTHERPAYMENTBY,SUM(SETTLEMENT.SETLEAMOUNT) AS PAYAMOUNT " +
                                      " FROM SETTLEMENT  " +
                                      " WHERE ISNULL(SETTLEMENT.DELFLG,0)=0 " +
                                      " GROUP BY SETTLEMENT.BILLRID,SETTLEMENT.SETLETYPE,ISNULL(SETTLEMENT.OTHERPAYMENTBY,'')  " +
                                      " ) SETTLEMENT ON (SETTLEMENT.BILLRID=BILL.RID) " +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0 ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BILLREGDTLWITHSETTINFO");
                strproc = " CREATE PROCEDURE SP_BILLREGDTLWITHSETTINFO  " +
                                " (@p_fromdate datetime,@p_todate datetime)" +
                                " AS BEGIN" +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE,BILL.BILLPAX,MSTCUST.CUSTNAME,MSTCUST.GSTNO,MSTTABLE.TABLENAME," +
                                " BILL.TOTROFF,BILL.TOTADDDISCAMT,BILL.NETAMOUNT," +
                                " MSTITEM.INAME,BILLDTL.IQTY,BILLDTL.IRATE,BILLDTL.DISCPER,BILLDTL.DISCAMT," +
                                " BILLDTL.NEWSERCHRPER,BILLDTL.NEWSERCHRAMT,BILLDTL.LIQVATPER,BILLDTL.LIQVATAMT," +
                                " BILLDTL.CGSTPER,BILLDTL.CGSTAMT,BILLDTL.SGSTPER,BILLDTL.SGSTAMT,BILLDTL.IGSTPER,BILLDTL.IGSTAMT,BILLDTL.IAMT" +
                                " FROM BILL" +
                                " LEFT JOIN BILLDTL ON (BILL.RID=BILLDTL.BILLRID)" +
                                " LEFT JOIN MSTITEM ON (MSTITEM.RID=BILLDTL.IRID)" +
                                " LEFT JOIN MSTTABLE ON (MSTTABLE.RID=BILL.TABLERID)" +
                                " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID)" +
                                " WHERE ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0 " +
                                " AND ISNULL(BILLDTL.DELFLG,0)=0" +
                                " AND BILL.BILLDATE between @p_fromdate and @p_todate " +
                                " ORDER BY BILL.RID,BILL.REFBILLNUM,BILL.BILLDATE" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_REGDETAILS");
                strproc = " CREATE PROCEDURE SP_REGDETAILS" +
                                " ( " +
                                " @p_mode as int, " +
                                " @p_rid bigint, " +
                                " @p_regdate datetime, " +
                                " @p_regname nvarchar(1000), " +
                                " @p_ownername nvarchar(1000), " +
                                " @p_contpername nvarchar(1000), " +
                                " @p_contpermob nvarchar(500)," +
                                " @p_proptype nvarchar(500)," +
                                " @p_regno1 nvarchar(1000),  " +
                                " @p_regno2 nvarchar(1000),  " +
                                " @p_regno3 nvarchar(1000),  " +
                                " @p_add1 nvarchar(500),  " +
                                " @p_add2 nvarchar(500),  " +
                                " @p_add3 nvarchar(500),  " +
                                " @p_cityname bigint," +
                                " @p_zipcode nvarchar(100),  " +
                                " @p_staterid bigint," +
                                " @p_countryrid bigint," +
                                " @p_phoneno nvarchar(100),  " +
                                " @p_mobno nvarchar(100)," +
                                " @p_faxno nvarchar(100),   " +
                                " @p_email nvarchar(100),  " +
                                " @p_website nvarchar(100),  " +
                                " @p_regremark nvarchar(max), " +
                                " @p_restbranchnm nvarchar(50), " +
                                " @p_userid bigint,   " +
                                " @p_errstr as nvarchar(max) out,  " +
                                " @p_retval as int out,  " +
                                " @p_id as bigint out  " +
                                " ) as  " +
                                " Begin " +
                                " try " +
                                " begin " +
                                  " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0)  " +
                                " begin " +
                                        " insert into REGDETAILS (REGDATE,REGNAME,OWNERNAME,CONTPERNAME,CONTPERMOB,PROPTYPE,REGNO1,REGNO2,REGNO3,ADD1,ADD2,ADD3," +
                                                                " CITYNAME,ZIPCODE,STATERID,COUNTRYRID,PHONENO,MOBNO,FAXNO,EMAIL,WEBSITE,REGREMARK,restbranchnm," +
                                                                " auserid,adatetime,DelFlg) " +
                                        " values (@p_REGDATE,@p_REGNAME,@p_OWNERNAME,@p_CONTPERNAME,@p_CONTPERMOB,@p_PROPTYPE,@p_REGNO1,@p_REGNO2,@p_REGNO3,@p_ADD1,@p_ADD2,@p_ADD3," +
                                               "  @p_CITYNAME,@p_ZIPCODE,@p_STATERID,@p_COUNTRYRID,@p_PHONENO,@p_MOBNO,@p_FAXNO,@p_EMAIL,@p_WEBSITE,@p_REGREMARK,@p_restbranchnm," +
                                               "  @p_userid,getdate(),0) " +
                                    " set @p_id=SCOPE_IDENTITY() " +
                                    " End " +
                                " else if (@p_mode=1)  " +
                                  "   begin " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                        " UPDATE REGDETAILS SET REGDATE=@p_REGDATE,REGNAME=@p_REGNAME,OWNERNAME=@p_OWNERNAME,CONTPERNAME=@p_CONTPERNAME,CONTPERMOB=@p_CONTPERMOB,PROPTYPE=@p_PROPTYPE," +
                                                            " REGNO1=@p_REGNO1,REGNO2=@p_REGNO2,REGNO3=@p_REGNO3,ADD1=@p_ADD1,ADD2=@p_ADD2,ADD3=@p_ADD3," +
                                                            " CITYNAME=@p_CITYNAME,ZIPCODE=@p_ZIPCODE,STATERID=@p_STATERID,COUNTRYRID=@p_COUNTRYRID,PHONENO=@p_PHONENO,MOBNO=@p_MOBNO,FAXNO=@p_FAXNO,EMAIL=@p_EMAIL," +
                                                            " WEBSITE=@p_WEBSITE,REGREMARK=@p_REGREMARK,restbranchnm=@p_restbranchnm, " +
                                                            " euserid = @p_userid,edatetime = getdate() " +
                                        " where rid = @p_rid " +
                                    " End  " +
                                " End  " +
                                " end  " +
                                " try   " +
                                  "   begin catch " +
                                    " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage " +
                                     " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                    " Return   " +
                                    " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MSTIMPDOC");
                strproc = " CREATE PROCEDURE SP_MSTIMPDOC " +
                        " (  " +
                        " @p_mode as int,  " +
                        " @p_rid bigint," +
                        " @p_docname nvarchar(200),     " +
                        " @p_validfrom datetime,  " +
                        " @p_validto datetime,  " +
                        " @p_contper nvarchar(200)," +
                        " @p_contno nvarchar(200)," +
                        " @p_docremark nvarchar(1000)," +
                        " @p_docdesc nvarchar(max)," +
                        " @p_docimg image," +
                        " @p_doctype nvarchar(1000)," +
                        " @p_docregno nvarchar(1000)," +
                        " @p_docagencyname nvarchar(1000)," +
                        " @p_userid bigint,  " +
                        " @p_errstr as nvarchar(max) out,   " +
                        " @p_retval as int out,   " +
                        " @p_id as bigint out   " +
                        " ) as   " +
                        " Begin  " +
                        " try  " +
                        " begin  " +
                        " set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                        " if (@p_mode=0)   " +
                        " begin  " +
                              " insert into MSTIMPDOC (DOCNAME,VALIDFROM,VALIDTO,CONTPER,CONTNO,DOCREMARK,DOCDESC,DOCIMG,DOCTYPE,DOCREGNO,DOCAGENCYNAME," +
                                                  " auserid,adatetime,DelFlg)  " +
                              " values (@p_DOCNAME,@p_VALIDFROM,@p_VALIDTO,@p_CONTPER,@p_CONTNO,@p_DOCREMARK,@p_DOCDESC,@p_DOCIMG,@p_DOCTYPE,@p_DOCREGNO,@p_DOCAGENCYNAME," +
                                     "  @p_userid,getdate(),0)  " +
                          " set @p_id=SCOPE_IDENTITY()  " +
                          " End  " +
                        " else if (@p_mode=1)   " +
                          " begin  " +
                        " set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                              " UPDATE MSTIMPDOC SET DOCNAME=@p_DOCNAME,VALIDFROM=@p_VALIDFROM,VALIDTO=@p_VALIDTO,CONTPER=@p_CONTPER," +
                                                " CONTNO=@p_CONTNO,DOCREMARK=@p_DOCREMARK,DOCDESC=@p_DOCDESC,DOCIMG=@p_DOCIMG,DOCTYPE=@p_DOCTYPE,DOCREGNO=@p_DOCREGNO,DOCAGENCYNAME=@p_DOCAGENCYNAME," +
                                                " euserid = @p_userid,edatetime = getdate()  " +
                              " where rid = @p_rid " +
                          " End   " +
                        " End   " +
                        " end   " +
                        " try    " +
                          " begin catch  " +
                          " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage  " +
                           " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                          " Return    " +
                          " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_CUSTOMERWISEOUTSTANDINGDETAILS");
                strproc = "CREATE PROCEDURE SP_CUSTOMERWISEOUTSTANDINGDETAILS" +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN " +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE," +
                                " MSTCUST.RID AS CUSTRID,MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO," +
                                " BILL.NETAMOUNT,SETTLEMENT.PAYAMOUNT,(BILL.NETAMOUNT-SETTLEMENT.PAYAMOUNT) AS PENDINGAMT " +
                                " FROM BILL" +
                                " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID)" +
                                 " LEFT JOIN ( " +
                                         "  SELECT SETTLEMENT.BILLRID,SUM(SETTLEMENT.SETLEAMOUNT) AS PAYAMOUNT " +
                                         "  FROM SETTLEMENT  " +
                                         "  WHERE ISNULL(SETTLEMENT.DELFLG,0)=0 " +
                                         "  GROUP BY SETTLEMENT.BILLRID  " +
                                          "  ) SETTLEMENT ON (SETTLEMENT.BILLRID=BILL.RID) " +
                                " WHERE (BILL.NETAMOUNT-SETTLEMENT.PAYAMOUNT)>0 AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                " AND ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0  " +
                                " ORDER BY MSTCUST.CUSTNAME" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_BILLDTLCUSTOMERWISE");
                strproc = " CREATE PROCEDURE SP_BILLDTLCUSTOMERWISE" +
                                " (@p_fromdate datetime,@p_todate datetime)  " +
                                " AS BEGIN  " +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE, " +
                                " MSTCUST.RID AS CUSTRID,MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO, " +
                                " BILL.NETAMOUNT,SETTLEMENT.PAYAMOUNT,(BILL.NETAMOUNT-SETTLEMENT.PAYAMOUNT) AS PENDINGAMT," +
                                " BILLDTL.IRID,BILLDTL.INAME,BILLDTL.IQTY,BILLDTL.IRATE,BILLDTL.GSTAMT " +
                                " FROM BILL " +
                                " LEFT JOIN ( SELECT BILLDTL.BILLRID,MSTITEM.RID AS IRID,MSTITEM.INAME,BILLDTL.IQTY,BILLDTL.IRATE,BILLDTL.GSTAMT " +
                                            " FROM BILLDTL" +
                                            " LEFT JOIN MSTITEM ON (MSTITEM.RID=BILLDTL.IRID)" +
                                            " WHERE ISNULL(BILLDTL.DELFLG,0)=0" +
                                         " ) AS BILLDTL ON (BILLDTL.BILLRID = BILL.RID)	" +
                                " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID) " +
                               "  LEFT JOIN (  " +
                                        " SELECT SETTLEMENT.BILLRID,SUM(SETTLEMENT.SETLEAMOUNT) AS PAYAMOUNT  " +
                                        " FROM SETTLEMENT   " +
                                       "  WHERE ISNULL(SETTLEMENT.DELFLG,0)=0  " +
                                       "  GROUP BY SETTLEMENT.BILLRID   " +
                                         " ) SETTLEMENT ON (SETTLEMENT.BILLRID=BILL.RID)  " +
                                " WHERE (BILL.NETAMOUNT-SETTLEMENT.PAYAMOUNT)>0 AND BILL.BILLDATE between @p_fromdate and @p_todate " +
                                " AND ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0   " +
                                " ORDER BY MSTCUST.CUSTNAME " +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_SALESSUMMARY");
                strproc = " CREATE PROCEDURE SP_SALESSUMMARY" +
                                " (@p_fromdate datetime,@p_todate datetime)" +
                                " AS BEGIN  " +
                                " SELECT BILL.RID,BILL.BILLNO,BILL.REFBILLNO,BILL.BILLDATE,BILL.BILLPAX,MSTCUST.CUSTNAME,MSTCUST.GSTNO," +
                                " BILL.TOTDISCAMOUNT,(BILL.TOTLIQVATAMT + BILL.TOTBEVVATAMT +BILL.TOTVATAMOUNT) AS TOTVATAMT,BILL.TOTGSTAMT,BILL.NETAMOUNT," +
                                " SETTINFO.CASHAMT,SETTINFO.CHEQUEAMT,SETTINFO.CREDITCARDAMT,SETTINFO.COMPLEMENTARYAMT,SETTINFO.OTHERAMT," +
                                " (BILL.NETAMOUNT - (SETTINFO.CASHAMT + SETTINFO.CHEQUEAMT + SETTINFO.CREDITCARDAMT + SETTINFO.COMPLEMENTARYAMT + SETTINFO.OTHERAMT)) AS CREDITAMT" +
                                " FROM BILL" +
                                " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID) " +
                                " LEFT JOIN (" +
                                            " SELECT SETTLEMENT.BILLRID," +
                                            " SUM(CASE WHEN SETTLEMENT.SETLETYPE='CASH' THEN ISNULL(SETLEAMOUNT,0) ELSE 0 END) AS CASHAMT," +
                                            " SUM(CASE WHEN SETTLEMENT.SETLETYPE='CHEQUE' THEN ISNULL(SETLEAMOUNT,0) ELSE 0 END) AS CHEQUEAMT," +
                                            " SUM(CASE WHEN SETTLEMENT.SETLETYPE='CREDIT CARD' THEN ISNULL(SETLEAMOUNT,0) ELSE 0 END) AS CREDITCARDAMT," +
                                            " SUM(CASE WHEN SETTLEMENT.SETLETYPE='COMPLEMENTARY' THEN ISNULL(SETLEAMOUNT,0) ELSE 0 END) AS COMPLEMENTARYAMT," +
                                            " SUM(CASE WHEN SETTLEMENT.SETLETYPE='OTHER' THEN ISNULL(SETLEAMOUNT,0) ELSE 0 END) AS OTHERAMT" +
                                            " FROM SETTLEMENT			" +
                                            " WHERE ISNULL(SETTLEMENT.DELFLG,0)=0 " +
                                            " GROUP BY SETTLEMENT.BILLRID" +
                                           " ) SETTINFO ON (SETTINFO.BILLRID = BILL.RID)" +
                                " WHERE ISNULL(BILL.DELFLG,0)=0 AND isnull(BILL.ISREVISEDBILL,0)=0 AND BILL.BILLDATE between @p_fromdate and @p_todate" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_COMPLEMENTRYKOTREG");
                strproc = " CREATE PROCEDURE SP_COMPLEMENTRYKOTREG" +
                                " (@p_fromdate datetime,@p_todate datetime) " +
                                " AS BEGIN" +
                                " SELECT KOT.RID,KOT.KOTDATE,KOT.KOTTIME,KOT.KOTNO,KOT.REFKOTNO,KOT.KOTPAX," +
                                " KOT.KOTTABLENAME,(CASE WHEN KOT.ISCOMPKOT=1 THEN 'COMPLIMENTRY' ELSE '' END) AS COMP,KOT.CUSTNAME, " +
                                " MSTITEM.INAME,KOTDTL.IQTY,KOTDTL.IRATE,KOTDTL.IAMT" +
                                " FROM KOT" +
                                " LEFT JOIN KOTDTL ON (KOTDTL.KOTRID=KOT.RID)" +
                                " LEFT JOIN MSTITEM ON (MSTITEM.RID=KOTDTL.IRID)" +
                                " WHERE ISNULL(KOT.DELFLG,0)=0 AND ISNULL(KOTDTL.DELFLG,0)=0 AND ISNULL(KOT.ISCOMPKOT,0)=1 AND ISNULL(KOTDTL.ICOMPITEM,0)=1" +
                                " AND KOT.KOTDATE BETWEEN @p_fromdate and @p_todate" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_PHYSTOCK");
                strproc = "  CREATE PROCEDURE SP_PHYSTOCK " +
                                " (@p_mode as int,  @p_rid as bigint,@p_phystkno nvarchar(50), @p_phydate datetime,@p_entryby nvarchar(100), @p_phystkremark nvarchar(max),  " +
                                " @p_userid bigint,@p_errstr as nvarchar(max) out,@p_retval as int out, @p_id as bigint out  " +
                                " )  " +
                                " as    " +
                                " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                                " if (@p_mode=0)    " +
                                " begin 		   " +
                                " Insert Into PHYSTOCK(PHYSTKNO,PHYDATE,ENTRYBY,PHYSTKREMARK,AUSERID,ADATETIME,DELFLG)  " +
                                             " Values (@p_PHYSTKNO,@p_PHYDATE,@p_ENTRYBY,@p_PHYSTKREMARK,@p_userid,getdate(),0)   " +
                                " set @p_id=SCOPE_IDENTITY() End       " +
                                " else if (@p_mode=1)     " +
                                " begin set @p_Errstr='' set @p_Retval=0 set @p_id=0 " +
                                " update PHYSTOCK set PHYSTKNO=@p_PHYSTKNO,PHYDATE=@p_PHYDATE,entryby=@p_entryby,PHYSTKREMARK=@p_PHYSTKREMARK," +
                                " euserid = @p_userid,edatetime = getdate()   " +
                                " where rid = @p_rid   " +
                                " end  " +
                                " End end " +
                                " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;   " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return     " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_PHYSTOCKDTL");
                strproc = " Create Procedure sp_PHYSTOCKDTL" +
                                " (   " +
                                " @p_mode as int,   " +
                                " @p_rid as bigint,  " +
                                " @p_phyrid bigint,  " +
                                " @p_irid bigint, " +
                                " @p_iname nvarchar(100), " +
                                " @p_iqty decimal(18,3), " +
                                " @p_iunit nvarchar(50), " +
                                " @p_irate decimal(18,3), " +
                                " @p_stkqty decimal(18,3), " +
                                " @p_closingqty decimal(18,3), " +
                                " @p_userid bigint,   " +
                                " @p_errstr as nvarchar(max) out,   " +
                                " @p_retval as int out,  " +
                                " @p_id as bigint out   " +
                                " ) as   " +
                                " begin   " +
                                " try   " +
                                 " begin   " +
                                     " set @p_Errstr='' Set @p_Retval=0 set @p_id=0   " +
                                      " if (@p_mode=0)    " +
                                         " Begin " +
                                         "  Insert Into PHYSTOCKDTL (phyrid,irid,INAME,IQTY,IUNIT,IRATE,stkqty,closingqty,auserid,adatetime,DelFlg)  " +
                                                            " Values (@p_phyrid,@p_irid,@p_INAME,@p_IQTY,@p_IUNIT,@p_irate,@p_stkqty,@p_closingqty,@p_userid,getdate(),0 ) " +
                                                      " Set @p_id=SCOPE_IDENTITY() " +
                                                  " End " +
                                     " else if (@p_mode=1)    " +
                                       "   begin   " +
                                         "  Set @p_Errstr='' set @p_Retval=0 set @p_id=0 " +
                                            " Update PHYSTOCKDTL   " +
                                            " Set irid=@p_irid,INAME=@p_INAME,IQTY=@p_IQTY,IUNIT=@p_IUNIT,IRATE=@p_irate,stkqty=@p_stkqty,closingqty=@p_closingqty," +
                                            " euserid = @p_userid,edatetime = getdate() " +
                                            " Where Rid = @p_rid and phyrid=@p_phyrid " +
                                         " End   " +
                                      " End   " +
                                     " End	 " +
                                     " try    " +
                                        "  begin catch    " +
                                         "  SELECT   ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;  " +
                                         "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0   " +
                                          " Return    " +
                                          " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_GSTBILLINFO");
                strproc = " CREATE PROCEDURE SP_GSTBILLINFO " +
                           " (@p_fromdate datetime,@p_todate datetime) AS " +
                           " BEGIN  " +
                           " SELECT BILL.BILLNO,BILL.BILLDATE,BILL.REFBILLNUM, " +
                                " BILL.TOTAMOUNT,BILL.TOTDISCAMOUNT,(BILL.TOTAMOUNT-BILL.TOTDISCAMOUNT) AS TAXABLEAMT,BILL.CGSTAMT,BILL.SGSTAMT,BILL.IGSTAMT,BILL.NETAMOUNT, " +
                                " MSTCUST.CUSTNAME,MSTCUST.GSTNO,CUSTMOBNO,MSTSTATE.STATENAME  " +
                                " FROM BILL " +
                            " LEFT JOIN MSTCUST ON (MSTCUST.RID=BILL.CUSTRID)" +
                            " LEFT JOIN MSTSTATE ON (MSTSTATE.STATEID = MSTCUST.CUSTSTATEID)     " +
                            " WHERE ISNULL(BILL.DELFLG,0)=0 AND ISNULL(BILL.ISREVISEDBILL,0)=0 " +
                            " AND BILL.BILLDATE BETWEEN @p_fromdate AND @p_todate " +
                            " END ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTMEALTYPE");
                strproc = " CREATE PROCEDURE sp_MSTMEALTYPE " +
                               " (@p_mode as int, @p_rid bigint,  " +
                                " @p_mtcode nvarchar(50),@p_mtname nvarchar(100),  " +
                                " @p_mtrate decimal(18,3),@p_mtremark nvarchar(max), " +
                                " @p_mttax1 decimal(18,3),@p_mttax2 decimal(18,3)," +
                                " @p_mttax3 decimal(18,3),@p_mttax4 decimal(18,3),@p_mttax5 decimal(18,3)," +
                                " @p_userid bigint,   " +
                                " @p_errstr as nvarchar(max) out,   " +
                                " @p_retval as int out,  " +
                                " @p_id as bigint out   " +
                                " ) as   " +
                                " begin  " +
                                " try  " +
                                " begin  " +
                                " if (@p_mode=0)  " +
                                " begin   " +
                                " declare @codeRowCount as int   " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " select @codeRowCount = (select count(*) from MSTMEALTYPE where MTCODE = @p_MTCODE and ISNULL(DelFlg,0)=0)   " +
                                " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.'   " +
                                " Return   " +
                                " End	  " +
                                " Begin   " +
                                " SET NOCOUNT ON  " +
                                " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                " select  @nameRowCount = (select count(*) from MSTMEALTYPE where MTNAME = @p_MTNAME and ISNULL(DelFlg,0)=0)    " +
                                " if ( @nameRowCount > 0)   " +
                                " begin     " +
                                " set @p_Retval = 1 set @p_Errstr ='Name Already exits.' " +
                                " Return  " +
                                " End   " +
                                " end  " +
                                " begin  " +
                                " Insert Into MSTMEALTYPE (MTCODE,MTNAME,MTRATE,MTTAX1,MTTAX2,MTTAX3,MTTAX4,MTTAX5,MTREMARK,auserid,adatetime,DelFlg)  " +
                                                 " Values (@p_MTCODE,@p_MTNAME,@p_MTRATE,@p_MTTAX1,@p_MTTAX2,@p_MTTAX3,@p_MTTAX4,@p_MTTAX5,@p_MTREMARK,@p_userid,getdate(),0)  " +
                                " set @p_id=SCOPE_IDENTITY() " +
                                " End  End   " +
                                " else if (@p_mode=1)  " +
                                " begin    " +
                                " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0  " +
                                " select @codeRowCount1 = (select count(*) from MSTMEALTYPE where MTCODE = @p_MTCODE and rid <> @p_rid and ISNULL(DelFlg,0)=0 )   " +
                                " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Code Already exits.' " +
                                " Return   End   " +
                                " Begin   " +
                                " declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0  " +
                                " select  @nameRowCount1 = (select count(*) from MSTMEALTYPE where MTNAME = @p_MTNAME and rid <> @p_rid and ISNULL(DelFlg,0)=0) " +
                                " if ( @nameRowCount1 > 0) " +
                                " begin    " +
                                " set @p_Retval = 1 set @p_Errstr ='Name Already exits.'  " +
                                " Return   " +
                                " End  END  " +
                                " begin  " +
                                " Update MSTMEALTYPE set MTCODE=@p_MTCODE,  " +
                                " MTNAME = @p_MTNAME,MTRATE=@p_MTRATE, MTTAX1=@p_MTTAX1,MTTAX2=@p_MTTAX2,MTTAX3=@p_MTTAX3,MTTAX4=@p_MTTAX4,MTTAX5=@p_MTTAX5," +
                                " MTREMARK = @p_MTREMARK, " +
                                " euserid = @p_userid,edatetime = getdate()  " +
                                " where rid = @p_rid    " +
                                " End  End  End   " +
                                " End   " +
                                " try  begin catch   " +
                                " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;  " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0  " +
                                " Return  END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MEALRESER");
                strproc = " CREATE PROCEDURE SP_MEALRESER  " +
                                " ( @p_mode as int,  @p_rid as bigint,@p_reserno nvarchar(50), @p_regdate datetime,@p_refno nvarchar(50),@p_custrid bigint,@p_sdt datetime,@p_edt datetime,@p_restype nvarchar(100),@p_reserremark nvarchar(max)," +
                                  " @p_mealbamt decimal(18,3),@p_mealdiscamt decimal(18,3),@p_mealtaxableamt decimal(18,3),@p_mealtax1amt decimal(18,3),@p_mealtax2amt decimal(18,3),@p_mealtax3amt decimal(18,3)," +
                                  " @p_mealtax4amt decimal(18,3),@p_mealtax5amt decimal(18,3),@p_mealtotamt decimal(18,3)," +
                                  " @p_userid bigint,@p_errstr as nvarchar(max) out,@p_retval as int out, @p_id as bigint out" +
                                " )   " +
                                " as     " +
                                " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0     " +
                                " if (@p_mode=0)     " +
                                " begin 		    " +
                                " Insert Into MEALRESER(RESERNO,REGDATE,REFNO,CUSTRID,SDT,EDT,RESTYPE,RESERREMARK,MEALBAMT,MEALDISCAMT,MEALTAXABLEAMT," +
                                                    " MEALTAX1AMT,MEALTAX2AMT,MEALTAX3AMT,MEALTAX4AMT,MEALTAX5AMT,MEALTOTAMT," +
                                                    " AUSERID,ADATETIME,DELFLG)   " +
                                           " Values (@p_RESERNO,@p_REGDATE,@p_REFNO,@p_CUSTRID,@p_SDT,@p_EDT,@p_RESTYPE,@p_RESERREMARK,@p_MEALBAMT,@p_MEALDISCAMT,@p_MEALTAXABLEAMT," +
                                                   " @p_MEALTAX1AMT,@p_MEALTAX2AMT,@p_MEALTAX3AMT,@p_MEALTAX4AMT,@p_MEALTAX5AMT,@p_MEALTOTAMT," +
                                                   " @p_userid,getdate(),0)    " +
                                " set @p_id=SCOPE_IDENTITY() END " +
                                " else if (@p_mode=1)      " +
                                " begin set @p_Errstr='' set @p_Retval=0 set @p_id=0  " +
                                " UPDATE MEALRESER SET RESERNO=@p_RESERNO,REGDATE=@p_REGDATE,REFNO=@p_REFNO,CUSTRID=@p_CUSTRID,SDT=@p_SDT,EDT=@p_EDT,RESTYPE=@p_RESTYPE,RESERREMARK=@p_RESERREMARK,MEALBAMT=@p_MEALBAMT," +
                                                    " MEALDISCAMT=@p_MEALDISCAMT,MEALTAXABLEAMT=@p_MEALTAXABLEAMT," +
                                                    " MEALTAX1AMT=@p_MEALTAX1AMT,MEALTAX2AMT=@p_MEALTAX2AMT,MEALTAX3AMT=@p_MEALTAX3AMT,MEALTAX4AMT=@p_MEALTAX4AMT,MEALTAX5AMT=@p_MEALTAX5AMT,MEALTOTAMT=@p_MEALTOTAMT," +
                                                    " euserid = @p_userid,edatetime = getdate()    " +
                                " where rid = @p_rid" +
                                " end   " +
                                " End end  " +
                                " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;    " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return      " +
                                " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MEALRESERDTL");
                strproc = " CREATE PROCEDURE SP_MEALRESERDTL   " +
                                " ( @p_mode as int,  @p_rid as bigint,@p_mealreserrid bigint, " +
                                  " @p_mealrid Bigint,@p_mealdesc nvarchar(1000),@p_brate decimal(18,3),@p_bqty decimal(18,3),@p_bamt decimal(18,3),@p_discamt decimal(18,3),@p_taxableamt decimal(18,3), " +
                                  " @p_tax1per  decimal(18,3),@p_tax1amt decimal(18,3),@p_tax2per  decimal(18,3),@p_tax2amt decimal(18,3),@p_tax3per  decimal(18,3),@p_tax3amt  decimal(18,3), " +
                                  " @p_tax4per  decimal(18,3),@p_tax4amt  decimal(18,3),@p_tax5per  decimal(18,3),@p_tax5amt  decimal(18,3),@p_totamt decimal(18,3), " +
                                  " @p_userid bigint,@p_errstr as nvarchar(max) out,@p_retval as int out, @p_id as bigint out " +
                                " )   " +
                                " AS       " +
                                " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0  " +
                                " if (@p_mode=0)      " +
                                " begin 		     " +
                                " Insert Into MEALRESERDTL(MEALRESERRID,MEALRID,MEALDESC,BRATE,BQTY,BAMT,DISCAMT,TAXABLEAMT,TAX1PER,TAX1AMT,TAX2PER,TAX2AMT,TAX3PER,TAX3AMT, " +
                                                        " TAX4PER,TAX4AMT,TAX5PER,TAX5AMT,TOTAMT, " +
                                                        " AUSERID,ADATETIME,DELFLG) " +
                                           " Values (@p_MEALRESERRID,@p_MEALRID,@p_MEALDESC,@p_BRATE,@p_BQTY,@p_BAMT,@p_DISCAMT,@p_TAXABLEAMT,@p_TAX1PER,@p_TAX1AMT,@p_TAX2PER,@p_TAX2AMT,@p_TAX3PER,@p_TAX3AMT, " +
                                                   " @p_TAX4PER,@p_TAX4AMT,@p_TAX5PER,@p_TAX5AMT,@p_TOTAMT, " +
                                                   " @p_userid,getdate(),0) " +
                                " set @p_id=SCOPE_IDENTITY() END         " +
                                " else if (@p_mode=1)       " +
                                " begin set @p_Errstr='' set @p_Retval=0 set @p_id=0   " +
                                " UPDATE MEALRESERDTL SET MEALRESERRID=@p_MEALRESERRID,MEALRID=@p_MEALRID,MEALDESC=@p_MEALDESC,BRATE=@p_BRATE,BQTY=@p_BQTY,BAMT=@p_BAMT,DISCAMT=@p_DISCAMT,TAXABLEAMT=@p_TAXABLEAMT,TAX1PER=@p_TAX1PER,TAX1AMT=@p_TAX1AMT, " +
                                                  "  TAX2PER=@p_TAX2PER,TAX2AMT=@p_TAX2AMT,TAX3PER=@p_TAX3PER,TAX3AMT=@p_TAX3AMT,TAX4PER=@p_TAX4PER,TAX4AMT=@p_TAX4AMT,TAX5PER=@p_TAX5PER,TAX5AMT=@p_TAX5AMT,TOTAMT=@p_TOTAMT, " +
                                                   " euserid = @p_userid,edatetime = getdate()     " +
                                " where rid = @p_rid " +
                                " end    " +
                                " End end   " +
                                " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return       " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MEALBILL");
                strproc = " CREATE PROCEDURE SP_MEALBILL  " +
                                  " ( @p_mode as int,  @p_rid as bigint,@p_mealreserrid bigint,@p_mealbillno nvarchar(100),@p_mealbilldate datetime,@p_mealbilltime datetime," +
                                    " @p_refno nvarchar(50),@p_custrid bigint,@p_deliby nvarchar(100),@p_mealbillremark nvarchar(max), " +
                                    " @p_mealbillbamt decimal(18,3),@p_mealbilldiscamt decimal(18,3),@p_mealbilltaxableamt decimal(18,3),@p_mealbilltax1amt decimal(18,3),@p_mealbilltax2amt decimal(18,3),@p_mealbilltax3amt decimal(18,3), " +
                                    " @p_mealbilltax4amt decimal(18,3),@p_mealbilltax5amt decimal(18,3),@p_mealbilltotamt decimal(18,3), " +
                                    " @p_userid bigint,@p_errstr as nvarchar(max) out,@p_retval as int out, @p_id as bigint out " +
                                  " )    " +
                                  " as      " +
                                  " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0      " +
                                  " if (@p_mode=0)      " +
                                  " begin 		     " +
                                  " Insert Into MEALBILL(MEALRESERRID,MEALBILLNO,MEALBILLDATE,MEALBILLTIME,REFNO,CUSTRID,DELIBY,MEALBILLREMARK,MEALBILLBAMT,MEALBILLDISCAMT,MEALBILLTAXABLEAMT, " +
                                                      " MEALBILLTAX1AMT,MEALBILLTAX2AMT,MEALBILLTAX3AMT,MEALBILLTAX4AMT,MEALBILLTAX5AMT,MEALBILLTOTAMT, " +
                                                      " AUSERID,ADATETIME,DELFLG)    " +
                                             " Values (@p_MEALRESERRID,@p_MEALBILLNO,@p_MEALBILLDATE,@p_MEALBILLTIME,@p_REFNO,@p_CUSTRID,@p_DELIBY,@p_MEALBILLREMARK,@p_MEALBILLBAMT,@p_MEALBILLDISCAMT,@p_MEALBILLTAXABLEAMT, " +
                                                    "  @p_MEALBILLTAX1AMT,@p_MEALBILLTAX2AMT,@p_MEALBILLTAX3AMT,@p_MEALBILLTAX4AMT,@p_MEALBILLTAX5AMT,@p_MEALBILLTOTAMT, " +
                                                    "  @p_userid,getdate(),0)     " +
                                  " set @p_id=SCOPE_IDENTITY() END  " +
                                  " else if (@p_mode=1)       " +
                                  " begin set @p_Errstr='' set @p_Retval=0 set @p_id=0   " +
                                  " UPDATE MEALBILL SET MEALRESERRID=@p_MEALRESERRID,MEALBILLNO=@p_MEALBILLNO,MEALBILLDATE=@p_MEALBILLDATE,MEALBILLTIME=@p_MEALBILLTIME,REFNO=@p_REFNO,CUSTRID=@p_CUSTRID,DELIBY=@p_DELIBY,MEALBILLREMARK=@p_MEALBILLREMARK,MEALBILLBAMT=@p_MEALBILLBAMT, " +
                                                     "  MEALBILLDISCAMT=@p_MEALBILLDISCAMT,MEALBILLTAXABLEAMT=@p_MEALBILLTAXABLEAMT, " +
                                                     "  MEALBILLTAX1AMT=@p_MEALBILLTAX1AMT,MEALBILLTAX2AMT=@p_MEALBILLTAX2AMT,MEALBILLTAX3AMT=@p_MEALBILLTAX3AMT,MEALBILLTAX4AMT=@p_MEALBILLTAX4AMT,MEALBILLTAX5AMT=@p_MEALBILLTAX5AMT,MEALBILLTOTAMT=@p_MEALBILLTOTAMT, " +
                                                     "  euserid = @p_userid,edatetime = getdate()     " +
                                  " where rid = @p_rid " +
                                  " end    " +
                                  " End end   " +
                                  " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;     " +
                                  " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return       " +
                                  " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("SP_MEALBILLDTL");
                strproc = " CREATE PROCEDURE SP_MEALBILLDTL " +
                                " ( @p_mode as int,  @p_rid as bigint,@p_mealbillrid bigint,   " +
                                " @p_mealrid Bigint,@p_mealdesc nvarchar(1000),@p_brate decimal(18,3),@p_bqty decimal(18,3),@p_bamt decimal(18,3),@p_discamt decimal(18,3),@p_taxableamt decimal(18,3),   " +
                                " @p_tax1per  decimal(18,3),@p_tax1amt decimal(18,3),@p_tax2per  decimal(18,3),@p_tax2amt decimal(18,3),@p_tax3per  decimal(18,3),@p_tax3amt  decimal(18,3),   " +
                                " @p_tax4per  decimal(18,3),@p_tax4amt  decimal(18,3),@p_tax5per  decimal(18,3),@p_tax5amt  decimal(18,3),@p_totamt decimal(18,3),   " +
                                " @p_userid bigint,@p_errstr as nvarchar(max) out,@p_retval as int out, @p_id as bigint out   " +
                                " )      " +
                                " AS         " +
                                " begin  try  begin  set @p_Errstr=''  set @p_Retval=0 set @p_id=0    " +
                                " if (@p_mode=0)        " +
                                " begin 		       " +
                                " Insert Into MEALBILLDTL(MEALBILLRID,MEALRID,MEALDESC,BRATE,BQTY,BAMT,DISCAMT,TAXABLEAMT,TAX1PER,TAX1AMT,TAX2PER,TAX2AMT,TAX3PER,TAX3AMT,   " +
                                                      " TAX4PER,TAX4AMT,TAX5PER,TAX5AMT,TOTAMT,   " +
                                                      " AUSERID,ADATETIME,DELFLG)   " +
                                         " Values (@p_MEALBILLRID,@p_MEALRID,@p_MEALDESC,@p_BRATE,@p_BQTY,@p_BAMT,@p_DISCAMT,@p_TAXABLEAMT,@p_TAX1PER,@p_TAX1AMT,@p_TAX2PER,@p_TAX2AMT,@p_TAX3PER,@p_TAX3AMT,   " +
                                                "  @p_TAX4PER,@p_TAX4AMT,@p_TAX5PER,@p_TAX5AMT,@p_TOTAMT,   " +
                                                "  @p_userid,getdate(),0)   " +
                                " set @p_id=SCOPE_IDENTITY() END           " +
                                " else if (@p_mode=1)         " +
                                " begin set @p_Errstr='' set @p_Retval=0 set @p_id=0     " +
                                " UPDATE MEALBILLDTL SET MEALBILLRID=@p_MEALBILLRID,MEALRID=@p_MEALRID,MEALDESC=@p_MEALDESC,BRATE=@p_BRATE,BQTY=@p_BQTY,BAMT=@p_BAMT,DISCAMT=@p_DISCAMT,TAXABLEAMT=@p_TAXABLEAMT,TAX1PER=@p_TAX1PER,TAX1AMT=@p_TAX1AMT,   " +
                                                "  TAX2PER=@p_TAX2PER,TAX2AMT=@p_TAX2AMT,TAX3PER=@p_TAX3PER,TAX3AMT=@p_TAX3AMT,TAX4PER=@p_TAX4PER,TAX4AMT=@p_TAX4AMT,TAX5PER=@p_TAX5PER,TAX5AMT=@p_TAX5AMT,TOTAMT=@p_TOTAMT,   " +
                                                 " euserid = @p_userid,edatetime = getdate()       " +
                                " where rid = @p_rid   " +
                                " end      " +
                                " End end     " +
                                " try   begin catch  SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;       " +
                                " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0 Return         " +
                                " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MESSPAYMENT");
                strproc = " CREATE PROCEDURE sp_MESSPAYMENT " +
                                " (   " +
                                " @p_mode as int,   " +
                                " @p_rid bigint, " +
                                " @p_custrid bigint," +
                                " @p_paydate datetime," +
                                " @p_paymentno nvarchar(100)," +
                                " @p_cashamt decimal(18,3)," +
                                " @p_cardamt decimal(18,3)," +
                                " @p_chqamt decimal(18,3)," +
                                " @p_otheramt decimal(18,3)," +
                                " @p_compleamt decimal(18,3)," +
                                " @p_totamt decimal(18,3)," +
                                " @p_tipamt decimal(18,3)," +
                                " @p_cardno nvarchar(100)," +
                                " @p_cardbanknm nvarchar(100)," +
                                " @p_chqno nvarchar(100)," +
                                " @p_chqbanknm nvarchar(100)," +
                                " @p_otherpayby nvarchar(100)," +
                                " @p_otherremark1 nvarchar(100)," +
                                " @p_otherremark2 nvarchar(100)," +
                                " @p_prepby nvarchar(100)," +
                                " @p_payremark nvarchar(max)," +
                                " @p_userid bigint,   " +
                                " @p_errstr as nvarchar(max) out,    " +
                                " @p_retval as int out,    " +
                                " @p_id as bigint out    " +
                                " ) as    " +
                                " Begin   " +
                                " try   " +
                                " begin   " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0     " +
                                " if (@p_mode=0)    " +
                                " begin   " +
                                    " insert into MESSPAYMENT (CUSTRID,PAYDATE,PAYMENTNO,CASHAMT,CARDAMT,CHQAMT,OTHERAMT,COMPLEAMT,TOTAMT,TIPAMT,CARDNO,CARDBANKNM,CHQNO,CHQBANKNM," +
                                                               "  OTHERPAYBY,OTHERREMARK1,OTHERREMARK2,PREPBY,PAYREMARK," +
                                                               "  auserid,adatetime,DelFlg)   " +
                                    " values (@p_CUSTRID,@p_PAYDATE,@p_PAYMENTNO,@p_CASHAMT,@p_CARDAMT,@p_CHQAMT,@p_OTHERAMT,@p_COMPLEAMT,@p_TOTAMT,@p_TIPAMT,@p_CARDNO,@p_CARDBANKNM,@p_CHQNO,@p_CHQBANKNM," +
                                          "   @p_OTHERPAYBY,@p_OTHERREMARK1,@p_OTHERREMARK2,@p_PREPBY,@p_PAYREMARK," +
                                           "  @p_userid,getdate(),0)   " +
                                " set @p_id=SCOPE_IDENTITY()   " +
                                " End   " +
                                " else if (@p_mode=1)    " +
                                " begin   " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0     " +
                                  "   UPDATE MESSPAYMENT SET CUSTRID=@p_CUSTRID,PAYDATE=@p_PAYDATE,PAYMENTNO=@p_PAYMENTNO,CASHAMT=@p_CASHAMT,CARDAMT=@p_CARDAMT," +
                                                           "  CHQAMT=@p_CHQAMT,OTHERAMT=@p_OTHERAMT,COMPLEAMT=@p_COMPLEAMT,TOTAMT=@p_TOTAMT,TIPAMT=@p_TIPAMT,CARDNO=@p_CARDNO," +
                                                           "  CARDBANKNM=@p_CARDBANKNM,CHQNO=@p_CHQNO,CHQBANKNM=@p_CHQBANKNM,OTHERPAYBY=@p_OTHERPAYBY,OTHERREMARK1=@p_OTHERREMARK1,OTHERREMARK2=@p_OTHERREMARK2," +
                                                           "  PREPBY=@p_PREPBY,PAYREMARK=@p_PAYREMARK," +
                                                            " euserid = @p_userid,edatetime = getdate()  " +
                                    " where rid = @p_rid  " +
                                " End    " +
                                " End    " +
                                " end    " +
                                " try     " +
                                " begin catch   " +
                                " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage   " +
                                 " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0   " +
                                " Return     " +
                                " END CATCH";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MESSCUSTOMERPAYMENTPOSITION");
                strproc = " CREATE PROCEDURE sp_MESSCUSTOMERPAYMENTPOSITION" +
                                " (@p_fromdate datetime,@p_todate datetime)" +
                                " AS BEGIN" +
                                " SELECT MSTCUST.RID AS CUSTRID, MSTCUST.CUSTNAME,MSTCUST.CUSTMOBNO,MSTCUST.CUSTEMAIL,MSTCUST.CUSTADD1," +
                                " ISNULL(BILLDTL.TOTBILLAMT,0) AS TOTBILLAMT,ISNULL(PAYDTL.TOTPAYAMT,0) AS TOTPAYAMT,(ISNULL(BILLDTL.TOTBILLAMT,0) - ISNULL(PAYDTL.TOTPAYAMT,0)) AS OUTSTANDINGAMT" +
                                " FROM MSTCUST" +
                                " LEFT JOIN ( SELECT MEALBILL.CUSTRID,SUM(MEALBILL.MEALBILLTOTAMT) AS TOTBILLAMT" +
                                            " FROM MEALBILL 			" +
                                            " WHERE ISNULL(MEALBILL.DELFLG,0)=0 AND MEALBILL.MEALBILLDATE between @p_fromdate and @p_todate " +
                                            " GROUP BY MEALBILL.CUSTRID" +
                                           " ) AS BILLDTL ON (BILLDTL.CUSTRID=MSTCUST.RID)" +
                                " LEFT JOIN ( SELECT MESSPAYMENT.CUSTRID,SUM(MESSPAYMENT.TOTAMT) AS TOTPAYAMT" +
                                            " FROM MESSPAYMENT 	" +
                                            " WHERE ISNULL(MESSPAYMENT.DELFLG,0)=0 AND MESSPAYMENT.PAYDATE between @p_fromdate and @p_todate " +
                                            " GROUP BY MESSPAYMENT.CUSTRID" +
                                           " ) AS PAYDTL ON (PAYDTL.CUSTRID=MSTCUST.RID)" +
                                " WHERE ISNULL(MSTCUST.DELFLG,0)=0" +
                                " ORDER BY MSTCUST.CUSTNAME" +
                                " END";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTITEMPRICELIST");
                strproc = " create  procedure sp_MSTITEMPRICELIST" +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid bigint, " +
                         " @p_iplcode nvarchar(20)," +
                         " @p_iplname nvarchar(50),	" +
                         " @p_ipldesc nvarchar(max), " +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         " begin " +
                         " try " +
                         " begin " +
                         " if (@p_mode=0) " +
                         " begin  " +
                         " declare @codeRowCount as int  " +
                         " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                         " select @codeRowCount = (select count(*) from MSTITEMPRICELIST where iplcode = @p_iplcode and ISNULL(DelFlg,0)=0)    " +
                         " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Price List Code Already exits.'     " +
                             " Return    " +
                         " End	    " +
                   " Begin  " +
                     "  declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0      " +
                      " select  @nameRowCount = (select count(*) from MSTITEMPRICELIST where iplname = @p_iplname and ISNULL(DelFlg,0)=0)     " +
                      " if ( @nameRowCount > 0)    " +
                      " begin    " +
                      " set @p_Retval = 1 set @p_Errstr ='Price List Name Already exits.'    " +
                      " Return  " +
                             "   End" +
                             " end " +
                                         "  begin " +
                                         "  Insert Into MSTITEMPRICELIST (iplcode,iplname,ipldesc,senddata,auserid,adatetime,DelFlg) " +
                                         "  Values (@p_iplcode,@p_iplname,@p_ipldesc,@p_userid,0,getdate(),0)" +
                                        " 	set @p_id=SCOPE_IDENTITY()" +
                                        " End  End  " +
                                 " else if (@p_mode=1) " +
                                     "  begin    " +
                                      " declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0     " +
                                      " select @codeRowCount1 = (select count(*) from MSTITEMPRICELIST where iplcode = @p_iplcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                      " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Price List Code Already exits.'      " +
                                      " Return   End  " +
                              " Begin  " +
                                 "  declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0      " +
                                  " select  @nameRowCount1 = (select count(*) from MSTITEMPRICELIST where iplname = @p_iplname and rid <> @p_rid and ISNULL(DelFlg,0)=0)     " +
                                  " if ( @nameRowCount1 > 0)     " +
                                  " begin    " +
                                  " set @p_Retval = 1 set @p_Errstr ='Price List Name Already exits.' " +
                                  " Return  " +
                               " End  END " +
                              " begin  " +
                                      " Update MSTITEMPRICELIST set iplcode=@p_iplcode, " +
                                      " iplname = @p_iplname, " +
                                      " ipldesc = @p_ipldesc, " +
                                      " senddata=0,euserid = @p_userid,edatetime = getdate()    " +
                                      " where rid = @p_rid    " +
                                      " End  End  End  " +
                                        " End  " +
                                      " try  begin catch    " +
                                      " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                        "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                     " Return  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTITEMPRICELISTDTL");
                strproc = "";
                strproc = " CREATE Procedure sp_MSTITEMPRICELISTDTL" +
                         " ( " +
                         " @p_mode as int, " +
                         " @p_rid as bigint," +
                         " @p_iplrid bigint,	" +
                         " @p_irid bigint," +
                         " @p_irate decimal(18,2), " +
                         " @p_iprate decimal(18,2)," +
                         " @p_userid bigint, " +
                         " @p_errstr as nvarchar(max) out, " +
                         " @p_retval as int out," +
                         " @p_id as bigint out" +
                         " ) as " +
                         " begin " +
                         " try " +
                            " begin" +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " if (@p_mode=0) " +
                                    " begin " +
                                        " Insert Into MSTITEMPRICELISTDTL (iplrid,irid,irate,iprate,senddata,auserid,adatetime,DelFlg) " +
                                        " Values (@p_iplrid,@p_irid,@p_irate,@p_iprate,@p_userid,0,getdate(),0)" +
                                        " set @p_id=SCOPE_IDENTITY()" +
                                        " End    " +
                                 " else if (@p_mode=1)  " +
                                    " begin  " +
                                      " UPDATE MSTITEMPRICELISTDTL set irid=@p_irid, " +
                                      " irate = @p_irate, " +
                                      " iprate = @p_iprate, " +
                                      " senddata=0,euserid = @p_userid,edatetime = getdate()    " +
                                      " where rid = @p_rid and iplrid = @p_iplrid   " +
                                     " End " +
                                  " End" +
                                " end		" +
                                " try  " +
                                    " begin catch    " +
                                      " SELECT ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                      " set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                      " Return  " +
                                      " END CATCH ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);

                this.DeleteProcedureFromOnlineDb("sp_MSTEMPCAT");
                strproc = " CREATE  procedure sp_MSTEMPCAT " +
                                " ( " +
                                " @p_mode as int, " +
                                " @p_rid bigint, " +
                                " @p_empcatcode nvarchar(50)," +
                                " @p_empcatname nvarchar(250),	" +
                                " @p_empcatdesc nvarchar(max), " +
                                " @p_userid bigint, " +
                                " @p_errstr as nvarchar(max) out, " +
                                " @p_retval as int out," +
                                " @p_id as bigint out" +
                                " ) as " +
                                " begin " +
                                " try " +
                                " begin " +
                                " if (@p_mode=0) " +
                                " begin  " +
                                " declare @codeRowCount as int  " +
                                " set @p_Errstr=''  set @p_Retval=0 set @p_id=0   " +
                                " select @codeRowCount = (select count(*) from mstempcat where empcatcode = @p_empcatcode and ISNULL(DelFlg,0)=0)    " +
                                " if (@codeRowCount > 0) begin set @p_Retval=1 set @p_Errstr= 'Category Code Already exits.'     " +
                                "  Return    " +
                                " End	    " +
                                " Begin  " +
                                " declare  @nameRowCount int    set @p_Errstr=''  set @p_Retval=0 set @p_id=0      " +
                                " select  @nameRowCount = (select count(*) from mstempcat where empcatname = @p_empcatname and ISNULL(DelFlg,0)=0)     " +
                                " if ( @nameRowCount > 0)    " +
                                " begin    " +
                                " set @p_Retval = 1 set @p_Errstr ='Category Name Already exits.'    " +
                                " Return  " +
                                  "  End   " +
                                 " end " +
                                             "  begin " +
                                              " Insert Into mstempcat (empcatcode,empcatname,empcatdesc,senddata,auserid,adatetime,DelFlg) " +
                                              " Values (@p_empcatcode,@p_empcatname,@p_empcatdesc,0,@p_userid,getdate(),0)" +
                                                " set @p_id=SCOPE_IDENTITY()" +
                                              " End  End  " +
                                     " else if (@p_mode=1) " +
                                       "    begin    " +
                                         "  declare @codeRowCount1 as int set @p_Errstr='' set @p_Retval=0  set @p_id=0     " +
                                          " select @codeRowCount1 = (select count(*) from mstempcat where empcatcode = @p_empcatcode and rid <> @p_rid and ISNULL(DelFlg,0)=0 )  " +
                                          " if (@codeRowCount1 > 0) begin set @p_Retval=1 set @p_Errstr= 'Category Code Already exits.'      " +
                                          " Return   End  " +
                                  " Begin  " +
                                    "   declare  @nameRowCount1 int    set @p_Errstr=''  set @p_Retval=0      " +
                                      " select  @nameRowCount1 = (select count(*) from mstempcat where empcatname = @p_empcatname and rid <> @p_rid and ISNULL(DelFlg,0)=0)     " +
                                      " if ( @nameRowCount1 > 0)     " +
                                      " begin    " +
                                      " set @p_Retval = 1 set @p_Errstr ='Category Name Already exits.' " +
                                      " Return  " +
                                   " End  END " +
                                  " begin  " +
                                         "  UPDATE mstempcat set empcatcode=@p_empcatcode, " +
                                          " empcatname = @p_empcatname, " +
                                          " empcatdesc = @p_empcatdesc, " +
                                          " senddata=0, euserid = @p_userid,edatetime = getdate()    " +
                                          " where rid = @p_rid    " +
                                          " End  End  End  " +
                                            " End  " +
                                          " try  begin catch    " +
                                          " SELECT   ERROR_NUMBER() AS ErrorNumber,  ERROR_MESSAGE() AS ErrorMessage;     " +
                                            "  set   @p_Retval = ERROR_NUMBER()  set @p_Errstr = ERROR_MESSAGE() set @p_id=0" +
                                         " Return  END CATCH  ";
                this.ExecuteMsSqlOnlineCommand_NoMsg(strproc);



                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region SENDDATA

        public bool CheckValidity()
        {
            bool retval = false;
            DataTable dtinfo = new DataTable();
            string blockval = "";
            try
            {
                dtinfo = this.FillOnlineDataTable("SELECT * FROM MSTUSERS WHERE DBNAME = '" + clspublicvariable.GENONLINEDBNAME + "'", "MSTUSERS");

                if (dtinfo.Rows.Count > 0)
                {
                    blockval = dtinfo.Rows[0]["ISBLOCK"] + "";

                    if (blockval == "False")
                    {
                        retval = true;
                    }
                }

                return retval;
            }
            catch (Exception)
            {
                return retval;
            }
        }

        public string SENDATA_ONLINE_MSTCITY()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT CITYID,CITYNAME FROM MSTCITY ORDER BY CITYID", "MSTCITY");
                this.ExecuteMsSqlOnlineCommand_NoMsg("DELETE FROM MSTCITY WHERE CITYID>0");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTCITY"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTCITY", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTCOUNTRY()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT COUNTRYID,COUNTRYNAME FROM MSTCOUNTRY ORDER BY COUNTRYID", "MSTCOUNTRY");
                this.ExecuteMsSqlOnlineCommand_NoMsg("DELETE FROM MSTCOUNTRY WHERE COUNTRYID>0");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTCOUNTRY"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTCOUNTRY", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTSTATE()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT STATEID,STATENAME FROM MSTSTATE ORDER BY STATEID", "MSTSTATE");
                this.ExecuteMsSqlOnlineCommand_NoMsg("DELETE FROM MSTSTATE WHERE STATEID>0");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTSTATE"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTSTATE", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTUNIT()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT  RID,UNITCODE,UNITNAME,UNITDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA " +
                                                        " FROM MSTUNIT WHERE ISNULL(MSTUNIT.SENDDATA,0)=0 ORDER BY RID", "MSTUNIT");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTUNIT"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTUNIT", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTUNIT())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString(); ;
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTDEPT()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT   RID,DEPTCODE,DEPTNAME,DEPTDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,ISBARDEPT,ISKITCHENDEPT,ISHUKKADEPT,SENDDATA " +
                                                        " FROM MSTDEPT WHERE ISNULL(MSTDEPT.SENDDATA,0)=0 ORDER BY RID", "MSTDEPT");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTDEPT"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTDEPT", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTDEPT())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTUSERS()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,USERNAME,PASSWORD,REPASSWORD,USERDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,USERCODE,ISSHOWREPORT, " +
                                                        " HIDEMSTMNU,HIDEBANQMNU,HIDECRMMNU,HIDESMSMNU,HIDEPAYROLLMNU,HIDEUTILITYMNU,HIDEKOTBILLENT,HIDECASHMENOENT,HIDESETTLEMENTENT,HIDEQUICKBILLENT, " +
                                                        " HIDETABLEVIEWENT,HIDEMULTISETTLEMENTENT,HIDECASHONHANDENT,HIDESUPPLIERENT,HIDEPURCHASEENT,HIDEPAYMENYENT,HIDECHECKLISTENT,HIDEBUSINESSSUMMARY,DONTALLOWBILLEDIT, " +
                                                        " DONTALLOWBILLDELETE,DONTALLOWTBLCLEAR,HIDESTKISSUEENT,DONTALLOWBANQBOEDIT,DONTALLOWBANQBODELETE,DONTALLOWCHKLISTEDIT,DONTALLOWCHKLISTDELETE,DONTALLOWPURCHASEEDIT,DONTALLOWPURCHASEDELETE, " +
                                                        " DONTALLOWSTKISSUEEDIT,DONTALLOWSTKISSUEDELETE,HIDEPURITEMGROUPENT,HIDEPURITEMENT,DONTALLOWCHANGEDATEINKOTBILL,HIDEPURCHASEICO,ISTABLETUSER,DONTALLOWKOTEDIT,DONTALLOWKOTDELETE,DONTALLOWDISCINBILL,HIDEINVMNU,SENDDATA " +
                                                        " FROM MSTUSERS WHERE ISNULL(MSTUSERS.SENDDATA,0)=0 ORDER BY RID", "MSTUSERS");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTUSERS"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTUSERS", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTUSERS())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTREPORTDEPT()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,REPORTDEPTCODE,REPORTDEPTNAME,REPORTDEPTDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, SENDDATA " +
                                                        " FROM MSTREPORTDEPT WHERE ISNULL(MSTREPORTDEPT.SENDDATA,0)=0 ORDER BY RID", "MSTREPORTDEPT");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTREPORTDEPT"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTREPORTDEPT", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTREPORTDEPT())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTHSNCODE()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,HSNCODE,HSNCODEREMARK,ACTOTGSTPER,ACCGSTPER,ACSGSTPER,ACIGSTPER,NACTOTGSTPER,NACCGSTPER,NACSGSTPER,NACIGSTPER, " +
                                                        " CATOTGSTPER,CACGSTPER,CASGSTPER,CAIGSTPER,RSTOTGSTPER,RSCGSTPER,RSSGSTPER,RSIGSTPER,PURTOTGSTPER,PURCGSTPER,PURSGSTPER,PURIGSTPER, " +
                                                        " BANQTOTGSTPER,BANQCGSTPER,BANQSGSTPER,BANQIGSTPER,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA " +
                                                       "  FROM MSTHSNCODE WHERE ISNULL(MSTHSNCODE.SENDDATA,0)=0 ORDER BY RID", "MSTHSNCODE");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTHSNCODE"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTHSNCODE", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTHSNCODE())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTITEMGROUP()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,IGCODE,IGNAME,IGDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,NOTAPPLYDISC,IGPNAME, " +
                                                        " IGDISPORD,ISHIDEGROUP,SHOWINDIFFCOLOR,ISHIDEGROUPKOT,ISITEMREMGRP,ISHIDEGROUPCASHMEMO,HSNCODERID,IGBACKCOLOR,IGFORECOLOR, " +
                                                        " IGFONTNAME,IGFONTSIZE,IGFONTBOLD,IGPRINTORD,REGLANGIGNAME,SENDDATA " +
                                                        " FROM MSTITEMGROUP WHERE ISNULL(MSTITEMGROUP.SENDDATA,0)=0 ORDER BY RID", "MSTITEMGROUP");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTITEMGROUP"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTITEMGROUP", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTITEMGROUP())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTITEM()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT  RID,ICODE,INAME,IIMG,IGRPRID,IUNITRID,IDEPTRID,IRATE,IDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, " +
                                                    " ICOMMI,IPNAME,IMINQTY,IMAXQTY,IREQTY,ISITEMSTOCK,REPORTDEPTRID,ISHIDEITEM,ISNOTAPPSERTAXDISC,IDPRATE,ISRUNNINGITEM,DISDIFFCOLOR,ITEMTAXTYPE, " +
                                                    " ISCHKRPTITEM,ISNOTAPPLYVAT,COUPONCODE,IREWPOINT,NOTAPPLYGSTTAX,NOTAPPLYDISC,HSNCODERID,IBACKCOLOR,IFORECOLOR,IFONTNAME,IFONTSIZE,IFONTBOLD,REGLANGNAME, " +
                                                    " IREMARK,SENDDATA,IPURRATE,NUT1,NUT2,NUT3,NUT4,NUT5,NUT6,NUT7 " +
                                                    " FROM MSTITEM WHERE ISNULL(MSTITEM.SENDDATA,0)=0 ORDER BY RID", "MSTITEM");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTITEM"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTITEM", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTITEM())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTITEMPRICELIST()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,IPLCODE,IPLNAME,IPLDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA " +
                                                        " FROM MSTITEMPRICELIST WHERE ISNULL(MSTITEMPRICELIST.SENDDATA,0)=0 ORDER BY RID", "MSTITEMPRICELIST");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTITEMPRICELIST"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTITEMPRICELIST", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTITEMPRICELIST())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTITEMPRICELISTDTL()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT  RID,IPLRID,IRID,IRATE,IPRATE,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, SENDDATA " +
                                                        " FROM MSTITEMPRICELISTDTL WHERE ISNULL(MSTITEMPRICELISTDTL.SENDDATA,0)=0 ORDER BY RID", "MSTITEMPRICELISTDTL");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTITEMPRICELISTDTL"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTITEMPRICELISTDTL", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTITEMPRICELISTDTL())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTREMARK()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT  RID,REMARKCODE,REMARKNAME,REMARKDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA " +
                                                        " FROM MSTREMARK WHERE ISNULL(MSTREMARK.SENDDATA,0)=0 ORDER BY RID", "MSTREMARK");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTREMARK"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTREMARK", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTREMARK())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTTABLE()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,TABLECODE,TABLENAME,TABLEDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,ISROOMTABLE,ISPARCELTABLE,ROOMNO,TABDISC,PRICELISTRID, " +
                                                        " ISHOMEDELITABLE,ISNOTCALCCOMMI,ISHIDETABLE,TABPAX,TABDISPORD,GSTTAXTYPE,TABLEGROUP,SECNO,MSTTIEUPCOMPRID,SENDDATA,CUSTRID " +
                                                        " FROM MSTTABLE WHERE ISNULL(MSTTABLE.SENDDATA,0)=0 ORDER BY RID", "MSTTABLE");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTTABLE"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTTABLE", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTTABLE())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTEMPCAT()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT  RID,EMPCATCODE,EMPCATNAME,EMPCATDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA" +
                                                        " FROM MSTEMPCAT WHERE ISNULL(MSTEMPCAT.SENDDATA,0)=0 ORDER BY RID", "MSTEMPCAT");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTEMPCAT"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTEMPCAT", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTEMPCAT())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTEMP()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,EMPCODE,EMPNAME,EMPFATHERNAME,EMPADD1,EMPADD2,EMPADD3,EMPCATID,EMPCITYID,EMPSTATEID,EMPCOUNTRYID,EMPPIN,EMPTELNO,EMPMOBILENO, " +
                                                        " EMPEMAIL,EMPFAXNO,EMPBIRTHDATE,EMPJOINDATE,EMPLEAVEDATE,EMPGENDER,EMPMARITALSTATUS,ISDISPINKOT,EMPIMAGE,EMPDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID, " +
                                                        " DDATETIME,DELFLG,EMPANNIDATE,EMPBANKNAME,EMPBANKDETAILS,EMPBANKACCNO,ISNONACTIVE,SENDDATA " +
                                                        " FROM MSTEMP WHERE ISNULL(MSTEMP.SENDDATA,0)=0 ORDER BY RID", "MSTEMP");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTEMP"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTEMP", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTEMP())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTCUST()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,CUSTCODE,CUSTNAME,CUSTADD1,CUSTADD2,CUSTADD3,CUSTCITYID,CUSTSTATEID,CUSTCOUNTRYID,CUSTPIN,CUSTTELNO,CUSTMOBNO,CUSTEMAIL, " +
                                                        " CUSTFAXNO,CUSTBIRTHDATE,CUSTGENDER,CUSTMARITALSTATUS,CUSTANNIDATE,CUSTIMAGE,CUSTREGDATE,CUSTDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID, " +
                                                        " DDATETIME,DELFLG,CUSTMOBNO2,CUSTMOBNO3,CUSTMOBNO4,CUSTMOBNO5,CUSTAREA,CUSTADD2ADD1,CUSTADD2ADD2,CUSTADD2ADD3,CUSTADD2AREA,CUSTADD2CITY,CUSTADD2PIN, " +
                                                        " CUSTADD3ADD1,CUSTADD3ADD2,CUSTADD3ADD3,CUSTADD3AREA,CUSTADD3CITY,CUSTADD3PIN,CUSTADD4ADD1,CUSTADD4ADD2,CUSTADD4ADD3,CUSTADD4AREA,CUSTADD4CITY,CUSTADD4PIN, " +
                                                        " CUSTADD5ADD1,CUSTADD5ADD2,CUSTADD5ADD3,CUSTADD5AREA,CUSTADD5CITY,CUSTADD5PIN,CUSTDISCPER,CUSTLANDMARK,CUSTDOCIMAGE,FOODTOKEN,CARDNO,CARDACTDATE,CARDENROLLFEES,CARDSTATUS, " +
                                                        " CARDEXPDATE,CARDREMARK,GSTNO,PANNO,APPLYIGST,VATNO,SENDDATA, TIEUPRID " +
                                                        " FROM MSTCUST WHERE ISNULL(MSTCUST.SENDDATA,0)=0 ORDER BY RID", "MSTCUST");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTCUST"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTCUST", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTCUST())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_KOT()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "KOT.KOTDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,KOTDATE,KOTTIME,KOTNO,KOTTOKNO,KOTORDERPERID,ISPARSELKOT,KOTTABLEID,KOTTABLENAME,KOTREMARK,AUSERID,ADATETIME,EUSERID, " +
                                                        " EDATETIME,DUSERID,DDATETIME,DELFLG,KOTPAX,CUSTRID,CUSTNAME,CUSTADD,KOTINFO,CARDNO,REFKOTNO,REFKOTNUM,ISCOMPKOT, SENDDATA " +
                                                        " FROM KOT WHERE " + wstr_Startdate + " AND ISNULL(KOT.SENDDATA,0)=0 ORDER BY RID", "KOT");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_KOT"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLKOT", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_KOT())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_KOTDTL()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";

            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "KOT.KOTDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";


                dtinfo = clslocaldb.FillDataTableLocal("SELECT KOTDTL.RID,KOTDTL.KOTRID,KOTDTL.IRID,KOTDTL.INAME,KOTDTL.IQTY,KOTDTL.IRATE,KOTDTL.IAMT,KOTDTL.AUSERID,KOTDTL.ADATETIME,KOTDTL.EUSERID,KOTDTL.EDATETIME,KOTDTL.DUSERID,KOTDTL.DDATETIME,KOTDTL.DELFLG,KOTDTL.IREMARK,KOTDTL.IMODIFIER,KOTDTL.ICOMPITEM, KOTDTL.SENDDATA " +
                                                        " FROM KOT " +
                                                        " INNER JOIN KOTDTL ON (KOT.RID = KOTDTL.KOTRID)" +
                                                        " WHERE " + wstr_Startdate + " AND ISNULL(KOTDTL.SENDDATA,0)=0 ORDER BY KOT.RID,KOTDTL.RID", "KOTDTL");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_KOTDTL"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLKOTDTL", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_KOTDTL())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_BILL()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "BILL.BILLDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,BILLNO,BILLDATE,BILLTYPE,CUSTRID,CUSTNAME,CUSTCONTNO,TABLERID,PRICELISTRID,BILLPAX,TOTAMOUNT,TOTSERTAXPER,TOTSERTAXAMOUNT, " +
                                                        " TOTVATPER,TOTVATAMOUNT,TOTADDVATPER,TOTADDVATAMOUNT,TOTDISCPER,TOTDISCAMOUNT,TOTROFF,NETAMOUNT,BILLPREPBY,BILLREMARK,SETLETYPE,SETLEAMOUNT,CHEQUENO, " +
                                                        " CHEQUEBANKNAME,CREDITCARDNO,CREDITHOLDERNAME,CREDITBANKNAME,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,REFBILLNO,REFBILLNUM,custadd, " +
                                                        " TOTDISCOUNTABLEAMOUNT,ISREVISEDBILL,REVISEDBILLSRNO,REVISEDBILLNO,BASEBILLNO,MAINBASEBILLNO,LASTTABLERID,LASTTABLESTATUS,BILLORDERPERID,BILLTIME,TOTCALCVATPER, " +
                                                        " TOTCALCVATAMOUNT,ISBILLTOCUSTOMER,CNTPRINT,TOKENNO,ISPARCELBILL,ISCOMPLYBILL,TOTBEVVATPER,TOTBEVVATAMT,TOTLIQVATPER,TOTLIQVATAMT,REFBYRID,BILLINFO,TOTSERCHRPER,TOTSERCHRAMT,TOTNEWTOTALAMT, " +
                                                        " TOTADDDISCAMT,MSTTIEUPCOMPRID,COUPONNO,COUPONPERNAME,TOTKKCESSPER,TOTKKCESSAMT,OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1,OTHERPAYMENTBYREMARK2,PAYMENT,MULTICASHAMT,MULTICHEQUEAMT,MULTICREDITCARDAMT, " +
                                                        " MULTIOTHERAMT,MULTICHQNO,MULTICHQBANKNAME,MULTICARDNO,MULTICARDBANKNAME,MULTIOTHERPAYMENTBY,MULTIOTHERREMARK1,MULTIOTHERREMARK2,MULTITIPAMT,CARDNO,TOTBILLREWPOINT,TOTITEMREWPOINT,CGSTAMT,SGSTAMT,IGSTAMT,TOTGSTAMT,RECAMT,RETAMT,SENDDATA " +
                                                        " FROM BILL WHERE " + wstr_Startdate + " AND ISNULL(BILL.SENDDATA,0)=0 ORDER BY RID", "BILL");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_BILL"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLBILL", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_BILL())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_BILLDTL()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "BILL.BILLDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT BILLDTL.RID,BILLDTL.BILLRID,BILLDTL.IRID,BILLDTL.KOTRID,BILLDTL.IQTY,BILLDTL.IRATE,BILLDTL.IPAMT,BILLDTL.IAMT,BILLDTL.AUSERID,BILLDTL.ADATETIME,BILLDTL.EUSERID,BILLDTL.EDATETIME,BILLDTL.DUSERID,BILLDTL.DDATETIME,BILLDTL.DELFLG,BILLDTL.NOTAPPLYDISC, " +
                                                        " BILLDTL.NOTAPPLYSERTAX, BILLDTL.SERTAXTYPE, BILLDTL.NOTAPPLYVAT, BILLDTL.DISCPER, BILLDTL.DISCAMT, BILLDTL.SERTAXPER, BILLDTL.SERTAXAMT, BILLDTL.FOODVATPER, BILLDTL.FOODVATAMT, BILLDTL.LIQVATPER, BILLDTL.LIQVATAMT, BILLDTL.BEVVATPER, BILLDTL.BEVVATAMT, BILLDTL.SERCHRPER, BILLDTL.SERCHRAMT, " +
                                                        " BILLDTL.NEWSERCHRPER, BILLDTL.NEWSERCHRAMT, BILLDTL.KKCESSPER, BILLDTL.KKCESSAMT, BILLDTL.IREWPOINTS, BILLDTL.NOTAPPLYGST, BILLDTL.CGSTPER, BILLDTL.CGSTAMT, BILLDTL.SGSTPER, BILLDTL.SGSTAMT, BILLDTL.IGSTPER, BILLDTL.IGSTAMT, BILLDTL.GSTPER, BILLDTL.GSTAMT, BILLDTL.SENDDATA " +
                                                        " FROM BILL " +
                                                        " INNER JOIN BILLDTL ON(BILL.RID = BILLDTL.BILLRID) " +
                                                        " WHERE " + wstr_Startdate + " AND ISNULL(BILLDTL.SENDDATA,0)=0 ORDER BY BILL.RID,BILLDTL.RID", "BILLDTL");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_BILLDTL"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLBILLDTL", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_BILLDTL())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_SETTLEMENT()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "SETTLEMENT.SETLEDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT  RID,SETLEDATE,BILLRID,SETLENO,SETLETYPE,SETLEAMOUNT,CHEQUENO,CHEQUEBANKNAME,CREDITCARDNO,CREDITHOLDERNAME,CREDITBANKNAME,SETLEPREPBY,SETLEREMARK, " +
                                                        " AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,CUSTRID,ADJAMT,TIPAMT,OTHERPAYMENTBY,OTHERPAYMENTBYREMARK1,OTHERPAYMENTBYREMARK2,SENDDATA" +
                                                        " FROM SETTLEMENT WHERE " + wstr_Startdate + " AND ISNULL(SETTLEMENT.SENDDATA,0)=0 ORDER BY RID", "SETTLEMENT");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_SETTLEMENT"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLSETTLEMENT", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_SETTLEMENT())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_CASHONHAND()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "CASHONHAND.CASHDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,CASHDATE,CASHAMT,CASHSTATUS,CASHPERSONNAME,CASHREMARK,CASHDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,EMPRID,CASHNO,SENDDATA " +
                                                    " FROM CASHONHAND WHERE " + wstr_Startdate + " AND ISNULL(CASHONHAND.SENDDATA,0)=0 ORDER BY RID", "CASHONHAND");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_CASHONHAND"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLCASHONHAND", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_CASHONHAND())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTEXPENCES()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT  RID,EXCODE,EXNAME,EXREMARK,EXDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA,ISOPECOST,ISFUELCOST " +
                                                        " FROM MSTEXPENCES WHERE ISNULL(MSTEXPENCES.SENDDATA,0)=0 ORDER BY RID", "MSTEXPENCES");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTEXPENCES"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTEXPENCE", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTEXPENCES())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTINCOME()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,INCODE,INNAME,INREMARK,INDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG, SENDDATA FROM MSTINCOME WHERE ISNULL(MSTINCOME.SENDDATA,0)=0 ORDER BY RID", "MSTINCOME");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTINCOME"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTINCOME", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTINCOME())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTTIEUPCOMPANY()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,COMPCODE,COMPNAME,CONTPER,CONTNO,COMPDISC,COMPREMARK,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME, " +
                                                        " DELFLG,PAYMENTBY,SENDDATA,SALECOMMIPER,COMMIPER " +
                                                        " FROM MSTTIEUPCOMPANY WHERE ISNULL(MSTTIEUPCOMPANY.SENDDATA,0)=0 ORDER BY RID", "MSTTIEUPCOMPANY");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTTIEUPCOMPANY"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTTIEUPCOMPANY", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTTIEUPCOMPANY())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_MSTSUPPLIER()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,SUPPCODE,SUPPNAME,SUPPADD1,SUPPADD2,SUPPADD3,SUPPCITYID,SUPPSTATEID,SUPPCOUNTRYID,SUPPPIN,SUPPTELNO,SUPPMOBNO,SUPPFAXNO,SUPPCONTPERNAME1,SUPPCONTPERNAME2, " +
                                                        " SUPPEMAIL,SUPPPANNO,SUPPTINNO,SUPPCSTNO,SUPPGSTNO,SUPPREMARK,SUPPIMAGE,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SUPPTYPE,SENDDATA,SUPPPAYMENT,ITEMTYPE " +
                                                        " FROM MSTSUPPLIER WHERE ISNULL(MSTSUPPLIER.SENDDATA,0)=0 ORDER BY RID", "MSTSUPPLIER");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_MSTSUPPLIER"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLMSTSUPPLIER", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_MSTSUPPLIER())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_TABLERESERVATION()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            try
            {
                strret = "";
                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,REVNO,BODATE,REVDATE,REVTIME,CUSTRID,TABLERID,PAX,FUNCNAME,SPREQ,REVDESC,ENTRYBY,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA FROM TABLERESERVATION WHERE ISNULL(TABLERESERVATION.SENDDATA,0)=0 ORDER BY RID", "TABLERESERVATION");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_TABLERESERVATION"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLTABLERESERVATION", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_TABLERESERVATION())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_EXPENCES()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";

            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "EXPENCE.EXDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,EXPENCENO,EXDATE,EXTIME,EXRID,EXTYPE,EXAMOUNT,EXPERNAME,EXCONTNO,REMARK1,REMARK2,REMARK3,EXDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA FROM EXPENCE WHERE " + wstr_Startdate + " AND ISNULL(EXPENCE.SENDDATA,0)=0 ORDER BY RID", "EXPENCE");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_EXPENCE"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLEXPENCE", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_EXPENCES())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_INCOME()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "INCOME.INDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,INCOMENO,INDATE,INTIME,INRID,INTYPE,INAMOUNT,INPERNAME,INCONTNO,REMARK1,REMARK2,REMARK3,INDESC,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA FROM INCOME WHERE " + wstr_Startdate + " AND ISNULL(INCOME.SENDDATA,0)=0 ORDER BY RID", "INCOME");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_INCOME"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLINCOME", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_INCOME())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        public string SENDATA_ONLINE_OPCASH()
        {
            DataTable dtinfo = new DataTable();
            SqlTransaction objTrans = null;
            SqlConnection con = new SqlConnection();
            string strret = "";
            string wstr_Startdate = "";
            string fldnm1 = "";
            try
            {
                strret = "";
                wstr_Startdate = "";
                fldnm1 = "OPCASH.OPCASHDATE ";
                wstr_Startdate = "(" + fldnm1 + ">="
                                + (clspublicvariable.DateCriteriaconst
                                + (clspublicvariable.GENSTARTDATE.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)) + " )";

                dtinfo = clslocaldb.FillDataTableLocal("SELECT RID,OPCASHDATE,OPAMT,OPREMARK,OPENTRYBY,AUSERID,ADATETIME,EUSERID,EDATETIME,DUSERID,DDATETIME,DELFLG,SENDDATA FROM OPCASH WHERE " + wstr_Startdate + " AND ISNULL(OPCASH.SENDDATA,0)=0 ORDER BY RID", "OPCASH");

                using (con = new SqlConnection(clspublicvariable.Connstr_OnlineDb))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SENDDATA_INCOME"))
                    {
                        con.Open();
                        objTrans = con.BeginTransaction();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Transaction = objTrans;
                        cmd.Parameters.AddWithValue("@TBLINCOME", dtinfo);

                        try
                        {
                            cmd.ExecuteNonQuery();

                            if (!clslocaldb.UPDATE_TABLE_INCOME())
                            {
                                objTrans.Rollback();
                            }

                            objTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            strret = ex.Message.ToString();
                            objTrans.Rollback();
                        }
                    }
                }

                return strret;
            }
            catch (Exception ex)
            {
                return strret = ex.Message.ToString();
            }

            finally
            {
                con.Close();
            }
        }

        #endregion

    }
}
