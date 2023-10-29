using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour 
{
    private float fps;
    private Text counterText;


    private void Start()
    {
        counterText = GetComponent<Text>();
    }

    void Update() 
    {
        fps = 1.0f / Time.deltaTime;
        counterText.text = fps.ToString();
    }
}