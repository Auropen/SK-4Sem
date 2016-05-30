using SKOffice.domain;
using System;
using System.Collections.Generic;
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
                case "browseBlueprintBtn":

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

                    break;
                default:
                    break;
            }

        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            FileTransferClient.Instance.sendFile(paths);
        }
    }
}
