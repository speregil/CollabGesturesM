using UnityEngine.XR.ARFoundation;

public interface ITask
{
    public void SetupTask(ARPlane currentPlane);
    public void PlayTask();
    public void Clean();
}