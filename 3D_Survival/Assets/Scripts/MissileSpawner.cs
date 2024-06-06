using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay;

    float spawnTime;
    int count = 0;
    bool findTarget;

    public GameObject missilePrefab; // �̻��� ������
    public Transform spawnPoint; // �̻��� �߻� ��ġ

    private void FixedUpdate()
    {
        spawnTime -= Time.fixedDeltaTime;
        findTarget = GameManager.Instance.player.findTarget;

        if (spawnTime <= 0 && findTarget) // ���� ���� ���ļ� Ÿ���� �ȸ¾Ƽ� ��ĵ�� �ȵ�
        {
            MissileSpawn(0);
            spawnTime = spawnDelay;
        }
    }

    public void MissileSpawn(int index)
    {
        //Debug.Log("�̻��� ����");
        //GameObject missile = GameManager.Instance.missilePool.GetMissile(index);
        //missile.transform.position = transform.position;

        GameObject missile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);
        Missile missileComponent = missile.GetComponent<Missile>();
    }
}