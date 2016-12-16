using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countTextUI;
    public Text winTextUI;



    private Rigidbody2D mRigidBody2D;
    private int mCollectibleCount;

	// Use this for initialization
	void Start () {

        mRigidBody2D = GetComponent<Rigidbody2D>();
        mCollectibleCount = 0;
        setCountText();
        winTextUI.text = "";
        
		
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
            ++mCollectibleCount;
            setCountText();
        }
    }

    void setCountText()
    {
        countTextUI.text = "Count: " + mCollectibleCount.ToString();

        if(mCollectibleCount >= 10)
        {
            winTextUI.text = "You win !!";
        }
    }
}
