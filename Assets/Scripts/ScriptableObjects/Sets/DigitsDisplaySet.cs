using UnityEngine;
using VRSki.Scripts.Interfaces;

namespace VRSki.Scripts.ScriptableObjects.Sets
{
    [CreateAssetMenu(menuName = "Sets/DigitDisplay Set")]
    public class DigitsDisplaySet : RuntimeSet<IDigitDisplay>
    {
        public void Select(IDigitDisplay digitDisplay)
        {
            foreach (IDigitDisplay item in Items)
            {
                if (item == digitDisplay)
                {
                    item.Select();
                    return;
                    //continue;
                }

                //item.Deselect();
            }
        }

        public IDigitDisplay GetDigitDisplay(int x, int y)
        {
            return Items.Find(z => z.X == x && z.Y == y);
        }

        public void DeselectAll()
        {
            foreach (var item in Items)
            {
                item.Deselect();
            }
        }
    }
}