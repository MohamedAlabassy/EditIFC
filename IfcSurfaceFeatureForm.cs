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
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometryResource;
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
        
        public static String imagepath;
        public static String[] array2;
        private void button1_Click(object sender, EventArgs e)
        {
            // Select the ifc file that contains the IfcElement used to apply the surface feature on 
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG|*.jpg|JPEG|*.jpeg|PNG|*.png";
            dialog.Title = "Open an IFC File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                imagepath = dialog.FileName;
            }

            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        IIfcSurfaceFeature surfacefeature;
        private void button3_Click(object sender, EventArgs e)
        {
            using (var model = IfcStore.Open(EditIfcMainForm.FilePath, EditIfcMainForm.credentials, -1.0))
            {   
                // Getting the IfcElement selected from the list to contain the IfcSurfaceFeature
                IPersist surffeatelement = Array.Find(EditIfcMainForm.instances.ToArray(), e1 => e1.GetType().GUID.Equals(listBox2.SelectedIndex.GetType().GUID));
                
                // Creating the Surface Feature entity
                surfacefeature = model.Instances.New<Xbim.Ifc4.StructuralElementsDomain.IfcSurfaceFeature>();
                
                // Specifying the relative position of the surface feature to the IfcElement
                IIfcObjectPlacement objplacement = ((IIfcElement)surffeatelement).ObjectPlacement;
                IIfcLocalPlacement lp = objplacement.ReferencedByPlacements.FirstOrDefault();
                var origin = model.Instances.New<IfcCartesianPoint>();
                origin.SetXYZ(Convert.ToDouble(textBox17.Text), Convert.ToDouble(textBox15.Text), Convert.ToDouble(textBox16.Text));
                // Setting the axis placement3D attributes
                var ax3D = model.Instances.New<IfcAxis2Placement3D>();
                ax3D.Location = origin;
                ax3D.RefDirection.SetXYZ(0, 1, 0);
                ax3D.Axis = model.Instances.New<IfcDirection>();
                ax3D.Axis.SetXYZ(0, 0, 1);
                lp.RelativePlacement = ax3D;
                // Final positioning of the Surface Feature
                surfacefeature.ObjectPlacement = lp;

                surfacefeature.Name = "Texture";
                surfacefeature.GlobalId = new Xbim.Ifc4.UtilityResource.IfcGloballyUniqueId(Guid.NewGuid().ToString());


                //
                if (radioButton1.Checked) //IfcBlobTexture
                {

                }
                else if (radioButton2.Checked) //IfcImageTexture
                {
                    IfcURIReference uriref = imagepath;
                }
                else
                {
                    DialogResult result = MessageBox.Show("Select a surface feature type!");
                    if (result == DialogResult.OK)
                    {
                        Close();
                        IfcSurfaceFeatureForm.ActiveForm.Close();
                        IfcSurfaceFeatureForm.ActiveForm.ShowDialog();
                    }
                }
            }
                
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
