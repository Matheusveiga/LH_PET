# 🗄️ Configuração de Banco de Dados - LH_PET

## ✅ Status: Banco Local Criado e Populado

### Resumo da Instalação

**Data**: 10 de Fevereiro de 2026  
**Banco de Dados**: MySQL 8.0.45  
**Ambiente**: Codespaces  
**Status**: ✅ Pronto para uso

---

## 📊 Dados Populados

O banco foi criado e populado automaticamente com dados de teste para facilitar o desenvolvimento:

### Usuários de Teste (2)
| Username | Email | Senha |
|----------|-------|-------|
| `owner` | owner@lhpet.com | `Senha@123` |
| `veterinario` | vet@lhpet.com | `Senha@123` |

### Clientes (5)
| ID | Nome | CPF | Email |
|----|----|-----|-------|
| 1 | João Silva Santos | 12345678901 | joao@email.com |
| 2 | Maria Oliveira Costa | 10987654321 | maria@email.com |
| 3 | Pedro Ferreira Silva | 11223344556 | pedro@email.com |
| 4 | Ana Carolina Santos | 55443322110 | ana@email.com |
| 5 | Carlos Alberto Lima | 99887766554 | carlos@email.com |

### Animais (11)
Total de 11 animais distribuídos entre os 5 clientes:
- **Tipos**: Cachorro (7), Gato (4)
- **Raças**: Labrador, Siamês, Pinscher, Golden Retriever, Persa, Bulldog, Pastor Alemão, Vira-lata, Boxer, Poodle, Maine Coon
- **Todos com dados completos**: Nome, Tipo, Sexo, Raça, Idade

### Consultas Agendadas (11)
Consultas agendadas nos próximos 10 dias com:
- Vacinações
- Check-ups gerais
- Limpeza de dentes
- Banho e tosa
- Vermifugação
- Exames dermatológicos
- Tratamentos

### Fornecedores (4)
| ID | Nome | CNPJ | Email |
|----|------|------|-------|
| 1 | Ração Premium Ltda | 12345678000195 | vendas@racao.com |
| 2 | MediVet Farmácia | 98765432000187 | farmacia@medivet.com |
| 3 | Equipamentos Clínicos Inc | 56789012000134 | vendas@equip.com |
| 4 | Higiene Pet Supplies | 34567890000156 | contato@higienepet.com |

---

## 🔗 Configuração de Conexão

### Detalhes do Servidor MySQL

```sql
Host: 127.0.0.1
Port: 3306
Database: lh_pet_db
User: root
Password: (sem senha)
```

### User Secrets Configurados

Os seguintes secrets foram armazenados de forma segura:

```bash
ConnectionStrings:DefaultConnection = Server=127.0.0.1;Port=3306;Database=lh_pet_db;Uid=root;
Jwt:Key = [chave aleatória de 256 bits gerada automaticamente]
```

**Verificar secrets:**
```bash
dotnet user-secrets list
```

---

## 🚀 Como Usar

### 1. Iniciar a Aplicação

```bash
cd /workspaces/LH_PET
dotnet run
```

A aplicação será acessível em:
- http://localhost:5000 (HTTP - desenvolvimento)
- https://localhost:5001 (HTTPS)

### 2. Fazer Login

Use uma das contas de teste:

**Conta 1:**
- **Username**: `owner`
- **Senha**: `Senha@123`

**Conta 2:**
- **Username**: `veterinario`
- **Senha**: `Senha@123`

### 3. Explorar Funcionalidades

1. **Clientes**: Ver a lista de 5 clientes pré-cadastrados
2. **Animais**: Visualizar os 11 animais e seus proprietários
3. **Consultas**: Ver as 11 consultas agendadas
4. **Fornecedores**: Acessar os 4 fornecedores cadastrados

---

## 🛠️ Gerenciamento do MySQL

### Ver Status do MySQL

```bash
sudo service mysql status
```

### Iniciar MySQL (se parado)

```bash
sudo service mysql start
```

### Acessar Banco de Dados Diretamente

```bash
sudo mysql -u root lh_pet_db
```

### Executar Queries

```bash
sudo mysql -u root lh_pet_db -e "SELECT * FROM Cliente;"
```

### Backup do Banco

```bash
sudo mysqldump -u root lh_pet_db > backup_lh_pet_db.sql
```

### Restaurar de um Backup

```bash
sudo mysql -u root lh_pet_db < backup_lh_pet_db.sql
```

---

## 📁 Scripts SQL Disponíveis

### `/sql/create_schema.sql`
Cria as 5 tabelas principais do banco:
- Cliente
- Animal
- Consulta
- Fornecedor
- User

### `/sql/seed_data.sql`
Popula o banco com dados de teste (excluído do repositório por padrão)

---

## ⚠️ Notas Importantes

1. **Develop Local Apenas**: Esta configuração é apenas para desenvolvimento local. Para produção, use variáveis de ambiente ou Azure Key Vault.

2. **User Secrets**: Os secrets estão armazenados em: `~/.microsoft/usersecrets/<GuidDoProjeato>/secrets.json`

3. **CPF e CNPJ de Teste**: Os CPFs e CNPJs inseridos são números fictícios apenas para teste. Não use em produção.

4. **Senhas de Teste**: As senhas de teste (`Senha@123`) são armazenadas em hash BCrypt no banco:
   - Hash: `$2a$11$e2Y.KJlKHmyS8rkGlkdAWe5xWnRvuM9xhYeNMzKRk.HvJBLyKPRWO`

---

## 🔐 Segurança

- ✅ MySQL rodando localmente (sem acesso remoto em dev)
- ✅ Credenciais armazenadas em User Secrets (não em código)
- ✅ JWT Key gerada aleatoriamente
- ✅ Senhas com hash BCrypt
- ✅ Foreign Keys configuradas corretamente

---

## 📞 Troubleshooting

### Erro: "Cannot connect to MySQL"

**Solução:**
```bash
# Verificar se MySQL está rodando
sudo service mysql status

# Se não estiver, iniciar
sudo service mysql start
```

### Erro: "Unknown database 'lh_pet_db'"

**Solução:**
```bash
# Recriar banco
sudo mysql -u root -e "CREATE DATABASE lh_pet_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;"

# Executar schema
sudo mysql -u root < /workspaces/LH_PET/sql/create_schema.sql
```

### Erro: "Access denied for user 'root'"

**Solução:**
```bash
# Verificar se há senha configurada
sudo mysql -u root -e "SELECT 1;"

# Se não funcionar, resetar MySQL é necessário
sudo service mysql stop
sudo service mysql start
```

---

## 📚 Próximas Etapas

1. ✅ Banco de dados criado
2. ✅ User Secrets configurados
3. ✅ Dados populados para teste
4. ⏳ Executar migrations do EF Core (se necessário)
5. ⏳ Iniciar a aplicação

Você está pronto para começar a desenvolver! 🎉
