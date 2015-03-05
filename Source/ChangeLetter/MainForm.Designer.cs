namespace ChangeLetter {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmbVolumes = new System.Windows.Forms.ComboBox();
            this.lblVolumes = new System.Windows.Forms.Label();
            this.lblLetters = new System.Windows.Forms.Label();
            this.cmbLetters = new System.Windows.Forms.ComboBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbVolumes
            // 
            this.cmbVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVolumes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVolumes.FormattingEnabled = true;
            this.cmbVolumes.Location = new System.Drawing.Point(13, 30);
            this.cmbVolumes.Margin = new System.Windows.Forms.Padding(4);
            this.cmbVolumes.Name = "cmbVolumes";
            this.cmbVolumes.Size = new System.Drawing.Size(376, 24);
            this.cmbVolumes.TabIndex = 4;
            this.cmbVolumes.SelectedValueChanged += new System.EventHandler(this.cmbVolumes_SelectedValueChanged);
            // 
            // lblVolumes
            // 
            this.lblVolumes.AutoSize = true;
            this.lblVolumes.Location = new System.Drawing.Point(10, 9);
            this.lblVolumes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVolumes.Name = "lblVolumes";
            this.lblVolumes.Size = new System.Drawing.Size(66, 17);
            this.lblVolumes.TabIndex = 3;
            this.lblVolumes.Text = "&Volumes:";
            // 
            // lblLetters
            // 
            this.lblLetters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLetters.AutoSize = true;
            this.lblLetters.Location = new System.Drawing.Point(394, 9);
            this.lblLetters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLetters.Name = "lblLetters";
            this.lblLetters.Size = new System.Drawing.Size(56, 17);
            this.lblLetters.TabIndex = 0;
            this.lblLetters.Text = "&Letters:";
            // 
            // cmbLetters
            // 
            this.cmbLetters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLetters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLetters.FormattingEnabled = true;
            this.cmbLetters.Location = new System.Drawing.Point(397, 30);
            this.cmbLetters.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLetters.Name = "cmbLetters";
            this.cmbLetters.Size = new System.Drawing.Size(52, 24);
            this.cmbLetters.TabIndex = 1;
            this.cmbLetters.SelectedValueChanged += new System.EventHandler(this.cmbLetters_SelectedValueChanged);
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChange.Enabled = false;
            this.btnChange.Location = new System.Drawing.Point(329, 76);
            this.btnChange.Margin = new System.Windows.Forms.Padding(4, 18, 4, 4);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(120, 34);
            this.btnChange.TabIndex = 2;
            this.btnChange.Text = "&Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(13, 76);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 18, 4, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(120, 34);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHide.Location = new System.Drawing.Point(141, 76);
            this.btnHide.Margin = new System.Windows.Forms.Padding(4, 18, 4, 4);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(120, 34);
            this.btnHide.TabIndex = 6;
            this.btnHide.Text = "&Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Visible = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShow.Location = new System.Drawing.Point(141, 76);
            this.btnShow.Margin = new System.Windows.Forms.Padding(4, 18, 4, 4);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(120, 34);
            this.btnShow.TabIndex = 7;
            this.btnShow.Text = "&Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Visible = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnChange;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 123);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.lblLetters);
            this.Controls.Add(this.cmbLetters);
            this.Controls.Add(this.lblVolumes);
            this.Controls.Add(this.cmbVolumes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Change Letter";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVolumes;
        private System.Windows.Forms.Label lblVolumes;
        private System.Windows.Forms.Label lblLetters;
        private System.Windows.Forms.ComboBox cmbLetters;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnShow;
    }
}

