using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor
{
    /// <summary>
    /// Goal tile, when a box is slid on top of it, it's triggered
    /// </summary>
    public class Goal : BoardElement
    {
        public Action<int> Triggered;

        private bool IsTriggered;
        public bool isTriggered
        {
            get => IsTriggered;
            set
            {
                IsTriggered = value;
                Triggered?.Invoke(id);
            }
        }

        public int id;
    }
}

