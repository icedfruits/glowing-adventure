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
            _chatDialogueRunner = ObjectDictionary.Instance.chatDialogueRunner;
            _subDialogueRunner = ObjectDictionary.Instance.subDialogueRunner;
            _chatDialogueRunner.AddCommandHandler<string>("event",Event);
            _chatDialogueRunner.AddCommandHandler("over",ChatOver);
            _chatDialogueRunner.AddCommandHandler<string>("sub",PlaySub);
            _chatDialogueRunner.AddCommandHandler<string>("subtitle",PlaySubtitle);
            _chatDialogueRunner.AddCommandHandler<string>("subtitleNow",PlaySubtitleNow);
            _chatDialogueRunner.AddCommandHandler<string, float>("lsub",PlayLateSub);
            _chatDialogueRunner.AddCommandHandler<string, float>("lsubtitle",PlayLateSubtitle);
            _chatDialogueRunner.AddCommandHandler<string, float>("lsubtitleNow",PlayLateSubtitleNow);
            _chatDialogueRunner.AddCommandHandler("clearSubtitle",ClearSubtitle);
            
            _subDialogueRunner.AddCommandHandler<string>("event",Event);
            _subDialogueRunner.AddCommandHandler("over",SubOver);
            _subDialogueRunner.AddCommandHandler<string>("sub",PlaySub);
            _subDialogueRunner.AddCommandHandler<string>("subtitle",PlaySubtitle);
            _subDialogueRunner.AddCommandHandler<string>("subtitleNow",PlaySubtitleNow);
            _subDialogueRunner.AddCommandHandler<string, float>("lsub",PlayLateSub);
            _subDialogueRunner.AddCommandHandler<string, float>("lsubtitle",PlayLateSubtitle);
            _subDialogueRunner.AddCommandHandler<string, float>("lsubtitleNow",PlayLateSubtitleNow);
            _subDialogueRunner.AddCommandHandler("clearSubtitle",ClearSubtitle);
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
        
        //Chat

        private void ChatOver()
        {
            //待实现
        }
        private void SubOver()
        {
            //待实现
        }
        
        
        //Subtitle

        private void PlaySub(string text)
        {
            PlaySubtitle(text);
        }
        
        private void PlayLateSub(string text, float waitTime)
        {
            PlayLateSubtitle(text,waitTime);
        }
        
        private void PlaySubtitle(string text)
        {
            SubtitleManager.Instance.AddSub(text);
        }

        private void PlayLateSubtitle(string text, float waitTime)
        {
            SubtitleManager.Instance.AddSub(text, waitTime);
        }

        private void PlaySubtitleNow(string text)
        {
            SubtitleManager.Instance.AddSubTop(text);
        }
        
        private void PlayLateSubtitleNow(string text, float waitTime)
        {
            SubtitleManager.Instance.AddSubTop(text, waitTime);
        }

        private void ClearSubtitle()
        {
            Debug.Log("字幕队列被清除");
            SubtitleManager.Instance.ClearSub(true);
        }
    }
}
