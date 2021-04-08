using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameNS {
  [Serializable]
  public class Snake {

    public List<SnakePart> Body { get; }
    public SnakeHead Head { get; }
    public bool IsAlive { get; private set; }
    public int Lenght => Body.Count + 1; // + head! 
    public bool HasBody => Lenght > 1 ? true : false;

    public Snake(Point point) {
      Head = new SnakeHead(point, Direction.None);
      Body = new List<SnakePart>();
      Body.Add(Head);
      IsAlive = true;
    }

    // Adds new body to snake. Position should be same as position of tail, as tail field gets moved afterwards
    public void Extend() {
      Point newBodyPoint = new Point(Body.Last().Point.Row, Body.Last().Point.Column);
      Body.Add(new SnakeBodyPart(newBodyPoint));
    }

    // Moves snake and updates directions in correct order
    public void Move(Direction directionToMove) {
      MoveBody(directionToMove);
      UpdateBodyDirections(directionToMove);
    }

    public void Kill() {
      IsAlive = false;
    }

    // Moves body of snake
    private void MoveBody(Direction directionToMove) {
      // Move snake head. Is index 0 in body list
      Head.Move(directionToMove);
      // For every body part, move it based on direction of body part at previous index
      for(int i = 1; i < Body.Count; i++) { // Start at index one as head already moved
        if(!(Body[i] as SnakeBodyPart).NewBody) { // Dont move if new body
          Body[i].Move(Body[i - 1].Direction);
        }
        else { // Body not new anymore and will be moved next round
          (Body[i] as SnakeBodyPart).NewBody = false;
        }
      }
    }

    // Updates directions of body parts.
    private void UpdateBodyDirections(Direction directionToMove) {
      // Start at last index. Reverse order = swapping. Else direction will be the same for all items.
      for(int i = Body.Count - 1; i > 0; i--) {
        Body[i].Direction = Body[i - 1].Direction;
      }
      Head.Direction = directionToMove;
    }
  }
}
