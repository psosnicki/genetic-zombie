using Assets.Scripts;
using System.Text;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    SimulationController _simulation;
    void Start()
    {
        _simulation = GameObject.Find(nameof(SimulationController)).GetComponent<SimulationController>();
    }

    void Update()
    {
        GameObject.Find("Generation").GetComponent<TextMeshProUGUI>().text = $"Generation {_simulation.CurrentGeneration}";
        GameObject.Find("Survivors").GetComponent<TextMeshProUGUI>().text = $"Survived Zombies {_simulation.SurvivorsCount}/{_simulation.PopulationSize}";
        GameObject.Find("Parameters").GetComponent<TextMeshProUGUI>().text = GetSimulationParametersInfo();
    }
    private string GetSimulationParametersInfo()
    {
        return new StringBuilder()
          .AppendLine($"Population size: {_simulation.PopulationSize}")
          .AppendLine($"Mutation rate: {_simulation.MutationRate:0.00}")
          .AppendLine($"Genome length: {_simulation.GenomeLength}")
          .ToString();
    }
}
