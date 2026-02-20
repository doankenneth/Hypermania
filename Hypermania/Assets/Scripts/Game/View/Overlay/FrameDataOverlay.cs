using Design.Animation;
using Design.Configs;
using Game.Sim;
using UnityEngine;

namespace Game.View.Overlay
{
    public class FrameDataOverlay : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tableObject;

        [SerializeField]
        private GameObject _curFrameBar;

        [SerializeField]
        private GameObject _cellPrefab;

        [SerializeField]
        private int _numColumns;

        private float _cellWidth;
        private float _cellHeight;
        private FrameDataCell[,] _cells;

        public void Awake()
        {
            int numRows = 3;
            RectTransform rect = _tableObject.GetComponent<RectTransform>();
            _cellWidth = rect.rect.width / _numColumns;
            _cellHeight = rect.rect.height / numRows;
            _cells = new FrameDataCell[numRows, _numColumns];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < _numColumns; j++)
                {
                    GameObject cell = Instantiate(_cellPrefab, _tableObject.transform, false);

                    RectTransform cellRect = cell.GetComponent<RectTransform>();

                    cellRect.anchorMin = new Vector2(0f, 1f);
                    cellRect.anchorMax = new Vector2(0f, 1f);
                    cellRect.pivot = new Vector2(0f, 1f);

                    cellRect.sizeDelta = new Vector2(_cellWidth, _cellHeight);

                    float x = j * _cellWidth;
                    float y = -i * _cellHeight;
                    cellRect.anchoredPosition = new Vector2(x, y);

                    _cells[i, j] = cell.GetComponent<FrameDataCell>();
                    _cells[i, j].SetType(FrameType.Neutral);
                }
            }
        }

        public void AddFrameData(in GameState state, CharacterConfig[] characterConfigs, AudioConfig audioConfig)
        {
            int baseIdx = state.Frame.No % _numColumns;
            for (int i = 0; i < 2; i++)
            {
                _cells[i, baseIdx].SetType(state.Frame, state.Fighters[i], characterConfigs[i]);
            }

            _curFrameBar.GetComponent<RectTransform>().anchoredPosition = new Vector2((baseIdx + 1) * _cellWidth, 0f);

            if (audioConfig.BeatWithinWindow(state.Frame, AudioConfig.BeatSubdivision.QuarterNote, 0))
            {
                _cells[2, baseIdx].SetType(FrameType.Hitstun);
            }
            else
            {
                _cells[2, baseIdx].SetType(FrameType.Neutral);
            }
        }
    }
}
