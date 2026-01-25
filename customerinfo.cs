using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SchedulingApp
    {
    public partial class CustomerInfo : Form
        {
        private readonly bool isEditMode;
        private readonly int? customerId; 

        // Add mode
        public CustomerInfo() 
            {
            InitializeComponent();
            isEditMode = false;
            customerId = null;
            SetupForm();
            }

        // Edit mode
        public CustomerInfo(int customerId, string name, string email, string phone, string address) 
            {
            InitializeComponent();
            isEditMode = true;
            this.customerId = customerId;
            SetupForm();

            // Load customer data into fields
            textBoxName.Text = name;
            textBoxEmail.Text = email;
            TextBoxPhone.Text = phone;
            textBoxAddress.Text = address;
            }

        private void SetupForm()
            {
            if (!isEditMode)
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

        // Save button
        private void ButtonSave_Click(object sender, EventArgs e)
            {
            string name = textBoxName.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string phone = TextBoxPhone.Text.Trim();
            string address = textBoxAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
                {
                MessageBox.Show("All fields are required.");
                return;
                }
            
            string formattedPhone = InputValidator.ValidateAndFormatPhone(phone); // Validate and format phone number
            if (formattedPhone == null)
                {
                MessageBox.Show("Phone number must be exactly 10 digits.\nFormat: 123-456-7890",
                                "Invalid Phone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxPhone.Focus();
                return;
                }

            phone = formattedPhone;  // Overwrite with formatted value so it’s consistent
            TextBoxPhone.Text = formattedPhone;

            if (isEditMode && customerId.HasValue)
                {
                DbManager.UpdateCustomer(customerId.Value, name, email, phone, address); // Update existing
                }
            else
                {
                DbManager.AddCustomer(name, email, phone, address); // Insert new
                }

            MessageBox.Show("Customer saved successfully.");
            OpenCustomersForm();
            }

        // Delete button
        private void ButtonDelete_Click(object sender, EventArgs e)
            {
            if (isEditMode && customerId.HasValue)
                {
                var confirm = MessageBox.Show("Are you sure you want to delete this customer?",
                                              "Confirm Delete",
                                              MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                    {
                    DbManager.DeleteCustomer(customerId.Value);
                    MessageBox.Show("Customer deleted.");
                    OpenCustomersForm();
                    }
                }
            }

        // Cancel button
        private void ButtonCancel_Click(object sender, EventArgs e)
            {
            OpenCustomersForm();
            }

        // Navigation to Customers
        private void ButtonCustomers_Click(object sender, EventArgs e) => OpenCustomersForm();



        // Navigation to Appointments
        private void ButtonAppointments_Click(object sender, EventArgs e)
            {
            if (!customerId.HasValue)
                {
                MessageBox.Show("Please save the customer before viewing appointments.",
                                "No Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                }

        // Use the overloaded constructor: pass customerId + Name
        var apptsForm = new Appointments(customerId.Value, textBoxName.Text.Trim());
            apptsForm.Show();
            this.Close(); // close CustomerInfo so only one form is open
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
            var customersForm = new Customers();
            customersForm.Show();
            this.Close();
            }
        }
    }
