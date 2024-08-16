using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DepthMovementInput : MonoBehaviour
{
    
    public SteamVR_Action_Boolean depthGrip;
    
    public SteamVR_Input_Sources handType;

    public bool gripPressed;

    public Transform cursorPos;
    
    // Start is called before the first frame update
    void Start()
    {
        depthGrip.AddOnStateDownListener(GripPress, handType);
        depthGrip.AddOnStateUpListener(GripRelease, handType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GripPress(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {
        gripPressed = true;
        Debug.Log("gripped");
    }

    public void GripRelease(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {
        gripPressed = false;
        Debug.Log("ungripped");
    }

    private void OnTriggerStay(Collider other)
    {
        if (gripPressed == true)
        {
            Debug.Log("should move");
            this.gameObject.transform.position = new Vector3(0, 0, other.gameObject.transform.position.z);
        } 
    }
}
