using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Object refrences
    public TextMeshProUGUI InteractableText;

    // Variables
    bool isInteractable;
    string interactableName;

    // Stuff for referencing functions in other scripts (thanks Vaughan)
    private static UIManager instance;
    public static UIManager Instance => instance;

    void Awake()
    {
        instance = this;
        HideInteractableText();
    }

    void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.Log("Do " + interactableName + "Option"); //Replace with if statements that determine what to do depending on interactableName

            HideInteractableText();
        }
    }

    internal void SetInteractableText(string letter, string option) // option string determines what interactabel it is: (radio, power, resources)
    {
        isInteractable = true;
        InteractableText.enabled = true;
        InteractableText.text = "Press " + letter + " to interact";
        interactableName = option;
    }

    internal void HideInteractableText()
    {
        isInteractable = false;
        InteractableText.enabled = false;
    }
}
