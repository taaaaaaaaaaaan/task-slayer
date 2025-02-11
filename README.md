# Task Slayer

Task Slayer é uma aplicação web desenvolvida em C# que visa auxiliar no gerenciamento de tarefas pessoais e profissionais. Utilizando o padrão de design Model-View-ViewModel (MVVM), a aplicação oferece uma interface intuitiva para organizar suas atividades diárias.

## Funcionalidades

- **Gerenciamento de Tarefas**: Criação, edição e exclusão de tarefas com facilidade.
- **Categorias**: Organização de tarefas em diferentes categorias para facilitar a visualização.

## Tecnologias Utilizadas

- **Linguagem**: C#
- **Framework**: ASP.NET Core
- **Banco de Dados**: PostgreSQL
- **ORM**: Npgsql (Entity Framework Core)
- **Padrão de Design**: MVVM (Model-View-ViewModel)
- **Frontend**: HTML, CSS, JavaScript

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

- **Data/**: Contém classes relacionadas ao acesso e manipulação de dados.
- **Pages/**: Inclui as páginas da interface do usuário.
- **Properties/**: Contém arquivos de configuração do projeto.
- **Services/**: Inclui serviços que implementam a lógica de negócios.
- **ViewModels/**: Contém os modelos de visualização utilizados no padrão MVVM.
- **wwwroot/**: Arquivos estáticos como CSS, JavaScript e imagens.

## Como Executar o Projeto

1. **Pré-requisitos**:

   - [.NET SDK 9](https://dotnet.microsoft.com/pt-br/download/dotnet/9.0) instalado na máquina.
   - [PostgreSQL](https://www.enterprisedb.com/downloads/postgres-postgresql-downloads) instalado e configurado.

2. **Clonar o Repositório**:

   ```bash
   git clone https://github.com/taaaaaaaaaaaan/task-slayer.git
   cd task-slayer
   ```

3. **Restaurar Dependências**:

   ```bash
   dotnet restore
   ```

4. **Compilar o Projeto**:

   ```bash
   dotnet build
   ```

5. **Atualizar o Banco de Dados**:

   ```bash
   dotnet ef database update
   ```

6. **Executar a Aplicação**:

   ```bash
   dotnet run
   ```

7. **Acessar no Navegador**: Abra o navegador e acesse `http://localhost:5000` para utilizar o Task Slayer.

