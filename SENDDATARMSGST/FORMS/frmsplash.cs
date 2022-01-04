using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SENDDATARMSGST
{
    public partial class frmsplash : Form
    {
        public frmsplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.Visible = false;

            /// Show MDI Main Form
            mdimain MdiMain1 = new mdimain();
            MdiMain1.Instance.Show();
            //MdiMain1.Show();
            // this.Close();

        }

        private void frmsplash_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists((@clspublicvariable.AppPath + "\\Image\\Home.jpg")))
                {
                    this.BackgroundImage = new Bitmap(@clspublicvariable.AppPath + "\\Image\\Home.jpg");
                }

                this.timer1.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}
