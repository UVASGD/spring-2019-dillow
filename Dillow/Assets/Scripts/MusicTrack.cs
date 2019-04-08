using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicCategory {
	None,
	Idle,
	Combat,
	Cutscene,
	Death,
	Title,
}

public enum Island {
	None,
	Initial,
	SnowMonkey,
}

[CreateAssetMenu(menuName = "Audio/Music Track", order = 1)]
public class MusicTrack : ScriptableObject {
	public new string name;
	public AudioClip clip;
	public float tempo = 120f;
	public int timeSignature = 4; // timeSignature/4 time.
	public float introMeasures;
	public MusicCategory category;
	public Island island;
}
