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
    using System.Linq;

    public partial class MedicalOfficer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalOfficer()
        {
            this.Appointment = new HashSet<Appointment>();
        }
    
        public int ID_MedicalOfficer { get; set; }
        public string FName { get; set; }
        public string Name { get; set; }
        public string LName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int ID_Gender { get; set; }
        public int ID_Department { get; set; }
        public Nullable<int> ID_Cabinet { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointment { get; set; }
        public virtual Cabinet Cabinet { get; set; }

        public string DepartmentName
        {
            get
            {
                var DepartmentName = helper.GetContext().Department.Where(p => p.ID_Department == this.ID_Department).FirstOrDefault();
                return DepartmentName.Name;
            }
        }

        public string GenderName
        {
            get
            {
                var DepartmentName = helper.GetContext().Gender.Where(p => p.ID_Gender == this.ID_Gender).FirstOrDefault();
                return DepartmentName.Name;
            }
        }
    }
}
