using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
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
    class EGappsSkipper : LeafExtensionBase
    {
        public void Main(ILeafUI ui, IDevice device, ICommandExecutor executor)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                executor.OutputReceived += (s, e) => ui.WriteOutput(e.Text);
                ui.Closing += (s, e) =>
                {
                    executor.Dispose();
                    return true;
                };
                executor.AdbShell(device, "settings put secure user_setup_complete 1");
                executor.AdbShell(device, "settings put global device_provisioned 1");
                ui.Finish();
            }
        }
    }
}
