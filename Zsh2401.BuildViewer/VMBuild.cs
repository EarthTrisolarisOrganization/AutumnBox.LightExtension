using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Zsh2401.MVVM;

namespace Zsh2401.BuildViewer
{
    class VMBuild : ViewModelBase
    {
        public IDevice Device
        {
            get => _dev; set
            {
                _dev = value;
                RaisePropertyChanged();
                Load();
            }
        }
        private IDevice _dev;

        private Dictionary<string, string> Source { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Result
        {
            get => _result; set
            {
                _result = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<KeyValuePair<string, string>> _result;

        public ICommand Refresh
        {
            get => _ref; set
            {
                _ref = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _ref;

        public ICommand DoFilter
        {
            get => _doFilter; set
            {
                _doFilter = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _doFilter;

        public VMBuild()
        {
            RaisePropertyChangedOnDispatcher = true;
            Refresh = new FlexiableCommand(() =>
            {
                Load();
            });
            DoFilter = new FlexiableCommand(_DoFilter);
        }

        public async void Load()
        {
            var buildPorp = new DeviceBuildPropGetter(Device);
            await Task.Run(() =>
            {
                Source = buildPorp.GetFull();
                _DoFilter(null);
            });
        }

        private void _DoFilter(object _filter)
        {
            try
            {
                SLogger.Debug(this, "filting");
                if (string.IsNullOrEmpty(_filter?.ToString()))
                {
                    SLogger.Debug(this, "filter is null,do not filter but sort");
                    Result = from kv in Source
                             orderby Descriptions.IsHaveDescription(kv.Key) descending
                             select kv;
                    return;
                }
                var filter = _filter.ToString().ToLower();
                SLogger.Debug(this, "filter is not null do filter and sort");
                Result = from kv in Source
                         where kv.Key.ToLower().Contains(filter)
                         || kv.Value.ToLower().Contains(filter)
                         || Descriptions.DescContainers(kv.Key, filter)
                         orderby Descriptions.IsHaveDescription(kv.Key) descending
                         select kv;
                SLogger.Debug(this, "filted");
            }
            catch (Exception e)
            {
                SLogger.Warn(this, "Can not filter", e);
            }

        }
    }
}
