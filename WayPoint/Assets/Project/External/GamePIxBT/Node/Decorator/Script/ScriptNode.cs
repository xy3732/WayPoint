using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptNode : DecoratorNode
{
    [field: SerializeField]public string characterName { get; set; }
    [field: SerializeField]public string schoolClubName { get; set; }

    [field: SerializeField] [field: TextArea(4, 2)] public string script { get; set; }
    
    [field:Space(20)]
    [field:Header("Script Effect Settings")]
    [field: SerializeField] private int arrayNumber { get; set; }
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

    [HideInInspector] public bool alreadyRead = false;
    private CharacterImageEffect image { get; set; }
    protected override void OnStart()
    {
        container.isScriptTriger = false;

        // text 설정
        container.textScript = script;
        UIManager.instance.setScriptText(characterName,schoolClubName,script);

        if (!UIManager.instance.scriptsUiObject.activeSelf) UIManager.instance.scriptsUiObject.SetActive(true);

        // effect 설정
        image = UIManager.instance.CharacterSprites[arrayNumber];
        if (image == null) Debug.LogWarning($"{this.name} - CharacterImageEffect is null");

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
        if (container.isScriptTriger)
        {
            alreadyRead = true;

            child.Update();
        }

        if(child.state == State.Success) return State.Success;
        else return State.Running;
    }
}
