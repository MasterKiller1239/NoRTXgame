using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    public int PlayerHealth = 3;
    public int PlayerMaxHealth = 3;
    public int EnemyHealth = 5;
    public int EnemyMaxHealth = 5;

    // EnemyDefend, EnemyHit, EnemyAttack, PlayerHit, PlayerFailed, PlayerWon
    [Header("Attack type sounds")]
    public AudioClip EnemyDefend;
    public AudioClip EnemyAttack;

    [Header("Entity hit sounds")]
    public AudioClip EnemyHit;
    public AudioClip PlayerHit;

    [Header("Entity miss sounds")]
    public AudioClip EnemyMiss;
    public AudioClip PlayerMiss;

    [Header("Endgame sounds")]
    public AudioClip PlayerFailed;
    public AudioClip PlayerWon;
    public Transform Leftside;
    public Transform Rightside;
    public AudioSource source;
    IEnumerator _enemyLogic;

    public List<int> Attacks = new List<int>();

    public int AttackCount = 10;
    public int MaxOffenseInRow = 3;

    public float period = 0.0f;
    int currentpos = 1;
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
            yield return new WaitForSecondsRealtime(EnemyLogicCooldown);

            if (Attacks.Count <= 0)
                GenerateAttacks();

            int moveType = Attacks[0];
            Attacks.RemoveAt(0);

            currentpos = Random.Range(0, 2);

            //Debug.Log($"Type {(moveType == 0 ? "Defend from player" : "Attack player")}, currentPos: {currentpos}");
            Attacked = true;

            // Defend from player
            if (moveType == 0)
                PlaySound(currentpos, SoundType.EnemyDefend);
            else
                PlaySound(currentpos, SoundType.EnemyAttack);

            while (!PadController.Instance.GotInput && !PenController.Instance.GotInput && period < EnemyLogicCooldown)
            {
                period += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (PadController.Instance.GotInput || PenController.Instance.GotInput)
            {
                // Defend from player - use PenController
                if (moveType == 0)
                {
                    if (currentpos == 0 && PenController.Instance.SwipedLeft)
                    {
                        --EnemyHealth;
                        period = 0;
                        PlaySound(currentpos, SoundType.EnemyHit);
                    }
                    else if (currentpos == 1 && PenController.Instance.SwipedRight)
                    {
                        --EnemyHealth;
                        period = 0;
                        PlaySound(currentpos, SoundType.EnemyHit);
                    }
                    else
                    {
                        PlaySound(currentpos, SoundType.PlayerMiss);
                        period = 0;
                    }
                }
                // Attack player - use PadController
                else
                {
                    if (currentpos == 0 && PadController.Instance.XPressed)
                    {

                        period = 0;
                        PlaySound(currentpos, SoundType.EnemyMiss);
                    }
                    else if (currentpos == 1 && PadController.Instance.BPressed)
                    {
                        period = 0;
                        PlaySound(currentpos, SoundType.EnemyMiss);
                    }
                    else
                    {
                        --PlayerHealth;
                        period = 0;
                        PlaySound(currentpos, SoundType.PlayerHit);
                    }
                }
            }
            else
            {
                if (moveType == 0)
                    PlaySound(currentpos, SoundType.PlayerMiss);
                else
                    PlaySound(currentpos, SoundType.PlayerHit);
                period = 0;
            }           

            if (PlayerHealth <= 0)
            {
                yield return new WaitForSecondsRealtime(2.0f);
                PlaySound(currentpos, SoundType.PlayerFailed);
                PlayerHealth = PlayerMaxHealth;
                EnemyHealth = EnemyMaxHealth;
                yield return new WaitForSecondsRealtime(EnemyLogicCooldown);
                Attacks.Clear();

            }

            if (EnemyHealth <= 0)
            {
                EnemyHealth = EnemyMaxHealth;
                PlayerHealth = PlayerMaxHealth;
                yield return new WaitForSecondsRealtime(2.0f);
                PlaySound(currentpos, SoundType.PlayerWon);
                yield return new WaitForSecondsRealtime(2.0f);
                Attacks.Clear();
            }

            Attacked = false;

            PadController.Instance.ResetPadInput();
            PenController.Instance.ResetPenInput();

        }
    }

    public void PlaySound(int side, SoundType type)
    {
        if (side == 0)
        {
            this.transform.position = Leftside.position;
        }
        else
        {
            this.transform.position = Rightside.position;
        }
        switch (type)
        {

            case SoundType.EnemyDefend:
                source.clip = EnemyDefend;
                break;

            case SoundType.EnemyHit:
                source.clip = EnemyHit;
                break;

            case SoundType.EnemyAttack:
                source.clip = EnemyAttack;
                break;

            case SoundType.PlayerHit:
                source.clip = PlayerHit;
                break;

            case SoundType.PlayerWon:
                source.clip = PlayerWon;
                break;

            case SoundType.PlayerFailed:
                source.clip = PlayerFailed;
                break;

            case SoundType.EnemyMiss:
                source.clip = EnemyMiss;
                break;

            case SoundType.PlayerMiss:
                source.clip = PlayerMiss;
                break;

            default:

                break;
        
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

public enum SoundType
{
    EnemyDefend, EnemyHit, EnemyAttack, PlayerHit, PlayerFailed, PlayerWon, PlayerMiss, EnemyMiss
}
