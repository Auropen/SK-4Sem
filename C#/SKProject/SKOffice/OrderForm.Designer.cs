namespace SKOffice
{
    partial class OrderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blueprintBtn = new System.Windows.Forms.Button();
            this.requisitionBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.feedbackBp = new System.Windows.Forms.Label();
            this.feedbackReq = new System.Windows.Forms.Label();
            this.noteTxt = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.noteTxt);
            this.groupBox1.Controls.Add(this.feedbackReq);
            this.groupBox1.Controls.Add(this.feedbackBp);
            this.groupBox1.Controls.Add(this.closeBtn);
            this.groupBox1.Controls.Add(this.requisitionBtn);
            this.groupBox1.Controls.Add(this.blueprintBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 507);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Order Menu";
            // 
            // blueprintBtn
            // 
            this.blueprintBtn.Location = new System.Drawing.Point(7, 20);
            this.blueprintBtn.Name = "blueprintBtn";
            this.blueprintBtn.Size = new System.Drawing.Size(132, 23);
            this.blueprintBtn.TabIndex = 0;
            this.blueprintBtn.Text = "Blueprint";
            this.blueprintBtn.UseVisualStyleBackColor = true;
            // 
            // requisitionBtn
            // 
            this.requisitionBtn.Location = new System.Drawing.Point(7, 49);
            this.requisitionBtn.Name = "requisitionBtn";
            this.requisitionBtn.Size = new System.Drawing.Size(132, 23);
            this.requisitionBtn.TabIndex = 1;
            this.requisitionBtn.Text = "Requisition";
            this.requisitionBtn.UseVisualStyleBackColor = true;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(221, 20);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(132, 23);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // feedbackBp
            // 
            this.feedbackBp.AutoSize = true;
            this.feedbackBp.BackColor = System.Drawing.Color.RoyalBlue;
            this.feedbackBp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.feedbackBp.Location = new System.Drawing.Point(145, 25);
            this.feedbackBp.Name = "feedbackBp";
            this.feedbackBp.Size = new System.Drawing.Size(15, 15);
            this.feedbackBp.TabIndex = 11;
            this.feedbackBp.Text = "  ";
            // 
            // feedbackReq
            // 
            this.feedbackReq.AutoSize = true;
            this.feedbackReq.BackColor = System.Drawing.Color.RoyalBlue;
            this.feedbackReq.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.feedbackReq.Location = new System.Drawing.Point(145, 54);
            this.feedbackReq.Name = "feedbackReq";
            this.feedbackReq.Size = new System.Drawing.Size(15, 15);
            this.feedbackReq.TabIndex = 12;
            this.feedbackReq.Text = "  ";
            // 
            // noteTxt
            // 
            this.noteTxt.Location = new System.Drawing.Point(6, 78);
            this.noteTxt.Name = "noteTxt";
            this.noteTxt.ReadOnly = true;
            this.noteTxt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.noteTxt.Size = new System.Drawing.Size(347, 423);
            this.noteTxt.TabIndex = 13;
            this.noteTxt.Text = "";
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 531);
            this.Controls.Add(this.groupBox1);
            this.Name = "OrderForm";
            this.Text = "Order - ";
            this.Load += new System.EventHandler(this.OrderForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button requisitionBtn;
        private System.Windows.Forms.Button blueprintBtn;
        private System.Windows.Forms.Label feedbackReq;
        private System.Windows.Forms.Label feedbackBp;
        private System.Windows.Forms.RichTextBox noteTxt;
    }
}