using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{
    public class BoardEmplacement : MonoBehaviour
    {
        public Vector2Int Position;

        public List<BoardElement> boardElements = new List<BoardElement>();
    }
}