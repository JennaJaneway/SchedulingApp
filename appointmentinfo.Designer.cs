namespace SchedulingApp
    {
    partial class AppointmentInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppointmentInfo));
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelApptStartTime = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelApptType = new System.Windows.Forms.Label();
            this.buttonCustomers = new System.Windows.Forms.Button();
            this.labelAppointmentInfo = new System.Windows.Forms.Label();
            this.ButtonAppointments = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.LogoBox1 = new System.Windows.Forms.PictureBox();
            this.comboBoxApptType = new System.Windows.Forms.ComboBox();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.labelNote = new System.Windows.Forms.Label();
            this.ButtonStartTime = new System.Windows.Forms.Button();
            this.ButtonEndTime = new System.Windows.Forms.Button();
            this.monthCalendarPicker = new System.Windows.Forms.MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.BackColor = System.Drawing.Color.Transparent;
            this.labelAddress.ForeColor = System.Drawing.Color.White;
            this.labelAddress.Location = new System.Drawing.Point(59, 409);
            this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(151, 16);
            this.labelAddress.TabIndex = 52;
            this.labelAddress.Text = "Appointment End Time:*";
            // 
            // labelApptStartTime
            // 
            this.labelApptStartTime.AutoSize = true;
            this.labelApptStartTime.BackColor = System.Drawing.Color.Transparent;
            this.labelApptStartTime.ForeColor = System.Drawing.Color.White;
            this.labelApptStartTime.Location = new System.Drawing.Point(56, 353);
            this.labelApptStartTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelApptStartTime.Name = "labelApptStartTime";
            this.labelApptStartTime.Size = new System.Drawing.Size(154, 16);
            this.labelApptStartTime.TabIndex = 50;
            this.labelApptStartTime.Text = "Appointment Start Time:*";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(138, 251);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(72, 16);
            this.labelName.TabIndex = 48;
            this.labelName.Text = "Customer:*";
            // 
            // labelApptType
            // 
            this.labelApptType.AutoSize = true;
            this.labelApptType.BackColor = System.Drawing.Color.Transparent;
            this.labelApptType.ForeColor = System.Drawing.Color.White;
            this.labelApptType.Location = new System.Drawing.Point(85, 300);
            this.labelApptType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelApptType.Name = "labelApptType";
            this.labelApptType.Size = new System.Drawing.Size(125, 16);
            this.labelApptType.TabIndex = 46;
            this.labelApptType.Text = "Appointment Type:*";
            // 
            // buttonCustomers
            // 
            this.buttonCustomers.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.buttonCustomers.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buttonCustomers.FlatAppearance.BorderSize = 3;
            this.buttonCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCustomers.Location = new System.Drawing.Point(152, 690);
            this.buttonCustomers.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCustomers.Name = "buttonCustomers";
            this.buttonCustomers.Size = new System.Drawing.Size(200, 32);
            this.buttonCustomers.TabIndex = 45;
            this.buttonCustomers.Text = "View Customers -->";
            this.buttonCustomers.UseVisualStyleBackColor = false;
            // 
            // labelAppointmentInfo
            // 
            this.labelAppointmentInfo.AutoSize = true;
            this.labelAppointmentInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelAppointmentInfo.Font = new System.Drawing.Font("Old English Text MT", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppointmentInfo.ForeColor = System.Drawing.Color.White;
            this.labelAppointmentInfo.Location = new System.Drawing.Point(239, 203);
            this.labelAppointmentInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAppointmentInfo.Name = "labelAppointmentInfo";
            this.labelAppointmentInfo.Size = new System.Drawing.Size(224, 27);
            this.labelAppointmentInfo.TabIndex = 44;
            this.labelAppointmentInfo.Text = "Appointment Information";
            this.labelAppointmentInfo.UseWaitCursor = true;
            // 
            // ButtonAppointments
            // 
            this.ButtonAppointments.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ButtonAppointments.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonAppointments.FlatAppearance.BorderSize = 3;
            this.ButtonAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonAppointments.Location = new System.Drawing.Point(401, 690);
            this.ButtonAppointments.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonAppointments.Name = "ButtonAppointments";
            this.ButtonAppointments.Size = new System.Drawing.Size(200, 32);
            this.ButtonAppointments.TabIndex = 43;
            this.ButtonAppointments.Text = "View Appointments -->";
            this.ButtonAppointments.UseVisualStyleBackColor = false;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(459, 591);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(111, 32);
            this.buttonDelete.TabIndex = 42;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(321, 591);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 32);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(207, 591);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(85, 32);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
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
            this.LogoBox1.Location = new System.Drawing.Point(225, 62);
            this.LogoBox1.Margin = new System.Windows.Forms.Padding(0);
            this.LogoBox1.Name = "LogoBox1";
            this.LogoBox1.Size = new System.Drawing.Size(254, 121);
            this.LogoBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoBox1.TabIndex = 39;
            this.LogoBox1.TabStop = false;
            // 
            // comboBoxApptType
            // 
            this.comboBoxApptType.FormattingEnabled = true;
            this.comboBoxApptType.Location = new System.Drawing.Point(245, 297);
            this.comboBoxApptType.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxApptType.Name = "comboBoxApptType";
            this.comboBoxApptType.Size = new System.Drawing.Size(356, 24);
            this.comboBoxApptType.TabIndex = 1;
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(245, 248);
            this.comboBoxCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(356, 24);
            this.comboBoxCustomer.TabIndex = 0;
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(245, 349);
            this.dateTimeStart.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.ShowUpDown = true;
            this.dateTimeStart.Size = new System.Drawing.Size(155, 22);
            this.dateTimeStart.TabIndex = 2;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(245, 404);
            this.dateTimeEnd.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.ShowUpDown = true;
            this.dateTimeEnd.Size = new System.Drawing.Size(155, 22);
            this.dateTimeEnd.TabIndex = 3;
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.BackColor = System.Drawing.Color.Transparent;
            this.labelNote.ForeColor = System.Drawing.Color.White;
            this.labelNote.Location = new System.Drawing.Point(85, 550);
            this.labelNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(611, 16);
            this.labelNote.TabIndex = 60;
            this.labelNote.Text = "Note: Appointments must be scheduled during the business hours of 9am to 5pm, Mon" +
    "day–Friday, EST";
            // 
            // ButtonStartTime
            // 
            this.ButtonStartTime.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonStartTime.Location = new System.Drawing.Point(409, 346);
            this.ButtonStartTime.Name = "ButtonStartTime";
            this.ButtonStartTime.Size = new System.Drawing.Size(38, 29);
            this.ButtonStartTime.TabIndex = 61;
            this.ButtonStartTime.Text = "📅";
            this.ButtonStartTime.UseVisualStyleBackColor = true;
            // 
            // ButtonEndTime
            // 
            this.ButtonEndTime.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonEndTime.Location = new System.Drawing.Point(409, 401);
            this.ButtonEndTime.Name = "ButtonEndTime";
            this.ButtonEndTime.Size = new System.Drawing.Size(38, 29);
            this.ButtonEndTime.TabIndex = 62;
            this.ButtonEndTime.Text = "📅";
            this.ButtonEndTime.UseVisualStyleBackColor = true;
            // 
            // monthCalendarPicker
            // 
            this.monthCalendarPicker.Location = new System.Drawing.Point(459, 334);
            this.monthCalendarPicker.MaxSelectionCount = 1;
            this.monthCalendarPicker.Name = "monthCalendarPicker";
            this.monthCalendarPicker.TabIndex = 63;
            this.monthCalendarPicker.Visible = false;
            // 
            // AppointmentInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(736, 789);
            this.Controls.Add(this.monthCalendarPicker);
            this.Controls.Add(this.ButtonEndTime);
            this.Controls.Add(this.ButtonStartTime);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.dateTimeStart);
            this.Controls.Add(this.comboBoxCustomer);
            this.Controls.Add(this.comboBoxApptType);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.labelApptStartTime);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelApptType);
            this.Controls.Add(this.buttonCustomers);
            this.Controls.Add(this.labelAppointmentInfo);
            this.Controls.Add(this.ButtonAppointments);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.LogoBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AppointmentInfo";
            this.Text = "Appointment Information";
            this.Load += new System.EventHandler(this.AppointmentInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelApptStartTime;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelApptType;
        private System.Windows.Forms.Button buttonCustomers;
        private System.Windows.Forms.Label labelAppointmentInfo;
        private System.Windows.Forms.Button ButtonAppointments;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.PictureBox LogoBox1;
        private System.Windows.Forms.ComboBox comboBoxApptType;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.Button ButtonStartTime;
        private System.Windows.Forms.Button ButtonEndTime;
        private System.Windows.Forms.MonthCalendar monthCalendarPicker;
        }
    }