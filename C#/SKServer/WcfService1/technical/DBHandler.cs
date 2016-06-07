using WcfService.domain.order;
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
            if (prop.keyExists("databaseName"))
                prop.set("databaseName", "localhost");
            if (prop.keyExists("databaseTable"))
                prop.set("databaseTable", "SKDB");
            prop.Save();
            dbName = prop.get("databaseName");
            dbTable = prop.get("databaseTable");
            connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", dbName, dbTable);
        }
        
        // Create Methods
        
        public void createCompany(int compId, String compName, String compAdr, String compFone, String compEmail, int zip)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = String.Format("call ****()");
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

        public void createOrder(OrderConfirmation orderConfirmation)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int companyID = -1;
                    string sql = String.Format("createCompany({0}{1}{2}{3}{4}{5}{6}{7})",
                        orderConfirmation.CompanyInfo[0],
                        orderConfirmation.CompanyInfo[1],
                        orderConfirmation.CompanyInfo[2],
                        orderConfirmation.CompanyInfo[3],
                        orderConfirmation.CompanyInfo[4],
                        orderConfirmation.CompanyInfo[5],
                        orderConfirmation.CompanyInfo[6],
                        orderConfirmation.CompanyInfo[7]);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " Order");
                        companyID = (int) command.Parameters[7].Value;
                    }


                    sql = String.Format("createOrder({0}{1}{2}{3}{4}{5}{6}{7}{8})", 
                        orderConfirmation.DeliveryInfo, 
                        orderConfirmation.AltDeliveryInfo, 
                        orderConfirmation.ProducedDate,
                        orderConfirmation.OrderDate,
                        orderConfirmation.Week,
                        null,
                        null,
                        orderConfirmation.Status,
                        companyID);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " Order");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public List<OrderNote> createNotes(string orderNummer, string content)
        {
            List<OrderNote> result = new List<OrderNote>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = String.Format("createNotes('{0}', '{1}')", orderNummer, content);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " Notes");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }


        public List<OrderCategory> createOrderCategory(string orderNumber, OrderCategory category)
        {
            List<OrderCategory> result = new List<OrderCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = String.Format("createOrderCategory({0}, '{1}', '{2}')", category.ID, orderNumber, category.Name);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                        Console.WriteLine("Added " + command.ExecuteNonQuery() + " OrderCategory");
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        
        public void createOrderElements(OrderElement element, int catagoryId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = String.Format("createOrderCategory('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')", 
                        element.Position, 
                        element.Hinge, 
                        element.Finish, 
                        element.Amount, 
                        element.Unit, "", 
                        element.StationStatus[0], 
                        element.StationStatus[1],
                        element.StationStatus[2],
                        element.StationStatus[3],
                        element.StationStatus[4]);
                    using (SqlCommand command = new SqlCommand(sql, connection))
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

        public OrderConfirmation getOrder(string orderNumber)
        {
            OrderConfirmation orderConfirmation = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = String.Format("call getOrder{0}", orderNumber);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    orderConfirmation = new OrderConfirmation();
                                    orderConfirmation.OrderNumber = dr.GetString(0);        //Order Number
                                    orderConfirmation.ProducedDate = dr.GetDateTime(1);     //Start date/produced date
                                    orderConfirmation.OrderDate = dr.GetDateTime(2);        //Delivery date/order date
                                    orderConfirmation.Week = dr.GetString(3);               //Delivery week
                                    foreach (string line in dr.GetString(4).Split(';'))     //Delivery info
                                        orderConfirmation.DeliveryInfo.Add(line);
                                    foreach (string line in dr.GetString(5).Split(';'))     //Alt delivery info
                                        orderConfirmation.AltDeliveryInfo.Add(line);
                                    orderConfirmation.HousingAssociation = dr.GetString(6); //Housing Association
                                    orderConfirmation.Status = dr.GetString(9);             //Status

                                    //Company info:
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(10));    //Name
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(11));    //Address
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(12));    //ZipCode
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(13));    //Phone
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(14));    //FaxPhone
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(15));    //CVR
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(16));    //Email
                                }
                            }
                        }

                        foreach (OrderCategory category in getOrderCategories(orderNumber))
                        {
                            orderConfirmation.Categories.Add(category);
                        } 

                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return orderConfirmation;
        }


        public List<OrderNote> getNotes(string orderNumber)
        {
            List<OrderNote> result = new List<OrderNote>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = String.Format("getNotes('{0}')", orderNumber);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                result.Add(new OrderNote(dr.GetString(0)));
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
            return result;
        }

        public List<OrderCategory> getOrderCategories(string orderNumber)
        {
            List<OrderCategory> categories = new List<OrderCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = String.Format("getCategories('{0}')", orderNumber);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                OrderCategory category = new OrderCategory(dr.GetInt32(0), dr.GetString(1));
                                categories.Add(category);
                                foreach (OrderElement element in getOrderElements(category.ID))
                                {
                                    category.Elements.Add(element);
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
                    string sql = String.Format("getOrderElements({0})", categoryID);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OrderElement element = new OrderElement(dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                                    element.StationStatus[0] = dr.GetBoolean(6);
                                    element.StationStatus[0] = dr.GetBoolean(7);
                                    element.StationStatus[0] = dr.GetBoolean(8);
                                    element.StationStatus[0] = dr.GetBoolean(9);
                                    element.StationStatus[0] = dr.GetBoolean(10);
                                    elements.Add(element);
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
