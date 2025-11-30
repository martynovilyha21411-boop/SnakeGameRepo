using SnakeGameRepo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGameRepo
{ 
public class GameState
{
    public Snake Snake;
    public List<Food> Foods;
    public List<Obstacle> Obstacles;

    public bool GameOver;
    private GameSettings _settings;
    private Random _rnd;

    public GameState(GameSettings settings)
    {
        _settings = settings;
        _rnd = new Random();

        Snake = new Snake(new Position(_settings.Width / 2, _settings.Height / 2));
        Foods = new List<Food>();
        Obstacles = new List<Obstacle>();

        SpawnFood();
    }

    private bool IsOccupied(int x, int y)
    {
        foreach (var s in Snake.Segments)
            if (s.Position.X == x && s.Position.Y == y) return true;

        foreach (var f in Foods)
            if (f.Position.X == x && f.Position.Y == y) return true;

        foreach (var o in Obstacles)
            if (o.Position.X == x && o.Position.Y == y) return true;

        return false;
    }

    private void SpawnFood()
    {
        int x, y;
        do
        {
            x = _rnd.Next(0, _settings.Width);
            y = _rnd.Next(0, _settings.Height);
        }
        while (IsOccupied(x, y));

        Foods.Add(new Food(new Position(x, y)));
    }

    public void Update()
    {
        if (GameOver) return;

        Position next = Snake.GetNextHeadPosition();

        // стенки
        if (next.X < 0 || next.X >= _settings.Width ||
            next.Y < 0 || next.Y >= _settings.Height)
        {
            GameOver = true;
            return;
        }

        // самопересечение
        for (int i = 0; i < Snake.Segments.Count; i++)
        {
            if (Snake.Segments[i].Position.X == next.X &&
                Snake.Segments[i].Position.Y == next.Y)
            {
                GameOver = true;
                return;
            }
        }

        bool grow = false;

        // еда
        for (int i = 0; i < Foods.Count; i++)
        {
            if (Foods[i].Position.X == next.X &&
                Foods[i].Position.Y == next.Y)
            {
                Foods.RemoveAt(i);
                grow = true;
                SpawnFood();
                break;
            }
        }

        Snake.Move(grow);
    }
}
}