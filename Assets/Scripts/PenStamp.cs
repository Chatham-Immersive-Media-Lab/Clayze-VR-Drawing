using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clayze.Marching.Operations;
using Unity.VisualScripting;

namespace Clayze.Marching
{
    public class PenStamp : MonoBehaviour
    {

        public float radius;
        private float _distanceFromCamera = 5;
        public float maxSphereDistanceFromCamera;
        public float maxSphereSize;
        private OperationType _opType = OperationType.Add;
        [SerializeField] private OperationCollection opCollection;

        public Transform penPoint;

        private bool didPress;
        // Start is called before the first frame update
        void Start()
        {
            didPress = false;
        }

        // Update is called once per frame
        void Update()
        {
            var pos = penPoint.position;
            
            transform.position = pos;
        
			
            // if (Input.GetAxis("XRI_Right_Trigger") > 0 && didPress == false)
            // {
            //     didPress = true;
            //     _opType = OperationType.Add;
            //     
            //     Stamp();
            //     
            //     Debug.Log("Draw");
            // }
            // else
            // {
            //     didPress = false;
            // }
            
            if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
            {
                if (Input.GetMouseButton(1))
                {
                    radius += Input.mouseScrollDelta.y;
                    radius = Mathf.Clamp(radius, 0, maxSphereSize);
                }
                else
                {
                    _distanceFromCamera += Input.mouseScrollDelta.y;
                    _distanceFromCamera = Mathf.Clamp(_distanceFromCamera, 1, maxSphereDistanceFromCamera);
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetMouseButton(1))
                {
                    _opType = OperationType.Remove;
                }
                else
                {
                    _opType = OperationType.Add;
                }
				
                Stamp();
            }
            
        }

       
        [ContextMenu("Stamp")]
        private void Stamp()
        {
            SphereOp op = new SphereOp(transform.position,radius, _opType);
            opCollection.Add(op);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,radius);
        }
    }
}
