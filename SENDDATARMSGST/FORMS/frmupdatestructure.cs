using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SENDDATARMSGST
{
    public partial class frmupdatestructure : Form
    {
        private clsupdateonlinestructure clsupdateonlinedb = new clsupdateonlinestructure();

        private static frmupdatestructure _instance;

        public frmupdatestructure Instance
        {
            get
            {
                if (frmupdatestructure._instance == null)
                {
                    frmupdatestructure._instance = new frmupdatestructure();
                }

                return frmupdatestructure._instance;
            }
        }

        private bool PrintStatus(string strprint1)
        {
            try
            {
                this.txtupdatelog.Text = strprint1 + Environment.NewLine + this.txtupdatelog.Text;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public frmupdatestructure()
        {
            InitializeComponent();
        }

        private void txtupdatelog_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnstopupdatestructure_Click(object sender, EventArgs e)
        {
            frmupdatestructure._instance = null;
            this.Close();
        }

        private void btnstartupdatestructure_Click(object sender, EventArgs e)
        {
            // LOCAL DATABASE
            this.PrintStatus("( LOCAL DATA ) Process START : " + DateTime.Now.ToString());
            Application.DoEvents();
            this.PrintStatus("( LOCAL DATA ) Process END : " + DateTime.Now.ToString());

            // ONLINE DATABASE
            this.PrintStatus("[ ONLINE DATA ] Process START : " + DateTime.Now.ToString());
            Application.DoEvents();
            
            this.PrintStatus("[ ONLINE DATA ] START Create_OnlineDb_TABLE : " + DateTime.Now.ToString());
            Application.DoEvents();
            clsupdateonlinedb.Create_OnlineDb_Table();
            this.PrintStatus("[ ONLINE DATA ] END Create_OnlineDb_TABLE : " + DateTime.Now.ToString());
            Application.DoEvents();

            this.PrintStatus("[ ONLINE DATA ] START Alter_OnlineDb_TABLE : " + DateTime.Now.ToString());
            Application.DoEvents();
            clsupdateonlinedb.Alter_OnlineDb_Table();
            this.PrintStatus("[ ONLINE DATA ] END Alter_OnlineDb_TABLE : " + DateTime.Now.ToString());
            Application.DoEvents();

            this.PrintStatus("[ ONLINE DATA ] START Create_OnlineDb_TYPE : " + DateTime.Now.ToString());
            Application.DoEvents();
            clsupdateonlinedb.Create_OnlineDb_TYPE();
            this.PrintStatus("[ ONLINE DATA ] END Create_OnlineDb_TYPE : " + DateTime.Now.ToString());
            Application.DoEvents();
                       
            this.PrintStatus("[ ONLINE DATA ] START Create_OnlineDb_PROCEDURE : " + DateTime.Now.ToString());
            Application.DoEvents();
            clsupdateonlinedb.Create_OnlineDb_PROCEDURE();
            this.PrintStatus("[ ONLINE DATA ] END Create_OnlineDb_PROCEDURE : " + DateTime.Now.ToString());
            Application.DoEvents();

            this.PrintStatus("[ ONLINE DATA ] START Create_OnlineDb_STOREDPROCEDURE : " + DateTime.Now.ToString());
            Application.DoEvents();
            clsupdateonlinedb.Create_OnlineDb_STOREDPROCEDURE();
            this.PrintStatus("[ ONLINE DATA ] END Create_OnlineDb_STOREDPROCEDURE : " + DateTime.Now.ToString());
            Application.DoEvents();
            
            this.PrintStatus("[ ONLINE DATA ] START Create_OnlineDb_View : " + DateTime.Now.ToString());
            Application.DoEvents();
            clsupdateonlinedb.Create_OnlineDb_VIEW();
            this.PrintStatus("[ ONLINE DATA ] END Create_OnlineDb_View : " + DateTime.Now.ToString());
            Application.DoEvents();


            this.PrintStatus("[ ONLINE DATA ] Process END : " + DateTime.Now.ToString());
        }

        private void frmupdatestructure_Load(object sender, EventArgs e)
        {
            this.txtupdatelog.Text = "";
        }
    }
}
