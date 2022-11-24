using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    [SerializeField]
    private float openingSpeed = 1.5f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float theta = openingSpeed * Time.deltaTime;
        transform.Rotate(0, theta * 180.0f / 3.1415f, 0);
        Vector3 translation = new Vector3(0,0,theta * transform.localScale.x / 2);
        transform.Translate(translation);
    }
}
