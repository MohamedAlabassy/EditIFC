using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;

namespace EditIFC
{
    /// <summary>
    /// Generate the forms for editing ifc 4 entities
    /// </summary>
    public class FormFactory
    {
        private static readonly FormFactory entity = new FormFactory();
        public static FormFactory get()
        {
            return (FormFactory) entity;
        }
        public Form GetForm (IIfcObject obj)
        {
            if (obj is IIfcWall ifcWall)
            {
                return new IfcWallForm();
            }
            else if (obj is IIfcVoidingFeature ifcvoidingfeature)
            {
                return new IfcVoidingFeatureForm();
            }
            else if (obj is IIfcSurfaceFeature ifcsurfacefeature)
            {
                return new IfcSurfaceFeatureForm();
            }
            else return null;
        }

        /*public Form GetDamageForm (IIfcObject obj)
        {
            if (obj is IIfcVoidingFeature ifcvoidingfeature)
            {
                return new IfcVoidingFeatureForm();
            }
            else return null;
        }*/

    }
}
