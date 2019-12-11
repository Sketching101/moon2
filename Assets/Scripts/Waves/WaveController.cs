using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveController : MonoBehaviour
{
    public WaveData[] WaveArr;
    public SpawnerParent[] WaveSpawnerParents;
    [SerializeField]
    private int CurrentWave;

    public static WaveController Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        CurrentWave = -1;

        if (WaveArr.Length > 0)
        {
            NextWave();
        } 

        if (WaveArr.Length != WaveSpawnerParents.Length)
            Debug.LogError("ERROR: Wave size isn't the same as wave spawner parent size");
    }

    public void NextWave()
    {
        CurrentWave++;

        if (CurrentWave >= WaveArr.Length)
        {
            if (CurrentWave == WaveArr.Length && NextLevelMenu.Instance != null)
            {
                NextLevelMenu.Instance.Pause();
            }

            return;
        }


        WaveSpawnerParents[CurrentWave].EnableSpawners(WaveArr[CurrentWave]);
    }
    
    public void CreateWaveData(string name)
    {
#if UNITY_EDITOR
        WaveData asset = ScriptableObject.CreateInstance<WaveData>();
        name = name.Replace(" ", "_");
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Waves/" + name + ".asset");
        AssetDatabase.SaveAssets();
#endif
    }
}
