using Core.Scripts.Player.Input;
using UnityEngine;

namespace Core.Scripts.Player.Movement
{
	public class PlayerLooker : MonoBehaviour
	{
		[SerializeField] private float _sensitivityByX;
		[SerializeField] private float _sensitivityByY;
		[SerializeField] private float _minVerticalRotation;
		[SerializeField] private float _maxVerticalRotation;
		private PlayerInput _playerInput;
		private Camera _camera;
	
		private float _rotationByY;
	
		private void Awake()
		{
			_playerInput = GetComponent<PlayerInput>();
			_camera = GetComponentInChildren<Camera>();
		}

		private void OnEnable() => _playerInput.OnLookInputted += Look;
		private void OnDisable() => _playerInput.OnLookInputted -= Look;

		private void Look(Vector2 mouseDelta)
		{
			float mouseByX = mouseDelta.x;
			float mouseByY = mouseDelta.y;
		
			_rotationByY -= (mouseByY * Time.deltaTime) * _sensitivityByY;
			_rotationByY = Mathf.Clamp(_rotationByY, _minVerticalRotation, _maxVerticalRotation);
		
			_camera.transform.localRotation = Quaternion.Euler(_rotationByY, 0, 0);
		
			transform.Rotate(Vector3.up * (mouseByX * Time.deltaTime) * _sensitivityByX);
		}
	}
}