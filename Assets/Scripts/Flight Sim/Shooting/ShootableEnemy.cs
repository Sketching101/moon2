using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableEnemy : Shootable
{
    public Enemy enemyObject;

    protected override void Awake()
    {
        base.Awake();
        if(enemyObject == null)
        {
            enemyObject = GetComponent<Enemy>();
        }
    }

    public override void OnShot(GameObject other)
    {
        base.OnShot(other);
    }

    public override void ToggleHighlight(bool toggle)
    {
        if (enemyObject.alive)
        {
            base.ToggleHighlight(toggle);
        } else
        {
            base.ToggleHighlight(false);
        }

    }
}
