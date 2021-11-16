using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Exit : MonoBehaviour
{


    public void ExitGame() {
        Debug.Log("Ending game");
        Application.Quit();
    }

}
