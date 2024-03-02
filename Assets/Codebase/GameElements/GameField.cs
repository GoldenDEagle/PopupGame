using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codebase.GameElements
{
    public class GameField : MonoBehaviour, IPointerDownHandler
    {
        [Header("Gem")]
        [SerializeField] private Gem _gemObject;
        [SerializeField] private RectTransform _gemInitialRect;
        [Header("Rocks")]
        [SerializeField] private List<Rock> _rockObjects;
        [SerializeField] private List<RectTransform> _rockInitialPositions;

        private int _availableRocksCount;

        public event Action OnGameWon;

        public void PrepareNewGame()
        {
            _availableRocksCount = _rockObjects.Count;
            ResetGameElements();

            // Activate all rocks, in case they were removed earlier
            foreach (var rock in _rockObjects)
            {
                rock.gameObject.SetActive(true);
            }
        }

        private void ResetGameElements()
        {
            _gemObject.transform.SetLocalPositionAndRotation(_gemInitialRect.position, _gemInitialRect.rotation);


            if (_rockObjects.Count > _rockInitialPositions.Count)
            {
                throw new System.Exception("Not enough positions defined for rocks");
            }

            for (int i = 0; i < _rockObjects.Count; i++)
            {
                _rockObjects[i].transform.position = _rockInitialPositions[i].position;
                _rockObjects[i].transform.SetLocalPositionAndRotation(_rockInitialPositions[i].position, _rockInitialPositions[i].rotation);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_availableRocksCount > 0)
            {
                RemoveNextRock();
            }
            else
            {
                return;
            }
        }

        private void RemoveNextRock()
        {
            _rockObjects[_availableRocksCount - 1].Remove();
            _availableRocksCount--;

            if (_availableRocksCount == 0)
            {
                PlayWinAnimation();
            }
        }

        private void PlayWinAnimation()
        {
            Debug.Log("Game Won!");
            OnGameWon?.Invoke();
        }
    }
}