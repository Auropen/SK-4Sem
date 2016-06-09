using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WcfService.domain.order;

namespace SKOffice
{
    public partial class MainForm : Form
    {

        FolderBrowserDialog fbd;
        OpenFileDialog ofd;
        public string[] paths;
        private List<OrderConfirmation> orderConfirmations;
        private int updates;
        private Thread updateThread;
        private int updateFrequency;

        public MainForm()
        {
            updates = 0;
            updateThread = new Thread(new ThreadStart(updateLoop));
            updateFrequency = 30000;
            updateThread.Start();
            paths = new string[3];
            InitializeComponent();

            // Set the view to show details.
            orderOverViewList.View = View.Details;

            // Display grid lines.
            orderOverViewList.GridLines = true;

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            orderOverViewList.Columns.Add("Order ID", 150, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Started", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Done", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Started", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Done", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Started", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Done", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Started", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Done", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Started", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Done", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Note", -2, HorizontalAlignment.Left);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            updateList();
        }

        private void browse_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            ofd = new OpenFileDialog();
        
            //fbd.RootFolder = Environment.SpecialFolder.Desktop;

            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "browseE02Btn":
                    ofd.Filter = "Text Files (.e02)|*.e02";
                    ofd.FilterIndex = 1;
                    ofd.Multiselect = false;
                    ofd.ShowDialog();
                    tb_e02.Text = ofd.FileName;
                    paths[0] = tb_e02.Text;
                    break;
                /*case "browseBlueprintBtn":
                    ofd.Filter = "Text Files (.pdf)|*.pdf";
                    ofd.FilterIndex = 1;
                    ofd.Multiselect = false;
                    ofd.ShowDialog();
                    tb_Blueprints.Text = ofd.FileName;
                    paths.Add(tb_Blueprints.Text);
                    break;
                case "browseRequisitionBtn":
                    ofd.Filter = "Text Files (.pdf)|*.pdf";
                    ofd.FilterIndex = 1;
                    ofd.Multiselect = false;
                    ofd.ShowDialog();
                    tb_Requisition.Text = ofd.FileName;
                    paths.Add(tb_Requisition.Text);
                    break;*/
                default:
                    break;
            }
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            feedbackE02.BackColor = System.Drawing.Color.RoyalBlue; //Feedback: idle
            RestService.RestServiceClient rsClient = new RestService.RestServiceClient();

            //Read the content of the file into a string array
            List<string> fileContent = new List<string>();
            FileInfo fileInfo = new FileInfo(paths[0]);
            fileContent.Add(fileInfo.Name);
            using (StreamReader sReader = new StreamReader(paths[0]))
            {
                while (sReader.Peek() > -1)
                    fileContent.Add(sReader.ReadLine());
            }
            string msg = rsClient.addOrderConfirmation(fileContent.ToArray());
            //Tries to add the order confirmation on the service
            if (msg.StartsWith("OK"))
            {
                feedbackE02.BackColor = System.Drawing.Color.Green; //Feedback: success
                tb_e02.Clear();
                paths[0] = "";
            }
            else
                feedbackE02.BackColor = System.Drawing.Color.Red; //Feedback: failed
            MessageBox.Show(msg , "Service Response");
        }

        private void clickedOrder(object sender, MouseEventArgs e)
        {
            int y = 24;
            for (int i = 0; i < orderOverViewList.Items.Count; i++)
            {
                if (e.Y >= y && e.Y <= y + 16)
                {
                    Console.WriteLine(e.Y + " " + i);
                    OrderForm orderForm = new OrderForm(orderConfirmations[i]);
                    orderForm.Show();
                    break;
                }
                //each row is 16 pixels in height
                //So each element is 0-16, 17-32, 33-48
                y += 17;
            }
        }

        private void updateList()
        {
            FormRestService.ServiceWGetClient rsClient = new FormRestService.ServiceWGetClient();

            orderConfirmations = new List<OrderConfirmation>();
            try
            {
                //Clears the list
                orderOverViewList.Items.Clear();

                //Creates the items in the list
                foreach (FormRestService.OrderConfirmation oc in rsClient.getAllActiveOrders())
                {
                    orderConfirmations.Add((OrderConfirmation) oc);
                    orderOverViewList.Items.Add(new ListViewItem(new[]
                    {oc.OrderNumber + " " + oc.OrderDate.Day + "-" + oc.OrderDate.Month + "-" + oc.OrderDate.Year,
                    (oc.StationStatus.Station4 == "Active" || (oc.StationStatus.Station4 == "Done")? "X": " "),
                    (oc.StationStatus.Station4 == "Done") ? "X" : " ",
                    (oc.StationStatus.Station5 == "Active" || (oc.StationStatus.Station5 == "Done")? "X": " "),
                    (oc.StationStatus.Station5 == "Done") ? "X" : " ",
                    (oc.StationStatus.Station6 == "Active" || (oc.StationStatus.Station6 == "Done")? "X": " "),
                    (oc.StationStatus.Station6 == "Done") ? "X" : " ",
                    (oc.StationStatus.Station7 == "Active" || (oc.StationStatus.Station7 == "Done")? "X": " "),
                    (oc.StationStatus.Station7 == "Done") ? "X" : " ",
                    (oc.StationStatus.Station8 == "Active" || (oc.StationStatus.Station8 == "Done")? "X": " "),
                    (oc.StationStatus.Station8 == "Done") ? "X" : " ",
                    oc.Notes.Length + "" }));
                }
                //Resizes the last column to match the window.
                int lastIndex = orderOverViewList.Columns.Count - 1;
                orderOverViewList.Columns[lastIndex].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private bool checkServiceUpdates()
        {
            FormRestService.ServiceWGetClient rsClient = new FormRestService.ServiceWGetClient();
            int serviceUpdates = rsClient.getUpdates();
            bool result = !(serviceUpdates == updates);
            updates = serviceUpdates;
            return result;
        }

        private void updateLoop()
        {
            while (true)
            {
                if (checkServiceUpdates())
                    updateList();
                Thread.Sleep(updateFrequency);
            }
        }
    }
}
