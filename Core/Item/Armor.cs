using System;
using System.Text.Json.Serialization;

namespace SpartaDungeon.Core.Item
{
    [Serializable]
    public class Armor : BaseItem
    {
        
        public Armor(int id, string name, string description, int price, ItemOptions options) : base(id, name, description, price, options)
        {
            _itemType = ItemType.Armor;
            _isEquippable = 1;
        }
    }
}
