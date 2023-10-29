using HyperCasual.Core;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public void ClickButton()
    {
        UIManager.Instance.MoveSettingsButtons();
    }
}
