using System;
using Core.Scripts.Player.Input;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _distance = 3f;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private PlayerUI _playerUI;
        [SerializeField] private PlayerInput _playerInput;
        
        private Interactable _interactableObject;
        
        private void Awake()
        {
            _camera = GetComponentInChildren<Camera>();
            _playerUI = GetComponent<PlayerUI>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable() => _playerInput.OnInteractInputted += Interact;
        private void OnDisable() => _playerInput.OnInteractInputted -= Interact;

        private void Update()
        {
            _playerUI.UpdateText(string.Empty);

            FindInteractable();
        }

        private void Interact()
        {
            if (_interactableObject == null)
                throw new NullReferenceException("There's no objects to interact with.");
            
            _interactableObject.BaseInteract();
        }

        private void FindInteractable()
        {
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * _distance, Color.red);
            RaycastHit hitInfo;
            
            if (Physics.Raycast(ray, out hitInfo, _distance, _layerMask))
            {
                _interactableObject = hitInfo.collider.GetComponent<Interactable>();
                
                if (_interactableObject != null)
                    _playerUI.UpdateText(_interactableObject.promptMessage);
                
            }
            else
            {
                _interactableObject = null;
            }
        }
    }
}