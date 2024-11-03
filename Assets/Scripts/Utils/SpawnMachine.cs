using UnityEngine;
using System.Collections.Generic;

public enum SPAWNSTATE
{
    WAITING,
    WORKING
}

public enum MOBTYPE
{
    WARRIOR,
    ROUGUE,
    MAGE,
    END,
}

namespace Assets.Scripts.Utils
{
    internal class SpawnMachine : MonoBehaviour
    {
        private SPAWNSTATE state = SPAWNSTATE.WORKING;
        public List<string> monsters;
        public float maxSpawnArea;
        public float minSpawnScale;
        public float currentSpawnScale;
        private float localTimer;

        private void Update()
        {
            switch (state)
            {
                case SPAWNSTATE.WAITING:
                    break;
                case SPAWNSTATE.WORKING:
                    localTimer += Time.deltaTime;
                    if (localTimer > currentSpawnScale)
                    {
                        localTimer = 0f;
                        RandomSpawnMobs();
                    }
                    break;
            }        
        }

        void RandomSpawnMobs()
        {
            int index = Random.Range(0, monsters.Count);
            float spawnAreaX = Random.Range(-maxSpawnArea, maxSpawnArea);
            float spawnAreaZ = Random.Range(-maxSpawnArea, maxSpawnArea);
            
            GameObject obj = PoolManager.Instance.SpawnMonster(monsters[index]);
            obj.transform.position = this.transform.position + new Vector3(spawnAreaX, Vector3.up.y * 1f, spawnAreaZ);
        }
    }
}
