using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

/**
 * Beheviour to set up the main interaction plane of the session and offer it to the different tasks
 */
public class SetupManager : MonoBehaviour
{
    //------------------------------------------------------------------------------
    // Fields
    //------------------------------------------------------------------------------

    [SerializeField]
    private GameObject AROrigin;                // Origin of the AR service

    private ARRaycastManager raycastManager;    // Component for the AR raycast manager
    private ARPlaneManager planeManager;        // Component for the  plane detection

    ARPlane currentPlane;                       // Current detected and selected plane

    private bool onSetup;                       // Determines if the session is on setup mode

    //------------------------------------------------------------------------------
    // Setups
    //------------------------------------------------------------------------------

    void Start()
    {
        raycastManager = AROrigin.GetComponent<ARRaycastManager>();
        planeManager = AROrigin.GetComponent<ARPlaneManager>();
        currentPlane = null;
        onSetup = true;
    }

    private void OnEnable()
    {
        // Enables touch interaction support
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();

        // Adds listener to the touch interaction
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        // Disables touch interaction
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }

    //------------------------------------------------------------------------------
    // Update
    //------------------------------------------------------------------------------

    public void Update()
    {
        if (onSetup) MouseDown();
    }

    //------------------------------------------------------------------------------
    // Functions
    //------------------------------------------------------------------------------

    /* Listener for the touch interaction */
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0 || !onSetup) return;
        CheckForPlane(finger.currentTouch.screenPosition);
    }

    /* Checks for mouse left click */
    private void MouseDown()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            CheckForPlane(mousePosition);
        }
    }

    /* Identifies the first plane selected via raycast, stores it, stops de plane
     * recognition and deletes other detected planes
     */
    private void CheckForPlane(Vector3 position)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(position,hits,TrackableType.PlaneWithinPolygon))
        {
            currentPlane = (ARPlane)hits[0].trackable;
            foreach(ARPlane plane in planeManager.trackables)
            {
                if (!GameObject.ReferenceEquals(plane.gameObject, currentPlane.gameObject))
                {
                    GameObject.Destroy(plane.gameObject);
                }
            }
            planeManager.enabled = false;
            onSetup = false;
        }
    }

    /*
     * Restarts the behaviour in setup mode to identify planes
     */
    public void EnterSetup()
    {
        currentPlane = null;
        planeManager.enabled = true;
        onSetup = true;
    }

    /* Returns the current identified plane */
    public ARPlane GetCurrentPlane(){ return currentPlane; }

    /* Returns the setup state of the beheviour */
    public bool IsOnSetup() { return onSetup; }
}