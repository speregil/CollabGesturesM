using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    private GameObject modelObject;

    // Start is called before the first frame update
    void Start()
    {
        modelObject = null;
    }

    private void Update()
    {
        if(modelObject == null)
        {
            modelObject = GameObject.FindGameObjectWithTag("modelObject");
            playButton.onClick.AddListener(() => modelObject.GetComponent<pointObjectTask>().OnPlayTask());
        }
    }
}
