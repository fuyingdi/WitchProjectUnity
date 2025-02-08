using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        public Image ProgressCircle;

        void Start()
        {
        }

        void Update()
        {

        }

        public void UpdateProgress(float val)
        {
            ProgressCircle.fillAmount = val;
        }

        public void Show(bool val)
        {
            ProgressCircle.enabled = val;
        }
    }
}