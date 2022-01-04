using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace SENDDATARMSGST
{
    public partial class mdimain : Form
    {
        private clsgeneral clsgen = new clsgeneral();
        private clsupdateonlinestructure clsonlinedb = new clsupdateonlinestructure();
        private clsupdatelocalstructure clslocaldb = new clsupdatelocalstructure();

        private static mdimain _instance;
        private bool _startprocess;

        public bool Startprocess
        {
            get { return _startprocess; }
            set { _startprocess = value; }
        }

        public mdimain()
        {
            InitializeComponent();
        }

        # region PROPERTY

        public mdimain Instance
        {
            get
            {
                if (mdimain._instance == null)
                {
                    mdimain._instance = new mdimain();
                }

                return mdimain._instance;
            }

        }

        #endregion

        private void mdimain_Load(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Refresh();
            this.Instance.Startprocess = false;

            this.pnllog.Visible = false;
            this.pnlcheckconnection.Visible = false;

            clspublicvariable.ISAUTOSTART = false;
            foreach (string arg in Environment.GetCommandLineArgs())
            {
                if (arg.ToUpper() == "AUTO")
                {
                    clspublicvariable.LoginUserId = 1;
                    clspublicvariable.LoginUserName = "AdminAuto";
                    clspublicvariable.LoginDatetime = System.DateTime.Now;
                    clspublicvariable.ISAUTOSTART = true;
                }
            }

            if (!clspublicvariable.ISAUTOSTART)
            {
                new frmlogin().ShowDialog();
            }

            if (clspublicvariable.LoginUserId <= 0)
            {
                Application.Exit();
            }

            clsgen.FillGeneralSetting();

            clspublicvariable.Connstr_OnlineDb = "Data Source=" + clspublicvariable.GENONLINESERVERNAME + ";Initial Catalog=" + clspublicvariable.GENONLINEDBNAME + ";Persist Security Info=True;User ID=" + clspublicvariable.GENONLINELOGIN + ";Password=" + clspublicvariable.GENONLINEPASSWORD;
            clspublicvariable.Connstr_LocalDb = "Data Source=" + clspublicvariable.GENLOCALSERVERNAME + ";Initial Catalog=" + clspublicvariable.GENLOCALDBNAME + ";Persist Security Info=True;User ID=" + clspublicvariable.GENLOCALLOGIN + ";Password=" + clspublicvariable.GENLOCALPASSWORD;

            clsgen.FillSTARTDATE();

            if (clspublicvariable.ISAUTOSTART)
            {
                //this.Process2Start();
                this.Instance.Startprocess = true;
                this.Process_Start_Send_data_Call1();
            }
            //if (File.Exists((@clspublicvariable.AppPath + "\\Image\\Home.jpg")))
            //{
            //    this.pnlright.BackgroundImage = new Bitmap(@clspublicvariable.AppPath + "\\Image\\Home.jpg");
            //}

        }

        private void mdimain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private bool Arrage_pnlLog()
        {
            try
            {
                this.pnlcheckconnection.Visible = false;
                this.pnllog.Visible = true;
                this.pnllog.BringToFront();

                this.txtlog.Top = this.pnlright.Top + 40;
                this.txtlog.Width = this.pnlright.Width - 15;
                this.txtlog.Height = this.pnlright.Height - 90;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Process_Start_Send_data_Call1()
        {
            try
            {
                this.txtlog.Text = "";
                this.Write_LogFile("START DATA PROCESS-------------------------------- 1 @ " + DateTime.Now.ToString(), Color.Blue);
                Application.DoEvents();
                this.Arrage_pnlLog();

                this.ProcessStart();
                //this.ProcessStop();
                this.Process_Start_Send_data_Call1();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Process_Start_Send_data_Call2()
        {
            try
            {
                this.txtlog.Text = "";
                this.Write_LogFile("START DATA PROCESS-------------------------------- 2 @ " + DateTime.Now.ToString(), Color.Blue);
                this.Arrage_pnlLog();
                this.ProcessStart();
                //this.ProcessStop();
                this.Process_Start_Send_data_Call1();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private void btnstart_Click(object sender, EventArgs e)
        {
            //this.txtlog.Text = "";
            //this.Arrage_pnlLog();
            //this.ProcessStart();
            //this.btnstop_Click(sender, e);
            //if (clsonlinedb.CheckValidity())
            //{
                this.Instance.Startprocess = true;
                this.Process_Start_Send_data_Call1();
            //}
            //else
            //{
            //    this.txtlog.Text = "";
            //    this.Write_LogFile("------------ VALIDITY EXPIRE. -----------------" + DateTime.Now.ToString(), Color.Red);
            //    Application.DoEvents();
            //    this.Arrage_pnlLog();
            //}
        }

        private bool ProcessStart()
        {
            string retval = "";
            try
            {
                Application.DoEvents();

                if (this.Instance.Startprocess)
                {
                    this.Write_LogFile("START DATA PROCESS @ " + DateTime.Now.ToString(), Color.Blue);
                    Application.DoEvents();
                    //this.Instance.Startprocess = true;

                    // --------- SEND MASTER DATA

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTCITY
                        retval = "";
                        this.Write_LogFile("START SENDATA_ONLINE_MSTCITY @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTCITY();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTCITY @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTCOUNTRY
                        retval = "";
                        this.Write_LogFile("START SENDATA_ONLINE_MSTCOUNTRY @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTCOUNTRY();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTCOUNTRY @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }
                    if (this.Instance.Startprocess)
                    {
                        // -- MSTSTATE
                        retval = "";
                        this.Write_LogFile("START SENDATA_ONLINE_MSTSTATE @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTSTATE();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTSTATE @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTUNIT
                        retval = "";
                        this.Write_LogFile("START SENDATA_ONLINE_MSTUNIT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTUNIT();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTUNIT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTDEPT
                        this.Write_LogFile("START SENDATA_ONLINE_MSTDEPT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTDEPT();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTDEPT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTUSERS
                        this.Write_LogFile("START SENDATA_ONLINE_MSTUSERS @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTUSERS();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTUSERS @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTREPORTDEPT
                        this.Write_LogFile("START SENDATA_ONLINE_MSTREPORTDEPT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTREPORTDEPT();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTREPORTDEPT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTHSNCODE
                        this.Write_LogFile("START SENDATA_ONLINE_MSTHSNCODE @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTHSNCODE();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTHSNCODE @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTITEMGROUP
                        this.Write_LogFile("START SENDATA_ONLINE_MSTITEMGROUP @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTITEMGROUP();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTITEMGROUP @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTITEM
                        this.Write_LogFile("START SENDATA_ONLINE_MSTITEM @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTITEM();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTITEM @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTITEMPRICELIST
                        this.Write_LogFile("START SENDATA_ONLINE_MSTITEMPRICELIST @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTITEMPRICELIST();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTITEMPRICELIST @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();

                        // -- MSTITEMPRICELISTDTL
                        this.Write_LogFile("START SENDATA_ONLINE_MSTITEMPRICELISTDTL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTITEMPRICELISTDTL();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTITEMPRICELISTDTL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTREMARK
                        this.Write_LogFile("START SENDATA_ONLINE_MSTREMARK @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTREMARK();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTREMARK @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTTABLE
                        this.Write_LogFile("START SENDATA_ONLINE_MSTTABLE @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTTABLE();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTTABLE @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTEMPCAT
                        this.Write_LogFile("START SENDATA_ONLINE_MSTEMPCAT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTEMPCAT();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTEMPCAT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTEMP
                        this.Write_LogFile("START SENDATA_ONLINE_MSTEMP @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTEMP();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTEMP @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();

                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTCUST
                        this.Write_LogFile("START SENDATA_ONLINE_MSTCUST @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTCUST();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTCUST @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTEXPENCES
                        this.Write_LogFile("START SENDATA_ONLINE_MSTEXPENCES @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTEXPENCES();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTEXPENCES @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTINCOME
                        this.Write_LogFile("START SENDATA_ONLINE_MSTINCOME @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTINCOME();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTINCOME @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- MSTTIEUPCOMPANY
                        this.Write_LogFile("START SENDATA_ONLINE_MSTTIEUPCOMPANY @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTTIEUPCOMPANY();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTTIEUPCOMPANY @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }


                    if (this.Instance.Startprocess)
                    {
                        // INVENTORY MENU
                        // -- MSTSUPPLIER
                        this.Write_LogFile("START SENDATA_ONLINE_MSTSUPPLIER @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_MSTSUPPLIER();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_MSTSUPPLIER @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }


                    // TRANSACTION MENU

                    if (this.Instance.Startprocess)
                    {
                        // -- KOT
                        this.Write_LogFile("START SENDATA_ONLINE_KOT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_KOT();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_KOT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();

                        // -- KOTDTL
                        this.Write_LogFile("START SENDATA_ONLINE_KOTDTL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_KOTDTL();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_KOTDTL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- BILL
                        this.Write_LogFile("START SENDATA_ONLINE_BILL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_BILL();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_BILL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();

                        // -- BILLDTL
                        this.Write_LogFile("START SENDATA_ONLINE_BILLDTL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_BILLDTL();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_BILLDTL @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- SETTLEMENT
                        this.Write_LogFile("START SENDATA_ONLINE_SETTLEMENT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_SETTLEMENT();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_SETTLEMENT @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- CASHONHAND
                        this.Write_LogFile("START SENDATA_ONLINE_CASHONHAND @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_CASHONHAND();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_CASHONHAND @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- EXPENCES
                        this.Write_LogFile("START SENDATA_ONLINE_EXPENCES @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_EXPENCES();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_EXPENCES @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- INCOME
                        this.Write_LogFile("START SENDATA_ONLINE_INCOME @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_INCOME();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_INCOME @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- OPENING CASH
                        this.Write_LogFile("START SENDATA_ONLINE_OPCASH @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_OPCASH();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_OPCASH @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                    if (this.Instance.Startprocess)
                    {
                        // -- TABLERESERVATION
                        this.Write_LogFile("START SENDATA_ONLINE_TABLERESERVATION @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                        retval = clsonlinedb.SENDATA_ONLINE_TABLERESERVATION();
                        if (retval != "")
                        {
                            this.Write_LogFile(retval + DateTime.Now.ToString(), Color.Blue);
                            Application.DoEvents();
                        }
                        this.Write_LogFile("END SENDATA_ONLINE_TABLERESERVATION @ " + DateTime.Now.ToString(), Color.Blue);
                        Application.DoEvents();
                    }

                }


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool PrintLog(string strprint1)
        {
            try
            {
                this.txtlog.Text = strprint1 + Environment.NewLine + this.txtlog.Text;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void txtlog_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btncheckconnection_Click(object sender, EventArgs e)
        {
            this.Arrage_pnlcheckconnection();
            this.Fill_pnlcheckconnection();
        }

        private bool Arrage_pnlcheckconnection()
        {
            try
            {
                this.pnllog.Visible = false;
                this.pnlcheckconnection.Visible = true;
                this.pnlcheckconnection.BringToFront();

                this.pnlcheckconnection.Top = this.pnlright.Top + 5;
                this.pnlcheckconnection.Width = this.pnlright.Width - 15;
                this.pnlcheckconnection.Height = this.pnlright.Height - 70;

                return true;
            }
            catch (Exception)
            {
                return false; ;
            }
        }

        private bool Fill_pnlcheckconnection()
        {
            try
            {
                this.txtlocalservername.Text = clspublicvariable.GENLOCALSERVERNAME;
                this.txtlocaldbname.Text = clspublicvariable.GENLOCALDBNAME;
                this.txtlocallogin.Text = clspublicvariable.GENLOCALLOGIN;
                this.txtlocalpassword.Text = clspublicvariable.GENLOCALPASSWORD;
                this.txtlocalport.Text = clspublicvariable.GENLOCALPORT;

                this.txtonlineservername.Text = clspublicvariable.GENONLINESERVERNAME;
                this.txtonlinedbname.Text = clspublicvariable.GENONLINEDBNAME;
                this.txtonlinelogin.Text = clspublicvariable.GENONLINELOGIN;
                this.txtonlinepassword.Text = clspublicvariable.GENONLINEPASSWORD;
                this.txtonlineport.Text = clspublicvariable.GENONLINEPORT;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnClosePanelcheckconnection_Click(object sender, EventArgs e)
        {
            this.pnlcheckconnection.Visible = false;
        }

        private void btncloselogpanel_Click(object sender, EventArgs e)
        {
            this.pnllog.Visible = false;
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            this.Instance.Startprocess = false;
            this.Arrage_pnlLog();
            //this.ProcessStop();
            this.pnllog.Visible = false;
        }

        private bool ProcessStop()
        {
            try
            {
                if (this.Instance.Startprocess)
                {
                    this.Write_LogFile("STOP DATA PROCESS @ " + DateTime.Now.ToString(), Color.Red);
                    this.Instance.Startprocess = false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Process2Start()
        {
            try
            {
                this.Instance.Startprocess = true;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Write_LogFile(string text, Color color)
        {
            try
            {
                if (clspublicvariable.GENDISPLOG == "1")
                {
                    if (text + "" != "")
                    {
                        this.txtlog.SelectionStart = txtlog.TextLength;
                        this.txtlog.SelectionLength = 0;
                        this.txtlog.SelectionColor = color;
                        this.txtlog.Text = text + "";
                        this.txtlog.SelectionColor = txtlog.ForeColor;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnchecklocaldata_Click(object sender, EventArgs e)
        {
            bool retval1;
            try
            {
                retval1 = clsgen.Check_LocalConnection(this.txtlocalservername.Text.Trim(), this.txtlocaldbname.Text.Trim(), this.txtlocallogin.Text.Trim(), this.txtlocalpassword.Text.Trim(), this.txtlocalport.Text.Trim());

                if (retval1)
                {
                    MessageBox.Show("Local Connection Sucessfully.", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            catch (Exception)
            { }
        }

        private void btncheckonlinedata_Click(object sender, EventArgs e)
        {
            bool retval1;
            try
            {
                retval1 = clsgen.Check_OnlineConnection(this.txtonlineservername.Text.Trim(), this.txtonlinedbname.Text.Trim(), this.txtonlinelogin.Text.Trim(), this.txtonlinepassword.Text.Trim(), this.txtonlineport.Text.Trim());

                if (retval1)
                {
                    MessageBox.Show("Online Connection Sucessfully.", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            catch (Exception)
            { }
        }

        private void btnUpdatestructure_Click(object sender, EventArgs e)
        {
            frmupdatestructure frmupsdate = new frmupdatestructure();
            frmupsdate.Instance.ShowDialog();
        }

        private void startProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Process_Start_Send_data_Call1();
        }

        private void stopProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Arrage_pnlLog();
            this.ProcessStop();
            this.pnllog.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //  Application.Exit();
                System.Environment.Exit(0);
            }
            catch (Exception)
            { }
        }

        private void btndelmasterdata_Click(object sender, EventArgs e)
        {
            try
            {
                this.Delete_Master_Data();
                this.Update_Local_Master_Data();

                MessageBox.Show("Master History Delete Sucessfully", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private bool Delete_Master_Data()
        {
            string str1 = "";

            try
            {
                str1 = " Delete from MSTCOUNTRY ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTSTATE ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTUNIT ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTDEPT ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTUSERS ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTREPORTDEPT ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTHSNCODE ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTITEMGROUP ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTITEM ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTITEMPRICELIST ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTITEMPRICELISTDTL ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTREMARK ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTTABLE ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTEMPCAT ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTEMP ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTCUST ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTTIEUPCOMPANY ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = " Delete from MSTSUPPLIER ";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_Local_Master_Data()
        {
            string str1 = "";

            try
            {
                str1 = " UPDATE MSTCOUNTRY SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTSTATE SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTUNIT SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTDEPT SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTUSERS SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTREPORTDEPT SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTHSNCODE SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTITEMGROUP SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTITEM SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = "UPDATE MSTITEMPRICELIST SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTITEMPRICELISTDTL SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTREMARK SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTTABLE SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTEMPCAT SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTEMP SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTCUST SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTTIEUPCOMPANY SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = " UPDATE MSTSUPPLIER SET SENDDATA=0";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Process_Delete_Onlinedata()
        {
            try
            {
                this.Delete_Settlement_Data();
                this.Delete_KotHistory_Data();
                this.Delete_Billing_Data();
                this.Delete_Purchase_Data();
                this.Delete_Purchase_Payment_Data();
                this.Delete_Income_Data();
                this.Delete_Expence_Data();
                this.Delete_CASHONHAND_Data();
                this.Delete_OPCASH_Data();


                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool Process_Update_Localdata()
        {
            try
            {
                this.Update_Settlement_Data();
                this.Update_KotHistory_Data();
                this.Update_Billing_Data();
                this.Update_Purchase_Data();
                this.Update_Purchase_Payment_Data();
                this.Update_Income_Data();
                this.Update_Expence_Data();
                this.Update_CASHONHAND_Data();
                this.Update_OPCASH_Data();

                MessageBox.Show("History Delete Sucessfully", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #region DeleteTransation

        private void cmbperiod_Click(object sender, EventArgs e)
        {
            try
            {
                this.dtpFrom.Visible = false;
                this.dtpTo.Visible = false;
                this.lblfrom.Visible = false;
                this.lblto.Visible = false;

                if (this.cmbperiod.Text == "Select Period")
                {
                    this.dtpFrom.Visible = true;
                    this.dtpTo.Visible = true;
                    this.lblfrom.Visible = true;
                    this.lblto.Visible = true;
                }
            }

            catch (Exception)
            {

            }
        }

        private string GetDateRange(string fldnm1)
        {
            string wStr;
            //string strqty1 = "";
            string strsummaryinfo = "";
            try
            {
                wStr = "";
                strsummaryinfo = "";

                DateTime firstOfNextMonth = new DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1).AddMonths(1);
                DateTime firstOfNextYear = new DateTime(System.DateTime.Today.Year, 1, 1).AddYears(1);

                DateTime Monthdate1 = new DateTime(System.DateTime.Today.Year, System.DateTime.Today.Month, 1);
                DateTime Monthdate2 = firstOfNextMonth.AddDays(-1);

                DateTime Yeardate1 = new DateTime(System.DateTime.Today.Year, 1, 1);
                DateTime Yeardate2 = firstOfNextYear.AddDays(-1);

                DateTime LastMonth1;
                DateTime LastMonth2;

                if (System.DateTime.Today.Month.ToString() != "1")
                {
                    LastMonth1 = new DateTime(System.DateTime.Today.Year, (System.DateTime.Today.Month - 1), 1);
                    LastMonth2 = new DateTime(System.DateTime.Today.Year, (System.DateTime.Today.Month - 1), DateTime.DaysInMonth(System.DateTime.Today.Year, System.DateTime.Today.Month - 1));
                    strsummaryinfo = "Summary Information From " + LastMonth1.ToString("dd/MM/yyyy") + " To " + LastMonth2.ToString("dd/MM/yyyy");
                }
                else
                {
                    LastMonth1 = new DateTime(System.DateTime.Today.Year - 1, 12, 1);
                    LastMonth2 = new DateTime(System.DateTime.Today.Year - 1, 12, DateTime.DaysInMonth(System.DateTime.Today.Year, 12));
                    strsummaryinfo = "Summary Information From " + LastMonth1.ToString("dd/MM/yyyy") + " To " + LastMonth2.ToString("dd/MM/yyyy");
                }

                switch (cmbperiod.Text)
                {
                    case "Today 's":
                        wStr = (wStr + (fldnm1 + "="
                                    + (clspublicvariable.DateCriteriaconst
                                    + (System.DateTime.Now.Date.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst))));
                        strsummaryinfo = "Summary Information of " + (System.DateTime.Now.Date.ToString("dd/MM/yyyy"));
                        break;

                    case "This Week":
                        wStr = (wStr + (fldnm1 + " >="
                                    + (clspublicvariable.DateCriteriaconst
                                    + System.DateTime.Now.Date.AddDays(0 - System.DateTime.Now.Date.DayOfWeek).ToString("dd-MMM-yyyy")
                                    + (clspublicvariable.DateCriteriaconst + (" and " + (fldnm1 + " <= "
                                    + (clspublicvariable.DateCriteriaconst
                                    + (System.DateTime.Now.Date.AddDays(Convert.ToDouble(6 - System.DateTime.Now.Date.DayOfWeek)).ToString("dd-MMM-yyyy")) + clspublicvariable.DateCriteriaconst)))))));

                        strsummaryinfo = "Summary Information From " + System.DateTime.Now.Date.AddDays(0 - System.DateTime.Now.Date.DayOfWeek).ToString("dd/MM/yyyy") + " To " + (System.DateTime.Now.Date.AddDays(Convert.ToDouble(6 - System.DateTime.Now.Date.DayOfWeek)).ToString("dd/MM/yyyy"));
                        break;

                    case "This Month":
                        wStr = (wStr + (fldnm1 + " >="
                                    + (clspublicvariable.DateCriteriaconst
                                    + (Monthdate1.ToString("dd-MMM-yyyy"))
                                    + (clspublicvariable.DateCriteriaconst + (" and " + (fldnm1 + " <= "
                                    + (clspublicvariable.DateCriteriaconst
                                    + (Monthdate2.ToString("dd-MMM-yyyy")) + clspublicvariable.DateCriteriaconst)))))));

                        strsummaryinfo = "Summary Information From " + Monthdate1.ToString("dd/MM/yyyy") + " To " + Monthdate2.ToString("dd/MM/yyyy");
                        break;

                    case "This Year":
                        wStr = (wStr + (fldnm1 + " >="
                                    + (clspublicvariable.DateCriteriaconst
                                    + (Yeardate1.ToString("dd-MMM-yyyy"))
                                    + (clspublicvariable.DateCriteriaconst + (" and " + (fldnm1 + " <= "
                                    + (clspublicvariable.DateCriteriaconst
                                    + (Yeardate2.ToString("dd-MMM-yyyy")) + clspublicvariable.DateCriteriaconst)))))));

                        strsummaryinfo = "Summary Information From " + Yeardate1.ToString("dd/MM/yyyy") + " To " + Yeardate2.ToString("dd/MM/yyyy");

                        break;

                    case "Last Week":
                        wStr = (wStr + (fldnm1 + " >="
                                   + (clspublicvariable.DateCriteriaconst
                                   + (System.DateTime.Now.Date.AddDays(-6).ToString("dd-MMM-yyyy"))
                                   + (clspublicvariable.DateCriteriaconst + (" and " + (fldnm1 + " <= "
                                   + (clspublicvariable.DateCriteriaconst
                                   + (System.DateTime.Now.Date.ToString("dd-MMM-yyyy")) + clspublicvariable.DateCriteriaconst)))))));

                        strsummaryinfo = "Summary Information From " + (System.DateTime.Now.Date.AddDays(-6).ToString("dd/MM/yyyy")) + " To " + (System.DateTime.Now.Date.ToString("dd/MM/yyyy"));

                        break;

                    case "Last One Month":
                        wStr = (wStr + (fldnm1 + " >="
                                    + (clspublicvariable.DateCriteriaconst
                                    + (LastMonth1.ToString("dd-MMM-yyyy"))
                                    + (clspublicvariable.DateCriteriaconst + (" and " + (fldnm1 + " <= "
                                    + (clspublicvariable.DateCriteriaconst
                                    + (LastMonth2.ToString("dd-MMM-yyyy")) + clspublicvariable.DateCriteriaconst)))))));

                        strsummaryinfo = "Summary Information From " + (LastMonth1.ToString("dd/MM/yyyy")) + " To " + (LastMonth2.ToString("dd/MM/yyyy"));

                        break;

                    case "Select Period":
                        wStr = (wStr + (fldnm1 + " >="
                                    + (clspublicvariable.DateCriteriaconst
                                    + (this.dtpFrom.Value.ToString("dd-MMM-yyyy")
                                    + (clspublicvariable.DateCriteriaconst + (" and " + (fldnm1 + " <= "
                                    + (clspublicvariable.DateCriteriaconst
                                    + (this.dtpTo.Value.ToString("dd-MMM-yyyy") + clspublicvariable.DateCriteriaconst)))))))));

                        strsummaryinfo = "Summary Information From " + (this.dtpFrom.Value.ToString("dd/MM/yyyy")) + " To " + (this.dtpTo.Value.ToString("dd/MM/yyyy"));

                        break;
                    case "All":
                        wStr = "(1=1)";
                        strsummaryinfo = "All Summary Information";
                        break;
                }

                //this.lblsummaryinfo.Text = strsummaryinfo;
                return wStr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetDateRange())");
                return "";
            }
        }

        private bool Delete_Settlement_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("setledate");

                str1 = " Delete from settlement " +
                       " where  " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_Settlement_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("setledate");

                str1 = " UPDATE settlement SET SENDDATA=0" +
                       " where  " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_KotHistory_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("ADATETIME");

                str1 = " Delete from KOTHISTORY " +
                       " where  " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_KotHistory_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("ADATETIME");

                str1 = " UPDATE KOTHISTORY SET SENDDATA=0 " +
                       " where  " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_Billing_Data()
        {
            string str1 = "";
            string wstr_date1 = "";
            DataTable dtbill = new DataTable();
            try
            {

                str1 = "";
                wstr_date1 = this.GetDateRange("bill.billdate");
                str1 = "DELETE FROM BILLDTL WHERE " +
                        " billdtl.BILLRID IN (SELECT RID From Bill WHERE " + wstr_date1 + " )";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                str1 = "";
                wstr_date1 = this.GetDateRange("billdate");
                str1 = "Delete From Bill " +
                        " where " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);


                wstr_date1 = this.GetDateRange("kot.kotdate");
                str1 = "DELETE FROM KOTDTL WHERE " +
                        " KOTDTL.KOTRID IN (SELECT RID From KOT WHERE " + wstr_date1 + " )";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("kotdate");
                str1 = "Delete From kot " +
                        " where " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_Billing_Data()
        {
            string str1 = "";
            string wstr_date1 = "";
            DataTable dtbill = new DataTable();
            try
            {

                str1 = "";
                wstr_date1 = this.GetDateRange("bill.billdate");
                str1 = "UPDATE BILLDTL SET SENDDATA = 0 WHERE " +
                        " billdtl.BILLRID IN (SELECT RID From Bill WHERE " + wstr_date1 + " )";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                str1 = "";
                wstr_date1 = this.GetDateRange("billdate");
                str1 = "UPDATE Bill SET SENDDATA = 0 " +
                        " where " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);


                wstr_date1 = this.GetDateRange("kot.kotdate");
                str1 = "UPDATE KOTDTL SET SENDDATA = 0 WHERE " +
                        " KOTDTL.KOTRID IN (SELECT RID From KOT WHERE " + wstr_date1 + " )";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("kotdate");
                str1 = "UPDATE kot SET SENDDATA = 0" +
                        " where " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_Purchase_Data()
        {
            string str1 = "";
            string wstr_date1 = "";
            DataTable dtbill = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = this.GetDateRange("ITEMPURCHASE.PURDATE");
                str1 = "Delete From ITEMPURCHASEDTL " +
                        " WHERE ITEMPURCHASEDTL.PURRID IN (SELECT RID FROM ITEMPURCHASE WHERE " + wstr_date1 + " )";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("PURDATE");
                str1 = "Delete From ITEMPURCHASE " +
                        " where " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("STOCKISSUE.ISSUEDATE");
                str1 = "Delete From STOCKISSUEDTL " +
                         " WHERE STOCKISSUEDTL.ISSUERID IN (SELECT RID FROM STOCKISSUE WHERE " + wstr_date1 + " )";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("ISSUEDATE");
                str1 = "Delete From STOCKISSUE " +
                        " where " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("STOCKCLOSING.CLDATE");
                str1 = "Delete From STOCKCLOSINGDTL " +
                       " WHERE STOCKCLOSINGDTL.CLRID IN (SELECT RID FROM STOCKCLOSING WHERE " + wstr_date1 + " )";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("CLDATE");
                str1 = "Delete From STOCKCLOSING " +
                        " where " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("STOCKWASTAGE.WSTDATE");
                str1 = "Delete From STOCKWASTAGEDTL " +
                        "  WHERE STOCKWASTAGEDTL.WSTRID IN (SELECT RID FROM STOCKWASTAGE WHERE " + wstr_date1 + " )";
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("WSTDATE");
                str1 = "Delete From STOCKWASTAGE " +
                        " where " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_Purchase_Data()
        {
            string str1 = "";
            string wstr_date1 = "";
            DataTable dtbill = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = this.GetDateRange("ITEMPURCHASE.PURDATE");
                str1 = "UPDATE ITEMPURCHASEDTL SET SENDDATA=0 " +
                        " WHERE ITEMPURCHASEDTL.PURRID IN (SELECT RID FROM ITEMPURCHASE WHERE " + wstr_date1 + " )";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("PURDATE");
                str1 = "UPDATE ITEMPURCHASE SET SENDDATA=0 " +
                        " where " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("STOCKISSUE.ISSUEDATE");
                str1 = "UPDATE STOCKISSUEDTL SET SENDDATA=0 " +
                         " WHERE STOCKISSUEDTL.ISSUERID IN (SELECT RID FROM STOCKISSUE WHERE " + wstr_date1 + " )";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("ISSUEDATE");
                str1 = "UPDATE STOCKISSUE SET SENDDATA=0 " +
                        " where " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("STOCKCLOSING.CLDATE");
                str1 = "UPDATE STOCKCLOSINGDTL SET SENDDATA=0 " +
                       " WHERE STOCKCLOSINGDTL.CLRID IN (SELECT RID FROM STOCKCLOSING WHERE " + wstr_date1 + " )";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("CLDATE");
                str1 = "UPDATE STOCKCLOSING SET SENDDATA=0 " +
                        " where " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("STOCKWASTAGE.WSTDATE");
                str1 = "UPDATE STOCKWASTAGEDTL SET SENDDATA=0 " +
                        "  WHERE STOCKWASTAGEDTL.WSTRID IN (SELECT RID FROM STOCKWASTAGE WHERE " + wstr_date1 + " )";
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                wstr_date1 = this.GetDateRange("WSTDATE");
                str1 = "UPDATE STOCKWASTAGE SET SENDDATA=0 " +
                        " where " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_Purchase_Payment_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("PAYDATE");

                str1 = " Delete from PAYMENTINFO " +
                       " where  " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_Purchase_Payment_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("PAYDATE");

                str1 = " update PAYMENTINFO SET SENDDATA=0 " +
                       " where  " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_Income_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("INDATE");

                str1 = " Delete from INCOME " +
                       " where  " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_Income_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("INDATE");

                str1 = " UPDATE INCOME SET SENDDATA=0 " +
                       " where  " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_Expence_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("EXDATE");

                str1 = " Delete from EXPENCE " +
                       " where  " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_Expence_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("EXDATE");

                str1 = " update EXPENCE set SENDDATA=0" +
                       " where  " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_CASHONHAND_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("CASHDATE");

                str1 = " Delete from CASHONHAND " +
                       " where  " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_CASHONHAND_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("CASHDATE");

                str1 = " UPDATE CASHONHAND SET SENDDATA = 0 " +
                       " where  " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Delete_OPCASH_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("OPCASHDATE");

                str1 = " Delete from OPCASH " +
                       " where  " + wstr_date1;
                clsonlinedb.ExecuteMsSqlOnlineCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool Update_OPCASH_Data()
        {
            string str1 = "";
            string wstr_date1 = "";

            DataTable dtsetlement = new DataTable();
            try
            {
                str1 = "";
                wstr_date1 = "";
                wstr_date1 = this.GetDateRange("OPCASHDATE");

                str1 = " UPDATE OPCASH SET SENDDATA = 0 " +
                       " where  " + wstr_date1;
                clslocaldb.ExecuteMsSqlLocalCommand_NoMsg(str1);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private void btndeletetransdata_Click(object sender, EventArgs e)
        {
            this.Process_Delete_Onlinedata();

            if (this.chkupdatelocaldata.CheckState == CheckState.Checked)
            {
                this.Process_Update_Localdata();
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.exitToolStripMenuItem_Click(sender, e);
        }

    }
}
