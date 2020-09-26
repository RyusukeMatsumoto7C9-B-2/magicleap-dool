using UnityEngine;
using UnityEngine.AI;

namespace MagicLeapDool
{
    /// <summary>
    /// 指定した場所にNavMeshAgentを移動させるためのスクリプト.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent), typeof(LineRenderer))]
    public class MoveToClickPoint : MonoBehaviour
    {
        NavMeshAgent agent;

        // パス.
        NavMeshPath path = null;
        
        // 座標リスト.
        Vector3[] positions = new Vector3[9];

        // ルート描画用Renderer.    
        LineRenderer lr;


        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            lr = GetComponent<LineRenderer>();
            lr.startWidth = 0.2f;
            lr.endWidth = 0.2f;
            lr.enabled = false;
        }


        /// <summary>
        /// Agentの座標を直接設定する.
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(
            Vector3 position)
        {
            agent.enabled = false;
            transform.position = position;
            agent.enabled = true;
        }
    

        /// <summary>
        /// 次の移動先を設定する.
        /// </summary>
        /// <param name="position"></param>
        public void OnInputClicked(
            Vector3 position)
        {
            lr.enabled = true;

            // 目的地の設定.
            agent.destination = position;

            // パスの計算.
            path = new NavMeshPath();
            NavMesh.CalculatePath(agent.transform.position, agent.destination, NavMesh.AllAreas, path);
            positions = path.corners;

            // ルートの描画.
            lr.widthMultiplier = 0.2f;
            lr.positionCount = positions.Length;

            for (int i = 0; i < positions.Length; i++) 
            {
                Debug.Log("point "+i+"="+ positions[i]);
                lr.SetPosition(i, positions[i]);
            }
        }

    }

}