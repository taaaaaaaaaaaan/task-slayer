using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task_slayer.Data.Contexts;
using task_slayer.Data.Entities;

namespace task_slayer.Data.Seeds
{
    public class StatusSeed
    {
        public static void Initialize(TaskSlayerContext context)
        {
            if (!context.Status.Any()) // Verifica se existem status no banco
            {
                var statusList = new[]
                {
                    new Status { Nome = "Pendente", IsDeleted = false },
                    new Status { Nome = "Em Andamento", IsDeleted = false },
                    new Status { Nome = "Concluído", IsDeleted = false }
                };

                context.Status.AddRange(statusList);
                context.SaveChanges();
            }
        }
    }
}