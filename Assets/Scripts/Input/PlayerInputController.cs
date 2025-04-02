using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private PlayerInput _input;
        public Vector2 MousePosition { get; private set; }

        public event Action<Vector2> ClickedOnScreen;
        public event Action PressCanceled;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.ClickedOnScreen.performed += OnScreenClicked;
            _input.Player.ClickedOnScreen.canceled += OnPressCanceled;
            _input.Player.Dragged.performed += OnDragged;
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.ClickedOnScreen.performed -= OnScreenClicked;
            _input.Player.ClickedOnScreen.canceled -= OnPressCanceled;
            _input.Player.Dragged.performed -= OnDragged;
        }

        private void OnScreenClicked(InputAction.CallbackContext context)
        {
            ClickedOnScreen?.Invoke(MousePosition);
        }

        private void OnPressCanceled(InputAction.CallbackContext context)
        {
            PressCanceled?.Invoke();
        }

        private void OnDragged(InputAction.CallbackContext context)
        {
            MousePosition = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }
    }
}
