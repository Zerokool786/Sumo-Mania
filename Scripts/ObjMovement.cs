using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovement : MonoBehaviour {

    private Vector2 direction;
    
    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public float LifeTime
    {
        get;
        set;
    }

    public Factory Factory
    {
        get;
        set;
    }

    private Rigidbody2D rb;
    private float curTime;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // Move the object based on the direction that's moving
        var pos = new Vector2(transform.position.x, transform.position.y);
        pos = pos + direction * Time.deltaTime;
        rb.MovePosition(pos);

        // Check if the object reached the end of the screen by converting the world position of the object to view port coordinates which go from 0 to 1.
        // So if the object is below 0 or above 1 that means it going outside the screen which we then fix by inverting the direction.
        var vpPos = Camera.main.WorldToViewportPoint(pos);


        // Check if it's going outside the screen on the right side
        if(vpPos.x > 1)
        {
            direction.x *= -1;
        }
        // Check the left side
        else if (vpPos.x < 0)
        {
            direction.x *= -1;
        }

        // Check if the lifetime for this object is done and if it is we kill the object
        if (curTime >= LifeTime)
        {
            Kill();
        }
        else
        {
            curTime += Time.deltaTime;
        }
	}

    public void Kill()
    {
        // Remove it from the factory and destroy it
        Factory.Kill(gameObject);
        Destroy(gameObject);
    }
}
