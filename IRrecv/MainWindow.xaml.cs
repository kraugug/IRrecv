using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IRrecv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Fileds

        public static readonly RoutedCommand CommandClearLogs = new RoutedCommand();
        public static readonly RoutedCommand CommandCloseSerialPort = new RoutedCommand();
        public static readonly RoutedCommand CommandOpenIrRemoteProfiles = new RoutedCommand();
        public static readonly RoutedCommand CommandOpenSerialPort = new RoutedCommand();
        public static readonly RoutedCommand CommandOpenSerialPortSettings = new RoutedCommand();
        public static readonly RoutedCommand CommandSaveLogs = new RoutedCommand();

        #endregion

        #region Properties

        public IrSerialPort SerialPort { get; }

        private NotifyIcon TrayIcon { get; }

        #endregion

        #region Methods

        private void Button_Click_1(object sender, RoutedEventArgs e) 
        {
            SerialPort.Open(Properties.Settings.Default.SerialPort);
        }

        #region Commands

        private void CommandClearLogs_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = !string.IsNullOrEmpty(TextBoxPortData.Text);

        private void CommandClearLogs_Executed(object sender, ExecutedRoutedEventArgs e) => TextBoxPortData.Clear();

        private void CommandCloseSerialPort_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = SerialPort.IsOpen;

        private void CommandCloseSerialPort_Executed(object sender, ExecutedRoutedEventArgs e) => SerialPort.Close();

        private void CommandOpenIrRemoteProfiles_Executed(object sender, ExecutedRoutedEventArgs e) => new IrRemoteProfilesWindow() { Owner = this }.ShowDialog();

        private void CommandOpenSerialPort_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = SerialPort.IsClose;

        private void CommandOpenSerialPort_Executed(object sender, ExecutedRoutedEventArgs e) => SerialPort.Open(Properties.Settings.Default.SerialPort);

        private void CommandOpenSerialPortSettins_Executed(object sender, ExecutedRoutedEventArgs e) => new SerialPortSettingsWindow() { Owner = this }.ShowDialog();

        private void CommandSaveLogs_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = !string.IsNullOrEmpty(TextBoxPortData.Text);

        private void CommandSaveLogs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.CheckPathExists = true;
            dialog.Title = "Save logs...";
            if (dialog.ShowDialog().Value)
                System.IO.File.WriteAllText(dialog.FileName, TextBoxPortData.Text);
        }

        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SerialPort.IsOpen)
                SerialPort.Close();
            TrayIcon.Visible = false;
            base.OnClosing(e);
        }

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void ParseReceivedData(string data)
        {

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string received = (sender as SerialPort).ReadExisting();
            if (Properties.Settings.Default.Logging)
                Dispatcher.Invoke(new Action(() =>
                {
                    TextBoxPortData.AppendText(received);
                    TextBoxPortData.ScrollToEnd();
                }));
            ParseReceivedData(received);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                ShowInTaskbar = false;
                WindowState = WindowState.Minimized;
            }
        }

        #endregion

        #region Constructor

        public MainWindow()
        {
            DataContext = this;
            SerialPort = new IrSerialPort();
            SerialPort.DataReceived += SerialPort_DataReceived;
            TrayIcon = new NotifyIcon();
            TrayIcon.DoubleClick += (object sender, EventArgs e) =>
            {
                ShowInTaskbar = true;
                WindowState = WindowState.Normal;
                Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (Width / 2);
                Top = (Screen.PrimaryScreen.Bounds.Height / 2) - (Height / 2);
                Activate();
            };
            TrayIcon.Icon = Properties.Resources.TrayIcon;
			TrayIcon.Text = "IRrecv";
			TrayIcon.Visible = true;
            InitializeComponent();
            if (Properties.Settings.Default.StartMinimized)
            {
                ShowInTaskbar = false;
                WindowState = WindowState.Minimized;
            }
            if(Properties.Settings.Default.OpenSerialPortAtStartup)
            {
                SerialPort.Open(Properties.Settings.Default.SerialPort);
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
