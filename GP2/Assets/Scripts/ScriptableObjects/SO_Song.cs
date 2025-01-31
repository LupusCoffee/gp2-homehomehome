using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_Song", menuName = "Scriptable Objects/SO_Song")]
public class SO_Song : ScriptableObject
{
    [SerializeField] string songName;
    [SerializeField] AK.Wwise.Event songEvent;

    [SerializeField] AbilityBehaviour.SpellType spellType;
    [SerializeField] List<SongManager.Notes> typeNoteSequence = new List<SongManager.Notes>();

    [SerializeField] AbilityBehaviour.SpellProperties spellProperties;
    [SerializeField] List<SongManager.Notes> propertyNoteSequence = new List<SongManager.Notes>();

    [SerializeField] AbilityBehaviour.SpellTarget spellTarget;
    [SerializeField] List<SongManager.Notes> targetNoteSequence = new List<SongManager.Notes>();

    public string GetName() => songName;
    public AK.Wwise.Event GetSongEvent() => songEvent;
    public List<SongManager.Notes> GetTypeSequence() => typeNoteSequence;
    public List<SongManager.Notes> GetPropertySequence() => propertyNoteSequence;
    public List<SongManager.Notes> GetTargetSequence() => targetNoteSequence;
    public List<SongManager.Notes> GetFullSequence()
    {
        List<SongManager.Notes> returnSequence = new List<SongManager.Notes>();
        returnSequence.AddRange(typeNoteSequence);
        returnSequence.AddRange(propertyNoteSequence);
        returnSequence.AddRange(targetNoteSequence);
        return returnSequence;
    }
    public AbilityBehaviour.SpellType GetSpellType() => spellType;
    public AbilityBehaviour.SpellProperties GetSpellProperty() => spellProperties;
    public AbilityBehaviour.SpellTarget GetSpellTarget() => spellTarget;
}
