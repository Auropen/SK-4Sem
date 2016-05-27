using System;
using System.Windows.Forms;

namespace SKOffice
{
    public partial class MainForm : Form
    {

        FolderBrowserDialog fbd;
        OpenFileDialog ofd;

        public MainForm()
        {
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
        
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "browseE02Btn":

                    ofd.Filter = "Text Files (.e02)|*.e02";
                    ofd.FilterIndex = 1;

                    ofd.Multiselect = false;

                    ofd.ShowDialog();

                    tb_e02.Text = ofd.FileName;

                    break;
                case "browseBlueprintBtn":

                    ofd.Filter = "Text Files (.pdf)|*.pdf";
                    ofd.FilterIndex = 1;

                    ofd.Multiselect = false;

                    ofd.ShowDialog();

                    tb_Blueprints.Text = ofd.FileName;

                    break;
                case "browseRequisitionBtn":

                    ofd.Filter = "Text Files (.pdf)|*.pdf";
                    ofd.FilterIndex = 1;

                    ofd.Multiselect = false;

                    ofd.ShowDialog();

                    tb_Requisition.Text = ofd.FileName;
                    break;
                default:
                    break;
            }
            
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

        }
    }
}
