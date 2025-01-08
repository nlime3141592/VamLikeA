using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [CreateAssetMenu(fileName = "New Phase Composite", menuName = "ScriptableObjects/Game Parameter/Phase Composite", order = 2)]
    public class PhaseCompositeSO : PhaseSO
    {
        public List<PhaseSO> phaseSoList;
    }
}