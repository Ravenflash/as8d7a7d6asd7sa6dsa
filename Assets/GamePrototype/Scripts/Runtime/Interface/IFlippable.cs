namespace Ravenflash.GamePrototype
{
    public interface IFlippable
    {
        bool IsClickable { get; set; }
        bool IsActiveAndClickable {  get; set; }

        void Flip();
        void Unflip(float delay = 0);
        void Hide();
    }
}