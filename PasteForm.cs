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
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProductExtension;

namespace EditIFC
{
    public partial class PasteForm : Form
    {
        public PasteForm()
        {
            InitializeComponent();
        }

        private PropertyTranformDelegate semanticFilter = (property, parentObject) =>
        {
            return property.PropertyInfo.GetValue(parentObject, null);
        };

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
            {
                DialogResult result = MessageBox.Show("Select an item from the above list to paste!");
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
            else
            {
                updateSelectedItemsAndIndeces(DateiName);
                //var secondmodel = model.Instances.OfType<IInstantiableEntity>();
                var model = IfcStore.Open(DateiName); //to be copied to Model
                var modelelements = model.Instances.OfType<IfcElement>().ToArray();
                var imodel = IfcStore.Open(EditIfcMainForm.FilePath);  //copied model
                foreach (var item in selecteditems)
                {
                    List<IIfcElement> selectedelements = new List<IIfcElement>();
                    selectedelements.Add(item);
                    // Pasting the selected items into the other model 
                    using (var txn = imodel.BeginTransaction("Insert copy"))
                    {
                        //single map should be used for all insertions between two models
                        var map = new XbimInstanceHandleMap(model, imodel);
                        foreach (var obj in selectedelements)
                        {
                            imodel.InsertCopy(obj, map, semanticFilter, true, false);
                        }
                        txn.Commit();
                    }
                }
                // saving the new merged file
                string filename = "merged_model.ifc";
                imodel.SaveAs(filename);
                string currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + filename;
                var mergedmodel = IfcStore.Open(currentlocation);

                // save the model
                mergedmodel.SaveAs(EditIfcMainForm.FilePath.Split('\\').Last());

                currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + EditIfcMainForm.FilePath.Split('\\').Last();
                // make a copy in the specified location by user using the dialog box
                System.IO.File.Copy(currentlocation, EditIfcMainForm.FilePath, true);
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
                    this.Close();
                }
            }


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static IIfcElement[] instances;
        public static String[] array;
        public static String[] array2;
        string DateiName;
        public static List<Xbim.Ifc4.ProductExtension.IfcBuildingStorey> storeys;
        public static List<IIfcElement> selecteditems = new List<IIfcElement>();
        public static List<int> selectedindices = new List<int>();

        private void updateListOfElements(string dateiname, XbimEditorCredentials cred)
        {
            using (var model = IfcStore.Open(dateiname, cred, -1.0))
            {
                instances = model.Instances.OfType<IIfcElement>().ToArray();
                array = new String[instances.Length];
                listBox1.Items.Clear();
                array = string.Join<Xbim.Common.IPersist>(Environment.NewLine, instances).Split('\n');
                for (int i = 0; i < array.Length; i++)
                {
                    listBox1.Items.Add(array[i]);
                }
            }
        }

        private void updateSelectedItemsAndIndeces(string filename)
        {
            selectedindices.Clear();
            selecteditems.Clear();
            selectedindices = listBox1.SelectedIndices.Cast<int>().ToList();
            using (var model = IfcStore.Open(filename, EditIfcMainForm.credentials, -1.0))
            {
                instances = model.Instances.OfType<IIfcElement>().ToArray();
                foreach (var i in selectedindices)
                {
                    selecteditems.Add(Array.Find(instances, e => e.GetType().GUID.Equals(instances[i].GetType().GUID)));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
            dialog.Title = "Open an IFC File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                DateiName = textBox1.Text;
                updateListOfElements(DateiName, EditIfcMainForm.credentials);
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DateiName = textBox1.Text;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
