using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Rendering;
using Unity.Properties;

public class SpellMaker : MonoBehaviour
{
    [SerializeField] bool noteSheetActive = false;
    public bool NoteSheetActive { get => noteSheetActive; }

    private enum SpellProgress { TYPE, PROPERTY, TARGET }
    SpellProgress spellProgress = 0;

    AbilityBehaviour abilityBehaviour = new AbilityBehaviour();

    AbilityBehaviour.SpellType currentType;
    AbilityBehaviour.SpellProperties currentProperty;
    AbilityBehaviour.SpellTarget currentTarget;


    //UI
    List<GameObject> attributeUiButtons = new List<GameObject>();
    List<GameObject> attributeNameSpaces = new List<GameObject>();

    private float _desiredAlpha = 0;
    private float _currentAlpha = 0;


    #region Input Setup
    private void OnEnable()
    {
        UserInputs.Instance._activateNoteSheet.performed += OnToggleNoteSheet;
        UserInputs.Instance._note1.performed += OnAttributeLeft;
        UserInputs.Instance._note2.performed += AttributeDown;
        UserInputs.Instance._note3.performed += AttributeRight;
        UserInputs.Instance._note4.performed += AttributeUp;
    }

    private void OnDisable()
    {
        UserInputs.Instance._activateNoteSheet.performed -= OnToggleNoteSheet;
        UserInputs.Instance._note1.performed -= OnAttributeLeft;
        UserInputs.Instance._note2.performed -= AttributeDown;
        UserInputs.Instance._note3.performed -= AttributeRight;
        UserInputs.Instance._note4.performed -= AttributeUp;
    }
    #endregion


    private void Start()
    {
        foreach (Transform child in UiManager.Instance.GetButtonPromptUI().transform)
            attributeUiButtons.Add(child.gameObject);

        foreach (Transform child in UiManager.Instance.GetAttributeNameSpaceUI().transform)
            attributeNameSpaces.Add(child.gameObject);
    }
    private void Update()
    {
        _currentAlpha = Mathf.MoveTowards(_currentAlpha, _desiredAlpha, 5 * Time.deltaTime);
        UiManager.Instance.GetSpellMakerCanGroup().alpha = _currentAlpha;
    }


    #region Open & Close Input
    private void ToggleNoteSheet()
    {

        if (noteSheetActive)
        {
            Player.Instance.SetIsPlayingMusic(false);
            noteSheetActive = false;
            ResetNoteSheet();

            _desiredAlpha = 0;
            UserInputs.Instance.OnCloseNoteSheet();
        }
        else
        {
            if (Player.Instance.isOutOfBody) return;

            Player.Instance.SetIsPlayingMusic(true);
            noteSheetActive = true;
            DisplayAttributeButtons();

            _desiredAlpha = 1;
            UserInputs.Instance.OnOpenNoteSheet();
        }
    }

    //needed externally for the mind control system
    public void CloseNoteSheet()
    {
        Player.Instance.SetIsPlayingMusic(false);
        noteSheetActive = false;
        ResetNoteSheet();

        _desiredAlpha = 0;
        UserInputs.Instance.OnCloseNoteSheet();
    }

    private void OnToggleNoteSheet(InputAction.CallbackContext obj)
    {
        ToggleNoteSheet();
    }
    #endregion

    #region Spell Inputs
    //there's gooootta be a better way of doing this
    private void OnAttributeLeft(InputAction.CallbackContext obj)
    {
        ChooseAttributeAt(0);
    }
    private void AttributeDown(InputAction.CallbackContext obj)
    {
        ChooseAttributeAt(1);
    }
    private void AttributeRight(InputAction.CallbackContext obj)
    {
        ChooseAttributeAt(2);
    }
    private void AttributeUp(InputAction.CallbackContext obj)
    {
        ChooseAttributeAt(3);
    }
    #endregion


    #region Other Functions
    private void ChooseAttributeAt(int buttonIndex)
    {
        if (!noteSheetActive) return;

        switch (spellProgress)
        {
            case SpellProgress.TYPE:
                SO_SpellAtt_Type typeAtt = SongManager.Instance.GetTypeCross().GetAttributeAt(buttonIndex);
                if (typeAtt == null) return;

                currentType = typeAtt.GetSpellType();
                DisplayAttributeName((int)spellProgress, typeAtt.GetName());
                StartCoroutine(PlayAttributeAudio(typeAtt.GetMusicEvent()));
                break;

            case SpellProgress.PROPERTY:
                SO_SpellAtt_Property propertyAtt = SongManager.Instance.GetPropertyCross().GetAttributeAt(buttonIndex);
                if (propertyAtt == null) return;

                currentProperty = propertyAtt.GetSpellProperty();
                DisplayAttributeName((int)spellProgress, propertyAtt.GetName());
                StartCoroutine(PlayAttributeAudio(propertyAtt.GetMusicEvent()));
                break;

            case SpellProgress.TARGET:
                SO_SpellAtt_Target targetAtt = SongManager.Instance.GetTargetCross().GetAttributeAt(buttonIndex);
                if (targetAtt == null) return;

                currentTarget = targetAtt.GetSpellTarget();
                DisplayAttributeName((int)spellProgress, targetAtt.GetName());
                StartCoroutine(PlayAttributeAudio(targetAtt.GetMusicEvent()));

                StartCoroutine(OnActivateAbility(currentType, currentProperty, currentTarget));
                return;
        }

        spellProgress++;
        DisplayAttributeButtons();
    }
    private IEnumerator OnActivateAbility(AbilityBehaviour.SpellType spellType, AbilityBehaviour.SpellProperties spellProperty, AbilityBehaviour.SpellTarget spellTarget)
    {
        abilityBehaviour.Activate(spellType, spellTarget, spellProperty);
        Player.Instance.PlaySpellParticles();

        yield return new WaitForSeconds(0.5f);

        //should not be able to press any music stuff from here...

        ToggleNoteSheet();

        //... till here

        yield break;
    }
    private void ResetNoteSheet()
    {
        spellProgress = 0;

        //Stupid ui
        foreach (var nameSpace in attributeNameSpaces)
            nameSpace.SetActive(false);
    }

    private IEnumerator PlayAttributeAudio(AK.Wwise.Event _event)
    {
        SfxManager.Instance.PostEvent("ChooseAttribute");

        yield return new WaitForSeconds(0);

        MusicManager.Instance.PostEvent(_event);

        yield break;
    }
    private IEnumerator PlayAttributeAudio(float delay, AK.Wwise.Event _event)
    {
        SfxManager.Instance.PostEvent("ChooseAttribute");

        yield return new WaitForSeconds(delay);

        MusicManager.Instance.PostEvent(_event);

        yield break;
    }

    //UI shit
    private void DisplayAttributeButtons()
    {
        switch (spellProgress)
        {
            case SpellProgress.TYPE:
                for (int i = 0; i < attributeUiButtons.Count; i++)
                    SetAttributeButtonVisuals(i, SongManager.Instance.GetTypeCross().GetAttributeAt(i));
                break;

            case SpellProgress.PROPERTY:
                for (int i = 0; i < attributeUiButtons.Count; i++)
                    SetAttributeButtonVisuals(i, SongManager.Instance.GetPropertyCross().GetAttributeAt(i));
                break;

            case SpellProgress.TARGET:
                for (int i = 0; i < attributeUiButtons.Count; i++)
                    SetAttributeButtonVisuals(i, SongManager.Instance.GetTargetCross().GetAttributeAt(i));
                break;
        }
    }
    private void SetAttributeButtonVisuals(int index, SO_SpellAttribute attribute)
    {
        Texture texture;

        if (attribute == null) texture = UiManager.Instance.GetDefaultButtonSprite();
        else texture = attribute.GetSprite();

        attributeUiButtons[index].GetComponent<RawImage>().texture = texture;
    }
    private void DisplayAttributeName(int index, string name)
    {
        attributeNameSpaces[index].GetComponent<TextMeshProUGUI>().text = name;
        attributeNameSpaces[index].SetActive(true);
    }
    #endregion
}
