﻿using System.Threading.Tasks;

namespace Kong.Interop
{
    public interface IConsumer
    {
        string Id { get; }
        long CreatedAt { get; }

        string CustomId { get; set; }
        string Username { get; set; }

        Task Delete();

        Task<IConsumer> Save();
    }
}
