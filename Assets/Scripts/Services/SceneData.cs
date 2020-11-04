using Match3.Assets.Scripts.UnityComponents.UI;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private SettingsView _settingsView = null;
    [SerializeField] private NavigationView _navigationView = null;

    public Camera Camera => _camera;
    public SettingsView SettingsView => _settingsView;
    public NavigationView NavigationView => _navigationView;
}
