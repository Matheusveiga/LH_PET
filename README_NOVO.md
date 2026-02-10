# 🐾 LH_PET - Sistema de Gestão para Clínicas Veterinárias

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License](https://img.shields.io/badge/license-MIT-green)]()
[![Security](https://img.shields.io/badge/security-hardened-blue)]()

O **LH_PET** é um sistema web moderno desenvolvido em **ASP.NET MVC** (.NET 8.0) voltado para a gestão completa de **clientes**, **animais** e **consultas** em clínicas veterinárias.

> 🔐 **Segurança em Primeiro Lugar**: Este projeto implementa as melhores práticas de segurança incluindo autenticação robusta, rate limiting, validação avançada e logging de auditoria.

---

## ✨ Destaques Principais

- ✅ **Autenticação Segura** com BCrypt e JWT
- ✅ **Rate Limiting** contra força bruta (5 tentativas/15 min)
- ✅ **Validação de CPF/CNPJ** com dígito verificador
- ✅ **Logging de Auditoria** com Serilog
- ✅ **Credenciais Seguras** via User Secrets
- ✅ **Proteção HTTPS** obrigatória em produção
- ✅ **CSRF Protection** em todos os formulários
- ✅ Responsividade para dispositivos móveis

---

## 📸 Demonstrações

### 🔐 Login com Autenticação Segura
![image](https://github.com/user-attachments/assets/17f31cac-a4a9-493f-8328-4c8f205e3680)

### 🏠 Dashboard Principal
![TelaHome](https://github.com/user-attachments/assets/0e356bbe-16ff-4ca2-a90d-13b875c4ea73)

### 👤 Cadastro de Cliente (com Validação de CPF)
![TelaCadastroCliente](https://github.com/user-attachments/assets/04eedf39-873a-421e-bcf7-2ed28ac64d8f)

### 🔍 Consulta de Clientes
![TelaConsultaClientes](https://github.com/user-attachments/assets/439ae459-724e-4420-82e5-d1baed4ebe36)

### 📋 Detalhes do Cliente e Animais Associados
![TelaDadosCliente](https://github.com/user-attachments/assets/1a698cba-03db-44bf-a75e-69f9ca7c168b)

### 🐶 Cadastro de Animal
![TelaCadastroAnimal](https://github.com/user-attachments/assets/ccd5f4b0-461f-4d5e-84cd-ab062a5943d5)

### 📅 Agendamento de Consultas
![image](https://github.com/user-attachments/assets/f8c17171-92b3-4767-b1c3-7a9b248e3d5d)

---

## ⚙️ Tecnologias Utilizadas

### Backend
- **Framework**: ASP.NET MVC (.NET 8.0)
- **Linguagem**: C# 13
- **Banco de Dados**: MySQL 8.0
- **ORM**: Entity Framework Core 8.0
- **Autenticação**: BCrypt.Net + JWT

### Segurança & Observabilidade
- **Rate Limiting**: IMemoryCache
- **Auditoria**: Serilog
- **Validação**: Atributos customizados (CPF/CNPJ)
- **Configuration**: User Secrets (.NET)

### Frontend
- **Template Engine**: Razor Pages
- **Framework CSS**: Bootstrap 5
- **Validação Client**: jQuery Validation

---

## 🔐 Segurança Implementada

### 1. **Autenticação e Autorização**
- ✅ Senhas com hash BCrypt
- ✅ JWT tokens com expiração configurável
- ✅ Cookies de autenticação seguro (HttpOnly)
- ✅ CSRF protection em todos os formulários POST
- ✅ Validação de força de senha (8+ caracteres, maiúscula, minúscula, número, especial)

### 2. **Rate Limiting contra Força Bruta**
- ✅ Máximo 5 tentativas de login em 15 minutos
- ✅ IP e Username rastreados
- ✅ Bloqueio automático com mensagem amigável
- ✅ Aplicado tanto ao login Web quanto à API

### 3. **Validação de Dados**
- ✅ CPF validado com dígito verificador (algoritmo oficial)
- ✅ CNPJ validado com dígito verificador
- ✅ Email validado com RFC 5322
- ✅ Sanitização automática de entrada

### 4. **Logging de Auditoria**
Todos os eventos críticos são registrados via Serilog:
- Login bem-sucedido (usuário + IP + timestamp)
- Login falhado (motivo + IP + timestamp)
- Registro de novo usuário
- Tentativas bloqueadas por rate limit
- Operações sensíveis (logout)

Logs localizados em: `logs/lh_pet-{data}.txt`

### 5. **Configuração Segura**
- ✅ Credenciais **NUNCA** em código fonte
- ✅ User Secrets para desenvolvimento
- ✅ Variáveis de ambiente para produção
- ✅ JWT Key validada na inicialização
- ✅ HTTPS obrigatório em produção

---

## 📂 Funcionalidades

### ✅ Implementadas
- [x] **Clientes**: Cadastro, consulta, busca, detalhes, validação de CPF
- [x] **Animais**: Cadastro, consulta, edição, exclusão, busca por tipo/raça
- [x] **Consultas**: Agendamento, visualização, edição
- [x] **Fornecedores**: Cadastro, consulta, validação de CNPJ
- [x] **Autenticação**: Login/Registro com segurança robusta
- [x] **Rate Limiting**: Proteção contra força bruta
- [x] **Auditoria**: Logs estruturados de eventos críticos
- [x] **Responsividade**: Mobile-first design

### 🔄 Em Desenvolvimento
- [ ] Prontuários e histórico médico
- [ ] Integração com gateway de pagamento
- [ ] Agendamento automático por email
- [ ] Dashboard com estatísticas

---

## 🚀 Guia de Instalação

### Pré-requisitos
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) ou superior
- MySQL 8.0 (local ou Docker)
- Git

### 1️⃣ Clonar Repositório

```bash
git clone https://github.com/Matheusveiga/LH_PET.git
cd LH_PET
```

### 2️⃣ Configurar MySQL

**Opção A: MySQL Local**
```bash
mysql -u root -p < sql/create_schema.sql
```

**Opção B: Docker MySQL**
```bash
docker run --name lh-mysql \
  -e MYSQL_ROOT_PASSWORD=sua_senha \
  -p 3306:3306 \
  -d mysql:8

# Aguarde o container inicializar (~10s) e execute:
docker exec -i lh-mysql mysql -u root -p'sua_senha' < sql/create_schema.sql
```

### 3️⃣ Configurar User Secrets (Desenvolvimento)

Este projeto **NÃO armazena credenciais no código**. Você precisa configurar User Secrets:

```bash
# Inicializar User Secrets (executar UMA VEZ)
dotnet user-secrets init

# Configurar Connection String
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "Server=127.0.0.1;Port=3306;Database=lh_pet_db;Uid=root;Pwd=SUA_SENHA;"

# Configurar JWT Key (gerar chave aleatória forte)
# Linux/Mac:
dotnet user-secrets set "Jwt:Key" "$(openssl rand -base64 32)"

# Windows PowerShell:
# $key = [System.Convert]::ToBase64String([System.Security.Cryptography.RandomNumberGenerator]::GetBytes(32))
# dotnet user-secrets set "Jwt:Key" $key
```

**Verificar configuração:**
```bash
dotnet user-secrets list
```

Para detalhes completos: Veja [SECRETS_SETUP.md](./SECRETS_SETUP.md)

### 4️⃣ Restaurar Dependências e Compilar

```bash
dotnet restore
dotnet build
```

### 5️⃣ Executar Aplicação

```bash
dotnet run
```

A aplicação estará disponível em:
- [http://localhost:5000](http://localhost:5000) (HTTP - desenvolvimento)
- [https://localhost:5001](https://localhost:5001) (HTTPS)

---

## 📖 Guia de Uso

### 🔑 Primeiro Acesso

1. Acesse [http://localhost:5000](http://localhost:5000)
2. Clique em **Criar Conta**
3. **Credenciais obrigatórias**:
   - Username (único)
   - Email (válido)
   - Senha (min 8 caracteres, maiúscula, minúscula, número, especial)

### 👤 Cadastrar Cliente

1. Login na aplicação
2. Menu → **Clientes** → **Cadastrar Cliente**
3. Preencha:
   - **Nome**: Máx 80 caracteres
   - **CPF**: Será validado automaticamente (dígito verificador)
   - **Email**: Válido e único

### 🐶 Cadastrar Animal

1. Menu → **Animais** → **Cadastrar Animal**
2. Selecione o cliente associado
3. Preencha dados: Nome, Tipo, Sexo, Raça, Idade

### 📅 Agendar Consulta

1. Menu → **Consultas** → **Agendar Consulta**
2. Selecione: Cliente e Animal
3. Escolha data/hora
4. Adicione descrição (opcional)

---

## 🔍 Monitoramento & Logs

### Acessar Logs

Os logs são armazenados em:
```
logs/lh_pet-YYYY-MM-DD.txt
```

**Exemplo de log:**
```
[2026-02-10 14:35:22] [INF] Successful login for user joao.silva from IP 192.168.1.100
[2026-02-10 14:35:45] [WRN] Failed login attempt for user joao.silva from IP 192.168.1.100 - Reason: Invalid credentials
[2026-02-10 14:35:48] [WRN] Rate limit exceeded for joao.silva from IP 192.168.1.100. Possible brute force attack.
[2026-02-10 14:36:10] [INF] New user registered - Username: maria.santos, Email: maria@email.com
```

### Configurar Logging

Edite `appsettings.Development.json` para ajustar o nível de log:

```json
"Logging": {
  "LogLevel": {
    "Default": "Information",      // Default: Information, Debug, Warning
    "Microsoft.AspNetCore": "Warning"
  }
}
```

---

## 🛠️ Estrutura do Projeto

```
LH_PET/
├── Controllers/              # Controladores MVC
│   ├── AutenticacaoController.cs
│   ├── DadosClienteController.cs
│   ├── DadosAnimalController.cs
│   └── ...
├── Models/                   # Modelos de dados
│   ├── User.cs
│   ├── Cliente.cs
│   ├── Animal.cs
│   └── ValidationAttributes.cs
├── Services/                 # Lógica de negócio
│   ├── IValidationService.cs
│   ├── IRateLimitService.cs
│   ├── IAuditService.cs
│   └── ...
├── Views/                    # Razor Views
│   ├── Autenticacao/
│   ├── DadosCliente/
│   └── ...
├── Context/                  # EF Core DbContext
│   └── AppDbContext.cs
├── Migrations/               # EF Core Migrations
└── sql/                      # Scripts SQL
```

---

## 🔧 Configurações Importantes

### appsettings.json (Produção)

```json
{
  "Security": {
    "RequireHttps": true,
    "RateLimiting": {
      "Enabled": true,
      "MaxAttempts": 5,
      "WindowMinutes": 15
    }
  },
  "Jwt": {
    "ExpireMinutes": 120
  }
}
```

### Variáveis de Ambiente (Produção)

```bash
# Linux/Mac
export ConnectionStrings__DefaultConnection="Server=db;Port=3306;Database=lh_pet_db;Uid=user;Pwd=pass;"
export Jwt__Key="sua_chave_jwt_forte_aqui"
export ASPNETCORE_ENVIRONMENT="Production"

# Windows
set ConnectionStrings__DefaultConnection=Server=db;Port=3306;Database=lh_pet_db;Uid=user;Pwd=pass;
set Jwt__Key=sua_chave_jwt_forte_aqui
set ASPNETCORE_ENVIRONMENT=Production
```

---

## API Endpoints

### Autenticação
- `POST /Autenticacao/Registro` - Registrar novo usuário
- `POST /Autenticacao/Login` - Login (cookie)
- `POST /Autenticacao/Token` - Obter JWT token
- `POST /Autenticacao/Logout` - Sair

### Clientes
- `GET /DadosCliente/Buscar` - Listar/buscar clientes
- `GET /DadosCliente/Create` - Formulário de cadastro
- `POST /DadosCliente/Create` - Salvar novo cliente
- `GET /DadosCliente/Detalhes/{id}` - Ver detalhes

### Animais
- `GET /DadosAnimal/BuscarAnimal` - Listar/buscar animais
- `GET /DadosAnimal/CreateAnimal` - Formulário de cadastro
- `POST /DadosAnimal/CreateAnimal` - Salvar novo animal
- `GET /DadosAnimal/EditarAnimal/{id}` - Editar animal

### Consultas
- `GET /DadosConsulta/Agendar` - Formulário agendamento
- `POST /DadosConsulta/Agendar` - Salvar consulta
- `GET /DadosConsulta/Consulta` - Listar consultas
- `GET /DadosConsulta/Editar/{id}` - Editar consulta
- `DELETE /DadosConsulta/Excluir/{id}` - Deletar consulta

---

## 🐛 Troubleshooting

### Erro: "Connection string 'DefaultConnection' is not configured"
**Solução**: Configure User Secrets ou variáveis de ambiente (veja seção 3️⃣ acima)

### Erro: "JWT Key is not configured properly"
**Solução**: Configure a chave JWT com `dotnet user-secrets set "Jwt:Key" "..."`

### Erro: "Unable to connect to MySQL"
**Solução**: 
1. Verifique se MySQL está rodando: `mysql -u root -p -e "SELECT 1;"`
2. Verifique connection string em User Secrets
3. Verifique banco de dados foi criado: `dotnet run` executará migrations automaticamente

### Erro: Database does not exist
**Solução**: Execute migrations:
```bash
dotnet ef database update
```

---

## 🤝 Contribuições

Contribuições são bem-vindas! Por favor:

1. Faça um Fork do repositório
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

---

## 📝 Licença

Este projeto está licenciado sob a Licença MIT - veja [LICENSE](LICENSE) para detalhes.

---

## 👨‍💻 Autor

Desenvolvido por **Matheus Veiga**

- GitHub: [@Matheusveiga](https://github.com/Matheusveiga)
- Linkedin: [Matheus Veiga](https://linkedin.com/in/matheus-veiga)

---

## 📚 Referências & Recursos

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core)
- [OWASP Top 10 Security Risks](https://owasp.org/www-project-top-ten)
- [Serilog Structured Logging](https://serilog.net)
- [JWT.io - JSON Web Tokens](https://jwt.io)

---

⭐ Se este projeto foi útil, considere dar uma estrela! ⭐
