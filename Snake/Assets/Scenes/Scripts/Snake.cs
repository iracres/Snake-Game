using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject GameOverUI;
    //sets automatically move right
    Vector2 dir = Vector2.right;
    private bool dead = false;
    public Text timerText; 

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();

    //Did the snake just eat?
    bool ate = false;
    public GameObject tailPrefab;
    public GameObject foodPrefab;
    //Borders
    public Transform LeftBorder;
    public Transform RightBorder;    
    public Transform TopBorder;
    public Transform BottomBorder;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //check if player changed direction
        if (Input.GetKey(KeyCode.RightArrow) && dir != Vector2.left)
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow) && dir != Vector2.up)
            dir = -Vector2.up;
        else if (Input.GetKey(KeyCode.LeftArrow) && dir != -Vector2.left)
            dir = -Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow) && dir != -Vector2.up)
            dir = Vector2.up;
    }

    // Move the snake one spot
    void Move() {
        //save where head is
        Vector2 v = transform.position;

        transform.Translate(dir);
        // if ate 
        if (ate) {
				// Load Prefab into the world
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
            Debug.Log("Game Over");
            dead = true;
            StartCoroutine(GameOver());
        }
    }


    IEnumerator GameOver() {    
        GameOverUI.SetActive(true);
        CancelInvoke();
        timerText.text = "restart in 3 seconds";
        yield return new WaitForSeconds(1);
        timerText.text = "restart in 2 seconds";
        yield return new WaitForSeconds(1);
        timerText.text = "restart in 1 seconds";
        yield return new WaitForSeconds(1);
        Application.LoadLevel(0);
    }

    void Spawn() {
        int x = (int)Random.Range(LeftBorder.position.x, RightBorder.position.x);
        int y = (int)Random.Range(BottomBorder.position.y, TopBorder.position.y);

        Debug.Log("New Food at " + Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity).transform.position);
    }

    void Pause() {
            Time.timeScale = 0;
    }
}
