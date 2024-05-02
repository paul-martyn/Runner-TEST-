using DG.Tweening;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        private void Start() => 
            Reset();

        public void Show()
        {
            _canvasGroup.alpha = 1f;
            Enable();
        }

        public void Hide()
        {
            _canvasGroup.DOFade(0f, _duration)
                .OnComplete(Reset)
                .SetLink(gameObject);
        }
        
        private void Enable() => 
            _canvasGroup.blocksRaycasts = false;

        private void Reset()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}