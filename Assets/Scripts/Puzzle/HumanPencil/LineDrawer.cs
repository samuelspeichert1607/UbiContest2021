using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Puzzle.HumanPencil
{
    public class LineDrawer : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject brush;
        [SerializeField]
        private GameObject drawingPoint;
        [SerializeField] 
        private GameObject drawingSurface;
        
        private float _maxDrawingDistance = 0.5f;
        private float _drawingSurfaceWidth = 0.2f;

        private const float NewPointDistanceTolerance = 0.001f;
        private LineRenderer _currentLineRenderer;
        private Vector3 _lastPoint;
        private bool _isDrawing = false;

        private List<GameObject> brushes = new List<GameObject>();
        // private bool _canDraw = false;
        

        public void Draw()
        {
            if (IsAllowedToDraw())
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
        }

        public void StopDrawing()
        {
            _isDrawing = false;
        }

        private void CreateBrush() 
        {
            GameObject brushInstance = Instantiate(brush);
            brushes.Add(brushInstance);
            _currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
            
            Vector3 newPoint = CapturePoint();

            _currentLineRenderer.SetPosition(0, newPoint);
            _currentLineRenderer.SetPosition(1, newPoint);
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

        private void AddAPoint(Vector3 pointPos) 
        {
            var positionCount = _currentLineRenderer.positionCount;
            
            positionCount++;
            _currentLineRenderer.positionCount = positionCount;
            int positionIndex = positionCount - 1;
            _currentLineRenderer.SetPosition(positionIndex, pointPos);
        }


        private bool IsNewPointDistinctFromLastPoint(Vector3 newPoint)
        {
            float distance = Vector3.Distance(newPoint, _lastPoint);
            return (distance > NewPointDistanceTolerance);
        }

        private Vector3 CapturePoint()
        {
            Vector3 newPoint = drawingPoint.transform.position; 
            newPoint.y = drawingSurface.transform.position.y + _drawingSurfaceWidth;
            return newPoint;
        }

        private bool IsAllowedToDraw()
        {
            return IsDrawingPointOverDrawingSurface();
        }

        private bool IsDrawingPointOverDrawingSurface()
        {
            RaycastHit hit;
            if (Physics.Raycast(drawingPoint.transform.position, Vector3.down, out hit, _maxDrawingDistance))
            {
                if (hit.collider.name == drawingSurface.name)
                {
                    return true;
                }
            }

            return false;
        }

        public void ClearAllDrawing()
        {
            foreach (GameObject brush in brushes)
            {
                Destroy(brush);
            }
        }

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
        
    }
}