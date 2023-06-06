using UnityEngine;

namespace Assets.Scripts
{
    public interface IPopulation<T> where T : Individual
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
