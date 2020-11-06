using UnityEngine;
using UnityEngine.UI;

namespace Match3.Assets.Scripts.UnityComponents.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _score = null;

        public void SetScore(int value)
        {
            _score.text ="счет: " + value.ToString();
        }
    }
}
