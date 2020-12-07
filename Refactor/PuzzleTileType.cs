using System;

namespace Scripts.Refactor {
    // Flags allow for multiple tile types to be represented in a single variable
    [Flags]
    public enum PuzzleTileType {
        None   = 0,
        Floor  = 1,
        Wall   = 2,
        Pusher = 4,
        Box    = 8,
        Goal   = 16
    }
}