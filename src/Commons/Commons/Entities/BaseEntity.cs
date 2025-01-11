using System.Reflection;

namespace Commons.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTimeOffset DateCreated { get; protected set; } = DateTimeOffset.Now;
        public DateTimeOffset? DateUpdated { get; protected set; }
        public DateTimeOffset? DateDeleted { get; protected set; }

        public BaseEntity()
        {
        }

        // Update envery entity field except Id and DateCreated
        public virtual void Update(BaseEntity entity)
        {
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                if (property.GetValue(entity) != null
                    && property.CanWrite
                    && property.Name != nameof(Id)
                    && property.Name != nameof(DateCreated))
                {
                    property.SetValue(this, property.GetValue(entity));
                }
            }
            DateUpdated = DateTime.Now;
        }

        public virtual void Delete()
        {
            DateDeleted = DateTime.Now;
        }
    }
}
