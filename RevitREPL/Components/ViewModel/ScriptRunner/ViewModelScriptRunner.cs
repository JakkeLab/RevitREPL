using Autodesk.Revit.UI;
using RevitREPL.Components.Model;
using RevitREPL.Components.Repository;
using RevitREPL.RevitEventHandlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitREPL.Components.ViewModel.ScriptRunner
{
    public class ViewModelScriptRunner : INotifyPropertyChanged
    {
        public ViewModelScriptRunner(CodeBlockStorage storage)
        {
            CodeBlocks = storage.CodeBlocks;
            RunCode = new RelayCommand<object>(ExecuteRunCode, CanExecuteCommand);
            RunCodeArg = new CodeBlockArg();
            RunCodeExternalEvent = ExternalEvent.Create(new RunCodeExternalEventHandler(RunCodeArg));
            AddNewCode = new RelayCommand<object>(ExecuteAddNewCode, CanExecuteCommand);
            Logs = new ObservableCollection<string>();  // 로그 메시지 컬렉션 초기화
        }

        public ViewModelScriptRunner()
        {
            CodeBlocks = null;
            RunCode = new RelayCommand<object>(ExecuteRunCode, CanExecuteCommand);
            RunCodeArg = new CodeBlockArg();
            RunCodeExternalEvent = ExternalEvent.Create(new RunCodeExternalEventHandler(RunCodeArg));
            AddNewCode = new RelayCommand<object>(ExecuteAddNewCode, CanExecuteCommand);
            Logs = new ObservableCollection<string>();  // 로그 메시지 컬렉션 초기화
        }

        public ObservableCollection<string> Logs { get; set; }  // 로그 메시지 프로퍼티 추가
        public ObservableCollection<CodeBlockModel> CodeBlocks { get; set; }
        private CodeBlockModel _currentCodeBlock;

        public CodeBlockModel CurrentCodeBlock
        {
            get { return _currentCodeBlock; }
            set 
            { 
                _currentCodeBlock = value;
                OnPropertyChanged(nameof(CurrentCodeBlock));
            }
        }

        public CodeBlockArg RunCodeArg { get; set; }
        public ExternalEvent RunCodeExternalEvent { get; set; }

        public ICommand RunCode { get; }
        public ICommand AddNewCode { get; }

        public void ExecuteRunCode(object param)
        {
            RunCodeArg.CodeBlock = CurrentCodeBlock;
            RunCodeExternalEvent.Raise();

            //로그에 추가
            Logs.Add(RunCodeArg.CodeBlock.ResultMessage);
        }

        public void ExecuteAddNewCode(object param)
        {
            CodeBlocks.Add(new CodeBlockModel("New Code"));
        }

        public bool CanExecuteCommand(object param)
        {
            return true;
        }

        private async Task WaitForCompletion()
        {
            while (RunCodeArg.CodeBlock.ResultMessage == null)
            {
                await Task.Delay(100);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
