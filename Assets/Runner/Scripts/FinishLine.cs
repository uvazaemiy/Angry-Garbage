using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// Ends the game on collision, forcing a win state.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class FinishLine : Spawnable
    {
        const string k_PlayerTag = "Player";

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag(k_PlayerTag))
            {
                StartCoroutine(GameManager.Instance.Win());
            }
        }
    }
}