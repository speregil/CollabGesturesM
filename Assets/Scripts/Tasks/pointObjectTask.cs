using UnityEngine;
using UnityEngine.XR.ARFoundation;

/**
 * Defines the behaviour of task 1: Point an object to someone
 */
public class PointObjectTask : MonoBehaviour, ITask
{
    //------------------------------------------------------------------------------
    // Fields
    //------------------------------------------------------------------------------

    [SerializeField]
    private GameObject taskPrefab;          // Prefab for the model used in the task

    private GameObject taskInstance;        // Current instance of the prefab model
    private ParticleSystem[] emitters;      // Set of particle emmitters of the current instance
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
        taskName = "Point at Object";
        Vector3 offset = new Vector3(0, (taskPrefab.transform.localScale.y) / 2, 0);
        taskInstance = Instantiate(taskPrefab, currentPlane.transform.position + offset, currentPlane.transform.rotation);
        emitters = taskInstance.GetComponentsInChildren<ParticleSystem>();
    }

    /**
     * Plays all the emmitters of the model to create a ping effect
     */
    public void PlayTask()
    {
        foreach (ParticleSystem p in emitters)
        {
            p.Play();
        }
    }

    /**
     * Destroys the current instance of the model
     */
    public void Clean()
    {
        GameObject.Destroy(taskInstance);
    }
}