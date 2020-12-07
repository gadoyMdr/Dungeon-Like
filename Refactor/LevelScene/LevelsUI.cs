using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Refactor;
using System.Linq;
public class LevelsUI : MonoBehaviour
{
    [SerializeField]
    private PuzzleCollectionButton collectionButtonPrefab;
    [SerializeField]
    private Transform collectionButtonContent;

    [SerializeField]
    private PuzzleButton puzzleButtonPrefab;
    [SerializeField]
    private Transform puzzleButtonsContent;

    [SerializeField]
    private TextMeshProUGUI collectionType;

    [SerializeField]
    private Transform levelsContent;

    private PuzzleCollectionsManager puzzleCollectionsManager;

    private void Awake()
    {
        puzzleCollectionsManager = GetComponent<PuzzleCollectionsManager>();
    }

    private void Start()
    {
        CreateCollectionButtons();
        DisplayDefaultCollection();
    }

    void DisplayDefaultCollection()
    {
        GameObject.FindObjectsOfType<PuzzleCollectionButton>().Where(x => x.id == 0).ToArray()[0].LoadCollectionPuzzles();
    }

    void CreateCollectionButtons()
    {
        int counter = 0;
        foreach(PuzzleCollection collection in puzzleCollectionsManager.puzzleCollections)
        {
            PuzzleCollectionButton current = Instantiate(collectionButtonPrefab, collectionButtonContent);
            current.id = counter;
            current.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = collection.collectionName;
            current.puzzleCollection = collection;
            counter++;
        }
    }

    public void LoadCollectionPuzzles(PuzzleCollection puzzleCollection)
    {
        int counter = 0;
        ClearPuzzleContentChildren();
        collectionType.text = $"Collection : {puzzleCollection.collectionName}";
        foreach (Puzzle puzzle in puzzleCollection.puzzles)
        {
            counter++;
            PuzzleButton current = Instantiate(puzzleButtonPrefab, puzzleButtonsContent);
            current.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = counter.ToString();
            current.puzzle = puzzle;
        }
    }

    void ClearPuzzleContentChildren()
    {
        foreach (Transform child in puzzleButtonsContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ChangeCategory(string collection)
    {
        collectionType.text = $"Collection : {collection}";
        LoadLevelFromWhatever();
    }

    void LoadLevelFromWhatever()
    {
        foreach (Transform t in levelsContent.transform) Destroy(t.gameObject);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
