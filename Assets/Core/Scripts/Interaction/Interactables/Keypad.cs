using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField] private Material material;
    
    protected override void Interact()
    {
        base.Interact();
        
        GetComponent<Material>().color = material.color;
    }
}