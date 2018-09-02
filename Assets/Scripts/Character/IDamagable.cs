using UnityEngine;

namespace Complete
{
    public interface IDamagable
    {


        void TakeDamage(float damage);

        void ShowDamageAnimation(ParticleSystem particlesystem);

    }
}
