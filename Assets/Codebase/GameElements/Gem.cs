using System;
using UnityEngine;

namespace Assets.Codebase.GameElements
{
    public class Gem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _winParticles;

        public ParticleSystem WinParticles => _winParticles;

        public event Action OnWinAnimationEnded;

        private void OnParticleSystemStopped()
        {
            OnWinAnimationEnded?.Invoke();
        }
    }
}