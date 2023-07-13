using UnityEngine;

/**
 * Simple behaviour of an object beeing hold in task 4
 */
public class RotateCube : MonoBehaviour
{
    //------------------------------------------------------------------------------
    // Fields
    //------------------------------------------------------------------------------

    [SerializeField]
    private Vector3 holdingRotation;        // Direction and speed rotation of the object

    private bool isHold;                    // Starts or stops the rotation animation

    //------------------------------------------------------------------------------
    // Setups
    //------------------------------------------------------------------------------

    void Start()
    {
        isHold = false;
    }

    //------------------------------------------------------------------------------
    // Update
    //------------------------------------------------------------------------------

    void Update()
    {
        if (isHold)
        {
            gameObject.transform.Rotate(holdingRotation * Time.deltaTime);
        }
    }

    //------------------------------------------------------------------------------
    // Functions
    //------------------------------------------------------------------------------

    /**
     * Changes the state of the animation
     * @param isHold True in the animation is playing, false otherwise
     */
    public void holding(bool isHold)
    {
        this.isHold = isHold;
    }

    /**
     * Returns the state of the animation
     * @return True if it is playing, False otherwise
     */
    public bool isHolding()
    {
        return isHold;
    }
}