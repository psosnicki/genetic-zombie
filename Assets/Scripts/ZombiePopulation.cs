using UnityEngine;

namespace Assets.Scripts
{
    public class ZombiePopulation : Population<Zombie>
    {
        private ZombiePopulation() { }
        public static IPopulation<Zombie> Create(int size, int genomeLength, float mutationRate, float eliteThreshold, GameObject individualPrefab, GameObject startPoint, GameObject targetPoint)
        {
            var population = CreateInstance<ZombiePopulation>();
            population.Initialize(size, genomeLength, mutationRate, eliteThreshold, individualPrefab, startPoint, targetPoint);
            return population;
        }
    }
}
