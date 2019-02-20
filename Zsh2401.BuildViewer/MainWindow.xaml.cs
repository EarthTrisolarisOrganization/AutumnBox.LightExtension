using AutumnBox.Basic.Device;
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

namespace Zsh2401.BuildViewer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IDevice device)
        {
            InitializeComponent();
            (DataContext as VMBuild).Device = device;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (DataContext as VMBuild).DoFilter.Execute(TBSearch.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TBSearch.Text = null;
        }
    }
}
