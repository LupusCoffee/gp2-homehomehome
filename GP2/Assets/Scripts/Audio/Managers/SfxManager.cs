//Created by Mohammed (the sex god)

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SfxManager : MonoBehaviour //shoulda inherited from something together with music manager but meh
{
    public static SfxManager Instance;

    [Header("SFX: One-Shots")]
    [SerializeField] private List<AK.Wwise.Event> oneShot2dEvents;
    [SerializeField] private List<AK.Wwise.Event> oneShot3dEvents;
    Dictionary<string, AK.Wwise.Event> sfxDictionary = new Dictionary<string, AK.Wwise.Event>();


    private void Awake()
    {
        Instance = this;
        SetDictionary();

        //gotta find a way to just get all the events straight into the dictionary without the above lists
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneExited;
    }
    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneExited;
    }

    private void SetDictionary()
    {
        foreach (var _event in oneShot2dEvents) sfxDictionary.Add(_event.Name, _event);
        foreach (var _event in oneShot3dEvents) sfxDictionary.Add(_event.Name, _event);
    }
    public void PostEvent(string eventName)
    {
        AK.Wwise.Event _event;

        if(sfxDictionary.TryGetValue(eventName, out _event)) _event.Post(gameObject);
    }
    public void PostEvent(string eventName, GameObject objectToPlayOn)
    {
        AK.Wwise.Event _event;

        if (sfxDictionary.TryGetValue(eventName, out _event)) _event.Post(objectToPlayOn);
    }
    private void OnSceneExited(Scene scene)
    {
        //TO-DO: fade out any sound from this scene that is still playing and then completely stop everything
    }

    //Note:
    //- Cutscenes simply take sfx, music, and state setters from wwise as events.
    //- At the start of a cutscene, we set the AudioState to "cutscene", so that only the cutscene audio is heard (and everything else is quiet)
    //- At the end of a cutscene, we set the AudioState back to "gameplay", so gameplay can be heard
}
