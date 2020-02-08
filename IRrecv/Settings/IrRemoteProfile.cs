using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRrecv.Settings
{
	[SettingsSerializeAs(SettingsSerializeAs.Xml)]
	public class IrRemoteProfile : INotifyPropertyChanged
	{
		#region Properties

		public IrRemoteBindingCollection Bindings
		{
			get;
			set;
		}

		public string Name
		{
			get => m_Name;
			set
			{
				m_Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		private string m_Name;

		#endregion

		#region Methods

		protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		#endregion

		#region Constructor

		public IrRemoteProfile()
		{
			Bindings = new IrRemoteBindingCollection();
			Bindings.Add(new IrRemoteBinding());
			Bindings.Add(new IrRemoteBinding());
		}

		public IrRemoteProfile(string name) => Name = name;

		#endregion

		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
