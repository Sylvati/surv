using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController controller;
    private Vector3 playerVelocity;
    QuickAccess quickAccess;
    private GameObject closest;
    private float angle;
    private GameObject currentProjectile;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private GameObject projectilePrefab;

    private void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        quickAccess = GameObject.FindWithTag("Game Manager").GetComponent<QuickAccess>();
        quickAccess.important.Add("Player", gameObject);

    }

    private void Update() {

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        controller.Move(playerVelocity * Time.deltaTime);

        closest = GetClosestEnemy(quickAccess.aliveEnemies.ToArray());
        
        angle = AngleToClosestEnemy();

        MakeAndAimProjectile(projectilePrefab, angle);
    }

    GameObject GetClosestEnemy(GameObject[] enemies) {
        GameObject gMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject g in enemies) {
            float dist = Vector3.Distance(g.transform.position, currentPos);
            if (dist < minDist) {
                gMin = g;
                minDist = dist;
            }
        }
        return gMin;
    }

    float AngleToClosestEnemy() {
        //Trig to find the angle
        float opp = Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(closest.transform.position.x, 0, 0));
        float adj = Vector3.Distance(new Vector3(transform.position.y, 0, 0), new Vector3(closest.transform.position.y, 0, 0));
        float a = Mathf.Rad2Deg * Mathf.Atan(opp / adj);
        //Adjusts angle to work properly
        if (transform.position.x > closest.transform.position.x && transform.position.y < closest.transform.position.y) {
            a = a + 90f;
        }

        if (transform.position.x < closest.transform.position.x && transform.position.y < closest.transform.position.y) {
            a = 90f - a;
        }

        if (transform.position.x > closest.transform.position.x && transform.position.y > closest.transform.position.y) {
            a = 270f - a;
        }

        if (transform.position.x < closest.transform.position.x && transform.position.y > closest.transform.position.y) {
            a = a + 270f;
        }
        //Dont want things breaking he he
        if (a == float.NaN) {
            a = 0;
        }

        return a;
    }

    void MakeAndAimProjectile(GameObject prefab, float angleToRotateTo) {
        currentProjectile = Instantiate(prefab);
        currentProjectile.transform.position = transform.position;
        currentProjectile.transform.Rotate(new Vector3(0f, 0f, angleToRotateTo));
    }
}
