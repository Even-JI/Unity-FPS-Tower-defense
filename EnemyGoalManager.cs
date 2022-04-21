using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoalManager : MonoBehaviour
{
    #region Singleton;

    public static EnemyGoalManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject EnemyGoal;
}
