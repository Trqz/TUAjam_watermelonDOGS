using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static bool created;
    private int prevSceneNum;
    [SerializeField] EventReference levelMusic;
    public EventInstance levelMusicI;
    [SerializeField] EventReference ambienceSFX;
    EventInstance ambienceI;
    [SerializeField] EventReference menuMusic;
    EventInstance menuMusicI;
    

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
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneNum = scene.buildIndex;
        Debug.Log(sceneNum);

        if (sceneNum <= 1)
        {
            if (!IsPlaying(menuMusicI))
            {
                FMOD.Studio.Bus playerBus = RuntimeManager.GetBus("bus:/BGM");
                playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                menuMusicI = RuntimeManager.CreateInstance(menuMusic);
                menuMusicI.start();
            }
        } 
        else
        {
            if(!IsPlaying(levelMusicI))
            {
                FMOD.Studio.Bus playerBus = RuntimeManager.GetBus("bus:/BGM");
                playerBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                levelMusicI = RuntimeManager.CreateInstance(levelMusic);
                levelMusicI.start();
            }
            if(!IsPlaying(ambienceI))
            {
                ambienceI = RuntimeManager.CreateInstance(ambienceSFX);
                RuntimeManager.AttachInstanceToGameObject(ambienceI, GameObject.FindGameObjectWithTag("Player").transform);
                ambienceI.start();
            }
        }
    }

       

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Update is called once per frame
    private void OnDisable()
    {
        levelMusicI.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        levelMusicI.release();
        ambienceI.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambienceI.release();
        menuMusicI.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        menuMusicI.release();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
