#if UNITY_EDITOR
using UnityEngine;
using Util;

namespace Flow
{
    [CreateAssetMenu(menuName = "Custom/Data/StageOpensGenerator")]
    public class StageOpensGenerator : UniqueScriptableObject<StageOpensGenerator>
    {
        [SerializeField] MatrixBool mStageOpens;
        [SerializeField] MatrixString mStages;

        [ContextMenu("GenerateData")]
        public void GenerateData()
        {
            Debug.Assert(mStageOpens != null);
            Debug.Assert(mStages != null);

            mStageOpens.Matrix.Clear();
            for (var i = 0; i < mStages.Matrix.Count; i++)
            {
                if (mStages.Matrix[i].List.Count == 0)
                {
                    continue;
                }

                mStageOpens.Matrix.Add(new CustomList<bool>());
                for (var j = 0; j < mStages.Matrix[j].List.Count; j++)
                {
                    mStageOpens.Matrix[i].List.Add(false);
                }
            }
        }
    }
}
#endif