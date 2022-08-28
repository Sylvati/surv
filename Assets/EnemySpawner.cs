using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    QuickAccess quickAccess;
    [SerializeField] private bool on;
    private void Start() {
        quickAccess = GameObject.FindWithTag("Game Manager").GetComponent<QuickAccess>();
        InvokeRepeating("Spawn", 0f, 1f);
    }

    private void Spawn() {

        if (!on) return;
        GameObject enemy = Instantiate(quickAccess.enemyDict["Basic"]);
        enemy.transform.position = new Vector3(-20, 0, 0);
    }
}
