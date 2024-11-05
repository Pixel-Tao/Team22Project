using UnityEngine;
using System.Collections.Generic;
using Defines;

namespace Assets.Scripts.Utils
{
    internal class SpawnMachine : MonoBehaviour
    {
        private Defines.SPAWNSTATE state = Defines.SPAWNSTATE.WORKING;
        public List<MOBTYPE> monsters;
        public float maxSpawnArea;
        public float minSpawnScale;
        public float currentSpawnScale;
        private float localTimer;

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
