using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace USEFUL {

    public class TimeList<T> {




        List<TimeListInfo<T>> _list;

        // maybe improve this with a timelistInfoPool

        public TimeList() {
            _list = new List<TimeListInfo<T>>();
        } // TimeList

        public TimeListInfo<T> Add(float totalTime) {
            TimeListInfo<T> temp = new TimeListInfo<T>(totalTime);
            _list.Add(temp);

            return temp;
        } // Add

        public void Remove(int id) {

            bool found = false;
            for (int i = 0; !found && i < _list.Count; ++i) {
                if (_list[i].getId() == id) {
                    found = true;
                    if (i + 1 < _list.Count) {
                        _list[i + 1].modifyNextTime(+_list[i].getTotalTime());
                    }
                    _list.RemoveAt(i);
                }
            } // end for

        } // Remove
    }

    public class TimeListInfo<T> {

        private static TimeList<T> _parent;

        private int _id;
        private float _nextTime;
        private float _totalTime;
        public T _element;
        public event VoidDelegate _eventsAtFinish;

        public TimeListInfo(float totaltime, VoidDelegate[] events = null) {
            _totalTime = totaltime;

            if (events != null) {
                for (int i = events.Length - 1; i >= 0; --i) {
                    _eventsAtFinish += events[i];
                }
            }
        } // TimeListInfo


        // gets & sets
        public int getId() {
            return _id;
        } // getId

        public float getTotalTime() {
            return _totalTime;
        } // getTotalTime

        public float getNextTime() {
            return _nextTime;
        } // getNextTime

        public void modifyNextTime(float mod) {
            _nextTime += mod;
        }
    } // TimeListInfo

}