using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace InteractableItems
{
    public class InteractableItem : MonoBehaviour
    {
        // public string Name;
        public string interactPreButtonText;
        public string interactPostButtonText;
        public string interactButtonName = "B";
        public string interactionStopPostButtonText;
        public float interactRadius = 3f;

        protected bool IsInteractedWith = false;
        protected bool HasPlayerInRange = false;
        protected string ToStartInteractText;
        protected string ToEndInteractText;
        protected TextRenderer TextRenderer;

        private GameObject _inRangePlayer;

        private bool _previousPlayerRangeState = false;
        private bool _playerHasEnteredRange = false;
        private bool _playerHasLeftRange = false;

        protected ControllerManager ControllerManager;
        protected ButtonLayout buttonLayout;



        private GameObject[] _players;

        protected void Start()
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
            ControllerManager = GetComponent<ControllerManager>();
            buttonLayout = ControllerManager.CurrentButtonLayout;
            ToStartInteractText = String.Join(" ", interactPreButtonText, buttonLayout.actionRight, interactPostButtonText);
            ToEndInteractText = String.Join(" ", interactPreButtonText, buttonLayout.actionRight, interactionStopPostButtonText);
        }

        private void Update()
        {

        }

        protected void CheckIfAPlayerIsInRange()
        {
            _previousPlayerRangeState = HasPlayerInRange;
            foreach (GameObject player in _players)
            {
                if(player!= null)
                {
                    float distance = Vector3.Distance(player.transform.position, transform.position);
                    if (distance <= interactRadius)
                    {
                        HasPlayerInRange = true;
                        _inRangePlayer = player;
                        UpdatePlayerRangeState();
                        return;
                    }
                }
                
            }
            HasPlayerInRange = false;
            UpdatePlayerRangeState();
        }



        private void UpdatePlayerRangeState()
        {
            if (_previousPlayerRangeState == HasPlayerInRange)
            {
                _playerHasEnteredRange = false;
                _playerHasLeftRange = false;
            }

            if (!_previousPlayerRangeState && HasPlayerInRange)
            {
                _playerHasEnteredRange = true;
                _playerHasLeftRange = false;
            }
            if (_previousPlayerRangeState && !HasPlayerInRange)
            {
                _playerHasEnteredRange = false;
                _playerHasLeftRange = true;
            }
        }

        protected GameObject GetInRangePlayer()
        {
            // if (inRangePlayer == null)
            // {
            //     throw new NullReferenceException();
            // }
            return _inRangePlayer;
        }


        public virtual void OnInteractStart()
        {

        }

        public virtual void OnInteractEnd()
        {

        }

        public virtual void OnPlayerEnterRange()
        {

        }

        public virtual void OnPlayerInRange()
        {

        }

        public virtual void OnPlayerExitRange()
        {

        }

        protected bool hasPlayerEnteredRange()
        {
            return _playerHasEnteredRange;
        }

        protected bool hasPlayerLeftRange()
        {
            return _playerHasLeftRange;
        }

        protected void FindTextRendererOfPlayerInRange()
        {
            TextRenderer = GetInRangePlayer().GetComponentInChildren<TextRenderer>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }

    }
}
