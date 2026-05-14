using APILayer.Controllers;
using ApplicationLayer.Cat.Commands.CreateCat;
using ApplicationLayer.Cat.Commands.DeleteCat;
using ApplicationLayer.Cat.Commands.UpdateCat;
using ApplicationLayer.Cat.DTOs;
using ApplicationLayer.Cat.Queries.GetCatbyId;
using DomainLayer.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestLayer.Controllers;

// Tests for CatsController — covers fetching, creating, updating, and deleting cats.
// A cat must exist before a Lost/Found advertisement can be posted for it.
[TestFixture]
public class CatsControllerTests
{
    private Mock<ISender> _mediator;   // fake version of MediatR — lets us control what it returns
    private CatsController _controller; // the real controller we are testing

    // Runs before every test — creates fresh fakes and wires them into the controller.
    [SetUp]
    public void SetUp()
    {
        _mediator = new Mock<ISender>();                     // create a blank fake mediator
        _controller = new CatsController(_mediator.Object); // inject the fake into the controller
    }

    // A valid cat ID is requested and the cat exists → controller should return 200 OK.
    [Test]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        // Arrange
        _mediator                                          // tell the fake mediator:
            .Setup(m => m.Send(                           //   when Send() is called
                It.IsAny<GetCatByIdQuery>(),              //   with any GetCatByIdQuery
                It.IsAny<CancellationToken>()))           //   and any cancellation token
            .ReturnsAsync(                                //   then return asynchronously
                OperationResult<CatResponseDto>.Success(  //   a successful result
                    new CatResponseDto()));                //   with an empty cat response

        // Act
        var result = await _controller.GetById(1); // call GetById with ID = 1

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>()); // result must be a 200 OK
    }

    // A cat ID is requested but no cat exists with that ID → controller should return 404 Not Found.
    [Test]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        _mediator                                          // tell the fake mediator:
            .Setup(m => m.Send(                           //   when Send() is called
                It.IsAny<GetCatByIdQuery>(),              //   with any GetCatByIdQuery
                It.IsAny<CancellationToken>()))           //   and any cancellation token
            .ReturnsAsync(                                //   then return asynchronously
                OperationResult<CatResponseDto>.Failure(  //   a failed result
                    "Cat not found."));                   //   with this error message

        // Act
        var result = await _controller.GetById(99); // call GetById with an ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>()); // result must be a 404 Not Found
    }

    // A new cat is registered successfully → controller should return 201 Created.
    [Test]
    public async Task Create_ReturnsCreated_WhenSuccess()
    {
        // Arrange
        _mediator                                          // tell the fake mediator:
            .Setup(m => m.Send(                           //   when Send() is called
                It.IsAny<CreateCatCommand>(),             //   with any CreateCatCommand
                It.IsAny<CancellationToken>()))           //   and any cancellation token
            .ReturnsAsync(                                //   then return asynchronously
                OperationResult<CatResponseDto>.Success(  //   a successful result
                    new CatResponseDto { CatId = 1 }));   //   with CatId set (needed for the 201 location header)

        // Act
        var result = await _controller.Create(            // call Create with a dto
            new CreateCatDto { FurColor = "Orange" });    //   FurColor is required so we give it a value

        // Assert
        Assert.That(result, Is.InstanceOf<CreatedAtActionResult>()); // result must be a 201 Created
    }

    // Creating a cat fails (e.g. the account that owns it does not exist) → controller should return 400 Bad Request.
    [Test]
    public async Task Create_ReturnsBadRequest_WhenFailure()
    {
        // Arrange
        _mediator                                          // tell the fake mediator:
            .Setup(m => m.Send(                           //   when Send() is called
                It.IsAny<CreateCatCommand>(),             //   with any CreateCatCommand
                It.IsAny<CancellationToken>()))           //   and any cancellation token
            .ReturnsAsync(                                //   then return asynchronously
                OperationResult<CatResponseDto>.Failure(  //   a failed result
                    "Account not found."));               //   with this error message

        // Act
        var result = await _controller.Create(            // call Create with a dto
            new CreateCatDto { FurColor = "Orange" });    //   FurColor is required so we give it a value

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>()); // result must be a 400 Bad Request
    }

    // A cat's details are updated successfully → controller should return 200 OK with the updated data.
    [Test]
    public async Task Update_ReturnsOk_WhenSuccess()
    {
        // Arrange
        _mediator                                          // tell the fake mediator:
            .Setup(m => m.Send(                           //   when Send() is called
                It.IsAny<UpdateCatCommand>(),             //   with any UpdateCatCommand
                It.IsAny<CancellationToken>()))           //   and any cancellation token
            .ReturnsAsync(                                //   then return asynchronously
                OperationResult<CatResponseDto>.Success(  //   a successful result
                    new CatResponseDto()));                //   with an empty cat response

        // Act
        var result = await _controller.Update(1, new UpdateCatDto()); // call Update with ID = 1 and an empty dto

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>()); // result must be a 200 OK
    }

    // Update is attempted but the cat does not exist → controller should return 404 Not Found.
    [Test]
    public async Task Update_ReturnsNotFound_WhenCatMissing()
    {
        // Arrange
        _mediator                                          // tell the fake mediator:
            .Setup(m => m.Send(                           //   when Send() is called
                It.IsAny<UpdateCatCommand>(),             //   with any UpdateCatCommand
                It.IsAny<CancellationToken>()))           //   and any cancellation token
            .ReturnsAsync(                                //   then return asynchronously
                OperationResult<CatResponseDto>.Failure(  //   a failed result
                    "Cat not found."));                   //   the controller checks this exact message to decide 404 vs 400

        // Act
        var result = await _controller.Update(99, new UpdateCatDto()); // call Update with an ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>()); // result must be a 404 Not Found
    }

    // Update is attempted but the data is invalid (e.g. fur colour is missing) → controller should return 400 Bad Request.
    [Test]
    public async Task Update_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        _mediator                                          // tell the fake mediator:
            .Setup(m => m.Send(                           //   when Send() is called
                It.IsAny<UpdateCatCommand>(),             //   with any UpdateCatCommand
                It.IsAny<CancellationToken>()))           //   and any cancellation token
            .ReturnsAsync(                                //   then return asynchronously
                OperationResult<CatResponseDto>.Failure(  //   a failed result
                    "FurColor is required."));            //   a validation error (not "not found") → controller returns 400

        // Act
        var result = await _controller.Update(1, new UpdateCatDto()); // call Update with a valid ID but bad data

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>()); // result must be a 400 Bad Request
    }

    // A cat is deleted successfully → controller should return 204 No Content.
    [Test]
    public async Task Delete_ReturnsNoContent_WhenSuccess()
    {
        // Arrange
        _mediator                                   // tell the fake mediator:
            .Setup(m => m.Send(                    //   when Send() is called
                It.IsAny<DeleteCatCommand>(),      //   with any DeleteCatCommand
                It.IsAny<CancellationToken>()))    //   and any cancellation token
            .ReturnsAsync(                         //   then return asynchronously
                OperationResult<bool>.Success(true)); //   a successful result

        // Act
        var result = await _controller.Delete(1); // call Delete with ID = 1

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>()); // result must be a 204 No Content
    }

    // Delete is attempted but the cat does not exist → controller should return 404 Not Found.
    [Test]
    public async Task Delete_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        _mediator                                   // tell the fake mediator:
            .Setup(m => m.Send(                    //   when Send() is called
                It.IsAny<DeleteCatCommand>(),      //   with any DeleteCatCommand
                It.IsAny<CancellationToken>()))    //   and any cancellation token
            .ReturnsAsync(                         //   then return asynchronously
                OperationResult<bool>.Failure(     //   a failed result
                    "Cat not found."));            //   with this error message

        // Act
        var result = await _controller.Delete(99); // call Delete with an ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>()); // result must be a 404 Not Found
    }
}
