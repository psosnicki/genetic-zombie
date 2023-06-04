using UnityEngine;

namespace Assets.Scripts
{
    public interface Individual
    {
        public Chromosome Chromosome { get; }
        public bool IsActive { get; }
        public bool Crashed { get; }
        public void  Create(Chromosome chromosome, Vector2 target);
        public float Fitness();
    }
}
