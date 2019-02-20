/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 16:21:56 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Logging;
using AutumnBox.OpenFramework.Content;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Zsh2401.MVVM
{
    class ViewModelBase : NotificationObject
    {
        private class VMContext : Context { }
        public ICommand OpenUrl
        {
            get
            {
                return _openUrl;
            }
            set
            {
                _openUrl = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _openUrl;

        public ICommand OpenGoUrl
        {
            get
            {
                return _openGoUrl;
            }
            set
            {
                _openGoUrl = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _openGoUrl;

        private readonly static VMContext ctx = new VMContext();

        public ViewModelBase()
        {
            OpenUrl = new FlexiableCommand(_OpenUrl);
            OpenGoUrl = new FlexiableCommand(_OpenGoUrl);
        }

        protected void _OpenGoUrl(object para)
        {
            try
            {
                var goPre = ctx.App.GetPublicResouce("UrlGoPrefix") as string;
                Process.Start(goPre + para);
            }
            catch (Exception e)
            {
                SLogger.Warn(this, $"can not open url {para}", e);
            }
        }
        protected void _OpenUrl(object para)
        {
            try
            {
                Process.Start(para as string);
            }
            catch (Exception e)
            {
                SLogger.Warn(this, $"can not open url {para}", e);
            }
        }

        protected virtual bool RaisePropertyChangedOnDispatcher { get; set; } = false;
        protected override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {

            if (RaisePropertyChangedOnDispatcher)
            {
                ctx.App.RunOnUIThread(() =>
                {
                    base.RaisePropertyChanged(propertyName);
                });
            }
            else
            {
                base.RaisePropertyChanged(propertyName);
            }
            base.RaisePropertyChanged(propertyName);
        }
    }
}
