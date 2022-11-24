using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private int movementDirection = 1;
    private float initialXPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialXPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime, 0, 0);
        if((transform.position.x - initialXPosition) > distance || (transform.position.x - initialXPosition) < 0)
        {
            movementDirection *= -1;
            transform.Translate(movementDirection * speed * Time.deltaTime, 0, 0);
        }
    }
}
