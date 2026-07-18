using System;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance = 3f;
        [SerializeField] private LayerMask _layerMask;

        private void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * _distance, Color.red);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, _distance, _layerMask))
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                
                if (interactable != null)
                {
                    Debug.Log(interactable.promptMessage);
                }
            }
        }
    }
}