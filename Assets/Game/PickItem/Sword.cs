using Assets.Game.Scripts.Interface;

namespace Game.PickItem
{
    public class Sword: IItem
    {
        public string Name { get; } = "Sword";
        public int Damage { get; } = 1;
     
    }
}