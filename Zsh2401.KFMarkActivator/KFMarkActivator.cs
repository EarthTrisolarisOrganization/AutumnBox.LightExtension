using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.OpenFramework.Open;
using System.IO;
using AutumnBox.Basic.Calling;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.Extension;

namespace Zsh2401.KFMarkActivator
{
    [ExtText(EXTRACTING, "Extracting...", "zh-cn:提取实例执行文件中")]
    [ExtText(PUSHING, "Pushing", "zh-cn:推送中")]
    [ExtText(EXECUTING, "Executing", "zh-cn:执行中")]
    [ExtName("KFMark Act", "zh-cn:快否激活器")]
    [ExtAuth("zsh2401")]
    [ExtIcon("kfmark.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public class KFMarkActivator : LeafExtensionBase
    {
        private const string EXTRACTING = "__ext";
        private const string PUSHING = "__p";
        private const string EXECUTING = "__wtf";

        [LMain]
        public void EntryPoint(IDevice device, ILeafUI ui, TextAttrManager text, IEmbeddedFileManager emb, ITemporaryFloder tmp)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();

                ui.Tip = text[EXTRACTING];
                tmp.Create();
                var filePath = Path.Combine(tmp.DirInfo.ToString(), "daemon");
                var tgtFile = new FileInfo(filePath);
                var file = emb.Get("daemon");

                ui.Tip = text[PUSHING];
                file.ExtractTo(tgtFile);

                ui.Tip = text[PUSHING];
                ICommandResult result = null;
                using (var executor = new CommandExecutor())
                {
                    executor.To(e => ui.WriteOutput(e.Text));
                    executor.Adb(device, $"push {tgtFile.FullName} /data/local/tmp/daemon");
                    executor.AdbShell(device, "chmod 777 /data/local/tmp/daemon");
                    result = executor.AdbShell(device, "./data/local/tmp/daemon &");
                }
                ui.Finish(result.ExitCode);
            }
        }
    }
}
