using UnityEngine;

namespace FPS
{
    public class Keypad : Interactable
    {
        protected override void Interact()
        {
            Debug.Log("Interacted with Keypad");
        }
    }
}