using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalPage : MonoBehaviour {
    
    public TextMeshProUGUI headerText;
    public Button tabButton;

    public void OpenTab() {
        Animator animator = tabButton.GetComponent<Animator>();
        animator.SetTrigger("OpenTab");
    }

    public void CloseTab() {
        Animator animator = tabButton.GetComponent<Animator>();
        animator.SetTrigger("CloseTab");
    }
}
