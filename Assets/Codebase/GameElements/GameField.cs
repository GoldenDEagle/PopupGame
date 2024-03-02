using DG.Tweening;
using System;
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
        [Header("Animation params")]
        [SerializeField] private float _smashShakeStrength = 10f;

        // Number of rocks in the pile
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

        /// <summary>
        /// Sets elements to initial position
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        private void ResetGameElements()
        {
            _gemObject.transform.SetLocalPositionAndRotation(_gemInitialRect.localPosition, _gemInitialRect.localRotation);


            if (_rockObjects.Count > _rockInitialPositions.Count)
            {
                throw new System.Exception("Not enough positions defined for rocks");
            }

            for (int i = 0; i < _rockObjects.Count; i++)
            {
                _rockObjects[i].transform.localScale = Vector3.one;
                _rockObjects[i].transform.position = _rockInitialPositions[i].position;
                _rockObjects[i].transform.SetLocalPositionAndRotation(_rockInitialPositions[i].localPosition, _rockInitialPositions[i].localRotation);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_availableRocksCount > 0)
            {
                ShakeField();
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

        private void ShakeField()
        {
            transform.DOShakeRotation(0.5f, _smashShakeStrength).SetEase(Ease.OutElastic);
        }

        private void PlayWinAnimation()
        {
            _gemObject.OnWinAnimationEnded += OnWinAnimationEnded;
            _gemObject.WinParticles.Play();
        }

        private void OnWinAnimationEnded()
        {
            _gemObject.OnWinAnimationEnded -= OnWinAnimationEnded;
            Debug.Log("Game won!");
            OnGameWon?.Invoke();
        }

        private void OnDisable()
        {
            // Reset rotation if closed during shake animation
            DOTween.Kill(transform);
            transform.rotation = Quaternion.identity;
        }
    }
}