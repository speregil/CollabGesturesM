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
    
    private List<ITask> tasksList;
    private SetupManager setupManager;

    private bool firstSetup;
    private int currentTask;
    

    void Start()
    {
        setupManager = GetComponent<SetupManager>();
        tasksList = new List<ITask>();
        tasksList.Add(GetComponent<pointObjectTask>());
        currentTask = 0;
        firstSetup = true;
    }

    void Update()
    {
        if (!setupManager.IsOnSetup() && firstSetup)
        {
            setupPanel.SetActive(false);
            taskPanel.SetActive(true);
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
        if(currentTask +1 < tasksList.Count)
        {
            currentTask++;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());

        }
    }

    public void OnPreviousButton()
    {
        if (currentTask - 1 >= 0)
        {
            currentTask--;
            tasksList[currentTask].SetupTask(setupManager.GetCurrentPlane());
        }
    }
}