using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DrawingInput : MonoBehaviour
{
    public SteamVR_Action_Boolean cursorAppear;

    public SteamVR_Action_Single cursorDraw;

    
    public SteamVR_Input_Sources handType;

    public GameObject cursor;
    public Transform cursorPos;
    
    
    // Start is called before the first frame update
    void Start()
    {

        cursorAppear.AddOnStateDownListener(TriggerPress, handType);
        cursorAppear.AddOnStateUpListener(TriggerRelease, handType);
        
        cursorDraw.AddOnAxisListener(TriggerHold, handType);

        cursor.GetComponent<MeshRenderer>().enabled = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        cursor.gameObject.transform.position = cursorPos.position;
    }

    
    public void TriggerPress(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {
        cursor.GetComponent<MeshRenderer>().enabled = true;
    }
    
    public void TriggerRelease(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
    {
        cursor.GetComponent<MeshRenderer>().enabled = false;
    }

    public void TriggerHold(SteamVR_Action_Single action, SteamVR_Input_Sources source, float a, float b)
    {
    }
}
