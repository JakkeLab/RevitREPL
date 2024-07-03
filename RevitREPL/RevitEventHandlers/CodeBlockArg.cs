using RevitREPL.Components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.RevitEventHandlers
{
    public class CodeBlockArg
    {
        public CodeBlockModel CodeBlock { get; set; }
        public void SetCodeBlock(CodeBlockModel codeBlock)
        {
            CodeBlock = codeBlock;
        }

        public CodeBlockArg()
        {
            CodeBlock = null;
        }
    }
}
