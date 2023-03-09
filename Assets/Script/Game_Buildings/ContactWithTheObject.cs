using System;
using UnityEngine;
using UnityEngine.Events;

namespace Building
{
    public class ContactWithTheObject : MonoBehaviour
    {

        public UnityEvent<GameObject> EventToContact { get; set; } = new UnityEvent<GameObject>();

        private void OnTriggerEnter(Collider other)
        {
            EventToContact?.Invoke(other.gameObject);
        }

        private void OnDestroy()
        {
            EventToContact.RemoveAllListeners();
        }
    }
}