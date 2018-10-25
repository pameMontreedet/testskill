using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using AppLib.Modules.Station;
using AppLib.Modules.Card;
using AppLib.Modules.Log;
using AppLib.Modules.CardType;

namespace AppLib {
    public class AppDbContext : DbContext {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}