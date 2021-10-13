using UnityEngine;
using UnityEngine.Events;

// The Code below is from this YouTube Tutorial by Drapper Dino:
// https://www.youtube.com/watch?v=iXNwWpG7EhM

namespace GameEvents
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Event/Void Event")]
    public class VoidEvent: BaseGameEvent<Void>
    {
        public void Raise()
        {
            Raise(new Void());
        }
    }

}