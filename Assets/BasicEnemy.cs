using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    bool playerIsSet;
    GameObject player = null;
    QuickAccess quickAccess;

    [SerializeField] string enemyName;
    [SerializeField] float speed = 1f;

    private void Start() {
        quickAccess = GameObject.FindWithTag("Game Manager").GetComponent<QuickAccess>();
        if(!quickAccess.enemyDict.ContainsKey(enemyName)) {
            quickAccess.enemyDict.Add(enemyName, gameObject);
        }
        
        quickAccess.aliveEnemies.Add(gameObject);
    }
    private void Update() {

        if (!playerIsSet) { //Optimized way to get the player once and just roll with it
            if (!quickAccess.important.TryGetValue("Player", out player)) {
                playerIsSet = true;
                return;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, new Vector3(player.transform.position.x, transform.position.y, 0f)); //Draw Adjacent
        Gizmos.DrawLine(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, 0f)); //Draw Hypotenuse
        Gizmos.DrawLine(player.transform.position, new Vector3(player.transform.position.x, transform.position.y, 0f)); //Draw Opposite
    }


}
