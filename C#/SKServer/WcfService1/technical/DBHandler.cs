﻿using WcfService.domain.order;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WcfService.technical
{
    class DBHandler
    {

        private static DBHandler instance;
        private string dbName;
        private string dbTable;
        private string connectionString;

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

        private DBHandler() {
            domain.Properties prop = new domain.Properties("database");

            dbName = prop.get("databaseName");
            dbTable = prop.get("databaseTable");
            connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", dbName, dbTable);
        }
        
        // Create Methods

        public void createCustomer(int custId, String Fname, String LName, String fone, String email, String custAdr, int zip)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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

        public void createCompany(int compId, String compName, String compAdr, String compFone, String compEmail, int zip)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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

        public void createOrder(int orderId, String altDelivery, String startDate, String deliveryDate, String deliveryWeek, String bluePrinkLink, int compId, int custId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO TblOrder VALUES(" + orderId + ", '" + altDelivery + "', '" + startDate + "', '" + deliveryDate + "', '" + deliveryWeek + "','" + bluePrinkLink + "'," + compId + ", " + custId + ")", connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " Order");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public void createNotes(String commentContent, int orderId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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


        public void createOrderCategory(String categoryName, int orderId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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


        public void createOrderElements(String posId, String hinge, String finish, String amount, String unit, String text, int catagoryId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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

        // Get methods

        public String getZipAndTown()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * FROM TblZipCodes", connection))
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                //Console.WriteLine(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6));
                                Console.WriteLine(dr.GetInt32(0) + " "
                                               + dr.GetString(1) + " This is Town & Zip ");
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


        public String getCustomer()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                                Console.WriteLine(dr.GetInt32(0) + " "
                                               + dr.GetString(1) + " "
                                               + dr.GetString(2) + " "
                                               + dr.GetString(3) + " "
                                               + dr.GetString(4) + " "
                                               + dr.GetInt32(5) + " This is Customer ");
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


        public String getCompany()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * FROM TblCompany", connection))
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                //Console.WriteLine(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6));
                                Console.WriteLine(dr.GetInt32(0)  + " " 
                                                + dr.GetString(1) + " " 
                                                + dr.GetString(2) + " " 
                                                + dr.GetString(3) + " " 
                                                + dr.GetString(4) + " " 
                                                + dr.GetInt32(5) + " This is Company ");
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


        public String getOrder(string orderNumber)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = String.Format("Select * FROM tblOrder WHERE OrderNumber = {0}", orderNumber);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    //Console.WriteLine(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6));
                                    Console.WriteLine(dr.GetString(0) + " "
                                                    + dr.GetString(1) + " "
                                                    + dr.GetString(2) + " "
                                                    + dr.GetString(3) + " "
                                                    + dr.GetString(4) + " "
                                                    + dr.GetString(5) + " "
                                                    + dr.GetString(6) + " "
                                                    + dr.GetInt32(7) + " This is Element ");
                                    OrderConfirmation orderConfirmation = new OrderConfirmation();
                                    orderConfirmation.OrderNumber = dr.GetString(0);
                                    orderConfirmation.ProducedDate = dr.GetDateTime(1);
                                }
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


        public String getNotes()
        {
            //Needs a List for the various Notes? 
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * FROM TblNotes", connection))
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                //Console.WriteLine(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6));
                                Console.WriteLine(dr.GetInt32(0) + " "
                                                + dr.GetString(1) + " "
                                                + dr.GetInt32(2) + " This is Note ");
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


        public List<OrderCategory> getOrderCategory()
        {
            List<OrderCategory> categories = new List<OrderCategory>();
            //Needs a List for the various Categories
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("Select * FROM TblOrderCategory", connection))
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                categories.Add(new OrderCategory(dr.GetString(1), dr.GetInt32(0)));
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
            return categories;
        }


        public List<OrderElement> getOrderElements(int categoryID)
        {

            List<OrderElement> elements = new List<OrderElement>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = String.Format("Select * FROM TblOrder WHERE CategoryID = {0}", categoryID);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    //Console.WriteLine(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5), dr.GetInt32(6));
                                    Console.WriteLine(dr.GetInt32(0) + " "
                                                    + dr.GetString(1) + " "
                                                    + dr.GetString(2) + " "
                                                    + dr.GetString(3) + " "
                                                    + dr.GetString(4) + " "
                                                    + dr.GetString(5) + " "
                                                    + dr.GetString(6) + " "
                                                    + dr.GetInt32(7) + " This is Element ");

                                }
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
            return elements;
        }

    }
}