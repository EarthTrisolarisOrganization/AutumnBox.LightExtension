using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System.Threading;

namespace Zsh2401.AppOpsXActivator
{
    [ExtName("App Ops X", "zh-cn:激活AppOpsX")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    [ExtIcon("icon.png")]
    [ExtText(ISINSTALL, "Please make sure that you have installed AppOpsX", "zh-cn:请确保你已经安装了AppOpsX")]
    public class EAppOpsXActivator : LeafExtensionBase
    {
        private const string ISINSTALL = "a";
        [LMain]
        public void EntryPoint(IDevice device, ILeafUI ui, TextAttrManager texts)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                if (!ui.DoYN(texts[ISINSTALL]))
                {
                    ui.EShutdown();
                }

                using (var executor = new CommandExecutor())
                {
                    executor.To(e => ui.WriteOutput(e.Text));
                    ui.WriteLine("启动AppOpsX主界面");
                    executor.AdbShell(device,"am start -n com.zzzmode.appopsx/com.zzzmode.appopsx.ui.main.MainActivity");
                    Thread.Sleep(1500);
                    var result = executor.AdbShell(device, "sh /sdcard/Android/data/com.zzzmode.appopsx/opsx.sh ");
                    ui.Finish(result.ExitCode);
                }
            }
        }
    }
}
