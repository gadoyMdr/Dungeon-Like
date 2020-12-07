using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Refactor {
    [RequireComponent(typeof (Board))]
    public class BoardBootstrapper : MonoBehaviour {

        [HideInInspector]
        public Puzzle _puzzle;

        [SerializeField]
        private InputAction _resetBoardInputAction;

        private Board _board;
        

        private void Awake () {

            _puzzle = PuzzleSceneInfo.puzzleToLoad;
            _board = GetComponent<Board> ();

            _resetBoardInputAction.performed += _ => _board.Build (_puzzle);

        }

        private void Start()
        {
            _board.Build(_puzzle);
        }

        private void OnEnable () 
        {
            _resetBoardInputAction.Enable ();
        }

        private void OnDisable () 
        {
            _resetBoardInputAction.Disable ();
        }
    }
}
