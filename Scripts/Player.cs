using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int initialWeight = 120;
    private int weight;

    public int Weight
    {
        get { return weight; }
    }

	// Use this for initialization
	void Start () {
        weight = initialWeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check with what did we collide?
        if(collision.tag == "Enemy")
        {
            // If it's an enemy we kill the player
            GameManager.Instance.KillPlayer();
        }
        else if (collision.tag == "GoodFood")
        {
            // If it's good food then we update our weight, remove the food object and play a sound
            weight += 2;
            collision.gameObject.GetComponent<ObjMovement>().Kill();
            GetComponent<AudioSource>().Play();
        }
        else if (collision.tag == "BadFood")
        {
            // If it's bad food then we update our weight, remove the food object and play a sound
            weight -= 3;
            weight = Mathf.Max(weight, 120);
            collision.gameObject.GetComponent<ObjMovement>().Kill();
        }
    }

    public void Init()
    {
        weight = initialWeight;
    }
}
