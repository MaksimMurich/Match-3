using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI
{
    public class NavigationView : MonoBehaviour
    {
        public event Action OpenSettingsClickEvent;

        [SerializeField] Button _openSettingsButton = null;

        private void Awake()
        {
            _openSettingsButton.onClick.AddListener(OpenSettingsClickEventHandler);
        }

        private void OpenSettingsClickEventHandler()
        {
            OpenSettingsClickEvent?.Invoke();
        }
    }
}
