using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json.Serialization;
using SpartaDungeon.Item;

namespace SpartaDungeon.GamePlayer
{
    // 인벤토리 배열로 구현
    [Serializable]
    public class EquipmentSlot
    {
        [JsonInclude]
        private Dictionary<ItemType, BaseItem?> _equipmentSlots;

        public EquipmentSlot()
        {
            _equipmentSlots = new Dictionary<ItemType, BaseItem?>();
            //// 기본 슬롯 초기화
            //foreach (EquipmentSlotType slotType in Enum.GetValues(typeof(EquipmentSlotType)))
            //{
            //    _equipmentSlots[slotType] = null;
            //}
        }

        /// <summary>
        /// 장비 슬롯에 아이템 장착
        /// </summary>
        /// <param name="slotType"></param>
        /// <param name="item"></param>
        /// <returns>이전에 장착한 아이템을 반환합니다.</returns>

        public BaseItem? EquipItem(ItemType slotType, BaseItem item)
        {
            if(item == null)
            {
                Console.WriteLine("잘못된 아이템입니다.");
                return null;
            }

            BaseItem? previousItem = _equipmentSlots.ContainsKey(slotType) ? _equipmentSlots[slotType] : null;
            _equipmentSlots[slotType] = item;
            return previousItem;
        }

        public BaseItem? UnEquipItem(ItemType slotType)
        {
            BaseItem? unEquippedItem = _equipmentSlots[slotType];
            _equipmentSlots[slotType] = null;
            return unEquippedItem;
        }

        public BaseItem? RemoveItem(ItemType slotType)
        {
            if (_equipmentSlots.ContainsKey(slotType))
            {
                BaseItem? item = _equipmentSlots[slotType];
                _equipmentSlots[slotType] = null;
                return item;
            }
            return null;
        }

        // TODO: 변수정 수정
        public BaseItem? GetItemByIndex(ItemType slotType)
        {
            return _equipmentSlots[slotType];
        }

        public List<BaseItem> GetItemList()
        {
            List<BaseItem> items = new List<BaseItem>();
            foreach (var item in _equipmentSlots)
            {
                if (item.Value != null)
                {
                    items.Add(item.Value);
                }
            }
            return items;
        }
        public int GetItemCount()
        {
            int count = 0;
            foreach(var item in _equipmentSlots)
            {
                if(item.Value != null)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
