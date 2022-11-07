using System.Collections.Generic;

namespace Json2BakinPlugin.Models
{
	public class MvEventMoveRoute
	{
		public bool repeat { get; set; }
		public bool skippable { get; set; }
		public bool wait { get; set; }
		public List<MvCode> list { get; set; }
	}

	public class MvEventMoveRouteHeader
	{
		public bool repeat { get; set; }
		public bool skippable { get; set; }
		public bool wait { get; set; }
		public string list { get; set; }
	}
}
