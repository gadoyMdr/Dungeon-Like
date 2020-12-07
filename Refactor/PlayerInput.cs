using UnityEngine;

namespace Scripts.Refactor {
    [RequireComponent (typeof (Board))]
    public class PlayerInput : MonoBehaviour {
        private Controls _controls;
        private Board _board;
        private void Awake () 
        {
            _board = GetComponent<Board> ();
            _controls = new Controls ();

            _controls.Game.MoveUp.performed += _ => _board.TryMovePusher (Vector2Int.up);
            _controls.Game.MoveDown.performed += _ => _board.TryMovePusher (Vector2Int.down);
            _controls.Game.MoveLeft.performed += _ => _board.TryMovePusher (Vector2Int.left);
            _controls.Game.MoveRight.performed += _ => _board.TryMovePusher (Vector2Int.right);
        }

        private void OnEnable () 
        {
            _controls.Game.Enable ();
        }

        private void OnDisable () 
        {
            _controls.Game.Disable();
        }
    }
}
