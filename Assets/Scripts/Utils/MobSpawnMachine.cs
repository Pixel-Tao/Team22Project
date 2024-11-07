using UnityEngine;
using System.Collections.Generic;
using Defines;

public class MobSpawnMachine : MonoBehaviour
{
    private Defines.SPAWNSTATE state = Defines.SPAWNSTATE.WORKING;
    [SerializeField] private List<MOBTYPE> monsters;
    [SerializeField] private float maxSpawnArea;
    [SerializeField] private float minSpawnScale;
    [SerializeField] private float currentSpawnScale;
    private float localTimer;

    //PP
    public float SpawnScale { get { return currentSpawnScale; } set { currentSpawnScale = value; } }

    private void Start()
    {
        GameManager.Instance.AddMachine(this);
    }

    private void Update()
    {
        switch (state)
        {
            case Defines.SPAWNSTATE.WAITING:
                break;
            case Defines.SPAWNSTATE.WORKING:
                localTimer += Time.deltaTime;
                if (localTimer > currentSpawnScale)
                {
                    localTimer = 0f;
                    RandomSpawnMobs();
                }
                break;
        }
    }

    private void RandomSpawnMobs()
    {
        int index = Random.Range(0, monsters.Count);
        float spawnAreaX = Random.Range(-maxSpawnArea, maxSpawnArea);
        float spawnAreaZ = Random.Range(-maxSpawnArea, maxSpawnArea);

        GameObject obj = PoolManager.Instance.SpawnMonster(monsters[index]);
        obj.transform.position = this.transform.position + new Vector3(spawnAreaX, Vector3.up.y * 1f, spawnAreaZ);
    }
    public void TriggerMachineState()
    {
        state = (state == SPAWNSTATE.WAITING ? SPAWNSTATE.WORKING : SPAWNSTATE.WAITING);
    }
    public void TriggerMachineState(SPAWNSTATE val)
    {
        state = val;
    }

    public void IncreaseScale() { currentSpawnScale++; }
    public void DecreaseScale()
    {
        currentSpawnScale = Mathf.Max(--currentSpawnScale, minSpawnScale);
    }

}

