using UnityEngine;
using UnityEngine.UI;

public class SoundButtonData : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Image currentImage;
    
    private float savedVolume;
    private bool stateOfVolume;

    public float ChangeVolume(float currentVolume)
    {
        float returnedVolume;
        
        if (!stateOfVolume)
        {
            savedVolume = currentVolume;
            currentImage.sprite = soundOff;
            returnedVolume = -1;
        }
        else
        {
            currentImage.sprite = soundOn;
            returnedVolume = savedVolume;
        }
        
        stateOfVolume = !stateOfVolume;
        return returnedVolume;
    }
}