using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingApp
    {
    public static class DbManager
        {
        private static readonly string connectionString = "server=localhost;database=scheduling_db;user=root;password=MySQLpa$$1;";

        // Retrieve Customers datatable
        public static DataTable GetCustomers()
            {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = "SELECT CustomerID, Name, Email, Phone, Address FROM customers";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                    adapter.Fill(dt);
                    }
                }
            return dt;
            }

        // Retrieve Appointments datatable for a specific customer
        public static DataTable GetAppointments(int customerId)
            {
            DataTable dt = new DataTable();DbManager.GetAllAppointments();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string query = @"
                    SELECT a.AppointmentID,
                           c.Name AS CustomerName,  -- Use 'Name' to match your table
                           a.Title AS AppointmentType,
                           a.Start,
                           a.End
                    FROM appointments a
                    JOIN customers c ON a.CustomerID = c.CustomerID
                    WHERE a.CustomerID = @CustomerID
                    ORDER BY a.Start";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                        adapter.Fill(dt);
                        }
                    }
                }

            return dt;
            }

        // Get all appointments from all customers
        public static DataTable GetAllAppointments()
            {
            DataTable dt = new DataTable();
            string query = @"
            SELECT a.AppointmentID,
                   c.Name AS CustomerName,
                   a.Title AS AppointmentType,
                   a.Start,
                   a.End
            FROM appointments a
            JOIN customers c ON a.CustomerID = c.CustomerID
            ORDER BY a.Start;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                conn.Open();
                adapter.Fill(dt);
                }

            return dt;
            }

        // Add new customer
        public static void AddCustomer(string name, string email, string phone, string address)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = "INSERT INTO customers (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)";
                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // Update existing customer
        public static void UpdateCustomer(int customerId, string name, string email, string phone, string address)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = "UPDATE customers SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address WHERE CustomerID=@CustomerID";
                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // Delete customer
        public static void DeleteCustomer(int customerId)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = "DELETE FROM customers WHERE CustomerID=@CustomerID";
                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.ExecuteNonQuery();
                    }
                }
            }

        public static DataRow GetUpcomingAppointmentWithinMinutes(int userId, int minutes)
            {
            DataTable dt = new DataTable();

            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                DateTime now = DateTime.Now;
                now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0); // drop seconds

                DateTime soon = now.AddMinutes(minutes);

                string sql = @"
            SELECT AppointmentID, Title, Start, End
            FROM appointments
            WHERE UserID = @UserID
              AND Start >= @Now
              AND Start <= @Soon
            ORDER BY Start
            LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, conn))
                    {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Now", now);
                    cmd.Parameters.AddWithValue("@Soon", soon);

                    using (var adapter = new MySqlDataAdapter(cmd))
                        {
                        adapter.Fill(dt);
                        }
                    }
                }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }

        public static int GetUserIdByUsername(string username)
            {
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand("SELECT UserID FROM users WHERE UserName = @UserName LIMIT 1;", conn))
                {
                conn.Open();
                cmd.Parameters.AddWithValue("@UserName", username);
                object result = cmd.ExecuteScalar();
                if (result == null) throw new Exception("User not found in database.");
                return Convert.ToInt32(result);
                }
            }

        // Get appointments by specific date
        public static DataTable GetAppointmentsByDate(DateTime date)
            {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string query = @"
            SELECT a.AppointmentID,
                   c.Name AS CustomerName,
                   a.Title AS AppointmentType,
                   a.Start,
                   a.End
            FROM appointments a
            JOIN customers c ON a.CustomerID = c.CustomerID
            WHERE DATE(a.Start) = @date
            ORDER BY a.Start";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@date", date.Date);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                        adapter.Fill(dt);
                        }
                    }
                }

            return dt;
            }

        // Get a single appointment by ID
        public static DataRow GetAppointmentById(int appointmentId)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string query = @"
SELECT a.AppointmentID,
       a.CustomerID,
       c.Name AS CustomerName,
       a.Title AS AppointmentType,
       a.Start,
       a.End
FROM appointments a
JOIN customers c ON a.CustomerID = c.CustomerID
WHERE a.AppointmentID = @AppointmentID";

                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);

                    using (var adapter = new MySqlDataAdapter(cmd))
                        {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return (dt.Rows.Count > 0) ? dt.Rows[0] : null;
                        }
                    }
                }
            }


        //Check for overlapping appointments
        public static bool AppointmentOverlapsForUser(int? appointmentId, int userId, DateTime newStart, DateTime newEnd)
            {
            const string sql = @"
        SELECT COUNT(*)
        FROM appointments
        WHERE UserID = @UserID
          AND Start < @NewEnd
          AND End > @NewStart
          AND (@AppointmentID IS NULL OR AppointmentID <> @AppointmentID);";

            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(sql, conn))
                {
                conn.Open();
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@NewStart", newStart);
                cmd.Parameters.AddWithValue("@NewEnd", newEnd);
                cmd.Parameters.AddWithValue(
                    "@AppointmentID",
                    appointmentId.HasValue ? (object)appointmentId.Value : DBNull.Value
                );

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
                }
            }

        // Add new appointment
        public static void AddAppointment(int customerId, int userId, string title, DateTime start, DateTime end)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = @"
            INSERT INTO appointments (CustomerID, UserID, Title, Start, End)
            VALUES (@CustomerID, @UserID, @Title, @Start, @End)";

                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Start", start);
                    cmd.Parameters.AddWithValue("@End", end);
                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // Update existing appointment
        public static void UpdateAppointment(int appointmentId, int customerId, int userId, string title, DateTime start, DateTime end)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = @"
            UPDATE appointments
            SET CustomerID=@CustomerID,
                UserID=@UserID,
                Title=@Title,
                Start=@Start,
                End=@End
            WHERE AppointmentID=@AppointmentID";

                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Start", start);
                    cmd.Parameters.AddWithValue("@End", end);
                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // Delete appointment
        public static void DeleteAppointment(int appointmentId)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = "DELETE FROM appointments WHERE AppointmentID=@AppointmentID";
                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // Helper method to get a new database connection
        public static MySqlConnection GetConnection()
            {
            return new MySqlConnection(connectionString);
            }

        // Get appointments for reports
        public static DataTable GetAppointmentsForReports()
            {
            DataTable dt = new DataTable();

            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string sql = @"
            SELECT 
                a.AppointmentID,
                a.UserID,
                u.UserName,
                a.CustomerID,
                c.Name AS CustomerName,
                a.Title AS AppointmentType,
                a.Start,
                a.End
            FROM appointments a
            JOIN customers c ON a.CustomerID = c.CustomerID
            JOIN users u ON a.UserID = u.UserID;
        ";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                    {
                    adapter.Fill(dt);
                    }
                }

            return dt;
            }

        }
    }