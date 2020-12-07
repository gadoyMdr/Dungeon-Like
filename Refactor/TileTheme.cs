using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Theme
{
    Normal,
    Dirty,
    Industrial,
    ThreeDimensions,
    ThreeDIndustrial
}

[System.Serializable]
public class TilesTheme
{
    public Sprite[] variants;
    public Theme theme;
}
