using Microsoft.EntityFrameworkCore;
using TicketModule.Domain;

namespace TicketModule.Context;

class TicketContext : DbContext
{
    public TicketContext(DbContextOptions<TicketContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketMessage> TicketMessages { get; set; }
}
