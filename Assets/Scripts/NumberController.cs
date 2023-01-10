using UnityEngine;
using VRSki.Scripts.ScriptableObjects.Variables;

namespace VRSki.Scripts
{
    public class NumberController : MonoBehaviour
    {
        [SerializeField]
        private int _digit;
        [SerializeField]
        private IntVariable _activeNumberIndex;
        [SerializeField]
        private IntVariable _activeNumberDigit;
        [SerializeField]
        private MeshArrayVariable _numbersMeshArray;
        [SerializeField]
        private PhysicsButton _internalButton;

        private void OnEnable()
        {
            _internalButton.OnPressed += InternalButtonOnPressed;
        }

        private void OnDisable()
        {
            _internalButton.OnPressed -= InternalButtonOnPressed;
        }

        private void InternalButtonOnPressed()
        {
            _activeNumberDigit.SetValue(_digit);
        }
    }
}