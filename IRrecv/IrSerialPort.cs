using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRrecv
{
	public class IrSerialPort : INotifyPropertyChanged
	{
		#region Properties

		public bool IsClose { get => Port == null ? true : !Port.IsOpen; }

		public bool IsOpen { get => Port == null ? false : Port.IsOpen; }

		private SerialPort Port { get; set; }

		#endregion

		#region Methods

		public void Close()
		{
			Port?.Close();
			Port?.Dispose();
			OnPropertyChanged(nameof(IsClose), nameof(IsOpen));
		}

		protected void OnPropertyChanged(params string[] propertyNames)
		{
			foreach (string propertyName in propertyNames)
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Open(Settings.SerialPort settings)
        {
            Port = new SerialPort(settings.Name, settings.Speed, settings.Parity, settings.DataBits, settings.StopBits) { Handshake = settings.Handshake };
            Port.DataReceived += Port_DataReceived;
            Port.Open();
            OnPropertyChanged(nameof(IsClose), nameof(IsOpen));
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e) => DataReceived?.Invoke(sender, e);

		#endregion

		#region Constructor

		public IrSerialPort()
		{
		}

		#endregion

		#region Events

		public event SerialDataReceivedEventHandler DataReceived;

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
