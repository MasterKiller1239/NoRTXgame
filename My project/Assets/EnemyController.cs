using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    public int Health = 5;
    public AudioClip Attack;
    public AudioClip Defend;
    public Transform Leftside;
    public Transform Rightside;
    public AudioSource source;
    IEnumerator _enemyLogic;

    public List<int> Attacks = new List<int>();

    public int AttackCount = 10;
    public int MaxOffenseInRow = 3;

    public float period = 0.0f;
    int currentpos = 1;
    public int i = 0;
    public float EnemyLogicCooldown = 5.0f;
    public bool Attacked = false;
    public bool GenerateInitialAttacks = true;

    // Start is called before the first frame update
    void Start()
    {
        if (GenerateInitialAttacks)
            GenerateAttacks();

        Instance = this;
        _enemyLogic = EnemyLogic();
        StartCoroutine(_enemyLogic);
    }

    IEnumerator EnemyLogic()
    {
        for (; ; )
        {
            Debug.Log("Waiting for attack");
            yield return new WaitForSecondsRealtime(EnemyLogicCooldown);

            if (Attacks.Count <= 0)
                GenerateAttacks();

            int moveType = Attacks[0];
            Attacks.RemoveAt(0);

            currentpos = Random.Range(0, 2);

            Debug.Log($"Type {(moveType == 0 ? "Defend from player" : "Attack player")}, currentPos: {currentpos}");
            Attacked = true;
            PlaySound(currentpos, 0);
            while (!PadController.Instance.GotInput && !PenController.Instance.GotInput && period < EnemyLogicCooldown)
            {
                period += UnityEngine.Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (PadController.Instance.GotInput || PenController.Instance.GotInput)
            {
                if (currentpos == 0 && (PadController.Instance.XPressed || PenController.Instance.SwipedLeft))
                {
                    Debug.Log("Hit");
                    i--;
                    period = 0;
                    PlaySound(currentpos, 1);
                }
                else if (currentpos == 1 && (PadController.Instance.BPressed || PenController.Instance.SwipedRight))
                {
                    Debug.Log("Hit");
                    i--;
                    period = 0;
                    PlaySound(currentpos, 1);
                }
                else
                {
                    Debug.Log("Wrong input");
                    Debug.Log("Miss");
                    period = 0;
                }
            }
            else
            {
                Debug.Log("Not enough time");
                Debug.Log("Miss");
                period = 0;
            }

            Attacked = false;

            PadController.Instance.ResetPadInput();
            PenController.Instance.ResetPenInput();
        }
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

    void GenerateAttacks()
    {
        int offenseInRow = 0;
        for (int i = 0; i < AttackCount; ++i)
        {
            // 0 - defend from player, 1 - attack player
            int attack = Random.Range(0, 2);

            if (offenseInRow >= MaxOffenseInRow)
            {
                attack = 0;
                offenseInRow = 0;
            }

            Attacks.Add(attack);

            if (attack == 1)
                ++offenseInRow;
        }
    }

}
