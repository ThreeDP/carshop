# CarShop
> Projeto com o foco de desenvolver um pequeno projeto de gestão de compra e venda de carros utilizando .net core 6, docker, blazor pages e postgresql.

# Como executar:
Para colocar o projeto em produção é necessário ter instalado `make`, `docker`, setar as variaveis de ambiente descritas no .env-sample e restaurar o dump do banco de dados no container `db`.

### Para subir o projeto:
Execute os seguinte comando para subir o projeto:
```
sudo make
```

### Para derrubar os containers e exclui-los:
```
sudo make clean
```

### Para derrubar os containers e excluir as images:
```
sudo make fclean
```