using System.Collections;
using System.Text.Json.Serialization;
using SpartaDungeon.Item;

namespace SpartaDungeon.GamePlayer
{
    [Serializable]
    public struct PlayerStatus
    {
        [JsonInclude]
        public ClassType ClassType;
        [JsonInclude]
        public int Level;
        [JsonInclude]
        public string Name;
        [JsonInclude]
        public float Attack;
        [JsonInclude]
        public int AttackBonus;
        [JsonInclude]
        public int Defense;
        [JsonInclude]
        public int DefenseBonus;
        [JsonInclude]
        public int MaxHealth;
        [JsonInclude]
        public int Health;
        [JsonInclude]
        public int MaxHealthBonus;
        [JsonInclude]
        public int Gold;
        [JsonInclude]
        public int Exp;
        [JsonInclude]
        public int MaxExp;
    }
    [Serializable]
    public class Player
    {
        [JsonInclude]
        private PlayerStatus _playerStatus;
        [JsonInclude]
        private Inventory _inventory;
        [JsonInclude]
        private EquipmentSlot _equipmentSlot;

        public PlayerStatus PlayerStatus
        {
            get { return _playerStatus; }
            set { _playerStatus = value; }
        }
        public Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public EquipmentSlot EquipmentSlot
        {
            get { return _equipmentSlot; }
            set { _equipmentSlot = value; }
        }

        public Player()
        {
            _inventory = new Inventory();
            _equipmentSlot = new EquipmentSlot();
        }

        public void Init()
        {
            _playerStatus.ClassType = ClassType.Warrior;
            _playerStatus.Level = 1;
            _playerStatus.Name = "스파르타 전사";
            _playerStatus.Attack = 10;
            _playerStatus.Defense = 5;
            _playerStatus.MaxHealth = 100;
            _playerStatus.Health = 100;
            _playerStatus.Gold = 5000;
            _playerStatus.Exp = 0;
            _playerStatus.MaxExp = 1;
        }

        public void EquipItem(int index)
        {
            BaseItem? item = _inventory.RemoveItem(index);
            BaseItem? unrequippedItem = _equipmentSlot.EquipItem(item.ItemType, item);
            if (unrequippedItem != null)
            {
                _inventory.AddItem(unrequippedItem);
            }

            UpdateState();
        }

        public void UnEquipItem(ItemType itemType)
        {
            BaseItem? item = _equipmentSlot.GetItemByIndex(itemType);
            if (item == null) return;

            _equipmentSlot.UnEquipItem(itemType);
            _inventory.AddItem(item);

            UpdateState();
        }

        public void UpdateState() // AddOption으로 변경 필요할듯?
        {
            _playerStatus.AttackBonus = 0;
            _playerStatus.DefenseBonus = 0;
            _playerStatus.MaxHealthBonus = 0;
            List<BaseItem> items = _equipmentSlot.GetItemList();
            foreach (BaseItem item in items) {
                var options = item.Options.Get();
                foreach (var option in options)
                {
                    if (option.Type == ItemOptionType.Power)
                    {
                        _playerStatus.AttackBonus += (int)option.Value;
                    }
                    else if (option.Type == ItemOptionType.Health)
                    {
                        _playerStatus.MaxHealthBonus += (int)option.Value;
                    }
                    else if (option.Type == ItemOptionType.Defense)
                    {
                        _playerStatus.DefenseBonus += (int)option.Value;
                    }
                }
            }

            if(_playerStatus.Health > _playerStatus.MaxHealth + _playerStatus.MaxHealthBonus)
            {
                _playerStatus.Health = _playerStatus.MaxHealth + _playerStatus.MaxHealthBonus;
            }
        }

        public void AddGold(int gold)
        {
            _playerStatus.Gold += gold;
        }


        public bool BuyItem(BaseItem item)
        {
            if (_playerStatus.Gold < item.Price)
            {
                return false;
            }
            _playerStatus.Gold -= item.Price;
            _inventory.AddItem(item);
            return true;
        }
        public void AddHealth(int health)
        {
            _playerStatus.Health += health;

            if (_playerStatus.Health > _playerStatus.MaxHealth + _playerStatus.MaxHealthBonus)
            {
                _playerStatus.Health = _playerStatus.MaxHealth + _playerStatus.MaxHealthBonus;
            }
            else if (_playerStatus.Health < 0)
            {
                _playerStatus.Health = 0;
            }
        }

        public void Recover()
        {
            _playerStatus.Health = _playerStatus.MaxHealth + _playerStatus.MaxHealthBonus;
        }

        public float GetTotalAttack()
        {
            return _playerStatus.Attack + _playerStatus.AttackBonus;
        }
        public int GetTotalDefense()
        {
            return _playerStatus.Defense + _playerStatus.DefenseBonus;
        }
        public int GetCurrentHealth()
        {
            return _playerStatus.Health;
        }

        public int GetTotalHealth()
        {
            return _playerStatus.MaxHealth + _playerStatus.MaxHealthBonus;
        }

        public int GetCurrentGold()
        {
            return _playerStatus.Gold;
        }
        public void AddExp(int exp)
        {
            _playerStatus.Exp += exp;

            if (_playerStatus.Exp >= _playerStatus.MaxExp)
            {
                _playerStatus.Exp -= _playerStatus.MaxExp;
                LevelUp();
            }
        }

        public void LevelUp()
        {
            _playerStatus.Level++;
            _playerStatus.MaxExp += 1;
            _playerStatus.Attack += 0.5f;
            _playerStatus.Defense += 1;
        }

        public string GetStatusAsString()
        {
            UpdateState();

            return $"\n{_playerStatus.Name} ({_playerStatus.ClassType.GetDescription()} )\n" +
                $"레벨 : {_playerStatus.Level:00}\n" +
                $"공격력 : {GetTotalAttack()} (+{_playerStatus.AttackBonus})\n" +
                $"방어력 : {GetTotalDefense()} (+{_playerStatus.DefenseBonus})\n" +
                $"체 력 : {_playerStatus.Health}/{GetTotalHealth()} (+{_playerStatus.MaxHealthBonus})\n" +
                $"Gold : {_playerStatus.Gold}G\n";
        }
    }
}

