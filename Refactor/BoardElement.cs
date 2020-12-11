using System.Linq;
using UnityEngine;

namespace Scripts.Refactor {
    [RequireComponent (typeof (SpriteRenderer))]
    public class BoardElement : MonoBehaviour {
        
        [SerializeField]
        protected TilesTheme[] tilesThemeArray;

        
        public BoardEmplacement parentBoardEmplacement;

        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetParameters(Theme theme, BoardEmplacement boardEmplacement)
        {
            SetVisual(theme);
            parentBoardEmplacement = boardEmplacement;
        }

        public virtual void SetVisual(Theme theme)
        {
            TilesTheme tilesTheme = tilesThemeArray.FirstOrDefault(x => x.theme.Equals(theme));

            if (tilesTheme != null)
            {
                spriteRenderer.sprite = tilesTheme.variants[Random.Range(0, tilesTheme.variants.Length)];
            }
            else
            {
                TilesTheme normalTileThem = tilesThemeArray.FirstOrDefault(x => x.theme.Equals(Theme.Normal));
                if (normalTileThem != null)
                    spriteRenderer.sprite = normalTileThem.variants[Random.Range(0, normalTileThem.variants.Length)];
            }
        }
    }
}
