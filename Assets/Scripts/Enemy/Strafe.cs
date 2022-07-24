using UnityEngine;

public class Strafe : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("Strafing distance in metres.")]
    public float distance = 1;
        
    [Tooltip("Strafing speed in m/s.")]
    public float speed = 1f;
        
    [Tooltip("When active, strafes while facing the player.")]
    public bool facePlayer;

    private Rigidbody _rigidbody;
    private float _coveredDistance;
    private int _directionMultiplier = 1;

    private Transform _player;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        DoStrafing();
        if (facePlayer)
        {
            FacePlayer();
        }
    }

    private void FacePlayer()
    {
        var playerPos = _player.position;
        transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
    }

    private void DoStrafing()
    {
        _rigidbody.velocity = transform.right * (speed * _directionMultiplier);
        _coveredDistance += Time.fixedDeltaTime * speed;
        if (_coveredDistance > distance)
        {
            _directionMultiplier *= -1;
            _coveredDistance = 0;
        }
    }
}