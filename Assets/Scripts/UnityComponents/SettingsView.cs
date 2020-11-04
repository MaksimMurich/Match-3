using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents
{
    public class SettingsView : MonoBehaviour
    {
        public event Action<int> ChangeFieldWidth;
        public event Action<int> ChangeFieldHeight;
        public event Action StartGame;

        [SerializeField] private int _minSideSize = 3;
        [SerializeField] private int _maxSideSize = 9;
        [SerializeField] private InputField _fieldWidth = null;
        [SerializeField] private InputField _fieldHeight = null;
        [SerializeField] private Button _startButton = null;

        private void Awake()
        {
            _fieldWidth.onValueChanged.AddListener(ChangeFieldSizeEventHandler);
            _fieldHeight.onValueChanged.AddListener(ChangeFieldSizeEventHandler);
            _startButton.onClick.AddListener(ClickStartEventHandler);
        }

        private void ClickStartEventHandler()
        {
            StartGame?.Invoke();
        }

        private void ChangeFieldSizeEventHandler(string value)
        {
            if (!int.TryParse(_fieldWidth.text, out int fieldWidth) || !int.TryParse(_fieldHeight.text, out int fieldHeight))
            {
                return;
            }

            fieldWidth = Mathf.Max(fieldWidth, _minSideSize);
            fieldWidth = Mathf.Min(fieldWidth, _maxSideSize);
            fieldHeight = Mathf.Max(fieldHeight, _minSideSize);
            fieldHeight = Mathf.Min(fieldHeight, _maxSideSize);
            
            _fieldHeight.text = fieldHeight.ToString();
            _fieldWidth.text = fieldWidth.ToString();

            ChangeFieldHeight?.Invoke(fieldHeight);
            ChangeFieldWidth?.Invoke(fieldWidth);
        }
    }
}
