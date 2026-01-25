using MySql.Data.MySqlClient;
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
    public partial class Appointments : Form
        {
        private readonly int? _customerId;   
        private readonly string _customerName;
        private bool _suppressEditPrompt = false;

        // Default constructor for all appointments for all customers
        public Appointments()
            {
            InitializeComponent();
            _customerId = null;     // Null means show all appointments, any customer
            _customerName = null;
            this.Text = "All Appointments";
            this.AcceptButton = null;
            }

        // Constructor for specific customer appointments
        public Appointments(int customerId, string customerName)
            {
            InitializeComponent();
            _customerId = customerId;
            _customerName = customerName;
            this.Text = $"Appointments for {_customerName}";
            this.AcceptButton = null;
            }

        private void ConvertUtcColumnsToLocal(DataTable dt)
            {
            if (dt == null) return;

            if (dt.Columns.Contains("Start"))
                {
                foreach (DataRow r in dt.Rows)
                    {
                    if (r["Start"] != DBNull.Value)
                        {
                        DateTime sUtc = Convert.ToDateTime(r["Start"]);
                        r["Start"] = DateTime.SpecifyKind(sUtc, DateTimeKind.Utc).ToLocalTime();
                        }
                    }
                }

            if (dt.Columns.Contains("End"))
                {
                foreach (DataRow r in dt.Rows)
                    {
                    if (r["End"] != DBNull.Value)
                        {
                        DateTime eUtc = Convert.ToDateTime(r["End"]);
                        r["End"] = DateTime.SpecifyKind(eUtc, DateTimeKind.Utc).ToLocalTime();
                        }
                    }
                }
            }

        private void Appointments_Load(object sender, EventArgs e)
            {
            try
                {
                // Styling setup
                DataGridViewAppointments.AutoGenerateColumns = true;
                DataGridViewAppointments.EnableHeadersVisualStyles = false;
                DataGridViewAppointments.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle();
                DataGridViewAppointments.RowHeadersDefaultCellStyle = new DataGridViewCellStyle();
                DataGridViewAppointments.RowsDefaultCellStyle = new DataGridViewCellStyle();
                DataGridViewAppointments.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle();

                DataGridViewAppointments.Font = new Font("Arial", 10, FontStyle.Regular);
                DataGridViewAppointments.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                DataGridViewAppointments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                DataGridViewAppointments.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridViewAppointments.Font, FontStyle.Bold);
                DataGridViewAppointments.RowHeadersDefaultCellStyle.BackColor = Color.DarkGray;
                DataGridViewAppointments.RowHeadersDefaultCellStyle.ForeColor = Color.White;
                DataGridViewAppointments.RowsDefaultCellStyle.BackColor = Color.White;
                DataGridViewAppointments.RowsDefaultCellStyle.ForeColor = Color.Black;
                DataGridViewAppointments.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                DataGridViewAppointments.DefaultCellStyle.SelectionBackColor = Color.Black;
                DataGridViewAppointments.DefaultCellStyle.SelectionForeColor = Color.Gray;
                DataGridViewAppointments.RowStateChanged += DataGridViewAppointments_RowStateChanged;

                // Default to today
                DateTime today = DateTime.Today;
                monthCalendarAppointments.SetDate(today);

                // Load appointments (filtered if needed)
                DataTable appointmentsData;
                if (_customerId.HasValue)
                    appointmentsData = DbManager.GetAppointments(_customerId.Value);
                else
                    appointmentsData = DbManager.GetAllAppointments();

                // Convert UTC time to local time
                ConvertUtcColumnsToLocal(appointmentsData); 

                DataGridViewAppointments.DataSource = appointmentsData;
                DataGridViewAppointments.ClearSelection();

                // Column widths + formats (no horizontal scrolling)
                DataGridViewAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                DataGridViewAppointments.ScrollBars = ScrollBars.Vertical; // prevents horizontal scroll

                // Hide AppointmentID but keep it for edit/delete
                if (DataGridViewAppointments.Columns.Contains("AppointmentID"))
                    {
                    DataGridViewAppointments.Columns["AppointmentID"].Visible = false;
                    }

                // CustomerName: size to content (or keep 150 if you prefer)
                if (DataGridViewAppointments.Columns.Contains("CustomerName"))
                    {
                    DataGridViewAppointments.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }

                // AppointmentType: FLEX column (this is the key)
                if (DataGridViewAppointments.Columns.Contains("AppointmentType"))
                    {
                    DataGridViewAppointments.Columns["AppointmentType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                // Start: fixed width
                if (DataGridViewAppointments.Columns.Contains("Start"))
                    {
                    DataGridViewAppointments.Columns["Start"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                    DataGridViewAppointments.Columns["Start"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    DataGridViewAppointments.Columns["Start"].Width = 170;
                    }

                // End: fixed width
                if (DataGridViewAppointments.Columns.Contains("End"))
                    {
                    DataGridViewAppointments.Columns["End"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                    DataGridViewAppointments.Columns["End"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    DataGridViewAppointments.Columns["End"].Width = 170;
                    }

                }
            catch (Exception ex)
                {
                MessageBox.Show("Error loading appointments: " + ex.Message);
                }
            }

        // Makes selected row bold
        private void DataGridViewAppointments_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
            {
            if (e.Row.Selected)
                {
                e.Row.DefaultCellStyle.Font = new Font(DataGridViewAppointments.Font, FontStyle.Bold);
                }
            else
                {
                e.Row.DefaultCellStyle.Font = new Font(DataGridViewAppointments.Font, FontStyle.Regular);
                }
            }

        // Apply alternating row colors method
        private void ApplyAlternatingRowColors()
            {
            for (int i = 0; i < DataGridViewAppointments.Rows.Count; i++)
                {
                DataGridViewAppointments.Rows[i].DefaultCellStyle.BackColor =
                    (i % 2 == 0) ? Color.LightGray : Color.White;
                DataGridViewAppointments.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
            }

        // Calendar date selected event
        private void MonthCalendarAppointments_DateSelected(object sender, DateRangeEventArgs e)
            {
            DateTime selectedDate = e.Start.Date;

            // Local day range
            DateTime startLocal = selectedDate.Date;
            DateTime nextLocal = selectedDate.Date.AddDays(1);

            // Convert LOCAL → UTC because DB stores UTC
            DateTime startUtc = DateTime.SpecifyKind(startLocal, DateTimeKind.Local).ToUniversalTime();
            DateTime nextUtc = DateTime.SpecifyKind(nextLocal, DateTimeKind.Local).ToUniversalTime();

            using (MySqlConnection connection = DbManager.GetConnection())
                {
                connection.Open();

                string query = @"
            SELECT a.AppointmentID,
                   c.Name AS CustomerName,
                   a.Title AS AppointmentType,
                   a.Start,
                   a.End
            FROM appointments a
            JOIN customers c ON a.CustomerID = c.CustomerID
            WHERE a.Start >= @StartUtc AND a.Start < @NextUtc
            ORDER BY a.Start;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                    cmd.Parameters.AddWithValue("@StartUtc", startUtc);
                    cmd.Parameters.AddWithValue("@NextUtc", nextUtc);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        ConvertUtcColumnsToLocal(dt);

                        DataGridViewAppointments.DataSource = dt;
                        DataGridViewAppointments.ClearSelection();
                        ApplyAlternatingRowColors();
                        }
                    }
                } 

            // Hide AppointmentID column (keep it for edit/delete)
            if (DataGridViewAppointments.Columns.Contains("AppointmentID"))
                DataGridViewAppointments.Columns["AppointmentID"].Visible = false;

            // Fixed sizing like your other screens
            DataGridViewAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (DataGridViewAppointments.Columns.Contains("CustomerName"))
                DataGridViewAppointments.Columns["CustomerName"].Width = 150;

            if (DataGridViewAppointments.Columns.Contains("AppointmentType"))
                DataGridViewAppointments.Columns["AppointmentType"].Width = 200;

            if (DataGridViewAppointments.Columns.Contains("Start"))
                {
                DataGridViewAppointments.Columns["Start"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                DataGridViewAppointments.Columns["Start"].Width = 170;
                }

            if (DataGridViewAppointments.Columns.Contains("End"))
                {
                DataGridViewAppointments.Columns["End"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                DataGridViewAppointments.Columns["End"].Width = 170;
                }

            // Force alternating row colors (first row gray)
            for (int i = 0; i < DataGridViewAppointments.Rows.Count; i++)
                {
                DataGridViewAppointments.Rows[i].DefaultCellStyle.BackColor =
                    (i % 2 == 0) ? Color.LightGray : Color.White;
                DataGridViewAppointments.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }

            DataGridViewAppointments.ClearSelection();
            }

        // Open the Customers form
        private void ButtonCustomers_Click(object sender, EventArgs e)
            {
            Customers custForm = new Customers(); 
            custForm.Show();
            this.Close(); // Close the current Appointments form
            }

        // Add Appointment button form
        private void ButtonAddAppt_Click(object sender, EventArgs e)
            {
            using (var form = new AppointmentInfo())
                {
                if (form.ShowDialog() == DialogResult.OK)
                    {
                    LoadAppointments();
                    }
                }
            }

        // Edit Appointment button click
        private void ButtonEditAppt_Click(object sender, EventArgs e)
            {
            if (DataGridViewAppointments.SelectedRows.Count == 0)
                {
                // If something triggered edit during refresh, don't nag the user
                if (_suppressEditPrompt) return;

                MessageBox.Show("Please select an appointment to edit.");
                return;
                }

            int appointmentId = Convert.ToInt32(DataGridViewAppointments.SelectedRows[0].Cells["AppointmentID"].Value);

            using (var form = new AppointmentInfo(appointmentId))
                {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadAppointments();
                }
            }

        // Delete Appointment button click
        private void ButtonDeleteAppt_Click(object sender, EventArgs e)
            {
            if (DataGridViewAppointments.SelectedRows.Count == 0)
                {
                MessageBox.Show("Please select an appointment to delete.");
                return;
                }

            DataGridViewRow row = DataGridViewAppointments.SelectedRows[0];
            int appointmentId = Convert.ToInt32(row.Cells["AppointmentID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this appointment?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
                {
                DbManager.DeleteAppointment(appointmentId);
                LoadAppointments();
                }
            }

        // All Appointments button
        private void ButtonAll_Click(object sender, EventArgs e)
            {
            new Appointments().Show();
            this.Close(); 
            }

        // Appointments datatable loader
        private void LoadAppointments(DateTime? date = null)
            {
            _suppressEditPrompt = true;
            try
                {
                DataTable dt;

                if (date.HasValue)
                    dt = DbManager.GetAppointmentsByDate(date.Value);
                else if (_customerId.HasValue)
                    dt = DbManager.GetAppointments(_customerId.Value);
                else
                    dt = DbManager.GetAllAppointments();

                // Ascending now (oldest -> newest)
                if (dt.Columns.Contains("Start"))
                    dt.DefaultView.Sort = "Start ASC";

                DataGridViewAppointments.DataSource = dt.DefaultView;

                if (DataGridViewAppointments.Columns.Contains("Start"))
                    {
                    DataGridViewAppointments.Columns["Start"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                    DataGridViewAppointments.Columns["Start"].Width = 170;
                    }

                if (DataGridViewAppointments.Columns.Contains("End"))
                    {
                    DataGridViewAppointments.Columns["End"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                    DataGridViewAppointments.Columns["End"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                    DataGridViewAppointments.Columns["End"].Width = 170;
                    }

                DataGridViewAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                ConvertUtcColumnsToLocal(dt);

                // keep nothing selected after refresh
                DataGridViewAppointments.ClearSelection();
                DataGridViewAppointments.CurrentCell = null;
                }
            finally
                {
                _suppressEditPrompt = false;
                }
            }

        // Reports button
        private void ButtonReports_Click(object sender, EventArgs e)
            {
            new Reports().Show();
            }

        }
    }
