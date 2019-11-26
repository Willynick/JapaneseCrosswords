using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JapaneseCrosswords.Views
{
    /// <summary>
    /// Interaction logic for ComposeACrossword.xaml
    /// </summary>
    public partial class ComposeACrosswordWindow : Window
    {
        public ComposeACrosswordWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
            }

            Border border;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    border = new Border();
                    border.BorderBrush = new SolidColorBrush(Colors.Black); border.BorderThickness = new Thickness(1);
                    border.Child = new Grid();
                    grid.Children.Add(border);
                    grid.Children.Add(new Button());

                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                }
            }

        }
    }
}
