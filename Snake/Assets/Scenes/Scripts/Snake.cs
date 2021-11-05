using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    //sets automatically move right
    Vector2 dir = Vector2.right;

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();

    //Did the snake just eat?
    bool ate = false;
    public GameObject tailPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //check if player changed direction
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    // Move the snake one spot
    void Move() {
        //save where head is
        Vector2 v = transform.position;

        transform.Translate(dir);
        // if ate 
        if (ate) {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
            tail.Insert(0, g.transform);

            ate = false;
        }
        //Move the tail
        else if (tail.Count > 0) {
            //move end to front
            tail.Last().position = v;

             // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count-1);
        }
    }
    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("Collision");
        if (coll.name.StartsWith("foodPrefab")) {
            Debug.Log("Collision with food");
            ate = true;
            //Destroy the food
            Destroy(coll.gameObject);
        }
        else {
            // You Lose!!
        }
    }
}
