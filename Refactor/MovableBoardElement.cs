using System.Collections;
using UnityEngine;

namespace Scripts.Refactor {
    public class MovableBoardElement : BoardElement
    {
        public static int randomTheme = 1000;
        [SerializeField]
        protected float MoveSpeed;

        public void Move (Vector2Int direction) {
            StopAllCoroutines ();
            StartCoroutine (MoveToGridPosition (direction));
        }

        private IEnumerator MoveToGridPosition (Vector2Int direction) 
        {
            Vector2Int test = direction + new Vector2Int((int)parentBoardEmplacement.transform.position.x, (int)parentBoardEmplacement.transform.position.y);
            
            BoardEmplacement targetEmplacement = Utils.FindBoardEmplacement(test);
            parentBoardEmplacement = targetEmplacement;
            transform.SetParent(parentBoardEmplacement.transform);
            
            
            Vector3 targetPosition = Vector3.zero;
            while (transform.localPosition != targetPosition) {
                transform.localPosition = Vector3.MoveTowards (transform.localPosition, targetPosition, MoveSpeed * Time.deltaTime);
                yield return null;
            }

            
        }
    }
}
