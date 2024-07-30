using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clayze.Marching.Operations;

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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            var pos = penPoint.position;
            
            transform.position = pos;

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
