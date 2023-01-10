using System.Collections.Generic;
using UnityEngine;
using VRSki.Scripts.Interfaces;
using VRSki.Scripts.ScriptableObjects.Events;
using VRSki.Scripts.ScriptableObjects.Sets;
using VRSki.Scripts.ScriptableObjects.Variables;

namespace VRSki.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _winNumber;
        [SerializeField]
        private IntVariable _winNumberLength;
        [SerializeField]
        private IntVariable _activeNumberIndex;
        [SerializeField]
        private IntVariable _activeNumberRowIndex;
        [SerializeField]
        private IntVariable _activeNumberDigit;
        [SerializeField]
        private DigitsDisplaySet _digitsDisplaySet;
        [SerializeField]
        private BoolVariable _canEvaluateVariable;
        [SerializeField]
        private GameEvent _numberCheckEvent;
        [SerializeField]
        private GameEvent _playerWinEvent;

        private List<int> _selectedNumbers;

        private void OnEnable()
        {
            _activeNumberIndex.OnValueChanged += ActiveNumberIndexOnValueChanged;
            _activeNumberRowIndex.OnValueChanged += ActiveNumberRowIndexOnValueChanged;
            _activeNumberDigit.OnValueChanged += ActiveNumberDigitOnValueChanged;
            _canEvaluateVariable.OnValueChanged += CanEvaluateVariableOnValueChanged;
        }

        private void OnDisable()
        {
            _activeNumberIndex.OnValueChanged -= ActiveNumberIndexOnValueChanged;
            _activeNumberRowIndex.OnValueChanged -= ActiveNumberRowIndexOnValueChanged;
            _activeNumberDigit.OnValueChanged -= ActiveNumberDigitOnValueChanged;
            _canEvaluateVariable.OnValueChanged -= CanEvaluateVariableOnValueChanged;
        }

        private void NumberCheckEventOnRaise()
        {
            //TODO: change to another class like WinChecker
            _numberCheckEvent.OnRaise -= NumberCheckEventOnRaise;
            bool playerWin = EvaluateWin();
            if (playerWin)
            {
                _playerWinEvent.Raise();
                return;
            }
            _canEvaluateVariable.SetValue(false);
            _activeNumberRowIndex.SetValue(_activeNumberRowIndex.Value + 1);
        }

        private bool EvaluateWin()
        {
            List<IDigitDisplay> digitDisplays = _digitsDisplaySet.Items.FindAll(x => x.Y == _activeNumberRowIndex.Value);
            string winNumberString = _winNumber.Value.ToString();

            foreach (var item in digitDisplays)
            {
                if (!item.Digit.ToString().Equals(winNumberString[item.X]))
                {
                    return false;
                }
            }
            return true;
        }

        private void CanEvaluateVariableOnValueChanged(bool canEvaluate)
        {
            if (canEvaluate)
            {
                _numberCheckEvent.OnRaise += NumberCheckEventOnRaise;
            }

        }

        private void ActiveNumberDigitOnValueChanged(int newDigit)
        {
            bool canSetDigit = CanSetDigit(newDigit);
            if (canSetDigit)
            {
                _selectedNumbers.Add(newDigit);
                IDigitDisplay digitDisplay = _digitsDisplaySet.GetDigitDisplay(_activeNumberIndex.Value, _activeNumberRowIndex.Value);
            
                if (digitDisplay != null)
                {
                    digitDisplay.SetDigit(newDigit);
                }

                if (_activeNumberIndex.Value == _winNumberLength.Value - 1)
                {
                    _canEvaluateVariable.SetValue(true);
                    return;
                }

                _activeNumberIndex.SetValue(_activeNumberIndex.Value + 1);
            }
        }

        private void ActiveNumberRowIndexOnValueChanged(int newRowIndex)
        {
            _activeNumberIndex.SetValue(0);
            _canEvaluateVariable.SetValue(false);
            _selectedNumbers.Clear();
        }

        private void ActiveNumberIndexOnValueChanged(int newIndex)
        {
            IDigitDisplay digitDisplay = _digitsDisplaySet.GetDigitDisplay(_activeNumberIndex.Value, _activeNumberRowIndex.Value);

            if (digitDisplay != null)
            {
                _digitsDisplaySet.Select(digitDisplay);
            }
        }

        private void Start()
        {
            _selectedNumbers = new List<int>();
            _activeNumberRowIndex.SetValue(0);
        }

        private bool CanSetDigit(int digit)
        {
            return !_selectedNumbers.Contains(digit);
        }
    }
}