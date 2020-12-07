using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCollectionButton : MonoBehaviour
{
    public PuzzleCollection puzzleCollection;
    public int id;
    public void LoadCollectionPuzzles()
    {
        GameObject.FindObjectOfType<LevelsUI>().LoadCollectionPuzzles(puzzleCollection);
    }
}
