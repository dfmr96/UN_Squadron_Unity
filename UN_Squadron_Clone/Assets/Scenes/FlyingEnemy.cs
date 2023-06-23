using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (_player != null)
        {
            Chase();
        }
    }

    private void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

    public void SetTarget(Player target)
    {
        _player = target;
    }
}
