using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement actions")]
    public Action<Vector3> OnMoveInputted;
    public Action<Vector2> OnLookInputted;
    public Action OnCrouchInputted;
    public Action OnLiedownInputted;
    public Action OnSprintInputted;
    public Action OnClimbInputted;
    public Action OnJumpInputted;
          
    [Header("Weapon interactions")]
    public Action OnShootInputted;
    public Action OnZoomInputted;
    public Action OnMeleeInputted;
    public Action OnReloadInputted;
    public Action OnCheckMagazineInputted;
    
    [Header("Equipment interactions")]
    public Action OnThrowGrenadeInputted;
    public Action OnQuickHealInputted;
 
    [Header("Common actions")]
    public Action OnInteractInputted;

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

    private void Update()
    {
        CheckForInput();
    }
    
    private void OnEnable() => _inputMap.Enable();
    private void OnDisable() => _inputMap.Disable();
    
    private void CheckForInput()
    {
        if (_onFoot.Move.ReadValue<Vector2>() != Vector2.zero)
            OnMoveInputted?.Invoke(ScaleDirection(_onFoot.Move.ReadValue<Vector2>()));
        if (_onFoot.Look.ReadValue<Vector2>() != Vector2.zero)
            OnLookInputted?.Invoke(_onFoot.Look.ReadValue<Vector2>());
    }
    
    private void SetMovementAction()
    {
        _onFoot.Crouch.performed += context => OnCrouchInputted?.Invoke();
        _onFoot.Liedown.performed += context => OnLiedownInputted?.Invoke();
        _onFoot.Sprint.performed += context => OnSprintInputted?.Invoke();
        _onFoot.Climb.performed += context => OnClimbInputted?.Invoke();
        _onFoot.Jump.performed += context => OnJumpInputted?.Invoke();
    }
    
    private void SetWeaponInteraction()
    {
        _onFoot.Shoot.performed += context => OnShootInputted?.Invoke();
        _onFoot.Zoom.performed += context => OnZoomInputted?.Invoke();
        _onFoot.Melee.performed += context => OnMeleeInputted?.Invoke();
        _onFoot.Reload.performed += context => OnReloadInputted?.Invoke();
        _onFoot.Reload.canceled += context => OnCheckMagazineInputted?.Invoke();
    }
    
    private void SetEquipmentInteraction()
    {
        _onFoot.ThrowGrenade.performed += context => OnThrowGrenadeInputted?.Invoke();
        _onFoot.QuickHeal.performed += context => OnQuickHealInputted?.Invoke();
    }
    
    private void SetCommonAction()
    {
        _onFoot.Interact.performed += context => OnInteractInputted?.Invoke();
    }
    
    private Vector3 ScaleDirection(Vector2 direction)
    {
        return new Vector3(direction.x, 0, direction.y);
    }
}