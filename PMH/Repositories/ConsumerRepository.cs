using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileHub.Objects;
namespace MobileHub.Repositories
{
    public class Consumers
    {
        private static Consumers consumers = new Consumers();
        public static Consumers Instance
        {
            get
            {
                return consumers;
            }
        }
        Dictionary<int, Consumer> ConsumerRepo = new Dictionary<int, Consumer>();

        public Dictionary<int, Consumer> GetAll()
        {
            return new Dictionary<int, Consumer>(ConsumerRepo);
        }

        public int Add(Consumer consumer)
        {
            int Key;
            var r = new Random();

            do Key = r.Next();
            while (ConsumerRepo.ContainsKey(Key));

            ConsumerRepo.Add(Key, consumer);
            consumer.ConsumerId = Key;
            return Key;
        }

        public void Modify(int ConnectionId, Consumer Data)
        {
            ConsumerRepo[ConnectionId] = Data;
        }

        public void Remove(int i)
        {
            ConsumerRepo.Remove(i);
        }

        public Consumer Retrieve(int i)
        {
            return ConsumerRepo[i];
        }

        public bool Exists(int i)
        {
            return ConsumerRepo.ContainsKey(i);
        }
    }
}