using UnityEngine;

namespace Scripts.Refactor {
    public static class Vector2IntExtensions
    {
        public static Vector3 ToWorldPosition (this Vector2Int gridPosition) {
            return new Vector3 (gridPosition.x, gridPosition.y * 0.625f);
        }
    }
}
