using GG.EventManager;
using UnityEngine;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]
public class GG_VoidEvent : DefaultEvent<Void>
{
    public void Raise() => Raise(new Void());

}    
