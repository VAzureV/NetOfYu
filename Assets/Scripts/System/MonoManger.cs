/****************************************************
    文件：MonoManger.cs
	作者：Azure
	功能：让不继承MonoBehaviour类可以开启协程，可以用FixedUpdate、Update、LateUpdate方法更新。
*****************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 作用：让不继承MonoBehaviour类可以开启协程，可以用FixedUpdate、Update、LateUpdate方法更新。
/// </summary>
public class MonoManager : Singleton<MonoManager>
{
    //构造方法私有化，防止外部new对象。
    private MonoManager() { }

    //执行Mono逻辑的属性。
    private MonoController monoExecuter;
    private MonoController MonoExecuter
    {
        get
        {
            //会确保场景中有一个游戏对象挂载了MonoController脚本。如果没有，则会自动创建该游戏对象，并挂载MonoController脚本。
            if (monoExecuter == null)
            {
                GameObject go = new GameObject(typeof(MonoController).Name);
                monoExecuter = go.AddComponent<MonoController>();
            }

            return monoExecuter;
        }
    }
    public void Init()
    {
        Debug.Log("____Init MonoManger Success____");
    }
    /// <summary>
    /// 添加FixedUpdate的事件。
    /// </summary>
    public void AddFixedUpdateListener(UnityAction call)
    {
        MonoExecuter.AddFixedUpdateListener(call);
    }
    /// <summary>
    /// 移除FixedUpdate的事件。
    /// </summary>
    public void RemoveFixedUpdateListener(UnityAction call)
    {
        MonoExecuter.RemoveFixedUpdateListener(call);
    }
    /// <summary>
    /// 移除所有FixedUpdate的事件。
    /// </summary>
    public void RemoveAllFixedUpdateListeners()
    {
        MonoExecuter.RemoveAllFixedUpdateListeners();
    }

    /// <summary>
    /// 添加Update的事件。
    /// </summary>
    public void AddUpdateListener(UnityAction call)
    {
        MonoExecuter.AddUpdateListener(call);
    }
    /// <summary>
    /// 移除Update的事件。
    /// </summary>
    public void RemoveUpdateListener(UnityAction call)
    {
        MonoExecuter.RemoveUpdateListener(call);
    }
    /// <summary>
    /// 移除所有Update的事件。
    /// </summary>
    public void RemoveAllUpdateListeners()
    {
        MonoExecuter.RemoveAllUpdateListeners();
    }

    /// <summary>
    /// 添加LateUpdate的事件。
    /// </summary>
    public void AddLateUpdateListener(UnityAction call)
    {
        MonoExecuter.AddLateUpdateListener(call);
    }
    /// <summary>
    /// 移除LateUpdate的事件。
    /// </summary>
    public void RemoveLateUpdateListener(UnityAction call)
    {
        MonoExecuter.RemoveLateUpdateListener(call);
    }
    /// <summary>
    /// 移除所有LateUpdate的事件。
    /// </summary>
    public void RemoveAllLateUpdateListeners()
    {
        MonoExecuter.RemoveAllLateUpdateListeners();
    }

    /// <summary>
    /// 移除所有Update、FixedUpdate、LateUpdate的事件。
    /// </summary>
    public void RemoveAllListeners()
    {
        MonoExecuter.RemoveAllListeners();
    }

    /// <summary>
    /// 开启协程、停止协程。
    /// </summary>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return MonoExecuter.StartCoroutine(routine);
    }
    public void StopCoroutine(IEnumerator routine)
    {
        if (routine != null)
            MonoExecuter.StopCoroutine(routine);
    }
    public void StopCoroutine(Coroutine routine)
    {
        if (routine != null)
            MonoExecuter.StopCoroutine(routine);
    }
    public void StopAllCoroutines()
    {
        MonoExecuter.StopAllCoroutines();
    }

    //用于执行MonoBehaviour逻辑的脚本。会挂载到场景的游戏对象MonoExecuter身上。
    private class MonoController : MonoBehaviour
    {
        event UnityAction updateEvent;//在生命周期方法Update中执行的事件。

        event UnityAction fixedUpdaetEvent;//在生命周期方法FixedUpdate中执行的事件。

        event UnityAction lateUpdateEvent;//在生命周期方法LateUpdate中执行的事件。

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void FixedUpdate()
        {
            fixedUpdaetEvent?.Invoke();
        }

        void Update()
        {
            updateEvent?.Invoke();
        }

        void LateUpdate()
        {
            lateUpdateEvent?.Invoke();
        }

        /// <summary>
        /// 添加FixedUpdate的事件。
        /// </summary>
        /// <param name="call">要监听的事件</param>
        public void AddFixedUpdateListener(UnityAction call)
        {
            fixedUpdaetEvent += call;
        }
        /// <summary>
        /// 移除FixedUpdate的事件。
        /// </summary>
        /// <param name="call">要监听的事件</param>
        public void RemoveFixedUpdateListener(UnityAction call)
        {
            fixedUpdaetEvent -= call;
        }
        /// <summary>
        /// 移除所有FixedUpdate的事件。
        /// </summary>
        public void RemoveAllFixedUpdateListeners()
        {
            fixedUpdaetEvent = null;
        }

        /// <summary>
        /// 添加Update的事件。
        /// </summary>
        /// <param name="call">要监听的事件</param>
        public void AddUpdateListener(UnityAction call)
        {
            updateEvent += call;
        }
        /// <summary>
        /// 移除Update的事件。
        /// </summary>
        public void RemoveUpdateListener(UnityAction call)
        {
            updateEvent -= call;
        }
        /// <summary>
        /// 移除所有Update的事件。
        /// </summary>
        public void RemoveAllUpdateListeners()
        {
            updateEvent = null;
        }

        /// <summary>
        /// 添加LateUpdate的事件。
        /// <param name="call">要监听的事件</param>
        /// </summary>
        public void AddLateUpdateListener(UnityAction call)
        {
            lateUpdateEvent += call;
        }
        /// <summary>
        /// 移除LateUpdate的事件。
        /// <param name="call">要监听的事件</param>
        /// </summary>
        public void RemoveLateUpdateListener(UnityAction call)
        {
            lateUpdateEvent -= call;
        }
        /// <summary>
        /// 移除所有LateUpdate的事件。
        /// </summary>
        public void RemoveAllLateUpdateListeners()
        {
            lateUpdateEvent = null;
        }

        /// <summary>
        /// 移除所有FixedUpdate、Update、LateUpdate的事件。
        /// </summary>
        public void RemoveAllListeners()
        {
            RemoveAllFixedUpdateListeners();
            RemoveAllUpdateListeners();
            RemoveAllUpdateListeners();
        }
    }
}