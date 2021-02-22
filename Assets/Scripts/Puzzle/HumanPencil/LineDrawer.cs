using System;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Puzzle.HumanPencil
{
    public class LineDrawer : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject brush;
        [SerializeField]
        private GameObject drawingTarget;

        private const float Tolerance = 0.001f;
        private LineRenderer _currentLineRenderer;
        private Vector3 _lastPoint;
        private bool _isDrawing = false;
        // private bool _canDraw = false;
        

        public void Draw()
        {
            if (_isDrawing)
            {
                PlaceNewPoint();
            }
            else
            {
                CreateBrush();
                _isDrawing = true;
            }
        }

        public void StopDrawing()
        {
            _isDrawing = false;
        }

        private void CreateBrush() 
        {
            GameObject brushInstance = Instantiate(brush);
            _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
            
            Vector3 newPoint = CapturePoint();

            _currentLineRenderer.SetPosition(0, newPoint);
            _currentLineRenderer.SetPosition(1, newPoint);
        }


        private void AddAPoint(Vector3 pointPos) 
        {
            var positionCount = _currentLineRenderer.positionCount;
            
            positionCount++;
            _currentLineRenderer.positionCount = positionCount;
            int positionIndex = positionCount - 1;
            _currentLineRenderer.SetPosition(positionIndex, pointPos);
            Debug.Log("Adding point " + pointPos);
        }

        private void PlaceNewPoint() 
        {
            Vector3 newPoint = CapturePoint();
            if (IsNewPointDistinctFromLastPoint(newPoint)) 
            {
                AddAPoint(newPoint);
                _lastPoint = newPoint;
            }
        }

        private bool IsNewPointDistinctFromLastPoint(Vector3 newPoint)
        {
            float distance = Vector3.Distance(newPoint, _lastPoint);
            return (distance > Tolerance);
        }

        private Vector3 CapturePoint()
        {
            return drawingTarget.transform.position;
        }

        // public bool IsAllowedToDraw()
        // {
        //     return _canDraw;
        // }
        //
        // public void ToggleDrawing()
        // {
        //     _canDraw = !_canDraw;
        // }
        //
        // public void AllowDrawing()
        // {
        //     _canDraw = true;
        // }
        //
        // public void PreventDrawing()
        // {
        //     _canDraw = false;
        // }
        //
    }
}