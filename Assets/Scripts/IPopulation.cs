using UnityEngine;

namespace Assets.Scripts
{
    public interface IPopulation<T> where T : MonoBehaviour, Individual
    {
        int CurrentGeneration { get; }
        bool HasAnyActiveIndividuals { get; }
        int SurvivorsCount { get; }
        void ChangeMutationRate(float mutationRate);
        void ChangePopulationSize(int size);
        void CreateNextGeneration();
        void Reset();
        void ChangeGenomeLength(int genomeLength);
    }
}
