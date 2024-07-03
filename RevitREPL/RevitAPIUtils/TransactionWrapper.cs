using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitREPL.RevitEventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.RevitAPIUtils
{
    public static class TransactionWrapper
    {
        public static void ExecuteTransaction(UIApplication uiapp, string transactionName, Action<Action<IEnumerable<ElementId>>> action)
        {
            var app = uiapp.Application;
            var elementIds = new List<ElementId>();

            using (var tr = new Transaction(uiapp.ActiveUIDocument.Document, transactionName))
            {
                tr.Start();

                action(elementIdsInvolved =>
                {
                    elementIds.AddRange(elementIdsInvolved);
                });

                var transactionTicket = new TransactionTicket(transactionName, elementIds);
                TransactionHandleHub.TransactionQueue.Enqueue(transactionTicket);

                tr.Commit();
            }
        }
    }
}
