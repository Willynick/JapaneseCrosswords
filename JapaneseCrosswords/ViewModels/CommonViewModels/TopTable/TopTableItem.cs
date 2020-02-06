using JapaneseCrosswords.Utility;
using JapaneseCrosswords.ViewModels.CommonViewModels.MainTable;
using System.Windows;

namespace JapaneseCrosswords.ViewModels.CommonViewModels.TopTable
{
    public class TopTableItem : OnPropertyChangedClass
    {
        string _TopNumber;
        public string TopNumber
        {
            get
            {
                return _TopNumber;
            }
            set
            {
                if (_TopNumber != value)
                {
                    if (value != "")
                    {
                        MainVM.mainVm.isEmpty = false;
                    }
                    _TopNumber = value;
                    OnPropertyChanged("TopNumber");
                }
            }
        }

        private Visibility _VerticalLineVisibility = Visibility.Collapsed;
        public Visibility VerticalLineVisibility
        {
            get
            {
                return _VerticalLineVisibility;
            }
            set
            {
                _VerticalLineVisibility = value;
                OnPropertyChanged("VerticalLineVisibility");
            }
        }
    }
}
