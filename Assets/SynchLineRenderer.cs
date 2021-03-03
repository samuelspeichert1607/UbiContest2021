using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class SynchLineRenderer : MonoBehaviourPun//, IPunObservable
{
    //https://www.youtube.com/watch?v=MUKz8ZX69xI&ab_channel=FirstGearGames
    //Pour passer avec les RPC et les events.
    private LineRenderer lineRenderer;
    Vector3[] newPos;
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        newPos = new Vector3[2];
    }
    
    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("Is Writing");
            stream.SendNext(lineRenderer.GetPositions(newPos));
        }
        else if (stream.IsReading)
        {
            Debug.Log("Is Reading");
            lineRenderer.SetPositions((Vector3[])stream.ReceiveNext());
        }
    }*/
}
