using System.Collections;
using UnityEngine;
using VRSki.Scripts.ScriptableObjects.Events;

namespace VRSki.Scripts
{
    public class PhysicsButtonEventTrigger : MonoBehaviour
    {
        [SerializeField]
        private PhysicsButton _checkButton;
        [SerializeField]
        private GameEvent _event;

        private void OnEnable()
        {
            _checkButton.OnPressed += CheckButtonOnPressed;
        }

        private void OnDisable()
        {
            _checkButton.OnPressed -= CheckButtonOnPressed;
        }

        private void CheckButtonOnPressed()
        {
            _event.Raise();
        }
    }
}