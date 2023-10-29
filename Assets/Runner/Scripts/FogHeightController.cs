using UnityEngine;

public class FogHeightController : MonoBehaviour
{
    public float fogHeight = 5.0f; // Здесь устанавливается желаемая высота тумана

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetFloat("_FogHeight", fogHeight);
    }
}