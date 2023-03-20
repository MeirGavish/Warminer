using HOG.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOG.GameLogic
{
    class HOGBattleManager
    {
        int EnemiesSpawned;
        int CurrWaveNumber;
    }

    public struct HOGBattlePlayerData
    {
        int Health;
    }

    public class HOGEnemyConfig
    {

    }

    public struct HOGEnemyData
    {
        readonly EnemyTypes EnemyType;
        int Health;
        float Speed;
        int Damage;
        int CoinReward;
    }

    public enum EnemyTypes
    {
        BasicEnemy = 0
    }
}
