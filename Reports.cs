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
    public partial class Reports : Form
        {
        public Reports()
            {
            InitializeComponent();

            ComboBoxReports.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBoxReports.Items.Add("Appointment types by month");
            ComboBoxReports.Items.Add("Schedule for each user");
            ComboBoxReports.Items.Add("Appointments by customer");
            ComboBoxReports.SelectedIndex = 0;
            ButtonRun.Click += ButtonRun_Click;
            }

        // ************** RUN REPORT BUTTON CLICK **************
        private void ButtonRun_Click(object sender, EventArgs e)
            {
            DataTable raw = DbManager.GetAppointmentsForReports();

            DataTable reportTable;

            if (ComboBoxReports.SelectedItem.ToString() == "Appointment types by month")
                {
                reportTable = BuildTypesByMonth(raw);
                }
            else if (ComboBoxReports.SelectedItem.ToString() == "Schedule for each user")
                {
                reportTable = BuildScheduleByUser(raw);
                }
            else // "Appointments by customer"
                {
                reportTable = BuildAppointmentsByCustomer(raw);
                }

            // Convert the columns on the table that will be displayed
            ConvertUtcColumnsToLocal(reportTable);

            DataGridViewReports.DataSource = reportTable;

            ApplyReportsGridStyle();
            DataGridViewReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewReports.ClearSelection();
            }


        // Converts Start/End columns from UTC (DB) to LOCAL (UI)
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

        // *********** REPORT 1 — Appointment types by month ************
        private DataTable BuildTypesByMonth(DataTable raw)
            {
            var results = raw.AsEnumerable()
                .Select(r => new
                    {
                    Month = Convert.ToDateTime(r["Start"]).ToString("yyyy-MM"),
                    Type = r["AppointmentType"].ToString()
                    })
                .GroupBy(x => new { x.Month, x.Type })   // lambda
                .Select(g => new
                    {
                    Month = g.Key.Month,
                    AppointmentType = g.Key.Type,
                    Count = g.Count()
                    })
                .OrderBy(x => x.Month)
                .ThenBy(x => x.AppointmentType)
                .ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("Month");
            dt.Columns.Add("AppointmentType");
            dt.Columns.Add("Count", typeof(int));

            foreach (var row in results)
                dt.Rows.Add(row.Month, row.AppointmentType, row.Count);

            return dt;
            }

        // ****** REPORT 2 — Schedule for each user ******
        private DataTable BuildScheduleByUser(DataTable raw)
            {
            var results = raw.AsEnumerable()
                .Select(r => new
                    {
                    UserName = r["UserName"].ToString(),
                    CustomerName = r["CustomerName"].ToString(),
                    AppointmentType = r["AppointmentType"].ToString(),
                    Start = Convert.ToDateTime(r["Start"]),
                    End = Convert.ToDateTime(r["End"])
                    })
                .OrderBy(x => x.UserName)    // lambda
                .ThenBy(x => x.Start)
                .ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("UserName");
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("AppointmentType");
            dt.Columns.Add("Start", typeof(DateTime));
            dt.Columns.Add("End", typeof(DateTime));

            foreach (var row in results)
                dt.Rows.Add(row.UserName, row.CustomerName, row.AppointmentType, row.Start, row.End);

            return dt;
            }

        // ******* REPORT 3 — Appointments by customer name *******
        private DataTable BuildAppointmentsByCustomer(DataTable raw)
            {
            var results = raw.AsEnumerable()
                .Select(r => r["CustomerName"].ToString())
                .GroupBy(name => name)     // lambda
                .Select(g => new
                    {
                    CustomerName = g.Key,
                    Appointments = g.Count()
                    })
                .OrderByDescending(x => x.Appointments)
                .ThenBy(x => x.CustomerName)
                .ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("Appointments", typeof(int));

            foreach (var row in results)
                dt.Rows.Add(row.CustomerName, row.Appointments);

            return dt;
            }

        // ************** STYLING FOR REPORTS DATA GRID **************
        private void ApplyReportsGridStyle()
            {
            GridStyles.ApplyStandardStyle(DataGridViewReports); // from GridStyles.cs
            DataGridViewReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewReports.MultiSelect = false;
            DataGridViewReports.ReadOnly = true;
            DataGridViewReports.AllowUserToAddRows = false;
            DataGridViewReports.AllowUserToDeleteRows = false;
            DataGridViewReports.AllowUserToResizeRows = false;
            DataGridViewReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }
    }

