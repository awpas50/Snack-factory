using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public GameObject path;
    [HideInInspector] public int listSize;
    [HideInInspector] public List<Transform> pathToFollow;
    [HideInInspector] public Transform target;
    public int index = 0;
    public float speed;
    [HideInInspector] public float speed_original;
    private bool deductedLives = false;
    void Start()
    {
        speed_original = speed;
        listSize = path.transform.childCount;
        for (int i = 0; i < listSize; i++)
        {
            pathToFollow.Add(path.transform.GetChild(i));
        }
        target = pathToFollow[0];
    }

    void Update()
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(target.transform.position, transform.position) <= 0.1f)
        {
            // reached the end
            if (index >= pathToFollow.Count - 1)
            {
                if (!deductedLives)
                {
                    GameManager.i.live -= 1;
                    deductedLives = true;
                }
                Destroy(gameObject, 0.1f);
                return;
            }
            index++;
            target = pathToFollow[index];
        }
    }
}
