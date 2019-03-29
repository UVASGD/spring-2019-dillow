﻿using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public struct SaveData {
	//TODO: Fungus data

	public Vector3 playerSpawnLocation;
	public string currentScene;
	public string targetScene;

	//public Dictionary<int, bool> obtainedCollectibles;
	//public Dictionary<CollectibleType, int> collectiblesCount;
	//public Dictionary<int, bool> abilities;
	public List<string> obtainedCollectibles;
	public int[] collectibleCounts;
	public List<int> abilities;
	public List<GameObject> inventory;

	public SaveData (Vector3 playerSpawnLocation, string currentScene, string targetScene, 
	                 List<string> obtainedCollectibles, int[] collectibleCounts,
					 List<int> abilities, List<GameObject> inventory) {
		this.playerSpawnLocation = playerSpawnLocation;
		this.currentScene = currentScene;
		this.targetScene = targetScene;
		this.obtainedCollectibles = obtainedCollectibles;
		this.collectibleCounts = collectibleCounts;
		this.abilities = abilities;
		this.inventory = inventory;
	}
}

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	private static string dataSubpath = "/data.json";

	public static GameObject player;

	public static SaveData saveData;
	public static HashSet<string> obtainedCollectibles;
    public static Dictionary<CollectibleType, int> collectibleCounts;

    private void Awake () {
		if (null == instance) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}

        obtainedCollectibles = new HashSet<string>();
        collectibleCounts = new Dictionary<CollectibleType, int>();

        player = GameObject.FindWithTag("Player");
		Load();
	}


	public static void Save () {
        //print("Saving!");
        SaveData saveData = new SaveData();
		saveData.obtainedCollectibles = new List<string>(obtainedCollectibles);
        int N = Enum.GetValues(typeof(CollectibleType)).Cast<int>().Max() + 1;
        saveData.collectibleCounts = new int[N];
        foreach (var item in collectibleCounts)
        {
            saveData.collectibleCounts[(int)item.Key]++;
        }

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
            obtainedCollectibles = new HashSet<string>(saveData.obtainedCollectibles);
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
    }

    public static bool HasCollectible(Collectible collectible)
    {
        return obtainedCollectibles.Contains(collectible.id);
    }
}
