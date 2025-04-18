using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SpartaDungeon.GamePlayer;

namespace SpartaDungeon.GameDungeon
{
    public class Dungeon
    {
        public DungeonType Level { get; private set; }
        private int _recommendedDefense;
        private int _baseReward;
        public Dungeon(DungeonType level) {
            Level = level;
            switch (level)
            {
                case DungeonType.Easy:
                    _recommendedDefense = 5;
                    _baseReward = 1000;
                    break;
                case DungeonType.Normal:
                    _recommendedDefense = 11;
                    _baseReward = 1700;
                    break;
                case DungeonType.Hard:
                    _recommendedDefense = 17;
                    _baseReward = 2500;
                    break;
            }
        }

        public DungeonResult CheckClearCondition(Player player)
        {
            DungeonResult result = new DungeonResult();
            result.level = Level;
            result.previousHealth = player.GetCurrentHealth();
            result.previousGold = player.GetCurrentGold();

            if (player.GetTotalDefense() < _recommendedDefense)
            {
                if(Random.Shared.Next(0, 100) < 40)
                {
                    result.isClear = false;
                    result.HealthLost = player.GetTotalHealth() / 2;
                    result.reward = 0;
                    return result;
                }
            }

                result.isClear = true;
                result.HealthLost = Random.Shared.Next(20, 35 + 1) + (_recommendedDefense - player.GetTotalDefense());
                result.reward = (int)(_baseReward * (1 + player.GetTotalAttack() / 100));
                return result;
        }

        public DungeonResult Enter(Player player)
        {
            return CheckClearCondition(player);
        }
    }

    public enum DungeonType { 


        [Description("쉬운 던전")]
        Easy,
        [Description("일반 던전")]
        Normal,
        [Description("어려운 던전")]
        Hard
    }

    public class DungeonResult()
    {
        public DungeonType level;
        public int reward;
        public bool isClear;
        public int previousGold;
        public int previousHealth;
        public int HealthLost;
    }
}
