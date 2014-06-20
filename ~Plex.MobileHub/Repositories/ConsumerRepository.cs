using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plex.MobileHub.Objects;
namespace Plex.MobileHub.Repositories
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

        Dictionary<Int32, Consumer> ConsumerRepo = new Dictionary<Int32, Consumer>();

        public Dictionary<Int32, Consumer> GetAll()
        {
            return new Dictionary<Int32, Consumer>(ConsumerRepo);
        }

        public Int32 Add(Consumer consumer)
        {
            Int32 Key;
            var r = new Random();

            do Key = r.Next();
            while (ConsumerRepo.ContainsKey(Key));

            ConsumerRepo.Add(Key, consumer);
            consumer.ConsumerId = Key;
            return Key;
        }

        public void Modify(Int32 ConnectionId, Consumer Data)
        {
            ConsumerRepo[ConnectionId] = Data;
        }

        public void Remove(Int32 i)
        {
            ConsumerRepo.Remove(i);
        }

        public Consumer Retrieve(Int32 i)
        {
            return ConsumerRepo[i];
        }

        public bool Exists(Int32 i)
        {
            return ConsumerRepo.ContainsKey(i);
        }
    }
}