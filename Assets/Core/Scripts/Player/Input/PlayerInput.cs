using System;
using UnityEngine;

namespace Core.Scripts.Player.Input
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Movement actions")]
        public Action<Vector3> OnMoveInputted;
        public Action<Vector2> OnLookInputted;
        public Action OnCrouchInputted;
        public Action OnLieDownInputted;
        public Action OnSprintInputted;
        public Action OnClimbInputted;
        public Action OnJumpInputted;
          
        [Header("Weapon interactions")]
        public Action OnShootInputted;
        public Action OnAltShootInputted;
        public Action OnMeleeInputted;
        public Action OnReloadInputted;
        public Action OnCheckMagazineInputted;
    
        [Header("Equipment interactions")]
        public Action OnThrowGrenadeInputted;
        public Action OnQuickHealInputted;
 
        [Header("Common actions")]
        public Action OnInteractInputted;

        [Header("Values")] 
        private Vector3 ScaledMoveInputDirection => ScaleDirection(_onFoot.Move.ReadValue<Vector2>());
        private Vector2 LookInputDirection => _onFoot.Look.ReadValue<Vector2>();

        [Header("Input map")]
        private InputMap _inputMap;
        private InputMap.OnFootActions _onFoot;
    
        private void Awake()
        {
            _inputMap = new InputMap();
            _onFoot = _inputMap.OnFoot;
        
            SetMovementAction();
            SetCommonAction();
            SetWeaponInteraction();
            SetEquipmentInteraction();
        }
        // ReSharper disable Unity.PerformanceAnalysis
        private void Update()
        {
            CheckForInput();
        }
    
        private void OnEnable() => _inputMap.Enable();
        private void OnDisable() => _inputMap.Disable();
    
        private void CheckForInput()
        {
            if (_onFoot.Move.ReadValue<Vector2>() != Vector2.zero)
                OnMoveInputted?.Invoke(ScaledMoveInputDirection);
            if (_onFoot.Look.ReadValue<Vector2>() != Vector2.zero)
                OnLookInputted?.Invoke(LookInputDirection);
        }
    
        private void SetMovementAction()
        {
            _onFoot.Crouch.performed += _ => OnCrouchInputted?.Invoke();
            _onFoot.Liedown.performed += _ => OnLieDownInputted?.Invoke();
            _onFoot.Sprint.performed += _ => OnSprintInputted?.Invoke();
            _onFoot.Climb.performed += _ => OnClimbInputted?.Invoke();
            _onFoot.Jump.performed += _ => OnJumpInputted?.Invoke();
        }
    
        private void SetWeaponInteraction()
        {
            _onFoot.Shoot.performed += _ => OnShootInputted?.Invoke();
            _onFoot.AltShoot.performed += _ => OnAltShootInputted?.Invoke();
            _onFoot.Melee.performed += _ => OnMeleeInputted?.Invoke();
            _onFoot.Reload.performed += _ => OnReloadInputted?.Invoke();
            _onFoot.Reload.canceled += _ => OnCheckMagazineInputted?.Invoke();
        }
    
        private void SetEquipmentInteraction()
        {
            _onFoot.ThrowGrenade.performed += _ => OnThrowGrenadeInputted?.Invoke();
            _onFoot.QuickHeal.performed += _ => OnQuickHealInputted?.Invoke();
        }
    
        private void SetCommonAction()
        {
            _onFoot.Interact.performed += _ => OnInteractInputted?.Invoke();
        }
    
        private static Vector3 ScaleDirection(Vector2 direction)
        {
            return new Vector3(direction.x, 0, direction.y);
        }
    }
}