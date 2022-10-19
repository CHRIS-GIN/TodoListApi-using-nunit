using Microsoft.EntityFrameworkCore;
using TodoListApi.Services;
using ToDoListApi.Models;

namespace TodoListApi.Tests;

public class TodoListService_Test
{
    public readonly DbContextOptions<TodoContext> dbContextOptions;

    public TodoListService_Test()
    {
        // Build DbContextOptions
        dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListsDatabase")
            .Options;
    }
    [Test]
    public async Task When_GetAll_Then_It_Should_Return_AllTodoItems()
    {
        var todoContext = new TodoContext(dbContextOptions);
        TodoListService repository = new TodoListService(todoContext);
        var newPost = new ToDoItem()
        {
            Id = 0,
            Timestamp = DateTime.UtcNow,
            Text = "GetAll_Test.",
            Done = false
        };

        // Act
        await repository.AddTodoItem(newPost);
        var result = await repository.GetAllTodoLists();

        //Assert
        Assert.AreEqual(newPost, result.Data.First());

    }

    [Test]
    public async Task When_Post_Is_Saved_Then_It_Should_Insert_New_Entry()
    {
        // Arrange
        var todoContext = new TodoContext(dbContextOptions);
        TodoListService repository = new TodoListService(todoContext);
        var newPost = new ToDoItem()
        {
            Id = 0,
            Timestamp = DateTime.UtcNow,
            Text = "AddTodoItem_Test.",
            Done = false
        };

        // Act
        await repository.AddTodoItem(newPost);

        // Assert
        Assert.AreEqual(1, await todoContext.TodoItems.CountAsync());
    }

    [Test]
    public async Task When_Put_Is_Saved_Then_It_Should_Update_New_TodoItem()
    {
        // Arrange
        var todoContext = new TodoContext(dbContextOptions);
        TodoListService repository = new TodoListService(todoContext);
        var newPost = new ToDoItem()
        {
            Id = 1,
            Timestamp = DateTime.Today,
            Text = "TodoItem_Test.",
            Done = false
        };
        var newPut = new ToDoItem()
        {
            Id = 1,
            Timestamp = DateTime.Today,
            Text = "UpdateTodoItem_Test.",
            Done = true
        };

        // Act
        await repository.AddTodoItem(newPost);
        await repository.UpdateTodoItem(newPut);
        
        // Assert
        Assert.AreEqual(newPost,todoContext.TodoItems.Find());
    }

    [Test]
    public async Task When_Delete_Is_Saved_Then_It_Should_Delete_the_TodoItem()
    {
        // Arrange
        var todoContext = new TodoContext(dbContextOptions);
        TodoListService repository = new TodoListService(todoContext);
        var newPost = new ToDoItem()
        {
            Id = 0,
            Timestamp = DateTime.UtcNow,
            Text = "DeleteTodoItem_Test.",
            Done = false
        };

        // Act
        await repository.AddTodoItem(newPost);
        await repository.DeleteTodoList(newPost.Id);

        // Assert
        Assert.AreEqual(0, await todoContext.TodoItems.CountAsync());
    }
}