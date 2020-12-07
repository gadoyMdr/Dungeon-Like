using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{
    public class Utils
    {
        public static BoardEmplacement FindBoardEmplacement(Vector2Int pos) 
            => GameObject.FindObjectsOfType<BoardEmplacement>().Where(x => x.Position.Equals(pos)).FirstOrDefault();

        public static List<T> GetBoardElementType<T>() =>
            GameObject.FindObjectsOfType<BoardElement>()
                .Where(x => x.GetType().IsSubclassOf(typeof(T))
                || x.GetType() == typeof(T))
            .Cast<T>()
            .ToList();

    }
}

