using Assets.Codebase.GameStructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codebase.GameElements
{
    public class GameView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform _popup;
        [SerializeField] private GameField _gameField;

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
            GameController.Instance.SwitchGameState(GameState.SwitchingState);
            _popup.gameObject.SetActive(true);
            _gameField.PrepareNewGame();
            // Select game popup to process clicks outside later
            EventSystem.current.SetSelectedGameObject(_popup.gameObject);
            // Animate popup show
            GameController.Instance.SwitchGameState(GameState.Active);
        }

        public void HideGame()
        {
            GameController.Instance.SwitchGameState(GameState.SwitchingState);
            // Animate popup hide
            // Disselect game popup
            EventSystem.current.SetSelectedGameObject(null);
            _popup.gameObject.SetActive(false);
            GameController.Instance.SwitchGameState(GameState.Inactive);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}