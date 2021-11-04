using UnityEngine;
using UnityEngine.UI;

namespace BallsFall
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public string score
        {
            set { _text.text = value.ToString(); }
            get { return score; }
        }
    }
}