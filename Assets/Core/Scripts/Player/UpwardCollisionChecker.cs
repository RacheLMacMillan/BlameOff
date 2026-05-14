using UnityEngine;

public class UpwardCollisionChecker : MonoBehaviour
{
    [SerializeField] private float directionalThreshold = 0.9f;

    private float normal;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contactPoint in collision.contacts)
        {
            normal = Vector3.Dot(contactPoint.normal, Vector3.up);
            
            if (normal > directionalThreshold)
            {
                Debug.Log("Object was hit from above by: " + collision.gameObject.name);
                
                break;
            }
        }
    }
}
