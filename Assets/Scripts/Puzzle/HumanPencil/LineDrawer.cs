using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Puzzle.HumanPencil
{
    public class LineDrawer : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject drawingPoint;
        [SerializeField] 
        private GameObject drawingSurface;
        [SerializeField] 
        private DrawableSurface[] displayMonitors;
        
        private DrawableSurface mainDrawableSurface; 
        private float _maxDrawingDistance = 0.5f;
        private float _drawingSurfaceWidth = 0.2f;

        private const float NewPointDistanceTolerance = 0.001f;
        private Vector3 _lastPoint;
        private bool _isDrawing = false;

        public void Start()
        {
            mainDrawableSurface = drawingSurface.GetComponent<DrawableSurface>();
        }

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
            Vector3 newPoint = CapturePoint();
            mainDrawableSurface.CreateBrush(newPoint);
            AddBrushOnSecondaryDisplays(newPoint);
        }

        private void AddBrushOnSecondaryDisplays(Vector3 newPoint)
        {
            Vector3 referenceTransformToUpperLeftCorner = newPoint - drawingSurface.transform.position;
            foreach (DrawableSurface monitor in displayMonitors)
            {
                monitor.CreateBrushRelativeToSelf(referenceTransformToUpperLeftCorner, drawingSurface.transform.eulerAngles);
            }
        }
        
        private void PlaceNewPoint() 
        {
            Vector3 newPoint = CapturePoint();
            if (IsNewPointDistinctFromLastPoint(newPoint)) 
            {
                mainDrawableSurface.AddAPoint(newPoint);
                PlaceNewPointOnSecondaryDisplays(newPoint);
                _lastPoint = newPoint;
            }
        }

        private void PlaceNewPointOnSecondaryDisplays(Vector3 newPoint)
        {
            Vector3 referenceTransformToUpperLeftCorner = newPoint - drawingSurface.transform.position;
            foreach (DrawableSurface monitor in displayMonitors)
            {
                monitor.AddAPointRelativeToSelf(referenceTransformToUpperLeftCorner, drawingSurface.transform.eulerAngles);
            }
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
            mainDrawableSurface.ClearDrawing();
            foreach (DrawableSurface monitor in displayMonitors)
            {
                monitor.ClearDrawing();
            }
        }
        
    }
}