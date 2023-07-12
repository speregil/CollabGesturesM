using UnityEngine.XR.ARFoundation;

/**
 * Interface that describes the contract for the common beheviour of any task of the data gathering
 */
public interface ITask
{
    /**
     * Defines the setup actions for the task, normally asociated to placing any
     * model in the current working plane
     * @param currentPlane AR plane detected and selected during the setup phase of the system
     */
    public void SetupTask(ARPlane currentPlane);
    
    /**
     * Defines the execusion of the particular task
     */
    public void PlayTask();
    
    /**
     * Defines the procedures to clean any model and any change to the system state previous to
     * changing to another task
     */
    public void Clean();

    /**
     * Defines the getter for the show name of the task 
     */
    public string GetTaskName();
}