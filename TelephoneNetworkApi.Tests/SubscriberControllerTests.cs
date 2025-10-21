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
            var saveResource = new SaveSubscriberResource { Name = "Анна", AutomaticTelephoneExchangeIds = new int[1] };
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
            var saveResource = new SaveSubscriberResource
            {
                Name = "Анна",
                AutomaticTelephoneExchangeIds = new int[1]
            };

            var subscriber = new Subscriber { Name = "Анна" };
            subscriber.AtsSubscribers = new List<AtsSubscriber>();
            var serviceResponse = new SubscriberResponse("Ошибка сохранения в базе данных"); // Неуспешный ответ


            _mockMapper.Setup(m => m.Map<SaveSubscriberResource, Subscriber>(saveResource)).Returns(subscriber);
            _mockSubscriberService.Setup(s => s.SaveAsync(It.IsAny<Subscriber>())).ReturnsAsync(serviceResponse);

            // Act
            var result = await _controller.PostAsync(saveResource);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ошибка сохранения в базе данных", badRequestResult.Value);
        }



    }
}
