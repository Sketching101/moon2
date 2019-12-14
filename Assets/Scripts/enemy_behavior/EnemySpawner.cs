using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject currEnemy;
    protected int currEnemyValue;

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
    public GameObject entranceEffect;
    public AudioSource entranceAudio;
    public GameObject currEffect;
    public float effectTime = 0.0f;
    public bool enabledSpawner { get { return (parent != null && parent.waveEnabled); } }

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target);
        elapsed = Time.deltaTime;
        effectTime = Time.deltaTime;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (enabledSpawner)
        {
            spawn();
            effectTime += Time.deltaTime;

            elapsed += Time.deltaTime;
        }
    }

    public virtual void spawn(int enemy_gen)
    {
        if (!currEnemy)
        {
            if (entranceEffect != null)
            {
                entranceEffect.GetComponent<PlayParticleEffectOnce>().PlayParticleSys();
                entranceEffect.transform.position = transform.position;
                entranceAudio.Play();
            }

            if (enemy_gen == 0)
            {
                currEnemy = Instantiate(interceptor) as GameObject;
                currEnemy.GetComponent<enemy_ship_ai>().set_target(target);
                currEnemyValue = 0;
            }

            else if (enemy_gen == 1)
            {
                currEnemy = Instantiate(drone) as GameObject;
                currEnemy.GetComponent<enemy_drone_ai>().set_target(target);
                currEnemyValue = 1;
            }
            else if (enemy_gen == 2)
            {
                currEnemy = Instantiate(turret) as GameObject;
                currEnemy.GetComponent<enemy_turret_ai>().set_target(target);
                currEnemy.GetComponent<enemy_turret_ai>().set_values(target);
                currEnemyValue = 2;
            }
            currEnemy.transform.position = transform.position;
            currEnemy.GetComponent<Enemy>().spawner = this;
        }
    }

    public virtual void spawn()
    {
        if (!currEnemy) {
            int enemy_gen = Random.Range(0, 3);
            //curr_effect.SetActive(true);
            if (entranceEffect != null)
            {
                entranceEffect.GetComponent<PlayParticleEffectOnce>().PlayParticleSys();
                entranceEffect.transform.position = transform.position;
            }

            if (enemy_gen == 0)
            {
                currEnemy = Instantiate(interceptor) as GameObject;
                currEnemy.GetComponent<enemy_ship_ai>().set_target(target);
                currEnemyValue = 0;
            }

            else if (enemy_gen == 1)
            {
                currEnemy = Instantiate(drone) as GameObject;
                currEnemy.GetComponent<enemy_drone_ai>().set_target(target);
                currEnemyValue = 1;
            }
            else if (enemy_gen == 2)
            {
                currEnemy = Instantiate(turret) as GameObject;
                currEnemy.GetComponent<enemy_turret_ai>().set_target(target);
                currEnemy.GetComponent<enemy_turret_ai>().set_values(target);
                currEnemyValue = 2;
            }
            currEnemy.transform.position = transform.position;
        }

    }

    // Returns "current enemy is dead" as true or false
    public bool getEndOfWave()
    {
        return !currEnemy;
    }

    protected void check_death() {
        if (currEnemyValue == 0)
        {
            if (currEnemy.GetComponent<enemy_ship_ai>().get_alive()) 
            {
                currEnemy = null;
                effectTime = 0.0f;
            }
        }
        else if (currEnemyValue == 1)
        {
            if (currEnemy.GetComponent<enemy_drone_ai>().get_alive())
            {
                currEnemy = null;
                effectTime = 0.0f;
            }
        }
        else if(currEnemyValue == 2) 
        {
            if (currEnemy.GetComponent<enemy_turret_ai>().get_alive())
            {
                currEnemy = null;
                effectTime = 0.0f;
            }
        }
    }
}
