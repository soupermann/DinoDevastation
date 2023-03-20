using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public Text StartingSpeech;
    private Vector2 originalPosition;
    public ButtonManager ButtonManager;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = StartingSpeech.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartingSpeech.GetComponent<RectTransform>().anchoredPosition.y < 3000) {
            StartingSpeech.GetComponent<RectTransform>().anchoredPosition += new Vector2(0f, 0.5f + Time.deltaTime);
        }
        if (StartingSpeech.GetComponent<RectTransform>().anchoredPosition.y > 3000) {
            ButtonManager.changeScene();
        }
    }
}
