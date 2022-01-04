using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;

namespace SENDDATARMSGST
{
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
            this.txtusername.Enter += new System.EventHandler(this.textBox_Enter);
            this.txtusername.Leave += new System.EventHandler(this.textBox_Leave);
            this.txtuserpassword.Enter += new System.EventHandler(this.textBox_Enter);
            this.txtuserpassword.Leave += new System.EventHandler(this.textBox_Leave);
        }

        #region CommonFunction

        private void textBox_Enter(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            a.BackColor = Color.Yellow;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            TextBox a = (TextBox)sender;
            a.BackColor = SystemColors.Window;
        }

        private void combobox_Enter(object sender, EventArgs e)
        {
            ComboBox a = (ComboBox)sender;
            a.BackColor = Color.Yellow;
        }

        private void combobox_Leave(object sender, EventArgs e)
        {
            ComboBox a = (ComboBox)sender;
            a.BackColor = SystemColors.Window;
        }

        private void Datetimepicker_Enter(object sender, EventArgs e)
        {
            DateTimePicker a = (DateTimePicker)sender;
            a.BackColor = Color.Yellow;
        }

        private void Datetimepicker_Leave(object sender, EventArgs e)
        {
            DateTimePicker a = (DateTimePicker)sender;
            a.BackColor = SystemColors.Window;
        }

        #endregion


        private bool checkReqValue()
        {
            try
            {
                if (this.txtusername.Text.Trim() == "")
                {
                    MessageBox.Show("User Name Cannot be Blank.", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtusername.Focus();
                    return false;
                }

                if (this.txtuserpassword.Text.Trim() == "")
                {
                    MessageBox.Show("Password Cannot be Blank.", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtuserpassword.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in checkReqValue())");
                return false;
            }
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkReqValue())
                {
                    clsLoginBal Loginbal = new clsLoginBal();

                    //MessageBox.Show("Before isValidUser");

                    if (Loginbal.isValidUser(this.txtusername.Text.Trim(), this.txtuserpassword.Text.Trim()))
                    {
                        // Check For RMS VALIDITY
                        if (clsgeneral.CHEKC_RMS_VALIDITY())
                        {
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("FATAL ERROR OCCURED. Something is miss behave.", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();

                            //this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid UserName or Passsword.", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in btnok_Click())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnkeyboard_Click(object sender, EventArgs e)
        {
            try
            {
                //System.Diagnostics.Process.Start("osk.exe");
                clsgeneral.StartOSK();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " Error occures in btnkeyboard_Click())", clspublicvariable.Project_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }       
    }
}
