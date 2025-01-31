using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [Header("Note Sheet Player")]
    [SerializeField] private GameObject noteSheetUi;
    [SerializeField] private GameObject barsParent;
    [SerializeField] private GameObject spellProgressTextParent;

    [Header("Spell Maker")]
    [SerializeField] private CanvasGroup canvasGroupSpellMaker;
    [SerializeField] private GameObject buttonPromptUi;
    [SerializeField] private Texture defaultButtonTexture;
    [SerializeField] private GameObject attributeNameSpaceUi;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    public CanvasGroup GetSpellMakerCanGroup() => canvasGroupSpellMaker;
    public GameObject GetButtonPromptUI() => buttonPromptUi;
    public Texture GetDefaultButtonSprite() => defaultButtonTexture;
    public GameObject GetAttributeNameSpaceUI() => attributeNameSpaceUi;

    //remove later (keep rn for functionality)
    public GameObject GetNoteSheetUi() => noteSheetUi;
    public GameObject GetBarsParent() => barsParent;
    public GameObject GetSpellProgressTextParent() => spellProgressTextParent;
}
