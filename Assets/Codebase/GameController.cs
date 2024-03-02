using Assets.Codebase.GameElements;
using Assets.Codebase.GameStructure;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codebase
{
    public class GameController : MonoBehaviour, IPointerClickHandler
    {
        public static GameController Instance { get; private set; }

        [SerializeField] private GameView _gameWindow;

        private GameState _state = GameState.Inactive;
        public GameState State => _state;

        public event Action<GameState> OnStateChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (_state)
            {
                case GameState.Inactive:
                    OpenGame();
                    break;
                case GameState.Active:
                    CloseGame();
                    break;
                case GameState.SwitchingState:
                    break;
            }
        }

        public void SwitchGameState(GameState newState)
        {
            _state = newState;
            OnStateChanged?.Invoke(newState);
            Debug.Log("Switched game state to: " + newState);
        }

        private void OpenGame()
        {
            SwitchGameState(GameState.SwitchingState);
            _gameWindow.ShowGame();
        }

        private void CloseGame()
        {
            SwitchGameState(GameState.SwitchingState);
            _gameWindow.HideGame();
        }
    }
}