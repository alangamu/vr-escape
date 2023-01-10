using System;
using UnityEngine;

namespace VRSki.Scripts
{
    public class PhysicsButton : MonoBehaviour
    {
        public event Action OnPressed;

        [SerializeField]
        private float _threshold = 0.1f;
        [SerializeField]
        private float _deadZone = 0.025f;

        private bool _isPressed;
        private Vector3 _startPosition;
        private ConfigurableJoint _joint;

        private void Start()
        {
            _startPosition = transform.localPosition;
            TryGetComponent(out _joint);
            _isPressed = false;
        }

        private void Update()
        {
            if (!_isPressed && GetValue() + _threshold >= 1)
            {
                Pressed();
            }
            if (_isPressed && GetValue() - _threshold <= 0)
            {
                Released();
            }
        }

        private float GetValue()
        {
            var value = Vector3.Distance(_startPosition, transform.localPosition) / _joint.linearLimit.limit;
            if (Math.Abs(value) < _deadZone)
            {
                value = 0f;
            }

            return Mathf.Clamp(value, -1f, 1f);
        }

        private void Pressed()
        {
            _isPressed = true;
            OnPressed?.Invoke();
        }

        private void Released()
        {
            _isPressed = false;
        }
    }
}
