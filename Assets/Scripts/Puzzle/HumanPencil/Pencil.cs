using System;
using InteractableItems;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

namespace Puzzle.HumanPencil
{
    public class Pencil : InteractableItem
    {
        public string drawingPreText;
        public string drawingPostText;
        
        [SerializeField] private string drawingAxisName;
        private float distancePlayerPencil = 1;

        private ControllerManager controllerManager;


        private string _toDrawText;
        private Transform _initialParent;
        private LineDrawer _lineDrawer;
        private PhotonView photonView;


        // Start is called before the first frame update
        private new void Start()
        {
            base.Start();
            photonView = PhotonView.Get(this);
            _toDrawText = String.Join(" ", drawingPreText , drawingAxisName, drawingPostText);
            _lineDrawer = GetComponent<LineDrawer>();
            _initialParent = this.gameObject.transform.parent;
            controllerManager = GetComponent<ControllerManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
            CheckIfAPlayerIsInRange();
            if (hasPlayerEnteredRange())
            {
                OnPlayerEnterRange();
            }
            else if (hasPlayerLeftRange())
            {
                OnPlayerExitRange();
            }
            else if (HasPlayerInRange)
            {
                OnPlayerInRange();
            }

            if (IsInteractedWith)
            {
                if (TextRenderer != null &&TextRenderer.IsClosed())
                {
                    TextRenderer.ShowInfoText( _toDrawText + "\n" + ToEndInteractText);
                }
                if (controllerManager.GetAxis(drawingAxisName) > 0)
                {
                    _lineDrawer.Draw();
                }
                else
                {
                   _lineDrawer.StopDrawing(); 
                }
            }

        }
        public override void OnPlayerInRange()
        {
            if (controllerManager.GetButtonDown(interactButtonName))
            {
                if (IsInteractedWith)
                {
                    OnInteractEnd();
                }
                else
                {
                    OnInteractStart();
                }
            }
        }
        
        public override void OnInteractStart()
        {
            IsInteractedWith = true;
            TextRenderer.ShowInfoText( _toDrawText + "\n" + ToEndInteractText);
            AppendSelfToPlayer();
        }

        


        public override void OnInteractEnd()
        {
            IsInteractedWith = false;
            TextRenderer.ShowInfoText(ToStartInteractText);
            DetachSelfFromPlayer();
        }
        
        public override void OnPlayerEnterRange()
        {
            FindTextRendererOfPlayerInRange();
            TextRenderer.ShowInfoText(ToStartInteractText);   
        }
        
        public override void OnPlayerExitRange()
        {
            TextRenderer.CloseInfoText();
        }

        // Possibilit� de append ca avec RPC
        private void AppendSelfToPlayer()
        {
            photonView.RequestOwnership();
            Transform player = GetInRangePlayer().transform;
            this.gameObject.transform.SetParent(player);
            transform.position = player.position + player.forward  + new Vector3(0, player.gameObject.GetComponent<BoxCollider>().bounds.size.y/2, 0);
            transform.rotation = Quaternion.identity;
        }

        private void DetachSelfFromPlayer()
        {
            this.gameObject.transform.SetParent(_initialParent);
        }
        
    }
}
