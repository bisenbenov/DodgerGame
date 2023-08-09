using UnityEngine;

public class BaddieSpawner : MonoBehaviour
{
    [SerializeField] private Baddie _baddiePrefab;
    [SerializeField] private int _baddiesCount = 12;
    [SerializeField] private Transform _bottomBorder;
    [SerializeField] private Transform _lineStart;
    [SerializeField] private Transform _lineEnd;

    private BaddiePool _baddiePool;

    private void Awake()
    {
        _baddiePool = new BaddiePool(_baddiePrefab, _baddiesCount);
    }

    private void OnEnable()
    {
        SpawnBaddies();
    }

    private void SpawnBaddies()
    {
        for (int i = 0; i < _baddiesCount; i++)
        {
            var baddie = _baddiePool.Get();
            baddie.transform.position = SpawnRandom(_lineStart, _lineEnd);
            baddie.OnReachBottom += ReturnToPool;
        }
    }

    private void ReturnToPool(object sender, Baddie baddie)
    {
        _baddiePool.Return(baddie);
        baddie.transform.position = SpawnRandom(_lineStart, _lineEnd);
        _baddiePool.Get();
    }

    private Vector3 SpawnRandom(Transform lineStart, Transform lineEnd)
    {
        var xRange = lineEnd.position.x - lineStart.position.x;
        var yRange = lineEnd.position.y - lineStart.position.y;

        var spawn = new Vector3(lineStart.position.x + (xRange * Random.value),
            lineStart.position.y + (yRange * Random.value));
        
        return spawn;
    }

    private void OnDisable()
    {
        _baddiePool.ReturnAll();
    }

    public void ResetBaddies()
    {
        _baddiePool.ReturnAll();
    }
}
