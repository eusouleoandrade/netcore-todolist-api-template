using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Interfaces.Repositories;
using Core.Application.Mappings;
using Core.Application.UseCases;
using Core.Domain.Entities;
using FluentAssertions;
using Infra.Notification.Contexts;
using Moq;
using Xunit;

namespace Tests.Unit.UseCases.Todos
{
    public class CreateTodoUseCaseTest
    {
        private readonly IMapper _mapperMock;
        private readonly Moq.Mock<ITodoRepositoryAsync> _todoRepositoryAsyncMock;

        public CreateTodoUseCaseTest()
        {
            // Mock repository
            _todoRepositoryAsyncMock = new Moq.Mock<ITodoRepositoryAsync>();

            // Set auto mapper configs
            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData("Ir ao mercado.", 1)]
        [InlineData("Ir ao Dentista.", 2)]
        [InlineData("Fazer investimentos.", 3)]
        [InlineData("Pagar as contas.", 4)]
        public async Task ShouldExecuteSucessfullyAsync(string title, int id)
        {
            // Arranje
            var todoRepositoryResponse = new Todo(id, title, false);
            _todoRepositoryAsyncMock.Setup(x => x.CreateAsync(It.IsAny<Todo>())).ReturnsAsync(todoRepositoryResponse);

            var notificationContext = new NotificationContext();
            var createTodoUseCase = new CreateTodoUseCase(notificationContext, _mapperMock, _todoRepositoryAsyncMock.Object);
            var useCaseRequest = new CreateTodoUseCaseRequest(title);

            // Act
            var useCaseResponse = await createTodoUseCase.RunAsync(useCaseRequest);

            // Assert
            useCaseResponse.Should().NotBeNull();
            useCaseResponse?.Done.Should().BeFalse();
            useCaseResponse?.Title.Should().NotBeNullOrWhiteSpace();
            useCaseResponse?.Title.Should().Be(title);
            useCaseResponse?.Id.Should().NotBe(0);
            useCaseResponse?.Id.Should().Be(id);

            notificationContext.HasErrorNotification.Should().BeFalse();
            notificationContext.ErrorNotifications.Should().HaveCount(0);
            notificationContext.ErrorNotifications.Should().BeEmpty();
        }

        /// <summary>
        /// Should not execute when title is invalid
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should not execute when title is invalid")]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldNotExecute_WhenTitleIsInvalid(string title)
        {
            // Arranje
            var notificationContext = new NotificationContext();
            var createTodoUseCase = new CreateTodoUseCase(notificationContext, _mapperMock, _todoRepositoryAsyncMock.Object);
            var useCaseRequest = new CreateTodoUseCaseRequest(title);

            // Act
            var useCaseResponse = await createTodoUseCase.RunAsync(useCaseRequest);

            // Assert
            useCaseResponse.Should().BeNull();
            useCaseResponse.Should().Be(default);

            notificationContext.Should().NotBeNull();
            notificationContext.HasErrorNotification.Should().BeTrue();
            notificationContext.ErrorNotifications.Should().NotBeEmpty();
            notificationContext.ErrorNotifications.Should().HaveCount(1);
            notificationContext.ErrorNotifications.Should().ContainSingle();
            notificationContext.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0001" && e.Message == "Title is required.");
            notificationContext.SuccessNotifications.Should().BeEmpty();
        }
    }
}