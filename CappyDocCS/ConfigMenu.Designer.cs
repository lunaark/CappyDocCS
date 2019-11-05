namespace CappyDocCS
{
    partial class ConfigMenu
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
            this.txtOutPath = new System.Windows.Forms.TextBox();
            this.btnFocusMode = new System.Windows.Forms.Button();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTempPath = new System.Windows.Forms.Button();
            this.btnLogoPath = new System.Windows.Forms.Button();
            this.btnOutPath = new System.Windows.Forms.Button();
            this.lblOutPath = new System.Windows.Forms.Label();
            this.btnProjPath = new System.Windows.Forms.Button();
            this.lblProjPath = new System.Windows.Forms.Label();
            this.txtProjPath = new System.Windows.Forms.TextBox();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtOutPath
            // 
            this.txtOutPath.Location = new System.Drawing.Point(12, 25);
            this.txtOutPath.Name = "txtOutPath";
            this.txtOutPath.ReadOnly = true;
            this.txtOutPath.Size = new System.Drawing.Size(324, 20);
            this.txtOutPath.TabIndex = 0;
            // 
            // btnFocusMode
            // 
            this.btnFocusMode.Location = new System.Drawing.Point(0, 0);
            this.btnFocusMode.Name = "btnFocusMode";
            this.btnFocusMode.Size = new System.Drawing.Size(75, 23);
            this.btnFocusMode.TabIndex = 18;
            // 
            // lblTemplate
            // 
            this.lblTemplate.Location = new System.Drawing.Point(0, 0);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(100, 23);
            this.lblTemplate.TabIndex = 17;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(347, 122);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnTempPath
            // 
            this.btnTempPath.Location = new System.Drawing.Point(0, 0);
            this.btnTempPath.Name = "btnTempPath";
            this.btnTempPath.Size = new System.Drawing.Size(75, 23);
            this.btnTempPath.TabIndex = 29;
            // 
            // btnLogoPath
            // 
            this.btnLogoPath.Location = new System.Drawing.Point(0, 0);
            this.btnLogoPath.Name = "btnLogoPath";
            this.btnLogoPath.Size = new System.Drawing.Size(75, 23);
            this.btnLogoPath.TabIndex = 28;
            // 
            // btnOutPath
            // 
            this.btnOutPath.Location = new System.Drawing.Point(0, 0);
            this.btnOutPath.Name = "btnOutPath";
            this.btnOutPath.Size = new System.Drawing.Size(75, 23);
            this.btnOutPath.TabIndex = 27;
            // 
            // lblOutPath
            // 
            this.lblOutPath.Location = new System.Drawing.Point(0, 0);
            this.lblOutPath.Name = "lblOutPath";
            this.lblOutPath.Size = new System.Drawing.Size(100, 23);
            this.lblOutPath.TabIndex = 26;
            // 
            // btnProjPath
            // 
            this.btnProjPath.Location = new System.Drawing.Point(347, 73);
            this.btnProjPath.Name = "btnProjPath";
            this.btnProjPath.Size = new System.Drawing.Size(75, 20);
            this.btnProjPath.TabIndex = 25;
            this.btnProjPath.Text = "Browse...";
            this.btnProjPath.UseVisualStyleBackColor = true;
            this.btnProjPath.Click += new System.EventHandler(this.btnProjPath_Click);
            // 
            // lblProjPath
            // 
            this.lblProjPath.AutoSize = true;
            this.lblProjPath.Location = new System.Drawing.Point(12, 56);
            this.lblProjPath.Name = "lblProjPath";
            this.lblProjPath.Size = new System.Drawing.Size(65, 13);
            this.lblProjPath.TabIndex = 24;
            this.lblProjPath.Text = "Project Path";
            // 
            // txtProjPath
            // 
            this.txtProjPath.Location = new System.Drawing.Point(12, 73);
            this.txtProjPath.Name = "txtProjPath";
            this.txtProjPath.ReadOnly = true;
            this.txtProjPath.Size = new System.Drawing.Size(324, 20);
            this.txtProjPath.TabIndex = 23;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(12, 9);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(64, 13);
            this.lblOutputPath.TabIndex = 30;
            this.lblOutputPath.Text = "Output Path";
            // 
            // ConfigMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 156);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.btnProjPath);
            this.Controls.Add(this.lblProjPath);
            this.Controls.Add(this.txtProjPath);
            this.Controls.Add(this.lblOutPath);
            this.Controls.Add(this.btnOutPath);
            this.Controls.Add(this.btnLogoPath);
            this.Controls.Add(this.btnTempPath);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTemplate);
            this.Controls.Add(this.btnFocusMode);
            this.Controls.Add(this.txtOutPath);
            this.Name = "ConfigMenu";
            this.Text = "Config Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutPath;
        private System.Windows.Forms.Button btnFocusMode;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTempPath;
        private System.Windows.Forms.Button btnLogoPath;
        private System.Windows.Forms.Button btnOutPath;
        private System.Windows.Forms.Label lblOutPath;
        private System.Windows.Forms.Button btnProjPath;
        private System.Windows.Forms.Label lblProjPath;
        private System.Windows.Forms.TextBox txtProjPath;
        private System.Windows.Forms.Label lblOutputPath;
    }
}