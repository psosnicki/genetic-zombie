using UnityEngine;

namespace Assets.Scripts
{
    public class SimulationController : MonoBehaviour
    {
        private IPopulation<Zombie> _zombiePopulation;
        public int CurrentGeneration => _zombiePopulation.CurrentGeneration;
        public int SurvivorsCount => _zombiePopulation.SurvivorsCount;

        public float MutationRate = 0.01f;
        public float EliteThreshold = 0.25f;
        public GameObject IndividualPrefab;
        public int PopulationSize=200;
        public int GenomeLength = 100;
        public GameObject TargetPoint;
        public GameObject StartPoint;
        public float Speed = 30f;

        void Start()
        {          
            _zombiePopulation = ZombiePopulation.Create(PopulationSize, GenomeLength, MutationRate, EliteThreshold, IndividualPrefab, StartPoint,TargetPoint);
        }

        void Update()
        {
            if (!_zombiePopulation.HasAnyActiveIndividuals)
                _zombiePopulation.CreateNextGeneration();
        }

        public void RecreatePopulation() => _zombiePopulation.Reset();
        
        #region Event Handlers

        public void ChangePopulationSize(float value)
        {
            PopulationSize = (int)value;
            _zombiePopulation.ChangePopulationSize(PopulationSize);
            _zombiePopulation.Reset();
        }

        public void ChangeMutationRate(float value)
        {
            MutationRate = value;
            _zombiePopulation.ChangeMutationRate(MutationRate);
        }

        public void ChangeGenomeLength(float value)
        {
            GenomeLength = (int)value;
            _zombiePopulation.ChangeGenomeLength(GenomeLength);
            _zombiePopulation.Reset();
        }

        #endregion
    }
}
