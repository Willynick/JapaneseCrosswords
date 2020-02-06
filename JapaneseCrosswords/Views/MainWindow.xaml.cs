using System;
using System.Windows;
using System.Windows.Controls;

namespace JapaneseCrosswords.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MoveCommand(object sender, RoutedEventArgs e)
        {
            MainMainWindow.thisWindow.ChangeIndex(Convert.ToInt32(((Button)sender).Tag));
        }
    }
}
