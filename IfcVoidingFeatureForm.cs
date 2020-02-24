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
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.RepresentationResource;
using Xbim.IO;
using Xbim.ModelGeometry.Scene;

namespace EditIFC
{
    public partial class IfcVoidingFeatureForm : Form
    {
        public IfcVoidingFeatureForm()
        {
            InitializeComponent();
            // Showing the IIfcElements of the original model file
            for (int i = 0; i < Form1.array.Length; i++)
            {
                listBox2.Items.Add(Form1.array[i]);
            }
        }

        private void IfcVoidingFeatureForm_Load(object sender, EventArgs e)
        {

        }

        //Filter to control the data copied over
        private PropertyTranformDelegate semanticFilter = (property, parentObject) =>
        {
            return property.PropertyInfo.GetValue(parentObject, null);
        };

        public static List<Xbim.Common.IPersist> instances = new List<IPersist>();
        public static IfcStore voidmodel;
        public static String[] array2;
        private void button1_Click(object sender, EventArgs e)
        {
            // Select the ifc file that contains the IfcElement used to cut the building element with IfcVoidingFeature 
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
            dialog.Title = "Open an IFC File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                String DateiName = dialog.SafeFileName;
            }

            var credentials = Form1.credentials;
            // Showing the IfcElement instances in the file to select the required element
            using (var model = IfcStore.Open(textBox1.Text, credentials, -1.0))
            {
                voidmodel = model;
                instances = model.Instances.OfType<IIfcElement>().ToList<Xbim.Common.IPersist>();
                String[] array = string.Join<Xbim.Common.IPersist>(Environment.NewLine, instances.ToArray()).Split('\n');
                for (int i = 0; i < array.Length; i++)
                {
                    listBox1.Items.Add(array[i]);
                }
            }

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var model = IfcStore.Open(textBox1.Text);  //original model
            var voids = model.Instances.OfType<IIfcBuildingElement>();
            var imodel = IfcStore.Open(Form1.FilePath); //inserted Model
            // Copying the file containing the void object into the original model 
            using (var txn = imodel.BeginTransaction("Insert copy"))
            {
                //single map should be used for all insertions between two models
                var map = new XbimInstanceHandleMap(model, imodel);
                foreach (var obj in voids)
                {
                    imodel.InsertCopy(obj, map, semanticFilter, true, false);
                    var placement = obj.ObjectPlacement;
                }
                txn.Commit();
            }
            // saving the new merged file
            string filename = "merged_model.ifc";
            imodel.SaveAs(filename);
            string currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + filename;     
            var mergedmodel = IfcStore.Open(currentlocation);
            IPersist[] mergedinstances = mergedmodel.Instances.ToList<IPersist>().ToArray();
            //Might cause a problem if there are multiple similar building elements since the find criterion is only based on the name!
            IPersist cuttingobj = Array.Find(mergedinstances, e1 => e1.GetType().Name.Equals(instances[listBox1.SelectedIndex].GetType().Name));  //ToString().Split('=')[1]
            IPersist tobecutobj = Array.Find(mergedinstances, e2 => e2.GetType().Name.Equals(Form1.instances[listBox2.SelectedIndex].GetType().Name));
            var mod = mergedmodel;
            // create the IfcVoidingFeature element
            using (var txn = mod.BeginTransaction("IfcVoidingFeature"))
            {
                var create = new Create(mod);
                var voidingfeature = mod.Instances.New<Xbim.Ifc4.StructuralElementsDomain.IfcVoidingFeature>();
                voidingfeature.Name = "Void";
                voidingfeature.GlobalId = new Xbim.Ifc4.UtilityResource.IfcGloballyUniqueId(Guid.NewGuid().ToString());
                voidingfeature.Representation = ((IIfcElement)cuttingobj).Representation as Xbim.Ifc4.RepresentationResource.IfcProductRepresentation;
                var decomposition = create.RelAggregates( rel =>
                {
                    rel.RelatingObject = (IIfcElement)tobecutobj;
                    rel.RelatedObjects.Add((IIfcElement)voidingfeature);
                }
                );
                voidingfeature.ObjectPlacement = ((IIfcElement)cuttingobj).ObjectPlacement as IfcObjectPlacement;
                var relvoidselements = create.RelVoidsElement(rel =>
                {
                    rel.RelatedOpeningElement = voidingfeature;//openingelement;
                    rel.RelatingBuildingElement = ((IIfcElement)tobecutobj);
                });
                // remove the solid representation "IfcBuildingElementProxy" void object blocking the view from the model
                mod.Delete((IPersistEntity)cuttingobj);
                txn.Commit();
                // save the model
                dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
                dialog.Title = "Save an IFC File";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    mod.SaveAs(dialog.FileName.Split('\\').Last());
                }
            }
            currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + dialog.FileName.Split('\\').Last();
            // make a copy in the specified location by user using the dialog box
            System.IO.File.Copy(currentlocation, dialog.FileName, true);
            // Delete the redundant file saved in the execution folder 
            if (System.IO.File.Exists(currentlocation))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(currentlocation);
                }
                catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            }

            // Delete the redundant merged_model file saved in the execution folder   
            currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + filename;
            if (System.IO.File.Exists(currentlocation))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(currentlocation);
                }
                catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            }
            this.Close();    
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
