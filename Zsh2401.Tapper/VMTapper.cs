using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ManagementV2.OS;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zsh2401.MVVM;

namespace Zsh2401.Tapper
{
    class VMTapper : ViewModelBase
    {
        public IDevice Device
        {
            get
            {
                return _dev;
            }
            set
            {
                _dev = value;
                RaisePropertyChanged();
            }
        }
        private IDevice _dev;

        public ICommand Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _input;

        private readonly ICommandExecutor executor;
        public VMTapper()
        {
            Input = new MVVMCommand(InputImpl);
            executor = new HestExecutor();
        }
        private void InputImpl(object para)
        {
            try
            {
                AndroidKeyCode code = (AndroidKeyCode)para;
                executor.AdbShell(Device, $"input keyevent {(int)code}");
            }
            catch (Exception e)
            {
                SLogger<ETapper>.Warn($"can not input key {para}", e);
            }
        }
    }
}
