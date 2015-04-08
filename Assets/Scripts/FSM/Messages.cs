using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM
{
    public enum MessageType
    {
        HiHoneyImHome,
        StewsReady
    }

    public struct Telegram
    {
        public double dispatchTime;
        public int sender;
        public int receiver;
        public MessageType messageType;

        public Telegram(double dt, int s, int r, MessageType mt)
        {
            dispatchTime = dt;
            sender = s;
            receiver = r;
            messageType = mt;
        }
    }

    public static class Message
    {
        public static List<Telegram> telegramQueue = new List<Telegram>();

        public static void Dispatch(double delay, int sender, int receiver, MessageType messageType)
        {
            //Agent sendingAgent = AgentManager.GetAgent(sender);
            Agent receivingAgent = AgentManager.GetAgent(receiver);

            Telegram telegram = new Telegram(0, sender, receiver, messageType);
            if(delay <= 0.0f) {
                Debug.Log("Instant Telegram dispatched by " + sender + " for " + receiver + " message is " + MessageToString(messageType));
                Send(receivingAgent, telegram);
            } else {
                telegram.dispatchTime = (int)Time.time + delay;
                telegramQueue.Add(telegram);
                Debug.Log("Delayed telegram from " + sender + " recorded at time " + Time.time);
            }
        }

        public static void SendDelayed()
        {
            for(int i = 0; i < telegramQueue.Count; i++) {
                if(telegramQueue[i].dispatchTime <= Time.time) {
                    Agent receivingAgent = AgentManager.GetAgent(telegramQueue[i].receiver);
                    Send(receivingAgent, telegramQueue[i]);
                    telegramQueue.RemoveAt(i);
                }
            }
        }

        public static void Send(Agent agent, Telegram telegram)
        {
            if(!agent.HandleMessage(telegram)) {
                Debug.Log("Message not handled!");
            }
        }

        public static String MessageToString(MessageType messageType)
        {
            switch(messageType) {
                case MessageType.HiHoneyImHome: return "Hi Honey I'm home!";
                case MessageType.StewsReady: return "Honey, the stew's ready!";
                default: return "Message not recognized";
            }
        }
    }
}
