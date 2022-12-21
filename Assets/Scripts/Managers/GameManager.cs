using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioManager AmbienceAudio;

    private void Start()
    {
        AmbienceAudio.Init();
    }
    private void Awake()
    {
        
    }
    private void Update()
    {
        //AmbienceAudio.PlayLoopedAudio("TownAmbience", true);
        AmbienceAudio.PlayAudio("TownAmbience");
    }
}
