using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RevitREPL.Components.Model;
using RevitREPL.RevitAPIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.RevitEventHandlers
{
    public class RunCodeExternalEventHandler : IExternalEventHandler
    {
        public RunCodeExternalEventHandler(CodeBlockArg codeBlock)
        {
            CodeBlockArg = codeBlock;
        }

        private string _transactionName = "RunCode";
        public CodeBlockArg CodeBlockArg { get; set; }

        public void Execute(UIApplication app)
        {
            ExecuteCode(app);
        }

        private void ExecuteCode(UIApplication app)
        {
            CodeBlockArg.CodeBlock.ResultMessage = "Success";

            try
            {
                TransactionWrapper.ExecuteTransaction(app, _transactionName, callback =>
                {
                    var codeString = CodeBlockArg.CodeBlock.CodeBlock;

                    var options = ScriptOptions.Default
                        .AddReferences(Assembly.GetExecutingAssembly())
                        .AddReferences(typeof(Element).Assembly)
                        .AddReferences(typeof(UIApplication).Assembly)
                        .AddImports("System")
                        .AddImports("System.Linq")
                        .AddImports("Autodesk.Revit.DB")
                        .AddImports("Autodesk.Revit.UI");

                    var globals = new Globals { App = app };

                    var script = CSharpScript.Create(codeString, options, typeof(Globals));
                    var compilation = script.GetCompilation();
                    var diagnostics = compilation.GetDiagnostics();

                    if (diagnostics.Any(d => d.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error))
                    {
                        CodeBlockArg.CodeBlock.ResultMessage = "Compilation errors: " + string.Join("\n", diagnostics.Where(d => d.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error).Select(d => d.ToString()));
                    }
                    else
                    {
                        var scriptState = script.RunAsync(globals).Result;
                        CodeBlockArg.CodeBlock.ResultMessage = scriptState.ReturnValue?.ToString() ?? "No result";
                    }
                });
            }
            catch (CompilationErrorException ex)
            {
                CodeBlockArg.CodeBlock.ResultMessage = "Compilation errors: " + string.Join("\n", ex.Diagnostics.Select(diagnostic => diagnostic.ToString()));
            }
            catch (Exception ex)
            {
                CodeBlockArg.CodeBlock.ResultMessage = "Error: " + ex.Message;
            }
        }

        public string GetName()
        {
            return "ExternalEventHandler_Raiser";
        }

        public class Globals
        {
            public UIApplication App { get; set; }
            public Document Doc => App?.ActiveUIDocument?.Document;
        }
    }
}
