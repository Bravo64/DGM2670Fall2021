using UnityEngine;

// The Code below is from this YouTube Tutorial by Drapper Dino:
// https://www.youtube.com/watch?v=iXNwWpG7EhM

namespace GameEvents
{
    [CreateAssetMenu(fileName = "New GameObject Event", menuName = "Event/GameObject Event")]
    public class GameObjectEvent : BaseGameEvent<GameObject>
    {
        
    }

}