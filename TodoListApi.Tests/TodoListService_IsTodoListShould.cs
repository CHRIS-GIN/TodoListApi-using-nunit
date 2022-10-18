using Microsoft.EntityFrameworkCore;
using TodoListApi.Services;
using ToDoListApi.Models;

namespace TodoListApi.Tests;

public class TodoListService_IsTodoListShould
{
    private TodoListService _todoListService;
    [SetUp]
    public void Setup()
    {
        //_todoListService = new TodoListService<List<ToDoItem>>().UseInMemoryDatabase(databaseName: "TodoListsInMemory").options;

    }

    [Test]
    public void Should_GetAllTodoLists_ReturnTodoLists()
    {
        // var result = _todoListService.GetAllTodoLists();
        // Assert.AreEqual(result,);
    }
}
public class TodoListService_AddTodoItemTest
{
    public readonly DbContextOptions<TodoContext> dbContextOptions;

    public TodoListService_AddTodoItemTest()
    {
        // Build DbContextOptions
        dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoListsDatabase")
            .Options;
    }

    [Test]
    public async Task WhenPostIsSavedThenItShouldInsertNewEntry()
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
}