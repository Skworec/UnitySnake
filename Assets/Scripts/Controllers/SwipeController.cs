using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private static Vector3 startCoordinates;
    private static bool swiping = false;
    [SerializeField] GameObject Snake;
    private SnakeController snkContrl;

    private void Awake()
    {
        snkContrl = Snake.GetComponent<SnakeController>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !swiping)
        {
            startCoordinates = Input.mousePosition;
            swiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && swiping)
        {
            Vector2 diraction = Input.mousePosition - startCoordinates;
            swiping = false;
            if (Snake.GetComponent<RaycastController>().IsActive)
            {
                switch (Mathf.Abs(diraction.x) > Mathf.Abs(diraction.y))
                {
                    case true:
                        {
                            if (diraction.x > 0)
                            {
                                snkContrl.ChangeDirection(SnakeController.Direction.Right);
                            }
                            else
                            {
                                snkContrl.ChangeDirection(SnakeController.Direction.Left);
                            }
                            break;
                        }
                    case false:
                        {
                            if (diraction.y > 0)
                            {
                                snkContrl.ChangeDirection(SnakeController.Direction.Up);
                            }
                            else
                            {
                                snkContrl.ChangeDirection(SnakeController.Direction.Down);
                            }
                            break;
                        }
                }
            }
        }
    }
}
