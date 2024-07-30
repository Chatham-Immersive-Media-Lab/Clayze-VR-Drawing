using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpFollow : MonoBehaviour
{
    
    public Transform TrackedHandle;

    private Transform PumpBase;
    
    // Start is called before the first frame update
    void Start()
    {
        PumpBase = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PumpBase.position = new Vector3(TrackedHandle.position.x, PumpBase.position.y, TrackedHandle.position.z);

        var RotatedX = TrackedHandle.rotation.x - 90;
        
        PumpBase.rotation = new Quaternion(RotatedX, TrackedHandle.rotation.y, TrackedHandle.rotation.z,
            TrackedHandle.rotation.w);
    }
}
