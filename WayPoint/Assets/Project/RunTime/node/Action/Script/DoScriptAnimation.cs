using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoScriptAnimation : ActionNode
{
    [field: SerializeField] private int arrayNumber { get; set; }
    private CharacterImageEffect image { get; set; }

    public enum selectEffect
    {
        idle,
        speak,
        smile,
        angry,
        embarrassment,
    }
    public selectEffect types;


    protected override void OnStart()
    {
        image = UiManager.instance.CharacterSprites[arrayNumber];

        if(image == null)
        {
            Debug.LogWarning($"{this.name} - CharacterImageEffect is null");
        }
        else
        {
            switch (types)
            {
                case selectEffect.idle:
                    image.Idle();
                    break;

                case selectEffect.speak:
                    image.Speak();
                    break;

                case selectEffect.smile:
                    image.Smile();
                    break;

                case selectEffect.angry:
                    image.Angry();
                    break;

                case selectEffect.embarrassment:
                    image.Embarrassment();
                    break;
            }
        }
    }

    protected override void OnStop()
    {
      
    }

    protected override State OnUpdate()
    {
        if (image != null) return State.Success;
        else return State.Failure;
    }
}
