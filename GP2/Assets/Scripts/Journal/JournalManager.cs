// Made by Martin M
using UnityEngine;
using UnityEngine.InputSystem;

public class JournalManager : MonoBehaviour
{

    public static JournalManager Instance; //probably not good - created by mohammed

    [Header("Components")]
    public CanvasGroup canvasGroup;
    public GameObject[] pages;
    
    private float _desiredAlpha;
    private float _currentAlpha;
    private bool _isJournalOpen = false;
    private int _activePageIndex = 0;
    private JournalPage _activeJournalPage;



    /// <summary>
    /// Subscribes to the inputs events
    /// </summary>
    private void OnEnable() {
         UserInputs.Instance._openJournal.performed += OpenMenuInput;
         UserInputs.Instance._nextPage.performed += NextInput;
         UserInputs.Instance._previousPage.performed  += PreviousInput;
    }

    private void OnDisable() {
        UserInputs.Instance._openJournal.performed -= OpenMenuInput;
        UserInputs.Instance._nextPage.performed -= NextInput;
        UserInputs.Instance._previousPage.performed  -= PreviousInput;
    }
    
    /// <summary>
    /// Singelton and sets the canvas group alpha to 0
    /// </summary>
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(this);

        _currentAlpha = 0;
        _desiredAlpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    
    /// <summary>
    /// Fades the journal in and out
    /// </summary>
    private void Update() {
        _currentAlpha = Mathf.MoveTowards(_currentAlpha, _desiredAlpha, 2.0f * Time.deltaTime);
        canvasGroup.alpha = _currentAlpha;
    }
    
    /// <summary>
    /// Toggle the journal on or off
    /// </summary>
    private void ToggleJournal() {

        if (!_isJournalOpen) {
            OpenJournal();
            SongManager songManager = FindFirstObjectByType<SongManager>();
            songManager.currentAlpha = 0;
            songManager.desiredAlpha = 0;
            songManager.canvasGroupSongWheel.alpha = 0;
            songManager.desiredAlphaText = 0;
            songManager.currentAlphaText = 0;
            if(songManager.ActiveRoutine != null) 
                StopCoroutine(songManager.ActiveRoutine);
            UserInputs.Instance.OnOpenJournal();

        }
        else {
            CloseJournal();
            SongManager songManager = FindFirstObjectByType<SongManager>();
            UserInputs.Instance.OnCloseJournal();
        }
    }

    /// <summary>
    /// Open the journal
    /// </summary>
    private void OpenJournal() {
        _desiredAlpha = 1;
        _isJournalOpen = true;
        GoToFirstPage();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Close the journal
    /// </summary>
    public void CloseJournal() {
        pages[_activePageIndex].GetComponent<JournalPage>().CloseTab();
        _desiredAlpha = 0;
        _isJournalOpen = false;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Sets the journal to the first page
    /// </summary>
    private void GoToFirstPage() {
        foreach (var page in pages) {
            page.gameObject.SetActive(false);
        }
        pages[0].gameObject.SetActive(true);
        _activePageIndex = 0;
        SetActivePage(_activePageIndex);
    }

    /// <summary>
    /// Go to next page in journal
    /// </summary>
    private void NextPage() {
        pages[_activePageIndex].GetComponent<JournalPage>().CloseTab();
        _activePageIndex++;
        
        if (_activePageIndex > pages.Length -1)
            _activePageIndex = 0;
        
        for (int i = 0; i < pages.Length; i++) {
            pages[i].gameObject.SetActive(false);
            if (_activePageIndex == i) {
                pages[i].gameObject.SetActive(true);
                SetActivePage(_activePageIndex);
            }
        }
    }
    
    /// <summary>
    /// Go to previous page in journal
    /// </summary>
    private void PreviousPage() {
        pages[_activePageIndex].GetComponent<JournalPage>().CloseTab();
        pages[_activePageIndex].gameObject.SetActive(false);
        _activeJournalPage.CloseTab();
        
        _activePageIndex--;
        
        if (_activePageIndex < 0)
            _activePageIndex = pages.Length - 1;
        
        pages[_activePageIndex].gameObject.SetActive(true);
        SetActivePage(_activePageIndex);
 
    }

    /// <summary>
    /// Set the page to the active page from index
    /// </summary>
    /// <param name="pageIndex"></param>
    private void SetActivePage(int pageIndex) {
        _activeJournalPage = pages[pageIndex].GetComponent<JournalPage>();
        _activeJournalPage.OpenTab();
    }

    /// <summary>
    /// Input to open menu
    /// </summary>
    /// <param name="obj"></param>
    private void OpenMenuInput(InputAction.CallbackContext obj) {
       ToggleJournal();
    }
    
    /// <summary>
    /// Input to go to next page
    /// </summary>
    /// <param name="obj"></param>
    private void NextInput(InputAction.CallbackContext obj) {
        if(_isJournalOpen)
            NextPage();
    }
    
    /// <summary>
    /// Input to go to previous page
    /// </summary>
    /// <param name="obj"></param>
    private void PreviousInput(InputAction.CallbackContext obj) {
        if(_isJournalOpen)
            PreviousPage();
    }


    // //we gotta fix this input system thing lmao - created by Mohammed
    // public void SetIsPlayingMusic(bool value)
    // {
    //     //isPlayingMusic = value;
    // }
}
