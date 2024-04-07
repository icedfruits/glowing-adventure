using System;
using Manager.Base;
using Manager.Core;
using Manager.Event;
using UnityEngine;
using Yarn.Unity;
// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable once CheckNamespace

namespace Manager.Dialogue
{
    //通过挂载创建
    public class SpinnerCommands : MonoBehaviour
    {
        private DialogueRunner _chatDialogueRunner;
        private DialogueRunner _subDialogueRunner;

        private void Start()
        {
            if (GameObject.Find("SpinnerNoCommand"))
            {
                return;
            }
            _chatDialogueRunner = ObjectDictionary.Instance.chatDialogueRunner;
            _subDialogueRunner = ObjectDictionary.Instance.subDialogueRunner;
            _chatDialogueRunner.AddCommandHandler<string>("event",Event);
            _chatDialogueRunner.AddCommandHandler("over",ChatOver);
            _chatDialogueRunner.AddCommandHandler<string>("sub",PlaySub);
            _chatDialogueRunner.AddCommandHandler<string>("btn",BtnText);
            _chatDialogueRunner.AddCommandHandler<string>("nsub",PlaySubNow);
            _chatDialogueRunner.AddCommandHandler<string, float>("hsub",PlayHoldSub);
            _chatDialogueRunner.AddCommandHandler<string, float>("lsub",PlayLateSub);
            
            _subDialogueRunner.AddCommandHandler<string>("event",Event);
            _subDialogueRunner.AddCommandHandler("over",SubOver);
            _subDialogueRunner.AddCommandHandler<string>("sub",PlaySub);
            _subDialogueRunner.AddCommandHandler<string, float>("hsub",PlayHoldSub);
            _subDialogueRunner.AddCommandHandler<string, float>("lsub",PlayLateSub);
        }

        private void Event(string eventName)
        {
            if (Enum.TryParse<EEventList>(eventName, out var eEventListName))
            {
                EventCenter.Instance.EventTrigger(eEventListName);
            }
            else
            {
                Debug.LogError("Yarn spinner调用了不存在的事件");
            }
        }

        private void ChatOver()
        {
            ChatManager.Instance.ChatOver(_chatDialogueRunner.CurrentNodeName);
        }
        private void SubOver()
        {
            PlayLateSub("**OVER**" + _subDialogueRunner.CurrentNodeName, 0f);
        }

        private void BtnText(string text)
        {
            ChatManager.Instance.ChangeButtonText(text);
        }

        private void PlaySub(string text)
        {
            SubtitleManager.Instance.AddSub(text);
        }

        private void PlaySubNow(string text)
        {
            //待实现
        }
        
        private void PlayHoldSub(string text, float holdTime)
        {
            SubtitleManager.Instance.AddSub(text, holdTime);
        }
        
        private void PlayLateSub(string text, float waitTime)
        {
            SubtitleManager.Instance.AddSub(text, 0f, waitTime);
        }

    }
}
