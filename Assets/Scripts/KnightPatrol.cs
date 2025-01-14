using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
// ReSharper disable InconsistentNaming

public class KnightPatrol : MonoBehaviour
{
     #region Variables
     [Header("WayPoints")]
     [SerializeField]
     private Vector3[] _points = new Vector3[] { Vector3.zero };
    
    [Header("Platform Behavior")]
    [SerializeField]
    private float _speed = 2f;
    private int _currentIndex = 0;
    [SerializeField]
    private bool _isLooping = true;
    private bool _isReversing = false;
    
    [Header("WayPoint Pausing")]
    [SerializeField]
    private bool _willPause = false;
    private bool _isPaused = false;
    [SerializeField]
    private float _pauseTime;
    private WaitForSeconds _pauseTimer;


    #endregion
    
    private void Start()
    {
        transform.position = _points[0];
        _pauseTimer = new WaitForSeconds(_pauseTime);
        _currentIndex = 1;
    }
    
    private void Update()
    {
        // If there are no waypoints, exit early to avoid unnecessary calculations.
        if (_points.Length == 0) return;
    
        // If the platform is paused, exit early.
        if (_isPaused) return;
    
        CalculateMovement();
    
        // Check if the platform is very close to the current waypoint.
        if (!(Vector3.Distance(transform.position, _points[_currentIndex]) < 0.01f)) return;
        // If pausing at waypoints is enabled, start the pause routine.
        if (_willPause)
            StartCoroutine(PauseRoutine());
    
        CalculateNextPoint();
    }
    
    private void CalculateMovement()
    {
        // Move the platform towards the current waypoint at a specified speed.
        transform.position = Vector3.MoveTowards(transform.position, _points[_currentIndex], _speed * Time.deltaTime);
    }
    
    private void CalculateNextPoint()
    {
        // Handle movement behavior based on the current looping/reversing state.
        if (_isLooping) // If looping is enabled, wrap around to the first waypoint when reaching the end.
        {
            _currentIndex = (_currentIndex + 1) % _points.Length; // Move to the next waypoint and loop back if necessary.
        }
        else if (_isReversing) // If reversing, move backward through the waypoints.
        {
            _currentIndex--; // Move to the previous waypoint.
            if (_currentIndex >= 0) return; // If the beginning of the array is reached...
            _isReversing = false; // Stop reversing.
            _currentIndex = 1; // Move to the second waypoint.
        }
        else // Move forward through the waypoints in normal order.
        {
            _currentIndex++; // Move to the next waypoint.
            if (_currentIndex != _points.Length) return; // If the end of the array is reached...
            _isReversing = true; // Start reversing.
            _currentIndex = _points.Length - 2; // Move to the second-to-last waypoint.
        }
    }

    private IEnumerator PauseRoutine()
    {
        _isPaused = true;
        yield return _pauseTimer;
        _isPaused = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (var i = 0; i < _points.Length; i++)
        {
            if (i < _points.Length - 1)
            {
                Gizmos.DrawLine(_points[i], _points[i+1]); 
            }
            else
            {
                Gizmos.DrawLine(_points[^1], _points[0]); 
            }
        }
    }
}

