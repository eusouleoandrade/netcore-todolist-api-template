using AutoMapper;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Application.Mappings;
using Core.Application.UseCases;
using Core.Domain.Entities;
using FluentAssertions;
using Infra.Notification.Contexts;
using Moq;
using Xunit;

namespace Tests.Unit.UseCases
{
    public class DeleteTodoUseCaseTest
    {
        private readonly IMapper _mapperMock;
        private readonly Mock<IGenericRepositoryAsync<Todo, int>> _genericRepositoryAsyncMock;
        private readonly Mock<IGetTodoUseCase> _getTodoUseCaseMock;

        public DeleteTodoUseCaseTest()
        {
            // Repository mock
            _genericRepositoryAsyncMock = new Mock<IGenericRepositoryAsync<Todo, int>>();

            // UseCase mock
            _getTodoUseCaseMock = new Mock<IGetTodoUseCase>();

            // Set auto mapper configs
            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Should execute successfully
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="done"></param>
        /// <returns></returns>
        [Theory(DisplayName = "Should execute successfully")]
        [InlineData(1, "Ir ao mercado.", true)]
        [InlineData(2, "Ir ao Dentista.", false)]
        [InlineData(3, "Fazer investimentos.", true)]
        [InlineData(4, "Pagar as contas.", false)]
        public async Task ShouldExecuteSucessfullyAsync(int id, string title, bool done)
        {
            // Arranje
            var getTodoUseCaseResponse = new GetTodoUseCaseResponse(id, title, done);
            _getTodoUseCaseMock.Setup(x => x.RunAsync(It.IsAny<int>())).ReturnsAsync(getTodoUseCaseResponse);

            var deleteGenericRepositoryAsyncResponse = true;
            _genericRepositoryAsyncMock.Setup(x => x.DeleteAsync(It.IsAny<Todo>())).ReturnsAsync(deleteGenericRepositoryAsyncResponse);

            var notificationContext = new NotificationContext();
            var deleteTodoUseCase = new DeleteTodoUseCase(_genericRepositoryAsyncMock.Object, notificationContext, _getTodoUseCaseMock.Object, _mapperMock);

            // Act
            await deleteTodoUseCase.RunAsync(id);

            // Assert
            notificationContext.HasErrorNotification.Should().BeFalse();
            notificationContext.ErrorNotifications.Should().HaveCount(0);
            notificationContext.ErrorNotifications.Should().BeEmpty();
        }

        /// <summary>
        /// Should not execute successfully
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "Should not execute successfully")]
        public async Task ShouldNotExecuteSucessfullyAsync()
        {
            // Arranje
            var getTodoUseCaseResponse = new GetTodoUseCaseResponse(1, "Title", true);
            _getTodoUseCaseMock.Setup(x => x.RunAsync(It.IsAny<int>())).ReturnsAsync(getTodoUseCaseResponse);

            var deleteGenericRepositoryAsyncResponse = false;
            _genericRepositoryAsyncMock.Setup(x => x.DeleteAsync(It.IsAny<Todo>())).ReturnsAsync(deleteGenericRepositoryAsyncResponse);

            var notificationContext = new NotificationContext();
            var deleteTodoUseCase = new DeleteTodoUseCase(_genericRepositoryAsyncMock.Object, notificationContext, _getTodoUseCaseMock.Object, _mapperMock);

            // Act
            await deleteTodoUseCase.RunAsync(1);

            // Assert
            notificationContext.Should().NotBeNull();
            notificationContext.HasErrorNotification.Should().BeTrue();
            notificationContext.ErrorNotifications.Should().HaveCount(1);
            notificationContext.ErrorNotifications.Should().NotBeEmpty();
            notificationContext.ErrorNotifications.Should().ContainSingle();
            notificationContext.ErrorNotifications.Should().Satisfy(e => e.Key == "COD0003" && e.Message == "Failed to remove Todo.");
            notificationContext.SuccessNotifications.Should().BeEmpty();
        }
    }
}