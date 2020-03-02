namespace EditIFC
{
    partial class IfcBuildingElementProxyForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.buildingProxyElementTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.complexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.partialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provisionForVoidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provisionForSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userdefinedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notdefinedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(693, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 44);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(212, 13);
            this.label27.TabIndex = 66;
            this.label27.Text = "IFC Shape Representations found in the file";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 63);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(776, 108);
            this.listBox1.TabIndex = 65;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(622, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(164, 23);
            this.button2.TabIndex = 67;
            this.button2.Text = "Create IfcBuildingElementProxy";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Select Local Placement";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(10, 202);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(776, 121);
            this.listBox2.TabIndex = 70;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildingProxyElementTypeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(15, 347);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(174, 24);
            this.menuStrip1.TabIndex = 72;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // buildingProxyElementTypeToolStripMenuItem
            // 
            this.buildingProxyElementTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.complexToolStripMenuItem,
            this.elementToolStripMenuItem,
            this.partialToolStripMenuItem,
            this.provisionForVoidToolStripMenuItem,
            this.provisionForSpaceToolStripMenuItem,
            this.userdefinedToolStripMenuItem,
            this.notdefinedToolStripMenuItem});
            this.buildingProxyElementTypeToolStripMenuItem.Name = "buildingProxyElementTypeToolStripMenuItem";
            this.buildingProxyElementTypeToolStripMenuItem.Size = new System.Drawing.Size(166, 20);
            this.buildingProxyElementTypeToolStripMenuItem.Text = "Building Proxy ElementType";
            this.buildingProxyElementTypeToolStripMenuItem.Click += new System.EventHandler(this.buildingProxyElementTypeToolStripMenuItem_Click);
            // 
            // complexToolStripMenuItem
            // 
            this.complexToolStripMenuItem.Name = "complexToolStripMenuItem";
            this.complexToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.complexToolStripMenuItem.Text = "Complex";
            this.complexToolStripMenuItem.Click += new System.EventHandler(this.complexToolStripMenuItem_Click);
            // 
            // elementToolStripMenuItem
            // 
            this.elementToolStripMenuItem.Name = "elementToolStripMenuItem";
            this.elementToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.elementToolStripMenuItem.Text = "Element";
            this.elementToolStripMenuItem.Click += new System.EventHandler(this.elementToolStripMenuItem_Click);
            // 
            // partialToolStripMenuItem
            // 
            this.partialToolStripMenuItem.Name = "partialToolStripMenuItem";
            this.partialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.partialToolStripMenuItem.Text = "Partial";
            this.partialToolStripMenuItem.Click += new System.EventHandler(this.partialToolStripMenuItem_Click);
            // 
            // provisionForVoidToolStripMenuItem
            // 
            this.provisionForVoidToolStripMenuItem.Name = "provisionForVoidToolStripMenuItem";
            this.provisionForVoidToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.provisionForVoidToolStripMenuItem.Text = "ProvisionForVoid";
            this.provisionForVoidToolStripMenuItem.Click += new System.EventHandler(this.provisionForVoidToolStripMenuItem_Click);
            // 
            // provisionForSpaceToolStripMenuItem
            // 
            this.provisionForSpaceToolStripMenuItem.Name = "provisionForSpaceToolStripMenuItem";
            this.provisionForSpaceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.provisionForSpaceToolStripMenuItem.Text = "ProvisionForSpace";
            this.provisionForSpaceToolStripMenuItem.Click += new System.EventHandler(this.provisionForSpaceToolStripMenuItem_Click);
            // 
            // userdefinedToolStripMenuItem
            // 
            this.userdefinedToolStripMenuItem.Name = "userdefinedToolStripMenuItem";
            this.userdefinedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.userdefinedToolStripMenuItem.Text = "Userdefined";
            this.userdefinedToolStripMenuItem.Click += new System.EventHandler(this.userdefinedToolStripMenuItem_Click);
            // 
            // notdefinedToolStripMenuItem
            // 
            this.notdefinedToolStripMenuItem.Name = "notdefinedToolStripMenuItem";
            this.notdefinedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.notdefinedToolStripMenuItem.Text = "Notdefined";
            this.notdefinedToolStripMenuItem.Click += new System.EventHandler(this.notdefinedToolStripMenuItem_Click);
            // 
            // IfcBuildingElementProxyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "IfcBuildingElementProxyForm";
            this.Text = "IfcBuildingElementProxyForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem buildingProxyElementTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem complexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem partialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem provisionForVoidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem provisionForSpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userdefinedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notdefinedToolStripMenuItem;
    }
}