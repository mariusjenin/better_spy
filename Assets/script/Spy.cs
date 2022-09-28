using System.Collections.Generic;
using System.Numerics;
using script.action;

namespace script
{
    public class Spy
    {
        private Room _currentRoom;
        private bool _spotted;
        private bool _escaped;
        private List<ActionSpy> _actions;
        private Vector2 _position;
        
        public Spy(Room room)
        {
            EnterRoom(room);
            _spotted = false;
            _escaped = false;
            _actions = new List<ActionSpy>();
        }

        public void EnterRoom(Room room)
        {
            _currentRoom = room;
            _position = room.PosEntrance;
        }

        public void Moove(Vector2 position)
        {
         // TODO   
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
    }
}