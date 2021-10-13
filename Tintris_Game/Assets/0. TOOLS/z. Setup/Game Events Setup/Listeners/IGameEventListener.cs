
// The Code below is from this YouTube Tutorial by Drapper Dino:
// https://www.youtube.com/watch?v=iXNwWpG7EhM

namespace GameEvents
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
