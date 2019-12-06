using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShipSpawner : EnemySpawner
{
    protected override void Update()
    {
        if (enabledSpawner)
        {
            effect_time += Time.deltaTime;

            elapsed += Time.deltaTime;
        }
    }
    
}
