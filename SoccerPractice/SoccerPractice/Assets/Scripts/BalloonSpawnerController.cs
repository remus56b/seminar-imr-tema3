using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawnerController : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    public void SpawnBalloon()
    {
        int chance = Random.Range(0, 2);
        if (chance == 1)
        {
            Instantiate(prefab, new Vector3(transform.position.x, -2, transform.position.z), Quaternion.identity);
        }
    }

}
