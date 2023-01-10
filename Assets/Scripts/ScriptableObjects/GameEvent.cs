﻿using System;
using UnityEngine;

namespace VRSki.Scripts.ScriptableObjects.Events
{
    [CreateAssetMenu(menuName = "Game Events/Void Event")]
    public class GameEvent : ScriptableObject
    {
        public event Action OnRaise;

        public virtual void Raise()
        {
            OnRaise?.Invoke();
        }
    }
}