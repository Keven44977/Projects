using Microsoft.EntityFrameworkCore;

namespace Revue_Par_Les_Pairs_V2.DAL_SQLServeur
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<CommentaireDTO> Commentaire { get; set; }
        public DbSet<EtudiantDTO> Etudiant { get; set; }
        public DbSet<CoursDTO> Cours { get; set; }
        public DbSet<SolutionDTO> Solution { get; set; }
        public DbSet<TravailDTO> Travail { get; set; }
        public DbSet<CorrectionDTO> Correction { get; set; }
        public DbSet<InscriptionDTO> Inscription { get; set; }
    }
}
