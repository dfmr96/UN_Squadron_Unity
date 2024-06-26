using UnityEngine;
public class Turret : Enemy
{

    protected float _angleToShoot;
    
    protected void ChangeTurretSprite(Vector3 dir)
    {
        float angle = Vector3.Angle(dir, -transform.right);
        bool flipX = false;
        Sprite selectedSprite = null;
        int angleToShoot = 0;

        var anglesMap = new (float, float, Sprite, int)[]
        {
            (0, 20, _sprites[0], 180),
            (20, 40, _sprites[1], 150),
            (40, 60, _sprites[2], 135),
            (60, 80, _sprites[3], 120),
            (80, 100, _sprites[4], 90),
            (100, 120, _sprites[3], 60),
            (120, 140, _sprites[2], 45),
            (140, 160, _sprites[1], 35),
            (160, 180, _sprites[0], 0),
        };

        foreach (var (minAngle, maxAngle, sprite, shootAngle) in anglesMap)
        {
            if (angle >= minAngle && angle < maxAngle)
            {
                selectedSprite = sprite;
                angleToShoot = shootAngle;
                if (minAngle >= 100)
                {
                    flipX = true;
                }
                break;
            }
        }

        _spriteRenderer.sprite = selectedSprite;
        _spriteRenderer.flipX = flipX;
        _angleToShoot = angleToShoot;
    }
}
