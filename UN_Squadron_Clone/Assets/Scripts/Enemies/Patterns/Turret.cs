using UnityEngine;
public class Turret : Enemy
{

    protected float _angleToShoot;
    
    protected void ChangeTurretSprite(Vector3 dir)
    {
        float angle = (int)Vector3.Angle(dir, -transform.right);
        _spriteRenderer.flipX = false;
        if (angle >= 0 && angle < 20)
        {
            _spriteRenderer.sprite = _sprites[0];
            _angleToShoot = 180;
        }
        else if (angle >= 20 && angle < 40)
        {
            _spriteRenderer.sprite = _sprites[1];
            _angleToShoot = 150;
        }
        else if (angle >= 40 && angle < 60)
        {
            _spriteRenderer.sprite = _sprites[2];
            _angleToShoot = 135;
        }
        else if (angle >= 60 && angle < 80)
        {
            _spriteRenderer.sprite = _sprites[3];
            _angleToShoot = 120;
        }
        else if (angle >= 80 && angle < 100)
        {
            _spriteRenderer.sprite = _sprites[4];
            _angleToShoot = 90;

        }
        else if (angle >= 100 && angle < 120)
        {
            _spriteRenderer.sprite = _sprites[3];
            _spriteRenderer.flipX = true;
            _angleToShoot = 60;
        }
        else if (angle >= 120 && angle < 140)
        {
            _spriteRenderer.sprite = _sprites[2];
            _spriteRenderer.flipX = true;
            _angleToShoot = 45;
        }
        else if (angle >= 140 && angle < 160)
        {
            _spriteRenderer.sprite = _sprites[1];
            _spriteRenderer.flipX = true;
            _angleToShoot = 35;
        }
        else if (angle >= 160 && angle < 180)
        {
            _spriteRenderer.sprite = _sprites[0];
            _spriteRenderer.flipX = true;
            _angleToShoot = 0;
        }
    }
}
