using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRrecv.Settings
{
	public enum IrRemoteProtocolType
	{
		Aiwa,
		Denon,
		Dish,

		[Description("JVC (The Japan Victor Company)")]
		Jvc,

		[Description("LG Electronics")]
		Lg,

		[Description("LEGO Power Functions")]
		LegoPf,
		Mitshubishi,

		[Description("NEC (Nippon Electric Company)")]
		Nec,

		[Description("RC-5/6 (Phillips)")]
		Rc5Rc6,
		Samsung,
		Sanyo,
		Sharp,
		Sony,
		Whynter
	}
}
