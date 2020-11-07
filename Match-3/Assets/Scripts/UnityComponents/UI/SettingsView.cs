using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI
{
    public class SettingsView : MonoBehaviour
    {
        public event Action<int> ChangeFieldWidthEvent;
        public event Action<int> ChangeFieldHeightEvent;
        public event Action StartGameClickEvent;
        public event Action CancelSettingsClickEvent;
        public event Action CloseAppClickEvent;

        [SerializeField] private int _minSideSize = 3;
        [SerializeField] private int _maxSideSize = 9;
        [SerializeField] private InputField _fieldWidth = null;
        [SerializeField] private InputField _fieldHeight = null;
        [SerializeField] private Button _startButton = null;
        [SerializeField] private Button _cancelButton = null;
        [SerializeField] private Button _closeAppButton = null;

        private void Awake()
        {
            _fieldWidth.onValueChanged.AddListener(ChangeFieldSizeEventHandler);
            _fieldHeight.onValueChanged.AddListener(ChangeFieldSizeEventHandler);
            _startButton.onClick.AddListener(ClickStartEventHandler);
            _cancelButton.onClick.AddListener(ClickCancelEventHandler);
            _closeAppButton.onClick.AddListener(ClickCloseAppEventHandler);
        }

        public void SetSettings(int levelWidth, int levelHeight)
        {
            _fieldWidth.text = levelWidth.ToString();
            _fieldHeight.text = levelHeight.ToString();
        }

        private void ClickStartEventHandler()
        {
            StartGameClickEvent?.Invoke();
        }

        private void ClickCancelEventHandler()
        {
            CancelSettingsClickEvent?.Invoke();
        }

        private void ClickCloseAppEventHandler()
        {
            CloseAppClickEvent?.Invoke();
        }

        private void ChangeFieldSizeEventHandler(string value)
        {
            if (int.TryParse(_fieldWidth.text, out int fieldWidth))
            {
                fieldWidth = Mathf.Max(fieldWidth, _minSideSize);
                fieldWidth = Mathf.Min(fieldWidth, _maxSideSize);
                _fieldWidth.text = fieldWidth.ToString();
                ChangeFieldWidthEvent?.Invoke(fieldWidth);
            }

            if (int.TryParse(_fieldHeight.text, out int fieldHeight))
            {
                fieldHeight = Mathf.Max(fieldHeight, _minSideSize);
                fieldHeight = Mathf.Min(fieldHeight, _maxSideSize);
                _fieldHeight.text = fieldHeight.ToString();
                ChangeFieldHeightEvent?.Invoke(fieldHeight);
            }
        }
    }
}
