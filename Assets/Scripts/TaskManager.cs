using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private GameObject setupPanel;

    [SerializeField]
    private GameObject taskPanel;

    private SetupManager setupManager;
    private Button prevButton;
    private Button nextButton;
    private Button playButton;

    private List<ITask> tasksList;
    
    private bool firstSetup;
    private int currentTask;
    

    void Start()
    {
        setupManager = GetComponent<SetupManager>();
        prevButton = taskPanel.transform.Find("PrevButton").gameObject.GetComponent<Button>();
        nextButton = taskPanel.transform.Find("NextButton").gameObject.GetComponent<Button>();
        playButton = taskPanel.transform.Find("PlayButton").gameObject.GetComponent<Button>();
        tasksList = new List<ITask>
        {
            GetComponent<pointObjectTask>(),
            GetComponent<pointPlaceTask>()
        };
        currentTask = 0;
        firstSetup = true;
    }

    void Update()
    {
        if (!setupManager.IsOnSetup() && firstSetup)
        {
            setupPanel.SetActive(false);
            taskPanel.SetActive(true);
            prevButton.interactable = false;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());
            firstSetup = false;
        }
    }

    public void OnPlayButton()
    {
        tasksList[currentTask].PlayTask();
    }

    public void OnNextButton()
    {
        playButton.interactable = false;
        if(currentTask + 1 < tasksList.Count)
        {
            if (currentTask == 0) { prevButton.interactable = true; }
            tasksList[currentTask].Clean();
            currentTask++;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());
            playButton.interactable = true;
        }

        if (currentTask == tasksList.Count - 1)
        {
            nextButton.interactable = false;
        }
    }

    public void OnPreviousButton()
    {
        playButton.interactable = false;
        if (currentTask - 1 >= 0)
        {
            if(currentTask == tasksList.Count - 1) { nextButton.interactable = true; }
            tasksList[currentTask].Clean();
            currentTask--;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());
            playButton.interactable = true;
        }

        if (currentTask == 0)
        {
            prevButton.interactable = false;
        }
    }
}