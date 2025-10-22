using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TelephoneNetworkApi.Controllers;
using TelephoneNetworkApi.Domain.Models;
using TelephoneNetworkApi.Resourse;
using TelephoneNetworkApi.Services;
using TelephoneNetworkApi.Services.Communication;

namespace TelephoneNetworkApi.Tests
{
    public class SubscriberControllerTests
    {
        private readonly Mock<ISubscriberService> _mockSubscriberService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SubscriberController _controller;

        public SubscriberControllerTests()
        {
            // Arrange (Общая часть для всех тестов)

            _mockSubscriberService = new Mock<ISubscriberService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new SubscriberController(_mockSubscriberService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfSubscriberResources()
        {
            // Arrange

            var subscribers = new List<Subscriber>
            {
                new Subscriber { Id = 1, Name = "Иван" },
                new Subscriber { Id = 2, Name = "Петр" }
            };

            var resources = new List<SubscriberResourse>
            {
                new SubscriberResourse { Id = 1, Name = "Иван" },
                new SubscriberResourse { Id = 2, Name = "Петр" }
            };

            _mockSubscriberService.Setup(s => s.ListAsync()).ReturnsAsync(subscribers);
            _mockMapper.Setup(m => m.Map<IEnumerable<Subscriber>, IEnumerable<SubscriberResourse>>(subscribers)).Returns(resources);

            // Act

            var result = await _controller.GetAllAsync();

            // Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.IsAssignableFrom<IEnumerable<SubscriberResourse>>(result);
        }

        [Fact]
        public async Task PostAsync_WhenModelStateIsValidAndServiceSucceeds_ShouldReturnOkResult()
        {
            // Arrange
            var saveResource = new SaveSubscriberResource { Name = "Анна" };
            var subscriber = new Subscriber { Id = 10, Name = "Анна" };
            var subscriberResource = new SubscriberResourse { Id = 10, Name = "Анна" };
            var serviceResponse = new SubscriberResponse(subscriber);

            _mockMapper.Setup(m => m.Map<SaveSubscriberResource, Subscriber>(saveResource)).Returns(subscriber);
            _mockMapper.Setup(m => m.Map<Subscriber, SubscriberResourse>(subscriber)).Returns(subscriberResource);

            // It.IsAny<Subscriber>() означает, что мы не проверяем точный экземпляр,
            // а ожидаем любой объект типа Subscriber
            _mockSubscriberService.Setup(s => s.SaveAsync(It.IsAny<Subscriber>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.PostAsync(saveResource);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResource = Assert.IsType<SubscriberResourse>(okResult.Value);
            Assert.Equal(10, returnedResource.Id);
        }

        [Fact]
        public async Task PostAsync_WhenModelStateIsInvalid_ShouldReturnBadRequest()
        {
            // Arrange
            // Добавляем ошибку в ModelState, чтобы симулировать невалидные данные
            _controller.ModelState.AddModelError("Name", "Required");
            var saveResource = new SaveSubscriberResource();

            // Act
            var result = await _controller.PostAsync(saveResource);

            // Assert
            // Проверяем, что результат - это BadRequestObjectResult
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task PostAsync_WhenServiceFails_ShouldReturnBadRequestWithMessage()
        {
            // Arrange
            var saveResource = new SaveSubscriberResource { Name = "Анна" };
            var subscriber = new Subscriber { Name = "Анна" };
            var serviceResponse = new SubscriberResponse("Ошибка сохранения в базе данных"); // Неуспешный ответ

            _mockMapper.Setup(m => m.Map<SaveSubscriberResource, Subscriber>(saveResource)).Returns(subscriber);
            _mockSubscriberService.Setup(s => s.SaveAsync(It.IsAny<Subscriber>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.PostAsync(saveResource);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ошибка сохранения в базе данных", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteAsync_WhenModelStateIsValidAndServiceSucceeds_ShouldReturnOkResult()
        {
            // Arrange
            var subscriberIdToDelete = 10;
            var subscriberFromDb = new Subscriber { Id = subscriberIdToDelete, Name = "Анна" };
            var successfulServiceResponse = new SubscriberResponse(subscriberFromDb);
            var subscriberResourceToReturn = new SubscriberResourse { Id = subscriberIdToDelete, Name = "Анна" };

            _mockSubscriberService
                .Setup(s => s.DeleteAsync(subscriberIdToDelete))
                .ReturnsAsync(successfulServiceResponse);

            _mockMapper
                .Setup(m => m.Map<Subscriber, SubscriberResourse>(subscriberFromDb))
                .Returns(subscriberResourceToReturn);

            // Act
            var result = await _controller.DeleteAsync(subscriberIdToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResource = Assert.IsType<SubscriberResourse>(okResult.Value);
            Assert.Equal(subscriberIdToDelete, returnedResource.Id);
        }

        [Fact]
        public async Task DeleteAsync_WhenModelStateIsInvalid_ShouldReturnBadRequest()
        {
            // Arrange
            var nonExistentSubscriberId = 99;
            var errorMessage = "Абонент с таким ID не найден.";
            var failedServiceResponse = new SubscriberResponse(errorMessage);

            _mockSubscriberService
                .Setup(s => s.DeleteAsync(nonExistentSubscriberId))
                .ReturnsAsync(failedServiceResponse);

            // Act
            var result = await _controller.DeleteAsync(nonExistentSubscriberId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMessage, badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteAsync_WhenServiceThrowsException_ShouldLetExceptionBubbleUp()
        {
            // Arrange

            var subscriberId = 1;
            var exceptionToThrow = new Exception("Ошибка подключения к базе данных"); 

            _mockSubscriberService
                .Setup(s => s.DeleteAsync(subscriberId))
                .ThrowsAsync(exceptionToThrow);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _controller.DeleteAsync(subscriberId);
            });

            Assert.Equal("Ошибка подключения к базе данных", exception.Message);
        }
    }
}