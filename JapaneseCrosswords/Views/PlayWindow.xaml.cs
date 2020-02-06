using System.Windows.Controls;

namespace JapaneseCrosswords.Views
{
    /// <summary>
    /// Interaction logic for Play.xaml
    /// </summary>
    public partial class PlayWindow : UserControl
    {
        public PlayWindow()
        {
            InitializeComponent();
            //DataContext = new MainVM(new DefaultDialogService(), new BinaryFileService());
        }
    }
}
