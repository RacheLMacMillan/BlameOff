using System;
using UnityEngine;

public class PlayerLooker : MonoBehaviour, IInitializable<Player>
{
    private float _xSensitivity = 5;
    private float _ySensitivity = 5;
    private float _minVerticalRotation = -90;
    private float _maxVerticalRotation = 90;

    private Player _player;
    private Camera _camera;

    private float _rotationByY;

    public event Action<float, float> OnSensitivityChanged;

    private void OnEnable() => _player.OnCameraSettingsChanged += SetNewSettings;
    private void OnDisable() => _player.OnCameraSettingsChanged -= SetNewSettings;

    public void Initialize(Player player)
    {
        _player = player;
        _camera = player.Camera;
        
        SetNewSettings(_player.XSensitivity, _player.YSensitivity);
    }
    
    public void Look(Vector2 delta)
    {
        _rotationByY -= delta.y * _ySensitivity * Time.deltaTime;
        _rotationByY = Math.Clamp(_rotationByY, _minVerticalRotation, _maxVerticalRotation);

        _camera.transform.localRotation = Quaternion.Euler(_rotationByY, 0, 0);

        transform.Rotate(Vector3.up * delta.x * _xSensitivity * Time.deltaTime);
    }
    
    private void SetNewSettings(float xSensitivity, float ySensitivity)
    {
        _xSensitivity = xSensitivity;
        _ySensitivity = ySensitivity;

        OnSensitivityChanged?.Invoke(xSensitivity, ySensitivity);
    }
}