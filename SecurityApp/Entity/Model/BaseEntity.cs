using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool State { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Este método debe ser llamado cuando se cree un nuevo registro
        public void OnCreate()
        {
            CreatedAt = DateTime.Now;  // Asigna la fecha actual al crearse el registro
            State = true;              // Por defecto, el estado es 'true'
        }

        // Este método debe ser llamado cuando se actualice un registro
        public void OnUpdate()
        {
            UpdatedAt = DateTime.Now;  // Actualiza la fecha cuando el registro es modificado
        }

        // Este método debe ser llamado cuando se elimine un registro (cambio de State a false)
        public void OnDelete()
        {
            if (!State)
            {
                DeletedAt = DateTime.Now;  // Asigna la fecha cuando se elimina (state = false)
            }
        }
    }
}
