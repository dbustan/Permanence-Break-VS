using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    const KeyCode PAUSE_KEY = KeyCode.Escape;
    bool isPaused;
    [SerializeField] GameObject pauseMenu;

    void Update() {
        if (Input.GetKey(PAUSE_KEY)) {
            if (!isPaused) PauseGame();
            else UnpauseGame();
        }
    }

    void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        isPaused = true;
    }

    void UnpauseGame() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        isPaused = false;
    }
}
