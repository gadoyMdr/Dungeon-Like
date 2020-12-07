using Scripts.Refactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleCollection
{
    public string collectionName;
    public Puzzle[] puzzles;
}

public class PuzzleCollectionsManager : MonoBehaviour
{
    public PuzzleCollection[] puzzleCollections;
}
