using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
     WaveConfig waveConfig;
     List<Transform> WayPoints;   
    int WayPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        WayPoints = waveConfig.GetWayPoints(); //waveConfig.GetWayPoints();
        transform.position = WayPoints[WayPointIndex].transform.position;

    }
    void Update()
    {
        Move();

    }
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    public void Move()
    {
        if (WayPointIndex <= WayPoints.Count - 1)
        {
            var TargetPosition = WayPoints[WayPointIndex].transform.position;
            var MovementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MovementThisFrame);

            if (transform.position == TargetPosition)
            {
                WayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
