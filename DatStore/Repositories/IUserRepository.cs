﻿using System;
using System.Collections;
using System.Collections.Generic;
using DatStore.Model;
using Google.Cloud.Datastore.V1;

namespace DatStore.Repositories
{
    public interface IUserRepository
    {
        Key AddUser(User user);
        ArrayList FindUsers(int offset, int limit);
        ArrayList FindUsersBySurname(int offset, int limit, string surname);
    }
}
