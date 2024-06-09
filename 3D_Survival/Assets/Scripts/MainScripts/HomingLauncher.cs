using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HomingLauncher : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    [SerializeField] float damage;
    float spawnTime;
    bool findTarget;

    private void FixedUpdate()
    {
        spawnTime -= Time.fixedDeltaTime;
        findTarget = GameManager.Instance.player.findTarget;

        if (spawnTime <= 0 && findTarget) // ¸ó½ºÅÍ ³¢¸® °ãÃÄ¼­ Å¸°ÙÀÌ ¾È¸Â¾Æ¼­ ½ºÄµÀÌ ¾ÈµÊ
        {
            HomingMissileSpawn(0);
            spawnTime = spawnDelay;
        }
    }

    public void HomingMissileSpawn(int index)
    {
        HomingMissile homingMissile = GameManager.Instance.bulletPool.GetBullet(index).GetComponent<HomingMissile>();
        homingMissile.SetDamage(damage);
        homingMissile.transform.position = transform.position;
    }
}
