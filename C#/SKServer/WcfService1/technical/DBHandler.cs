using WcfService.domain.order;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

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
            prop.reload();
            if (!prop.keyExists("databaseName"))
                prop.set("databaseName", "localhost");
            if (!prop.keyExists("databaseTable"))
                prop.set("databaseTable", "SKDB");
            prop.Save();
            dbName = prop.get("databaseName");
            dbTable = prop.get("databaseTable");
            connectionString = String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True", dbName, dbTable);
        }
        
        // Create Methods

        public void createOrder(OrderConfirmation orderConfirmation)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("createOrder", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        //Parsing arrays of strings to a complete string
                        string di = "";
                        foreach (string str in orderConfirmation.DeliveryInfo)
                            di += str + ";";
                        di.Substring(0, di.Length - 1);

                        string adi = "";

                        foreach (string str in orderConfirmation.AltDeliveryInfo)
                            adi += str + ";";
                        adi.Substring(0, adi.Length - 1);

                        command.Parameters.Add(new SqlParameter("@OrderNumber", orderConfirmation.OrderNumber));
                        command.Parameters.Add(new SqlParameter("@OrderName", orderConfirmation.OrderName));
                        command.Parameters.Add(new SqlParameter("@Delivery", di));
                        command.Parameters.Add(new SqlParameter("@AltDelivery", adi));
                        command.Parameters.Add(new SqlParameter("@HousingAssociation", orderConfirmation.HousingAssociation));
                        command.Parameters.Add(new SqlParameter("@StartDate", orderConfirmation.ProducedDate.ToString("yyyy-MM-dd")));
                        command.Parameters.Add(new SqlParameter("@DeliveryDate", orderConfirmation.OrderDate.ToString("yyyy-MM-dd")));
                        command.Parameters.Add(new SqlParameter("@DeliveryWeek", orderConfirmation.Week));
                        command.Parameters.Add(new SqlParameter("@BluePrintLink", ""));
                        command.Parameters.Add(new SqlParameter("@RequisitionLink", ""));
                        command.Parameters.Add(new SqlParameter("@ProgressStatus", orderConfirmation.Status));
                        command.Parameters.Add(new SqlParameter("@CompanyID", 1));
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
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

                    string info = "";
                    foreach (string str in element.ElementInfo)
                    {
                        info += str + ";";
                    }

                    string sql = String.Format("createOrderCategory('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')", 
                        element.Position, 
                        element.Hinge, 
                        element.Finish, 
                        element.Amount, 
                        element.Unit, 
                        info, 
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
                    string sql = String.Format("getOrder('{0}')", orderNumber);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    orderConfirmation = new OrderConfirmation();
                                    orderConfirmation.OrderNumber = dr.GetString(0);        //Order Number
                                    orderConfirmation.OrderName = dr.GetString(1);          //Order Name

                                    string[] splitName = orderConfirmation.OrderName.Split('/');
                                    orderConfirmation.AlternativeNumber =                   //Alternative Number
                                        splitName[0] + " " +
                                        orderConfirmation.OrderNumber + " " +
                                        splitName[1];

                                    orderConfirmation.ProducedDate = dr.GetDateTime(2);     //Start date/produced date
                                    orderConfirmation.OrderDate = dr.GetDateTime(3);        //Delivery date/order date
                                    orderConfirmation.Week = dr.GetString(4);               //Delivery week
                                    foreach (string line in dr.GetString(5).Split(';'))     //Delivery info
                                        orderConfirmation.DeliveryInfo.Add(line);
                                    foreach (string line in dr.GetString(6).Split(';'))     //Alt delivery info
                                        orderConfirmation.AltDeliveryInfo.Add(line);
                                    orderConfirmation.HousingAssociation = dr.GetString(7); //Housing Association
                                    orderConfirmation.Status = dr.GetString(10);             //Status

                                    //Company info:
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(11));    //Name
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(12));    //Address
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(13));    //ZipCode
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(14));    //Phone
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(15));    //FaxPhone
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(16));    //CVR
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(17));    //Email
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
                                    OrderElement element = new OrderElement(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
                                    foreach (string str in dr.GetString(5).Split(';'))
                                    {
                                        element.ElementInfo.Add(str);
                                    }
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
