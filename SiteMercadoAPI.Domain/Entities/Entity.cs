using System;

namespace SiteMercadoAPI.Domain.Entities
{
    public abstract class Entity 
    {

        public Guid Id { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

    }
}