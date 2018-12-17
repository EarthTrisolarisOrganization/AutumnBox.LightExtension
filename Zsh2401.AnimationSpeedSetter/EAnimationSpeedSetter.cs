using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsh2401.AnimationSpeedSetter
{
    [ExtName("动画速度调节器")]
    [ExtDesc("其实与开发者选项里能调的没啥区别")]
    [ExtAuth("zsh2401")]
    [ExtIcon("animation.png")]
    [ExtRequiredDeviceStates(AutumnBox.Basic.Device.DeviceState.Poweron)]
    class EAnimationSpeedSetter : SharpExtension
    {
        protected override void Processing(Dictionary<string, object> data)
        {
            RunOnUIThread(() =>
            {
                new MainWindow(this).ShowDialog();
            });
        }
    }
}
