using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class SetupPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject AROrigin;

    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;

    ARPlane currentPlane;

    void Start()
    {
        raycastManager = AROrigin.GetComponent<ARRaycastManager>();
        planeManager = AROrigin.GetComponent<ARPlaneManager>();
        currentPlane = null;
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }

    public void Update()
    {
        MouseDown();
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;
        CheckForPlane(finger.currentTouch.screenPosition);
    }

    private void MouseDown()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            CheckForPlane(mousePosition);
        }
    }

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
        }
    }

    public ARPlane getCurrentPlane(){ return currentPlane; }
}