using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHotkeys : MonoBehaviour
{
    AbilityBehaviour currentAbility = new AbilityBehaviour();

    private void OnEnable()
    {
        UserInputs.Instance._note1.performed += OnNote1;
        UserInputs.Instance._note2.performed += OnNote2;
        UserInputs.Instance._note3.performed += OnNote3;
        UserInputs.Instance._note4.performed += OnNote4;
    }

    private void OnDisable()
    {
        UserInputs.Instance._note1.performed -= OnNote1;
        UserInputs.Instance._note2.performed -= OnNote2;
        UserInputs.Instance._note3.performed -= OnNote3;
        UserInputs.Instance._note4.performed -= OnNote4;
    }

    public void OnNote1(InputAction.CallbackContext context)
    {
        print("test");
        currentAbility.Activate(AbilityBehaviour.SpellType.ACTIVATE, AbilityBehaviour.SpellTarget.ROOTED_OBJECTS, AbilityBehaviour.SpellProperties.SINGLE);
    }
    public void OnNote2(InputAction.CallbackContext obj) { currentAbility.Activate(AbilityBehaviour.SpellType.LIGHT, AbilityBehaviour.SpellTarget.CORRUPTION, AbilityBehaviour.SpellProperties.SINGLE); }
    public void OnNote3(InputAction.CallbackContext obj) { currentAbility.Activate(AbilityBehaviour.SpellType.MIND_CONTROL, AbilityBehaviour.SpellTarget.CORRUPTION, AbilityBehaviour.SpellProperties.SINGLE); }
    public void OnNote4(InputAction.CallbackContext obj) { currentAbility.Activate(AbilityBehaviour.SpellType.PUSH, AbilityBehaviour.SpellTarget.CORRUPTION, AbilityBehaviour.SpellProperties.SINGLE); }

}
