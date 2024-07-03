using RevitREPL.Components.Model;
using RevitREPL.Components.Repository;
using RevitREPL.Components.ViewModel.ScriptRunner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitREPL.Components.View.ScriptRunner
{
    /// <summary>
    /// Interaction logic for WDScriptRunner.xaml
    /// </summary>
    public partial class WDScriptRunner : Window
    {
        public ViewModelScriptRunner ViewModel;
        
        public WDScriptRunner()
        {
            InitializeComponent();
            ViewModel = new ViewModelScriptRunner();
            DataContext = ViewModel;
        }

        public WDScriptRunner(CodeBlockStorage codeBlockStroage)
        {
            InitializeComponent();
            ViewModel = new ViewModelScriptRunner(codeBlockStroage);
            DataContext = ViewModel;
        }
    }
}
