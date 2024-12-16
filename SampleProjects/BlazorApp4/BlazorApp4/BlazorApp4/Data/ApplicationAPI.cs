using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp4.Data
{
    public class ApplicationAPI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApplicationAPI()
        {
            RoleApis = new HashSet<ApplicationRoleAPI>();
        }

        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string API { get; set; }

        public virtual ICollection<ApplicationRoleAPI> RoleApis { get; set; }
    }
}
