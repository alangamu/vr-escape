using System;
using UnityEngine;
using VRSki.Scripts.Interfaces;
using VRSki.Scripts.ScriptableObjects.Variables;

namespace VRSki.Scripts
{

    public class NumberItemController : MonoBehaviour, IDigitDisplay
    {
        public event Action OnSelect;
        public event Action OnDeselect;
        public event Action<int> OnSetDigit;

        public int X => _x;
        public int Y => _y;

        public int Digit => _digit;

        [SerializeField]
        private IntVariable _winNumberLength;

        private int _x;
        private int _y;
        private int _digit;

        public void SetXY(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Select()
        {
            OnSelect?.Invoke();
        }

        public void Deselect()
        {
            OnDeselect?.Invoke();
        }

        public void SetDigit(int digit)
        {
            _digit = digit;
            OnSetDigit?.Invoke(digit);
        }
    }
}