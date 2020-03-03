using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.SharedBldgElements;

namespace EditIFC
{
    public partial class IfcBuildingElementProxyForm : Form
    {
        public IfcBuildingElementProxyForm()
        {
            InitializeComponent();
        }

        String dateiName;
        public static IIfcRepresentation [] instances;
        public static IIfcLocalPlacement[] placements;
        public static String[] array;
        public static String[] array2;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
            dialog.Title = "Open an IFC File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dateiName = dialog.FileName;
                textBox1.Text = dialog.FileName;
            }

            using (var model = IfcStore.Open(textBox1.Text, EditIfcMainForm.credentials, -1.0))
            {

                instances = model.Instances.OfType<IIfcRepresentation>().ToArray();
                array = new String[instances.Length];
                listBox1.Items.Clear();
                array = string.Join<Xbim.Common.IPersist>(Environment.NewLine, instances).Split('\n');
                for (int i = 0; i < array.Length; i++)
                {
                    listBox1.Items.Add(array[i]);
                }

                placements = model.Instances.OfType<IIfcLocalPlacement>().ToArray();
                array2 = new String[placements.Length];
                listBox2.Items.Clear();
                array2 = string.Join<Xbim.Common.IPersist>(Environment.NewLine, placements).Split('\n');
                for (int i = 0; i < array2.Length; i++)
                {
                    listBox2.Items.Add(array2[i]);
                }
            }
           
        }

        Nullable<IfcBuildingElementProxyTypeEnum> type = null;
        IIfcBuildingElementProxy ifcbeproxy;
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            using (var mod = IfcStore.Open(dateiName, EditIfcMainForm.credentials, -1.0))
            {
                using (var txn = mod.BeginTransaction("IfcBuildingElementProxy"))
                {

                    var create = new Create(mod);
                    ifcbeproxy = mod.Instances.New<IfcBuildingElementProxy>();
                    ifcbeproxy.Name = "Building Element Proxy";
                    ifcbeproxy.GlobalId = new Xbim.Ifc4.UtilityResource.IfcGloballyUniqueId(Guid.NewGuid().ToString());
                    ifcbeproxy.ObjectType = new IfcLabel("Building Element Proxy");
                    var selectedrepresentation = Array.Find(mod.Instances.ToList().ToArray(), e1 => e1.GetType().GUID.Equals(instances[listBox1.SelectedIndex].GetType().GUID));
                    ifcbeproxy.Representation = selectedrepresentation as IIfcProductRepresentation;
                    var localplacement = Array.Find(mod.Instances.ToList().ToArray(), e2 => e2.GetType().FullName.Equals(placements[listBox2.SelectedIndex].GetType().FullName));
                    ifcbeproxy.ObjectPlacement = localplacement as IIfcObjectPlacement;

                    //DialogResult result = MessageBox.Show("Select a voiding feature type from the drop down menu", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (type == null)
                    {
                        DialogResult result = MessageBox.Show("Select a Building Element Proxy type from the drop down menu!");
                        if (result == DialogResult.OK)
                        {
                            Close();
                            txn.Commit();
                            IfcBuildingElementProxyForm.ActiveForm.Close();
                        }

                    }
                    else
                    {
                        ifcbeproxy.PredefinedType = type;
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
                }
                        String currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + dialog.FileName.Split('\\').Last();
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
                        this.Close();
                }


        }

      
        private void button3_Click(object sender, EventArgs e)
        {

            
        }

        private void buildingProxyElementTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void complexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = IfcBuildingElementProxyTypeEnum.COMPLEX;
            buildingProxyElementTypeToolStripMenuItem.Text = "Complex";
        }

        private void elementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = IfcBuildingElementProxyTypeEnum.ELEMENT;
            buildingProxyElementTypeToolStripMenuItem.Text = "Element";
        }

        private void partialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = IfcBuildingElementProxyTypeEnum.PARTIAL;
            buildingProxyElementTypeToolStripMenuItem.Text = "Partial";
        }

        private void provisionForVoidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = IfcBuildingElementProxyTypeEnum.PROVISIONFORVOID;
            buildingProxyElementTypeToolStripMenuItem.Text = "ProvisionForVoid";
        }

        private void provisionForSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = IfcBuildingElementProxyTypeEnum.PROVISIONFORSPACE;
            buildingProxyElementTypeToolStripMenuItem.Text = "ProvisionForSpace";
        }

        private void userdefinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = IfcBuildingElementProxyTypeEnum.NOTDEFINED;
            buildingProxyElementTypeToolStripMenuItem.Text = "Notdefined";
        }

        private void notdefinedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = IfcBuildingElementProxyTypeEnum.NOTDEFINED;
            buildingProxyElementTypeToolStripMenuItem.Text = "Notdefined";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
