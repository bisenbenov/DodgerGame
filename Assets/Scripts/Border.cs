using System;
using UnityEngine;

public class Border : MonoBehaviour
{
    public event EventHandler OnHitBottom;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Baddie>(out var baddie))
        {
            OnHitBottom?.Invoke(this, EventArgs.Empty);
        }
    }
}
