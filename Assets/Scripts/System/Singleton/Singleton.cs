/************************************************************
 *  文件：Singleton
 *  作者：Azure
 *  功能：不继承MonoBehaviour的单例模式基类。
*************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>不继承MonoBehaviour的单例模式基类。</para>
/// <para>作用：继承这个类的类自带单例模式。</para>
/// <para>使用方法：让类A继承这个类，继承时，泛型写类A。外部要访问单例模式的类的对象，只需通过类名.Instance.成员名来访问。</para>
/// </summary>
public class Singleton<T> where T : Singleton<T>
{
    //禁止外部new这个类的对象。
    //但是继承这个类的类依然可以new对象，如果要让它们也无法new对象，则需要手动把它们的构造函数也设为private修饰的。
    protected Singleton() { }

    //线程锁。当多线程访问时，同一时刻仅允许一个线程访问。
    private static object locker = new object();

    //提供一个属性给外部访问，这个属性就相当于唯一的单例对象。
    //volatile关键字修饰的字段，当多个线程都会对它进行修改时，可以确保该字段在任何时刻呈现的都是最新的值。
    private volatile static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                        instance = Activator.CreateInstance(typeof(T), true) as T;//使用反射，调用无参构造函数来创建单例对象。
                }
            }
            return instance;
        }
    }
}
