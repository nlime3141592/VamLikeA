using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [CreateAssetMenu(fileName = "New Survival Phase", menuName = "ScriptableObjects/Game Parameter/Survival Phase", order = 2)]
    public class SurvivalPhaseSO : PhaseSO
    {
        public List<SpawnerSO> spawnerSoList;
        public float phaseDuration = 10.0f;
    }
}