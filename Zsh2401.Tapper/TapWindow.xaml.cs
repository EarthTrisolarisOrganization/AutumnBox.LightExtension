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

namespace Zsh2401.Tapper
{
    /// <summary>
    /// TapWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TapWindow
    {
        public TapWindow(IDevice targetDevice)
        {
            InitializeComponent();
            (DataContext as VMTapper).Device = targetDevice;
        }
    }
}
