using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Services;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Commands;
using VacApp_Bovinova_Platform.StaffAdministration.Domain.Model.Queries;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Resources;
using VacApp_Bovinova_Platform.StaffAdministration.Interfaces.REST.Transform;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Aggregates;
using VacApp_Bovinova_Platform.IAM.Domain.Model.Commands;

namespace VacApp.Tests.IntegrationTests
{
    public class StaffControllerIntegrationTests
    {
        private readonly Mock<IStaffCommandService> _commandServiceMock;
        private readonly Mock<IStaffQueryService> _queryServiceMock;
        private readonly StaffController _controller;
        private readonly User _user;

        public StaffControllerIntegrationTests()
        {
            _commandServiceMock = new Mock<IStaffCommandService>();
            _queryServiceMock = new Mock<IStaffQueryService>();

            _user = new User(new SignUpCommand(
                "usuario", "email@email.com", "pass"
            ));

            _controller = new StaffController(_commandServiceMock.Object, _queryServiceMock.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.Items["User"] = _user;
        }

        [Fact]
        public async Task CreateStaff_ReturnsCreated()
        {
            // Arrange
            var staff = new Staff("Juan Perez", 1, _user.Id);
            _commandServiceMock.Setup(x => x.Handle(It.IsAny<CreateStaffCommand>())).ReturnsAsync(staff);

            var resource = new CreateStaffResource("Juan Perez", 1);
            var expectedResource = StaffResourceFromEntityAssembler.ToResourceFromEntity(staff);

            // Act
            var result = await _controller.CreateStaffs(resource);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(expectedResource, createdResult.Value);
        }

        [Fact]
        public async Task GetAllStaff_ReturnsOk()
        {
            // Arrange
            var staffList = new List<Staff> { new Staff("Juan Perez", 1, _user.Id) };
            _queryServiceMock.Setup(x => x.Handle(It.IsAny<GetAllStaffQuery>())).ReturnsAsync(staffList);
            var expectedResources = staffList.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);

            // Act
            var result = await _controller.GetAllStaff();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResources, okResult.Value);
        }

        [Fact]
        public async Task GetStaffById_ReturnsOk()
        {
            // Arrange
            var staff = new Staff("Juan Perez", 1, _user.Id);
            _queryServiceMock.Setup(x => x.Handle(It.IsAny<GetStaffByIdQuery>())).ReturnsAsync(staff);
            var expectedResource = StaffResourceFromEntityAssembler.ToResourceFromEntity(staff);

            // Act
            var result = await _controller.GetStaffById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResource, okResult.Value);
        }

        [Fact]
        public async Task GetStaffByEmployeeStatus_ReturnsOk()
        {
            // Arrange
            var staffList = new List<Staff> { new Staff("Juan Perez", 1, _user.Id) };
            _queryServiceMock.Setup(x => x.Handle(It.IsAny<GetStaffByEmployeeStatusQuery>())).ReturnsAsync(staffList);
            var expectedResources = staffList.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);

            // Act
            var result = await _controller.GetStaffByEmployeeStatus(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResources, okResult.Value);
        }

        [Fact]
        public async Task UpdateStaff_ReturnsOk()
        {
            // Arrange
            var staff = new Staff("Juan Perez", 1, _user.Id);
            _commandServiceMock.Setup(x => x.Handle(It.IsAny<UpdateStaffCommand>())).ReturnsAsync(staff);

            var resource = new UpdateStaffResource("Juan Actualizado", 2);
            var expectedResource = StaffResourceFromEntityAssembler.ToResourceFromEntity(staff);

            // Act
            var result = await _controller.UpdateStaff(1, resource);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedResource, okResult.Value);
        }

        [Fact]
        public async Task DeleteStaff_ReturnsOk()
        {
            // Arrange
            var staff = new Staff("Juan Perez", 1, _user.Id);
            _commandServiceMock.Setup(x => x.Handle(It.IsAny<DeleteStaffCommand>())).ReturnsAsync(staff);

            // Act
            var result = await _controller.DeleteStaff(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equivalent(new { message = "Deleted successfully" }, okResult.Value);
        }
    }
}
