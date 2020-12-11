using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{
    /// <summary>
    /// This class will be used to check if certain goals (triggers) on the map have a box on them, if yes, the action will be executed
    /// </summary>
    [Serializable]
    public class GoalsActionSet
    {
        List<Goal> goals = new List<Goal>();
        IActionable action;
    }

    public class Board : MonoBehaviour
    {
        [SerializeField]
        GenerateAnything generateEnemies;

        [SerializeField]
        GenerateAnything generateItems;

        [SerializeField]
        private Theme theme;

        [SerializeField]
        private BoardElement _floorPrefab, _wallPrefab, _goalPrefab;

        [SerializeField]
        private BoardEmplacement _boardEmplacementPrefab;

        [SerializeField]
        private Pusher _pusherPrefab;

        [SerializeField]
        List<GoalsActionSet> goalsActionSets = new List<GoalsActionSet>();  

        public bool succeded = false;
        public Box _boxPrefab;

        private Pusher _pusher;
        private List<Goal> _goals = new List<Goal>();
        private List<Box> _boxes = new List<Box>();
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
            _unmovableElements.Clear(); //Reset walls ect...
            _movableElements.Clear();   //Reset boxes ect...

            foreach (Transform child in transform)
                Destroy(child.gameObject);
            

            List<PuzzleTileEncoding> encoding = new List<PuzzleTileEncoding>(puzzle.GetEncoding());

            encoding.ForEach(x => TrySpawnTile(x));

            generateEnemies.Generate();   //Spawn all little enemies everywhere
            generateItems.Generate();   //Spawn all little items everywhere
            FindObjectOfType<CinemachineVirtualCamera>().Follow = _pusher.transform;
        }

        
        //Huge mess, only because i used flags
        private void TrySpawnTile(PuzzleTileEncoding tileEncoding)
        {
            Vector3 pos = new Vector3(tileEncoding.Position.x, tileEncoding.Position.y);
            Quaternion rot = Quaternion.identity;

            if (tileEncoding.Type.HasFlag(PuzzleTileType.Floor)
                || tileEncoding.Type.HasFlag(PuzzleTileType.Wall)
                || tileEncoding.Type.HasFlag(PuzzleTileType.Pusher)
                || tileEncoding.Type.HasFlag(PuzzleTileType.Goal))
            {
                BoardEmplacement boardEmplacementInstance = Instantiate(_boardEmplacementPrefab, pos, rot, gameObject.transform);
                boardEmplacementInstance.Position = tileEncoding.Position;


                BoardElement tile;

                if (tileEncoding.Type.HasFlag(PuzzleTileType.Floor))
                {
                    tile = Instantiate(_floorPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<BoardElement>();
                    boardEmplacementInstance.boardElements.Add(tile);
                    tile.SetParameters(theme, boardEmplacementInstance);
                }
                if (tileEncoding.Type.HasFlag(PuzzleTileType.Wall))
                {
                    tile = Instantiate(_wallPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<BoardElement>();
                    boardEmplacementInstance.boardElements.Add(tile);
                    tile.SetParameters(theme, boardEmplacementInstance);
                }
                if (tileEncoding.Type.HasFlag(PuzzleTileType.Pusher))
                {
                    tile = Instantiate(_pusherPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<BoardElement>();
                    boardEmplacementInstance.boardElements.Add(tile);
                    tile.SetParameters(theme, boardEmplacementInstance);
                    _pusher = tile.GetComponent<Pusher>();
                }
                if (tileEncoding.Type.HasFlag(PuzzleTileType.Box))
                {
                    tile = Instantiate(_boxPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<BoardElement>();
                    boardEmplacementInstance.boardElements.Add(tile);
                    tile.SetParameters(theme, boardEmplacementInstance);
                    _boxes.Add(tile.GetComponent<Box>());
                }
                if (tileEncoding.Type.HasFlag(PuzzleTileType.Goal))
                {
                    Goal goal = Instantiate(_goalPrefab, pos, rot, boardEmplacementInstance.transform).GetComponent<Goal>();
                    goal.id = tileEncoding.Id;
                    boardEmplacementInstance.boardElements.Add(goal);
                    goal.SetParameters(theme, boardEmplacementInstance);
                    _goals.Add(goal);
                }
            }
        }

        
        public void TryMovePusher(Vector2Int direction)
        {
            _movableElements = Utils.GetBoardElementType<MovableBoardElement>();    //update the list

            _unmovableElements = Utils.GetBoardElementType<UnMovableBoardElement>();    //update the list


            _pusher.Face(direction);

            Vector2Int pusherTarget = _pusher.parentBoardEmplacement.Position + direction;  //target position
            bool wallIsInTheWay = _unmovableElements.Any(w => w.parentBoardEmplacement.Position == pusherTarget);

            if (wallIsInTheWay) return; //if there's something we cant budge, we don't 

            MovableBoardElement box = _movableElements.SingleOrDefault(b => b.parentBoardEmplacement.Position == pusherTarget);
            bool boxIsInTheWay = box != null;

            if (boxIsInTheWay)  //si il y a une box
            {
                Vector2Int boxTarget = box.parentBoardEmplacement.Position + direction;
                var canMoveBox = _unmovableElements.All(w => w.parentBoardEmplacement.Position != boxTarget) && _movableElements.All(b => b.parentBoardEmplacement.Position != boxTarget);
                if (!canMoveBox) return;

                box.Move(direction);    //we move the box

                CheckIfAllGoalsTriggered();

            }
            _pusher.Move(direction);
            playerMovementsSaver.SaveMovement(direction);   //Save system not done
            
        }


        private void CheckIfAllGoalsTriggered()
        {
            bool allGoalsTrigger = true;
            foreach (Goal goal in _goals)
            {
                if(!_boxes.Any(x => x.parentBoardEmplacement.Position.Equals(goal.parentBoardEmplacement.Position)))
                    allGoalsTrigger = false;
            }
            if (allGoalsTrigger)
                FindObjectsOfType<MonoBehaviour>().OfType<IActionable>().FirstOrDefault().Action();

        }

    }
}
