using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Refactor {
    [RequireComponent (typeof (SpriteRenderer))]
    public class Pusher : MovableBoardElement {
        

        [SerializeField]
        private Sprite _upSprite, _downSprite, _leftSprite, _rightSprite;

        private SpriteRenderer _spriteRenderer;
        private Dictionary<Vector2Int, Sprite> _spriteDirections;

        private void Awake () {
            _spriteRenderer = GetComponent<SpriteRenderer> ();
            _spriteDirections = new Dictionary<Vector2Int, Sprite> () {
                { Vector2Int.up, _upSprite },
                { Vector2Int.down, _downSprite },
                { Vector2Int.left, _leftSprite },
                { Vector2Int.right, _rightSprite }
            };
        }

        public void Face (Vector2Int direction) {
            _spriteRenderer.sprite = _spriteDirections[direction];
        }
    }
}
