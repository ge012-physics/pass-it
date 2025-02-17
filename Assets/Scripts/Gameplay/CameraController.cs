using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _followSpeed = 10;
    [SerializeField] Vector2 _zoomLimits;
    [SerializeField] Vector3 _offset;

    float _boundsSize;
    GameManager _game;

    void Start()
    {
        _game = GameManager.Instance;
    }

    private void LateUpdate()
    {
        if (_game.AlivePlayers.Count == 0) return;

        var center = GetBoundsCenter();
        center.z = -Mathf.Lerp(_zoomLimits.x, _zoomLimits.y, _boundsSize / _zoomLimits.y);
        var pos = Vector3.Lerp(transform.position, center + _offset, _followSpeed * Time.deltaTime);
        transform.position = pos;
    }

    Vector3 GetBoundsCenter()
    {
        var targets = _game.AlivePlayers;
        if (targets.Count == 1) return targets[0].transform.position;

        Bounds bounds = new(targets[0].transform.position, Vector3.zero);

        foreach (var target in targets)
        {
            bounds.Encapsulate(target.transform.position);
        }
        _boundsSize = bounds.size.x;

        return bounds.center;
    }
}
