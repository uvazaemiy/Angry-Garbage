using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HyperCasual.Gameplay;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing a Spawnable object.
    /// If a GameObject tagged "Player" collides
    /// with this object, it will be collected, 
    // incrementing the player's amount of this item.
    /// </summary>
    public class Collectable : Spawnable
    {
        [SerializeField]
        
        const string k_PlayerTag = "Player";

        [SerializeField] private Collider[] RagDollColliders;
        public Rigidbody[] RagDollRigidBodies;
        [Space]
        [SerializeField] private Collider[] MainColliders;
        public ItemPickedEvent m_Event;
        public int m_Count;

        public bool m_Collected;
        Renderer[] m_Renderers;

        /// <summary>
        /// Reset the gate to its initial state. Called when a level
        /// is restarted by the GameManager.
        /// </summary>
        public override void ResetSpawnable()
        {
            m_Collected = false;
            
            for (int i = 0; i < m_Renderers.Length; i++)
            {
                m_Renderers[i].enabled = true;
            }

            foreach (Collider collider in MainColliders)
                collider.enabled = true;

            foreach (Collider collider in RagDollColliders)
            {
                collider.enabled = false;
                collider.gameObject.layer = 3;
            }
            
            foreach (Rigidbody rb in RagDollRigidBodies)
                rb.isKinematic = true;
        }

        protected override void Awake()
        {
            base.Awake();

            m_Renderers = gameObject.GetComponentsInChildren<Renderer>();
        }

        public void Collect(bool isRight, Vector3 TrashPosition)
        {
            foreach (Collider collider in MainColliders)
                collider.enabled = false;
            
            foreach (Collider collider in RagDollColliders)
                collider.enabled = true;

            foreach (Rigidbody rb in RagDollRigidBodies)
                rb.isKinematic = false;
            
            if (m_Event != null && isRight)
                StartCoroutine(GoToTrash(TrashPosition));
            else
                AddedAudioManager.instance.PlayKick();

            m_Collected = true;
        }

        private IEnumerator GoToTrash(Vector3 TrashPosition)
        {
            AddedAudioManager.instance.PlayCollected();
            foreach (Rigidbody rb in RagDollRigidBodies)
                rb.useGravity = false;

            transform.DOMove(TrashPosition + new Vector3(0, 0.1f, 2f), 0.45f);
            yield return new WaitForSeconds(0.1f);
            yield return transform.DOScale(Vector3.zero, 0.35f).WaitForCompletion();

            for (int i = 0; i < m_Renderers.Length; i++)
                m_Renderers[i].enabled = false;
            
            SliderGirls.instance.EncreaseSlider();
            m_Event.Count = m_Count;
            m_Event.Raise();
        }
    }
}