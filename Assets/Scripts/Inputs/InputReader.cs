using UnityEngine;
using UnityEngine.InputSystem;
using System;
using DataSources;

namespace Inputs
{
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private DataSource<InputReader> _inputReaderDataSource;

        private void OnEnable()
        {
            if (_inputReaderDataSource != null )
                _inputReaderDataSource.Reference = this;
        }

        private void OnDisable()
        {
            if ( _inputReaderDataSource != null && _inputReaderDataSource.Reference == this)
                _inputReaderDataSource.Reference = null;
        }

        public event Action<Vector2> OnMove = delegate { };
        public event Action OnJump = delegate { };
        public event Action OnSprint = delegate { };
        public event Action<bool> OnAim = delegate { };
        public event Action OnShoot = delegate { };
        public event Action OnReaload = delegate { };


        public void OnMoveInput(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAimInput(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAim?.Invoke(true);
            else
                OnAim?.Invoke(false);
        }
    }
}