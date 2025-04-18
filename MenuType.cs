﻿using System.ComponentModel;
using System.Reflection;

namespace SpartaDungeon
{
    public enum MenuType
    {
        [Description("새 게임 시작")]
        NewGame,
        [Description("게임 불러오기")]
        LoadGame,
        [Description("저장하기")]
        SaveGame,
        [Description("게임 설명")]
        Help,
        [Description("게임 종료")]
        GameExit,
        [Description("인벤토리")]
        Inventory,
        [Description("장비 관리")]
        Equipment,
        [Description("상점")]
        Shop,
        [Description("구입하기")]
        Buy,
        [Description("판매하기")]
        Sell,
        [Description("상태보기")]
        Status,
        [Description("나가기")]
        Exit,
        [Description("휴식하기")]
        Rest,
        [Description("던전 입장")]
        Dungeon,
        [Description("쉬운 던전     | 방어력 5 이상 권장")]
        easy,
        [Description("일반 던전     | 방어력 11 이상 권장")]
        normal,
        [Description("어려운 던전   | 방어력 17 이상 권장")]
        hard,
    }
}
