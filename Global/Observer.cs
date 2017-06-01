using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Listener
    {
        #region Singleton
        private Listener() { }
        public static readonly Listener instance = new Listener();
        #endregion
        object sender;
        object reciever;

        public Listener From(object sender)
        {
            this.sender = sender;
            return this;
        }
        public Listener Let(object reciever)
        {
            this.reciever = reciever;
            return this;
        }
        public Listener Activate(Observer ob)
        {
            ob.eventArgs.reciever = reciever;
            ob.eventArgs.sender = sender;
            ob.Notify();
            return this;
        }
    }
    public class Observer
    {
        public delegate void ObserverEventHandler(EventArgs e);
        public event ObserverEventHandler Update;
        public EventArgs eventArgs = new EventArgs();
        public Observer() { }
        public virtual void Notify()
        {
            if (Update != null) { Update(eventArgs); }
        }
        public Observer From(object sender)
        {
            this.eventArgs.sender = sender;
            return this;
        }
        public Observer Let(object reciever)
        {
            eventArgs.reciever = reciever;
            return this;
        }
    }
}
