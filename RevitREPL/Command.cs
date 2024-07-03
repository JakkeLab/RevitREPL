using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitREPL.AddinControl;
using RevitREPL.Components.View.ScriptRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            WDScriptRunner scriptRunner = new WDScriptRunner(AddinController.Instance.CodeBlockStorage);
            scriptRunner.Show();

            return Result.Succeeded;
        }
    }
}
