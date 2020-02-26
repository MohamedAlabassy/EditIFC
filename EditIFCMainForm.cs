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
            foreach (string o in ifcentities)
            {
                listBox3.Items.Add(o);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
        public static List<IIfcElement> instances;
        public static String[] array;
        public static String[] array2;
        public static List<Xbim.Ifc4.ProductExtension.IfcBuildingStorey> storeys;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
            dialog.Title = "Open an IFC File";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
                String DateiName = dialog.SafeFileName;
                textBox1.Text = dialog.FileName;
            }

            using (var model = IfcStore.Open(textBox1.Text, credentials, -1.0))
            {
                storeys = model.Instances.OfType<IfcBuildingStorey>().ToList<Xbim.Ifc4.ProductExtension.IfcBuildingStorey>();
                instances = model.Instances.OfType<IfcElement>().ToList<IIfcElement>();
                array = new String[instances.Count];
                listBox1.Items.Clear();
                array = string.Join<Xbim.Common.IPersist>(Environment.NewLine, instances.ToArray()).Split('\n');
                for (int i = 0; i < array.Length; i++)
                {
                    listBox1.Items.Add(array[i]);
                }
  
            }

        }

        private void listBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        public static Xbim.Ifc4.ProductExtension.IfcBuildingStorey StoreyLevel;
        //public IfcStore model = IfcStore.Open(FilePath, credentials, -1.0);

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
            FilePath = textBox1.Text;
        }
        
        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex == -1 || listBox3.SelectedItems.Count>1)
            {
                MessageBox.Show("Please select one item first!");
            } else {
                string name = Convert.ToString(listBox3.SelectedItem);
                ifctype = Array.Find(allEntitiesType, element => element.Name.Split('.').Last().Equals(name));
                ConstructorInfo cinfo = ifctype.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(IModel), typeof(int), typeof(bool) }, null);
                IInstantiableEntity result = (IInstantiableEntity)cinfo.Invoke(new object[] {IfcStore.Open(textBox1.Text, credentials, -1.0), 1, true});
                FormFactory entity = FormFactory.get();
                entity.GetForm((IIfcObject)result).ShowDialog();
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
    }
}
