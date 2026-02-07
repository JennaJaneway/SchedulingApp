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
        private bool _needsRefresh;
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

            // Clears "current" row/cell
            if (DataGridViewCustomers.Rows.Count > 0)
                {
                DataGridViewCustomers.CurrentCell = null;
                }
            }

        private void Customers_Load(object sender, EventArgs e)
            {
            try
                {
                GridStyles.ApplyStandardStyle(DataGridViewCustomers);

                LoadCustomers();
                }
            catch (Exception ex)
                {
                MessageBox.Show("Error loading customers: " + ex.Message);
                }
            }

        // Reloads customer data from DB
        private void LoadCustomers()
            {
            DataGridViewCustomers.DataSource = DbManager.GetCustomers();

            // Hides columns that don't need to be displayed
            HideIfExists("Active");
            HideCustomerID();

            // Column formatting
            SetWidthIfExists("Name", 150);
            SetHeaderIfExists("Name", "Customer"); // Renames column header
            SetWidthIfExists("Phone", 80);
            SetWidthIfExists("City", 90);
            SetWidthIfExists("Country", 50);
            SetHeaderIfExists("PostalCode", "Postal Code"); // Renames column header
            SetWidthIfExists("PostalCode", 60);

            SetDisplayIndexIfExists("PostalCode", DataGridViewCustomers.Columns.Count - 1); // Puts PostalCode last

            DataGridViewCustomers.ClearSelection();
            DataGridViewCustomers.CurrentCell = null; 

            // ----- local helper methods -----
            void HideIfExists(string colName)
                {
                if (DataGridViewCustomers.Columns.Contains(colName))
                    DataGridViewCustomers.Columns[colName].Visible = false;
                }

            void SetWidthIfExists(string colName, int width)
                {
                if (DataGridViewCustomers.Columns.Contains(colName))
                    DataGridViewCustomers.Columns[colName].Width = width;
                }

            void SetHeaderIfExists(string colName, string headerText)
                {
                if (DataGridViewCustomers.Columns.Contains(colName))
                    DataGridViewCustomers.Columns[colName].HeaderText = headerText;
                }

            void SetDisplayIndexIfExists(string colName, int displayIndex)
                {
                if (DataGridViewCustomers.Columns.Contains(colName))
                    DataGridViewCustomers.Columns[colName].DisplayIndex = displayIndex;
                }
            }


        // ********** APPOINTMENTS BUTTON **********
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
                int customerId = Convert.ToInt32(
                    DataGridViewCustomers.Rows[e.RowIndex].Cells["CustomerID"].Value
                );

                var form = new CustomerInfo(customerId);
                form.Show();
                this.Close();
                }
            }

        // *********** ADD CUSTOMER BUTTON ***********
        private void ButtonAdd_Click(object sender, EventArgs e)
            {
            // Open customerinfo in Add mode
            var form = new CustomerInfo();
            form.PreviousCustomersForm = this;
            this.Hide();
            form.Show();
            }

        // *********** EDIT CUSTOMER BUTTON ************
        private void ButtonEdit_Click(object sender, EventArgs e)
            {
            if (DataGridViewCustomers.CurrentRow == null)
                {
                MessageBox.Show("Please select a customer.");
                return;
                }

            _needsRefresh = true;
            int customerId = Convert.ToInt32(DataGridViewCustomers.CurrentRow.Cells["CustomerID"].Value);

            var form = new CustomerInfo(customerId);
            form.PreviousCustomersForm = this;   // pass reference
            this.Hide();                         // Makes Edit form disappear immediately
            form.Show();
            }

        // ************** DELETE CUSTOMER BUTTON **************
        private void ButtonDelete_Click(object sender, EventArgs e)
            {
            // Makes sure a row is selected
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
                "This will also delete all of the customer's appointments.",
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
                if (DataGridViewCustomers.Columns.Contains("Active"))
                    DataGridViewCustomers.Columns["Active"].Visible = false;
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

        // ********** REPORTS BUTTON **********
        private void ButtonReports_Click(object sender, EventArgs e)
            {
            new Reports().Show();
            }

        // *********** CLEAR SELECTION BUTTON ***********
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

        // Refresh data when form is activated
        private void Customers_Activated(object sender, EventArgs e)
            {
            if (!_needsRefresh) return;
            _needsRefresh = false;
            LoadCustomers();
            }

        }
    }
