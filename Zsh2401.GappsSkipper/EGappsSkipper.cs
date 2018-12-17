using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsh2401.GappsSkipper
{
    [ExtName("跳过Gapps开机检测")]
    [ExtAuth("zsh2401")]
    [ExtIcon("googleplay.png")]
    [ExtDesc("帮助你跳过Gappps的开机检测")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    class EGappsSkipper : SharpExtension
    {
        protected override void Processing(Dictionary<string, object> data)
        {
            IDevice currentDevice = GetService<IDeviceSelector>(ServicesNames.DEVICE_SELECTOR).GetCurrent(this);
            Executor.AdbShell(currentDevice, "settings", "put", "secure", "user_setup_complete", "1");
            Executor.AdbShell(currentDevice, "settings", "put", "global", "device_provisioned ", "1");
            Ux.Message("已执行跳过命令");
        }
    }
}
