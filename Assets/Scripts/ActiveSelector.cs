using UnityEngine;
using VRSki.Scripts.Interfaces;

namespace VRSki.Scripts
{
    public class ActiveSelector : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;
        [SerializeField]
        private Material _activeMaterial;
        [SerializeField]
        private Material _normalMaterial;

        private IDigitDisplay _digitDisplay;

        private void OnEnable()
        {
            if (TryGetComponent(out _digitDisplay))
            {
                _digitDisplay.OnSelect += PutActiveMaterial;
                _digitDisplay.OnDeselect += PutNormalMaterial;
                _digitDisplay.OnSetDigit += DigitDisplayOnSetDigit;
            }
        }

        private void OnDisable()
        {
            if (_digitDisplay != null)
            {
                _digitDisplay.OnSelect -= PutActiveMaterial;
                _digitDisplay.OnDeselect -= PutNormalMaterial;
                _digitDisplay.OnSetDigit -= DigitDisplayOnSetDigit;
            }
        }

        private void DigitDisplayOnSetDigit(int digit)
        {
            PutNormalMaterial();
        }

        private void PutNormalMaterial()
        {
            _meshRenderer.material = _normalMaterial;
        }

        private void PutActiveMaterial()
        {
            _meshRenderer.material = _activeMaterial;
        }
    }
}