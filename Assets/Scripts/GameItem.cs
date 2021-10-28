using System;
using System.Collections;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public event Action<GameItem> Pressed;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _colorIndex;
    [SerializeField] private int _shelfNumber;
    [SerializeField] private int _positionOnShelf;

    private Coroutine _moveToPointCo;
    private bool isMoving;

    public int ColorIndex => _colorIndex;
    public int ShelfNumber => _shelfNumber;
    public int PositionOnShelf => _positionOnShelf;
    public bool IsMoving => isMoving;

    public void SetShelfNumber(int shelfNumber)
    {
        _shelfNumber = shelfNumber;
    }

    public void SetPositionOnShelf(int shelfPosition)
    {
        _positionOnShelf = shelfPosition;
    }

    public void SetColor(Color newColor, int colorIndex)
    {
        _spriteRenderer.color = newColor;
        _colorIndex = colorIndex;
    }

    public void SetIcon(Sprite icon)
    {
        _spriteRenderer.sprite = icon;
    }
    
    public void MoveToPoint(Vector2 point)
    {
        _moveToPointCo = StartCoroutine(MoveToPointCo(point));
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator MoveToPointCo(Vector2 point)
    {
        isMoving = true;
        var startingPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startingPos, point, (elapsedTime / 1f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }
    
    private void OnMouseDown()
    {
        Pressed?.Invoke(this);
    }
}