# Instruções para Configurar o Banco de Dados

## 1. Verificar se o MySQL está rodando
Certifique-se de que o MySQL está instalado e rodando na sua máquina.

## 2. Criar o banco de dados
Execute os seguintes comandos no MySQL:

```sql
CREATE DATABASE customers;
CREATE DATABASE customers_dev;
```

## 3. Executar Migrations
No terminal, dentro da pasta CustomerOrders.API, execute:

```bash
# Adicionar uma migration inicial
dotnet ef migrations add InitialCreate

# Aplicar a migration ao banco
dotnet ef database update
```

## 4. Verificar a conexão
Execute o arquivo de teste:
```bash
dotnet run TestConnection.cs
```

## 5. Configurações importantes

### String de conexão atual:
- **Produção**: `customers`
- **Desenvolvimento**: `customers_dev`
- **Usuário**: `root`
- **Senha**: `secret123!`
- **Porta**: `3306`

### Se você quiser alterar a senha ou usuário:
1. Edite o arquivo `appsettings.json` ou `appsettings.Development.json`
2. Altere os valores `Uid` e `Pwd` na string de conexão

### Exemplo de string de conexão personalizada:
```
"Server=localhost;Port=3306;Database=customers;Uid=seu_usuario;Pwd=sua_senha;"
```

## 6. Problemas comuns e soluções

### Erro: "Unable to connect to any of the specified MySQL hosts"
- Verifique se o MySQL está rodando
- Confirme a porta (padrão é 3306)
- Verifique se o usuário e senha estão corretos

### Erro: "Access denied for user"
- Verifique as credenciais no MySQL
- Certifique-se de que o usuário tem permissões para criar bancos

### Erro: "Database does not exist"
- Crie o banco manualmente no MySQL antes de executar as migrations

