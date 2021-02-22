using System.Collections;
using System.Collections.Generic;
using Puzzle.HumanPencil;
using UnityEngine;

public class HumanPencilManager : MonoBehaviour
{
    [SerializeField]
    private PressableButton resetPuzzleButton;

    [SerializeField] 
    private GameObject pencil;

    // [SerializeField] private GameObject[] displayMonitors;
    
    private Vector3 pencilInitialPosition;
    private LineDrawer _lineDrawer;
    private Pencil _pencil;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineDrawer = pencil.GetComponent<LineDrawer>();
        _pencil = pencil.GetComponent<Pencil>();
        pencilInitialPosition = pencil.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (resetPuzzleButton.IsPressed())
        {
            ResetPuzzle();
        }
    }
    

    private void ResetPuzzle()
    {
        _lineDrawer.ClearAllDrawing();
        _pencil.transform.position = pencilInitialPosition;
    }
}
