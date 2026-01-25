namespace SchedulingApp
    {
    partial class CustomerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerInfo));
            this.labelCustomerInfo = new System.Windows.Forms.Label();
            this.ButtonAppointments = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.LogoBox1 = new System.Windows.Forms.PictureBox();
            this.buttonCustomers = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.TextBoxPhone = new System.Windows.Forms.TextBox();
            this.labelPhone = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCustomerInfo
            // 
            this.labelCustomerInfo.AutoSize = true;
            this.labelCustomerInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelCustomerInfo.Font = new System.Drawing.Font("Old English Text MT", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustomerInfo.ForeColor = System.Drawing.Color.White;
            this.labelCustomerInfo.Location = new System.Drawing.Point(235, 201);
            this.labelCustomerInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCustomerInfo.Name = "labelCustomerInfo";
            this.labelCustomerInfo.Size = new System.Drawing.Size(202, 27);
            this.labelCustomerInfo.TabIndex = 27;
            this.labelCustomerInfo.Text = "Customer Information";
            this.labelCustomerInfo.UseWaitCursor = true;
            // 
            // ButtonAppointments
            // 
            this.ButtonAppointments.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ButtonAppointments.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonAppointments.FlatAppearance.BorderSize = 3;
            this.ButtonAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonAppointments.Location = new System.Drawing.Point(414, 572);
            this.ButtonAppointments.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonAppointments.Name = "ButtonAppointments";
            this.ButtonAppointments.Size = new System.Drawing.Size(213, 32);
            this.ButtonAppointments.TabIndex = 26;
            this.ButtonAppointments.Text = "View Appointments -->";
            this.ButtonAppointments.UseVisualStyleBackColor = false;
            this.ButtonAppointments.Click += new System.EventHandler(this.ButtonAppointments_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(456, 494);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(111, 32);
            this.buttonDelete.TabIndex = 25;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(313, 494);
            this.ButtonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(108, 32);
            this.ButtonCancel.TabIndex = 24;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(202, 494);
            this.ButtonSave.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(85, 32);
            this.ButtonSave.TabIndex = 23;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // LogoBox1
            // 
            this.LogoBox1.AccessibleName = "Logo - Gotham Consulting";
            this.LogoBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LogoBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LogoBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.LogoBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogoBox1.Image = ((System.Drawing.Image)(resources.GetObject("LogoBox1.Image")));
            this.LogoBox1.InitialImage = null;
            this.LogoBox1.Location = new System.Drawing.Point(241, 46);
            this.LogoBox1.Margin = new System.Windows.Forms.Padding(0);
            this.LogoBox1.Name = "LogoBox1";
            this.LogoBox1.Size = new System.Drawing.Size(254, 121);
            this.LogoBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoBox1.TabIndex = 22;
            this.LogoBox1.TabStop = false;
            // 
            // buttonCustomers
            // 
            this.buttonCustomers.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.buttonCustomers.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonCustomers.FlatAppearance.BorderSize = 3;
            this.buttonCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCustomers.Location = new System.Drawing.Point(138, 572);
            this.buttonCustomers.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCustomers.Name = "buttonCustomers";
            this.buttonCustomers.Size = new System.Drawing.Size(200, 32);
            this.buttonCustomers.TabIndex = 28;
            this.buttonCustomers.Text = "View Customers -->";
            this.buttonCustomers.UseVisualStyleBackColor = false;
            this.buttonCustomers.Click += new System.EventHandler(this.ButtonCustomers_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(157, 251);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(52, 16);
            this.labelName.TabIndex = 29;
            this.labelName.Text = "Name:*";
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.Color.LightGray;
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Location = new System.Drawing.Point(241, 251);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(355, 22);
            this.textBoxName.TabIndex = 30;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.BackColor = System.Drawing.Color.LightGray;
            this.textBoxEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEmail.Location = new System.Drawing.Point(240, 305);
            this.textBoxEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(355, 22);
            this.textBoxEmail.TabIndex = 32;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.BackColor = System.Drawing.Color.Transparent;
            this.labelEmail.ForeColor = System.Drawing.Color.White;
            this.labelEmail.Location = new System.Drawing.Point(160, 307);
            this.labelEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(49, 16);
            this.labelEmail.TabIndex = 31;
            this.labelEmail.Text = "Email:*";
            // 
            // TextBoxPhone
            // 
            this.TextBoxPhone.BackColor = System.Drawing.Color.LightGray;
            this.TextBoxPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxPhone.Location = new System.Drawing.Point(240, 356);
            this.TextBoxPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TextBoxPhone.Name = "TextBoxPhone";
            this.TextBoxPhone.Size = new System.Drawing.Size(355, 22);
            this.TextBoxPhone.TabIndex = 34;
            this.TextBoxPhone.TextChanged += new System.EventHandler(this.TextBoxPhone_TextChanged);
            // 
            // labelPhone
            // 
            this.labelPhone.AutoSize = true;
            this.labelPhone.BackColor = System.Drawing.Color.Transparent;
            this.labelPhone.ForeColor = System.Drawing.Color.White;
            this.labelPhone.Location = new System.Drawing.Point(145, 358);
            this.labelPhone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(64, 16);
            this.labelPhone.TabIndex = 33;
            this.labelPhone.Text = "Phone #:*";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.BackColor = System.Drawing.Color.LightGray;
            this.textBoxAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAddress.Location = new System.Drawing.Point(240, 406);
            this.textBoxAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxAddress.Multiline = true;
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(355, 52);
            this.textBoxAddress.TabIndex = 36;
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.BackColor = System.Drawing.Color.Transparent;
            this.labelAddress.ForeColor = System.Drawing.Color.White;
            this.labelAddress.Location = new System.Drawing.Point(148, 408);
            this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(66, 16);
            this.labelAddress.TabIndex = 35;
            this.labelAddress.Text = "Address:*";
            // 
            // CustomerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(749, 690);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.TextBoxPhone);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCustomers);
            this.Controls.Add(this.labelCustomerInfo);
            this.Controls.Add(this.ButtonAppointments);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.LogoBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CustomerInfo";
            this.Text = "Customer Info";
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.Label labelCustomerInfo;
        private System.Windows.Forms.Button ButtonAppointments;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.PictureBox LogoBox1;
        private System.Windows.Forms.Button buttonCustomers;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox TextBoxPhone;
        private System.Windows.Forms.Label labelPhone;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label labelAddress;
        }
    }