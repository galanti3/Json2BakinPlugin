namespace Json2BakinPlugin.Models
{
    public class MvEventPage : MvCodeList
    {
        #region Properties
        public MvEventConditions conditions { get; set; }
        public bool directionFix { get; set; }
        public MvEventImage image { get; set; }
        public int moveFrequency { get; set; }
        public MvEventMoveRoute moveRoute { get; set; }
        public int moveSpeed { get; set; }
        public int moveType { get; set; }
        public int priorityType { get; set; }
        public bool stepAnime { get; set; }
        public bool through { get; set; }
        public bool walkAnime { get; set; }

        #endregion

        #region Methods
        public string TriggerCode
        {
            get
            {
                switch (trigger)
                {
                    case 1:
                        return "HIT";
                    case 2:
                        return "HIT_FROM_EV";
                    case 3:
                        return "AUTO_REPEAT";
                    case 4:
                        return "PARALLEL";
                    default:
                        return "TALK";
                }
            }
        }
        #endregion

    }
}
