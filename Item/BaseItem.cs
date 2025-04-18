using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpartaDungeon.Item
{
    [Serializable]
    public enum ItemType
    {
        Weapon,
        Armor,
        Ring
    };

    [Serializable]
    public class BaseItem : ICloneable
    {
        // 아이템 기본 정보
        [JsonInclude]
        protected int _id;
        [JsonInclude]
        protected string _name;
        [JsonInclude]
        protected string _description;
        [JsonInclude]
        protected int _price;
        [JsonInclude]
        protected int _isEquippable;
        [JsonInclude]
        protected ItemType _itemType;
        [JsonInclude]
        protected ItemOptions _options;

        // 옵션
        [JsonInclude]
        protected int _power;
        [JsonInclude]
        protected int _defense;

        public int Id { get { return _id; } set { _id = value; } }
        public int Power { get { return _power; } set { _power = value; } }
        public int Defense { get { return _defense; } set { _defense = value; } }
        public int Price { get { return _price; } }
        public ItemType ItemType { get { return _itemType; } }

        public ItemOptions Options {
            get { return _options; }
        }

        public object Clone()
        {
            BaseItem clone = (BaseItem)this.MemberwiseClone();
            clone._options = (ItemOptions)_options.Clone();

            return clone;
        }

        public string GetOptionAsString() {
            return _options.ToString();
        }

        public BaseItem(int id, string name, string description, int value, ItemOptions options)
        {
            _id = id;
            _name = name;
            _description = description;
            _price = value;
            _options = options;
        }
        
        public override string ToString()
        {
            return $"{_name} | {GetOptionAsString()} | {_description}";
        }
    }
}
