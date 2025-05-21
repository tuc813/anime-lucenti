using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    public Transform player;
    public float lockOnRange = 15f;
    public KeyCode lockOnKey = KeyCode.Tab;

    private Transform currentTarget;
    private bool isLockedOn = false;

    void Update()
    {
        if (Input.GetKeyDown(lockOnKey))
        {
            if (isLockedOn)
            {
                isLockedOn = false;
                currentTarget = null;
            }
            else
            {
                currentTarget = FindClosestEnemy();
                if (currentTarget != null) isLockedOn = true;
            }
        }

        if (isLockedOn && currentTarget != null)
        {
            Vector3 dir = currentTarget.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

            // Ruota anche il player verso il nemico
            Vector3 lookDir = currentTarget.position - player.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
            {
                Quaternion playerRot = Quaternion.LookRotation(lookDir);
                player.rotation = Quaternion.Slerp(player.rotation, playerRot, Time.deltaTime * 5f);
            }
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(player.position, enemy.transform.position);
            if (dist < minDist && dist <= lockOnRange)
            {
                minDist = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
