using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SchedulingApp.Properties;

namespace SchedulingApp
    { 
    public partial class LoginForm : Form
        {
        public LoginForm()
            {
            InitializeComponent();

            this.AcceptButton = ButtonLogin; // Allows "Enter" key to trigger login
        }

    private void LoginForm_Load(object sender, EventArgs e)
            {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture; // Gets current system culture (language and region)
            RegionInfo region = new RegionInfo(currentCulture.Name); // Gets region info from the culture
            LabelLocation.Text = "Location: " + region.EnglishName;
            LabelLanguage.Text = "Language: " + currentCulture.DisplayName;
            ButtonLogin.Text = Resources.LoginButtonText;
            LabelUsername.Text = Resources.UsernameLabel;
            LabelPassword.Text = Resources.PasswordLabel;
            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            }

        // ************** LOGIN BUTTON CLICK **************
        private void ButtonLogin_Click(object sender, EventArgs e)
            {
            string username = TextBoxUsername.Text.Trim();
            string password = TextBoxPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                MessageBox.Show("Please enter a username and password.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                }

            try
                {
                // Authenticates against DB
                int userId = DbManager.ValidateUser(username, password); // return userId if valid, else -1

                if (userId < 0)
                    {
                    MessageBox.Show(Properties.Resources.LoginError, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    }

                // Record login in the log file (use a stable path)
                string logLine = $"{DateTime.Now}: {username}";
                string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login_History.txt");
                System.IO.File.AppendAllText(logPath, logLine + Environment.NewLine);

                // Set session
                Session.CurrentUserId = userId;
                Session.CurrentUsername = username;

                // Alert if appointment within 15 minutes
                DataRow upcoming = DbManager.GetUpcomingAppointmentWithinMinutes(Session.CurrentUserId, 15);
                if (upcoming != null)
                    {
                    DateTime startUtc = Convert.ToDateTime(upcoming["start"]);
                    DateTime startLocal = DateTime.SpecifyKind(startUtc, DateTimeKind.Utc).ToLocalTime();
                    string title = upcoming["title"].ToString();

                    MessageBox.Show(
                        $"Good day! You have an appointment within 15 minutes!\n\nTitle: {title}\n at {startLocal:hh:mm tt}",
                        "Upcoming Appointment Alert",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    }

                Customers customers = new Customers();
                customers.Show();
                this.Hide();
                }
            catch (MySqlException ex)
                {
                MessageBox.Show(
                    "Database connection failed. Whomp, whomp.\n\n" + ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                }
            catch (Exception ex)
                {
                MessageBox.Show(
                    "An unexpected error occurred during login. Whomp, whomp.\n\n" + ex.Message,
                    "Login Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                }
            }

        }
    }
