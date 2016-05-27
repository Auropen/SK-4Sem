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
            fbd.SelectedPath;

            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "browseE02Btn":

                    ofd.Filter = "Text Files (.e02)|*.e02|";
                    ofd.FilterIndex = 1;

                    ofd.Multiselect = false;
                    
                    tb_e02.Text = fbd.SelectedPath;

                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        folderName = folderBrowserDialog1.SelectedPath;
                        if (!fileOpened)
                        {
                            // No file is opened, bring up openFileDialog in selected path.
                            openFileDialog1.InitialDirectory = folderName;
                            openFileDialog1.FileName = null;
                            openMenuItem.PerformClick();
                        }
                    }

                    break;
                case "browseBlueprintBtn":

                    ofd.Filter = "Text Files (.pdf)|*.pdf|";
                    ofd.FilterIndex = 1;

                    ofd.Multiselect = false;

                    tb_e02.Text = fbd.SelectedPath;

                    break;
                case "browseRequisitionBtn":

                    ofd.Filter = "Text Files (.pdf)|*.pdf|";
                    ofd.FilterIndex = 1;

                    ofd.Multiselect = false;

                    tb_e02.Text = fbd.SelectedPath;
                    break;
                default:
                    break;
            }
            
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

        }
    }
}
