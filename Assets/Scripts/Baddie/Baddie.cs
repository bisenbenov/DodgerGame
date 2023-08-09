using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Baddie : MonoBehaviour
{
    private float _scale;
    private float _velocity = 2f;

    public event EventHandler<Baddie> OnReachBottom;

    private void OnEnable()
    {
        _scale = Random.Range(0.5f, 4.2f);
        _velocity = Random.Range(0.5f, 4f);
        gameObject.transform.localScale = new Vector3(_scale, _scale);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.down * _velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Border>(out var border))
        {
            OnReachBottom?.Invoke(this, this);
        }
    }
}
