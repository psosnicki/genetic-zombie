using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Population<T> : ScriptableObject, IPopulation<T> where T : MonoBehaviour, Individual
{
    protected int _populationSize;
    protected int _genomeLength;
    protected float _mutationRate;
    protected float _eliteThreshold;
    protected GameObject _individualPrefab;
    protected GameObject _startPoint;
    protected GameObject _targetPoint;
    protected readonly IList<GameObject> _population = new List<GameObject>();

    public int CurrentGeneration { get; private set; } = 0;
    public bool HasAnyActiveIndividuals => _population.Any(x => x.GetComponent<T>().IsActive);
    public int SurvivorsCount => _population.Count(x => !x.GetComponent<T>().Crashed);

    protected Population() { }
    public void Initialize(int size, int genomeLength, float mutationRate, float eliteThreshold, GameObject individualPrefab, GameObject startPoint, GameObject targetPoint)
    {
        _populationSize = size;
        _genomeLength = genomeLength;
        _mutationRate = mutationRate;
        _eliteThreshold = eliteThreshold;
        _individualPrefab = individualPrefab;
        _startPoint = startPoint;
        _targetPoint = targetPoint;
        CreateZeroGeneration();
    }

    public virtual void CreateNextGeneration()
    {
        var elite = _population.OrderByDescending(x => x.GetComponent<T>().Fitness())
                               .Take(Mathf.RoundToInt(_eliteThreshold * _populationSize))
                               .ToList();

        foreach (var probe in _population)
            Destroy(probe);
        _population.Clear();

        for (int k = 0, i = 0; i < _populationSize; k++)
        {
            var chromosome = elite[k].GetComponent<T>().Chromosome;
            var offsprings = chromosome.Cross(elite[Random.Range(0, elite.Count - 1)].GetComponent<T>().Chromosome);
            foreach (var offspring in offsprings)
            {
                if (i == _populationSize) break;
                offspring.Mutate(_mutationRate);
                var go = Instantiate(_individualPrefab, (Vector2)_startPoint.transform.position, Quaternion.identity);
                var individual = go.GetComponent<T>();
                individual.Create(offspring, (Vector2)_targetPoint.transform.position);
                _population.Add(go);
                i++;
            }
            if (k == elite.Count - 1) k = 0;
        }

        foreach (var e in elite)
            Destroy(e);

        CurrentGeneration++;
    }

    public void Reset()
    {
        Destroy();
        CreateZeroGeneration();
        CurrentGeneration = 0;
    }

    public virtual void CreateZeroGeneration()
    {
        for (int i = 0; i < _populationSize; i++)
        {
            var chromosome = new Chromosome(_genomeLength);
            var go = Instantiate(_individualPrefab, (Vector2)_startPoint.transform.position, Quaternion.identity);
            var individual = go.GetComponent<T>();
            individual.Create(chromosome, (Vector2)_targetPoint.transform.position);
            _population.Add(go);
        }
    }

    public void ChangeGenomeLength(int genomeLength)
    {
        _genomeLength = genomeLength;
        Reset();
    }

    public void ChangeMutationRate(float mutationRate)
    {
        _mutationRate = mutationRate;
    }

    public void ChangePopulationSize(int size)
    {
        _populationSize = size;
    }

    private void Destroy()
    {
        foreach (var p in _population)
            Destroy(p);
        _population.Clear();
    }
}
