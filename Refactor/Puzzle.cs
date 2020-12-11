using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Scripts.Refactor {

    public class PuzzleInfoField
    { 

        public string name;

        public static PuzzleInfoField playerMovements {get => new PuzzleInfoField("Player Movements");}
        public static PuzzleInfoField time { get => new PuzzleInfoField("Time"); }
        public static PuzzleInfoField succeded { get => new PuzzleInfoField("Succeded"); }

        public PuzzleInfoField(string name)
        {
            this.name = name;
        }
    }

    [CreateAssetMenu (fileName = "New Puzzle", menuName = "Sokoban/Puzzle", order = 1)]
    public class Puzzle : ScriptableObject 
    {
        [TextArea (6, 100)] 
        public string Text;

        string textTest;

        private void Awake()
        {
            textTest = Text;
        }

        public string GetPuzzleInfoFieldValue(PuzzleInfoField puzzleInfoField)
        {
            int wordIndex = Text.IndexOf(puzzleInfoField.name);
            if(wordIndex != 0)
            {
                string value = "";

                return value;
            }
            return "";
        }

        public void AddOrReplaceInfoField(PuzzleInfoField puzzleInfoField, string value)
        {
            string potentialValue = GetPuzzleInfoFieldValue(puzzleInfoField);

            //Replace
            if (!string.IsNullOrEmpty(potentialValue))
            {

            }
            //Add
            else
            {
                textTest += "\r\n";
                textTest += puzzleInfoField.name + " : " + value;
            }
            Debug.Log(textTest);
        }

        public List<PuzzleTileEncoding> GetEncoding () {
            var tileEncodings = new List<PuzzleTileEncoding> ();

            using (var reader = new StringReader (Text)) 
            {
                string line;
                var lineIndex = 0;

                // Iterate over every line in the multi-line string, calculate the position, and parse the tile type
                while ((line = reader.ReadLine ()) != null) 
                {
                    // Can be converted to linq expression, but is messy.
                    for (var i = 0; i < line.Length; i++) 
                    {
                        var tileEncoding = new PuzzleTileEncoding 
                        {
                            Position = new Vector2Int (i, lineIndex),
                            Type = ParseTileType (line[i])
                        };

                        if (int.TryParse(line[i].ToString(), out int bar)) //if char is int
                            tileEncoding.Id = bar;

                        tileEncodings.Add (tileEncoding);
                    }

                    lineIndex--;
                }
            }

            return tileEncodings;
        }

        private static PuzzleTileType ParseTileType (char character) 
        {
            switch (character) 
            {
                case '#':
                    return PuzzleTileType.Wall | PuzzleTileType.Floor;
                case '@':
                    return PuzzleTileType.Pusher | PuzzleTileType.Floor;
                case '_':
                    return PuzzleTileType.Floor;
                case 'B':
                    return PuzzleTileType.Box | PuzzleTileType.Floor;
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return PuzzleTileType.Goal | PuzzleTileType.Floor;
                default:
                    return PuzzleTileType.None;
            }
        }
    }
}
