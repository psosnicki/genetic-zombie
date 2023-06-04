using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _isDragging;

    public Color DragColor = new(1f, 1f, 1f, 0.5f);

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        _isDragging = true;
    }

    public void OnMouseUp()
    {
        _isDragging = false;
        _spriteRenderer.color = Color.white;
    }

    private void OnMouseOver()
    {
        _spriteRenderer.color = DragColor;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = Color.white;
    }

    void Update()
    {
        if (_isDragging)
        {
            _spriteRenderer.color  = DragColor;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
