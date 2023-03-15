using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void changeScene() {
        SceneManager.LoadScene("Initial Scene");
    }
    public void GM1() {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void GM2() {
        SceneManager.LoadScene("gm2", LoadSceneMode.Single);
    }
    public void Exit() {
        Application.Quit();
    }


}
