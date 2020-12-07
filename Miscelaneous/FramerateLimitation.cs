using UnityEngine;

namespace Scripts {
    public class FramerateLimitation : MonoBehaviour {
        [SerializeField] private int _targetFramerate;

        private void Awake () {
            Application.targetFrameRate = _targetFramerate;
        }
    }
}