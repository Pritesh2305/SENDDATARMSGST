using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SENDDATARMSGST
{
    class clsLoginDal
    {
        clsmssqldbfunction clsmssql = new clsmssqldbfunction();

        public DataTable GetTableData(string usename1)
        {
            DataTable dt = new DataTable();
            string str1;

            try
            {
                if (usename1.ToLower().Trim() == "all")
                {
                    str1 = "Select * from mstusers where isnull(delflg,0)=0";
                }
                else
                {
                    str1 = "Select * from mstusers where UserName = " + "'" + usename1 + "' And isnull(delflg,0)=0";
                }

                dt = clsmssql.FillDataTable(str1, "mstusers");

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in GetTableData())");
                return dt;
            }
        }
    }
}
