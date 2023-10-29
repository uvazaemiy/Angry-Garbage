using UnityEngine;
using UnityEngine.UI;

public class LevelNameScript : MonoBehaviour
{
    public static LevelNameScript instance;

    [SerializeField] private Text levelText;

    private void Awake()
    {
        instance = this;
    }

    public void SetText(string levelNumber)
    {
        levelText.text = levelNumber;
    }
    
}
