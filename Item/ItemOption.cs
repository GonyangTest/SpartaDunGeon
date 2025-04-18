using System.Collections;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;

namespace SpartaDungeon.Item
{
    [Serializable]
    public class ItemOptions : ICloneable
    {
        public object Clone()
        {
            ItemOptions clone = new ItemOptions();
            foreach (ItemOption option in _itemOptions)
            {
                clone.AddOption(option.Type, option.Value);
            }
            return clone;
        }

        [JsonInclude]
        private List<ItemOption> _itemOptions = new List<ItemOption>();
        public ItemOptions() { }

        public void AddOption(ItemOptionType itemOptionType, float value)
        {
            _itemOptions.Add(new ItemOption(itemOptionType, value));
        }
        public void AddOption(ItemOptionType itemOptionType, int value)
        {
            _itemOptions.Add(new ItemOption(itemOptionType, value));
        }

        public List<ItemOption> Get()
        {
            return _itemOptions;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _itemOptions.Count; i++)
            {
                ItemOption option = _itemOptions[i];
                if(option.Value != 0)
                    sb.Append(option.ToString() + (i == _itemOptions.Count - 1 ? "" : ","));
            }
            return sb.ToString();
        }
    }
    [Serializable]
    public class ItemOption
    {
        [JsonInclude]
        public ItemOptionType Type { get; private set; }
        [JsonInclude]
        public float Value { get; private set; }

        public ItemOption(ItemOptionType type, float value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Utils.GetDescription(Type)} +{Value}";
        }
    }
}
