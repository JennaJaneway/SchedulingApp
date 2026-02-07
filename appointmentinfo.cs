using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchedulingApp
    {
    public partial class AppointmentInfo : Form
        {
        private enum DatePickTarget { Start, End }
        private DatePickTarget _datePickTarget = DatePickTarget.Start;
        private readonly int? appointmentId;  
        private readonly bool isEditMode;

        public AppointmentInfo(int? apptId = null)
            {
            InitializeComponent();
            appointmentId = apptId;
            isEditMode = appointmentId.HasValue;
            comboBoxApptType.DropDownStyle = ComboBoxStyle.DropDownList;

            // Wire calendar buttons and calendar selection
            ButtonStartTime.Click += ButtonStartTime_Click;
            ButtonEndTime.Click += ButtonEndTime_Click;
            monthCalendarPicker.DateSelected += MonthCalendarPicker_DateSelected;
            }

        private void AppointmentInfo_Load(object sender, EventArgs e)
            {
            // Populate customers
            DataTable customers = DbManager.GetCustomers();
            comboBoxCustomer.DataSource = customers;
            comboBoxCustomer.DisplayMember = "Name";
            comboBoxCustomer.ValueMember = "CustomerID";

            // Populate appointment types 
            comboBoxApptType.Items.Clear();
            comboBoxApptType.Items.AddRange(new object[]
            {
            "Consultation",
            "Follow-up",
            "Therapy Session",
            "Cybersecurity",
            "Martial Arts Training",
            "Criminal Tactics",
            "Crime Scene Investigation",
            "Electroshock Therapy"
            });

            // If in edit mode, load the appointment record into the controls
            if (isEditMode && appointmentId.HasValue)
                {
                DataRow row = DbManager.GetAppointmentById(appointmentId.Value);
                if (row != null)
                    {
                    comboBoxCustomer.SelectedValue = Convert.ToInt32(row["CustomerID"]);
                    string typeFromDb = row["AppointmentType"]?.ToString()?.Trim();

                    int idx = comboBoxApptType.FindStringExact(typeFromDb);
                    if (idx >= 0)
                        {
                        comboBoxApptType.SelectedIndex = idx;
                        }
                    else
                        {
                        // Safety fallback in case DB has a value not in the list
                        comboBoxApptType.Items.Add(typeFromDb);
                        comboBoxApptType.SelectedIndex = comboBoxApptType.Items.Count - 1;
                        }

                    // DB stores UTC, convert to LOCAL for the DateTimePickers
                    DateTime startUtcFromDb = Convert.ToDateTime(row["Start"]);
                    DateTime endUtcFromDb = Convert.ToDateTime(row["End"]);

                    DateTime startLocal = DateTime.SpecifyKind(startUtcFromDb, DateTimeKind.Utc).ToLocalTime();
                    DateTime endLocal = DateTime.SpecifyKind(endUtcFromDb, DateTimeKind.Utc).ToLocalTime();

                    dateTimeStart.Value = TrimToMinute(startLocal);
                    dateTimeEnd.Value = TrimToMinute(endLocal);
                    }
                else
                    {
                    // default start/end values
                    dateTimeStart.Value = TrimToMinute(DateTime.Now);
                    dateTimeEnd.Value = TrimToMinute(DateTime.Now.AddHours(1));
                    monthCalendarPicker.MaxSelectionCount = 1;
                    monthCalendarPicker.Visible = false;
                    monthCalendarPicker.SetDate(dateTimeStart.Value.Date);
                    }
                }
            }

        // Calendar popup handlers
        private void ButtonStartTime_Click(object sender, EventArgs e)
            {
            _datePickTarget = DatePickTarget.Start;
            ShowCalendarUnder(ButtonStartTime, dateTimeStart.Value.Date);
            }

        private void ButtonEndTime_Click(object sender, EventArgs e)
            {
            _datePickTarget = DatePickTarget.End;
            ShowCalendarUnder(ButtonEndTime, dateTimeEnd.Value.Date);
            }

        private void ShowCalendarUnder(Control anchor, DateTime initialDate)
            {
            monthCalendarPicker.Left = anchor.Left;
            monthCalendarPicker.Top = anchor.Bottom + 4;

            monthCalendarPicker.SetDate(initialDate);
            monthCalendarPicker.Visible = true;
            monthCalendarPicker.BringToFront();
            }

        // Handles date selection from calendar popup
        private void MonthCalendarPicker_DateSelected(object sender, DateRangeEventArgs e)
            {
            DateTime chosenDate = e.Start.Date;

            if (_datePickTarget == DatePickTarget.Start)
                {
                TimeSpan time = dateTimeStart.Value.TimeOfDay;
                dateTimeStart.Value = chosenDate.Add(time);

                if (dateTimeEnd.Value <= dateTimeStart.Value)
                    dateTimeEnd.Value = dateTimeStart.Value.AddHours(1);
                }
            else 
                {
                TimeSpan time = dateTimeEnd.Value.TimeOfDay;
                dateTimeEnd.Value = chosenDate.Add(time);

                if (dateTimeEnd.Value <= dateTimeStart.Value)
                    dateTimeEnd.Value = dateTimeStart.Value.AddHours(1);
                }

            monthCalendarPicker.Visible = false;
            }
        // End calendar popup handlers

        // ********* SAVE BUTTON **************
        private void ButtonSave_Click(object sender, EventArgs e)
            {
            try
                {
                // Validate customer selection
                if (comboBoxCustomer.SelectedItem == null)  
                    {
                    MessageBox.Show("Please select a customer.");
                    return;
                    }
                int customerId = Convert.ToInt32(comboBoxCustomer.SelectedValue);

                // Validate type
                if (comboBoxApptType.SelectedItem == null)
                    {
                    MessageBox.Show("Please select an appointment type.");
                    return;
                    }
                string type = comboBoxApptType.Text.Trim();

                // Times from picker (local)
                DateTime startLocal = TrimToMinute(dateTimeStart.Value);
                DateTime endLocal = TrimToMinute(dateTimeEnd.Value);

                dateTimeStart.Value = startLocal;
                dateTimeEnd.Value = endLocal;

                // Convert LOCAL → UTC for storage & comparison
                DateTime startUtc = DateTime.SpecifyKind(startLocal, DateTimeKind.Local).ToUniversalTime();
                DateTime endUtc = DateTime.SpecifyKind(endLocal, DateTimeKind.Local).ToUniversalTime();

                // basic checks (still valid)
                if (endUtc <= startUtc)
                    {
                    // End time check
                    MessageBox.Show("End time must be later than Start time.");
                    return;
                    }

                // Business hours (validate using UTC values)
                if (!InputValidator.ValidateBusinessHours(startUtc, endUtc))
                    {
                    MessageBox.Show("Appointments must be scheduled Monday–Friday between 9:00 AM and 5:00 PM EST.");
                    return;
                    }

                // Appointment Overlap check (per user, UTC)
                int userId = Session.CurrentUserId;

                if (DbManager.AppointmentOverlapsForUser(appointmentId, userId, startUtc, endUtc))
                    {
                    MessageBox.Show("This appointment overlaps with another appointment.");
                    return;
                    }

                // Save using UTC values
                if (isEditMode && appointmentId.HasValue)
                    {
                    DbManager.UpdateAppointment(appointmentId.Value, customerId, userId, type, startUtc, endUtc);
                    }
                else
                    {
                    DbManager.AddAppointment(customerId, userId, type, startUtc, endUtc);
                    }

                this.DialogResult = DialogResult.OK;
                this.Close();
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error saving appointment: " + ex.Message);
                }
            }

        // ********* DELETE BUTTON **************
        private void ButtonDelete_Click(object sender, EventArgs e)
            {
            if (!isEditMode || !appointmentId.HasValue)
                return;

            var confirm = MessageBox.Show("Are you sure you want to delete this appointment?",
                                          "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
                {
                DbManager.DeleteAppointment(appointmentId.Value);
                MessageBox.Show("Appointment deleted!");
                this.DialogResult = DialogResult.OK;
                this.Close();
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error deleting appointment: " + ex.Message);
                }
            }

        // ********* CANCEL BUTTON **********
        private void ButtonCancel_Click(object sender, EventArgs e)
            {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            }

        // Helper to trim seconds/milliseconds from DateTime
        private static DateTime TrimToMinute(DateTime dt)
            {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, dt.Kind);
            }

        }
    }