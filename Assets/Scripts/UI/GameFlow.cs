using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{

    public static float currentHP {
        get
        {
            return PlayerStats.Instance.HP;
        }
    }
    public static float maxHP = 1000;

    public readonly Color HEALTHY_COLOR = new Color((92f / 255f), (230f / 255f), (237f / 255f), 1f); //blue
    public readonly Color LOW_COLOR = new Color((250f / 255f), (168f / 255f), (37f / 255f), 1f); //yellow
    public readonly Color CRITICAL_COLOR = new Color((245f / 255f), (44f / 255f), (22f / 255f), 1f); //red

    public Transform healthBar;
    public Transform healthPoints;

    public static float currentScore
    {
        get
        {
            return PlayerStats.Instance.Score;
        }
    }

    public const float scoreIncrement = 1000;

    public Transform score;
    public Transform pauseMenuScore;

    void Start()
    {
        //Initialize bar to green color
        healthBar.GetComponent<Image>().color = HEALTHY_COLOR;
    }

    void Update()
    {
        //Updates color of health bar
        if (currentHP / maxHP < 0.25f) //25% health
        {
            healthBar.GetComponent<Image>().color = CRITICAL_COLOR;
        }
        else if (currentHP / maxHP < 0.5f) //50% health
        {
            healthBar.GetComponent<Image>().color = LOW_COLOR;
        }
        else // > 50% health
        {
            healthBar.GetComponent<Image>().color = HEALTHY_COLOR;
        }

        //Updates size of health bar
        healthBar.GetComponent<RectTransform>().localScale = new Vector3(currentHP / maxHP, 1, 1);

        //Updates score value
        score.GetComponent<Text>().text = currentScore.ToString();
        pauseMenuScore.GetComponent<Text>().text = currentScore.ToString();
        healthPoints.GetComponent<Text>().text = currentHP + " / 1000";
    }
}
