using UnityEngine;

[CreateAssetMenu(fileName = "SO_Type_", menuName = "Scriptable Objects/SpellAttribute/SO_SpellAtt_Type")]
public class SO_SpellAtt_Type : SO_SpellAttribute
{
    [SerializeField] private AbilityBehaviour.SpellType spellType;
    public AbilityBehaviour.SpellType GetSpellType() => spellType;
}
