using Assets.Codebase.GameElements;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codebase.GameStructure
{
    /// <summary>
    /// Singleton, launching game and controlling its states
    /// </summary>
    public class GameController : MonoBehaviour, IPointerClickHandler
    {
        public static GameController Instance { get; private set; }

        [SerializeField] private GameView _gameView;

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
            if (_state == GameState.Inactive)
            {
                OpenGame();
            }
        }

        public void SwitchGameState(GameState newState)
        {
            _state = newState;
            OnStateChanged?.Invoke(newState);
            Debug.Log("Minigame state: " + newState);
        }

        private void OpenGame()
        {
            _gameView.ShowGame();
        }
    }
}