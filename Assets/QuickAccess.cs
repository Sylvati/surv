using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAccess : MonoBehaviour
{
    public Dictionary<string, GameObject> important = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> enemyDict = new Dictionary<string, GameObject>();
    public List<GameObject> aliveEnemies = new List<GameObject>();
}
