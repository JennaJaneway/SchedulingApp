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

            this.AcceptButton = ButtonLogin; // Pressing Enter anywhere triggers login
        }

    private void LoginForm_Load(object sender, EventArgs e)
            {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture; // Get current system culture (language and region)
            RegionInfo region = new RegionInfo(currentCulture.Name); // Get region info from the culture
            LabelLocation.Text = "Location: " + region.EnglishName;
            LabelLanguage.Text = "Language: " + currentCulture.DisplayName;
            ButtonLogin.Text = Resources.LoginButtonText;
            LabelUsername.Text = Resources.UsernameLabel;
            LabelPassword.Text = Resources.PasswordLabel;
            this.BackgroundImage = Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            }

        private void ButtonLogin_Click(object sender, EventArgs e)
            {
            string username = TextBoxUsername.Text.Trim();
            string password = TextBoxPassword.Text.Trim();

            if (username == "test" && password == "test")
                {

                // Record login in the log file
                string logLine = $"{DateTime.Now}: {username}";
                System.IO.File.AppendAllText("Login_History.txt", logLine + Environment.NewLine);

                // Set session
                Session.CurrentUserId = DbManager.GetUserIdByUsername(username);
                Session.CurrentUsername = username;

                // Alert if appointment within 15 minutes
                DataRow upcoming = DbManager.GetUpcomingAppointmentWithinMinutes(Session.CurrentUserId, 15);
                if (upcoming != null)
                    {
                    DateTime start = Convert.ToDateTime(upcoming["Start"]);
                    string title = upcoming["Title"].ToString();

                    MessageBox.Show(
                        $"Hello, you have an appointment within 15 minutes!\n\n" +
                        $"Title: {title}\n at {start: hh:mm tt}",
                        "Upcoming Appointment Alert",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    }

                // Open Customers form
                Customers customers = new Customers();
                customers.Show();
                this.Hide();
                }

            else
                {
                // Failed login, show error
                MessageBox.Show(Properties.Resources.LoginError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
