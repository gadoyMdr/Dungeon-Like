using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementsSaver : MonoBehaviour
{
    public string playersMovement;

    public void SaveMovement(Vector2Int vector2Int)
    {
        if (vector2Int.Equals(Vector2Int.up))
            playersMovement += "U";
        if (vector2Int.Equals(Vector2Int.down))
            playersMovement += "D";
        if (vector2Int.Equals(Vector2Int.right))
            playersMovement += "R";
        if (vector2Int.Equals(Vector2Int.left))
            playersMovement += "L";
    }
}
