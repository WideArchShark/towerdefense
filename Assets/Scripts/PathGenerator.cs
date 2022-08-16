using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator
{
    private int width, height;
    public List<Vector2Int> pathCells;
    public List<Vector2Int> pathRoute;

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void GeneratePath()
    {
        pathCells = new List<Vector2Int>();
        int y = (int)(height / 2);
        int x = 0;

        while (x < width)
        {
            pathCells.Add(new Vector2Int(x, y));

            bool validMove = false;

            while (!validMove)
            {
                int move = Random.Range(0, 3);

                if (move == 0 || x % 2 == 0 || x > (width -2))
                {
                    x++;
                    validMove = true;
                }
                else if (move == 1 && CellIsEmpty(x, y + 1) && y < (height - 2))
                {
                    y++;
                    validMove = true;
                }
                else if (move == 2 && CellIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }
    }

    public void GenerateRoute()
    {
        Vector2Int direction = Vector2Int.right;
        pathRoute = new List<Vector2Int>();
        Vector2Int currentCell = pathCells[0];

        while (currentCell.x < width)
        {
            pathRoute.Add(new Vector2Int(currentCell.x, currentCell.y));

            if (CellIsTaken(currentCell + direction))
            {
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.up) && direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.down) && direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.right) && direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.left) && direction != Vector2Int.right)
            {
                direction = Vector2Int.left;
                currentCell = currentCell + direction;
            }
            else
            {
                // I think if we hit this... We're done. Just return
                return;
            }
        }
    }

    public bool GenerateCrossroads()
    {
        for (int i = 0; i < pathCells.Count; i++)
        {
            Vector2Int pathCell = pathCells[i];

            if (pathCell.x > 3 && pathCell.x < width - 4 && pathCell.y > 2 && pathCell.y < height - 3)
            {
                if (CellIsEmpty(pathCell.x, pathCell.y + 3) && CellIsEmpty(pathCell.x + 1, pathCell.y + 3) && CellIsEmpty(pathCell.x + 2, pathCell.y + 3)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y + 2) && CellIsEmpty(pathCell.x, pathCell.y + 2) && CellIsEmpty(pathCell.x + 1, pathCell.y + 2) && CellIsEmpty(pathCell.x + 2, pathCell.y + 2) && CellIsEmpty(pathCell.x + 3, pathCell.y + 2)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y + 1) && CellIsEmpty(pathCell.x, pathCell.y + 1) && CellIsEmpty(pathCell.x + 1, pathCell.y + 1) && CellIsEmpty(pathCell.x + 2, pathCell.y + 1) && CellIsEmpty(pathCell.x + 3, pathCell.y + 1)
                    && CellIsEmpty(pathCell.x + 1, pathCell.y) && CellIsEmpty(pathCell.x + 2, pathCell.y) && CellIsEmpty(pathCell.x + 3, pathCell.y)
                    && CellIsEmpty(pathCell.x + 1, pathCell.y - 1) && CellIsEmpty(pathCell.x + 2, pathCell.y - 1))

                {
                    pathCells.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathCell.x + 1, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y + 1), new Vector2Int(pathCell.x + 2, pathCell.y + 2), new Vector2Int(pathCell.x + 1, pathCell.y + 2), new Vector2Int(pathCell.x, pathCell.y + 2), new Vector2Int(pathCell.x, pathCell.y + 1) });
                    return true;
                }

                if (CellIsEmpty(pathCell.x + 1, pathCell.y + 1) && CellIsEmpty(pathCell.x + 2, pathCell.y + 1)
                    && CellIsEmpty(pathCell.x + 1, pathCell.y) && CellIsEmpty(pathCell.x + 2, pathCell.y) && CellIsEmpty(pathCell.x + 3, pathCell.y)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y - 1) && CellIsEmpty(pathCell.x, pathCell.y - 1) && CellIsEmpty(pathCell.x + 1, pathCell.y - 1) && CellIsEmpty(pathCell.x + 2, pathCell.y - 1) && CellIsEmpty(pathCell.x + 3, pathCell.y - 1)
                    && CellIsEmpty(pathCell.x - 1, pathCell.y - 2) && CellIsEmpty(pathCell.x, pathCell.y - 2) && CellIsEmpty(pathCell.x + 1, pathCell.y - 2) && CellIsEmpty(pathCell.x + 2, pathCell.y - 2) && CellIsEmpty(pathCell.x + 3, pathCell.y - 2)
                    && CellIsEmpty(pathCell.x, pathCell.y - 3) && CellIsEmpty(pathCell.x + 1, pathCell.y - 3) && CellIsEmpty(pathCell.x + 2, pathCell.y - 3))
                {
                    pathCells.InsertRange(i + 1, new List<Vector2Int> { new Vector2Int(pathCell.x + 1, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y), new Vector2Int(pathCell.x + 2, pathCell.y - 1), new Vector2Int(pathCell.x + 2, pathCell.y - 2), new Vector2Int(pathCell.x + 1, pathCell.y - 2), new Vector2Int(pathCell.x, pathCell.y - 2), new Vector2Int(pathCell.x, pathCell.y - 1) });
                    return true;
                }
            }
        }

        return false;
    }

    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(Vector2Int cell)
    {
        return pathCells.Contains(cell);
    }

    public int getCellNeighbourValue(int x, int y)
    {
        int returnValue = 0;

        if (CellIsTaken(x, y - 1))
        {
            returnValue += 1;
        }

        if (CellIsTaken(x - 1, y))
        {
            returnValue += 2;
        }

        if (CellIsTaken(x + 1, y))
        {
            returnValue += 4;
        }

        if (CellIsTaken(x, y + 1))
        {
            returnValue += 8;
        }

        return returnValue;
    }

}
