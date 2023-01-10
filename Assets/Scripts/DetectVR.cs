using UnityEngine;
using UnityEngine.XR.Management;

namespace VRSki.Scripts
{
    public class DetectVR : MonoBehaviour
    {
        [SerializeField]
        private bool _startInVR = true;
        [SerializeField]
        private GameObject _xrOrigin;
        [SerializeField]
        private GameObject _desktopCharacter;

        private void Start()
        {
            if (_startInVR)
            {
                var xrSettings = XRGeneralSettings.Instance;
                if (xrSettings == null)
                {
                    print("XRGeneralSettings is null");
                    return;
                }

                var xrManager = xrSettings.Manager;
                if (xrManager == null)
                {
                    print("XRManagerSettings is null");
                    return;
                }

                var xrLoader = xrManager.activeLoader;
                if (xrLoader == null)
                {
                    print("XRLoader is null");
                    _xrOrigin.SetActive(false);
                    _desktopCharacter.SetActive(true);
                    return;
                }

                print("XRLoader is not null");
                _xrOrigin.SetActive(true);
                _desktopCharacter.SetActive(false);
            }
            else
            {
                _xrOrigin.SetActive(false);
                _desktopCharacter.SetActive(true);
            }

        }
    }
}