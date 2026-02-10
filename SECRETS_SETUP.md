# 🔒 Configuração de User Secrets

Este projeto armazena credenciais sensíveis em **User Secrets** para desenvolvimento.

## Para configurar localmente:

### 1. Inicialize o arquivo de secrets (execute UMA VEZ):
```bash
dotnet user-secrets init
```

### 2. Configure as credenciais do banco de dados:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1;Port=3306;Database=lh_pet_db;Uid=root;Pwd=SUA_SENHA_AQUI;"
```

### 3. Configure a JWT Key (gerar chave forte):
```bash
# Gere uma chave aleatória forte:
dotnet user-secrets set "Jwt:Key" "sua_chave_jwt_forte_aqui_minimo_32_caracteres_aleatorios"
```

### 4. Verifique os secrets configurados:
```bash
dotnet user-secrets list
```

### 5. Limpar um secret (se necessário):
```bash
dotnet user-secrets remove "NomeDaChave"
```

## ⚠️ Importante

- **Nunca** commit de credenciais reais no repositório
- User Secrets é armazenado em: `%APPDATA%\Microsoft\UserSecrets\` (Windows) ou `~/.microsoft/usersecrets/` (Linux/Mac)
- User Secrets é apenas para DESENVOLVIMENTO
- Para PRODUÇÃO: use variáveis de ambiente ou Azure Key Vault

## Gerando JWT Key Forte

```bash
# Linux/Mac
openssl rand -base64 32

# PowerShell (Windows)
[System.Convert]::ToBase64String([System.Security.Cryptography.RandomNumberGenerator]::GetBytes(32))

# Ou use online: https://generate-random.org/
```

Copie o resultado e configure com:
```bash
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_AQUI"
```
