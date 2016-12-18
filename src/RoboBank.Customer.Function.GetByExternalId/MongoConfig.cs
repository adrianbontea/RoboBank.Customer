using MongoDB.Bson.Serialization;
using RoboBank.Customer.Domain;

namespace RoboBank.Customer.Function.Update
{
    public static class MongoConfig
    {
        public static void RegisterMappings()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof (Dynamic)))
            {
                BsonClassMap.RegisterClassMap<Dynamic>(cm =>
                {
                    cm.AutoMap();
                    cm.MapExtraElementsProperty(entity => entity.Properties);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Profile)))
            {
                BsonClassMap.RegisterClassMap<Profile>(cm =>
                {
                    cm.AutoMap();
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Domain.Customer)))
            {
                BsonClassMap.RegisterClassMap<Domain.Customer>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIdMember(cm.GetMemberMap(cst => cst.Id));
                });
            }
        }
    }
}