using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using System.Resources;

namespace SENDDATARMSGST
{
    public class clspublicvariable
    {
        public static string AppPath = Path.GetDirectoryName(Application.ExecutablePath).ToString();
        public static DateTime GENSTARTDATE;
        public static long LoginUserId;
        public static string LoginUserName;
        public static DateTime LoginDatetime;
        public static string Project_Title = "SEND DATA";
        public static string DateCriteriaconst = "'";
        public static string ActualFilePath = AppPath + "\\SENDDATARMSGST.exe";
        public static string SendDataVer = System.Diagnostics.FileVersionInfo.GetVersionInfo(ActualFilePath).FileVersion.ToString();
        public static string VALIDITYTYPEVALUE = "RMS@PRITESH";
        public static Int64 AMCDAYS = 365;
        public static string Connstr_OnlineDb = "";
        public static string Connstr_LocalDb = "";       

        public static string GENLOCALSERVERNAME = "";
        public static string GENLOCALDBNAME = "";
        public static string GENLOCALLOGIN = "";
        public static string GENLOCALPASSWORD = "";
        public static string GENLOCALPORT = "";
        public static string GENONLINESERVERNAME = "";
        public static string GENONLINEDBNAME = "";
        public static string GENONLINELOGIN = "";
        public static string GENONLINEPASSWORD = "";
        public static string GENONLINEPORT = "";
        public static bool ISAUTOSTART = false;
        public static string GENDISPLOG = "";

        public static string GENMAINSERVERNAME = "43.231.124.94";
        public static string GENMAINDBNAME = "ONLINERMS";
        public static string GENMAINLOGIN = "pritesh";
        public static string GENMAINPASSWORD = "prit.rms123";
        public static string GENMAINPORT = "";

        
    }
}
