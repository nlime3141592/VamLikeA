using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [CreateAssetMenu(fileName = "New Boss Phase", menuName = "ScriptableObjects/Game Parameter/Boss Phase", order = 2)]
    public class BossPhaseSO : PhaseSO
    {
        public SpawnerSO bossSpawnerSO;
        public List<SpawnerSO> miscellaneousSpawnerSoList;

        public bool useTimerStop = true;
    }
}