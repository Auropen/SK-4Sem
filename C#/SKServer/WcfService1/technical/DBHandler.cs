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

        /// <summary>
        /// Construcks a Singleton
        /// </summary>
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

        /// <summary>
        /// Reads the Properties File for the Connection information 
        /// </summary>
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

        /// <summary>
        /// Recieves the orderConfirmation Object and takes the information to Create and Order
        /// </summary>
        /// <param name="orderConfirmation"></param>
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

                        command.Parameters.Add(new SqlParameter("@OrderNumber", SqlDbType.VarChar, 64));
                        command.Parameters["@OrderNumber"].Value = orderConfirmation.OrderNumber;
                        command.Parameters.Add(new SqlParameter("@OrderName", SqlDbType.VarChar, 64));
                        command.Parameters["@OrderName"].Value = orderConfirmation.OrderName;
                        command.Parameters.Add(new SqlParameter("@Delivery", SqlDbType.VarChar, 256));
                        command.Parameters["@Delivery"].Value = di;
                        command.Parameters.Add(new SqlParameter("@AltDelivery", SqlDbType.VarChar, 256));
                        command.Parameters["@AltDelivery"].Value = adi;
                        command.Parameters.Add(new SqlParameter("@HousingAssociation", SqlDbType.VarChar, 64));
                        command.Parameters["@HousingAssociation"].Value = orderConfirmation.HousingAssociation;
                        command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date));
                        command.Parameters["@StartDate"].Value = orderConfirmation.ProducedDate.ToString("yyyy-MM-dd");
                        command.Parameters.Add(new SqlParameter("@DeliveryDate", SqlDbType.Date));
                        command.Parameters["@DeliveryDate"].Value = orderConfirmation.OrderDate.ToString("yyyy-MM-dd");
                        command.Parameters.Add(new SqlParameter("@DeliveryWeek", SqlDbType.VarChar, 32));
                        command.Parameters["@DeliveryWeek"].Value = orderConfirmation.Week;
                        command.Parameters.Add(new SqlParameter("@BluePrintLink", SqlDbType.VarChar, 256));
                        command.Parameters["@BluePrintLink"].Value = "";
                        command.Parameters.Add(new SqlParameter("@RequisitionLink", SqlDbType.VarChar, 256));
                        command.Parameters["@RequisitionLink"].Value = "";
                        command.Parameters.Add(new SqlParameter("@ProgressStatus", SqlDbType.VarChar, 16));
                        command.Parameters["@ProgressStatus"].Value = orderConfirmation.Status;
                        command.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int));
                        command.Parameters["@CompanyID"].Value = 1;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();

                    foreach (OrderCategory category in orderConfirmation.Categories)
                    {
                        createOrderCategory(orderConfirmation.OrderNumber, category);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }

        }

        /// <summary>
        /// Creates notes and saves it to the database
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public List<OrderNote> createNotes(string orderNumber, OrderNote note)
        {
            List<OrderNote> result = new List<OrderNote>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("createNotes", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@OrderNumber", SqlDbType.VarChar, 64));
                        command.Parameters["@OrderNumber"].Value = orderNumber;
                        command.Parameters.Add(new SqlParameter("@Content", SqlDbType.VarChar, 1024));
                        command.Parameters["@Content"].Value = note.Text;
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Creates Categories saves themn to the Database
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<OrderCategory> createOrderCategory(string orderNumber, OrderCategory category)
        {
            List<OrderCategory> result = new List<OrderCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int categoryID = -1;
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("createOrderCategory", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@OrderNumber", SqlDbType.VarChar, 64));
                        command.Parameters["@OrderNumber"].Value = orderNumber;
                        command.Parameters.Add(new SqlParameter("@CategoryName", SqlDbType.VarChar, 64));
                        command.Parameters["@CategoryName"].Value = category.Name;
                        //command.Parameters.Add(new SqlParameter("@new_id", SqlDbType.Int, 0, "fldCategoryID").Direction = ParameterDirection.Output);
                        command.ExecuteNonQuery();

                        //categoryID = Convert.ToInt32(command.Parameters["@new_id"].Value);
                    }
                    connection.Close();

                    //Gets the last ID created, in this case the id for this Category
                    categoryID = getLastID("dbo.TblOrderCategory");

                    //Creates all the elements for the category
                    foreach (OrderElement element in category.Elements)
                    {
                        createOrderElements(element, categoryID);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        
        /// <summary>
        /// Creates OrderElements and stores them with the orderCategory ID
        /// </summary>
        /// <param name="element"></param>
        /// <param name="categoryId"></param>
        public void createOrderElements(OrderElement element, int categoryId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("createOrderElements", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@Pos", SqlDbType.VarChar, 32));
                        command.Parameters["@Pos"].Value = element.Position;
                        command.Parameters.Add(new SqlParameter("@Hinge", SqlDbType.VarChar, 64));
                        command.Parameters["@Hinge"].Value = element.Hinge;
                        command.Parameters.Add(new SqlParameter("@Finish", SqlDbType.VarChar, 64));
                        command.Parameters["@Finish"].Value = element.Finish;
                        command.Parameters.Add(new SqlParameter("@Amount", SqlDbType.VarChar, 32));
                        command.Parameters["@Amount"].Value = element.Amount;
                        command.Parameters.Add(new SqlParameter("@Unit", SqlDbType.VarChar, 32));
                        command.Parameters["@Unit"].Value = element.Unit;

                        string info = "";
                        foreach (string str in element.ElementInfo)
                        {
                            info += str + ";";
                        }

                        command.Parameters.Add(new SqlParameter("@Text", SqlDbType.VarChar, 256));
                        command.Parameters["@Text"].Value = info;
                        command.Parameters.Add(new SqlParameter("@Station4Status", SqlDbType.Bit));
                        command.Parameters["@Station4Status"].Value = element.StationStatus[0];
                        command.Parameters.Add(new SqlParameter("@Station5Status", SqlDbType.Bit));
                        command.Parameters["@Station5Status"].Value = element.StationStatus[1];
                        command.Parameters.Add(new SqlParameter("@Station6Status", SqlDbType.Bit));
                        command.Parameters["@Station6Status"].Value = element.StationStatus[2];
                        command.Parameters.Add(new SqlParameter("@Station7Status", SqlDbType.Bit));
                        command.Parameters["@Station7Status"].Value = element.StationStatus[3];
                        command.Parameters.Add(new SqlParameter("@Station8Status", SqlDbType.Bit));
                        command.Parameters["@Station8Status"].Value = element.StationStatus[4];
                        command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int));
                        command.Parameters["@CategoryID"].Value = categoryId;

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Get methods

        /// <summary>
        /// Get an order from the Database by OrderNumber with the Categorylist containing the Elementlist by
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public OrderConfirmation getOrder(string orderNumber)
        {
            OrderConfirmation orderConfirmation = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getOrder", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@OrderNumber", SqlDbType.VarChar, 64));
                        command.Parameters["@OrderNumber"].Value = orderNumber;

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
                    }
                    connection.Close();

                    //Adds all the categories linked to this order confirmation
                    foreach (OrderCategory category in getOrderCategories(orderNumber))
                    {
                        orderConfirmation.Categories.Add(category);
                    }
                    foreach (OrderNote note in getNotes(orderNumber))
                    {
                        orderConfirmation.Notes.Add(note);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return orderConfirmation;
        }

        /// <summary>
        /// Get a list of Orders that are active containing a list of Categories containign a list of Elements
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<OrderConfirmation> getAllOrdersOfStatus(string status)
        {
            List<OrderConfirmation> result = new List<OrderConfirmation>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getAllOrdersOfStatus", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@ProgressStatus", SqlDbType.VarChar, 64));
                        command.Parameters["@ProgressStatus"].Value = status;

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OrderConfirmation orderConfirmation = new OrderConfirmation();
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
                                    orderConfirmation.CompanyInfo.Add(dr.GetInt32(13) + "");    //ZipCode
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(14));    //Phone
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(15));    //FaxPhone
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(16));    //CVR
                                    orderConfirmation.CompanyInfo.Add(dr.GetString(17));    //Email
                                    result.Add(orderConfirmation);
                                }
                            }
                        }
                        connection.Close();
                        //Get all categories for all orders
                        foreach (OrderConfirmation orderConfirmation in result)
                        {
                            foreach (OrderCategory category in getOrderCategories(orderConfirmation.OrderNumber))
                            {
                                orderConfirmation.Categories.Add(category);
                            }
                            foreach (OrderNote note in getNotes(orderConfirmation.OrderNumber))
                            {
                                orderConfirmation.Notes.Add(note);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// gets a list of Notes by orderNumber
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public List<OrderNote> getNotes(string orderNumber)
        {
            List<OrderNote> result = new List<OrderNote>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getNotes", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@OrderNumber", SqlDbType.VarChar, 64));
                        command.Parameters["@OrderNumber"].Value = orderNumber;

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    result.Add(new OrderNote(dr.GetString(2)));
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
            return result;
        }
        
        /// <summary>
        /// Creates a list of Categories by orderNumber
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        public List<OrderCategory> getOrderCategories(string orderNumber)
        {
            List<OrderCategory> result = new List<OrderCategory>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getCategories", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@OrderNumber", SqlDbType.VarChar, 64));
                        command.Parameters["@OrderNumber"].Value = orderNumber;

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OrderCategory category = new OrderCategory(dr.GetString(1));
                                    result.Add(category);
                                }
                            }
                        }
                    }
                    connection.Close();

                    for (int i = 0; i < result.Count; i++)
                    {
                        foreach (OrderElement element in getOrderElements(i+1))
                        {
                            result[i].Elements.Add(element);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return result;
        }

        /// <summary>
        /// Creates a list of Elements by category ID from the Database
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public List<OrderElement> getOrderElements(int categoryID)
        {
            List<OrderElement> elements = new List<OrderElement>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("getOrderElements", connection) { CommandType = CommandType.StoredProcedure })
                    {
                        command.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.VarChar, 64));
                        command.Parameters["@CategoryID"].Value = categoryID;

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    OrderElement element = new OrderElement(dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));  //(dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
                                    foreach (string str in dr.GetString(6).Split(';'))
                                    {
                                        element.ElementInfo.Add(str);
                                    }
                                    element.StationStatus[0] = dr.GetBoolean(7);
                                    element.StationStatus[0] = dr.GetBoolean(8);
                                    element.StationStatus[0] = dr.GetBoolean(9);
                                    element.StationStatus[0] = dr.GetBoolean(10);
                                    element.StationStatus[0] = dr.GetBoolean(11);
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

        /// <summary>
        /// Retries the last ID from a given table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public int getLastID(string tableName)
        {
            int id = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(String.Format("SELECT IDENT_CURRENT('{0}') AS ID", tableName), connection))
                        id = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }
    }
}
