# 🎯 CREDENCIAIS E ACESSO - LH_PET

## 🌐 Acesso à Aplicação

**URL**: [http://localhost:5259](http://localhost:5259)

> **Nota**: Use HTTP durante desenvolvimento. HTTPS pode ter certificado auto-assinado.

---

## 🔑 Credenciais de Teste

### Opção 1: Proprietário
```
Username: owner
Senha: Senha@123
Email: owner@lhpet.com
```

### Opção 2: Veterinário
```
Username: veterinario
Senha: Senha@123
Email: vet@lhpet.com
```

---

## 🗄️ Credenciais do Banco de Dados

### Acesso Direto via MySQL

```bash
# Login como usuário da aplicação
mysql -u lh_pet -p -h 127.0.0.1
# Digite a senha quando solicitado: lh_pet_password_2026

# Login como root (com sudo)
sudo mysql -u root
```

### Detalhes de Conexão

| Campo | Valor |
|-------|-------|
| **Host** | localhost ou 127.0.0.1 |
| **Port** | 3306 |
| **Database** | lh_pet_db |
| **User (App)** | lh_pet |
| **Password (App)** | lh_pet_password_2026 |
| **User (Admin)** | root |
| **Password (Admin)** | (sem senha) |

---

## 📝 Dados Populados

### 5 Clientes
1. João Silva Santos (CPF: 12345678901)
2. Maria Oliveira Costa (CPF: 10987654321)
3. Pedro Ferreira Silva (CPF: 11223344556)
4. Ana Carolina Santos (CPF: 55443322110)
5. Carlos Alberto Lima (CPF: 99887766554)

### 11 Animais
- 7 Cães (Labrador, Pinscher, Golden Retriever, Bulldog, Pastor Alemão, Boxer, Poodle)
- 4 Gatos (Siamês, Persa, Maine Coon, Vira-lata)

### 11 Consultas Agendadas
- Próximas 2 semanas
- Tipos: Vacinação, Check-up, Limpeza, Tosa, Vermifugação, Exame Dermatológico

### 4 Fornecedores
1. Ração Premium Ltda (CNPJ: 12345678000195)
2. MediVet Farmácia (CNPJ: 98765432000187)
3. Equipamentos Clínicos Inc (CNPJ: 56789012000134)
4. Higiene Pet Supplies (CNPJ: 34567890000156)

---

## 🔧 User Secrets Configurados

Os seguintes secrets estão armazenados de forma segura:

```bash
# Verificar
dotnet user-secrets list

# Resultado:
# Jwt:Key = [chave aleatória de 256 bits]
# ConnectionStrings:DefaultConnection = Server=127.0.0.1;Port=3306;...
```

---

## 🚀 Como Usar

### 1. **Login**
Acesse http://localhost:5259 e use uma das credenciais acima

### 2. **Explorar Funcionalidades**

**Menu Principal:**
- 👤 **Clientes** → Cadastrar, Consultar, Ver Detalhes
- 🐶 **Animais** → Cadastrar, Consultar, Editar, Deletar
- 📅 **Consultas** → Agendar, Visualizar, Editar
- 🏢 **Fornecedores** → Cadastrar, Consultar

### 3. **Teste Detalhado**

```
1. Faça login com owner/Senha@123
2. Vá para Clientes → Buscar
3. Escolha um cliente (ex: João Silva Santos)
4. Clique em "Detalhes" para ver seus animais
5. Volte e vá para Consultas → Agendar
6. Agende uma nova consulta
7. Liste todas as consultas agendadas
```

---

## 📊 Estatísticas

| Entidade | Quantidade |
|----------|-----------|
| Usuários | 2 |
| Clientes | 5 |
| Animais | 11 |
| Consultas | 11 |
| Fornecedores | 4 |

---

## 🔐 Segurança Implementada

✅ **Senhas com Hash BCrypt**
- Armazenadas como hash, nunca em texto plano
- Validação de força de senha (8+ caracteres)

✅ **Autenticação por Cookie + JWT**
- Cookies HttpOnly para web
- JWT para APIs futuras

✅ **Rate Limiting**
- 5 tentativas de login por 15 minutos
- Proteção contra força bruta

✅ **User Secrets**
- Credenciais não no repositório
- Apenas em máquina local do desenvolvedor

✅ **Validação de Dados**
- CPF com dígito verificador
- CNPJ com dígito verificador
- Email validado

✅ **CSRF Protection**
- Tokens Anti-CSRF em todos os forms

---

## 📚 Documentação Relacionada

- [DATABASE_SETUP.md](./DATABASE_SETUP.md) - Setup completo do banco
- [SECRETS_SETUP.md](./SECRETS_SETUP.md) - Gerenciamento de secrets
- [README.md](./README.md) - Guia geral do projeto

---

## ⚠️ Importante

**Estas são credenciais de DESENVOLVIMENTO apenas.**

Para PRODUÇÃO:
- ❌ Nunca use essas senhas
- ❌ Use variáveis de ambiente ou Azure Key Vault
- ❌ Use HTTPS obrigatório
- ❌ Implemente rate limiting em produção
- ❌ Use autenticação segura (OAuth2, SAML, etc)

---

## 🆘 Troubleshooting

### Erro: "Access denied for user 'lh_pet'"
```bash
# Reconectar ao MySQL
sudo mysql -u root
```

```sql
GRANT ALL PRIVILEGES ON lh_pet_db.* TO 'lh_pet'@'localhost';
FLUSH PRIVILEGES;
```

### Erro: "Port 5259 already in use"
```bash
sudo pkill -f "dotnet run"
```

### Erro: "Cannot connect to database"
```bash
# Verificar se MySQL está rodando
sudo service mysql status

# Se não estiver, iniciar
sudo service mysql start
```

---

Qualquer dúvida, consulte os arquivos de documentação! 📚
