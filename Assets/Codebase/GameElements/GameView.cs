using Assets.Codebase.GameStructure;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Assets.Codebase.GameElements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameView : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("Place where active game window should be")]
        [SerializeField] private RectTransform _shownTransform;
        [Header("Game window itself")]
        [SerializeField] private RectTransform _popup;
        [SerializeField] private GameField _gameField;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _gameField.OnGameWon += HideGame;
        }

        private void OnDisable()
        {
            _gameField.OnGameWon -= HideGame;
        }

        public void ShowGame()
        {
            // Do nothing if it's switching now
            if (GameController.Instance.State == GameState.SwitchingInProgress) return;

            GameController.Instance.SwitchGameState(GameState.SwitchingInProgress);
            _popup.gameObject.SetActive(true);
            _gameField.PrepareNewGame();

            AnimatedShow();
        }

        public void HideGame()
        {
            // Do nothing if it's switching now
            if (GameController.Instance.State == GameState.SwitchingInProgress) return;

            GameController.Instance.SwitchGameState(GameState.SwitchingInProgress);
            AnimatedHide();
        }

        private void AnimatedShow()
        {
            Sequence s = DOTween.Sequence();
            s.SetEase(Ease.OutQuart);
            s.Append(transform.DOMove(_shownTransform.position, 1f));
            s.Join(_canvasGroup.DOFade(1f, 1f));
            s.Join(_popup.DOScale(1f, 1f));
            s.OnComplete(() =>
            {
                // Select game popup to process clicks outside later
                EventSystem.current.SetSelectedGameObject(_popup.gameObject);
                GameController.Instance.SwitchGameState(GameState.Active);
            });
        }

        private void AnimatedHide()
        {
            Sequence s = DOTween.Sequence();
            s.SetEase(Ease.InQuart);
            s.Append(transform.DOLocalMove(Vector3.zero, 1f));
            s.Join(_canvasGroup.DOFade(0f, 1f));
            s.Join(_popup.DOScale(0f, 1f));
            s.OnComplete(() =>
            {
                _popup.gameObject.SetActive(false);
                GameController.Instance.SwitchGameState(GameState.Inactive);
            });
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}