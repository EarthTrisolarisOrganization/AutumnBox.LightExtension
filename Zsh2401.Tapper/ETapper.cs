using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ManagementV2.OS;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsh2401.Tapper
{
    [ExtName("Tapper", "zh-cn:Tapper控制器")]
    [ExtIcon("icon.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    class ETapper : LeafExtensionBase
    {
        public static IAppManager _app;
        [LMain]
        public void Main(IDevice device, IAppManager app)
        {
            _app = app;
            app.RunOnUIThread(() =>
            {
                new TapWindow(device).Show();
            });
        }
    }
}
