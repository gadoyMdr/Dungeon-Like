using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Refactor
{
    public class DestroyWall : MonoBehaviour, IActionable
    {
        [SerializeField]
        private int actionId = 1;   //Not used. Suposed to be the id to which this IActionable is attached to


        void OnEnable()
        {
            FindObjectsOfType<Goal>().ToList().ForEach(x => x.Triggered += CheckIfCanTrigger);
        }

        void OnDisable()
        {
            FindObjectsOfType<Goal>().ToList().ForEach(x => x.Triggered -= CheckIfCanTrigger);
        }

        void CheckIfCanTrigger(int id)
        {
            if (!FindObjectsOfType<Goal>().Any(x => !x.isTriggered))
                Action();
        }

        private void Update()
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
                Action();
        }

        public void Action()
        {
            //Okay this is EXTREMELY dirty but clearly don't have the time to do something cleaner
            Destroy(Utils.FindBoardEmplacement(new Vector2Int(15, -5)).transform.Find("Wall(Clone)").GetComponent<BoardElement>().gameObject);
        }
    }
}

