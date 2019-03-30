using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public struct SaveData {
    //TODO: Fungus data

    public Vector3 playerSpawnLocation;
    public string currentScene;
    public string targetScene;

    //public Dictionary<int, bool> obtainedCollectibles;
    //public Dictionary<CollectibleType, int> collectiblesCount;
    //public Dictionary<int, bool> abilities;
    public List<ulong> obtainedCollectibles;
    public int[] collectibleCounts;
    public List<int> abilities;

    public SaveData (Vector3 playerSpawnLocation, string currentScene, string targetScene, 
                     List<ulong> obtainedCollectibles, int[] collectibleCounts,
                     List<int> abilities) {
        this.playerSpawnLocation = playerSpawnLocation;
        this.currentScene = currentScene;
        this.targetScene = targetScene;
        this.obtainedCollectibles = obtainedCollectibles;
        this.collectibleCounts = collectibleCounts;
        this.abilities = abilities;
    }
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private static string dataSubpath = "/data.json";

    public GameObject player;
    public Vector3 playerSpawnLocation;
    bool spawned;

    public static SaveData saveData;
    public static HashSet<ulong> obtainedCollectibles = new HashSet<ulong>();
    public static Dictionary<CollectibleType, int> collectibleCounts = new Dictionary<CollectibleType, int>();

    private void Awake () {
        if (null == instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        Load();

        player = GameObject.FindWithTag("Player");
        GameObject spawn = GameObject.FindGameObjectWithTag("Respawn");
        playerSpawnLocation = (spawn) ? spawn.transform.position : player.transform.position;

        FadeController.FadeInStartedEvent += delegate { spawned = true; };
        FadeController.FadeOutCompletedEvent += delegate { if (!spawned) StartCoroutine(RespawnCo()); };
    }

    public static void Save () {
        //print("Saving!");
        int N = Enum.GetValues(typeof(CollectibleType)).Cast<int>().Max() + 1;
        int[] saveCollectibleCounts = new int[N];
        foreach (var item in collectibleCounts)
        {
            saveCollectibleCounts[(int)item.Key] = item.Value;
        }
        SaveData saveData = new SaveData(
            instance.playerSpawnLocation,
            null,
            null,
            new List<ulong>(obtainedCollectibles),
            saveCollectibleCounts,
            new List<int>()
        );

        string jsonData = JsonUtility.ToJson(saveData);
        string filePath = Application.dataPath + dataSubpath;
        File.WriteAllText(filePath, jsonData);
    }

    public static void Load () {
        string filePath = Application.dataPath + dataSubpath;
        //print("Loading to: " + filePath);

        if (File.Exists(filePath)) {
            //print("File found");
            string jsonData = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);
            obtainedCollectibles = new HashSet<ulong>(saveData.obtainedCollectibles);
            collectibleCounts = new Dictionary<CollectibleType, int>();
            for (int i=0; i < saveData.collectibleCounts.Length; i++)
            {
                collectibleCounts[(CollectibleType)i] = saveData.collectibleCounts[i];
            }
        } else {
            //print("New Game");
            Save();
        }
    }

    public static void AddCollectible(Collectible collectible)
    {
        obtainedCollectibles.Add(collectible.id);
        collectibleCounts[collectible.type]++;
        Save();
    }

    public static bool HasCollectible(Collectible collectible)
    {
        return obtainedCollectibles.Contains(collectible.id);
    }

    public void Respawn()
    {
        spawned = false;
        FadeController.instance.FadeOut(0.1f);
    }

    IEnumerator RespawnCo()
    {
        player.gameObject.transform.position = playerSpawnLocation;
        print(playerSpawnLocation);
        DillowController.instance.body.Live();
        yield return new WaitForSeconds(1.5f);
        FadeController.instance.FadeIn();
    }
}
