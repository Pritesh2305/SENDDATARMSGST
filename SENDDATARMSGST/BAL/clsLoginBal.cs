using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SENDDATARMSGST
{
    class clsLoginBal
    {
        private string _username = "";
        private string _password = "";
        clsLoginDal Logindal = new clsLoginDal();

        public string Username
        {
            get { return _username; }
            set
            {
                if (value.Length == 0)
                {
                    throw new Exception("Username Cannot Be Blank.");
                }

                this._username = value;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value.Length == 0)
                {
                    throw new Exception("Password Cannot Be Blank.");
                }
                this._password = value;
            }
        }

        public DataTable GetTableData(string username1)
        {

            DataTable dt = new DataTable();
            try
            {
                dt = Logindal.GetTableData(username1);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        public bool isValidUser(string usernm1, string password1)
        {
            clsLoginDal Logindal = new clsLoginDal();
            DataTable dt = new DataTable();
            bool Userstatus;
            Userstatus = false;

            try
            {
                dt = Logindal.GetTableData(usernm1);

                if (dt.Rows.Count <= 0)
                {
                    Userstatus = false;
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string pass1 = dr["Password"] + "".ToString();
                        string istableuser1 = dr["istabletuser"] + "".ToString();

                        if (pass1 == password1)
                        {
                            long uid;
                            string unm1 = "";
                            string showrpt = "";

                            unm1 = dr["UserName"] + "".ToString();
                            showrpt = dr["ISSHOWREPORT"] + "".ToString();

                            long.TryParse(dr["RID"].ToString(), out uid);

                            clspublicvariable.LoginUserId = uid;
                            clspublicvariable.LoginUserName = unm1;
                            clspublicvariable.LoginDatetime = System.DateTime.Now;
                            
                            Userstatus = true;


                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                Userstatus = false;
            }

            return Userstatus;
        }
    }
}
