using Assets.Codebase.GameStructure;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codebase.GameElements
{
    public class GameView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform _popup;

        public void ShowGame()
        {
            _popup.gameObject.SetActive(true);
            GameController.Instance.SwitchGameState(GameState.Active);
        }

        public void HideGame()
        {
            _popup.gameObject.SetActive(false);
            GameController.Instance.SwitchGameState(GameState.Inactive);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}