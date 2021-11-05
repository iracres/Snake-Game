using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    //Food Prefab
    public GameObject foodPrefab;
    //Borders
    public Transform LeftBorder;
    public Transform RightBorder;    
    public Transform TopBorder;
    public Transform BottomBorder;



    // Start is called before the first frame update
    void Start() {
        //Create food every 4 seconds, starting at 3
        InvokeRepeating("Spawn", 3, 4);
        
    }

    void Spawn() {
        int x = (int)Random.Range(LeftBorder.position.x, RightBorder.position.x);

        int y = (int)Random.Range(TopBorder.position.y, BottomBorder.position.y);


        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity);
    }

}
