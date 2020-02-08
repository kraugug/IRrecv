using System;
using System.Collections.Generic;
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
	/// Interaction logic for IrRemoteProfilesWindow.xaml
	/// </summary>
	public partial class IrRemoteProfilesWindow : Window
	{
		#region Properties

		

		#endregion

		#region Constructor

		public IrRemoteProfilesWindow()
		{
			DataContext = this;
			InitializeComponent();
		}

		#endregion
	}
}
