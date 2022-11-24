using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDeviceCoroutine : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    [SerializeField] private float speed;

    enum DoorState {Open, Closed, Opening, Closing};

    private Vector3 _closedPosition;
    private Vector3 _openPosition;
    private Vector3 _targetPosition;
    private DoorState _doorState;


    void Start()
    {
      _closedPosition = transform.position;
      _openPosition = transform.position + dPos;
      _doorState = DoorState.Closed;
    }

    public void Operate()
    {
      switch (_doorState)
      {
        case DoorState.Closed:
          _doorState = DoorState.Opening;
          _targetPosition = _openPosition;
          StartCoroutine(MoveDoor());
          break;

        case DoorState.Open:
          _doorState = DoorState.Closing;
          _targetPosition = _closedPosition;
          StartCoroutine(MoveDoor());
          break;

        case DoorState.Closing:
          _doorState = DoorState.Opening;
          _targetPosition = _openPosition;
          break;

        case DoorState.Opening:
          _doorState = DoorState.Closing;
          _targetPosition = _closedPosition;
          break;
      }
    }

    public void Activate()
    {
      _targetPosition = _openPosition;
      if (_doorState == DoorState.Closed)
      {
        _doorState = DoorState.Opening;
        StartCoroutine(MoveDoor());
      }
      else
      {
        _doorState = DoorState.Opening;
      }
    }

    public void Deactivate()
    {
      _targetPosition = _closedPosition;
      if (_doorState == DoorState.Open)
      {
        _doorState = DoorState.Closing;
        StartCoroutine(MoveDoor());
      }
      else
      {
        _doorState = DoorState.Closing;
      }
    }

    private IEnumerator MoveDoor()
    {
      while((_targetPosition - transform.position).magnitude > 0.1)
      {
        MoveTowardsTarget();
        yield return null;
      }


      if(_doorState == DoorState.Opening)
      {
        _doorState = DoorState.Open;
      }
      else
      {
        _doorState = DoorState.Closed;
      }
    }

    private void MoveTowardsTarget()
    {
      Vector3 direction = _targetPosition - transform.position;
      direction = direction / direction.magnitude;
      transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

}
