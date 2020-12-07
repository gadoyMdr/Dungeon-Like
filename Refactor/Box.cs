using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{
    [RequireComponent(typeof (SpriteRenderer))]
    public class Box : MovableBoardElement
    {
        [SerializeField]
        Sprite _offGoal, _onGoal;

        [SerializeField]
        TilesTheme[] tilesThemeArrayToggled;

        private SpriteRenderer _spriteRenderer;

        private bool _isOnGoal;
        public bool IsOnGoal {
            get => _isOnGoal;
            set {
                _isOnGoal = value;
                _spriteRenderer.sprite = _isOnGoal ? _onGoal : _offGoal;
            }
        }

        private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer> ();
        

        public override void SetVisual(Theme theme)
        {

            TilesTheme tilesTheme = tilesThemeArray.FirstOrDefault(x => x.theme.Equals(theme));
            TilesTheme tilesThemeToggled = tilesThemeArrayToggled.FirstOrDefault(x => x.theme.Equals(theme));
            
            if (tilesTheme != null)
            {
                if(randomTheme==1000) randomTheme = Random.Range(0, tilesTheme.variants.Length);
                _offGoal = tilesTheme.variants[randomTheme];
                _onGoal = tilesThemeToggled.variants[randomTheme];
            }
            else
            {
                TilesTheme normalTilesTheme = tilesThemeArray.FirstOrDefault(x => x.theme.Equals(Theme.Normal));
                TilesTheme normalTilesThemeToggled = tilesThemeArrayToggled.FirstOrDefault(x => x.theme.Equals(Theme.Normal));
                if (normalTilesTheme != null)
                {
                    _offGoal = normalTilesTheme.variants[randomTheme];
                    _onGoal = normalTilesThemeToggled.variants[randomTheme];
                }
                    
            }
            _spriteRenderer.sprite = _isOnGoal ? _onGoal : _offGoal;
        }
    }
}