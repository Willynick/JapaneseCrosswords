using JapaneseCrosswords.Utility;
using JapaneseCrosswords.ViewModels.CommonViewModels.MainTable;
using System.Windows;

namespace JapaneseCrosswords.ViewModels.CommonViewModels.LeftTable
{
    public class LeftTableItem : OnPropertyChangedClass
    {
        string _LeftNumber;
        public string LeftNumber
        {
            get
            {
                return _LeftNumber;
            }
            set
            {
                if (_LeftNumber != value)
                {
                    if (value != "")
                    {
                        MainVM.mainVm.isEmpty = false;
                    }
                    _LeftNumber = value;
                    OnPropertyChanged("LeftNumber");
                }
            }
        }

        private Visibility _HorizontalLineVisibility = Visibility.Collapsed;
        public Visibility HorizontalLineVisibility
        {
            get
            {
                return _HorizontalLineVisibility;
            }
            set
            {
                _HorizontalLineVisibility = value;
                OnPropertyChanged("HorizontalLineVisibility");
            }
        }
    }
}
