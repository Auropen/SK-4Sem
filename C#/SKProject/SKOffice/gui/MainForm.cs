using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SKOffice
{
    public partial class MainForm : Form
    {

        FolderBrowserDialog fbd;
        OpenFileDialog ofd;
        public List<string> paths;


        public MainForm()
        {
            paths = new List<string>();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            ServiceReference1.ServiceWGetClient rsClient = new ServiceReference1.ServiceWGetClient();

            // Set the view to show details.
            orderOverViewList.View = View.Details;

            // Display grid lines.
            orderOverViewList.GridLines = true;

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            orderOverViewList.Columns.Add("Order ID", 150, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("i gang", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Færdig", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("i gang", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Færdig", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("i gang", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Færdig", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("i gang", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Færdig", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("i gang", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Færdig", 50, HorizontalAlignment.Center);
            orderOverViewList.Columns.Add("Note", -2, HorizontalAlignment.Left);
            
            try
            {
                foreach (ServiceReference1.OrderConfirmation oc in rsClient.getAllActiveOrders())
                {
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
                    "Note" }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
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

                    paths.Add(tb_e02.Text);

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

            //Tries to add the order confirmation on the service
            if (rsClient.addOrderConfirmation(fileContent.ToArray()))
            {
                Console.WriteLine("Order Confirmation was successful added to the service.");
                feedbackE02.BackColor = System.Drawing.Color.Green; //Feedback: success
            }
            else
            {
                Console.WriteLine("Order Confirmation was not added.");
                feedbackE02.BackColor = System.Drawing.Color.Red; //Feedback: failed
            }
        }

        private void orderOverViewList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
