using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Utility;
using System.Windows;
using JapaneseCrosswords.Views;

namespace JapaneseCrosswords.ViewModels
{
    public class OpenWindow : OnPropertyChangedClass
    {
        /*private ICommand _tCommand;
        public ICommand tCommand => _tCommand ?? (_tCommand = new RelayCommand(obj =>
        {
            Window window = null;
            window = new ComposeACrossword();
            window.Show();
            
        }));*/

        private RelayCommand _command;

        public object openWindow
        {
            get
            {
                return _command ??
                    (_command = new RelayCommand(obj =>
                    {
                        string window = obj as string;
                        if (window == "ComposeACrossword")
                        {
                            new ComposeACrosswordWindow().Show();
                        }
                        else if (window == "SolveCrossword")
                        {
                            new SolveCrosswordWindow().Show();
                        }
                        else if (window == "Play")
                        {
                            new PlayWindow().Show();
                        }
                    }));
            }
        }

    }
}
