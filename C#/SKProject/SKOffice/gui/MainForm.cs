using SKOffice.domain;
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

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
                    
            RestService.RestServiceClient rsClient = new RestService.RestServiceClient();

            //listView1.Items.AddRange(rsClient.getOrder);

    }
    }
}
