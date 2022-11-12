namespace Json2BakinPlugin.Models
{
    public class MvEventConditions
    {
        #region Properties
        public int actorId { get; set; }
        public bool actorValid { get; set; }
        public int itemId { get; set; }
        public bool itemValid { get; set; }
        public string selfSwitchCh { get; set; }
        public bool selfSwitchValid { get; set; }
        public int switch1Id { get; set; }
        public bool switch1Valid { get; set; }
        public int switch2Id { get; set; }
        public bool switch2Valid { get; set; }
        public int variableId { get; set; }
        public bool variableValid { get; set; }
        public int variableValue { get; set; }
        #endregion
    }
}
