using UnityEngine;
using UnityEngine.Events;

// The Code below is from this YouTube Tutorial by Drapper Dino:
// https://www.youtube.com/watch?v=iXNwWpG7EhM

namespace GameEvents
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "Event/Int Event")]
    public class IntEvent: BaseGameEvent<int>
    {
        
    }

}