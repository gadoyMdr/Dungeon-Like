using Scripts.Refactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleButton : MonoBehaviour
{
    public Puzzle puzzle;

    public void LoadLevel()
    {
        PuzzleSceneInfo.puzzleToLoad = puzzle;
        SceneManager.LoadScene("Puzzle");
    }
}
