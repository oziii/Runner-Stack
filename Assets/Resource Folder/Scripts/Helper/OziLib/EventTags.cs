using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OziLib
{
    public sealed class EventTags
    {
        static public string TEST = "TEST";
        static public string TEST_DATA = "TEST_DATA";
        static public string TEST_I = "TEST_I";

        //Game Action
        static public string LEVEL_START = "LEVEL_START";
        static public string LEVEL_END = "LEVEL_END";
        static public string NEXT_LEVEL = "NEXT_LEVEL";
        
        //Special Action
        static public string DIA_COLLECT = "DIA_COLLECT";
        static public string COIN_COLLECT = "COIN_COLLECT";
        static public string STACK_COUNT_BUTTON = "STACK_COUNT_BUTTON";
        static public string DIA_DESTROY = "DIA_DESTROY";
        
        //Data Name
        static public string LEVEL_COUNTER = "LEVEL_COUNTER";
        static public string COIN_COUNTER = "COIN_COUNTER";
        static public string STACK_MAX_COUNTER = "STACK_MAX_COUNTER";
        static public string START_STACK_COUNT = "START_STACK_COUNT";
        static public string STACK_UPGRADE_VALUE = "STACK_UPGRADE_VALUE";
        
        //Const Data
        public const int UPGRADE_RATIO = 15;
    }
}