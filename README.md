## DESCRIÇÃO DO PROJETO

WiseMoneyTest é um projeto de API de um banco, que traz as seguintes funcionalidades:
- Criação de usuário,
- Login,
- Criação de conta,
- Verificação de saldo da conta,
- Transferência entre contas,
- Deposito,
- Obtenção de extrato da conta por período.

## DESCRIÇÃO DE ENDPOINTS

ROTA USER

[POST] /api/user
Recebe um JSON com e-mail e senha para a criação de um usuário.
{
  "emailAdress": "string",
  "password": "string"
}

[POST] api/user/login
Recebe um JSON com o usuário(e-mail) e senha, conforme exemplo:
{
  "user": "string",
  "password": "string"
}
Retorna um token, que deve ser utlizado com o esquema Bearer para acessar os próximos endpoints.

ROTA ACCOUNT

[POST] /api/account
Cria uma conta para o usuário que está logado.
Retorna o número da conta.

[GET] api/account/{accountNumber}/balance
Recebe o número da conta como parâmetro, e retorna o saldo da conta consultada.

ROTA TRANSACTIONS

[POST] /api/transaction/deposit
Recebe um JSON com o número da conta e valor do depósito(conforme exemplo), e adiciona o valor ao saldo.
{
  "accountNumber": 0,
  "value": 0
}

[POST] api/transaction/transfer
Recebe um JSON com os números das contas envolvidas na transação, diferenciando a que está enviando e a que está recebendo a transferência, e o valor da transferência, conforme exemplo:
{
  "sendingAccountNumber": 0,
  "receivingAccountNumber": 0,
  "transferValue": 0
}

[GET] api/transaction/{accountNumber}/bankstatement
Recebe como parâmetro o número da conta(accountNumber), e o intervalo das datas que devem ser checadas (startingDate e finishDate).
Retorna uma lista com as transações da conta no período.

## COMO RODAR O PROJETO

UTILIZANDO .NET
Requisitos:
NET Versão 6.0 ou mais atual.

PASSO A PASSO
- Abrir o prompt de comando e navegar até a pasta onde está a raiz do projeto;
- Rodar o comando "dotnet build";
![image](https://user-images.githubusercontent.com/57686224/223191850-90d20728-06e4-4960-9e5a-4d0acf6c772b.png)
- Rodar o comando "dotnet run";
![image](https://user-images.githubusercontent.com/57686224/223192689-60528384-789c-4ae3-8d80-c2d100dbad41.png)
- Para acessar o Swagger, acessar no browser: https://localhost:<porta_em_que_o_projeto_esta_rodando>/swagger/index.html
Exemplo: https://localhost:7298/swagger/index.html

UTILIZANDO DOCKER
Requisitos:
Ter o docker na sua máquina

PASSO A PASSO:  
- Abra o prompt de comando e navegue até a raiz do projeto(onde está o arquivo Dockerfile);
- Rodar o comando: docker build -t <nome_da_imagem_que_deseja_criar> .;
Exemplo: docker build -t wisemoneytest .
- Após criada a imagem, rodar o comando: docker run -p <porta_local>:80 <nome_da_imagem>
Exemplo: docker run -p 5000:80 wisemoneytest
O swagger estará rodando no endereço: http://localhost:<porta_local>/swagger
Exemplo: http://localhost:5000/swagger
