using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Xbim.Common;
using Xbim.Ifc;
using System.IO;
using Xbim.Ifc4.ExternalReferenceResource;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MeasureResource;
using Xbim.Ifc4.PresentationAppearanceResource;
using Xbim.Ifc4.RepresentationResource;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.Common.Collections;

namespace EditIFC
{
    public partial class IfcSurfaceFeatureForm : Form
    {
        public IfcSurfaceFeatureForm()
        {
            InitializeComponent();
            // Showing the IIfcElements of the original model file
            updateListOfElements(dateiname);

        }

        public static string dateiname = EditIfcMainForm.FilePath;
        public static List <IIfcElement> instances = new List<IIfcElement>();
        public static IIfcElement selecteditem;
        public static int selectedindex;
        public static String imagepath;
        public static String[] array;

        private void updateListOfElements(string filename)
        {
            using (var model = IfcStore.Open(filename, EditIfcMainForm.credentials, -1.0))
            {
                instances.Clear();
                listBox2.Items.Clear();
                instances = model.Instances.OfType<IIfcElement>().ToList();
                array = new String[instances.Count];
                array = string.Join<Xbim.Common.IPersist>(Environment.NewLine, instances).Split('\n');
                for (int i = 0; i < array.Length; i++)
                {
                    listBox2.Items.Add(array[i]);
                }
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

        private string createHexBlob (string imgpath)
        {
            Image img = Image.FromFile(imgpath);
            ImageConverter imgCon = new ImageConverter();
            byte[] byarr = (byte[])imgCon.ConvertTo(img, typeof(byte[]));
            string str = "";
            for (int i = 0; i < byarr.Length; i++)
            {
                string binaries = Convert.ToString(byarr[i], 2).PadLeft(8, '0');
                int dec = Convert.ToInt32(binaries, 2);
                string hexStr = Convert.ToString(dec, 16);
                str += hexStr;
            }
            return str.ToUpper();
        }

        private byte[] createByteBlob(string imgpath)
        {
            Image img = Image.FromFile(imgpath);
            ImageConverter imgCon = new ImageConverter();
            byte[] byarr = (byte[])imgCon.ConvertTo(img, typeof(byte[]));
            return byarr;
        }

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
            string currentlocation = "";
            SaveFileDialog dialog = new SaveFileDialog();

            using (var model = IfcStore.Open(dateiname, EditIfcMainForm.credentials, -1.0))
            {

                if (radioButton1.Checked) //IfcBlobTexture
                {
                    using (var txn = model.BeginTransaction("Add IfcBlobTexture"))
                    {
                        var bloborimagetex = model.Instances.New<IfcBlobTexture>();
                        initializeSurfaceFeature(model, currentlocation, dialog, bloborimagetex);
                        txn.Commit();
                    }
                }
                else if (radioButton2.Checked) //IfcImageTexture
                {
                    using (var txn = model.BeginTransaction("Add IfcImageTexture"))
                    {
                        var bloborimagetex = model.Instances.New<IfcImageTexture>();
                        initializeSurfaceFeature(model, currentlocation, dialog, bloborimagetex);
                        txn.Commit();
                    }
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
                // save the model
                dialog.Filter = "IFC|*.ifc|Ifc ZIP|*.ifczip|XML|*.xml";
                dialog.Title = "Save an IFC File";
                dialog.RestoreDirectory = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    model.SaveAs(dialog.FileName.Split('\\').Last());
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
                this.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void initializeSurfaceFeature(IfcStore model, String currentlocation, SaveFileDialog dialog, IIfcSurfaceTexture bloborimagetex)
        {
            if (bloborimagetex.GetType().Name.Equals("IfcBlobTexture"))
            {
                ((IfcBlobTexture)bloborimagetex).RasterFormat = new IfcIdentifier(new Xbim.Ifc4.UtilityResource.IfcGloballyUniqueId(Guid.NewGuid().ToString()));
                ((IfcBlobTexture)bloborimagetex).RasterCode = new IfcBinary(createByteBlob(imagepath));
            }
            else
            {
                ((IfcImageTexture)bloborimagetex).URLReference = new IfcURIReference(imagepath);
            }
            // Getting the IfcElement selected from the list to contain the IfcSurfaceFeature
            IIfcElement surffeatelement = (IIfcElement)Array.Find(model.Instances.OfType<IIfcElement>().ToArray(), e1 => e1.GetType().GUID.Equals((instances[listBox2.SelectedIndex]).GetType().GUID));

            // Creating the Surface Feature entity
            surfacefeature = model.Instances.New<Xbim.Ifc4.StructuralElementsDomain.IfcSurfaceFeature>();
            // Adding Name & GUID
            surfacefeature.Name = "Surface Feature with " + bloborimagetex.GetType().Name;
            surfacefeature.GlobalId = new Xbim.Ifc4.UtilityResource.IfcGloballyUniqueId(Guid.NewGuid().ToString());

            //Positioning
            //Specifying the relative position of the surface feature to the IfcElement
            //IIfcObjectPlacement objplacement = ((IIfcElement)surffeatelement).ObjectPlacement;
            /*IIfcLocalPlacement lp = model.Instances.New<IfcLocalPlacement>();
            var origin = model.Instances.New<IfcCartesianPoint>();
            origin.SetXYZ(Convert.ToDouble(textBox17.Text), Convert.ToDouble(textBox15.Text), Convert.ToDouble(textBox16.Text));
            // Setting the axis placement3D attributes
            var ax3D = model.Instances.New<IfcAxis2Placement3D>();
            ax3D.Location = origin;
            ax3D.RefDirection = model.Instances.New<IfcDirection>();
            ax3D.RefDirection.SetXYZ(0, 1, 0);
            ax3D.Axis = model.Instances.New<IfcDirection>();
            ax3D.Axis.SetXYZ(0, 0, 1);
            lp.RelativePlacement = ax3D;*/

            //Final positioning of the Surface Feature

            /*var origin = model.Instances.New<IfcCartesianPoint>();
            var lp = model.Instances.New<IfcLocalPlacement>();
            lp = (IfcLocalPlacement)((IPersist)(surffeatelement.ObjectPlacement));
            IfcAxis2Placement ax2pl = lp.RelativePlacement;
            origin.SetXYZ(ax2pl.P[0].X + Convert.ToDouble(textBox17.Text), ax2pl.P[1].Y + Convert.ToDouble(textBox15.Text), ax2pl.P[2].Z + Convert.ToDouble(textBox16.Text));
            var ax3D = model.Instances.New<IfcAxis2Placement3D>();
            ax3D.Location = origin;
            ax3D.RefDirection = model.Instances.New<IfcDirection>();
            ax3D.RefDirection.SetXYZ(0, 1, 0);
            ax3D.Axis = model.Instances.New<IfcDirection>();
            ax3D.Axis.SetXYZ(1, 0, 0);
            lp.RelativePlacement = ax3D;*/

            var neworigin = model.Instances.New<IfcCartesianPoint>();
            neworigin.SetXYZ(Convert.ToDouble(textBox17.Text), Convert.ToDouble(textBox15.Text), Convert.ToDouble(textBox16.Text));
            var cartesiantransoperator2d = model.Instances.New<IfcCartesianTransformationOperator2D>();
            IfcDirection axis1 = model.Instances.New<IfcDirection>();
            IfcDirection axis2 = model.Instances.New<IfcDirection>();
            axis1.SetXYZ(0.0, 0.0, 1.0);
            axis2.SetXYZ(1.0, 0.0, 0.0);
            cartesiantransoperator2d.Axis1 = axis1;
            cartesiantransoperator2d.Axis2 = axis2;
            var localorigin = model.Instances.New<IfcCartesianPoint>();
            localorigin.SetXYZ(0.0, 0.0, 0.0);
            cartesiantransoperator2d.LocalOrigin = localorigin;
            cartesiantransoperator2d.Scale = new IfcReal(1.0);

            IfcAxis2Placement3D newplacement = model.Instances.New<IfcAxis2Placement3D>(newpl =>
            {
                newpl.Axis = axis1;
                newpl.RefDirection = axis2;
                newpl.Location = neworigin;
            });

            var lp = model.Instances.New<IfcLocalPlacement>(relpos=>
            {
                relpos.PlacementRelTo = (IfcLocalPlacement)surffeatelement.ObjectPlacement;
                relpos.RelativePlacement = newplacement;
            });

            surfacefeature.ObjectPlacement = lp;


            //Representation
            /* var cartesiantransoperator2d = model.Instances.New<IfcCartesianTransformationOperator2D>();
            IfcDirection axis1 = model.Instances.New<IfcDirection>(); ;
            IfcDirection axis2 = model.Instances.New<IfcDirection>();
            axis1.SetXYZ(0.0, 1.0, 0.0);
            axis2.SetXYZ(1.0, 0.0, 0.0);
            cartesiantransoperator2d.Axis1 = axis1;
            cartesiantransoperator2d.Axis2 = axis2;
            cartesiantransoperator2d.LocalOrigin = neworigin;
            cartesiantransoperator2d.Scale = new IfcReal(1.0);*/

            var x = new List<IfcLengthMeasure>();
            var pointlist = model.Instances.New<IfcCartesianPointList3D>(cpl =>
            {
                //IfcCartesianPointList3D.CoordList.Add(IfcCartesianPoint object)
                cpl.CoordList.Clear();
                cpl.CoordList.GetAt(0).AddRange(new List<IfcLengthMeasure>() { 0.0, 0.0, 0.0 });
                cpl.CoordList.GetAt(1).AddRange(new List<IfcLengthMeasure>() { 1.0, 0.0, 0.0 });
                cpl.CoordList.GetAt(2).AddRange(new List<IfcLengthMeasure>() { 1.0, 1.0, 0.0 });
                cpl.CoordList.GetAt(3).AddRange(new List<IfcLengthMeasure>() { 0.0, 1.0, 0.0 });
                cpl.CoordList.GetAt(4).AddRange(new List<IfcLengthMeasure>() { 0.0, 0.0, 2.0 });
                cpl.CoordList.GetAt(5).AddRange(new List<IfcLengthMeasure>() { 1.0, 0.0, 2.0 });
                cpl.CoordList.GetAt(6).AddRange(new List<IfcLengthMeasure>() { 1.0, 1.0, 2.0 });
                cpl.CoordList.GetAt(7).AddRange(new List<IfcLengthMeasure>() { 0.0, 1.0, 2.0 });
            });

            var indextritexmap = model.Instances.New<IfcIndexedTriangleTextureMap>(itm =>
            {
                itm.TexCoordIndex.GetAt(0).AddRange(new IfcPositiveInteger[] { 1, 4, 3 });
                itm.TexCoordIndex.GetAt(1).AddRange(new IfcPositiveInteger[] { 1, 2, 4 });
                itm.TexCoordIndex.GetAt(2).AddRange(new IfcPositiveInteger[] { 3, 1, 4 });
                itm.TexCoordIndex.GetAt(3).AddRange(new IfcPositiveInteger[] { 4, 1, 2 });
                itm.TexCoordIndex.GetAt(4).AddRange(new IfcPositiveInteger[] { 8, 7, 6 });
                itm.TexCoordIndex.GetAt(5).AddRange(new IfcPositiveInteger[] { 6, 7, 5 });
                itm.TexCoordIndex.GetAt(6).AddRange(new IfcPositiveInteger[] { 4, 3, 2 });
                itm.TexCoordIndex.GetAt(7).AddRange(new IfcPositiveInteger[] { 2, 3, 1 });
                itm.TexCoordIndex.GetAt(8).AddRange(new IfcPositiveInteger[] { 5, 8, 7 });
                itm.TexCoordIndex.GetAt(9).AddRange(new IfcPositiveInteger[] { 8, 5, 6 });
                itm.TexCoordIndex.GetAt(10).AddRange(new IfcPositiveInteger[] { 2, 4, 3 });
                itm.TexCoordIndex.GetAt(11).AddRange(new IfcPositiveInteger[] { 3, 1, 2 });
            });

            bloborimagetex.RepeatS = true;
            bloborimagetex.RepeatT = true;
            bloborimagetex.Mode = "TEXTURE";
            bloborimagetex.TextureTransform = cartesiantransoperator2d;

            var trifaceset = model.Instances.New<IfcTriangulatedFaceSet>(tfs=>
            {
                
                tfs.CoordIndex.GetAt(0).AddRange(new IfcPositiveInteger[] { 1, 6, 5 });
                tfs.CoordIndex.GetAt(1).AddRange(new IfcPositiveInteger[] { 1, 2, 6 });
                tfs.CoordIndex.GetAt(2).AddRange(new IfcPositiveInteger[] { 6, 2, 7 });
                tfs.CoordIndex.GetAt(3).AddRange(new IfcPositiveInteger[] { 7, 2, 3 });
                tfs.CoordIndex.GetAt(4).AddRange(new IfcPositiveInteger[] { 7, 8, 6 });
                tfs.CoordIndex.GetAt(5).AddRange(new IfcPositiveInteger[] { 6, 8, 5 });
                tfs.CoordIndex.GetAt(6).AddRange(new IfcPositiveInteger[] { 5, 8, 1 });
                tfs.CoordIndex.GetAt(7).AddRange(new IfcPositiveInteger[] { 1, 8, 4 });
                tfs.CoordIndex.GetAt(8).AddRange(new IfcPositiveInteger[] { 4, 2, 1 });
                tfs.CoordIndex.GetAt(9).AddRange(new IfcPositiveInteger[] { 2, 4, 3 });
                tfs.CoordIndex.GetAt(10).AddRange(new IfcPositiveInteger[] { 4, 8, 7 });
                tfs.CoordIndex.GetAt(11).AddRange(new IfcPositiveInteger[] { 7, 3, 4 });

            });
            trifaceset.Closed = true;
            trifaceset.Coordinates = pointlist;
            
            
            var vl = model.Instances.New<IfcTextureVertexList>(itm =>
            {
                itm.TexCoordsList.GetAt(0).AddRange(new IfcParameterValue[] { 0.0, -1.5 });
                itm.TexCoordsList.GetAt(1).AddRange(new IfcParameterValue[] { 1.0, -0.5 });
                itm.TexCoordsList.GetAt(2).AddRange(new IfcParameterValue[] { 0.0, 1.5 });
                itm.TexCoordsList.GetAt(3).AddRange(new IfcParameterValue[] { 1.0, 1.5 });
                itm.TexCoordsList.GetAt(4).AddRange(new IfcParameterValue[] { 0.0, 0.0 });
                itm.TexCoordsList.GetAt(5).AddRange(new IfcParameterValue[] { 0.0, 1.0 });
                itm.TexCoordsList.GetAt(6).AddRange(new IfcParameterValue[] { 1.0, 0.0 });
                itm.TexCoordsList.GetAt(7).AddRange(new IfcParameterValue[] { 1.0, 1.0 });

            });
            indextritexmap.TexCoords = vl;
            var styleditem = model.Instances.New<IfcStyledItem>();

            var surfstyle = model.Instances.New<IfcSurfaceStyle>();
            surfstyle.Side = IfcSurfaceSide.POSITIVE;
         
            IfcSurfaceStyleWithTextures surfstylewithtexture = model.Instances.New<IfcSurfaceStyleWithTextures>();
            surfstylewithtexture.Textures.Add((IfcSurfaceTexture)bloborimagetex);
            //Create a Definition shape to hold the the surface feature
            var shape = model.Instances.New<IfcShapeRepresentation>();
            shape.RepresentationIdentifier = "Body";
            shape.RepresentationType = "Tessalation";
            surfstyle.Styles.Add(surfstylewithtexture);
            styleditem.Item = trifaceset;
            styleditem.Styles.Add(surfstyle);
            indextritexmap.MappedTo = trifaceset;
            //Create a Product Definition and add the texture geometry to surface feature
            var product = model.Instances.New<IfcProductDefinitionShape>();
            shape.Items.Add(trifaceset);
            product.Representations.Add(shape);
            surfacefeature.Representation = product;
            surfacefeature.ObjectPlacement = lp;
            
            var relaggregate = new Create(model).RelAggregates(rel =>
            {
                rel.RelatedObjects.Add(surfacefeature);
                rel.RelatingObject = surffeatelement;
            });
        }
    }
}
