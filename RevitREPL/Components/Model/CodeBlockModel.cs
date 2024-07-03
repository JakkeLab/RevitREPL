using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitREPL.Components.Model
{
    public class CodeBlockModel : INotifyPropertyChanged
    {
		private string _title;
		private string _codeBlock;
		private string _resultMessage;

		public string Title
		{
			get { return _title; }
			set 
			{ 
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}

        public string CodeBlock
        {
            get { return _codeBlock; }
            set
            {
                _codeBlock = value;
                OnPropertyChanged(nameof(CodeBlock));
            }
        }

        public string ResultMessage
        {
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged(nameof(ResultMessage));
            }
        }

        public CodeBlockModel(string title)
        {
            Title = title;
            CodeBlock = string.Empty;
        }

        public CodeBlockModel()
        {
            Title = string.Empty;
            CodeBlock = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
	}
}
