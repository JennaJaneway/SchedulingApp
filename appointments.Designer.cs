using System.Windows.Forms;

namespace SchedulingApp
    {
    public partial class Appointments : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Appointments));
            this.labelAppts = new System.Windows.Forms.Label();
            this.ButtonCustomers = new System.Windows.Forms.Button();
            this.ButtonDeleteAppt = new System.Windows.Forms.Button();
            this.ButtonEditAppt = new System.Windows.Forms.Button();
            this.ButtonAddAppt = new System.Windows.Forms.Button();
            this.DataGridViewAppointments = new System.Windows.Forms.DataGridView();
            this.LogoBox1 = new System.Windows.Forms.PictureBox();
            this.monthCalendarAppointments = new System.Windows.Forms.MonthCalendar();
            this.ButtonReports = new System.Windows.Forms.Button();
            this.ButtonAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAppts
            // 
            this.labelAppts.AutoSize = true;
            this.labelAppts.BackColor = System.Drawing.Color.Transparent;
            this.labelAppts.Font = new System.Drawing.Font("Old English Text MT", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppts.ForeColor = System.Drawing.Color.White;
            this.labelAppts.Location = new System.Drawing.Point(142, 170);
            this.labelAppts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAppts.Name = "labelAppts";
            this.labelAppts.Size = new System.Drawing.Size(126, 27);
            this.labelAppts.TabIndex = 21;
            this.labelAppts.Text = "Appointments";
            this.labelAppts.UseWaitCursor = true;
            // 
            // ButtonCustomers
            // 
            this.ButtonCustomers.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ButtonCustomers.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonCustomers.FlatAppearance.BorderSize = 3;
            this.ButtonCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonCustomers.Location = new System.Drawing.Point(716, 561);
            this.ButtonCustomers.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonCustomers.Name = "ButtonCustomers";
            this.ButtonCustomers.Size = new System.Drawing.Size(216, 32);
            this.ButtonCustomers.TabIndex = 20;
            this.ButtonCustomers.Text = "View Customers -->";
            this.ButtonCustomers.UseVisualStyleBackColor = false;
            this.ButtonCustomers.Click += new System.EventHandler(this.ButtonCustomers_Click);
            // 
            // ButtonDeleteAppt
            // 
            this.ButtonDeleteAppt.Location = new System.Drawing.Point(803, 461);
            this.ButtonDeleteAppt.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonDeleteAppt.Name = "ButtonDeleteAppt";
            this.ButtonDeleteAppt.Size = new System.Drawing.Size(165, 32);
            this.ButtonDeleteAppt.TabIndex = 19;
            this.ButtonDeleteAppt.Text = "Delete Appointment";
            this.ButtonDeleteAppt.UseVisualStyleBackColor = true;
            this.ButtonDeleteAppt.Click += new System.EventHandler(this.ButtonDeleteAppt_Click);
            // 
            // ButtonEditAppt
            // 
            this.ButtonEditAppt.Location = new System.Drawing.Point(556, 461);
            this.ButtonEditAppt.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonEditAppt.Name = "ButtonEditAppt";
            this.ButtonEditAppt.Size = new System.Drawing.Size(175, 32);
            this.ButtonEditAppt.TabIndex = 18;
            this.ButtonEditAppt.Text = "Edit Appointment";
            this.ButtonEditAppt.UseVisualStyleBackColor = true;
            this.ButtonEditAppt.Click += new System.EventHandler(this.ButtonEditAppt_Click);
            // 
            // ButtonAddAppt
            // 
            this.ButtonAddAppt.Location = new System.Drawing.Point(300, 461);
            this.ButtonAddAppt.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonAddAppt.Name = "ButtonAddAppt";
            this.ButtonAddAppt.Size = new System.Drawing.Size(164, 32);
            this.ButtonAddAppt.TabIndex = 17;
            this.ButtonAddAppt.Text = "Add Appointment";
            this.ButtonAddAppt.UseVisualStyleBackColor = true;
            this.ButtonAddAppt.Click += new System.EventHandler(this.ButtonAddAppt_Click);
            // 
            // DataGridViewAppointments
            // 
            this.DataGridViewAppointments.AllowUserToAddRows = false;
            this.DataGridViewAppointments.AllowUserToDeleteRows = false;
            this.DataGridViewAppointments.AllowUserToResizeColumns = false;
            this.DataGridViewAppointments.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridViewAppointments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridViewAppointments.BackgroundColor = System.Drawing.SystemColors.InactiveCaptionText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ScrollBar;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewAppointments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewAppointments.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewAppointments.EnableHeadersVisualStyles = false;
            this.DataGridViewAppointments.Location = new System.Drawing.Point(367, 207);
            this.DataGridViewAppointments.Margin = new System.Windows.Forms.Padding(4);
            this.DataGridViewAppointments.Name = "DataGridViewAppointments";
            this.DataGridViewAppointments.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewAppointments.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridViewAppointments.RowHeadersWidth = 51;
            this.DataGridViewAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewAppointments.Size = new System.Drawing.Size(848, 199);
            this.DataGridViewAppointments.TabIndex = 16;
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
            this.LogoBox1.Location = new System.Drawing.Point(504, 50);
            this.LogoBox1.Margin = new System.Windows.Forms.Padding(0);
            this.LogoBox1.Name = "LogoBox1";
            this.LogoBox1.Size = new System.Drawing.Size(254, 121);
            this.LogoBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoBox1.TabIndex = 15;
            this.LogoBox1.TabStop = false;
            // 
            // monthCalendarAppointments
            // 
            this.monthCalendarAppointments.Location = new System.Drawing.Point(96, 208);
            this.monthCalendarAppointments.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendarAppointments.Name = "monthCalendarAppointments";
            this.monthCalendarAppointments.TabIndex = 22;
            this.monthCalendarAppointments.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendarAppointments_DateSelected);
            // 
            // ButtonReports
            // 
            this.ButtonReports.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ButtonReports.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ButtonReports.FlatAppearance.BorderSize = 3;
            this.ButtonReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonReports.Location = new System.Drawing.Point(478, 561);
            this.ButtonReports.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonReports.Name = "ButtonReports";
            this.ButtonReports.Size = new System.Drawing.Size(162, 32);
            this.ButtonReports.TabIndex = 23;
            this.ButtonReports.Text = "Reports -->";
            this.ButtonReports.UseVisualStyleBackColor = false;
            this.ButtonReports.Click += new System.EventHandler(this.ButtonReports_Click);
            // 
            // ButtonAll
            // 
            this.ButtonAll.Location = new System.Drawing.Point(1020, 461);
            this.ButtonAll.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonAll.Name = "ButtonAll";
            this.ButtonAll.Size = new System.Drawing.Size(148, 32);
            this.ButtonAll.TabIndex = 24;
            this.ButtonAll.Text = "All Appointments";
            this.ButtonAll.UseVisualStyleBackColor = true;
            this.ButtonAll.Click += new System.EventHandler(this.ButtonAll_Click);
            // 
            // Appointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1315, 647);
            this.Controls.Add(this.ButtonAll);
            this.Controls.Add(this.ButtonReports);
            this.Controls.Add(this.monthCalendarAppointments);
            this.Controls.Add(this.labelAppts);
            this.Controls.Add(this.ButtonCustomers);
            this.Controls.Add(this.ButtonDeleteAppt);
            this.Controls.Add(this.ButtonEditAppt);
            this.Controls.Add(this.ButtonAddAppt);
            this.Controls.Add(this.DataGridViewAppointments);
            this.Controls.Add(this.LogoBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Appointments";
            this.Text = "appointments";
            this.Activated += new System.EventHandler(this.Appointments_Activated);
            this.Load += new System.EventHandler(this.Appointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogoBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.Label labelAppts;
        private System.Windows.Forms.Button ButtonCustomers;
        private System.Windows.Forms.Button ButtonDeleteAppt;
        private System.Windows.Forms.Button ButtonEditAppt;
        private System.Windows.Forms.Button ButtonAddAppt;
        private System.Windows.Forms.DataGridView DataGridViewAppointments;
        private System.Windows.Forms.PictureBox LogoBox1;
        private System.Windows.Forms.MonthCalendar monthCalendarAppointments;
        private Button ButtonReports;
        private Button ButtonAll;
        }
    }