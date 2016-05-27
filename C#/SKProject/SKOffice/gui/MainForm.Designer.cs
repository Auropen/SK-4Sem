namespace SKOffice
{
    partial class MainForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tb_e02 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tb_Requisition = new System.Windows.Forms.TextBox();
            this.tb_Blueprints = new System.Windows.Forms.TextBox();
            this.browseE02Btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tpBox = new System.Windows.Forms.TabControl();
            this.tpOrderFiles = new System.Windows.Forms.TabPage();
            this.tpOverview = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.tpBox.SuspendLayout();
            this.tpOrderFiles.SuspendLayout();
            this.tpOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tb_e02
            // 
            this.tb_e02.Location = new System.Drawing.Point(7, 38);
            this.tb_e02.Name = "tb_e02";
            this.tb_e02.Size = new System.Drawing.Size(300, 20);
            this.tb_e02.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.tb_Requisition);
            this.groupBox1.Controls.Add(this.tb_Blueprints);
            this.groupBox1.Controls.Add(this.browseE02Btn);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tb_e02);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 226);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload Orderfiles";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Requisition (*.pdf)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Blueprints (*.pdf)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Winner Tool file (*.e02)";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(312, 147);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.browse_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(312, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.browse_Click);
            // 
            // tb_Requisition
            // 
            this.tb_Requisition.Location = new System.Drawing.Point(7, 149);
            this.tb_Requisition.Name = "tb_Requisition";
            this.tb_Requisition.Size = new System.Drawing.Size(300, 20);
            this.tb_Requisition.TabIndex = 4;
            // 
            // tb_Blueprints
            // 
            this.tb_Blueprints.Location = new System.Drawing.Point(7, 93);
            this.tb_Blueprints.Name = "tb_Blueprints";
            this.tb_Blueprints.Size = new System.Drawing.Size(300, 20);
            this.tb_Blueprints.TabIndex = 3;
            // 
            // browseE02Btn
            // 
            this.browseE02Btn.Location = new System.Drawing.Point(312, 35);
            this.browseE02Btn.Name = "browseE02Btn";
            this.browseE02Btn.Size = new System.Drawing.Size(75, 23);
            this.browseE02Btn.TabIndex = 2;
            this.browseE02Btn.Text = "Browse";
            this.browseE02Btn.UseVisualStyleBackColor = true;
            this.browseE02Btn.Click += new System.EventHandler(this.browse_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tpBox
            // 
            this.tpBox.Controls.Add(this.tpOrderFiles);
            this.tpBox.Controls.Add(this.tpOverview);
            this.tpBox.Location = new System.Drawing.Point(12, 12);
            this.tpBox.Name = "tpBox";
            this.tpBox.SelectedIndex = 0;
            this.tpBox.Size = new System.Drawing.Size(460, 264);
            this.tpBox.TabIndex = 3;
            // 
            // tpOrderFiles
            // 
            this.tpOrderFiles.Controls.Add(this.groupBox1);
            this.tpOrderFiles.Location = new System.Drawing.Point(4, 22);
            this.tpOrderFiles.Name = "tpOrderFiles";
            this.tpOrderFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tpOrderFiles.Size = new System.Drawing.Size(452, 238);
            this.tpOrderFiles.TabIndex = 0;
            this.tpOrderFiles.Text = "Upload Orderfiles";
            this.tpOrderFiles.UseVisualStyleBackColor = true;
            this.tpOrderFiles.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tpOverview
            // 
            this.tpOverview.Controls.Add(this.groupBox2);
            this.tpOverview.Location = new System.Drawing.Point(4, 22);
            this.tpOverview.Name = "tpOverview";
            this.tpOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tpOverview.Size = new System.Drawing.Size(452, 238);
            this.tpOverview.TabIndex = 1;
            this.tpOverview.Text = "Overview";
            this.tpOverview.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(7, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(442, 228);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 288);
            this.Controls.Add(this.tpBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tpBox.ResumeLayout(false);
            this.tpOrderFiles.ResumeLayout(false);
            this.tpOverview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tb_e02;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button browseE02Btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tpBox;
        private System.Windows.Forms.TabPage tpOrderFiles;
        private System.Windows.Forms.TabPage tpOverview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_Requisition;
        private System.Windows.Forms.TextBox tb_Blueprints;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

