using UnityEngine;

public abstract class Reaction : ScriptableObject
    //ScriptableObject supports polymorphic serialization, hence allowing to write type specific inspector views
{
    public void Init ()
    {
        SpecificInit ();
    }


    protected virtual void SpecificInit()
    {}


    public void React (MonoBehaviour monoBehaviour)
    {
        ImmediateReaction ();
    }


    protected abstract void ImmediateReaction ();
}
