//Created by Mohammed

using System;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;
using static SO_SpellAttributes;

public class NotePlayer : MonoBehaviour
{
    private List<GameObject> barsUi = new List<GameObject>();
    private List<GameObject> spellProgressTextUi = new List<GameObject>();

    bool noteSheetActive = false;

    AbilityBehaviour currentAbility = new AbilityBehaviour();
    private List<SongManager.Notes> currentSequence = new List<SongManager.Notes>();

    private enum SpellProgress { TYPE, PROPERTY, TARGET }
    SpellProgress spellProgress = 0;
    int seqMin = 0;

    SO_SpellAttributes spellAttributesSO;
    SO_SpellAttributes.TypeAttribute typeAttribute;
    SO_SpellAttributes.PropertyAttribute propertyAttribute;
    SO_SpellAttributes.TargetAttribute targetAttribute;

    private float _desiredAlpha;
    private float _currentAlpha;

    #region Input Setup
    private void OnEnable()
    {
        UserInputs.Instance._activateNoteSheet.performed += OpenNoteSheetInput;
        UserInputs.Instance._activateNoteSheet.canceled += CloseNoteSheetInput;
        UserInputs.Instance._note1.performed += OnNote1;
        UserInputs.Instance._note2.performed += OnNote2;
        UserInputs.Instance._note3.performed += OnNote3;
        UserInputs.Instance._note4.performed += OnNote4;
        UserInputs.Instance._note5.performed += OnNote5;
        UserInputs.Instance._note6.performed += OnNote6;
    }

    private void OnDisable()
    {
        UserInputs.Instance._activateNoteSheet.performed -= OpenNoteSheetInput;
        UserInputs.Instance._activateNoteSheet.canceled -= CloseNoteSheetInput;
        UserInputs.Instance._note1.performed -= OnNote1;
        UserInputs.Instance._note2.performed -= OnNote2;
        UserInputs.Instance._note3.performed -= OnNote3;
        UserInputs.Instance._note4.performed -= OnNote4;
        UserInputs.Instance._note5.performed -= OnNote5;
        UserInputs.Instance._note6.performed -= OnNote6;
    }
    #endregion


    private void Start()
    {
        spellAttributesSO = SongManager.Instance.GetSpellAttributesObject();

        foreach (Transform child in UiManager.Instance.GetBarsParent().transform)
            barsUi.Add(child.gameObject);

        foreach (Transform child in UiManager.Instance.GetSpellProgressTextParent().transform)
            spellProgressTextUi.Add(child.gameObject);

        SongManager.Instance.canvasGroupNoteSheet.interactable = false;
        SongManager.Instance.canvasGroupNoteSheet.blocksRaycasts = false;
    }

    private void Update()
    {
        _currentAlpha = Mathf.MoveTowards(_currentAlpha, _desiredAlpha, 2.0f * Time.deltaTime);
        SongManager.Instance.canvasGroupNoteSheet.alpha = _currentAlpha;
    }

    #region Open & Close
    private void CloseNoteSheetInput(InputAction.CallbackContext obj)
    {
        _desiredAlpha = 0;
        UserInputs.Instance.OnCloseNoteSheet();
        ResetNoteSheet();
        noteSheetActive = false;
        SongManager.Instance.canvasGroupNoteSheet.interactable = false;
        SongManager.Instance.canvasGroupNoteSheet.blocksRaycasts = false;
    }

    private void OpenNoteSheetInput(InputAction.CallbackContext obj)
    {
        _desiredAlpha = 1;
        UserInputs.Instance.OnOpenNoteSheet();
        noteSheetActive = true;
        SongManager.Instance.canvasGroupNoteSheet.interactable = true;
        SongManager.Instance.canvasGroupNoteSheet.blocksRaycasts = true;
    }
    #endregion

    public void OnNote1(InputAction.CallbackContext obj) { PlayNote(SongManager.Notes.X, MusicManager.Instance.GetCurrentScale()[0]); }
    public void OnNote2(InputAction.CallbackContext obj) { PlayNote(SongManager.Notes.A, MusicManager.Instance.GetCurrentScale()[1]); }
    public void OnNote3(InputAction.CallbackContext obj) { PlayNote(SongManager.Notes.B, MusicManager.Instance.GetCurrentScale()[2]); }
    public void OnNote4(InputAction.CallbackContext obj) { PlayNote(SongManager.Notes.Y, MusicManager.Instance.GetCurrentScale()[3]); }
    public void OnNote5(InputAction.CallbackContext obj) { PlayNote(SongManager.Notes.R2, MusicManager.Instance.GetCurrentScale()[4]); }
    public void OnNote6(InputAction.CallbackContext obj) { PlayNote(SongManager.Notes.L2, MusicManager.Instance.GetCurrentScale()[5]); }
    
    private void PlayNote(SongManager.Notes noteInput, AK.Wwise.Event noteEvent)
    {
        if (!noteSheetActive) return;
        if (currentSequence.Count == SongManager.Instance.GetMaxNotes()) ResetNoteSheet();

        List<GameObject> barNotes = new List<GameObject>();
        foreach (Transform child in barsUi[currentSequence.Count].transform)
        {
            barNotes.Add(child.gameObject);
        }
        barNotes[((int)noteInput)].SetActive(true);

        currentSequence.Add(noteInput);
        MusicManager.Instance.PostEvent(noteEvent);


        switch (spellProgress) //TO-DO: put these into functions, like this shit's disgusting
        {
            case SpellProgress.TYPE:
                if (currentSequence.Count != seqMin + spellAttributesSO.GetTypeAttLength()) return;
                
                foreach (var spellAttribute in spellAttributesSO.GetTypeAttributes())
                {
                    if (CheckOrderMatch(currentSequence.GetRange(seqMin, currentSequence.Count - seqMin), spellAttribute.GetNoteSequence()))
                    {
                        spellProgressTextUi[(int)spellProgress].SetActive(true);
                        spellProgressTextUi[(int)spellProgress].GetComponent<TextMeshProUGUI>().text = Enum.GetName(spellAttribute.GetSpellType().GetType(), spellAttribute.GetSpellType());

                        MusicManager.Instance.SetCurrentScale(spellAttribute.GetScaleToActivate());
                        typeAttribute = spellAttribute;
                        seqMin = currentSequence.Count;
                        spellProgress++;
                        break;
                    }
                }
                break;
        

            case SpellProgress.PROPERTY:
                if (currentSequence.Count != seqMin + spellAttributesSO.GetPropertyAttLength()) return;

                foreach (var spellAttribute in spellAttributesSO.GetPropertyAttribute())
                {
                    if (CheckOrderMatch(currentSequence.GetRange(seqMin, currentSequence.Count - seqMin), spellAttribute.GetNoteSequence()))
                    {
                        spellProgressTextUi[(int)spellProgress].SetActive(true);
                        spellProgressTextUi[(int)spellProgress].GetComponent<TextMeshProUGUI>().text = Enum.GetName(spellAttribute.GetSpellProperty().GetType(), spellAttribute.GetSpellProperty());

                        //MusicManager.Instance.SetCurrentScale(spellAttribute.GetScaleToActivate());
                        propertyAttribute = spellAttribute;
                        seqMin = currentSequence.Count;
                        spellProgress++;
                        break;
                    }
                }
                break;


            case SpellProgress.TARGET:
                if (currentSequence.Count != seqMin + spellAttributesSO.GetTargetAttLength()) return;

                foreach (var spellAttribute in spellAttributesSO.GetTargetAttribute())
                {
                    if (CheckOrderMatch(currentSequence.GetRange(seqMin, currentSequence.Count - seqMin), spellAttribute.GetNoteSequence()))
                    {
                        spellProgressTextUi[(int)spellProgress].SetActive(true);
                        spellProgressTextUi[(int)spellProgress].GetComponent<TextMeshProUGUI>().text = Enum.GetName(spellAttribute.GetSpellTarget().GetType(), spellAttribute.GetSpellTarget());

                        currentAbility.Activate(typeAttribute.GetSpellType(), spellAttribute.GetSpellTarget(), propertyAttribute.GetSpellProperty());
                        DisableNoteSheet();
                        break;
                    }
                }
                break;
        }
    }


    private void ResetNoteSheet()
    {
        spellProgress = 0;
        seqMin = 0;
        currentSequence.Clear();
        MusicManager.Instance.ResetCurrentScale();

        //disguting art
        foreach (var item in barsUi)
        {
            foreach (Transform child in item.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        foreach (var item in spellProgressTextUi)
        {
            item.SetActive(false);
        }
    }
    private void DisableNoteSheet()
    {
        ResetNoteSheet();
        //noteSheetUi.SetActive(false);
    }

    bool CheckOrderMatch(List<SongManager.Notes> l1, List<SongManager.Notes> l2)
    {
        if (l1.Count != l2.Count)
            return false;

        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
                return false;
        }

        return true;
    }
    public SpellAttribute CheckSpellAttribute(List<SpellAttribute> attributes)
    {
        return new SpellAttribute();

        /*foreach (var spellAttribute in attributes)
        {
            if (CheckOrderMatch(currentSequence.GetRange(seqMin, currentSequence.Count - seqMin), spellAttribute.GetNoteSequence()))
            {
                seqMin = currentSequence.Count;
                return spellAttribute;
            }
        }
        return null;
        */
    }
}
