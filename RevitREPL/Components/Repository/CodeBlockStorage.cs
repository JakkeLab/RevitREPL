using RevitREPL.Components.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.Components.Repository
{
    public class CodeBlockStorage
    {
        public ObservableCollection<CodeBlockModel> CodeBlocks { get; set; }

        public CodeBlockStorage()
        {
            CodeBlocks = new ObservableCollection<CodeBlockModel>();
        }
    }
}
