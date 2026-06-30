using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class PlayerInteractor : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private float _distance = 3f;
    [SerializeField] private LayerMask _interactableLayer;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        
        Debug.DrawRay(ray.origin, ray.direction * _distance, Color.red);
        
        RaycastHit hit;
        
        Physics.Raycast(ray, out hit, _distance, _interactableLayer);

        if (Physics.Raycast(ray, out hit, _distance, _interactableLayer))
        {
            if (hit.collider.GetComponent<Interactable>() != null)
            {
                Debug.Log(hit.collider.GetComponent<Interactable>().promptMessage);
            }
        }
    }
}