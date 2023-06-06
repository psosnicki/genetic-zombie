using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Individual: MonoBehaviour
    {
        protected bool _ready;
        protected Vector2 _target;
        protected Chromosome _chromosome;
        public Chromosome Chromosome => _chromosome;
        public abstract bool IsActive { get; }
        public abstract bool Crashed { get; }
        public void Create(Chromosome chromosome, Vector2 target)
        {
            _chromosome = chromosome;
            _target = target;
            _ready = true;
        }
        public abstract float Fitness();
    }
}
