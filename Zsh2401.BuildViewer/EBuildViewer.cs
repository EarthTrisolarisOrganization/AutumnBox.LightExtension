using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.Open;
using System.Threading;

namespace Zsh2401.BuildViewer
{
    [ExtIcon("list.png")]
    [ExtName("Build浏览器")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(
        DeviceState.Poweron |
        DeviceState.Recovery
        )]
    [ExtVersion(1,2,0)]
    class EBuildViewer : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IDevice device, IAppManager app)
        {
            bool isClosed = false;
            app.RunOnUIThread(() =>
            {
                var window = new MainWindow(device);
                window.Closed += (s, e) => isClosed = true;
                window.Show();
            });
            while (!isClosed)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
