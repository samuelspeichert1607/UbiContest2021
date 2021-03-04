using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class DrawableSurface : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject brush;
    
    private List<GameObject> brushes = new List<GameObject>();
    private LineRenderer _currentLineRenderer;
    private PhotonView photonView;

    public void CreateBrush(Vector3 referencePoint)
    {
        GameObject brushInstance = PhotonNetwork.Instantiate("Brush",brush.transform.position, brush.transform.rotation);
        _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();


        _currentLineRenderer.SetPosition(0, referencePoint);
        _currentLineRenderer.SetPosition(1, referencePoint);

        brushes.Add(brushInstance);
        photonView = PhotonView.Get(this);

        photonView.RPC("CreateRemoteBrush", RpcTarget.Others, referencePoint);
    }

    public void AddAPoint(Vector3 pointPos)
    {
        photonView = PhotonView.Get(this);
        var positionCount = _currentLineRenderer.positionCount;

        positionCount++;
        _currentLineRenderer.positionCount = positionCount;
        int positionIndex = positionCount - 1;
        _currentLineRenderer.SetPosition(positionIndex, pointPos);
        photonView.RPC("AddRemotePoint", RpcTarget.Others, pointPos);
    }

    public void ClearDrawing()
    {
        photonView = PhotonView.Get(this);
        foreach (GameObject brush in brushes)
        {
            PhotonNetwork.Destroy(brush);
        }
        photonView.RPC("EraseRemote", RpcTarget.Others);
    }

    public void CreateBrushRelativeToSelf(Vector3 referenceTransformToUpperLeftCorner, Vector3 referenceSurfaceEulerAngles)
    {
        Vector3 adjustedPoint =
            AdjustRelativeTransformToSelf(referenceTransformToUpperLeftCorner, referenceSurfaceEulerAngles);
        CreateBrush(adjustedPoint);
    }
    
    public void AddAPointRelativeToSelf(Vector3 referenceTransformToUpperLeftCorner, Vector3 referenceSurfaceEulerAngles)
    {
        Vector3 adjustedPoint =
            AdjustRelativeTransformToSelf(referenceTransformToUpperLeftCorner, referenceSurfaceEulerAngles);
        AddAPoint(adjustedPoint);
    }

    private Vector3 AdjustRelativeTransformToSelf(Vector3 referenceTransformToUpperLeftCorner, Vector3 referenceSurfaceEulerAngles)
    {
        Vector3 monitorULCorner = transform.position;
        Vector3 angles = transform.eulerAngles - referenceSurfaceEulerAngles;
        
        // angles.y = 270;
        // angles.z = -180;
        // angles.x = -90;
        // Vector3 adjustedTransform = RotatePointAroundOrigin(referenceTransformToUpperLeftCorner, angles);
        //TODO this is an ugly abomination but I don't know how to fix the rotation correctly. 
        Vector3 adjustedTransform = new Vector3(referenceTransformToUpperLeftCorner.y, -referenceTransformToUpperLeftCorner.x,
            referenceTransformToUpperLeftCorner.z);
        return monitorULCorner + adjustedTransform;
    }
    
    private Vector3 RotatePointAroundOrigin(Vector3 point, Vector3 angles)
    {
        return Quaternion.Euler(angles) * point;
    }

    [PunRPC]
    void CreateRemoteBrush(Vector3 referencePoint)
    {
        GameObject brushInstance = PhotonNetwork.Instantiate("Brush", brush.transform.position, brush.transform.rotation);
        _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        _currentLineRenderer.SetPosition(0, referencePoint);
        _currentLineRenderer.SetPosition(1, referencePoint);

        brushes.Add(brush);
    }

    [PunRPC]
    void EraseRemote()
    {
        ClearDrawing();
    }

    [PunRPC]
    void AddRemotePoint(Vector3 pointPos)
    {
        var positionCount = _currentLineRenderer.positionCount;

        positionCount++;
        _currentLineRenderer.positionCount = positionCount;
        int positionIndex = positionCount - 1;
        _currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }*/
}
