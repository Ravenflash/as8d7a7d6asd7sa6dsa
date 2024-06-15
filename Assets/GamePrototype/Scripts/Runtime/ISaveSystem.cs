namespace Ravenflash.GamePrototype
{
    public interface ISaveSystem
    {
        SaveData Load();
        void Save(SaveData data);
        void Clear();
    }
}