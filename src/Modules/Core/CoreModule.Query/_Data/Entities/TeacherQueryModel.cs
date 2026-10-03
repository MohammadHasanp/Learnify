using Common.Domain;
using CoreModule.Domain.Teachers.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("Teachers", Schema = "dbo")]
class TeacherQueryModel : Entity
{
    public Guid UserId { get; private set; }

    [Unicode(false)]
    [Required]
    [MaxLength(100)]
    public string UserName { get; private set; } = null!;

    [Required]
    [MaxLength(100)]
    public string CvFileName { get; private set; } = null!;

    public TeacherStatus TeacherStatus { get; private set; }

    [ForeignKey("UserId")]
    public required UserQueryModel Users { get; set; }
}
