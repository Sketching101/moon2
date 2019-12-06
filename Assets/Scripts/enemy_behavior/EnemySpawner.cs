using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject curr_enemy;
    protected int curr_enemy_value;

    [Header("Prefabs")]
    public GameObject interceptor;
    public GameObject drone;
    public GameObject turret;
    [Header("Player Transform")]
    public Transform target;
    [Header("Parent Spawner Manager")]
    public SpawnerParent parent;
    [Header("Misc")]
    public float elapsed = 0.0f;
    public GameObject entrance_effect;
    public GameObject curr_effect;
    public float effect_time = 0.0f;
    public bool enabledSpawner { get { return (parent != null && parent.waveEnabled); } }

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target);
        elapsed = Time.deltaTime;
        effect_time = Time.deltaTime;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (enabledSpawner)
        {
            spawn();
            effect_time += Time.deltaTime;

            elapsed += Time.deltaTime;
        }
    }

    public virtual void spawn(int enemy_gen)
    {
        if (!curr_enemy)
        {
            if (entrance_effect != null)
            {
                entrance_effect.GetComponent<PlayParticleEffectOnce>().PlayParticleSys();
                entrance_effect.transform.position = transform.position;
            }

            if (enemy_gen == 0)
            {
                curr_enemy = Instantiate(interceptor) as GameObject;
                curr_enemy.GetComponent<enemy_ship_ai>().set_target(target);
                curr_enemy_value = 0;
            }

            else if (enemy_gen == 1)
            {
                curr_enemy = Instantiate(drone) as GameObject;
                curr_enemy.GetComponent<enemy_drone_ai>().set_target(target);
                curr_enemy_value = 1;
            }
            else if (enemy_gen == 2)
            {
                curr_enemy = Instantiate(turret) as GameObject;
                curr_enemy.GetComponent<enemy_turret_ai>().set_target(target);
                curr_enemy.GetComponent<enemy_turret_ai>().set_values(target);
                curr_enemy_value = 2;
            }
            curr_enemy.transform.position = transform.position;
        }
    }

    public virtual void spawn()
    {
        if (!curr_enemy) {
            int enemy_gen = Random.Range(0, 3);
            //curr_effect.SetActive(true);
            if (entrance_effect != null)
            {
                entrance_effect.GetComponent<PlayParticleEffectOnce>().PlayParticleSys();
                entrance_effect.transform.position = transform.position;
            }

            if (enemy_gen == 0)
            {
                curr_enemy = Instantiate(interceptor) as GameObject;
                curr_enemy.GetComponent<enemy_ship_ai>().set_target(target);
                curr_enemy_value = 0;
            }

            else if (enemy_gen == 1)
            {
                curr_enemy = Instantiate(drone) as GameObject;
                curr_enemy.GetComponent<enemy_drone_ai>().set_target(target);
                curr_enemy_value = 1;
            }
            else if (enemy_gen == 2)
            {
                curr_enemy = Instantiate(turret) as GameObject;
                curr_enemy.GetComponent<enemy_turret_ai>().set_target(target);
                curr_enemy.GetComponent<enemy_turret_ai>().set_values(target);
                curr_enemy_value = 2;
            }
            curr_enemy.transform.position = transform.position;
        }

    }

    // Returns "current enemy is dead" as true or false
    public bool getEndOfWave()
    {
        return !curr_enemy;
    }

    protected void check_death() {
        if (curr_enemy_value == 0)
        {
            if (curr_enemy.GetComponent<enemy_ship_ai>().get_alive()) 
            {
                curr_enemy = null;
                effect_time = 0.0f;
            }
        }
        else if (curr_enemy_value == 1)
        {
            if (curr_enemy.GetComponent<enemy_drone_ai>().get_alive())
            {
                curr_enemy = null;
                effect_time = 0.0f;
            }
        }
        else if(curr_enemy_value == 2) 
        {
            if (curr_enemy.GetComponent<enemy_turret_ai>().get_alive())
            {
                curr_enemy = null;
                effect_time = 0.0f;
            }
        }
    }
}
