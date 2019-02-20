using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsh2401.BuildViewer
{
    static class Descriptions
    {
        public static bool DescContainers(string key, string str)
        {
            return Get(key).ToLower().Contains(str.ToLower());
        }
        public static bool IsHaveDescription(string key)
        {
            return descs.TryGetValue(key, out string result);
        }
        public static string Get(string key)
        {
            if (descs.TryGetValue(key, out string result))
            {
                return result;
            }
            else
            {
                return "无";
            }
        }
        private readonly static Dictionary<string, string> descs = new Dictionary<string, string>()
        {
            { "DEVICE_PROVISIONED","设备是否被初始化过"},
            { "ro.build.version.release","安卓版本号"},
            { "ro.build.product","通常是设备代号"},
                    { "ro.product.model","通常是设备的机型"},
                      { "ro.product.brand","设备厂家"},
                      { "ro.serialno","设备序列号"},
                                { "ro.sf.lcd_density","设备默认DPI"},
                                 { "ro.vendor.miui.region","MIUI系统的地区类型"},
                                            { "ro.wlan.vendor","wlan芯片供应商"},
                                                  { "wifi.interface","wifi接口"},
                                                                  { "net.hostname","网络设备名"},
                                                                            { "net.bt.name","网络系统名"},
                                                                            { "persist.sys.device_name","设备名"},
        };
    }
}
