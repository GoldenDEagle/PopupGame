using DG.Tweening;
using UnityEngine;

namespace Assets.Codebase.GameElements
{
    public class Rock : MonoBehaviour
    {
        public void Remove()
        {
            // Generate random direction and distance
            var flyDirection = GenerateRandomDirection();
            float flyDistance = Random.Range(1f, 3f);

            AnimateRemoval(flyDirection, flyDistance);
        }

        private void AnimateRemoval(Vector3 flyDirection, float flyDistance)
        {
            Sequence s = DOTween.Sequence();
            s.Append(transform.DOMove(flyDirection * flyDistance, 0.5f).SetEase(Ease.OutCubic));
            s.Join(transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)), 1f));
            s.Join(transform.DOScale(0.01f, 1f));
            s.OnComplete(() =>
            {
                // On animation ended
                gameObject.SetActive(false);
            });
        }

        private Vector3 GenerateRandomDirection()
        {
            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);

            Vector2 randomDirection = new Vector2(randomX, randomY);
            randomDirection.Normalize();
            return randomDirection;
        }
    }
}