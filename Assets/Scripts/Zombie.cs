using Assets.Scripts;
using UnityEngine;

public class Zombie : MonoBehaviour,  Individual
{
    private SpriteRenderer _spriteRenderer;
    private Chromosome _chromosome;
    private int _pathIndex = 0;
    private Vector2 _nextPosition, _target;
    private bool _pathCompleted;
    private bool _crashed, _ready;
    private Color _crashedColor = Color.red;
    private readonly string _deadAnimationTirgger = "DeadTrigger";

    public bool IsActive => !_pathCompleted && !_crashed;
    public Chromosome Chromosome => _chromosome;
    public bool Crashed => _crashed;
    public LayerMask ObstaclesLayer;
    public float Speed = 10f;
    public float TargetDistance => Vector2.Distance(transform.position, _target);

    public  float Fitness()
    {
        var hitsFactor = 1f;
        if (Settings.Instance.EnableObstacleRaycast)
        {
            var hits = Physics2D.RaycastAll(transform.position, _target, ObstaclesLayer);
            hitsFactor = 1f - 0.2f * hits.Length;
        }
        var targetDistance = Vector2.Distance(transform.position, _target);
        if (targetDistance == 0)
            targetDistance = 0.009f;
        return hitsFactor * (_crashed ? 0.5f : 1f) / targetDistance;
    }

    public  void Create(Chromosome chromosome, Vector2 target)
    {
        _chromosome = chromosome;
        _target = target;
        _ready = true;
    }

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _nextPosition = transform.position;
    }

    void Update()
    {
        if (!_ready) return;

        if (_crashed)
        {
            _spriteRenderer.color = _crashedColor;
            return;
        }

        if (_pathIndex == _chromosome.GenomeLength - 1)
        {
            _pathCompleted = true;
            return;
        }

        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f - (float)_pathIndex / (3* _chromosome.GenomeLength));

        if ((Vector2)transform.position == _nextPosition)
        {
            _nextPosition = (Vector2)transform.position + _chromosome.Genes[_pathIndex];
            _pathIndex++;
        }
        else transform.position = Vector2.MoveTowards(transform.position, _nextPosition, Settings.Instance.Speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _crashed = true;
        GetComponent<Animator>().SetTrigger(_deadAnimationTirgger);
    }
}
