using System;
using Core.Scripts.Player.Input;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
    public class PlayerLieDowner : MonoBehaviour
    {
        [field: SerializeField] public float LayingHeight { get; private set; } 
        [field: SerializeField] public Vector3 LayingCenter { get; private set; } 

        public Action OnPlayerLayDown;
        
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _playerInput.OnLieDownInputted += Liedown;
        }

        private void OnDisable()
        {
            _playerInput.OnLieDownInputted -= Liedown;
        }

        private void Liedown()
        {
            OnPlayerLayDown?.Invoke();
        }
    }
}