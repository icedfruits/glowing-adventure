using System;
using Manager.Base;
using Manager.Core;
using Manager.Event;
using UnityEngine;
using Yarn.Unity;
// ReSharper disable MemberCanBeMadeStatic.Local

namespace Manager.Dialogue
{
    //通过挂载创建
    public class SpinnerCommands : MonoBehaviour
    {
        private DialogueRunner _dialogueRunner;

        private void Start()
        {
            _dialogueRunner = ObjectDictionary.Instance.chatDialogueRunner;
            Debug.Log(_dialogueRunner);
            _dialogueRunner.AddCommandHandler<string>("event",Event);
            _dialogueRunner.AddCommandHandler("chatOver",ChatOver);
            _dialogueRunner.AddCommandHandler<string>("sub",PlaySub);
            _dialogueRunner.AddCommandHandler<string>("subtitle",PlaySubtitle);
            _dialogueRunner.AddCommandHandler<string>("subtitleNow",PlaySubtitleNow);
            _dialogueRunner.AddCommandHandler<string, float>("lsub",PlayLateSub);
            _dialogueRunner.AddCommandHandler<string, float>("lsubtitle",PlayLateSubtitle);
            _dialogueRunner.AddCommandHandler<string, float>("lsubtitleNow",PlayLateSubtitleNow);
            _dialogueRunner.AddCommandHandler("clearSubtitle",ClearSubtitle);
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
