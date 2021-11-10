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

    //Food Prefab
    public GameObject foodPrefab;
    //Borders
    public Transform LeftBorder;
    public Transform RightBorder;    
    public Transform TopBorder;
    public Transform BottomBorder;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.2f, 0.2f);
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
        Debug.Log("Save Head position: " + v);

        transform.Translate(dir);
        Debug.Log("Move Head to" + transform.position);
        // if ate 
        if (ate) {
            Debug.Log("Hit Food at " + transform.position);
				// Load Prefab into the world
                Debug.Log("grow snake at" + v);
				GameObject g = (GameObject)Instantiate (tailPrefab,
					              v,
					              Quaternion.identity);

				// Keep track of it in our tail list
				tail.Insert (0, g.transform);

				// Reset the flag
				ate = false;
			} 
        else if (tail.Count > 0) {	// Do we have a Tail?
					// Move last Tail Element to where the Head was
					tail.Last().position = v;
                    Debug.Log("Move tail to old head positon" + v);

					// Add to front of list, remove from the back
					tail.Insert(0, tail.Last());
					tail.RemoveAt(tail.Count - 1);
			
        }
    }
    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("Collision");
        if (coll.name.StartsWith("food")) {
			// Get longer in next Move call
			ate = true;

			// Remove the Food
			Destroy(coll.gameObject);
            Spawn();
		}
        else {
            Debug.Log(
                "game Over"
            );
            // You Lose!!
        }
    }

    void Spawn() {
        int x = (int)Random.Range(LeftBorder.position.x, RightBorder.position.x);
        int y = (int)Random.Range(BottomBorder.position.y, TopBorder.position.y);

        Debug.Log("New Food at " + Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity).transform.position);
    }
}
