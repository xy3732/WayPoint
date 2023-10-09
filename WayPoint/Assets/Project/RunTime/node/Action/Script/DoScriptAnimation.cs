using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoScriptAnimation : ActionNode
{
    [field: SerializeField] private int arrayNumber { get; set; }
    private CharacterImageEffect image { get; set; }

    public enum emotionEffect
    {
        none,
        question,
        shine,
        waterDrop,
    }


    public enum selectEffect
    {
        idle,
        speak,
        smile,
        angry,
        embarrassment,
    }
    public selectEffect types;
    public emotionEffect emtionTypes;


    protected override void OnStart()
    {
        image = UiManager.instance.CharacterSprites[arrayNumber];

        if(image == null) Debug.LogWarning($"{this.name} - CharacterImageEffect is null");

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

            switch (emtionTypes)
            {
                case emotionEffect.none:
                    image.characterEmotion.None();
                    break;

                case emotionEffect.question:
                    image.characterEmotion.QuestionMark();
                    break;

                case emotionEffect.shine:
                    image.characterEmotion.Shine();
                    break;

                case emotionEffect.waterDrop:
                    image.characterEmotion.WaterDrop();
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
