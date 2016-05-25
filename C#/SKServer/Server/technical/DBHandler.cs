using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.technical
{
    class DBHandler
    {

        private static DBHandler instance;
        private string dbName;


        public static void getProperties(string propertyFile)
        {
            domain.Properties prop = new domain.Properties(propertyFile);

            prop.get("databaseName");
        }

        private DBHandler() { }

        public static DBHandler Instance
        {
            get
            {
                if (instance == null) instance = new DBHandler();
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        static void createCustomer(int custId, String Fname, String LName, String fone, String email, String custAdr, int zip)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=PIRATEBOAT-LT;Initial Catalog=SKDB;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TblCustomer VALUES(" + custId + ", '" + Fname + "', '" + LName + "', '" + fone + "', '" + email + "', '" + custAdr + "', " + zip + ")", connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " customers");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }



        static String getCustomer()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=PIRATEBOAT-LT;Initial Catalog=SKDB;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * FROM TblCustomer", connection))
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                //Console.WriteLine(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6));
                                Console.WriteLine(dr.GetInt32(0) + " " + dr.GetString(1) + " " + dr.GetString(2) + " " + dr.GetString(3) + " " + dr.GetString(4) + " " + dr.GetString(5) + " " + dr.GetInt32(6));
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return "";
        }


        static void createCompany(int compId, String compName, String compAdr, String compFone, String compEmail, int zip)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=PIRATEBOAT-LT;Initial Catalog=SKDB;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TblCompany VALUES(" + compId + ", '" + compName + "','" + compAdr + "', '" + compFone + "', '" + compEmail + "', " + zip + ")", connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " company");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static void createOrder(int orderId, String altDelivery, String startDate, String deliveryDate, int compId, int custId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=PIRATEBOAT-LT;Initial Catalog=SKDB;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TblOrder VALUES(" + orderId + ", '" + altDelivery + "', '" + startDate + "', '" + deliveryDate + "', " + compId + ", " + custId + ")", connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " Order");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        static void createNotes(String commentContent, int orderId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=PIRATEBOAT-LT;Initial Catalog=SKDB;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TblNotes VALUES('" + commentContent + "', " + orderId + ")", connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " Notes");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        static void createOrderCategory(String categoryName, int orderId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=PIRATEBOAT-LT;Initial Catalog=SKDB;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TblOrderCategory VALUES('" + categoryName + "', " + orderId + ")", connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " OrderCategory");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        static void createOrderElements(String posId, String hinge, String finish, String amount, String unit, String text, int catagoryId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=PIRATEBOAT-LT;Initial Catalog=SKDB;Integrated Security=True"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TblOrderCategory VALUES('" + posId + "','" + hinge + "','" + finish + "','" + amount + "', '" + unit + "', '" + text + "', " + catagoryId + ")", connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " OrderCategory");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }



    }
}
