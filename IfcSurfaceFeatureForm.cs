using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.ExternalReferenceResource;
using Xbim.Ifc4.Interfaces;

namespace EditIFC
{
    public partial class IfcSurfaceFeatureForm : Form
    {
        public IfcSurfaceFeatureForm()
        {
            InitializeComponent();
            // Showing the IIfcElements of the original model file
            for (int i = 0; i < EditIfcMainForm.array.Length; i++)
            {
                listBox2.Items.Add(EditIfcMainForm.array[i]);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void IfcSurfaceFeatureForm_Load(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public static List<Xbim.Common.IPersist> instances = new List<IPersist>();
        public static IfcURIReference uriref;
        public static String imagepath;
        public static String[] array2;
        private void button1_Click(object sender, EventArgs e)
        {
            // Select the ifc file that contains the IfcElement used to cut the building element with IfcVoidingFeature 
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG|*.jpg|JPEG|*.jpeg|PNG|*.png";
            dialog.Title = "Open an IFC File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                imagepath = dialog.SafeFileName;
                uriref = imagepath;
            }

            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
