﻿using System;
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
using Xbim.Ifc4.ProductExtension;

namespace EditIFC
{
    public partial class copyForm : Form
    {
        public copyForm()
        {
            InitializeComponent();
        }

        public static String dateiName;
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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private PropertyTranformDelegate semanticFilter = (property, parentObject) =>
        {
            return property.PropertyInfo.GetValue(parentObject, null);
        };

        public static IfcStore model, imodel;
        private void button3_Click(object sender, EventArgs e)
        {


            //var secondmodel = model.Instances.OfType<IInstantiableEntity>();
            model = IfcStore.Open(EditIfcMainForm.FilePath);  //copied model
            var modelelements = model.Instances.OfType<IfcElement>().ToArray();
            List<IfcElement> selectedelements = new List<IfcElement>();
            selectedelements.Add(modelelements[EditIfcMainForm.selectedindex]);
            
            imodel = IfcStore.Open(dateiName); //inserted Model
            // Copying the selected items into the other model 
            using (var txn = imodel.BeginTransaction("Insert copy"))
            {
                //single map should be used for all insertions between two models
                var map = new XbimInstanceHandleMap(model, imodel);
                foreach (var obj in selectedelements)
                {
                    imodel.InsertCopy(obj, map, semanticFilter, true, false);
                    var placement = obj.ObjectPlacement; //DON'T FORGET THE PLACEMENT
                }
                txn.Commit();
            }
            // saving the new merged file
            string filename = "merged_model.ifc";
            imodel.SaveAs(filename);
            string currentlocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + filename;
            var mergedmodel = IfcStore.Open(currentlocation);

            // save the model
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
            dialog.Title = "Save an IFC File";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mergedmodel.SaveAs(dialog.FileName.Split('\\').Last());
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
                this.Close();
            }
        }
    }
}