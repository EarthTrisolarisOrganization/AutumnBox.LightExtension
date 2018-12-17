using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsh2401.MVVM;

namespace Zsh2401.AnimationSpeedSetter
{
    class VMMain : ViewModelBase
    {
        public IDevice Device
        {
            get => _device; set
            {
                _device = value;
                RaisePropertyChanged();
            }
        }
        private IDevice _device;

        public Context Context
        {
            get => _ctx; set
            {
                _ctx = value;
                if (Device == null)
                {
                    Device = _ctx.GetService<IDeviceSelector>(ServicesNames.DEVICE_SELECTOR).GetCurrent(_ctx);
                }
            }
        }
        private Context _ctx;

        public string WindowSpeed
        {
            get => _windowSpeed; set
            {
                _windowSpeed = value;
                RaisePropertyChanged();
            }
        }
        private string _windowSpeed = "0.75";

        public string AnimatorSpeed
        {
            get => _animatorSpeed; set
            {
                _animatorSpeed = value;
                RaisePropertyChanged();
            }
        }
        private string _animatorSpeed = "0.75";

        public string TransitionSpeed
        {
            get => _transitionSpeed; set
            {
                _transitionSpeed = value;
                RaisePropertyChanged();
            }
        }
        private string _transitionSpeed = "0.75";

        public FlexiableCommand SetWindowSpeed
        {
            get => _setWindowSpeed;
            set
            {
                _setWindowSpeed = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _setWindowSpeed;

        public FlexiableCommand SetAnimatorSpeed
        {
            get => _setAnimatorSpeed;
            set
            {
                _setAnimatorSpeed = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _setAnimatorSpeed;

        public FlexiableCommand SetTransitionSpeed
        {
            get => _setTransitionSpeed;
            set
            {
                _setTransitionSpeed = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _setTransitionSpeed;

        private readonly CommandExecutor executor;
        public VMMain()
        {
            executor = new CommandExecutor();
            SetWindowSpeed = new FlexiableCommand(SetWindowSpeedAction);
            SetAnimatorSpeed = new FlexiableCommand(SetAnimatorSpeedAction);
            SetTransitionSpeed = new FlexiableCommand(SetTransitionSpeedAction);
        }
        private void SetWindowSpeedAction()
        {
            if (float.TryParse(WindowSpeed, out float num))
            {
                executor.AdbShell(Device, "settings put global window_animation_scale", num.ToString());
            }
            else
            {
                Context.Ux.Warn("请输入一个正确的数字!");
            }
        }
        private void SetAnimatorSpeedAction()
        {
            if (float.TryParse(AnimatorSpeed, out float num))
            {
                executor.AdbShell(Device, "settings put global transition_animation_scale ", num.ToString());
            }
            else
            {
                Context.Ux.Warn("请输入一个正确的数字!");
            }
        }
        private void SetTransitionSpeedAction()
        {
            if (int.TryParse(TransitionSpeed, out int num))
            {
                executor.AdbShell(Device, "settings put global animator_duration_scale ", num.ToString());
            }
            else
            {
                Context.Ux.Warn("请输入一个正确的数字!");
            }
        }
    }
}
