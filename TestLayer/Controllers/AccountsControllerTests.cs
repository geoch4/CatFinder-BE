using APILayer.Controllers;
using ApplicationLayer.Users.Commands.UpdateUser;
using ApplicationLayer.Users.DTOs;
using ApplicationLayer.Users.Queries.GetUserbyId;
using DomainLayer.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestLayer.Controllers;

// Tests for AccountsController — covers fetching and updating a user profile.
[TestFixture]
public class AccountsControllerTests
{
    private Mock<ISender> _mediator;      // fake version of MediatR — lets us control what it returns
    private AccountsController _controller; // the real controller we are testing

    // Runs before every test — creates fresh fakes and wires them into the controller.
    [SetUp]
    public void SetUp()
    {
        _mediator = new Mock<ISender>();                        // create a blank fake mediator
        _controller = new AccountsController(_mediator.Object); // inject the fake into the controller
    }

    // A valid account ID is requested and the account exists → controller should return 200 OK.
    [Test]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        // Arrange
        _mediator                                               // tell the fake mediator:
            .Setup(m => m.Send(                                //   when Send() is called
                It.IsAny<GetAccountByIdQuery>(),               //   with any GetAccountByIdQuery
                It.IsAny<CancellationToken>()))                //   and any cancellation token
            .ReturnsAsync(                                     //   then return asynchronously
                OperationResult<AccountResponseDto>.Success(   //   a successful result
                    new AccountResponseDto()));                 //   with an empty account response

        // Act
        var result = await _controller.GetById(1); // call GetById with ID = 1

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>()); // result must be a 200 OK
    }

    // An account ID is requested but no account exists with that ID → controller should return 404 Not Found.
    [Test]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        _mediator                                               // tell the fake mediator:
            .Setup(m => m.Send(                                //   when Send() is called
                It.IsAny<GetAccountByIdQuery>(),               //   with any GetAccountByIdQuery
                It.IsAny<CancellationToken>()))                //   and any cancellation token
            .ReturnsAsync(                                     //   then return asynchronously
                OperationResult<AccountResponseDto>.Failure(   //   a failed result
                    "Account not found."));                    //   with this error message

        // Act
        var result = await _controller.GetById(99); // call GetById with an ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>()); // result must be a 404 Not Found
    }

    // A user updates their profile successfully → controller should return 200 OK with the updated data.
    [Test]
    public async Task Update_ReturnsOk_WhenSuccess()
    {
        // Arrange
        _mediator                                               // tell the fake mediator:
            .Setup(m => m.Send(                                //   when Send() is called
                It.IsAny<UpdateAccountCommand>(),              //   with any UpdateAccountCommand
                It.IsAny<CancellationToken>()))                //   and any cancellation token
            .ReturnsAsync(                                     //   then return asynchronously
                OperationResult<AccountResponseDto>.Success(   //   a successful result
                    new AccountResponseDto()));                 //   with an empty account response

        // Act
        var result = await _controller.Update(1, new UpdateAccountDto()); // call Update with ID = 1 and an empty dto

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>()); // result must be a 200 OK
    }

    // Update is attempted but the account does not exist → controller should return 404 Not Found.
    [Test]
    public async Task Update_ReturnsNotFound_WhenAccountMissing()
    {
        // Arrange
        _mediator                                               // tell the fake mediator:
            .Setup(m => m.Send(                                //   when Send() is called
                It.IsAny<UpdateAccountCommand>(),              //   with any UpdateAccountCommand
                It.IsAny<CancellationToken>()))                //   and any cancellation token
            .ReturnsAsync(                                     //   then return asynchronously
                OperationResult<AccountResponseDto>.Failure(   //   a failed result
                    "Account not found."));                    //   the controller checks this exact message to decide 404 vs 400

        // Act
        var result = await _controller.Update(99, new UpdateAccountDto()); // call Update with an ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>()); // result must be a 404 Not Found
    }

    // Update is attempted but the new data is invalid (e.g. username already taken) → controller should return 400 Bad Request.
    [Test]
    public async Task Update_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        _mediator                                               // tell the fake mediator:
            .Setup(m => m.Send(                                //   when Send() is called
                It.IsAny<UpdateAccountCommand>(),              //   with any UpdateAccountCommand
                It.IsAny<CancellationToken>()))                //   and any cancellation token
            .ReturnsAsync(                                     //   then return asynchronously
                OperationResult<AccountResponseDto>.Failure(   //   a failed result
                    "Username already taken."));               //   a validation error (not "not found") → controller returns 400

        // Act
        var result = await _controller.Update(1, new UpdateAccountDto()); // call Update with a valid ID but bad data

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>()); // result must be a 400 Bad Request
    }
}
