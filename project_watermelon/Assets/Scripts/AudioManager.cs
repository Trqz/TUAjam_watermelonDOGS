using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool created;
    [SerializeField] EventReference levelMusic;
    public EventInstance levelMusicI;

    private void Start()
    {
        levelMusicI = RuntimeManager.CreateInstance(levelMusic);
        levelMusicI.start();
    }

    void Awake()
    {
        if (created)
        {
            Destroy(gameObject);
        }
        else
        {
            created = true;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDisable()
    {
        levelMusicI.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        levelMusicI.release();
    }
}
