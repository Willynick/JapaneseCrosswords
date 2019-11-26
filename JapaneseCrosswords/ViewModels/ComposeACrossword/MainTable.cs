using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using JapaneseCrosswords.Views;
using Utility;
using System.Windows;

namespace JapaneseCrosswords.ViewModels.ComposeACrossword
{
    class MainTable : OnPropertyChangedClass
    {
        private int width = 10;
        private int height = 10;

        public string Width
        {
            get
            {
                return width.ToString();
            }
            set
            {
                width = Convert.ToInt32(value);
                OnPropertyChanged("Width");
            }
        }

        public string Height
        {
            get
            {
                return height.ToString();
            }
            set
            {
                height = Convert.ToInt32(value);
                OnPropertyChanged("Height");
            }
        }

        private RelayCommand _command;

        public object createMainTable
        {
            get
            {
                return _command ??
                    (_command = new RelayCommand(obj =>
                    {
                        /*for (int i = 0; i < width; i++)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                dataGrid.Columns.Add(new DataGridTextColumn());
                            }
                        }
                        dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;*/
                    }));
            }
        }
    }
}
