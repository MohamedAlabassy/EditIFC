using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.IO;
using System.Windows.Forms;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.ProductExtension;
using System.Diagnostics;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc4.ActorResource;
using Xbim.Ifc4.DateTimeResource;
using Xbim.Ifc4.ExternalReferenceResource;
using Xbim.Ifc4.PresentationOrganizationResource;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.MaterialResource;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.ProfileResource;
using Xbim.Ifc4.PropertyResource;
using Xbim.Ifc4.QuantityResource;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.SharedBldgElements;
using System.Reflection;
using System.IO;
using System.Collections;

namespace EditIFC
{

    public partial class EditIfcMainForm : Form
    {

        public EditIfcMainForm()
        {
            InitializeComponent();
            foreach (Type ifcType in allEntitiesType)
            {
                if (ifcType.Name.StartsWith("Ifc") && !ifcType.Namespace.Contains(".Interfaces")
                    && ifcType.GetInterface(nameof(IInstantiableEntity)) != null)
                {
                    ifcentities.Add(ifcType.Name);
                }
            }
            ifcentities.Sort();
            foreach (string e in ifcentities)
            {
                listBox3.Items.Add(e);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateListOfElements (string dateiname, XbimEditorCredentials cred)
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

        private void updateSelectedItemsAndIndeces (string filename)
        {
            selectedindices.Clear();
            selecteditems.Clear();
            selectedindices = listBox1.SelectedIndices.Cast<int>().ToList();
            using (var model = IfcStore.Open(filename, credentials, -1.0))
            {
                instances = model.Instances.OfType<IIfcElement>().ToArray();
                foreach (var i in selectedindices)
                {
                    selecteditems.Add(Array.Find(instances, e => e.GetType().GUID.Equals(instances[i].GetType().GUID)));
                }
            }
        }

        public static XbimEditorCredentials credentials = new XbimEditorCredentials
        {
            ApplicationDevelopersName = "Prfessur Intelligentes Technisches Design",
            ApplicationFullName = "EditIFC",
            ApplicationIdentifier = "EditIFC",
            ApplicationVersion = "1.0",
            EditorsFamilyName = "Alabassy",
            EditorsGivenName = "Mohamed S. H.",
            EditorsOrganisationName = "Buhaus-Universitaet Weimar"
        };

        public static IIfcElement[] instances;
        public static String[] array;
        public static String[] array2;
        string DateiName;
        public static List<Xbim.Ifc4.ProductExtension.IfcBuildingStorey> storeys;
        public static List<IIfcElement> selecteditems = new List<IIfcElement>();
        public static List<int> selectedindices = new List<int>();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
            dialog.Title = "Open an IFC File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
                DateiName = textBox1.Text;
                updateListOfElements(DateiName, credentials);
            }
            
        }

        private void listBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            updateSelectedItemsAndIndeces(DateiName);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private static string ifc4dllpath = Application.StartupPath.ToString() + @"\Xbim.Ifc4.dll";
        public static Assembly assembly = Assembly.LoadFile(ifc4dllpath);
        public static Type[] allEntitiesType = assembly.GetTypes();
        public static Type[] entitiesType;
        public static ArrayList indeces = new ArrayList();
        public static ArrayList ifcentities = new ArrayList();
        public static Type ifctype;

        private void button10_Click(object sender, EventArgs e)
        {

        }
        public static string FilePath;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DateiName = textBox1.Text;
            FilePath = DateiName;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex == -1 || listBox3.SelectedItems.Count > 1 || selecteditems.Count > 1)
            {
                MessageBox.Show("Please select only one item first!");
            }
            else
            {
                string name = listBox3.SelectedItem.ToString();
                ifctype = Array.Find(allEntitiesType, element => element.Name.Split('.').Last().Equals(name));
                ConstructorInfo cinfo = ifctype.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(IModel), typeof(int), typeof(bool) }, null);
                IInstantiableEntity result = (IInstantiableEntity)cinfo.Invoke(new object[] { IfcStore.Open(textBox1.Text, credentials, -1.0), 1, true });
                FormFactory entity = FormFactory.get();
                entity.GetForm((IfcObject)result).ShowDialog();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form copy = new CopyForm();
            if (listBox1.SelectedItems.Count==0)
            {
                DialogResult result = MessageBox.Show("Select an item from the above list to copy!");
                if (result == DialogResult.OK)
                {
                    Close();
                } 
            } else
            {
                updateSelectedItemsAndIndeces(DateiName);
                copy.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            String currentlocation;
            if (listBox1.SelectedItems.Count == 0)
            {
                DialogResult result = MessageBox.Show("Select an item from the above list to delete!");
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
            else
            {
                using (var model = IfcStore.Open(FilePath, credentials, -1.0))
                {
                    using (var txn = model.BeginTransaction("Delete selected element"))
                    {
                        foreach (var i in selecteditems) 
                        {
                            IPersistEntity element2bedeleted = Array.Find(model.Instances.ToArray(), e1 => e1.GetType().GUID.Equals(i.GetType().GUID));
                            model.Delete(element2bedeleted); 
                        }
                        txn.Commit();
                    }
                    model.SaveAs(FilePath.Split('\\').Last());
                }
                currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + FilePath.Split('\\').Last();
                System.IO.File.Copy(currentlocation, FilePath, true);
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
            }
            updateListOfElements(DateiName, credentials);
            updateSelectedItemsAndIndeces(DateiName);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form paste = new PasteForm();
            paste.ShowDialog();
            updateListOfElements(DateiName, credentials);
            updateSelectedItemsAndIndeces(DateiName);
        }
    }
}
