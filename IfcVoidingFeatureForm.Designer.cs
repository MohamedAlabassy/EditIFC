namespace EditIFC
{
    partial class IfcVoidingFeatureForm
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.voidingFeatureTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cUTOUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nOTCHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hOLEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mITERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cHAMFERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eDGEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSERDEFINEDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uNDEFINEDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(646, 108);
            this.listBox1.TabIndex = 7;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(563, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Open an IFC file and select the cutting IFC Element from the list below";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(358, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Select the IFC Element to apply the IfcVoidingFeature to from the list below";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(12, 185);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(646, 173);
            this.listBox2.TabIndex = 11;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(200, 365);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Add Voiding Feature";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.voidingFeatureTypeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(5, 364);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(256, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // voidingFeatureTypeToolStripMenuItem
            // 
            this.voidingFeatureTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cUTOUTToolStripMenuItem,
            this.nOTCHToolStripMenuItem,
            this.hOLEToolStripMenuItem,
            this.mITERToolStripMenuItem,
            this.cHAMFERToolStripMenuItem,
            this.eDGEToolStripMenuItem,
            this.uSERDEFINEDToolStripMenuItem,
            this.uNDEFINEDToolStripMenuItem});
            this.voidingFeatureTypeToolStripMenuItem.Name = "voidingFeatureTypeToolStripMenuItem";
            this.voidingFeatureTypeToolStripMenuItem.Size = new System.Drawing.Size(128, 20);
            this.voidingFeatureTypeToolStripMenuItem.Text = "Voiding Feature Type";
            this.voidingFeatureTypeToolStripMenuItem.Click += new System.EventHandler(this.voidingFeatureTypeToolStripMenuItem_Click);
            // 
            // cUTOUTToolStripMenuItem
            // 
            this.cUTOUTToolStripMenuItem.Name = "cUTOUTToolStripMenuItem";
            this.cUTOUTToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cUTOUTToolStripMenuItem.Text = "Cutout";
            this.cUTOUTToolStripMenuItem.Click += new System.EventHandler(this.cUTOUTToolStripMenuItem_Click);
            // 
            // nOTCHToolStripMenuItem
            // 
            this.nOTCHToolStripMenuItem.Name = "nOTCHToolStripMenuItem";
            this.nOTCHToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nOTCHToolStripMenuItem.Text = "Notch";
            this.nOTCHToolStripMenuItem.Click += new System.EventHandler(this.nOTCHToolStripMenuItem_Click);
            // 
            // hOLEToolStripMenuItem
            // 
            this.hOLEToolStripMenuItem.Name = "hOLEToolStripMenuItem";
            this.hOLEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hOLEToolStripMenuItem.Text = "Hole";
            this.hOLEToolStripMenuItem.Click += new System.EventHandler(this.hOLEToolStripMenuItem_Click);
            // 
            // mITERToolStripMenuItem
            // 
            this.mITERToolStripMenuItem.Name = "mITERToolStripMenuItem";
            this.mITERToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mITERToolStripMenuItem.Text = "Miter";
            this.mITERToolStripMenuItem.Click += new System.EventHandler(this.mITERToolStripMenuItem_Click);
            // 
            // cHAMFERToolStripMenuItem
            // 
            this.cHAMFERToolStripMenuItem.Name = "cHAMFERToolStripMenuItem";
            this.cHAMFERToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cHAMFERToolStripMenuItem.Text = "Chamfer";
            this.cHAMFERToolStripMenuItem.Click += new System.EventHandler(this.cHAMFERToolStripMenuItem_Click);
            // 
            // eDGEToolStripMenuItem
            // 
            this.eDGEToolStripMenuItem.Name = "eDGEToolStripMenuItem";
            this.eDGEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eDGEToolStripMenuItem.Text = "Edge";
            this.eDGEToolStripMenuItem.Click += new System.EventHandler(this.eDGEToolStripMenuItem_Click);
            // 
            // uSERDEFINEDToolStripMenuItem
            // 
            this.uSERDEFINEDToolStripMenuItem.Name = "uSERDEFINEDToolStripMenuItem";
            this.uSERDEFINEDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSERDEFINEDToolStripMenuItem.Text = "Userdefined";
            this.uSERDEFINEDToolStripMenuItem.Click += new System.EventHandler(this.uSERDEFINEDToolStripMenuItem_Click);
            // 
            // uNDEFINEDToolStripMenuItem
            // 
            this.uNDEFINEDToolStripMenuItem.Name = "uNDEFINEDToolStripMenuItem";
            this.uNDEFINEDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uNDEFINEDToolStripMenuItem.Text = "Undefined";
            this.uNDEFINEDToolStripMenuItem.Click += new System.EventHandler(this.uNDEFINEDToolStripMenuItem_Click);
            // 
            // IfcVoidingFeatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 396);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "IfcVoidingFeatureForm";
            this.Text = "IfcVoidingFeature";
            this.Load += new System.EventHandler(this.IfcVoidingFeatureForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem voidingFeatureTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cUTOUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nOTCHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hOLEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mITERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cHAMFERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eDGEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSERDEFINEDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uNDEFINEDToolStripMenuItem;
    }
}