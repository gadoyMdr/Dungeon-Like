using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{
    public class GenerateAnything : MonoBehaviour
    {
        [SerializeField]
        private DropItem dropItem;

        [SerializeField]
        private bool spawnEnemies;

        [SerializeField]
        private float oddsPerTile = 10f;

        [SerializeField]
        private RandomGOInSet randomObjects;

        readonly float hundredandone = 101f;

        public void Generate()
        {
            foreach(BoardElement be in Utils.GetBoardElementType<BoardElement>(false)   //Find all emplacement with only 1 emplacement cause it means it's only ground (floor)
                .Where(x => x.parentBoardEmplacement.boardElements.Count == MagicNumbers.one))
            {
                if (Random.Range(MagicNumbers.zero, hundredandone) <= oddsPerTile)
                {
                    if (spawnEnemies) Instantiate(randomObjects.GetRandomObject(), be.transform.position, be.transform.rotation);
                    else dropItem.Drop(be.transform, MagicNumbers.one, randomObjects.GetRandomObject().GetComponent<Item>().itemData);
                }
            }
        }
    }
}

