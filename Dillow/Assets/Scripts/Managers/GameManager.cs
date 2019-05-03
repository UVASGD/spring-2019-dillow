using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class SaveData {
    //TODO: Fungus data

    [NonSerialized] public string fileName;
    public Vector3 playerSpawnLocation;
    public string currentScene;
    public string targetScene;
    public int saveIconIndex;

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

public delegate void ManagementEvent();

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    // File stuff
    public static int fileCount;
    public static readonly string SAVE_FOLDER = "/Saves";
    public static readonly string TEST_LOC = "data.json";
    public static string currentSaveFile = TEST_LOC;
    private static string DataSubpath => SAVE_FOLDER + "/" + currentSaveFile;

    [Tooltip("These objects get unloaded when returning to the Main Menu")]
    public List<GameObject> ObjectsToKill;

    [Header("Player Stuff")]
    public GameObject player;
    public Vector3 playerSpawnLocation;
    public static Island current;
    bool spawned;

    [Header("Global UI shit")]
    public DialogueBox dialogue;
    public Animator LoadingScreen;
    public Slider progressSlider;
    public static ManagementEvent StartReturnToMenu, StartSceneChange;
    public static bool loadingFile;

    [Header("Defaults")]
    public Sprite emptyFileSprite;
    public List<Sprite> saveFileSprites;

    public static SaveData saveData;
    public static HashSet<ulong> obtainedCollectibles = new HashSet<ulong>();
    public static Dictionary<CollectibleType, int> collectibleCounts = new Dictionary<CollectibleType, int>();

    public static readonly float bakedLoadTime = 1.5f;
    public static readonly string HorizontalAxis1 = "Horizontal";
    public static readonly string HorizontalAxis2 = "Camera X";
    public static readonly string VerticalAxis1 = "Vertical";
    public static readonly string VerticalAxis2 = "Camera Y";
    private AsyncOperation loadOp;

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

#if UNITY_EDITOR
        // play the island's theme if we are not starting from the menu
        // this shouldn't happen in game time
        if (!SceneManager.GetActiveScene().name.ToLower().Contains("mainmenu")) {
            current = FindObjectOfType<Island>();
            if (current) {
                AudioManager.PlayMusic(current.IdleMusic, fadeDuration: 1f);
            }
        }
#endif

        FadeController.FadeInStartedEvent += delegate { spawned = true; };
        FadeController.FadeOutCompletedEvent += delegate { if (!spawned) StartCoroutine(RespawnCo()); };
        SceneManager.sceneLoaded += OnSceneLoaded;

        EventNames = new EventNames();
    }

    /// <summary>
    /// Set some initial stuff we can only do when the level is fully loaded
    /// </summary>
    public static void OnSceneLoaded(Scene level, LoadSceneMode mode) {
        if (loadingFile) {
            // Loading a game/scene
            HideLoading();

            current = FindObjectOfType<Island>();
            if (current) {
                AudioManager.PlayMusic(current.IdleMusic, fadeDuration: 1f);
            }

            FadeController.instance.FadeIn(1 / 3f);
            if(!instance.player)
                instance.player = GameObject.FindWithTag("Player");
            GameObject spawn = GameObject.FindGameObjectWithTag("Respawn");
            instance.playerSpawnLocation = (spawn) ? spawn.transform.position : 
                instance.player.transform.position;
        } else {
            // Returning to menu
            FadeController.instance.FadeIn(1 / 10f);
        }
    }

    public static IEnumerator Save () {
        yield return null;
        //print("Saving!");
        int N = Enum.GetValues(typeof(CollectibleType)).Cast<int>().Max() + 1;
        int[] saveCollectibleCounts = new int[N];
        foreach (var item in collectibleCounts)
        {
            saveCollectibleCounts[(int)item.Key] = item.Value;
        }
        SaveData saveData = new SaveData(
            instance.playerSpawnLocation,
            SceneManager.GetActiveScene().name,
            null,
            new List<ulong>(obtainedCollectibles),
            saveCollectibleCounts,
            new List<int>()
        );

        string jsonData = JsonUtility.ToJson(saveData);
        string filePath = Application.dataPath + DataSubpath;
        File.WriteAllText(filePath, jsonData);
    }

    /// <summary>
    /// Load data into the stat of the game
    /// </summary>
    /// <returns></returns>
    public static IEnumerator Load () {
        yield return null;
        string filePath = Application.dataPath + DataSubpath;
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

    /// <summary>
    /// Load the next scene
    /// </summary>
    /// <param name="levelName"></param>
    public static void LoadLevel(string levelName) {
        instance.LoadingScreen.SetBool("Open", true);
        loadingFile = true;
        instance.StartCoroutine(AsyncronousLoad(levelName));
    }

    /// <summary>
    /// We want to bake some time into loading so the loading screen doesn't show for
    /// a split second
    /// </summary>
    private static IEnumerator AsyncronousLoad(string levelName) {
        yield return new WaitForSeconds(bakedLoadTime);
        instance.loadOp = SceneManager.LoadSceneAsync(levelName);
        StartSceneChange?.Invoke();

        while (!instance.loadOp.isDone) {
            float progress = Mathf.Clamp01(instance.loadOp.progress / 0.9f);
            Debug.Log("Load Progress: "+ progress);
            if (instance.progressSlider) instance.progressSlider.value = progress;
            yield return null;
        }
    }

    public static void HideLoading() {
        if (instance.LoadingScreen)
            instance.LoadingScreen.SetBool("Open", false);
    }

#if UNITY_EDITOR
    public static void SaveFromEditor() {
        int N = Enum.GetValues(typeof(CollectibleType)).Cast<int>().Max() + 1;
        int[] saveCollectibleCounts = new int[N];
        foreach (var item in collectibleCounts) {
            saveCollectibleCounts[(int)item.Key] = item.Value;
        }

        SaveData data = PreLoad();
        SaveData saveData = new SaveData(
            data.playerSpawnLocation,
            null,
            null,
            new List<ulong>(obtainedCollectibles),
            saveCollectibleCounts,
            new List<int>()
        );

        string jsonData = JsonUtility.ToJson(saveData);
        string filePath = Application.dataPath + SAVE_FOLDER + "/" + TEST_LOC;
        File.WriteAllText(filePath, jsonData);
    }
#endif

    public static SaveData PreLoad() {
        string filePath = Application.dataPath + SAVE_FOLDER + "/" + TEST_LOC;
        return File.Exists(filePath) ? JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath)) 
            : new SaveData(Vector3.zero, "","", null,null, null);
    }

    public static void AddCollectible(Collectible collectible)
    {
        obtainedCollectibles.Add(collectible.id);
        collectibleCounts[collectible.type]++;
        Save();

        switch (collectible.type) {
            case CollectibleType.Gear:
                UIController.instance.ShowGear(collectibleCounts[collectible.type]);
                break;
        }
    }

    public static bool HasCollectible(Collectible collectible)
    {
        return obtainedCollectibles.Contains(collectible.id);
    }

    public static bool RemoveCollectible(Collectible collectible) {
        bool success =  obtainedCollectibles.Remove(collectible.id);
        if(success) collectibleCounts[collectible.type]--;
        return success;
    }

    public void Respawn()
    {
        spawned = false;
        FadeController.instance.FadeOut(0.1f, true);
    }

    IEnumerator RespawnCo()
    {
        player.gameObject.transform.position = playerSpawnLocation;
        DillowController.instance.body.rb.velocity = Vector3.zero;
        print(playerSpawnLocation);
        DillowController.instance.body.Live();
        yield return new WaitForSeconds(1.5f);
        FadeController.instance.FadeIn();
    }

    #region ================= RETURN TO MENU =================

    public static void ReturnToMenu() {
        // dont judge me
        YesNoMaybe("Do you maybe want to save first?", ConfirmReturnToMenuSave, ConfirmReturnToMenu,
            delegate { StartReturnToMenu = null; CloseDialog(); },txtMaybe:"No", txtOK:"Yes");
    }

    public static void ConfirmReturnToMenuSave() {
        // save game first
        if (currentSaveFile.Contains("Temp")) {
            // the player needs to set the save file they want to overwrite
        } else {
            // unload persistant objects that shouldn't be in MM
            foreach (GameObject go in instance.ObjectsToKill)
                Destroy(go);

            instance.StartCoroutine(Save());
            ConfirmReturnToMenu();
        }
    }

    public static void ConfirmReturnToMenu() {
        FadeController.FadeOutCompletedEvent = CommitToMenu;
        FadeController.instance.FadeOut(1 / 6f);
        AudioManager.PlayMusic("", fadeDuration: 3f);
        StartReturnToMenu?.Invoke();
        StartSceneChange?.Invoke();
    }

    /// <summary>
    /// return to the main menu
    /// </summary>
    public static void CommitToMenu() {
        FadeController.FadeOutCompletedEvent -= CommitToMenu;
        loadingFile = false;
        SceneManager.LoadScene("MainMenu");
    }

    #endregion


    #region ================= QUIT =================

    public static void Quit() {
        FadeController.FadeOutCompletedEvent = CommitQuit;
        FadeController.instance.FadeOut(Color.black, 1 / 6f);
        AudioManager.PlayMusic("", fadeDuration: 3f);
    }

    public static void CommitQuit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
        Application.Quit();
#endif
    }

    #endregion


    #region ================= MSF DIALOG BOX =================

    /// <summary>
    /// Show a yes no dialog box
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="ok"></param>
    public static void YesNo(string msg, Action ok, string txtOK="Yes", string txtNO="No") {
        DialogBoxData dat = new DialogBoxData(msg);
        dat.LeftButtonAction = instance.dialogue.CloseDialog;
        dat.LeftButtonText = txtNO;
        dat.RightButtonAction = ok;
        dat.RightButtonText = txtOK;

        instance.dialogue.ShowDialog(dat);
    }

    public static void YesNoMaybe(string msg, Action ok, Action maybe,
        string txtOK = "OK", string txtMaybe = "Um", string txtNO = "Cancel") {
        YesNoMaybe(msg, ok, maybe, CloseDialog, txtOK, txtMaybe, txtNO);
    }

    /// <summary>
    /// Show a yes no maybe dialog box. (basically yes no with an extra option)
    /// </summary>
    public static void YesNoMaybe(string msg, Action ok, Action maybe, Action cancel, 
        string txtOK="OK", string txtMaybe="Um", string txtNO = "Cancel") {
        DialogBoxData dat = new DialogBoxData(msg);
        dat.LeftButtonAction = cancel;
        dat.LeftButtonText = txtNO;

        dat.MiddleButtonAction = maybe;
        dat.MiddleButtonText = txtMaybe;

        dat.RightButtonAction = ok;
        dat.RightButtonText = txtOK;

        instance.dialogue.ShowDialog(dat);
    }

    public static void CloseDialog() {
        instance.dialogue.CloseDialog();
    }

    /// <summary>
    /// Default events channel
    /// </summary>
    public static EventsChannel Events { get; private set; }

    /// <summary>
    /// List of event names, used within the framework
    /// </summary>
    public static EventNames EventNames { get; private set; }
}

/// <summary>
/// List of event names, used within the framework
/// </summary>
public class EventNames {
    public string ShowLoading { get { return "gm.loading"; } }
    public string ShowDialogBox { get { return "gm.showDialog"; } }
}
    #endregion

