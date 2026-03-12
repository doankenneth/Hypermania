using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Runners
{
    public class ManualRunner : SingleplayerRunner
    {
        [SerializeField]
        private Key _advanceKey;

        [SerializeField]
        private float _holdS;

        private float _curHoldS;

        public override void Poll(float deltaTime)
        {
            if (!_initialized)
            {
                return;
            }
            _inputBufferP1.Saturate();
            if (Keyboard.current[Key.RightArrow].wasPressedThisFrame)
            {
                GameLoop();
                _inputBufferP1.Clear();
            }
            if (Keyboard.current[Key.RightArrow].isPressed)
            {
                _curHoldS += deltaTime;
                if (_curHoldS >= _holdS)
                {
                    GameLoop();
                    _inputBufferP1.Clear();
                }
            }
            else
            {
                _curHoldS = 0;
            }
        }
    }
}
