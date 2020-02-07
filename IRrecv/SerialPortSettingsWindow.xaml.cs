using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IRrecv
{
    /// <summary>
    /// Interaction logic for SerialPortSettingsWindow.xaml
    /// </summary>
    public partial class SerialPortSettingsWindow : Window, INotifyPropertyChanged
    {
        #region Properties

        public List<string> AvailableSerialPorts { get => System.IO.Ports.SerialPort.GetPortNames().ToList(); }

        public string SelectedSerialPort
        {
            get => Properties.Settings.Default.SerialPort.Name;
            set
            {
                Properties.Settings.Default.SerialPort.Name = value;
                OnPropertyChanged(nameof(SelectedSerialPort));
            }
        }

        #endregion

        #region Methods

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Methods

        protected override void OnClosed(EventArgs e)
        {
            IRrecv.Properties.Settings.Default.Save();
            base.OnClosed(e);
        }

        #endregion

        #region Constructor

        public SerialPortSettingsWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
