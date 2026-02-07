using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SchedulingApp
    {
    public partial class CustomerInfo : Form
        {
        private readonly int? _customerId;      // nullable so HasValue/Value works
        private readonly bool _isEditMode;

        public Form PreviousCustomersForm { get; set; }

        // ADD mode
        public CustomerInfo()
            {
            InitializeComponent();

            _isEditMode = false;
            _customerId = null;
            SetupForm();
            if (_customerId.HasValue)
                LoadCustomer(_customerId.Value);
            }

        public CustomerInfo(int customerId)
            {
            InitializeComponent();

            _isEditMode = true;
            _customerId = customerId;

            SetupForm();
            LoadCustomer(customerId);
            }

        private void SetupForm()
            {
            if (!_isEditMode)
                {
                this.Text = "Add Customer";
                buttonDelete.Enabled = false;
                }
            else
                {
                this.Text = "Edit Customer";
                buttonDelete.Enabled = true;
                }
            }

        // ***** SAVE BUTTON *****
        private void ButtonSave_Click(object sender, EventArgs e)
            {
            string name = textBoxName.Text.Trim();
            string address = textBoxAddress.Text.Trim();
            string city = textBoxCity.Text.Trim();
            string country = textBoxCountry.Text.Trim();
            string zip = textBoxZip.Text.Trim();
            string phone = TextBoxPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(country) ||
                string.IsNullOrWhiteSpace(zip))
                {
                MessageBox.Show("All fields are required.");
                return;
                }

            if (!Regex.IsMatch(name, @"^[A-Za-z\s\-']+$"))
                {
                MessageBox.Show("Name field must use letters only.");
                return;
                }

            if (!System.Text.RegularExpressions.Regex.IsMatch(country, @"^[A-Za-z\s\.\-]{2,50}$"))
                {
                MessageBox.Show("Country field must use letters only.");
                return;
                }

            if (!System.Text.RegularExpressions.Regex.IsMatch(zip, @"^[0-9\-]{3,10}$"))
                {
                MessageBox.Show("Zip/Postal Code must be digits (and optional dash).");
                return;
                }

            string formattedPhone = InputValidator.ValidateAndFormatPhone(phone);
            if (formattedPhone == null)
                {
                MessageBox.Show("Phone number must be exactly 10 digits.\nFormat: 123-456-7890",
                                "Invalid Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxPhone.Focus();
                return;
                }

            phone = formattedPhone;
            TextBoxPhone.Text = formattedPhone;

            if (_isEditMode && _customerId.HasValue)
                {
                DbManager.UpdateCustomer(
                    _customerId.Value,
                    name,
                    phone,
                    address,
                    city,
                    country,
                    zip
                );
                }
            else
                {
                DbManager.AddCustomer(
                    name,
                    phone,
                    address,
                    city,
                    country,
                    zip
                );
                }

            MessageBox.Show("Customer saved successfully.");
            OpenCustomersForm();
            }

        //  ***** DELETE BUTTON *****
        private void ButtonDelete_Click(object sender, EventArgs e)
            {
            if (_isEditMode && _customerId.HasValue)
                {
                var confirm = MessageBox.Show("Are you sure you want to delete this customer?",
                                              "Confirm Delete",
                                              MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                    {
                    DbManager.DeleteCustomer(_customerId.Value);
                    MessageBox.Show("Customer deleted.");
                    OpenCustomersForm();
                    }
                }
            }

        // Live phone number input formatting
        private void TextBoxPhone_TextChanged(object sender, EventArgs e)
            {
            // Reformat as user types
            string formatted = InputValidator.ValidateAndFormatPhone(TextBoxPhone.Text);

            if (formatted != null && TextBoxPhone.Text != formatted)
                {
                TextBoxPhone.Text = formatted;
                TextBoxPhone.SelectionStart = TextBoxPhone.Text.Length;
                }
            }

        // Navigate to Customers form
        private void OpenCustomersForm()
            {
            // Close the previous Customers form
            PreviousCustomersForm?.Close();

            // Open a brand-new Customers form (fresh data)
            var customersForm = new Customers();
            customersForm.Show();

            // Close this CustomerInfo form
            this.Close();
            }

        private void LoadCustomer(int customerId)
            {
            DataRow row = DbManager.GetCustomerById(customerId);

            if (row == null)
                {
                MessageBox.Show("Customer not found.");
                return;
                }

            textBoxName.Text = row["Name"]?.ToString();
            TextBoxPhone.Text = row["Phone"]?.ToString();
            textBoxAddress.Text = row["Address"]?.ToString();
            textBoxCity.Text = row["City"]?.ToString();
            textBoxCountry.Text = row["Country"]?.ToString();
            textBoxZip.Text = row["PostalCode"]?.ToString();
            }

        // ***** CANCEL BUTTON *****
        private void ButtonCancel_Click(object sender, EventArgs e)
            {
            PreviousCustomersForm?.Show();
            this.Close();
            }

        // ***** APPOINTMENTS BUTTON *****
        private void ButtonAppointments_Click(object sender, EventArgs e)
            {
            if (!_customerId.HasValue)
                {
                MessageBox.Show("Please save the customer before viewing appointments.",
                                "No Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                }

            var apptsForm = new Appointments(_customerId.Value, textBoxName.Text.Trim());
            apptsForm.Show();
            this.Close(); // close CustomerInfo so only one form is open
            }

        // ***** CUSTOMERS BUTTON *****
        private void ButtonCustomers_Click(object sender, EventArgs e)
            {
            OpenCustomersForm();
            }
        }
    }
