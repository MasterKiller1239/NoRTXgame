using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Health = 5;
    public AudioClip Attack;
    public AudioClip Defend;
    public Transform Leftside;
    public Transform Rightside;
    public AudioSource source;
    IEnumerator _enemyLogic;
    // Start is called before the first frame update
    void Start()
    {
        _enemyLogic = LoopTest();
        StartCoroutine(_enemyLogic);
    }
    public void PlaySound(int side,int type)
    {
        if (side == 0)
        {
            this.transform.position = Leftside.position;
        }
        else
        {
            this.transform.position = Rightside.position;
        }
        if (type == 0)
        {
            source.clip = Attack;
        }
        else
        {
            source.clip = Defend;
            
        }
        source.Play();

    }
    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator LoopTest()
    {
        while(true)
        {
            PlaySound(Random.Range(0, 2), Random.Range(0, 2));
            yield return new WaitForSecondsRealtime(4);
        }
       
    }
}
