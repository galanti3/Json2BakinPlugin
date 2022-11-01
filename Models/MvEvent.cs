using System.Collections.Generic;

namespace Json2BakinPlugin.Models
{
    //They are all defined in RPG Maker MV

    public class MvEvent
    {
        public int id { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public List<MvEventPage> pages { get; set; }
    }

    public class MvEventMoveRoute
    {
        public bool repeat { get; set; }
        public bool skippable { get; set; }
        public bool wait { get; set; }
        public List<MvCode> list { get; set; }
    }

    public class MvEventImage
    {
        public int characterIndex { get; set; }
        public string characterName { get; set; }
        public int direction { get; set; }
        public int pattern { get; set; }
        public int tileId { get; set; }
    }

    public class MvEventConditions
    {
        public int actorId { get; set; }
        public bool actorValid { get; set; }
        public int itemId { get; set; }
        public bool itemValid { get; set; }
        public bool selfSwitchValid { get; set; }
        public int switch1Id { get; set; }
        public bool switch1Valid { get; set; }
        public int switch2Id { get; set; }
        public bool switch2Valid { get; set; }
        public int variableId { get; set; }
        public bool variableValid { get; set; }
        public int variableValue { get; set; }
    }
}