using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            int currentSceneName = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene((currentSceneName + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }
}
