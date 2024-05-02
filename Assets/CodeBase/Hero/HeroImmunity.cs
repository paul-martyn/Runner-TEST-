using System.Collections;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroImmunity : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _fx;
        
        private Coroutine _immunityCoroutine;
        
        private const int HeroLayer = 31; 
        private const int ObstacleLayer = 30; 
        private const int InvisibleGroundLayer = 28;

        public void Activate(float duration)
        {
            if (_immunityCoroutine != null) 
                StopCoroutine(_immunityCoroutine);
            _immunityCoroutine = StartCoroutine(ImmunityCoroutine(duration));
        }

        private IEnumerator ImmunityCoroutine(float duration)
        {
            Physics.IgnoreLayerCollision(HeroLayer, ObstacleLayer, true);
            Physics.IgnoreLayerCollision(HeroLayer, InvisibleGroundLayer, false);
            _fx.Play();
            yield return new WaitForSeconds(duration);
            _fx.Stop();
            Physics.IgnoreLayerCollision(HeroLayer, ObstacleLayer, false);
            Physics.IgnoreLayerCollision(HeroLayer, InvisibleGroundLayer, true);
        }
    }
}