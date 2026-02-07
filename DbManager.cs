using MySqlConnector;
using System;
using System.Configuration;
using System.Data;

namespace SchedulingApp
    {
    public static class DbManager
        {
        private static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["client_schedule"].ConnectionString;

        public static MySqlConnection GetConnection() => new MySqlConnection(connectionString);

        // Validates user credentials
        public static int ValidateUser(string username, string password)
            {
            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(
                "SELECT userId FROM `user` WHERE userName = @UserName AND password = @Password LIMIT 1;",
                conn))
                {
                conn.Open();
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);

                object result = cmd.ExecuteScalar();
                return (result == null) ? -1 : Convert.ToInt32(result);
                }
            }

        // Retrieves Customers datatable
        public static DataTable GetCustomers()
            {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string query = @"
                SELECT
                    c.customerId   AS CustomerID,
                    c.customerName AS Name,
                    a.phone        AS Phone,
                    a.address      AS Address,
                    a.postalCode   AS PostalCode,
                    ci.city        AS City,
                    co.country     AS Country,
                    c.active       AS Active
                FROM customer c
                JOIN address a   ON c.addressId = a.addressId
                JOIN city ci     ON a.cityId = ci.cityId
                JOIN country co  ON ci.countryId = co.countryId
                ORDER BY c.customerId;";

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
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string sql = @"
                SELECT
                    a.appointmentId,
                    c.customerName AS CustomerName,
                    a.title        AS AppointmentType,
                    a.start        AS Start,
                    a.end          AS End
                FROM appointment a
                JOIN customer c ON a.customerId = c.customerId
                WHERE a.customerId = @CustomerID
                ORDER BY a.start;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
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
            string sql = @"
            SELECT
                a.appointmentId,
                c.customerName AS CustomerName,
                a.title        AS AppointmentType,
                a.start        AS Start,
                a.end          AS End
            FROM appointment a
            JOIN customer c ON a.customerId = c.customerId
            ORDER BY a.start;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                conn.Open();
                adapter.Fill(dt);
                }

            return dt;
            }

        public static DataRow GetCustomerById(int customerId)
            {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string query = @"
                SELECT
                    c.customerId   AS CustomerID,
                    c.customerName AS Name,
                    a.phone        AS Phone,
                    a.address      AS Address,
                    a.postalCode   AS PostalCode,
                    ci.city        AS City,
                    co.country     AS Country,
                    c.active       AS Active
                FROM customer c
                JOIN address a   ON c.addressId = a.addressId
                JOIN city ci     ON a.cityId = ci.cityId
                JOIN country co  ON ci.countryId = co.countryId
                WHERE c.customerId = @CustomerID
                LIMIT 1;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                        adapter.Fill(dt);
                        }
                    }
                }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }

        // Add new customer
        public static void AddCustomer(string name, string phone, string address, string city, string country, string zip)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                    {
                    // Ensures country exists
                    var countryCmd = new MySqlCommand(@"
                INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy)
                SELECT @Country, NOW(), @User, NOW(), @User
                WHERE NOT EXISTS (SELECT 1 FROM country WHERE country = @Country);", conn, tx);

                    countryCmd.Parameters.AddWithValue("@Country", country);
                    countryCmd.Parameters.AddWithValue("@User", Session.CurrentUsername);
                    countryCmd.ExecuteNonQuery();

                    // Ensures city exists (tied to that country)
                    var cityCmd = new MySqlCommand(@"
                    INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy)
                    SELECT @City,
                           (SELECT countryId FROM country WHERE country = @Country LIMIT 1),
                           NOW(), @User, NOW(), @User
                    WHERE NOT EXISTS (
                        SELECT 1 FROM city
                        WHERE city = @City AND countryId = (SELECT countryId FROM country WHERE country = @Country LIMIT 1)
                    );", conn, tx);

                    cityCmd.Parameters.AddWithValue("@City", city);
                    cityCmd.Parameters.AddWithValue("@Country", country);
                    cityCmd.Parameters.AddWithValue("@User", Session.CurrentUsername);
                    cityCmd.ExecuteNonQuery();

                    // Insert address (cityId must match city + country)
                    var addressCmd = new MySqlCommand(@"
                    INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES (
                        @Address,
                        '',
                        (SELECT ci.cityId
                           FROM city ci
                           JOIN country co ON co.countryId = ci.countryId
                          WHERE ci.city = @City AND co.country = @Country
                          LIMIT 1),
                        @Postal,
                        @Phone,
                        NOW(), @User, NOW(), @User
                    );", conn, tx);

                    addressCmd.Parameters.AddWithValue("@Address", address);
                    addressCmd.Parameters.AddWithValue("@City", city);
                    addressCmd.Parameters.AddWithValue("@Country", country);
                    addressCmd.Parameters.AddWithValue("@Postal", zip);
                    addressCmd.Parameters.AddWithValue("@Phone", phone);
                    addressCmd.Parameters.AddWithValue("@User", Session.CurrentUsername);
                    addressCmd.ExecuteNonQuery();


                    long addressId = addressCmd.LastInsertedId;

                    // Insert customer
                    var customerCmd = new MySqlCommand(@"
                    INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdateBy)
                    VALUES (@Name, @AddressId, 1, NOW(), @User, @User);", conn, tx);

                    customerCmd.Parameters.AddWithValue("@Name", name);
                    customerCmd.Parameters.AddWithValue("@AddressId", addressId);
                    customerCmd.Parameters.AddWithValue("@User", Session.CurrentUsername); // or "app" if you prefer
                    customerCmd.ExecuteNonQuery();

                    tx.Commit();
                    }
                }
            }

        // Update existing customer
        public static void UpdateCustomer(int customerId, string name, string phone, string address, string city, string country, string zip)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                    {
                    // Update customer name
                    var cmd1 = new MySqlCommand(@"
                    UPDATE customer
                    SET customerName = @Name,
                        lastUpdate = NOW(),
                        lastUpdateBy = @User
                    WHERE customerId = @CustomerID;", conn, tx);

                    cmd1.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd1.Parameters.AddWithValue("@Name", name);
                    cmd1.Parameters.AddWithValue("@User", Session.CurrentUsername ?? "app");
                    cmd1.ExecuteNonQuery();

                    var ensureCountryCmd = new MySqlCommand(@"
                    INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy)
                    SELECT @Country, NOW(), @User, NOW(), @User
                    WHERE NOT EXISTS (SELECT 1 FROM country WHERE country = @Country);", conn, tx);

                    ensureCountryCmd.Parameters.AddWithValue("@Country", country);
                    ensureCountryCmd.Parameters.AddWithValue("@User", Session.CurrentUsername ?? "app");
                    ensureCountryCmd.ExecuteNonQuery();

                    var ensureCityCmd = new MySqlCommand(@"
                    INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy)
                    SELECT @City,
                           (SELECT countryId FROM country WHERE country = @Country LIMIT 1),
                           NOW(), @User, NOW(), @User
                    WHERE NOT EXISTS (
                        SELECT 1 FROM city
                        WHERE city = @City
                          AND countryId = (SELECT countryId FROM country WHERE country = @Country LIMIT 1)
                    );", conn, tx);

                    ensureCityCmd.Parameters.AddWithValue("@City", city);
                    ensureCityCmd.Parameters.AddWithValue("@Country", country);
                    ensureCityCmd.Parameters.AddWithValue("@User", Session.CurrentUsername ?? "app");
                    ensureCityCmd.ExecuteNonQuery();

                    var getCityIdCmd = new MySqlCommand(@"
                    SELECT ci.cityId
                    FROM city ci
                    JOIN country co ON co.countryId = ci.countryId
                    WHERE ci.city = @City AND co.country = @Country
                    LIMIT 1;", conn, tx);

                    getCityIdCmd.Parameters.AddWithValue("@City", city);
                    getCityIdCmd.Parameters.AddWithValue("@Country", country);

                    object cityIdObj = getCityIdCmd.ExecuteScalar();
                    if (cityIdObj == null)
                        throw new Exception("Could not resolve cityId for the given City/Country.");

                    int cityId = Convert.ToInt32(cityIdObj);

                    // Update linked address fields (now uses @CityId and @Postal)
                    var cmd2 = new MySqlCommand(@"
                    UPDATE address a
                    JOIN customer c ON c.addressId = a.addressId
                    SET a.address = @Address,
                        a.phone = @Phone,
                        a.postalCode = @Postal,
                        a.cityId = @CityId,
                        a.lastUpdate = NOW(),
                        a.lastUpdateBy = @User
                    WHERE c.customerId = @CustomerID;", conn, tx);

                    cmd2.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd2.Parameters.AddWithValue("@Address", address);
                    cmd2.Parameters.AddWithValue("@Phone", phone);
                    cmd2.Parameters.AddWithValue("@Postal", zip);
                    cmd2.Parameters.AddWithValue("@CityId", cityId);
                    cmd2.Parameters.AddWithValue("@User", Session.CurrentUsername ?? "app");
                    cmd2.ExecuteNonQuery();

                    tx.Commit();
                    }
                }
            }

        // ******* DELETE CUSTOMER (also deletes appointments and address) *******
        public static void DeleteCustomer(int customerId)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                    {
                    // Remove appointments first
                    var delAppts = new MySqlCommand("DELETE FROM appointment WHERE customerId = @CustomerID;", conn, tx);
                    delAppts.Parameters.AddWithValue("@CustomerID", customerId);
                    delAppts.ExecuteNonQuery();

                    // Get addressId
                    var getAddr = new MySqlCommand("SELECT addressId FROM customer WHERE customerId = @CustomerID LIMIT 1;", conn, tx);
                    getAddr.Parameters.AddWithValue("@CustomerID", customerId);
                    object addrObj = getAddr.ExecuteScalar();
                    int? addressId = (addrObj == null) ? (int?)null : Convert.ToInt32(addrObj);

                    // Delete customer
                    var delCust = new MySqlCommand("DELETE FROM customer WHERE customerId = @CustomerID;", conn, tx);
                    delCust.Parameters.AddWithValue("@CustomerID", customerId);
                    delCust.ExecuteNonQuery();

                    // Delete address row
                    if (addressId.HasValue)
                        {
                        var delAddr = new MySqlCommand("DELETE FROM address WHERE addressId = @AddressID;", conn, tx);
                        delAddr.Parameters.AddWithValue("@AddressID", addressId.Value);
                        delAddr.ExecuteNonQuery();
                        }

                    tx.Commit();
                    }
                }
            }

        // Check for upcoming appointment within X minutes for a user (used at login)
        public static DataRow GetUpcomingAppointmentWithinMinutes(int userId, int minutes)
            {
            DataTable dt = new DataTable();

            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                DateTime now = DateTime.UtcNow;
                DateTime soon = now.AddMinutes(minutes);

                string sql = @"
                SELECT appointmentId, title, start, end
                FROM appointment
                WHERE userId = @userId
                AND start >= @Now
                AND start <= @Soon
                ORDER BY start
                LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, conn))
                    {
                    cmd.Parameters.AddWithValue("@userId", userId);
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


        // Get appointments by specific date
        public static DataTable GetAppointmentsByDate(DateTime date)
            {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string sql = @"
                SELECT
                    a.appointmentId,
                    c.customerName AS CustomerName,
                    a.title        AS AppointmentType,
                    a.start        AS Start,
                    a.end          AS End
                FROM appointment a
                JOIN customer c ON a.customerId = c.customerId
                WHERE DATE(a.start) = @date
                ORDER BY a.start;";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
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

                string sql = @"
                SELECT
                    a.appointmentId,
                    a.customerId,
                    c.customerName AS CustomerName,
                    a.title        AS AppointmentType,
                    a.start        AS Start,
                    a.end          AS End
                FROM appointment a
                JOIN customer c ON a.customerId = c.customerId
                WHERE a.appointmentId = @appointmentId;";


                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);

                    using (var adapter = new MySqlDataAdapter(cmd))
                        {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return (dt.Rows.Count > 0) ? dt.Rows[0] : null;
                        }
                    }
                }
            }

        // Check for overlapping appointments
        public static bool AppointmentOverlapsForUser(int? appointmentId, int userId, DateTime newStart, DateTime newEnd)
            {
            const string sql = @"
            SELECT COUNT(*)
            FROM appointment
            WHERE userId = @userId
              AND start<@NewEnd
              AND end> @NewStart
              AND (@appointmentId IS NULL OR appointmentId <> @appointmentId); ";

            using (var conn = new MySqlConnection(connectionString))
            using (var cmd = new MySqlCommand(sql, conn))
                {
                conn.Open();
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@NewStart", newStart);
                cmd.Parameters.AddWithValue("@NewEnd", newEnd);
                cmd.Parameters.AddWithValue(
                    "@appointmentId",
                    appointmentId.HasValue ? (object)appointmentId.Value : DBNull.Value
                );

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
                }
            }

        // *********** ADD NEW APPOINTMENT ***********
        public static void AddAppointment(int customerId, int userId, string title, DateTime start, DateTime end)
            {
            // Required defaults section
            string description = "404. No appointment description found";
            string location = "In office";
            string contact = "N/A";
            string type = "N/A";
            string url = "404. No URL found.";
            string user = Session.CurrentUsername ?? "app";

            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string query = @"
                INSERT INTO appointment
                (customerId, userId, title, description, location, contact, type, url,
                 start, end, createDate, createdBy, lastUpdateBy)
                VALUES
                (@CustomerID, @UserID, @Title, @Description, @Location, @Contact, @Type, @Url,
                 @Start, @End, NOW(), @User, @User);";

                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Url", url);
                    cmd.Parameters.AddWithValue("@Start", start);
                    cmd.Parameters.AddWithValue("@End", end);
                    cmd.Parameters.AddWithValue("@User", user);

                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // ********** UPDATE APPOINTMENT **********
        public static void UpdateAppointment(
        int appointmentId,
        int customerId,
        int userId,
        string type,
        DateTime startUtc,
        DateTime endUtc)
            {
            // Backstage defaults 
            string user = Session.CurrentUsername ?? "app";
            string title = $"{type}";
            string description = "404. No appointment description found";
            string location = "N/A";
            string contact = "N/A";
            string url = "N/A";

            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();

                string query = @"
                UPDATE appointment
                SET customerId = @CustomerID,
                    userId = @UserID,
                    title = @Title,
                    description = @Description,
                    location = @Location,
                    contact = @Contact,
                    type = @Type,
                    url = @Url,
                    start = @Start,
                    end = @End,
                    lastUpdate = NOW(),
                    lastUpdateBy = @User
                WHERE appointmentId = @AppointmentID;";

                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Url", url);
                    cmd.Parameters.AddWithValue("@Start", startUtc);
                    cmd.Parameters.AddWithValue("@End", endUtc);
                    cmd.Parameters.AddWithValue("@User", user);

                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // ***** DELETE APPOINTMENT *****
        public static void DeleteAppointment(int appointmentId)
            {
            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string query = "DELETE FROM appointment WHERE appointmentId=@appointmentId";
                using (var cmd = new MySqlCommand(query, conn))
                    {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                    cmd.ExecuteNonQuery();
                    }
                }
            }

        // ******* GET APPOINTMENTS FOR REPORTS (includes user and customer info) *******
        public static DataTable GetAppointmentsForReports()
            {
            DataTable dt = new DataTable();

            using (var conn = new MySqlConnection(connectionString))
                {
                conn.Open();
                string sql = @"
                SELECT 
                    a.appointmentId,
                    a.userId,
                    u.userName,
                    a.customerID,
                    c.customerName AS CustomerName,
                    a.title AS AppointmentType,
                    a.start,
                    a.end
                FROM appointment a
                JOIN customer c ON a.customerID = c.customerID
                JOIN `user` u ON a.userId = u.userId;
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