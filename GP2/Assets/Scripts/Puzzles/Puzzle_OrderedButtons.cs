using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static CompactMath;

public class Puzzle_OrderedButtons : PuzzleEvent
{
    [SerializeField] List<AK.Wwise.Event> soundOrder;
    [SerializeField] List<GameObject> orderedButtons; //this is the order that needs to be followed
    [SerializeField] Puzzle_SpellButton startButton;
    List<Puzzle_ActivateButton> activateButtons = new List<Puzzle_ActivateButton>();


    private List<GameObject> currentOrder = new List<GameObject>();

    private void Start()
    {
        //listen to all buttons
        foreach (GameObject button in orderedButtons)
        {
            button.GetComponent<Puzzle_ActivateButton>().OnButtonPressed += ButtonPressed;
            activateButtons.Add(button.GetComponent<Puzzle_ActivateButton>());
        }

        startButton.OnButtonPressed += StartButtonPressed;
    }

    private void ButtonPressed(GameObject obj)
    {
        //add the button to the current order
        currentOrder.Add(obj);
        //check if the current order is the same as the code

        MusicManager.Instance.PostEvent(soundOrder[activateButtons.IndexOf(obj.GetComponent<Puzzle_ActivateButton>())]);

        if (currentOrder.Count == orderedButtons.Count)
        {
            for (int i = 0; i < orderedButtons.Count; i++)
            {
                Debug.Log("Checking order: " + currentOrder[i].name + " vs " + orderedButtons[i].name);
                if (currentOrder[i] != orderedButtons[i])
                {
                    //if the order is wrong, reset the current order
                    Debug.Log("Wrong order!");
                    currentOrder.Clear();
                    MusicManager.Instance.PostEvent("Puzzle_OrderedButtonsWrong");

                    ResetButtons();
                    return;
                }
            }
            //if the order is correct, do something
            Debug.Log("Correct order!");
            Invoke("TriggerOnButtonPressed", 1);
        }
    }

    private async void ResetButtons()
    {
        await Task.Delay(SecondsToMilli(0.5F));

        foreach (Puzzle_ActivateButton button in activateButtons)
        {
            button.StartCoroutine("ResetButton");
        }

        foreach (Puzzle_ActivateButton button in activateButtons)
        {
            button.isPressable = false;
        }
    }


    public IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(2);

        MusicManager.Instance.PostEvent("Puzzle_OrderedButtonsCorrectMelody");

        yield return null;
    }
    
    private void StartButtonPressed(GameObject obj)
    {
        currentOrder.Clear();

        foreach (Puzzle_ActivateButton button in activateButtons) {
            button.isPressable = true;
            StartCoroutine(PlayMusic());
        }

        //IMPLEMENT THE INFORMATION THAT THE PUZZLE HAS STARTED AND WHICH ORDER THERE IS
    }

}
