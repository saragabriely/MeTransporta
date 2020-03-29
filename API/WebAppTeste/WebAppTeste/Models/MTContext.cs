using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppTeste.Models
{
    // [DbConfigurationType(typeof(MyConfiguration))]
    public class MTContext : DbContext
    {
        public MTContext() : base("name=MeTransporta") {  }

        //public System.Data.Entity.DbSet<WebAppTeste.Models.teste> Testes { get; set; }
        
        public DbSet<Login>           Acesso           { get; set; }

        public DbSet<AcompanhaColeta> Acompanhas       { get; set; }
                                                       
        public DbSet<Banco>           Bancos           { get; set; }
                                                       
        public DbSet<BandeiraCartao>  Bandeiras        { get; set; }
                                                       
        public DbSet<CartaoCredito>   Cartao           { get; set; }
                                                       
        public DbSet<Cliente>         Clientes         { get; set; }
                                                       
        public DbSet<Coleta>          Coletas          { get; set; }

        public DbSet<ColetaVisualiza> ColetasVisualiza { get; set; }

        public DbSet<ContaBancaria>   ContaBancarias   { get; set; }
                                                       
        public DbSet<Material>        Materials        { get; set; }
                                                       
        public DbSet<Motorista>       Motoristas       { get; set; }
                                                       
        public DbSet<Orcamento>       Orcamentos       { get; set; }
                                                       
        public DbSet<Status>          Status           { get; set; }
                                                       
        public DbSet<teste>           Testes           { get; set; }
                                                       
        public DbSet<TipoVeiculo>     TiposVeiculo     { get; set; }
                                                       
        public DbSet<TipoUsuario>     TipoUsuarios     { get; set; }
                                                       
        public DbSet<Veiculo>         Veiculos         { get; set; }
                                                        
        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        } */
    }
}