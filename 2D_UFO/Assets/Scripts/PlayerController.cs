using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D mRigidBody2D;

	// Use this for initialization
	void Start () {

        mRigidBody2D = GetComponent<Rigidbody2D>();
		
	}

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        mRigidBody2D.AddForce(new Vector2(moveHorizontal, moveVertical) * speed);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
