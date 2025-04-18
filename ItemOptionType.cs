using System.ComponentModel;

namespace SpartaDungeon
{
    [Serializable]
    public enum ItemOptionType
    {
        [Description("공격력")]
        Power,
        [Description("방어력")]
        Defense,
        [Description("체력")]
        Health,
    }
}
