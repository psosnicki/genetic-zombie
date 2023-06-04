using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chromosome
{
    public int GenomeLength { get; private set; }
    public IList<Vector2> Genes { get; private set; } = new List<Vector2>();

    public Chromosome(int genomeLength=40)
    {
        GenomeLength = genomeLength;
        for (int i = 0; i < genomeLength; i++)
            Genes.Add(new Vector2(Random.Range(0f, 1f), Random.Range(-1.2f, 1.2f)));
    }

    private Chromosome(IList<Vector2> genes)
    {
        GenomeLength = genes.Count;
        Genes = genes;
    }

    public IEnumerable<Chromosome> Cross(Chromosome partner)
    {
        var splitLineIndex = Random.Range(0, GenomeLength);
        yield return new Chromosome(partner.Genes.Take(splitLineIndex).Concat(Genes.Skip(splitLineIndex)).ToList());
        yield return new Chromosome(Genes.Take(splitLineIndex).Concat(partner.Genes.Skip(splitLineIndex)).ToList());
    }

    public void Mutate(float mutationRate=0.01f)
    {
        for (int i = 0; i < Genes.Count; i++)
        {
            if(Random.Range(0f,1f) <= mutationRate)
                Genes[i] = new Vector2(Random.Range(0f, 1f), Random.Range(-1f, 1f));
        }
    }
}

