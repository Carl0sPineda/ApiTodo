using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiTodo.Repos.Models;

[Table("tasks")]
public partial class Task
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(40)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("autor")]
    [StringLength(30)]
    [Unicode(false)]
    public string Autor { get; set; } = null!;

    [Column("status_task")]
    [StringLength(25)]
    [Unicode(false)]
    public string StatusTask { get; set; } = null!;

    [Column("description")]
    [StringLength(150)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("start_date")]
    public DateOnly StartDate { get; set; }

    [Column("end_date")]
    public DateOnly EndDate { get; set; }
}
