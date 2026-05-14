using APILayer.Controllers;
using ApplicationLayer.Location.DTOs;
using ApplicationLayer.Location.Queries.GetAllLocations;
using ApplicationLayer.Location.Queries.GetLocationbyId;
using DomainLayer.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestLayer.Controllers;

// Tests for LocationsController — covers fetching all locations and fetching a single location by ID.
// Locations are pre-defined options users pick from when creating an advertisement.
[TestFixture]
public class LocationsControllerTests
{
    private Mock<ISender> _mediator;        // fake version of MediatR — lets us control what it returns
    private LocationsController _controller; // the real controller we are testing

    // Runs before every test — creates fresh fakes and wires them into the controller.
    [SetUp]
    public void SetUp()
    {
        _mediator = new Mock<ISender>();                            // create a blank fake mediator
        _controller = new LocationsController(_mediator.Object);   // inject the fake into the controller
    }

    // All locations are requested → controller should always return 200 OK (even if the list is empty).
    [Test]
    public async Task GetAll_ReturnsOk_Always()
    {
        // Arrange
        _mediator                                                              // tell the fake mediator:
            .Setup(m => m.Send(                                               //   when Send() is called
                It.IsAny<GetAllLocationsQuery>(),                             //   with any GetAllLocationsQuery
                It.IsAny<CancellationToken>()))                               //   and any cancellation token
            .ReturnsAsync(                                                    //   then return asynchronously
                OperationResult<List<LocationResponseDto>>.Success([]));      //   a successful result with an empty list

        // Act
        var result = await _controller.GetAll(); // call GetAll with no arguments

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>()); // result must be a 200 OK
    }

    // A valid location ID is requested and it exists → controller should return 200 OK.
    [Test]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        // Arrange
        _mediator                                                        // tell the fake mediator:
            .Setup(m => m.Send(                                          //   when Send() is called
                It.IsAny<GetLocationByIdQuery>(),                        //   with any GetLocationByIdQuery
                It.IsAny<CancellationToken>()))                          //   and any cancellation token
            .ReturnsAsync(                                               //   then return asynchronously
                OperationResult<LocationResponseDto>.Success(            //   a successful result
                    new LocationResponseDto()));                          //   with an empty location response

        // Act
        var result = await _controller.GetById(1); // call GetById with ID = 1

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>()); // result must be a 200 OK
    }

    // A location ID is requested but no location exists with that ID → controller should return 404 Not Found.
    [Test]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        _mediator                                                        // tell the fake mediator:
            .Setup(m => m.Send(                                          //   when Send() is called
                It.IsAny<GetLocationByIdQuery>(),                        //   with any GetLocationByIdQuery
                It.IsAny<CancellationToken>()))                          //   and any cancellation token
            .ReturnsAsync(                                               //   then return asynchronously
                OperationResult<LocationResponseDto>.Failure(            //   a failed result
                    "Location not found."));                             //   with this error message

        // Act
        var result = await _controller.GetById(99); // call GetById with an ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>()); // result must be a 404 Not Found
    }
}
