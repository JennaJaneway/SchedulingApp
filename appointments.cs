using MySqlConnector;
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
            _customerId = null;     // Null means show all appointment, any customer
            _customerName = null;
            this.Text = "All Appointments";
            this.AcceptButton = null;
            }

        // Constructor for specific customer appointment
        public Appointments(int customerId, string customerName)
            {
            InitializeComponent();
            _customerId = customerId;
            _customerName = customerName;
            this.Text = $"Appointments for {_customerName}";
            this.AcceptButton = null;
            }

        // Convert Start/End columns from UTC (DB) to LOCAL (UI)
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
                // Applies universal theme
                GridStyles.ApplyStandardStyle(DataGridViewAppointments);

                // Default calendar to today
                monthCalendarAppointments.SetDate(DateTime.Today);

                // Load appointments
                DataTable appointmentData = _customerId.HasValue
                    ? DbManager.GetAppointments(_customerId.Value)
                    : DbManager.GetAllAppointments();

                // Convert UTC -> Local for display
                ConvertUtcColumnsToLocal(appointmentData);

                // Bind grid
                DataGridViewAppointments.DataSource = appointmentData;
                DataGridViewAppointments.ClearSelection();
                DataGridViewAppointments.CurrentCell = null;

                // Layout / formatting 
                DataGridViewAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                DataGridViewAppointments.ScrollBars = ScrollBars.Vertical;

                if (DataGridViewAppointments.Columns.Contains("appointmentId"))
                    DataGridViewAppointments.Columns["appointmentId"].Visible = false;

                if (DataGridViewAppointments.Columns.Contains("CustomerName"))
                    DataGridViewAppointments.Columns["CustomerName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                if (DataGridViewAppointments.Columns.Contains("AppointmentType"))
                    DataGridViewAppointments.Columns["AppointmentType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (DataGridViewAppointments.Columns.Contains("Start"))
                    {
                    DataGridViewAppointments.Columns["Start"].DefaultCellStyle.Format = "MM/dd/yy hh:mm tt";
                    DataGridViewAppointments.Columns["Start"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    DataGridViewAppointments.Columns["Start"].Width = 170;
                    }

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
            SELECT a.appointmentId,
                   c.customerName AS CustomerName,
                   a.Title AS AppointmentType,
                   a.Start,
                   a.End
            FROM appointment a
            JOIN customer c ON a.CustomerID = c.CustomerID
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
                        }
                    }
                } 

            // Hides appointmentId column
            if (DataGridViewAppointments.Columns.Contains("appointmentId"))
                DataGridViewAppointments.Columns["appointmentId"].Visible = false;

            // Layout / formatting
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

            DataGridViewAppointments.ClearSelection();
            }

        // ************ CUSTOMERS FORM BUTTON ************
        private void ButtonCustomers_Click(object sender, EventArgs e)
            {
            Customers custForm = new Customers(); 
            custForm.Show();
            this.Close(); // Close the current Appointments form
            }

        // *********** ADD BUTTON ***********
        private void ButtonAddAppt_Click(object sender, EventArgs e)
            {
            using (var form = new AppointmentInfo())
                {
                if (form.ShowDialog() == DialogResult.OK)
                    {
                    GridStyles.ApplyStandardStyle(DataGridViewAppointments);
                    LoadAppointments();
                    }
                }
            }

        // ******** EDIT BUTTON ********
        private void ButtonEditAppt_Click(object sender, EventArgs e)
            {
            if (DataGridViewAppointments.SelectedRows.Count == 0)
                {
                if (_suppressEditPrompt) return;

                MessageBox.Show("Please select an appointment to edit.");
                return;
                }

            int appointmentId = Convert.ToInt32(DataGridViewAppointments.SelectedRows[0].Cells["appointmentId"].Value);

            using (var form = new AppointmentInfo(appointmentId))
                {
                if (form.ShowDialog() == DialogResult.OK)
                GridStyles.ApplyStandardStyle(DataGridViewAppointments);
                LoadAppointments();
                }
            }

        // ******* DELETE BUTTON ********
        private void ButtonDeleteAppt_Click(object sender, EventArgs e)
            {
            if (DataGridViewAppointments.SelectedRows.Count == 0)
                {
                MessageBox.Show("Please select an appointment to delete.");
                return;
                }

            DataGridViewRow row = DataGridViewAppointments.SelectedRows[0];
            int appointmentId = Convert.ToInt32(row.Cells["appointmentId"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this appointment?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
                {
                DbManager.DeleteAppointment(appointmentId);
                GridStyles.ApplyStandardStyle(DataGridViewAppointments);
                LoadAppointments();
                }
            }

        // ********** ALL APPOINTMENTS BUTTON **********
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
                DataGridViewAppointments.ClearSelection();

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

        // ********* REPORTS BUTTON *********
        private void ButtonReports_Click(object sender, EventArgs e)
            {
            new Reports().Show();
            }

        private void Appointments_Activated(object sender, EventArgs e)
            {
            LoadAppointments(); 
            }

        }
    }
