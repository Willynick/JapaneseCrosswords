using JapaneseCrosswords.Utility;
using System.Windows;
using System.Windows.Input;

namespace JapaneseCrosswords.ViewModels.CommonViewModels.MainTable
{
    public class ItemVM : OnPropertyChangedClass
    {

        public ItemVM()
        {
            
        }

        GridState _Color;
        public GridState Color
        {
            get
            {
                return _Color;
            }
            set
            {
                if (_Color != value)
                {
                    _Color = value;
                    OnPropertyChanged("Color");
                }
            }
        }

        private Visibility _CrossVisibility = Visibility.Collapsed;
        public Visibility CrossVisibility
        {
            get { return _CrossVisibility; }
            set
            {
                _CrossVisibility = value;
                OnPropertyChanged("CrossVisibility");
            }
        }

        private Visibility _RedCrossVisibility = Visibility.Collapsed;
        public Visibility RedCrossVisibility
        {
            get { return _RedCrossVisibility; }
            set
            {
                _RedCrossVisibility = value;
                OnPropertyChanged("RedCrossVisibility");
            }
        }

        private ICommand _VisibilityCommand;

        public ICommand VisibilityCommand
        {

            get
            {
                return _VisibilityCommand ??
                    (_VisibilityCommand = new RelayCommand(obj =>
                    {
                        if (CrossVisibility == Visibility.Collapsed)
                        {
                            CrossVisibility = Visibility.Visible;
                        }
                        else
                        {
                            CrossVisibility = Visibility.Collapsed;
                        }

                    }));
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

        private ICommand _ColorCommand;

        public ICommand ColorCommand
        {

            get
            {
                return _ColorCommand ??
                    (_ColorCommand = new RelayCommand(obj =>
                    {
                        CrossVisibility = Visibility.Collapsed;
                        RedCrossVisibility = Visibility.Collapsed;

                        if (Color == GridState.White)
                        {
                            Color = GridState.Black;
                        }
                        else
                        {
                            Color = GridState.White;
                        }
                        if (MainVM.mainVm.createNewTables)
                        MainVM.mainVm.CreateTablesItems(MainVM.mainVm.createNewTables);
                        MainVM.mainVm.isEmpty = false;

                    }));
            }
        }
    }
}
