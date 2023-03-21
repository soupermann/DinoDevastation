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
        SceneManager.LoadScene("Difficulty1v1", LoadSceneMode.Single);
    }
    public void GM2() {
        SceneManager.LoadScene("Difficulty2v2", LoadSceneMode.Single);
    }
    public void Normal2v2() {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    public void Hardcore2v2() {
        SceneManager.LoadScene("GameScene2v2Hardcore", LoadSceneMode.Single);
    }
    public void Normal1v1() {
        SceneManager.LoadScene("GameScene1v1", LoadSceneMode.Single);
    }
    public void Hardcore1v1() {
        SceneManager.LoadScene("HardCoreNew1v1", LoadSceneMode.Single);
    }
    public void Exit() {
        Application.Quit();
    }
    public void Exit2Menu() {
        SceneManager.LoadScene("Initial Scene", LoadSceneMode.Single);
    }
    public void instructionsMenu() {
        SceneManager.LoadScene("Instructions", LoadSceneMode.Single);
    }

}
