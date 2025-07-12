using UnityEngine;

public class PlayerLooker : MonoBehaviour, IInitializable<Player>
{
    private float _xSensitivity;
    private float _ySensitivity;
    private float _minVerticalRotation;
    private float _maxVerticalRotation;

    private Camera _camera;

    private float _rotationByY;
    
    public void Initialize(Player player)
    {
        // _camera = player.Camera
    }
    
    public void Look(Vector2 delta)
    {
        
    }
}