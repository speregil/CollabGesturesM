using UnityEngine;
using UnityEngine.XR.ARFoundation;

/**
 * Defines the behaviour of task 1: Point an object to someone
 */
public class pointPersonTask : MonoBehaviour, ITask
{
    //------------------------------------------------------------------------------
    // Fields
    //------------------------------------------------------------------------------

    [SerializeField]
    private GameObject taskPrefab;          // Prefab for the model used in the task

    private GameObject taskInstance;        // Current instance of the prefab model
    private ParticleSystem emitter;         // Particle emmitter of the current instance
    private string taskName;                // Show name of the task

    //------------------------------------------------------------------------------
    // Functions
    //------------------------------------------------------------------------------

    /**
     * Returns the show name of the task 
     */
    public string GetTaskName()
    {
        return taskName;
    }

    /**
     * Creates an instance of the model on top of the plane given as parameter
     * @currentPlane Current AR plane detected in the main setup 
     */
    public void SetupTask(ARPlane currentPlane)
    {
        taskName = "Point at Person";
        Vector3 planeSize = currentPlane.GetComponent<Renderer>().bounds.size;
        Vector3 offset = new Vector3(0.0f, -1.0f, (planeSize.z / 2.0f)*-1.0f - 0.5f);
        taskInstance = Instantiate(taskPrefab, currentPlane.transform.position + offset, Quaternion.Euler(0,0,0));
        emitter = taskInstance.GetComponentInChildren<ParticleSystem>();
    }

    /**
     * Plays all the emmitters of the model to create a ping effect
     */
    public void PlayTask()
    {
        emitter.Play();
    }

    /**
     * Destroys the current instance of the model
     */
    public void Clean()
    {
        GameObject.Destroy(taskInstance);
    }
}