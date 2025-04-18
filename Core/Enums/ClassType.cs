using System.ComponentModel;
using System.Reflection;

namespace SpartaDungeon.Core.Enums {
    [Serializable]
    public enum ClassType
    {
        [Description("전사")]
        Warrior,
        [Description("마법사")]
        Mage,
        [Description("궁수")]
        Archer,
    }  
}
