﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DatStore.Model;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Datastore.V1;

namespace DatStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatastoreDb _db;
        private readonly KeyFactory _keyFactory;

        public UserRepository()
        {

            _db = DatastoreDb.Create("david-on-microservices");
            _keyFactory = _db.CreateKeyFactory("User");
        }

        public Key AddUser(User user)
        {
            DateTime dateTime = DateTime.SpecifyKind(DateTime.ParseExact(user.Dob, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTimeKind.Utc);
            Entity entity = new Entity()
            {
                Key = _keyFactory.CreateIncompleteKey(),
                ["name"] = new Value()
                {
                    StringValue = user.Name
                },
                ["surname"] = new Value()
                {
                    StringValue = user.Surname
                },
                ["gender"] = new Value()
                {
                    StringValue = user.Gender
                },
                ["dob"] = new Value()
                {
                    StringValue = user.Dob
                },
                ["dob_timestamp"] = new Value()
                {
                    TimestampValue = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(dateTime)
                }
            };
            return _db.Insert(entity);
        }

        public ArrayList FindUsers(int offset, int limit) 
        {

            Query query = new Query("User")
            {
                Limit = limit,
                Offset = offset,
                Order = { { "dob_timestamp", PropertyOrder.Types.Direction.Descending } }
            };

            IEnumerable<Entity> entities = _db.RunQuery(query).Entities;
            ArrayList users = new ArrayList();
            foreach (Entity entity in entities) {
                User user = new User();
                user.Name = entity.Properties["name"].StringValue;
                user.Surname = entity.Properties["surname"].StringValue;
                user.Dob = entity.Properties["dob"].StringValue;
                user.Gender = entity.Properties["gender"].StringValue;
                users.Add(user);
            }
            return users;
        }

        public ArrayList FindUsersBySurname(int offset, int limit, string surname) 
        {
            Query query = new Query("User")
            {
                Limit = limit,
                Offset = offset,
                Filter = Filter.And(Filter.GreaterThanOrEqual("surname", surname), Filter.LessThan("surname", surname + "\ufffd")), // Trick
                Order = { { "dob_timestamp", PropertyOrder.Types.Direction.Descending } }
            };

            IEnumerable<Entity> entities = _db.RunQuery(query).Entities;
            ArrayList users = new ArrayList();
            foreach (Entity entity in entities)
            {
                User user = new User();
                user.Name = entity.Properties["name"].StringValue;
                user.Surname = entity.Properties["surname"].StringValue;
                user.Dob = entity.Properties["dob"].StringValue;
                user.Gender = entity.Properties["gender"].StringValue;
                users.Add(user);
            }
            return users;
        }

        public ArrayList FindUsersByNameAndSurname(int offset, int limit, string name, string surname)
        {
            Filter filter = null;

            if (string.IsNullOrEmpty(name)) {
                filter = Filter.Equal("surname", surname);
            } else {
                filter = Filter.And(Filter.Equal("name", name), Filter.Equal("surname", surname));
            }
            Filter.And(filter, Filter.And(Filter.GreaterThan("name", "david")));

            Query query = new Query("User")
            {
                Limit = limit,
                Offset = offset,
                Order = { { "dob_timestamp", PropertyOrder.Types.Direction.Descending } },
                Filter = filter
            };

            IEnumerable<Entity> entities = _db.RunQuery(query).Entities;
            ArrayList users = new ArrayList();
            foreach (Entity entity in entities)
            {
                User user = new User();
                user.Name = entity.Properties["name"].StringValue;
                user.Surname = entity.Properties["surname"].StringValue;
                user.Dob = entity.Properties["dob"].StringValue;
                user.Gender = entity.Properties["gender"].StringValue;
                users.Add(user);
            }
            return users;
        }

        public ArrayList FindByMultipleCriteria(int offset, int limit, params SearchCriteria[] args)
        {
            Filter filter = Filter.Equal(args[0].Key, args[0].Value);
            for (int i = 1; i < args.Length; i++) {
                filter = Filter.And(filter, Filter.Equal(args[i].Key, args[i].Value));
            }

            Query query = new Query("User")
            {
                Filter = filter,
                Offset = offset,
                Limit = limit
            };

            IEnumerable<Entity> entities = _db.RunQuery(query).Entities;
            ArrayList users = new ArrayList();
            foreach (Entity entity in entities)
            {
                User user = new User();
                user.Name = entity.Properties["name"].StringValue;
                user.Surname = entity.Properties["surname"].StringValue;
                user.Dob = entity.Properties["dob"].StringValue;
                user.Gender = entity.Properties["gender"].StringValue;
                users.Add(user);
            }
            return users;
        }
    }

}
