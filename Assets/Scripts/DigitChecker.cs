using UnityEngine;
using VRSki.Scripts.Interfaces;
using VRSki.Scripts.ScriptableObjects.Events;
using VRSki.Scripts.ScriptableObjects.Variables;

namespace VRSki.Scripts
{
    public class DigitChecker : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _numberCheckEvent;
        [SerializeField]
        private IntVariable _activeNumberRowIndex;
        [SerializeField]
        private Material _correctMaterial;
        [SerializeField]
        private Material _nonExistentMaterial;
        [SerializeField]
        private Material _misplacedMaterial;
        [SerializeField]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        private IntVariable _winNumber;

        private IDigitDisplay _digitDisplay;
        private Material _revealedMaterial;

        private void OnEnable()
        {
            if (TryGetComponent(out _digitDisplay))
            {
                _digitDisplay.OnSetDigit += DigitDisplayOnSetDigit;
            }
        }

        private void OnDisable()
        {
            if (_digitDisplay != null)
            {
                _digitDisplay.OnSetDigit -= DigitDisplayOnSetDigit;
            }
        }

        private void DigitDisplayOnSetDigit(int digit)
        {
            _numberCheckEvent.OnRaise += NumberCheckEventOnRaise;
            CheckDigit();
        }

        private void NumberCheckEventOnRaise()
        {
            _numberCheckEvent.OnRaise -= NumberCheckEventOnRaise;
            Reveal();
        }

        private void Reveal()
        {
            print($"reveal {_digitDisplay.Digit}");
            _meshRenderer.material = _revealedMaterial;
        }

        private void CheckDigit()
        {
            string winNumber = _winNumber.Value.ToString();
            string digit = _digitDisplay.Digit.ToString();
            if (winNumber.Contains(digit))
            {
                if (winNumber[_digitDisplay.X].ToString().Equals(digit))
                {
                    print($"es {digit}");
                    _revealedMaterial = _correctMaterial;
                    return;
                }
                print($"contiene {digit}");
                _revealedMaterial = _misplacedMaterial;
                return;
            }
            print($"no esta {digit}");
            _revealedMaterial = _nonExistentMaterial;
        }
    }
}