//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kursovoy.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int ID_Appointment { get; set; }
        public int ID_Patient { get; set; }
        public System.DateTime Date { get; set; }
        public int ID_Department { get; set; }
        public int ID_MedicalOfficer { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual MedicalOfficer MedicalOfficer { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
