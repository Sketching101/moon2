using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    private GameObject curr_enemy;
    private int curr_enemy_value;
    public GameObject interceptor;
    public GameObject drone;
    public GameObject turret;
    public Transform target;
    public float elapsed = 0.0f;
    public GameObject entrance_effect;
    public GameObject curr_effect;
    public float effect_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target);
        elapsed = Time.deltaTime;
        effect_time = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawn();

        effect_time += Time.deltaTime;
        //if (effect_time >= 5.0f && curr_effect) {
        //    curr_effect.SetActive(false);
        //}


        elapsed += Time.deltaTime;

    }

    void spawn()
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

    public void shipDeadSig()
    {

    }

    void check_death() {
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
