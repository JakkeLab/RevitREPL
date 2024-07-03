using RevitREPL.RevitAPIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.RevitEventHandlers
{
    public static class TransactionHandleHub
    {
        public static TransactionQueue<TransactionTicket> TransactionQueue { get; } = new TransactionQueue<TransactionTicket>();
    }
}
