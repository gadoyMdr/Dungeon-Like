using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{

    public class Board : MonoBehaviour
    {
        [SerializeField]
        private Theme theme;

        [SerializeField]
        private BoardElement _floorPrefab, _wallPrefab;

        [SerializeField]
        private BoardEmplacement _boardEmplacementPrefab;

        [SerializeField]
        private Pusher _pusherPrefab;

        public bool succeded = false;
        public Box _boxPrefab;

        private Pusher _pusher;
        private List<BoardEmplacement> _emplacements = new List<BoardEmplacement>();

        private List<UnMovableBoardElement> _unmovableElements = new List<UnMovableBoardElement>();
        private List<MovableBoardElement> _movableElements = new List<MovableBoardElement>();

        private PlayerMovementsSaver playerMovementsSaver;


        private void Awake()
        {
            playerMovementsSaver = GameObject.FindObjectOfType<PlayerMovementsSaver>();
        }


        public void Build(Puzzle puzzle)
        {
            _pusher = null;
            _unmovableElements.Clear();
            _movableElements.Clear();


            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            var encoding = puzzle.GetEncoding();

            encoding.ForEach(x => TrySpawnTile(x));

            _movableElements = Utils.GetBoardElementType<MovableBoardElement>();
            
            _unmovableElements = Utils.GetBoardElementType<UnMovableBoardElement>();

            CropCamera();
        }

        private void TrySpawnTile(PuzzleTileEncoding tileEncoding)
        {
            Vector3 pos = new Vector3(tileEncoding.Position.x, tileEncoding.Position.y);
            Quaternion rot = Quaternion.identity;

            if (tileEncoding.Type.HasFlag(PuzzleTileType.Floor)
                || tileEncoding.Type.HasFlag(PuzzleTileType.Wall)
                || tileEncoding.Type.HasFlag(PuzzleTileType.Pusher))
            {
                BoardEmplacement boardEmplacementInstance = Instantiate(_boardEmplacementPrefab, pos, rot, gameObject.transform);
                boardEmplacementInstance.Position = tileEncoding.Position;


                BoardElement tile;

                if (tileEncoding.Type.HasFlag(PuzzleTileType.Floor))
                {
                    tile = Instantiate(_floorPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<BoardElement>();
                    boardEmplacementInstance.boardElements.Add(tile);
                    tile.GetComponent<BoardElement>().SetParameters(theme, boardEmplacementInstance);
                }
                if (tileEncoding.Type.HasFlag(PuzzleTileType.Wall))
                {
                    tile = Instantiate(_wallPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<BoardElement>();
                    boardEmplacementInstance.boardElements.Add(tile);
                    tile.GetComponent<BoardElement>().SetParameters(theme, boardEmplacementInstance);
                }
                if (tileEncoding.Type.HasFlag(PuzzleTileType.Pusher))
                {
                    tile = Instantiate(_pusherPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<BoardElement>();
                    boardEmplacementInstance.boardElements.Add(tile);
                    tile.GetComponent<BoardElement>().SetParameters(theme, boardEmplacementInstance);
                    _pusher = tile.GetComponent<Pusher>();
                }
            }
        }

        
        public void TryMovePusher(Vector2Int direction)
        {
            _pusher.Face(direction);

            var pusherTarget = _pusher.parentBoardEmplacement.Position + direction;
            var wallIsInTheWay = _unmovableElements.Any(w => w.parentBoardEmplacement.Position == pusherTarget);

            if (wallIsInTheWay) return;

            var box = _movableElements.SingleOrDefault(b => b.parentBoardEmplacement.Position == pusherTarget);
            var boxIsInTheWay = box != null;

            if (boxIsInTheWay)
            {
                var boxTarget = box.parentBoardEmplacement.Position + direction;
                var canMoveBox = _unmovableElements.All(w => w.parentBoardEmplacement.Position != boxTarget) && _movableElements.All(b => b.parentBoardEmplacement.Position != boxTarget);
                if (!canMoveBox) return;

                box.Move(direction);
                
            }
            _pusher.Move(direction);
            playerMovementsSaver.SaveMovement(direction);
        }

        void CropCamera()
        {
            float maxX = _unmovableElements.Max(w => w.parentBoardEmplacement.Position.x);
            float minX = _unmovableElements.Min(w => w.parentBoardEmplacement.Position.x);
            float maxY = _unmovableElements.Max(w => w.parentBoardEmplacement.Position.y);
            float minY = _unmovableElements.Min(w => w.parentBoardEmplacement.Position.y);
            float midX = minX + (maxX - minX) / 2f;
            float midY = minY + (maxY - minY) / 2f;

            Camera.main.transform.position = new Vector3(midX, midY, -1f);
        }

    }
}
