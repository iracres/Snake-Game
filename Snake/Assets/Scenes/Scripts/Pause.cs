using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused;
    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Space)) {
        isPaused = !isPaused;
    }
    if (isPaused) {
        ActivateMenu();
    }
    else {DeactivateMenu();}
    }

    void ActivateMenu() {
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
            

    }
    void DeactivateMenu() {
            Time.timeScale = 1;
            pauseMenuUI.SetActive(false);

    }
}
