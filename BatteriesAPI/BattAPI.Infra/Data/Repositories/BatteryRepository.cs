﻿using BattAPI.Domain.Entities;
using BattAPI.Domain.Repositories;

namespace BattAPI.Infra.Data.Repositories
{
    public class BatteryRepository(AppDbContext context) : RepositoryBase<Battery>(context), IBatteryRepository
    {
    }
}
