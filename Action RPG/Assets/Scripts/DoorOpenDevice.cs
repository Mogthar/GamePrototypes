using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    [SerializeField] private float speed;
    private bool _open = false;
    private bool _toggled = false;
    private float _distanceTravelled = 0.0f;
    private Vector3 _startPosition;

    void Start()
    {
      _startPosition = transform.position;
    }

    public void Operate()
    {
      _toggled = true;
      _open = !_open;
    }

    public void Activate()
    {
      _toggled = true;
      _open = true;
    }

    public void Deactivate()
    {
      _toggled = true;
      _open = false;
    }

    void Update()
    {
      if(_toggled)
      {
        if(_open)
        {
          _distanceTravelled += speed * Time.deltaTime;
          if(_distanceTravelled > dPos.magnitude)
          {
            _distanceTravelled = dPos.magnitude;
            _toggled = false;
          }
          transform.position = Vector3.Lerp(_startPosition, _startPosition + dPos, _distanceTravelled / dPos.magnitude);
        }
        else
        {
          _distanceTravelled -= speed * Time.deltaTime;
          if(_distanceTravelled < 0.0f)
          {
            _distanceTravelled = 0.0f;
            _toggled = false;
          }
          transform.position = Vector3.Lerp(_startPosition + dPos, _startPosition, 1.0f - _distanceTravelled / dPos.magnitude);
        }
      }
    }
}
