﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security;

public struct SaveData {
	//TODO: Fungus data

	public Vector3 playerSpawnLocation;
	public string currentScene;
	public string targetScene;

	//public Dictionary<int, bool> obtainedCollectibles;
	//public Dictionary<CollectibleType, int> collectiblesCount;
	//public Dictionary<int, bool> abilities;
	public List<ulong> obtainedCollectibles;
	public List<int> collectiblesCount;
	public List<int> abilities;
	public List<GameObject> inventory;

	public SaveData (Vector3 playerSpawnLocation, string currentScene, string targetScene, 
	                 List<ulong> obtainedCollectibles, List<int> collectiblesCount,
					 List<int> abilities, List<GameObject> inventory) {
		this.playerSpawnLocation = playerSpawnLocation;
		this.currentScene = currentScene;
		this.targetScene = targetScene;
		this.obtainedCollectibles = obtainedCollectibles;
		this.collectiblesCount = collectiblesCount;
		this.abilities = abilities;
		this.inventory = inventory;
	}
}

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	private static string dataSubpath = "/data.json";

	public GameObject player;
    public Vector3 playerSpawnLocation;
    private float spawn_smooth_time = 0.05f;

	public static SaveData saveData;
	public static Dictionary<ulong, bool> obtainedCollectibles;

	private void Awake () {
		//print("Size: " + sizeof(CollectibleType));
		obtainedCollectibles = new Dictionary<ulong, bool>();

		if (null == instance) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}

		player = GameObject.FindWithTag("Player");
        GameObject spawn = GameObject.FindGameObjectWithTag("Respawn");
        playerSpawnLocation = (spawn) ? spawn.transform.position : player.transform.position;

        Load();
	}


	public static void Save () {
		//print("Saving!");
		saveData.obtainedCollectibles = new List<ulong>();
		foreach (ulong item in obtainedCollectibles.Keys) {
			saveData.obtainedCollectibles.Add(item);
		}
		//saveData.obtainedCollectibles.Add()

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
			saveData = JsonUtility.FromJson<SaveData>(jsonData);

			foreach (ulong item in saveData.obtainedCollectibles) {
				obtainedCollectibles.Add(item, true);
			}
		} else {
			//print("New Game");
			saveData = new SaveData();
			saveData.obtainedCollectibles = new List<ulong>();
			saveData.collectiblesCount = new List<int>();
			for (int i = 0; i < sizeof(CollectibleType); i++) {
				saveData.collectiblesCount.Add(0);
			}
			//saveData.obtainedCollectibles = new List<int>();
			//saveData.collectiblesCount = new List<int>();
			//saveData.abilities = new List<int>();
			//saveData.inventory = new List<GameObject>();
			Save();
		}
	}

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        float vel = 0;
        FadeController.instance.FadeOut(0.1f);
        while (Time.timeScale > 0.01f)
        {
            Time.timeScale = Mathf.SmoothDamp(Time.timeScale, 0, ref vel, spawn_smooth_time);
            yield return null;
        }
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        player.gameObject.transform.position = playerSpawnLocation;
        FadeController.instance.FadeIn();
    }
}
