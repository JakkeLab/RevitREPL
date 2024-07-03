using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.RevitAPIUtils
{
    public class TransactionTicket
    {
        public string TransactionName { get; }
        public IEnumerable<ElementId> RevitElementIds { get; }

        public TransactionTicket(string transactionName, IEnumerable<ElementId> elementIds)
        {
            TransactionName = transactionName;
            RevitElementIds = new List<ElementId>(elementIds);
        }

        public TransactionTicket(string transactionName)
        {
            TransactionName = transactionName;
            RevitElementIds = new List<ElementId>();
        }
    }
}
