using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int enemyCount;
    public float radiusOfSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyDrop());
    }

    Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) 
        {
            finalPosition = hit.position;            
            finalPosition.y = 0f;
        }
        return finalPosition;
    }

    // Update is called once per frame
    IEnumerator enemyDrop()
    {
        int counter = 0;
        while(counter < enemyCount)
        {   
            Instantiate(enemy, RandomNavmeshLocation(radiusOfSpawn), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            counter += 1;
        }
    }
}
