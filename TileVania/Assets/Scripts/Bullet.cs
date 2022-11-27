using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 20f;
    float xSpeed;
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        Vector2 initial_transform = new Vector2 (transform.localScale.x * player.transform.localScale.x, transform.localScale.y * player.transform.localScale.x);
        transform.localScale = initial_transform;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);   
    }
}
