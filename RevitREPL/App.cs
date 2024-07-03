using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using RevitREPL.AddinControl;
using RevitREPL.TabUI;
using System;
using System.Reflection;

namespace RevitREPL
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                //Addin Controller에 Application 등록
                AddinController.Instance.RegisterApplication(application);

                // RibbonTab
                string tabName = "ScriptRunner";
                application.CreateRibbonTab(tabName);
                Assembly assembly = Assembly.GetExecutingAssembly();
                string assemblyPath = assembly.Location;

                // RibbonPanel
                RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "My Panel");
                PushButtonData buttonData = new PushButtonData("btnDataScriptRunner", "ScriptRunner", assemblyPath, "RevitREPL.Command");
                PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

                // return result
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                return Result.Failed;
            }
        }



        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }

    public class CommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication app, CategorySet cat)
        {
            if (app.ActiveUIDocument == null)
            {
                return true;
            }
            return false;
        }
    }
}
