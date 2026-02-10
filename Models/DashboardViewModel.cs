using System;
using System.Collections.Generic;

namespace LH_PET.Models
{
    public class DashboardViewModel
    {
        public int TotalClientes { get; set; }
        public int TotalAnimais { get; set; }
        public int TotalConsultas { get; set; }
        public int TotalFornecedores { get; set; }
        
        // Próximas consultas
        public List<ConsultaProximaViewModel> ProximasConsultas { get; set; } = new();
        
        // Dados para gráficos
        public List<string> TiposAnimais { get; set; } = new();
        public List<int> QuantidadesPorTipo { get; set; } = new();
        
        public List<string> TiposConsulta { get; set; } = new();
        public List<int> QuantidadesConsultaPorTipo { get; set; } = new();
        
        // Animais recentes
        public List<AnimalRecenteViewModel> AnimaisRecentes { get; set; } = new();
        
        // Clientes recentes
        public List<ClienteRecenteViewModel> ClientesRecentes { get; set; } = new();
    }
    
    public class ConsultaProximaViewModel
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeAnimal { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string StatusCss { get; set; } = "info";
    }
    
    public class AnimalRecenteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
    }
    
    public class ClienteRecenteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int QuantidadeAnimais { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
