using IRrecv;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace IRrecv
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Methods

        protected override void OnExit(ExitEventArgs e)
        {
            IRrecv.Properties.Settings.Default.Save();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IRrecv.Properties.Settings.Default.Reload();
            IRrecv.Properties.Settings.Default.PropertyChanged += (object sender, PropertyChangedEventArgs e2) =>
            {
                if (e2.PropertyName.CompareTo(nameof(IRrecv.Properties.Settings.Default.Autostart)) == 0)
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                        if (IRrecv.Properties.Settings.Default.Autostart)
                            key.SetValue(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase), Assembly.GetExecutingAssembly().Location, RegistryValueKind.String);
                        else
                            key.DeleteValue(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase));
            };
            if (IRrecv.Properties.Settings.Default.IrProfiles == null)
            {
                IRrecv.Properties.Settings.Default.IrProfiles = new Settings.IrRemoteProfileCollection();
                IRrecv.Properties.Settings.Default.Save();
            }
            if (IRrecv.Properties.Settings.Default.SerialPort == null)
            {
                IRrecv.Properties.Settings.Default.SerialPort = new IRrecv.Settings.SerialPort();
                IRrecv.Properties.Settings.Default.Save();
            }
        }

        #endregion
    }
}
