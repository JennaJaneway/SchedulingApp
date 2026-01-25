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
    public partial class Customers : Form
        {
        public Customers()
            {
            InitializeComponent();
            DataGridViewCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewCustomers.MultiSelect = false;

            // Prevent auto-select on load/bind
            DataGridViewCustomers.DataBindingComplete += DataGridViewCustomers_DataBindingComplete;

            // Extra safety: after the form is displayed
            this.Shown += Customers_Shown;
            }

        private void DataGridViewCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
            {
            ClearCustomerSelection();
            }

        private void Customers_Shown(object sender, EventArgs e)
            {
            ClearCustomerSelection();
            }

        private void ClearCustomerSelection()
            {
            DataGridViewCustomers.ClearSelection();

            // Clear "current" row/cell that WinForms loves to keep
            if (DataGridViewCustomers.Rows.Count > 0)
                {
                DataGridViewCustomers.CurrentCell = null;
                }
            }

        private void CustomerForm_Load(object sender, EventArgs e)
            {
            try
                {
                DataGridViewCustomers.AutoGenerateColumns = true;

                DataTable customersData = DbManager.GetCustomers();
                DataGridViewCustomers.DataSource = customersData;
                DataGridViewCustomers.ClearSelection();
                DataGridViewCustomers.CurrentCell = null;

                // Hides CustomerID column after binding
                if (DataGridViewCustomers.Columns.Contains("CustomerID"))
                    {
                    DataGridViewCustomers.Columns["CustomerID"].Visible = false;
                    }

                // General DataGridView colors
                DataGridViewCustomers.BackgroundColor = Color.LightGray;
                DataGridViewCustomers.BorderStyle = BorderStyle.None;
                DataGridViewCustomers.EnableHeadersVisualStyles = false;

                // Column headers
                DataGridViewCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
                DataGridViewCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                DataGridViewCustomers.ColumnHeadersDefaultCellStyle.Font =
                    new Font(DataGridViewCustomers.Font, FontStyle.Bold);

                // Row headers
                DataGridViewCustomers.RowHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
                DataGridViewCustomers.RowHeadersDefaultCellStyle.ForeColor = Color.White;

                // Rows
                DataGridViewCustomers.RowsDefaultCellStyle.BackColor = Color.White;
                DataGridViewCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                DataGridViewCustomers.RowsDefaultCellStyle.ForeColor = Color.Black;

                // Selection
                DataGridViewCustomers.DefaultCellStyle.SelectionBackColor = Color.DarkRed;
                DataGridViewCustomers.DefaultCellStyle.SelectionForeColor = Color.White;

                DataGridViewCustomers.ClearSelection();
                DataGridViewCustomers.CurrentCell = null;
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error loading customers: " + ex.Message);
                }
            }


        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }

        private void Customers_Load(object sender, EventArgs e)
            {
            try
                {
                // Clear any previous styles to avoid designer overrides
                DataGridViewCustomers.AutoGenerateColumns = true;
                DataGridViewCustomers.EnableHeadersVisualStyles = false;
                DataGridViewCustomers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle();
                DataGridViewCustomers.RowHeadersDefaultCellStyle = new DataGridViewCellStyle();
                DataGridViewCustomers.RowsDefaultCellStyle = new DataGridViewCellStyle();
                DataGridViewCustomers.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle();
                DataGridViewCustomers.Font = new Font("Arial", 10, FontStyle.Regular);
                DataGridViewCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                DataGridViewCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                DataGridViewCustomers.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridViewCustomers.Font, FontStyle.Bold);
                DataGridViewCustomers.RowHeadersDefaultCellStyle.BackColor = Color.DarkGray;
                DataGridViewCustomers.RowHeadersDefaultCellStyle.ForeColor = Color.White;
                DataGridViewCustomers.RowsDefaultCellStyle.BackColor = Color.White;
                DataGridViewCustomers.RowsDefaultCellStyle.ForeColor = Color.Black;
                DataGridViewCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                DataGridViewCustomers.DefaultCellStyle.SelectionBackColor = Color.Black;
                DataGridViewCustomers.DefaultCellStyle.SelectionForeColor = Color.Gray;
                DataGridViewCustomers.RowStateChanged += DataGridViewCustomers_RowStateChanged;

                // Bind the data 
                DataTable customersData = DbManager.GetCustomers();
                DataGridViewCustomers.DataSource = customersData;
                DataGridViewCustomers.ClearSelection();
                DataGridViewCustomers.CurrentCell = null; 
                HideCustomerID();

                // Custom Column widths
                if (DataGridViewCustomers.Columns.Contains("Name"))
                    {
                    DataGridViewCustomers.Columns["Name"].Width = 150;
                    DataGridViewCustomers.Columns["Name"].HeaderText = "Customer Name";
                    }
                if (DataGridViewCustomers.Columns.Contains("Email"))
                    DataGridViewCustomers.Columns["Email"].Width = 240;
                if (DataGridViewCustomers.Columns.Contains("Phone"))
                    DataGridViewCustomers.Columns["Phone"].Width = 100;

                }
            catch (Exception ex)
                {
                MessageBox.Show("Error loading customers: " + ex.Message);
                }
            if (DataGridViewCustomers.Rows.Count > 0)
                {
                DataGridViewCustomers.ClearSelection();
                DataGridViewCustomers.CurrentCell = null;
                }
            }
        // Makes selected row bold
        private void DataGridViewCustomers_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
            {
            if (e.Row.Selected)
                {
                e.Row.DefaultCellStyle.Font = new Font(DataGridViewCustomers.Font, FontStyle.Bold);
                }
            else
                {
                e.Row.DefaultCellStyle.Font = new Font(DataGridViewCustomers.Font, FontStyle.Regular);
                }
            }

        private void ButtonAppointments_Click(object sender, EventArgs e)
            {
            Appointments apptForm;

            if (DataGridViewCustomers.SelectedRows.Count > 0)
                {
                var row = DataGridViewCustomers.SelectedRows[0];
                int customerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
                string customerName = row.Cells["Name"].Value.ToString();

                apptForm = new Appointments(customerId, customerName);
                }
            else
                {
                apptForm = new Appointments();
                }

            apptForm.Show();
            this.Close();
            }

        // Double-click on a row event
        private void DataGridViewCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
            if (e.RowIndex >= 0)
                {
                var row = DataGridViewCustomers.Rows[e.RowIndex];
                int customerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
                string name = row.Cells["Name"].Value.ToString();
                string email = row.Cells["Email"].Value.ToString();
                string phone = row.Cells["Phone"].Value.ToString();
                string address = row.Cells["Address"].Value.ToString();

                var form = new CustomerInfo(customerId, name, email, phone, address);
                form.Show();
                this.Close();
                }
            }

        // Add Customer button
        private void ButtonAdd_Click(object sender, EventArgs e)
            {
            // Open customerinfo in Add mode
            var form = new CustomerInfo();
            form.Show();
            this.Close();
            }

        // Edit Customer button
        private void ButtonEdit_Click(object sender, EventArgs e)
            {
            if (DataGridViewCustomers.SelectedRows.Count == 0)
                {
                MessageBox.Show("Please select a customer to edit.");
                return;
                }

            var row = DataGridViewCustomers.SelectedRows[0];
            int customerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
            string name = row.Cells["Name"].Value.ToString();
            string email = row.Cells["Email"].Value.ToString();
            string phone = row.Cells["Phone"].Value.ToString();
            string address = row.Cells["Address"].Value.ToString();

            // Pass selected customer data to customerinfo (Edit mode)
            var form = new CustomerInfo(customerId, name, email, phone, address);
            form.Show();
            this.Close();
            }

        // Delete Customer button
        private void ButtonDelete_Click(object sender, EventArgs e)
            {
            // Make sure a row is selected
            if (DataGridViewCustomers.CurrentRow == null)
                {
                MessageBox.Show("Please select a customer from the grid to delete.", "No Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                }

            int customerId = Convert.ToInt32(
                DataGridViewCustomers.CurrentRow.Cells["CustomerID"].Value);

            string customerName =
                DataGridViewCustomers.CurrentRow.Cells["Name"].Value.ToString();

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete '{customerName}'?\n" +
                "This will also delete related appointments.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
                {
                DbManager.DeleteCustomer(customerId);

                MessageBox.Show("Customer deleted successfully.",
                    "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Rebind
                DataGridViewCustomers.DataSource = DbManager.GetCustomers();
                DataGridViewCustomers.ClearSelection();
                DataGridViewCustomers.CurrentCell = null;

                // RE-HIDE ID COLUMN
                HideCustomerID();

                DataGridViewCustomers.ClearSelection();
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error deleting customer: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        // Reports button
        private void ButtonReports_Click(object sender, EventArgs e)
            {
            new Reports().Show();
            }

        // Clear Selection button
        private void ButtonClearSelection_Click(object sender, EventArgs e)
            {
            DataGridViewCustomers.ClearSelection();
            DataGridViewCustomers.CurrentCell = null; // important
            }

        // Hides CustomerID column
        private void HideCustomerID()
            {
            if (DataGridViewCustomers.Columns.Contains("CustomerID"))
                DataGridViewCustomers.Columns["CustomerID"].Visible = false;
            }
        }
    }
