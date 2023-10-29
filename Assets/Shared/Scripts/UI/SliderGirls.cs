using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.UI;

public class SliderGirls : MonoBehaviour
{
    public static SliderGirls instance;
    
    private Slider slider;
    private int girlsCount;

    private void Start()
    {
        instance = this;
        
        slider = GetComponent<Slider>();
    }

    public void Init(int count)
    {
        girlsCount = count;
        slider.maxValue = girlsCount;
    }

    public void EncreaseSlider()
    {
        slider.value++;
    }

    public void DecreaseSlider(int count)
    {
        slider.value -= count;
    }

    public void ResetSlider()
    {
        slider.value = 15;
    }
}
