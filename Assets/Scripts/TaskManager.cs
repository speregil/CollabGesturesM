using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Behaviour to control the execution of all the tasks in the session and connect them to the different
 * assets they would need
 */
public class TaskManager : MonoBehaviour
{
    //------------------------------------------------------------------------------
    // Fields
    //------------------------------------------------------------------------------

    [SerializeField]
    private GameObject setupPanel;              // Serialized reference to the Setup Panel in the UI

    [SerializeField]
    private GameObject taskPanel;               // Serialized reference to the Task Panel in the UI

    private SetupManager setupManager;          // Reference to the Setup Manager Beheviour
    private Button prevButton;                  // Reference to the button that changes to the previous task
    private Button nextButton;                  // Reference to the button that changes to the next task
    private Button playButton;                  // Reference to the button that plays the curent task
    private TextMeshProUGUI taskNameText;       // Reference to the text in UI with the name of the current task

    private List<ITask> tasksList;              // List of all the behaviours of type ITask that represent the set
                                                // of tasks to perform during the session 
    
    private bool firstSetup;                    // Flag that determines if the first plane setup has finished or not
    private int currentTask;                    // Index in the task list of the current task

    //------------------------------------------------------------------------------
    // Setup
    //------------------------------------------------------------------------------

    void Start()
    {
        setupManager = GetComponent<SetupManager>();
        prevButton = taskPanel.transform.Find("PrevButton").gameObject.GetComponent<Button>();
        nextButton = taskPanel.transform.Find("NextButton").gameObject.GetComponent<Button>();
        playButton = taskPanel.transform.Find("PlayButton").gameObject.GetComponent<Button>();
        taskNameText = setupPanel.GetComponentInChildren<TextMeshProUGUI>();
        
        // The behaviour must exist as a component of the TaskManager gameobject
        tasksList = new List<ITask>
        {
            GetComponent<pointObjectTask>(),
            GetComponent<pointPlaceTask>(),
            GetComponent<pointPersonTask>()
        };

        currentTask = 0;
        taskPanel.SetActive(false);
        firstSetup = true;
    }

    //------------------------------------------------------------------------------
    // Update
    //------------------------------------------------------------------------------

    void Update()
    {
        if (!setupManager.IsOnSetup() && firstSetup)
        {
            taskPanel.SetActive(true);
            prevButton.interactable = false;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());
            taskNameText.text = tasksList[currentTask].GetTaskName();
            firstSetup = false;
        }
    }

    //------------------------------------------------------------------------------
    // Functions
    //------------------------------------------------------------------------------

    /**
     * Response to the play button
     * Asks the current task in the list to play
     */
    public void OnPlayButton()
    {
        tasksList[currentTask].PlayTask();
    }

    /**
     * Response to the next button
     * Moves the current task one space and modifies the UI as necesary
     */
    public void OnNextButton()
    {
        playButton.interactable = false;
        if(currentTask + 1 < tasksList.Count) // Move only if there are still tasks
        {
            if (currentTask == 0) { prevButton.interactable = true; }   // Active the prev button if there are prev tasks
            tasksList[currentTask].Clean();
            currentTask++;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());
            taskNameText.text = tasksList[currentTask].GetTaskName();
            playButton.interactable = true;
        }

        if (currentTask == tasksList.Count - 1) // Deactivate the button if we are in the last
        {
            nextButton.interactable = false;
        }
    }

    /**
     * Response to the Previous button
     * Moves the current tasks one space back and modifies the UI as necesary
     */
    public void OnPreviousButton()
    {
        playButton.interactable = false;
        if (currentTask - 1 >= 0) // Move back only if there are still tasks
        {
            if(currentTask == tasksList.Count - 1) { nextButton.interactable = true; }  // Active the next button if there are prev tasks
            tasksList[currentTask].Clean();
            currentTask--;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());
            taskNameText.text = tasksList[currentTask].GetTaskName();
            playButton.interactable = true;
        }

        if (currentTask == 0) // Deactivate the button if we are in the first tasks
        {
            prevButton.interactable = false;
        }
    }
}