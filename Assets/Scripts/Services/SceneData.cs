using Match3.Assets.Scripts.UnityComponents;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private SettingsView _settingsView = null;

    public Camera Camera => _camera;
    public SettingsView SettingsView => _settingsView;
}
