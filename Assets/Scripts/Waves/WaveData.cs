using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Resources/Waves", order = 1)]
public class WaveData : ScriptableObject
{
    public int EnemyCount;
    public EnemyData[] EnemiesToSpawn;

    public WaveData(int count)
    {
        EnemyCount = count;
        EnemiesToSpawn = new EnemyData[count];
    }
}


[System.Serializable]
public class EnemyData
{
    public string EnemyType;

    public int GetEnemyType()
    {
        if(EnemyType == "Interceptor")
        {
            return 0;
        } else if(EnemyType == "Turret")
        {
            return 1;
        } else if(EnemyType == "Drone")
        {
            return 2;
        } else
        {
            return -1;
        }
    }
}