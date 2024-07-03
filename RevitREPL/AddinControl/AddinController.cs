using Autodesk.Revit.UI;
using RevitREPL.Components.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.AddinControl
{
    public class AddinController
    {
        private static Lazy<AddinController> _instance = new Lazy<AddinController>(() => new AddinController());

        public static AddinController Instance { get { return _instance.Value; } }

        public CodeBlockStorage CodeBlockStorage { get; set; }

        private UIControlledApplication _application;

        public void RegisterApplication(UIControlledApplication app)
        {
            _application = app;
        }

        public UIControlledApplication GetRegisteredApplication()
        {
            return _application;
        }

        public AddinController()
        {
            CodeBlockStorage = new CodeBlockStorage();
        }
    }
}
