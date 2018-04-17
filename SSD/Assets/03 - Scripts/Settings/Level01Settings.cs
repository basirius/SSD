using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level01Settings : MonoBehaviour
{
    public float LevelSpeed;
    public float SpawnInterval;
    public float TunnelSpawnInterval;
    public float TunnelSegmentLength;
    public float SpawnStartDelay;
    public GameObject TunnelSegment;
    public GameObject[] SpawnGameObjects;
    public AudioSource[] LevelMusicArray;
}