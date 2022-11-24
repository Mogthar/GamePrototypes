using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] int[] testArray;
    // Start is called before the first frame update
    void Start()
    {
      Debug.Log(testArray.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
