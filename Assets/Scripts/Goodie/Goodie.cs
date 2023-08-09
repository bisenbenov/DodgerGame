using System;
using UnityEngine;

public class Goodie : MonoBehaviour
{
    [SerializeField] private Transform _goodGuy;
    [SerializeField] private float _moveSpeed = 2.0f;

    private InputSystem _inputSystem;

    private Vector3 _position;
    private bool _isGoodieAlive = true;
    public bool isGoodieAlive => _isGoodieAlive;

    public event EventHandler onGoodieDied;
    
    private void Awake()
    {
        _inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
        _position = transform.position;
    }

    private void Update()
    {
        Vector2 direction = _inputSystem.Player.Move.ReadValue<Vector2>();
        Move(direction);
    }
    
    public void ResetGoodie()
    {
        _goodGuy.position = _position;
        _goodGuy.gameObject.SetActive(true);
        _isGoodieAlive = true;
    }

    private void Move(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, direction.y);
        _goodGuy.position += moveDirection * _moveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Baddie>(out var baddie))
        {
            _goodGuy.gameObject.gameObject.SetActive(false);
            _isGoodieAlive = false;
            onGoodieDied?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
    }
}
