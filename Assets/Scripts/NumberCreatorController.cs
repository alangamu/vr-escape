using System.Linq;
using UnityEngine;
using VRSki.Scripts.ScriptableObjects.Variables;

namespace VRSki.Scripts
{
    public class NumberCreatorController : MonoBehaviour
    {
        [SerializeField]
        private IntVariable _winNumberLength;

        [SerializeField]
        private IntVariable _winNumber;

        private int[] _numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private void Start()
        {
            int winNumber = GenerateWinNumber();
            _winNumber.SetValue(winNumber);
        }

        private int GenerateWinNumber()
        {
            int winNumber = 0;
            for (int i = 0; i < _winNumberLength.Value; i++)
            {
                winNumber = (winNumber * 10) + GetRandomDigit();
            }
            return winNumber;
        }

        private int GetRandomDigit()
        {
            int randomNumberIndex = Random.Range(0, _numbers.Length);
            int randomDigit = _numbers[randomNumberIndex];
            _numbers = _numbers.Where(n => n != randomDigit).ToArray();
            return randomDigit;
        }
    }
}