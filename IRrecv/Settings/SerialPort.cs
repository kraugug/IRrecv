using System.ComponentModel;
using System.Configuration;
using System.IO.Ports;

namespace IRrecv.Settings
{
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class SerialPort : INotifyPropertyChanged
    {
        #region Properties

        public int DataBits { get; set; }

        public Handshake Handshake { get; set; }

        public string Name { get; set; }

        public Parity Parity { get; set; }

        public int Speed { get; set; }

        public StopBits StopBits { get; set; }

        #endregion

        #region Methods

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            foreach(string propertyName in propertyNames)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #endregion

        #region Constructor

        public SerialPort()
        {
            DataBits = 8;
            Handshake = Handshake.XOnXOff;
            Name = null;
            Parity = Parity.None;
            Speed = 9600;
            StopBits = StopBits.One;
        }

        #endregion

        #region Events
        
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
