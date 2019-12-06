using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerParent : MonoBehaviour
{
    public bool waveEnabled = false;
    public EnemySpawner[] Spawners;
    private WaveData waveInfo;
    public int enemiesSpawned = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        Spawners = GetComponentsInChildren<EnemySpawner>();
        foreach(EnemySpawner spawner in Spawners)
        {
            spawner.parent = this;
        }
    }

    private void Update()
    {
        if (!waveEnabled)
            return;

        if (enemiesSpawned >= waveInfo.EnemyCount)
        {
            bool waveEnd = true;

            foreach(EnemySpawner spawner in Spawners)
            {
                waveEnd &= spawner.getEndOfWave();
            }

            if (waveEnd)
            {
                DisableSpawners();
                WaveController.Instance.NextWave();
            }
        } else
        {
            foreach(EnemySpawner spawner in Spawners)
            {
                if(enemiesSpawned >= waveInfo.EnemyCount)
                {
                    return;
                }
                if(!spawner.curr_enemy)
                {
                    Debug.Log("Spawning enemy");
                    if(waveInfo.EnemiesToSpawn.Length > enemiesSpawned)
                        spawner.spawn(waveInfo.EnemiesToSpawn[enemiesSpawned].GetEnemyType());
                    else
                        spawner.spawn();

                    enemiesSpawned++;
                }
            }
        }

    }

    public void EnableSpawners(WaveData waveData)
    {
        waveInfo = waveData;
        ToggleSpawners();
    }

    public void DisableSpawners()
    {
        waveInfo = null;
        ToggleSpawners();
    }

    public bool ToggleSpawners()
    {
        return (waveEnabled = !waveEnabled);
    }
}
