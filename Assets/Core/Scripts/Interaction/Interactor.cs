using UnityEngine;

namespace Core.Scripts.Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private string _promptMessage;

        public void BaseInteract()
        {
            Interact();
        }

        protected virtual void Interact()
        {
            
        }
    }
}