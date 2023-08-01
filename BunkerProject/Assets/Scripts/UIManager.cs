using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Object refrences
    public TextMeshProUGUI InteractableText;

    public Image RadioIcon;
    public Sprite RadioDone;
    public Image EnergyIcon;
    public Sprite EnergyDone;
    public Image ResourceIcon;
    public Sprite ResourceDone;

    public TextMeshProUGUI RadioText;
    public TextMeshProUGUI EnergyText;
    public TextMeshProUGUI ResourceText;

    // Variables
    bool isInteractable;
    string interactableName;

    public bool radioDone;
    public bool energyDone;
    public bool resourceDone;

    // Stuff for referencing functions in other scripts (thanks Vaughan)
    private static UIManager instance;
    public static UIManager Instance => instance;

    void Awake()
    {
        instance = this;
        HideInteractableText();

        // Setting Done Variables to false
        radioDone = false;
        energyDone = false;
        resourceDone = false;
    }

    void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E)) 
        {
            CompleteTask(interactableName);
            Debug.Log(interactableName + " Task Done"); //Replace with if statements that determine what to do depending on interactableName
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

    void CompleteTask(string task) // Used to mark off tasks (duh)
    {
        if (task == "Radio")
        {
            RadioIcon.sprite = RadioDone;
            RadioText.color = Color.black;
            radioDone = true;
        }
        if (task == "Energy")
        {
            EnergyIcon.sprite = EnergyDone;
            EnergyText.color = Color.black;
            energyDone = true;
        }
        if (task == "Resources")
        {
            ResourceIcon.sprite = ResourceDone;
            ResourceText.color = Color.black;
            resourceDone = true;
        }
    }
}
