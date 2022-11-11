using System.Collections.Generic;

namespace Json2BakinPlugin.Models
{
	public class MvEventMoveRoute
	{
        #region Properties
        public bool repeat { get; set; }
		public bool skippable { get; set; }
		public bool wait { get; set; }
		public List<MvCode> list { get; set; }
		#endregion
	}

	public class MvEventMoveRouteHeader
	{
        #region Properties
        public bool repeat { get; set; }
		public bool skippable { get; set; }
		public bool wait { get; set; }
		public string list { get; set; }
		#endregion
	}
}
