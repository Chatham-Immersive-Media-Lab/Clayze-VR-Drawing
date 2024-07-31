using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePump : MonoBehaviour
{

    public bool pumpExtended;

    public Transform pumpHandle;

    public Transform prefabToScale;

    private Vector3 currentScale;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentScale = prefabToScale.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (pumpHandle.position.y >= .9 && pumpHandle.position.y < 1.0)
        {
            pumpExtended = true;
        }
        else if (pumpHandle.position.y >= 1.0)
        {
            pumpExtended = false;
        }

        if (pumpExtended == true)
        {
            var targetScale = currentScale + new Vector3(1, 1, 1);
            
            float i = Mathf.InverseLerp(1.0f, 0.5f, pumpHandle.position.y);
            
            Debug.Log(i);
            
            //prefabToScale.localScale += new Vector3(i,i,i);

            prefabToScale.localScale = Vector3.Lerp(currentScale, targetScale, i);

            if (pumpHandle.position.y <= .575)
            {
                currentScale = prefabToScale.localScale;
                pumpExtended = false;
            }
        }
    }
}
