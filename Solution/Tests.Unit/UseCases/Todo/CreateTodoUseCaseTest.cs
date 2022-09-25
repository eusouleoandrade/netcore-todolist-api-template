using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Interfaces.Repositories;
using Core.Application.Mappings;
using Core.Application.UseCases;
using FluentAssertions;
using Infra.Notification.Contexts;
using Xunit;

namespace Tests.Unit.UseCases.Todo
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

        [Theory(DisplayName = "Should Run Successfully")]
        [InlineData("Ir ao mercado.")]
        [InlineData("Ir ao Dentista.")]
        [InlineData("Fazer investimentos.")]
        [InlineData("Pagar as contas.")]
        public async Task ShouldExecuteSucessfullyAsync(string title)
        {
            // Arranje
            var notificationContext = new NotificationContext();
            var createTodoUseCase = new CreateTodoUseCase(notificationContext, _mapperMock, _todoRepositoryAsyncMock.Object);
            var useCaseRequest = new CreateTodoUseCaseRequest(title);

            // Act
            var useCaseResponse = await createTodoUseCase.RunAsync(useCaseRequest);

            // Assert
            useCaseResponse.Should().BeNull();
        }
    }
}