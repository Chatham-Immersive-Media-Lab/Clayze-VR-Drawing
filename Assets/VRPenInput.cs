using System.Collections;
using System.Collections.Generic;
using Clayze.Ink;
using SyncedProperty;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class VRPenInput : MonoBehaviour
{
    public SyncColor color;
    public float BaseThickness;
    public InkManager3D InkManager;
    [FormerlySerializedAs("DrawReference")] public InputActionReference DrawInput;//0 is off, 1 is on full, 0.5 if half thick

    [Header("Pen Settings")]
    [Tooltip("Distance pen must move in world space beffore a new point is added.")]
    [SerializeField]
    private float minRadius = 0.1f;

    [Tooltip("Minimum time after previous point before a new point is added.")] [SerializeField]
    private float _minTime = (1f / 50f);

    private float _lastAddTime = Mathf.Infinity;
    
    private Stroke3 _currentStroke;
    // Start is called before the first frame update
    void Start()
    {
        DrawInput.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _lastAddTime += Time.deltaTime;
        var input = DrawInput.action.ReadValue<float>();
        CheckPenInputTick(input);
        CheckPenDragTick(input);
        CheckPenReleaseTick(input);
    }

  
    private void CheckPenInputTick(float input)
    {
        //press
        if (input > 0 && _currentStroke == null)
        {
            _currentStroke = InkManager.StartStroke(0, true, color.Value, BaseThickness);
        }
    }
    private void CheckPenDragTick(float input)
    {
        //drag
        if (input > 0 && _currentStroke != null)
        {
            if (_lastAddTime >= _minTime)
            {
                if (_currentStroke.Points.Count > 1)
                {
                    var d = InkPoint3.Distance(_currentStroke.Points[^1], transform.position);
                    if (d < minRadius || d == 0)
                    {
                        //quit if distance is too short.
                        return;
                    }
                }

                _currentStroke.AddPoint(NewPointAtCurrent(input));
                _lastAddTime = 0;
            }
            else
            {
                //not enough time to stamp new point, move last point to here.
                _currentStroke.UpdateLastPoint(NewPointAtCurrent(input));
            }
        }
    }

    private void CheckPenReleaseTick(float input)
    {
        if (input <= 0 && _currentStroke != null)
        {
            _currentStroke.Finish();
            _currentStroke = null;
        }
    }
    

    private InkPoint3 NewPointAtCurrent(float width)
    {
        byte w = (byte)(Mathf.Clamp01(width)*255);
        return new InkPoint3(transform.position, w);
    }
}
