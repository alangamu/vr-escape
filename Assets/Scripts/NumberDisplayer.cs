using UnityEngine;
using VRSki.Scripts.Interfaces;
using VRSki.Scripts.ScriptableObjects.Variables;

namespace VRSki.Scripts
{
    public class NumberDisplayer : MonoBehaviour
    {
        [SerializeField]
        private MeshArrayVariable _numbersMeshArray;
        [SerializeField]
        private MeshFilter _numberMeshFilter;

        private IDigitDisplay _digitDisplay;

        private void Start()
        {
            _numberMeshFilter.gameObject.SetActive(false);
        }

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

        private void DigitDisplayOnSetDigit(int value)
        {
            _numberMeshFilter.gameObject.SetActive(true);
            _numberMeshFilter.mesh = _numbersMeshArray.Value[value];
        }
    }
}