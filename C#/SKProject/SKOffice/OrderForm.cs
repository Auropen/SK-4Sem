using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfService.domain.order;

namespace SKOffice
{
    public partial class OrderForm : Form
    {
        public OrderConfirmation OrderConfirmation { get; private set; }
        public string OrderName { get; private set; }

        //Renames the window with the OrderNumber
        public OrderForm(OrderConfirmation orderConfirmation) 
        {
            this.OrderConfirmation = orderConfirmation;
            InitializeComponent();
            Text = "Order - " + orderConfirmation.OrderNumber;
        }
        
        //Makes a status check if there is a link for the Blueprint and Requisition button
        private void OrderForm_Load(object sender, EventArgs e)
        {
            feedbackBp.BackColor = Color.Red;
            feedbackReq.BackColor = Color.Red;
            Console.WriteLine("NOTES: " + OrderConfirmation.Notes.Count);
            foreach (OrderNote note in OrderConfirmation.Notes)
            {
                addNote(note.Text);
            }
        }

        //Closes the window when you press the button "Close"
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Fills the text area with the note text. 
        private void addNote(string txt)
        {
            Console.WriteLine("Adding note: " + txt);
            noteTxt.Text += "-------Start Note------\n";
            noteTxt.Text += txt + "\n";
            noteTxt.Text += "---------End Note------\n";
        }
    }
}
