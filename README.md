# 🐾 LH_PET - Sistema de Gestão para Clínicas Veterinárias



O **LH_PET** é um sistema web desenvolvido em ASP.NET MVC voltado para a gestão de **clientes** e **animais** em clínicas veterinárias. O objetivo do projeto é facilitar o controle e visualização de informações essenciais, como cadastro de clientes, vínculo com seus animais e operações de consulta, edição e exclusão.

> 💡 Este projeto está em desenvolvimento e visa aprimorar as habilidades práticas com C#, ASP.NET MVC, Entity Framework e Razor Views.


---

## 📸 Demonstrações
### Tela de Login com autenticação funcional
![image](https://github.com/user-attachments/assets/17f31cac-a4a9-493f-8328-4c8f205e3680)

### 🏠 Tela Inicial
![TelaHome](https://github.com/user-attachments/assets/0e356bbe-16ff-4ca2-a90d-13b875c4ea73)

### 👤 Cadastro de Cliente
![TelaCadastroCliente](https://github.com/user-attachments/assets/04eedf39-873a-421e-bcf7-2ed28ac64d8f)

### 🔍 Consulta de Clientes
![TelaConsultaClientes](https://github.com/user-attachments/assets/439ae459-724e-4420-82e5-d1baed4ebe36)

### 📋 Dados do Cliente e seus Animais
![TelaDadosCliente](https://github.com/user-attachments/assets/1a698cba-03db-44bf-a75e-69f9ca7c168b)

### 🐶 Cadastro de Animal
![TelaCadastroAnimal](https://github.com/user-attachments/assets/ccd5f4b0-461f-4d5e-84cd-ab062a5943d5)

### 🔍 Consulta de Animais
![TelaConsultaAnimal](https://github.com/user-attachments/assets/800235a1-4aed-4ca6-8eb9-bf625727249b)

### ✏️ Edição de Animal
![TelaEdicaoAnimal](https://github.com/user-attachments/assets/9af7348f-a325-48ee-b339-1fd420adf2f7)

### 📋 Agendamento de consultas
![image](https://github.com/user-attachments/assets/f8c17171-92b3-4767-b1c3-7a9b248e3d5d)

### 🔍 Calendario de consultas agendadas
![image](https://github.com/user-attachments/assets/29454af8-ee2e-4c14-b783-be4138f0b6a3)

### ✏️ Tela de edição de consultas
![image](https://github.com/user-attachments/assets/6762584c-0895-46bb-ac88-d6586278b6dc)

---

## ⚙️ Tecnologias Utilizadas

- ASP.NET MVC (.NET 8.0)
- C#
- Entity Framework
- Razor Pages
- SQL Server (LocalDB)
- Bootstrap
- JWT Tokens
- BCrypt

---

## 📂 Funcionalidades

- [x] Cadastro, consulta e edição de clientes
- [x] Relacionamento entre cliente e animais
- [x] Cadastro, consulta, edição e exclusão de animais
- [x] Visualização dos animais de um cliente
- [X] Autenticação e login
- [X] Responsividade para dispositivos móveis
- [X] Agendamento de consultas
- [ ]  Criação e controle de prontuários

---

## 🚀 Como Executar o Projeto

1. Clone o repositório:

```bash
git clone https://github.com/Matheusveiga/LH_PET.git
```

Recomendações para rodar no Codespaces / local:

1. Restaurar e buildar:

```bash
dotnet restore
dotnet build
```

2. Criar banco MySQL e executar o script de schema (arquivo `sql/create_schema.sql`):

```bash
# se tiver mysql instalado
mysql -u root -p < sql/create_schema.sql

# ou usando um container docker MySQL
docker run --name lh-mysql -e MYSQL_ROOT_PASSWORD=Matheus@123 -p 3306:3306 -d mysql:8
# espere iniciar e então rode:
docker exec -i lh-mysql mysql -u root -pMatheus@123 < sql/create_schema.sql
```

3. Ajuste `appsettings.Development.json` se necessário (connection string / jwt key)

4. Rodar a aplicação:

```bash
dotnet run
```

Objetivo do Projeto

Este sistema é um exemplo de aplicação ASP.NET MVC com EF Core, autenticação por cookie e JWT, e boas práticas básicas.


