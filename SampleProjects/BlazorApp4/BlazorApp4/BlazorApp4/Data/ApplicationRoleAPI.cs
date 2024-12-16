using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp4.Data
{
    [Table("AspNetRoleAPI")]
    public class ApplicationRoleAPI
    {
        public ApplicationRoleAPI()
        {
            Permissions = new HashSet<APIPermission>();
        }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Column(Order = 2)]
        public virtual string RoleId { get; set; }

        [Column(Order = 3)]
        public virtual int ApiId { get; set; }

        public virtual ApplicationRole Role { get; set; }

        public virtual ApplicationAPI API { get; set; }
        public ICollection<APIPermission> Permissions { get; set; }

    }
}
