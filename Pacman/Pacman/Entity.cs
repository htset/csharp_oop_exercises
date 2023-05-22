﻿namespace Pacman
{
  public enum EntityType
  {
    Pacman, Ghost
  }

  public abstract class Entity
  {
    public int x;
    public int y;
    public Direction direction;
    public EntityType type;
    public Game game;

    public void Move(int newX, int newY)
    {
      game.map[newX, newY].entity = game.map[x, y].entity;
      game.map[x, y].entity = null;
      x = newX;
      y = newY;
    }

    public abstract void Play();
  }
}
