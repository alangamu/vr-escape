using System;

namespace VRSki.Scripts.Interfaces
{
    public interface IDigitDisplay
    {
        event Action<int> OnSetDigit;
        event Action OnSelect;
        event Action OnDeselect;
        int X { get; }
        int Y { get; }
        int Digit { get; }
        void SetXY(int x, int y);
        void Select();
        void Deselect();
        void SetDigit(int digit);
    }
}