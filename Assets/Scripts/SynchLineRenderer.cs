using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class SynchLineRenderer : MonoBehaviourPun
{
    //https://www.youtube.com/watch?v=MUKz8ZX69xI&ab_channel=FirstGearGames
    //Pour passer avec les RPC et les events.

    private LineRenderer lineRenderer;
    private Vector3[] newPos;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        newPos = new Vector3[2];
    }
}
