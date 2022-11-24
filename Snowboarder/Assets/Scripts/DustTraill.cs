using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTraill : MonoBehaviour
{
    [SerializeField] ParticleSystem dustEffect;

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            dustEffect.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            dustEffect.Stop();
        }
    }
}
