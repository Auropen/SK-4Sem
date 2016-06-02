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
            this.tb_e02 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.feedbackRequisition = new System.Windows.Forms.Label();
            this.feedbackBlueprint = new System.Windows.Forms.Label();
            this.feedbackE02 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browseRequisitionBtn = new System.Windows.Forms.Button();
            this.browseBlueprintBtn = new System.Windows.Forms.Button();
            this.tb_Requisition = new System.Windows.Forms.TextBox();
            this.tb_Blueprints = new System.Windows.Forms.TextBox();
            this.browseE02Btn = new System.Windows.Forms.Button();
            this.uploadBtn = new System.Windows.Forms.Button();
            this.tpBox = new System.Windows.Forms.TabControl();
            this.tpOrderFiles = new System.Windows.Forms.TabPage();
            this.tpOverview = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.groupBox1.SuspendLayout();
            this.tpBox.SuspendLayout();
            this.tpOrderFiles.SuspendLayout();
            this.tpOverview.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.feedbackRequisition);
            this.groupBox1.Controls.Add(this.feedbackBlueprint);
            this.groupBox1.Controls.Add(this.feedbackE02);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.browseRequisitionBtn);
            this.groupBox1.Controls.Add(this.browseBlueprintBtn);
            this.groupBox1.Controls.Add(this.tb_Requisition);
            this.groupBox1.Controls.Add(this.tb_Blueprints);
            this.groupBox1.Controls.Add(this.browseE02Btn);
            this.groupBox1.Controls.Add(this.uploadBtn);
            this.groupBox1.Controls.Add(this.tb_e02);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 226);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload Orderfiles";
            // 
            // feedbackRequisition
            // 
            this.feedbackRequisition.AutoSize = true;
            this.feedbackRequisition.BackColor = System.Drawing.Color.RoyalBlue;
            this.feedbackRequisition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.feedbackRequisition.Location = new System.Drawing.Point(393, 152);
            this.feedbackRequisition.Name = "feedbackRequisition";
            this.feedbackRequisition.Size = new System.Drawing.Size(15, 15);
            this.feedbackRequisition.TabIndex = 12;
            this.feedbackRequisition.Text = "  ";
            // 
            // feedbackBlueprint
            // 
            this.feedbackBlueprint.AutoSize = true;
            this.feedbackBlueprint.BackColor = System.Drawing.Color.RoyalBlue;
            this.feedbackBlueprint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.feedbackBlueprint.Location = new System.Drawing.Point(393, 96);
            this.feedbackBlueprint.Name = "feedbackBlueprint";
            this.feedbackBlueprint.Size = new System.Drawing.Size(15, 15);
            this.feedbackBlueprint.TabIndex = 11;
            this.feedbackBlueprint.Text = "  ";
            // 
            // feedbackE02
            // 
            this.feedbackE02.AutoSize = true;
            this.feedbackE02.BackColor = System.Drawing.Color.RoyalBlue;
            this.feedbackE02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.feedbackE02.Location = new System.Drawing.Point(393, 40);
            this.feedbackE02.Name = "feedbackE02";
            this.feedbackE02.Size = new System.Drawing.Size(15, 15);
            this.feedbackE02.TabIndex = 10;
            this.feedbackE02.Text = "  ";
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
            // browseRequisitionBtn
            // 
            this.browseRequisitionBtn.Location = new System.Drawing.Point(312, 147);
            this.browseRequisitionBtn.Name = "browseRequisitionBtn";
            this.browseRequisitionBtn.Size = new System.Drawing.Size(75, 23);
            this.browseRequisitionBtn.TabIndex = 6;
            this.browseRequisitionBtn.Text = "Browse";
            this.browseRequisitionBtn.UseVisualStyleBackColor = true;
            this.browseRequisitionBtn.Click += new System.EventHandler(this.browse_Click);
            // 
            // browseBlueprintBtn
            // 
            this.browseBlueprintBtn.Location = new System.Drawing.Point(312, 91);
            this.browseBlueprintBtn.Name = "browseBlueprintBtn";
            this.browseBlueprintBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBlueprintBtn.TabIndex = 5;
            this.browseBlueprintBtn.Text = "Browse";
            this.browseBlueprintBtn.UseVisualStyleBackColor = true;
            this.browseBlueprintBtn.Click += new System.EventHandler(this.browse_Click);
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
            // uploadBtn
            // 
            this.uploadBtn.Location = new System.Drawing.Point(359, 197);
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Size = new System.Drawing.Size(75, 23);
            this.uploadBtn.TabIndex = 1;
            this.uploadBtn.Text = "Upload";
            this.uploadBtn.UseVisualStyleBackColor = true;
            this.uploadBtn.Click += new System.EventHandler(this.uploadBtn_Click);
            // 
            // tpBox
            // 
            this.tpBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.groupBox2.Controls.Add(this.vScrollBar1);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Location = new System.Drawing.Point(7, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 228);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Overview";
            // 
            // listView1
            // 
            this.listView1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listView1.Location = new System.Drawing.Point(6, 19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(427, 203);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.vScrollBar1.LargeChange = 100;
            this.vScrollBar1.Location = new System.Drawing.Point(415, 19);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(21, 203);
            this.vScrollBar1.TabIndex = 1;
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
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tb_e02;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button browseE02Btn;
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.TabControl tpBox;
        private System.Windows.Forms.TabPage tpOrderFiles;
        private System.Windows.Forms.TabPage tpOverview;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browseRequisitionBtn;
        private System.Windows.Forms.Button browseBlueprintBtn;
        private System.Windows.Forms.TextBox tb_Requisition;
        private System.Windows.Forms.TextBox tb_Blueprints;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label feedbackE02;
        private System.Windows.Forms.Label feedbackRequisition;
        private System.Windows.Forms.Label feedbackBlueprint;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}

