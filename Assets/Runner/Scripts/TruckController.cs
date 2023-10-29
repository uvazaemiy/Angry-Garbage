using System.Collections;
using DG.Tweening;
using HyperCasual.Runner;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    public static TruckController instance;
    
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator Move()
    {
        transform.DOMoveZ(800, 10).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(3);
    }
}
