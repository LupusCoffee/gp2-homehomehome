using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SO_SpellAttribute : ScriptableObject
{
    [SerializeField] private string attributeName;
    [SerializeField] private Texture attributeSprite;
    [SerializeField] private AK.Wwise.Event musicEvent;
    [SerializeField] private List<SongManager.Notes> typeNoteSequence;
    public string GetName() => attributeName;
    public Texture GetSprite() => attributeSprite;
    public AK.Wwise.Event GetMusicEvent() => musicEvent;
    public List<SongManager.Notes> GetNoteSequence() => typeNoteSequence;
}
