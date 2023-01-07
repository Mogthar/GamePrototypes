using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField] float _speed;
    [SerializeField] float _paddingLeft;
    [SerializeField] float _paddingRight;
    [SerializeField] float _paddingBottom;
    [SerializeField] float _paddingTop;
    Vector2 _minimumBound;
    Vector2 _maximumBound;

    Shooter shooter;
    // Update is called once per frame
    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 delta = rawInput * _speed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, _minimumBound.x + _paddingLeft, _maximumBound.x - _paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, _minimumBound.y + _paddingBottom, _maximumBound.y- _paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }



    void InitBounds()
    {
        _minimumBound = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        _maximumBound = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

    }
}
