using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    // The prefab to be created
    public GameObject prefab;
    // Maximum number of elements on the screen for this factory
    public int maxNumOnScreen = 7;
    // Range of speed for the elements created by this factory (min speed and max speed)
    public float minSpeed = 0.5f;
    public float maxSpeed = 4;

    private List<GameObject> elements = new List<GameObject>();

	// Update is called once per frame
	void Update ()
    {
        // Only create new elements if we didn't reach the maximum number and also we add some random factor so that they don't get created one after the other
        if (elements.Count < maxNumOnScreen && Random.Range(0, 1f) > 0.98f)
        {
            var x = 0;
            // Probability of creating an object on the left side of the screen is 50%
            var left = Random.Range(0, 2);
            if (left == 0)
            {
                x = 1;
            }

            // Grab a number between 0 and 1 for the y coordinate
            var y = Random.Range(0, 1.0f);
            // Convert the position from viewport to world position so that it's easier to position elements on the edges of the screen
            var pos = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
            pos.z = 0;
            // Create the object
            var go = Instantiate(prefab, pos, Quaternion.identity);
            // Update the direction of the object. First we randomly decide if the element is going left or right and then we multiply by a random number between 0.5f and 4 to give a random speed
            var mov = go.GetComponent<ObjMovement>();
            mov.Direction = new Vector2((Random.Range(0, 2) * 2 - 1) * Random.Range(0.5f, 4), 0);
            // Lifetime for the object is a number between 5 and 10 seconds
            mov.LifeTime = Random.Range(5, 10);
            mov.Factory = this;

            // Add the object to the list
            elements.Add(go);
        }
    }

    public void Init()
    {
        // Destroy all elements in the list and clear it
        foreach(var el in elements)
        {
            Destroy(el);
        }

        elements.Clear();
    }

    public void Kill(GameObject go)
    {
        elements.Remove(go);
    }
}
