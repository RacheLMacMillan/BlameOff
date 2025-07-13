using System;
using UnityEngine;

public class PlayerLooker : MonoBehaviour, IInitializable<Player>
{
    private float _xSensitivity = 10;
    private float _ySensitivity = 10;
    private float _minVerticalRotation = -90;
    private float _maxVerticalRotation = 90;

    private Camera _camera;

    private float _rotationByY;
    
    public void Initialize(Player player)
    {
        _camera = player.Camera;
    }
    
    public void Look(Vector2 delta)
    {
        _rotationByY -= delta.y * _ySensitivity * Time.deltaTime;
        _rotationByY = Math.Clamp(_rotationByY, _minVerticalRotation, _maxVerticalRotation);

        _camera.transform.localRotation = Quaternion.Euler(_rotationByY, 0, 0);

        transform.Rotate(Vector3.up * delta.x * _xSensitivity * Time.deltaTime);
    }
}