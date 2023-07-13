using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.GraphicsBuffer;

/**
 * Defines the behaviour of task 4: Hold an object
 */
public class HoldObjectTask : MonoBehaviour, ITask
{
    //------------------------------------------------------------------------------
    // Fields
    //------------------------------------------------------------------------------

    [SerializeField]
    private GameObject taskPrefab;          // Prefab for the model used in the task

    [SerializeField]
    private GameObject anchor;              // Object representing the anchor for grabing the task object

    [SerializeField]
    private float speed;                    // How fast the object mooves to the anchor point

    private GameObject taskInstance;        // Current instance of the prefab model
    private string taskName;                // Show name of the task
    private bool holding = false;           // Flag determining if the holding animation is beeing played

    //------------------------------------------------------------------------------
    // Update
    //------------------------------------------------------------------------------

    void Update()
    {
        if (holding) // Moves only when the Play action has been called
        {
            // Smoothly moves towards the anchor point
            taskInstance.transform.position = Vector3.MoveTowards(taskInstance.transform.position, anchor.transform.position, Time.deltaTime * speed);

            // Stops when close to the anchor point and initiates the animation
            if (Vector3.Distance(taskInstance.transform.position, anchor.transform.position) < 0.001f)
            {
                taskInstance.transform.parent = anchor.transform;
                RotateCube rotateCube = taskInstance.GetComponent<RotateCube>();
                rotateCube.holding(true);
                holding = false;
            }
        }
    }

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
        taskName = "Hold an Object";
        Vector3 offset = new Vector3(0, (taskPrefab.transform.localScale.y) / 2, 0);
        taskInstance = Instantiate(taskPrefab, currentPlane.transform.position + offset, currentPlane.transform.rotation);
    }

    /**
     * Initiates the moving animation of the model object to the anchor point
     */
    public void PlayTask()
    {
        holding = true;
    }

    /**
     * Destroys the current instance of the model
     */
    public void Clean()
    {
        GameObject.Destroy(taskInstance);
    }
}