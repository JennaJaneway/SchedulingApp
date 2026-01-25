namespace SchedulingApp
    {
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.TextBoxUsername = new System.Windows.Forms.TextBox();
            this.LabelUsername = new System.Windows.Forms.Label();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.ButtonLogin = new System.Windows.Forms.Button();
            this.LabelLanguage = new System.Windows.Forms.Label();
            this.LabelLocation = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LogoBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TextBoxUsername
            // 
            this.TextBoxUsername.BackColor = System.Drawing.Color.LightGray;
            this.TextBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxUsername.Location = new System.Drawing.Point(331, 178);
            this.TextBoxUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxUsername.Name = "TextBoxUsername";
            this.TextBoxUsername.Size = new System.Drawing.Size(247, 22);
            this.TextBoxUsername.TabIndex = 0;
            // 
            // LabelUsername
            // 
            this.LabelUsername.AutoSize = true;
            this.LabelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUsername.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelUsername.Location = new System.Drawing.Point(223, 178);
            this.LabelUsername.Name = "LabelUsername";
            this.LabelUsername.Size = new System.Drawing.Size(68, 15);
            this.LabelUsername.TabIndex = 1;
            this.LabelUsername.Text = "Username:";
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.BackColor = System.Drawing.Color.LightGray;
            this.TextBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxPassword.Location = new System.Drawing.Point(331, 222);
            this.TextBoxPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.Size = new System.Drawing.Size(247, 22);
            this.TextBoxPassword.TabIndex = 2;
            this.TextBoxPassword.UseSystemPasswordChar = true;
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelPassword.Location = new System.Drawing.Point(228, 222);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(64, 15);
            this.LabelPassword.TabIndex = 3;
            this.LabelPassword.Text = "Password:";
            // 
            // ButtonLogin
            // 
            this.ButtonLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ButtonLogin.Font = new System.Drawing.Font("Old English Text MT", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLogin.Location = new System.Drawing.Point(331, 274);
            this.ButtonLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonLogin.Name = "ButtonLogin";
            this.ButtonLogin.Size = new System.Drawing.Size(137, 43);
            this.ButtonLogin.TabIndex = 4;
            this.ButtonLogin.Text = "Log in";
            this.ButtonLogin.UseVisualStyleBackColor = true;
            this.ButtonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // LabelLanguage
            // 
            this.LabelLanguage.AutoSize = true;
            this.LabelLanguage.BackColor = System.Drawing.Color.Transparent;
            this.LabelLanguage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelLanguage.Location = new System.Drawing.Point(268, 379);
            this.LabelLanguage.Name = "LabelLanguage";
            this.LabelLanguage.Size = new System.Drawing.Size(71, 16);
            this.LabelLanguage.TabIndex = 5;
            this.LabelLanguage.Text = "Language:";
            // 
            // LabelLocation
            // 
            this.LabelLocation.AutoSize = true;
            this.LabelLocation.BackColor = System.Drawing.Color.Transparent;
            this.LabelLocation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelLocation.Location = new System.Drawing.Point(220, 417);
            this.LabelLocation.Name = "LabelLocation";
            this.LabelLocation.Size = new System.Drawing.Size(119, 16);
            this.LabelLocation.TabIndex = 6;
            this.LabelLocation.Text = "Detected Location:";
            // 
            // LogoBox1
            // 
            this.LogoBox1.AccessibleName = "Logo - Gotham Consulting";
            this.LogoBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LogoBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LogoBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogoBox1.Image = ((System.Drawing.Image)(resources.GetObject("LogoBox1.Image")));
            this.LogoBox1.InitialImage = null;
            this.LogoBox1.Location = new System.Drawing.Point(271, 25);
            this.LogoBox1.Margin = new System.Windows.Forms.Padding(0);
            this.LogoBox1.Name = "LogoBox1";
            this.LogoBox1.Size = new System.Drawing.Size(254, 121);
            this.LogoBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoBox1.TabIndex = 7;
            this.LogoBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(799, 594);
            this.Controls.Add(this.LogoBox1);
            this.Controls.Add(this.LabelLocation);
            this.Controls.Add(this.LabelLanguage);
            this.Controls.Add(this.ButtonLogin);
            this.Controls.Add(this.LabelPassword);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.LabelUsername);
            this.Controls.Add(this.TextBoxUsername);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LoginForm";
            this.Text = "Login Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.TextBox TextBoxUsername;
        private System.Windows.Forms.Label LabelUsername;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.Button ButtonLogin;
        private System.Windows.Forms.Label LabelLanguage;
        private System.Windows.Forms.Label LabelLocation;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox LogoBox1;
        }
    }

