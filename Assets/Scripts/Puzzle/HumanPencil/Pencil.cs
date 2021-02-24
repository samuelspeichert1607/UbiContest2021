using System;
using InteractableItems;
using UnityEditor;
using UnityEngine;

namespace Puzzle.HumanPencil
{
    public class Pencil : InteractableItem
    {
        public string drawingPreText;
        public string drawingPostText;
        
        [SerializeField] private string drawingAxisName;
        
        private string _toDrawText;
        private Transform _initialParent;
        private LineDrawer _lineDrawer;

        
        // Start is called before the first frame update
        private new void Start()
        {
            base.Start();
            _toDrawText = String.Join(" ", drawingPreText , drawingAxisName, drawingPostText);
            _lineDrawer = GetComponent<LineDrawer>();
            _initialParent = this.gameObject.transform.parent;
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
                if (TextRenderer.IsClosed())
                {
                    TextRenderer.ShowInfoText( _toDrawText + "\n" + ToEndInteractText);
                }
                if (Input.GetAxis(drawingAxisName) > 0)
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
            if (Input.GetButtonDown(interactButtonName))
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

        private void AppendSelfToPlayer()
        {
            this.gameObject.transform.SetParent(GetInRangePlayer().transform);
        }

        private void DetachSelfFromPlayer()
        {
            this.gameObject.transform.SetParent(_initialParent);
        }
        
    }
}
