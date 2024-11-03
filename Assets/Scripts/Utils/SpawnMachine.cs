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
        public List<GameObject> monsters;
        public int maxSpawnArea;
        public float minSpawnScale;
        public float spawnScale;
        private float localTimer;

        private void Update()
        {
            switch (state)
            {
                case SPAWNSTATE.WAITING:
                    break;
                case SPAWNSTATE.WORKING:
                    localTimer += Time.deltaTime;
                    if (localTimer > spawnScale)
                    {
                        localTimer = 0f;
                        RandomSpawnMobs();
                    }
                    break;
            }        
        }

        public void Init()
        {
        }

        void RandomSpawnMobs()
        {
            int index = Random.Range(0, monsters.Count);
            int spawnAreaX = Random.Range(-maxSpawnArea, maxSpawnArea);
            int spawnAreaZ = Random.Range(-maxSpawnArea, maxSpawnArea);
            //GameObject obj = PoolManager.Instance.SpawnMonster(monsters[index]);
            GameObject obj = Instantiate(monsters[index], this.transform);
            obj.transform.position = this.transform.position + new Vector3(spawnAreaX, Vector3.up.y * 1f, spawnAreaZ);
        }
    }
}
