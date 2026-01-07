using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VRMain.Assets.Code.GamePlay.Character
{
    public class MovementController : MonoBehaviour
    {
        [Header("Movement parameters")]
        [SerializeField] private float _movementSpeed = 3f;
        [SerializeField] private float _sprintMultiplier = 2f;
        [SerializeField] private float _jumpForce = 50f;
        [SerializeField] private float _mouseSensitivity = 2f;
        [SerializeField] private float _upDownRange = 80f;
        [SerializeField] private Transform _cameraTransform;
        private float _verticalRotation = 0f;
        [SerializeField] private float _groundCheckDistance = 0.2f;
        [SerializeField] private LayerMask _groundLayer;
        private CharacterInput _input = null;
        private Vector2 _movementVector = Vector2.zero;
        private float _activeSpeedModifier = 1f;
        private Rigidbody _rigidbody = null;
        private bool _isLocked = false;
        public event Action OnInteractPressed;

        private static MovementController _instance;
        public static MovementController Singleton => _instance;

        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = new CharacterInput();

            _input.CharacterMap.Movement.performed += OnMovementPerformed;
            _input.CharacterMap.Movement.canceled += OnMovementCancelled;
            _input.CharacterMap.Sprint.performed += OnSprintPerformed;
            _input.CharacterMap.Sprint.canceled += OnSprintCancelled;
            _input.CharacterMap.Jump.performed += OnJumpPerformed;
            _input.CharacterMap.Interact.performed += OnInteractPerformed;

            if (_instance == null)
            {
                _instance = this;
            }
        }

        public void OnEnable()
        {
            _input.CharacterMap.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void OnDisable()
        {
            _input.CharacterMap.Disable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Update()
        {
            if (!_isLocked)
            {
                HandleLookAround();
            }
        }

        private void FixedUpdate()
        {
            if (!_isLocked)
            {
                HandleMove();
            }
        }


        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            if (_isLocked)
            {
                return;
            }

            OnInteractPressed?.Invoke();
        }

        private void OnMovementPerformed(InputAction.CallbackContext callbackContext)
        {
            _movementVector = callbackContext.ReadValue<Vector2>();
        }

        private void OnMovementCancelled(InputAction.CallbackContext callbackContext)
        {
            _movementVector = Vector2.zero;
        }

        private void OnJumpPerformed(InputAction.CallbackContext callbackContext)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(transform.up * _jumpForce);
            }
        }

        private void OnSprintPerformed(InputAction.CallbackContext callbackContext)
        {
            _activeSpeedModifier = _sprintMultiplier;
        }

        private void OnSprintCancelled(InputAction.CallbackContext callbackContext)
        {
            _activeSpeedModifier = 1f;
        }

        private void HandleMove()
        {
            Vector3 move = transform.right * _movementVector.x + transform.forward * _movementVector.y;
            move.y = 0f;

            Vector3 targetPosition = _rigidbody.position + move.normalized * _movementSpeed * _activeSpeedModifier * Time.fixedDeltaTime;

            _rigidbody.MovePosition(targetPosition);
        }


        private void HandleLookAround()
        {
            Vector2 mouseDelta = _input.CharacterMap.Look.ReadValue<Vector2>();

            float mouseX = mouseDelta.x * _mouseSensitivity;
            transform.Rotate(Vector3.up * mouseX);

            float mouseY = mouseDelta.y * _mouseSensitivity;
            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -_upDownRange, _upDownRange);
            _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(transform.position + Vector3.down * 0.5f, 0.25f, _groundLayer);
        }

    }
}