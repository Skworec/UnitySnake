using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private GameObject snakeBodyTile;
    [SerializeField] private ushort startLenght = 4;
    [SerializeField] private RaycastController rycstCntrl;


    private List<GameObject> snakeBody;
    private Direction direction;
    private const float tileSize = 0.4375f;

    #region Movement
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public void ChangeDirection(Direction dir)
    {
        lock (snakeBody)
        {
            switch (dir)
            {
                case Direction.Up:
                    {
                        if ((snakeBody[0].transform.position - snakeBody[1].transform.position).y != -0.4375f)
                        {
                            direction = dir;
                        }
                        break;
                    }
                case Direction.Down:
                    {
                        if ((snakeBody[0].transform.position - snakeBody[1].transform.position).y != 0.4375f)
                        {
                            direction = dir;
                        }
                        break;
                    }
                case Direction.Left:
                    {
                        if ((snakeBody[0].transform.position - snakeBody[1].transform.position).x != 0.4375f)
                        {
                            direction = dir;
                        }
                        break;
                    }
                case Direction.Right:
                    {
                        if ((snakeBody[0].transform.position - snakeBody[1].transform.position).x != -0.4375f)
                        {
                            direction = dir;
                        }
                        break;
                    }
            }
        }
    }
    public void StartMove()
    {
        StartCoroutine("Move");
    }
    public void StopMove()
    {
        StopCoroutine("Move");
    }
    private IEnumerator Move()
    {
        while (true)
        {
            Vector3 newPos = new Vector3();
            switch (direction)
            {
                case Direction.Up:
                    {
                        newPos = Vector3.up * tileSize;
                        break;
                    }
                case Direction.Down:
                    {
                        newPos = Vector3.down * tileSize;
                        break;
                    }
                case Direction.Left:
                    {
                        newPos = Vector3.left * tileSize;
                        break;
                    }
                case Direction.Right:
                    {
                        newPos = Vector3.right * tileSize;
                        break;
                    }
            }
            rycstCntrl.Ray(snakeBody[0].transform.position + newPos, snakeBody[0].tag);
            yield return new WaitForSeconds(0.00001f);
            snakeBody[0].transform.position += newPos;

            GameObject temp = snakeBody[snakeBody.Count - 1];
            snakeBody.RemoveAt(snakeBody.Count - 1);
            snakeBody.Insert(1, temp);
            snakeBody[1].transform.position = snakeBody[0].transform.position - newPos;
            yield return new WaitForSeconds(1 / speed - 0.001f);
        }
    }
    public void AddLength()
    {
        snakeBody.Add(Instantiate(snakeBodyTile, snakeBody[snakeBody.Count - 1].transform.position, Quaternion.identity, gameObject.transform));
        snakeBody[snakeBody.Count - 1].name = snakeBody.Count.ToString();
    }
    #endregion Movement

    public void OnDeathHandler()
    {
        //Отключить рэйкаст контроллер
        StopMove();
    }

    public void OnEatHandler()
    {
        AddLength();
    }

    private void Start()
    {
        DataController.instance.onDeath.AddListener(OnDeathHandler);
        DataController.instance.onEat.AddListener(OnEatHandler);
        rycstCntrl.IsActive = true;
        InitialManager();
    }

    public void InitialManager()
    {
        StopAllCoroutines();
        if (snakeBody != null)
            snakeBody.ForEach(x => Destroy(x));
        snakeBody = new List<GameObject>();
        for (int i = 0; i < startLenght; i++)
        {
            snakeBody.Add(Instantiate(snakeBodyTile, new Vector3(0, 0.4375f * i, 0), Quaternion.identity, gameObject.transform));
            snakeBody[snakeBody.Count - 1].name = snakeBody.Count.ToString();
        }
        direction = Direction.Down;
        StartMove();
    }


}
