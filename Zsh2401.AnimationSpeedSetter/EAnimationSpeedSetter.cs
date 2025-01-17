﻿using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsh2401.AnimationSpeedSetter
{
    [ExtName("动画速度调节器")]
    [ExtDesc("其实与开发者选项里能调的没啥区别,并且在部分安卓系统中,此模块的实现方法已经失效")]
    [ExtAuth("zsh2401")]
    [ExtIcon("animation.png")]
    [ExtVersion(0,0,5)]
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
