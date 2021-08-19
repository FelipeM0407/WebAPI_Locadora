# WebAPI_Locadora com EntityFrameworkCore.InMemory

# CLIENTES

GET: /v1/clientes/selecionar/{id} -
        Seleciona todos ou apenas um cliente caso o Id seja mencionado na URL.
  
POST: /v1/clientes/cadastrar -
        Cadastra um cliente mencionand apenas o Nome
  
POST: /v1/clientes/inativar/{id} -
        Inativa um cliente especifico, ID obrigatorio
  
POST: /v1/clientes/ativar/{id} -
        Ativa um cliente especifico, ID obrigatorio  
  
# FILMES

GET: /v1/filmes/selecionar/{id} -
        Seleciona todos ou apenas um filme caso o Id seja mencionado na URL.
   
POST: /v1/filmes/cadastrar -
        Cadastra um cliente mencionand apenas o Nome
        
POST: /v1/filmes/inativar/{id} -
        Inativa um filme especifico, ID obrigatorio
        
POST: /v1/filmes/ativar/{id} -
        Ativa um filme especifico, ID obrigatorio 
        
# LOCACOES

POST: /v1/locacoes/alugar - 
        Aluga um filme com base no Id do Cliente e do Filme, faz validações da existencia de ambos e valida se o filme ja esta alugado, ativo e disponivel
        
post: /v1/locacoes/devolver -
        Devolve um filme com base no Id do Cliente e do Filme, faz validações da existencia de ambos e se o filme esta sendo devolvido com atraso. 
  
 
 
  
  
