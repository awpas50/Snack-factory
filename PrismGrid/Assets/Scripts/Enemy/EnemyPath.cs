using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{

    //effect
    public Color originalColor;
    public Color burntColor;
    public GameObject burnEffect;
    //icon
    public SpriteRenderer speedBoostIcon;
    //state
    public int worthScore = 1;
    private int worth = 3;
    private bool isBurnt = false;
    private float burnTimer = 0;
    [SerializeField] private float burnDuration = 3;

    public GameObject path;
    [HideInInspector] public int listSize;
    [HideInInspector] public List<Transform> pathToFollow;
    [HideInInspector] public Transform target;
    public int index = 0;
    public float speed;
    [HideInInspector] public float speed_original;
    [HideInInspector] public float speed_burnt = 1;
    [HideInInspector] public float speed_buffed = 1;
    private bool addProps = false;
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
        //Speed

        speed = speed_original * speed_burnt * speed_buffed;

        // Following path:
        Vector3 dir = target.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(target.transform.position, transform.position) <= 0.1f)
        {
            // reached the end
            if (index >= pathToFollow.Count - 1)
            {
                // add scores
                if (!addProps)
                {
                    GameManager.i.live -= 1;
                    GameManager.i.score += worthScore;
                    GameManager.i.currency += worth;
                    addProps = true;
                }
                Destroy(gameObject, 0.1f);
                return;
            }
            index++;
            target = pathToFollow[index];
        }

        // enemy will ignore any other lasers in 5 seconds.
        if(isBurnt)
        {
            burnEffect.SetActive(true);
            GetComponent<SpriteRenderer>().color = burntColor;
            burnTimer += Time.deltaTime;
            if(burnTimer >= burnDuration)
            {
                burnTimer = 0;
                burnEffect.SetActive(false);
                GetComponent<SpriteRenderer>().color = originalColor;
                speed_burnt = 1;
                isBurnt = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy reaction to laser:
        if (other.gameObject.tag == "Laser" && !isBurnt)
        {
            //80% buff
            speed_burnt = 1.8f;
            isBurnt = true;
            //temporary disable the laser (5 sec)
            other.gameObject.GetComponent<LineRenderer>().enabled = false;
            other.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            other.gameObject.GetComponent<Laser>().hasEnergy = false;
        }
    }
}
