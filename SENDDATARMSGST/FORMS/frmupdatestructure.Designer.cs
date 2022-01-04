namespace SENDDATARMSGST
{
    partial class frmupdatestructure
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmupdatestructure));
            this.pnlmain = new System.Windows.Forms.Panel();
            this.pnlbutton = new System.Windows.Forms.Panel();
            this.btnstartupdatestructure = new System.Windows.Forms.Button();
            this.btnstopupdatestructure = new System.Windows.Forms.Button();
            this.label162 = new System.Windows.Forms.Label();
            this.pnlupdatestructure = new System.Windows.Forms.Panel();
            this.txtupdatelog = new System.Windows.Forms.TextBox();
            this.pnlmain.SuspendLayout();
            this.pnlbutton.SuspendLayout();
            this.pnlupdatestructure.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlmain
            // 
            this.pnlmain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlmain.Controls.Add(this.pnlbutton);
            this.pnlmain.Controls.Add(this.label162);
            this.pnlmain.Controls.Add(this.pnlupdatestructure);
            this.pnlmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlmain.Location = new System.Drawing.Point(0, 0);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(637, 488);
            this.pnlmain.TabIndex = 0;
            // 
            // pnlbutton
            // 
            this.pnlbutton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlbutton.Controls.Add(this.btnstartupdatestructure);
            this.pnlbutton.Controls.Add(this.btnstopupdatestructure);
            this.pnlbutton.Location = new System.Drawing.Point(4, 409);
            this.pnlbutton.Name = "pnlbutton";
            this.pnlbutton.Size = new System.Drawing.Size(628, 73);
            this.pnlbutton.TabIndex = 13;
            // 
            // btnstartupdatestructure
            // 
            this.btnstartupdatestructure.FlatAppearance.BorderSize = 0;
            this.btnstartupdatestructure.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstartupdatestructure.Image = ((System.Drawing.Image)(resources.GetObject("btnstartupdatestructure.Image")));
            this.btnstartupdatestructure.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnstartupdatestructure.Location = new System.Drawing.Point(0, 1);
            this.btnstartupdatestructure.Name = "btnstartupdatestructure";
            this.btnstartupdatestructure.Size = new System.Drawing.Size(97, 69);
            this.btnstartupdatestructure.TabIndex = 17;
            this.btnstartupdatestructure.Text = "START";
            this.btnstartupdatestructure.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnstartupdatestructure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnstartupdatestructure.UseVisualStyleBackColor = true;
            this.btnstartupdatestructure.Click += new System.EventHandler(this.btnstartupdatestructure_Click);
            // 
            // btnstopupdatestructure
            // 
            this.btnstopupdatestructure.FlatAppearance.BorderSize = 0;
            this.btnstopupdatestructure.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstopupdatestructure.Image = ((System.Drawing.Image)(resources.GetObject("btnstopupdatestructure.Image")));
            this.btnstopupdatestructure.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnstopupdatestructure.Location = new System.Drawing.Point(528, 1);
            this.btnstopupdatestructure.Name = "btnstopupdatestructure";
            this.btnstopupdatestructure.Size = new System.Drawing.Size(97, 69);
            this.btnstopupdatestructure.TabIndex = 17;
            this.btnstopupdatestructure.Text = "CLOSE";
            this.btnstopupdatestructure.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnstopupdatestructure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnstopupdatestructure.UseVisualStyleBackColor = true;
            this.btnstopupdatestructure.Click += new System.EventHandler(this.btnstopupdatestructure_Click);
            // 
            // label162
            // 
            this.label162.BackColor = System.Drawing.Color.GreenYellow;
            this.label162.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label162.ForeColor = System.Drawing.Color.Black;
            this.label162.Location = new System.Drawing.Point(-1, 0);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(637, 33);
            this.label162.TabIndex = 12;
            this.label162.Text = "UPDATE STRUCTURE";
            this.label162.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlupdatestructure
            // 
            this.pnlupdatestructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlupdatestructure.Controls.Add(this.txtupdatelog);
            this.pnlupdatestructure.Location = new System.Drawing.Point(3, 34);
            this.pnlupdatestructure.Name = "pnlupdatestructure";
            this.pnlupdatestructure.Size = new System.Drawing.Size(629, 370);
            this.pnlupdatestructure.TabIndex = 0;
            // 
            // txtupdatelog
            // 
            this.txtupdatelog.BackColor = System.Drawing.Color.Black;
            this.txtupdatelog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtupdatelog.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtupdatelog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtupdatelog.Location = new System.Drawing.Point(1, 1);
            this.txtupdatelog.Multiline = true;
            this.txtupdatelog.Name = "txtupdatelog";
            this.txtupdatelog.Size = new System.Drawing.Size(625, 365);
            this.txtupdatelog.TabIndex = 0;
            this.txtupdatelog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtupdatelog_KeyPress);
            // 
            // frmupdatestructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(224)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(637, 488);
            this.Controls.Add(this.pnlmain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmupdatestructure";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UPDATE STRUCTURE";
            this.Load += new System.EventHandler(this.frmupdatestructure_Load);
            this.pnlmain.ResumeLayout(false);
            this.pnlbutton.ResumeLayout(false);
            this.pnlupdatestructure.ResumeLayout(false);
            this.pnlupdatestructure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlmain;
        private System.Windows.Forms.Panel pnlupdatestructure;
        private System.Windows.Forms.Label label162;
        private System.Windows.Forms.TextBox txtupdatelog;
        private System.Windows.Forms.Panel pnlbutton;
        private System.Windows.Forms.Button btnstopupdatestructure;
        private System.Windows.Forms.Button btnstartupdatestructure;
    }
}