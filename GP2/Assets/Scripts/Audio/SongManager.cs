using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static CompactMath;

public class SongManager : MonoBehaviour {
    public static SongManager Instance;

    public enum Notes {
        X,
        A,
        B,
        Y,
        R2,
        L2
    }

    [Header("Song Management")] [SerializeField]
    private int maxNotes;

    [SerializeField] SO_AttributeCross_Type SO_TypeCross;
    [SerializeField] SO_AttributeCross_Property SO_PropertyCross;
    [SerializeField] SO_AttributeCross_Target SO_TargetCross;

    [SerializeField] SO_SpellAttributes spellAttributesSO;

    [Header("Components")] public CanvasGroup canvasGroupSongWheel;
    public CanvasGroup canvasGroupNoteSheet;
    [SerializeField] private TextMeshProUGUI songText;


    [Header("Button References")] [SerializeField]
    private Song song0Button;

    [SerializeField] private Song song1Button;
    [SerializeField] private Song song2Button;
    [SerializeField] private Song song3Button;
    [SerializeField] private Song song4Button;
    [SerializeField] private Song song5Button;
    [SerializeField] private Song song6Button;
    [SerializeField] private Song song7Button;

    [HideInInspector] public float desiredAlpha;
    [HideInInspector] public float currentAlpha;
    [HideInInspector] public float desiredAlphaText;
    [HideInInspector] public float currentAlphaText;
    [HideInInspector] public Coroutine ActiveRoutine;

    private Button _selectedButton = null;
    private int _selectedIndex = -1;
    private bool _menuIsOpen = false;
    private Vector2 _mouseInputVector;
    private Vector2 _controllerInputVector;
    private Vector2 _lastControllerInputVector;


    /// <summary>
    /// Subscribes to all events
    /// </summary>
    private void OnEnable() {
        UserInputs.Instance._songWheelMouseLook.performed += ReadMouseInput;
        UserInputs.Instance._songWheelControllerLook.performed += ReadControllerInput;
        UserInputs.Instance._songWheelControllerLook.canceled += ReadControllerInput;
        UserInputs.Instance._openSongWheel.canceled += CloseMenuInput;
        UserInputs.Instance._openSongWheel.performed += OpenMenuInput;
    }

    private void OnDisable() {
        UserInputs.Instance._songWheelMouseLook.performed -= ReadMouseInput;
        UserInputs.Instance._songWheelControllerLook.performed -= ReadControllerInput;
        UserInputs.Instance._songWheelControllerLook.canceled -= ReadControllerInput;
        UserInputs.Instance._openSongWheel.canceled -= CloseMenuInput;
        UserInputs.Instance._openSongWheel.performed -= OpenMenuInput;
    }


    /// <summary>
    /// Singelton and set all canvases to not be shown
    /// </summary>
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(this);

        currentAlpha = 0;
        desiredAlpha = 0;
        currentAlphaText = 0;
        desiredAlphaText = 0;

        canvasGroupNoteSheet.interactable = false;
        canvasGroupNoteSheet.blocksRaycasts = false;
        canvasGroupSongWheel.interactable = false;
        canvasGroupSongWheel.blocksRaycasts = false;
    }

    /// <summary>
    /// Sets the song text to empty string
    /// </summary>
    private void Start() {
        songText.text = "";
    }

    /// <summary>
    /// Invokes the onclick event on the correct button
    /// Fades the Song text and the song wheel to either be shown or not
    /// </summary>
    private void Update() {
        if (_menuIsOpen) {
            OpenMenu();
        }

        if (UserInputs.Instance._songSelect.WasPressedThisFrame() && _selectedButton != null) {
            _selectedButton.onClick.Invoke();
        }

        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 2.0f * Time.deltaTime);
        canvasGroupSongWheel.alpha = currentAlpha;

        currentAlphaText = Mathf.MoveTowards(currentAlphaText, desiredAlphaText, 2f * Time.deltaTime);
        songText.color = new Color(songText.color.a, songText.color.g, songText.color.b,
            currentAlphaText);
    }

    /// <summary>
    /// Opens the song wheel
    /// </summary>
    private void OpenMenu() {
        desiredAlpha = 1;
        UserInputs.Instance.OnOpenSongWheel();
        canvasGroupSongWheel.interactable = true;
        canvasGroupSongWheel.blocksRaycasts = true;
    }

    /// <summary>
    /// Closes the song wheel
    /// </summary>
    public void CloseMenu() {
        desiredAlpha = 0;
        _menuIsOpen = false;
        _selectedButton = null;
        UserInputs.Instance.OnCloseSongWheel();
        canvasGroupSongWheel.interactable = false;
        canvasGroupSongWheel.blocksRaycasts = false;
    }

    /// <summary>
    /// Input for opening the song wheel - Key down Q
    /// </summary>
    /// <param name="obj"></param>
    private void OpenMenuInput(InputAction.CallbackContext obj) {
        desiredAlphaText = 0;
        _menuIsOpen = true;
        RemoveHighlight();
    }

    /// <summary>
    /// Inpur for closing the song wheel - Let go of key Q
    /// </summary>
    /// <param name="obj"></param>
    private void CloseMenuInput(InputAction.CallbackContext obj) {
        if (_selectedButton != null) {
            _selectedButton.onClick.Invoke();
        }

        CloseMenu();
    }


    /// <summary>
    /// Input for controller Left stick
    /// </summary>
    /// <param name="obj"></param>
    private void ReadControllerInput(InputAction.CallbackContext obj) {
        if (!_menuIsOpen) return;

        if (obj.ReadValue<Vector2>() == Vector2.zero) {
            if (_selectedButton != null) {
                _selectedButton.onClick.Invoke();
                return;
            }

            RemoveHighlight();
            SetCorrectButton(-1);
            return;
        }

        _controllerInputVector = obj.ReadValue<Vector2>();
        float angle = GetAngleFromScreenCenter(_controllerInputVector);
        int index = GetIndexFromCircle(8, angle);
        SetCorrectButton(index);
    }


    /// <summary>
    /// Inpur for mouse movement
    /// </summary>
    /// <param name="obj"></param>
    private void ReadMouseInput(InputAction.CallbackContext obj) {
        if (!_menuIsOpen) return;

        if (obj.ReadValue<Vector2>() == Vector2.zero) {
            RemoveHighlight();
            SetCorrectButton(-1);
            return;
        }

        _mouseInputVector = obj.ReadValue<Vector2>();
        float angle = GetCursorAngleFromCenter(_mouseInputVector);
        int index = GetIndexFromCircle(8, angle);
        SetCorrectButton(index);
    }

    /// <summary>
    /// Highlight and sets the selectedButton to be the one that is highlighted
    /// Sends in an index for the highlighted button
    /// </summary>
    /// <param name="index"></param>
    private void SetCorrectButton(int index) {
        _selectedIndex = index;
        RemoveHighlight();

        switch (_selectedIndex) {
            case 0:
                song0Button.Highlight();
                _selectedButton = song0Button.GetButton();
                break;
            case 1:
                song1Button.Highlight();
                _selectedButton = song1Button.GetButton();
                break;
            case 2:
                song2Button.Highlight();
                _selectedButton = song2Button.GetButton();
                break;
            case 3:
                song3Button.Highlight();
                _selectedButton = song3Button.GetButton();
                break;
            case 4:
                song4Button.Highlight();
                _selectedButton = song4Button.GetButton();
                break;
            case 5:
                song5Button.Highlight();
                _selectedButton = song5Button.GetButton();
                break;
            case 6:
                song6Button.Highlight();
                _selectedButton = song6Button.GetButton();
                break;
            case 7:
                song7Button.Highlight();
                _selectedButton = song7Button.GetButton();
                break;
            default:
                _selectedIndex = -1;
                _selectedButton = null;
                RemoveHighlight();
                break;
        }
    }

    /// <summary>
    /// Removes the highlight from all buttons in the song wheel
    /// </summary>
    private void RemoveHighlight() {
        song0Button.RemoveHighlight();
        song1Button.RemoveHighlight();
        song2Button.RemoveHighlight();
        song3Button.RemoveHighlight();
        song4Button.RemoveHighlight();
        song5Button.RemoveHighlight();
        song6Button.RemoveHighlight();
        song7Button.RemoveHighlight();
    }


    /// <summary>
    /// Set the selected song to the song that is chosen 
    /// </summary>
    /// <param name="notes"></param>
    public void SetSong(string notes) {
        if (ActiveRoutine != null) {
            StopCoroutine(ActiveRoutine);
        }

        ActiveRoutine = StartCoroutine(FadeTextRoutine());
        songText.text = notes;
        RemoveHighlight();
        CloseMenu();
    }

    /// <summary>
    /// Coroutine for fading in and then out the song text
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeTextRoutine() {
        desiredAlphaText = 1;
        yield return new WaitForSeconds(7f);
        desiredAlphaText = 0;
    }


    // Getters
    public int GetMaxNotes() => maxNotes;
    public SO_AttributeCross_Type GetTypeCross() => SO_TypeCross;
    public SO_AttributeCross_Property GetPropertyCross() => SO_PropertyCross;
    public SO_AttributeCross_Target GetTargetCross() => SO_TargetCross;

    public SO_SpellAttributes GetSpellAttributesObject() => spellAttributesSO;
    /*public List<SO_SpellAttributes.SpellAttribute> GetSpellAttributes(AbilityBehaviour.SpellTarget attribute)
    {
        switch (attribute)
        {
            case AbilityBehaviour.SpellTarget.PLAYER: return spellAttributesSO.GetTypeAttributes();
                break;
            case AbilityBehaviour.SpellTarget.CORRUPTION:
                break;
            case AbilityBehaviour.SpellTarget.PUSHABLE_OBJECT:
                break;
            default:
                break;
        }
    }*/
}