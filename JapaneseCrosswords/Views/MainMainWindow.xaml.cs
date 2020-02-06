using JapaneseCrosswords.Utility.FileManager;
using JapaneseCrosswords.ViewModels;
using JapaneseCrosswords.ViewModels.CommonViewModels.MainTable;
using System.Windows;
using System.Windows.Input;

namespace JapaneseCrosswords.Views
{
    /// <summary>
    /// Interaction logic for MainMainWindow.xaml
    /// </summary>
    public partial class MainMainWindow : Window
    {
        public static MainMainWindow thisWindow;

        public MainMainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM(new DefaultDialogService(), new BinaryFileService());
            thisWindow = this;
        }

        public void ChangeIndex(int index)
        {
            transitioner1.SelectedIndex = index;
            if (MainVM.mainVm.isEmpty == false)
            {
                DataContext = new MainVM(new DefaultDialogService(), new BinaryFileService());
            }
        }
    }
}
